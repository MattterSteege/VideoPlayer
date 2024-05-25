//When you minify the JS code use this website: https://www.digitalocean.com/community/tools/minify, with the following settings:
//  - Compress
//      - dead_code, drop_console, drop_debugger
//  - Mangle
//      - keep_classnames, keep_fnames, toplevel, safari10

//need to implement the following functions
// - When user adjusts the video time and the time is outside the buffer, fetch needed segment (in progress) need the /{guid}/mpd endpoint for segment lengths (partial)
// - make a queue for fetching segments 

class VideoPlayer {
    
    constructor(video_id) {
        //set by user - shallow
        this.videoId = video_id;
        this.minBuffer = 5; //seconds
        this.AutoBuffer = true;
        this.triggerSeekedEnded = 250; //ms
        
        logLevel = 0b00000111; // 0b00000001 - Log, 0b00000010 - Debug, 0b00000100 - Error
        
        //set by user - deep
        this.mimeCodec_audio = "audio/mp4; codecs = mp4a.40.2";
        this.mimeCodec_video = "video/mp4; codecs = avc1.4D401F";
        
        //set / changed internally
        this.mediaSource;
        
        this.sourceBuffer_audio;
        this.maxSegNum_audio = 0;
        this.loadedSegments_audio = [];
        
        this.sourceBuffer_video;
        this.maxSegNum_video = 0;
        this.loadedSegments_video = [];
        
        this.manifest = null;

        //MANIFEST
        fetch("api/Video/" + this.videoId + "/manifest")
        .then(response => response.json())
        .then(data => {

            this.manifest = this.parseManifest(data);
            
            //calc the amount of video segments
            this.manifest.period.adaptationSet[0].segmentTemplate.segmentTimeline.forEach(segment => {
                this.maxSegNum_video += segment.repeatAfter + 1;
                this.loadedSegments_video = new Array(this.maxSegNum_video).fill(false);
            });
            
            //calc the amount of audio segments
            this.manifest.period.adaptationSet[1].segmentTemplate.segmentTimeline.forEach(segment => {
                this.maxSegNum_audio += segment.repeatAfter + 1;
                this.loadedSegments_audio = new Array(this.maxSegNum_audio).fill(false);
            });

            VideoDebug("Loaded manifest file:", data);
            this.canPlay = true;
        });
        
        this.canPlay = false;
        
        VideoLog("VideoPlayer is created with next settings:\n                 -   videoId: " + this.videoId + ",\n                 -   minBuffer: " + this.minBuffer + ",\n                 -   AutoBuffer: " + this.AutoBuffer + ",\n                 -   mimeCodec_audio: " + this.mimeCodec_audio + ",\n                 -   mimeCodec_video: " + this.mimeCodec_video);
    }

    startup() {

        VideoDebug("VideoPlayer is queued...");

        waitForObjectState(() => this.canPlay)
        .then(() => {
            VideoDebug("VideoPlayer is starting");
            this.startupInternal();
        })
        .catch((error) => {
            VideoError("An error occurred:", error);
        });
    }

    startupInternal() {
        if (MediaSource.isTypeSupported(this.mimeCodec_video)) {
            this.mediaSource = new MediaSource();
            
            this.mediaSource.addEventListener("webkitsourceopen", (evt) => {
                VideoLog("MediaSource opened (type of webkitsourceopen)");
                this.sourceOpen(evt);
            }, false);
            
            this.mediaSource.addEventListener('sourceopen', (evt) => {
                VideoLog('MediaSource opened (type of sourceopen)');
                this.sourceOpen(evt);
            }, false);
            
            this.video = document.querySelector('video');
            this.video.src = URL.createObjectURL(this.mediaSource);
            this.video.ontimeupdate = () => this.checkBuffer();
            this.video.onseeking  = (evt) => this.Seeking(evt);
            
        } else {
            VideoError('Unsupported MIME type or codec: ', this.mimeCodec_video);
        }
    }
    
    sourceOpen(e) {
        this.sourceBuffer_video = this.mediaSource.addSourceBuffer(this.mimeCodec_video);
        this.sourceBuffer_video.mode = 'segments';
        
        this.sourceBuffer_audio = this.mediaSource.addSourceBuffer(this.mimeCodec_audio);
        this.sourceBuffer_audio.mode = 'segments';
        
        var self = this; // Store a reference to 'this' for fetch callback

        VideoDebug('Source buffers created', this.sourceBuffer_video, this.sourceBuffer_audio);

        var init = 0;
        
        this.fetch("api/Video/" + this.videoId + "/video/init.mp4", function (buf) {
            self.sourceBuffer_video.appendBuffer(buf);
            self.loadedSegments_video[0] = true;
            init++;
        });

        this.fetch("api/Video/" + this.videoId + "/audio/init.mp4", function (buf) {
            self.sourceBuffer_audio.appendBuffer(buf);
            self.loadedSegments_audio[0] = true;
            init++;
        });

        waitForObjectState(() => init === 2)
        .then(() => {
            VideoDebug("Init segments loaded");
            this.loadSpecificSegment(1);
        });

        //VIDEO QUEUE
        this.sourceBuffer_video.addEventListener('updateend', () => {
            this.canLoadVideoSegment = true;
            this.loadTroughVideoQueue();
        });
        this.loadTroughVideoQueue();
        
        //AUDIO QUEUE
        this.sourceBuffer_audio.addEventListener('updateend', () => {
            this.canLoadAudioSegment = true;
            this.loadTroughAudioQueue();
        });
        this.loadTroughAudioQueue();
    };
    
    loadSpecificSegment(segNum) {
        this.getAudioSegment(segNum);
        this.getVideoSegment(segNum);
    }

    
    videoSegmentsQueued = [];
    canLoadVideoSegment = true;
    
    getVideoSegment(segNum) {
        if (this.videoSegmentsQueued.includes(segNum)) return;

        VideoDebug("Queuing video segment: " + segNum);
        this.videoSegmentsQueued.push(segNum);
    }
    
    loadTroughVideoQueue() {
        VideoDebug("Waiting for queued video segments...");
        waitForObjectState(() => this.canLoadVideoSegment && this.videoSegmentsQueued.length > 0)
        .then(() => {
            this.canLoadVideoSegment = false;
            let segNum = this.videoSegmentsQueued.shift();

            VideoDebug("Loading video segment: " + segNum);

            var self = this; // Store a reference to 'this' for fetch callback
            this.fetch("api/Video/" + this.videoId + "/video/seg-" + segNum + ".m4s", function (buf) {
                self.sourceBuffer_video.appendWindowStart = 0;
                self.sourceBuffer_video.appendWindowEnd = self.timeBeforeSegment(segNum, "video") + self.manifest.period.adaptationSet[0].segmentTemplate.betterTimeline[segNum - 1].duration;
                self.sourceBuffer_video.appendBuffer(buf);
                self.loadedSegments_video[segNum] = true;
            });
        });
    }
    
    audioSegmentsQueued = [];
    canLoadAudioSegment = true;
    
    getAudioSegment(segNum) {        
        if (this.audioSegmentsQueued.includes(segNum)) return;

        VideoDebug("Queuing audio segment: " + segNum);
        this.audioSegmentsQueued.push(segNum);
    }
    
    loadTroughAudioQueue() {
        VideoDebug("Waiting for queued audio segments...");
        waitForObjectState(() => this.canLoadAudioSegment && this.audioSegmentsQueued.length > 0)
        .then(() => {
            this.canLoadAudioSegment = false;
            let segNum = this.audioSegmentsQueued.shift();

            VideoDebug("Loading audio segment: " + segNum);

            var self = this; // Store a reference to 'this' for fetch callback
            this.fetch("api/Video/" + this.videoId + "/audio/seg-" + segNum + ".m4s", function (buf) {
                self.sourceBuffer_audio.appendWindowStart = 0;
                self.sourceBuffer_audio.appendWindowEnd = self.timeBeforeSegment(segNum, "audio") + self.manifest.period.adaptationSet[1].segmentTemplate.betterTimeline[segNum - 1].duration;
                self.sourceBuffer_audio.appendBuffer(buf);
                self.loadedSegments_audio[segNum] = true;
            });
        });
    }
    
    timeBeforeSegment(segNum, type) {
        var time = 0;
        if (type === "audio") {
            if (segNum === 1)
                return 0;
            
            if (segNum > this.maxSegNum_audio)
                return -1;
            
            for (var i = 1; i < segNum; i++) {
                time += this.manifest.period.adaptationSet[1].segmentTemplate.betterTimeline[i - 1].duration;
            }
        }
        else {
            if (segNum === 1) 
                return 0;
            
            if (segNum > this.maxSegNum_video) 
                return -1;
            
            for (var i = 1; i < segNum; i++) {
                time += this.manifest.period.adaptationSet[0].segmentTemplate.betterTimeline[i - 1].duration;
            }
        }
        return time;
    }
    
    fetch(url, cb) {
        VideoDebug("Fetching: " + url);
        var xhr = new XMLHttpRequest;
        xhr.open('get', url);
        xhr.responseType = 'arraybuffer';
        xhr.onload = function () {
            cb(xhr.response);
        };
        xhr.send();
    }

    checkBuffer(forced = false) {
        
        if (!this.AutoBuffer && forced === false) return;
        
        //AUTO BUFFER VIDEO
        var currentTime = this.video.currentTime;
        var currentSegment = this.calculateSegmentForTime(currentTime, "video");
        var segmentToLoad = currentSegment + 2;
        
        if (segmentToLoad > this.maxSegNum_video) return;
        if (this.loadedSegments_video[segmentToLoad] === true) return;
        
        if (currentTime >= this.timeBeforeSegment(segmentToLoad, "video") - this.minBuffer) {
            this.loadSpecificSegment(segmentToLoad);
        }      
        
        //AUTO BUFFER AUDIO
        currentSegment = this.calculateSegmentForTime(currentTime, "audio");
        segmentToLoad = currentSegment + 2;
        
        if (segmentToLoad > this.maxSegNum_audio) return;
        if (this.loadedSegments_audio[segmentToLoad] === true) return;
        
        if (currentTime >= this.timeBeforeSegment(segmentToLoad, "audio") - this.minBuffer) {
            this.loadSpecificSegment(segmentToLoad);
        }
    }
    
    play() {
        try {
            this.video.play();
        }
        catch (e) {
            VideoError("Could not play video, user probably needs to interact with the page first.");
        }
    }
    
    pause() {
        this.video.pause();
    }
    
    seek(time) {
        this.video.currentTime = time;
    }
    
    currentTime() {
        return this.video.currentTime;
    }
    
    duration() {
        return this.video.duration;
    }

    calculateSegmentForTime(time, type) {
        let segNum = 0;
        let timeSoFar = 0;
        this.manifest.period.adaptationSet[type === "video" ? 0 : 1].segmentTemplate.betterTimeline.some(segment => {
            timeSoFar += segment.duration;
            if (timeSoFar > time) {
                return true;
            }
            segNum++;
            return false;
        });
        return segNum;
    }

    seekingTimeout;
    
    Seeking(event) { 
        if (this.seekingTimeout) {
            clearTimeout(this.seekingTimeout);
        }
        
        this.seekingTimeout = setTimeout(() => {
            this.seekingEnded();
        }, this.triggerSeekedEnded);
    }  
    
    seekingEnded() {

        //AUTO BUFFER VIDEO
        var currentTime = this.video.currentTime;
        var currentSegment = this.calculateSegmentForTime(currentTime, "video");
        var segmentToLoad = currentSegment + 1;

        if (segmentToLoad > this.maxSegNum_video) return;
        if (this.loadedSegments_video[segmentToLoad] === true) return;

        if (currentTime >= this.timeBeforeSegment(segmentToLoad, "video") - this.minBuffer) {
            this.loadSpecificSegment(segmentToLoad);
        }

        currentTime = this.video.currentTime;
        currentSegment = this.calculateSegmentForTime(currentTime, "video");
        segmentToLoad = currentSegment + 2;

        if (segmentToLoad > this.maxSegNum_video) return;
        if (this.loadedSegments_video[segmentToLoad] === true) return;

        if (currentTime >= this.timeBeforeSegment(segmentToLoad, "video") - this.minBuffer) {
            this.loadSpecificSegment(segmentToLoad);
        }

        //AUTO BUFFER AUDIO
        currentSegment = this.calculateSegmentForTime(currentTime, "audio");
        segmentToLoad = currentSegment + 1;

        if (segmentToLoad > this.maxSegNum_audio) return;
        if (this.loadedSegments_audio[segmentToLoad] === true) return;

        if (currentTime >= this.timeBeforeSegment(segmentToLoad, "audio") - this.minBuffer) {
            this.loadSpecificSegment(segmentToLoad);
        }
        
        currentSegment = this.calculateSegmentForTime(currentTime, "audio");
        segmentToLoad = currentSegment + 2;
        
        if (segmentToLoad > this.maxSegNum_audio) return;
        if (this.loadedSegments_audio[segmentToLoad] === true) return;
        
        if (currentTime >= this.timeBeforeSegment(segmentToLoad, "audio") - this.minBuffer) {
            this.loadSpecificSegment(segmentToLoad);
        }
    }

    parseManifest(jsonObj) {
        var model = {
            type: jsonObj.Type,
            profiles: jsonObj.Profiles,
            minBufferTime: jsonObj.MinBufferTime,
            mediaPresentationDuration: jsonObj.MediaPresentationDuration,
            period: {
                adaptationSet: jsonObj.Period.AdaptationSet.map(adaptationSet => {
                    return {
                        mimeType: adaptationSet.MimeType,
                        maxWidth: adaptationSet.MaxWidth,
                        maxHeight: adaptationSet.MaxHeight,
                        segmentAlignment: adaptationSet.SegmentAlignment,
                        startWithSAP: adaptationSet.StartWithSAP,
                        representation: {
                            audioChannelConfiguration: adaptationSet.Representation.AudioChannelConfiguration,
                            bandwidth: adaptationSet.Representation.Bandwidth,
                            codecs: adaptationSet.Representation.Codecs,
                            frameRate: adaptationSet.Representation.FrameRate,
                            height: adaptationSet.Representation.Height,
                            id: adaptationSet.Representation.Id,
                            scanType: adaptationSet.Representation.ScanType,
                            width: adaptationSet.Representation.Width,
                            audioSamplingRate: adaptationSet.Representation.AudioSamplingRate
                        },
                        segmentTemplate: {
                            initialization: adaptationSet.SegmentTemplate.Initialization,
                            media: adaptationSet.SegmentTemplate.Media,
                            startNumber: adaptationSet.SegmentTemplate.StartNumber,
                            timescale: adaptationSet.SegmentTemplate.Timescale,
                            segmentTimeline: adaptationSet.SegmentTemplate.SegmentTimeline.map(segment => {
                                return {
                                    duration: Number(segment.Duration),
                                    repeatAfter: Number(segment.RepeatAfter)
                                };
                            })
                        }
                    };
                })
            }
        };

        model.period.adaptationSet.forEach(adaptationSet => {
            //create an array of the segment timeline (so we can easily calculate the timestamp offset)
            adaptationSet.segmentTemplate.betterTimeline = [];
            adaptationSet.segmentTemplate.segmentTimeline.forEach(segment => {
                for (var i = 0; i <= segment.repeatAfter; i++) {
                    adaptationSet.segmentTemplate.betterTimeline.push({duration: Number(segment.duration) / Number(adaptationSet.segmentTemplate.timescale)});
                }
            });
        });

        return model;
    }     
}

function waitForObjectState(check, interval = 100) {
    return new Promise(resolve => {
        const checkInterval = setInterval(() => {
            if (check()) {
                clearInterval(checkInterval);
                resolve();
            }
        }, interval);
    });
}




//DEBUG
var logLevel;

const VideoLog = (...args) =>  ((logLevel & 0b00000001) !== 0) ? console.log(`%c[VideoPlayer.js]%c`, 'font-weight:700;color:royalblue;', '', ...args) : null;
const VideoDebug = (...args) => (logLevel & 0b00000010) !== 0 ? console.log(`%c[VideoPlayer.js]%c`, 'font-weight:700;color:orange;', '', ...args) : null;
const VideoError = (...args) => (logLevel & 0b00000100) !== 0 ? console.log(`%c[VideoPlayer.js]%c`, 'font-weight:700;color:red;', '', ...args) : null;