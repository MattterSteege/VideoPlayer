//When you minify the JS code use this website: https://www.digitalocean.com/community/tools/minify, with the following settings:
//  - Compress
//      - dead_code, drop_console, drop_debugger
//  - Mangle
//      - keep_classnames, keep_fnames, toplevel, safari10

//need to implement the following functions
// - First fetch the amount of segments (video and audio) from the server /api/Video/{videoId} (done)
// - When user adjusts the video time and the time is outside the buffer, fetch needed segment (in progress) need the /{guid}/mpd endpoint for segment lengths


class VideoPlayer {
    
    constructor(video_id) {
        //set by user - shallow
        this.videoId = video_id;
        this.minBuffer = 5; //seconds
        
        //set by user - deep
        this.mimeCodec_audio = "audio/mp4; codecs = mp4a.40.2";
        this.mimeCodec_video = "video/mp4; codecs = avc1.4D401F";
        
        //set / changed internally
        this.mediaSource;
        this.sourceBuffer_video;
        this.sourceBuffer_audio;
        this.maxSegNum_audio = 0;
        this.segNum_audio = 1;
        this.maxSegNum_video = 0;
        this.segNum_video = 1;
        
        this.manifest = null;

        fetch("api/Video/" + this.videoId + "/manifest")
        .then(response => response.json())
        .then(data => {

            this.manifest = this.parseManifest(data);
            
            //calc the amount of video segments
            this.manifest.period.adaptationSet[0].segmentTemplate.segmentTimeline.forEach(segment => {
                this.maxSegNum_video += segment.repeatAfter + 1;
            });
            
            //calc the amount of audio segments
            this.manifest.period.adaptationSet[1].segmentTemplate.segmentTimeline.forEach(segment => {
                this.maxSegNum_audio += segment.repeatAfter + 1;
            });

            VideoLog("Loaded manifest file:", data);
            this.canPlay = true;
        });
        
        this.canPlay = false;
    }

    startup() {
        
        VideoLog("VideoPlayer is queued...");

        waitForObjectState(() => this.canPlay)
        .then(() => {
            VideoLog("VideoPlayer is starting");
            this.startupInternal();
        })
        .catch((error) => {
            VideoLog("An error occurred:", error);
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
            this.video.oninput = (event) => this.durationChange(event);
            
        } else {
            VideoLog('Unsupported MIME type or codec: ', this.mimeCodec_video);
        }
    }
    
    sourceOpen(e) {
        this.sourceBuffer_video = this.mediaSource.addSourceBuffer(this.mimeCodec_video);
        this.sourceBuffer_video.mode = 'sequence';
        this.sourceBuffer_audio = this.mediaSource.addSourceBuffer(this.mimeCodec_audio);
        this.sourceBuffer_audio.mode = 'sequence';
        var self = this; // Store a reference to 'this' for fetch callback
        VideoLog('Source buffers created', this.sourceBuffer_video, this.sourceBuffer_audio);

        var init = 0;
        
        this.fetch("api/Video/" + this.videoId + "/video/init.mp4", function (buf) {
            self.sourceBuffer_video.appendBuffer(buf);
            init++;
        });

        this.fetch("api/Video/" + this.videoId + "/audio/init.mp4", function (buf) {
            self.sourceBuffer_audio.appendBuffer(buf);
            init++;
        });

        waitForObjectState(() => init === 2)
        .then(() => {
            VideoLog("Init segments loaded");
            this.loadSegment();
        });
    };
    
    loadSegment() {
        if (this.segNum_audio <= this.maxSegNum_audio) {
            this.getAudioSegment(this.segNum_audio);
            this.segNum_audio++;
        }
        
        if (this.segNum_video <= this.maxSegNum_video) {
            this.getVideoSegment(this.segNum_video);
            this.segNum_video++;
        }
    }
    
    loadSpecificSegment(segNum) {
        this.getAudioSegment(segNum);
        this.getVideoSegment(segNum);
    }
    
    getVideoSegment(segNum) {
        var self = this; // Store a reference to 'this' for fetch callback
        this.fetch("api/Video/" + this.videoId + "/video/seg-" + segNum + ".m4s", function (buf) {
            self.sourceBuffer_video.appendBuffer(buf);
        });
    }
    
    getAudioSegment(segNum) {
        var self = this; // Store a reference to 'this' for fetch callback
        this.fetch("api/Video/" + this.videoId + "/audio/seg-" + segNum + ".m4s", function (buf) {
            self.sourceBuffer_audio.appendBuffer(buf);
        });

    }
    
    fetch(url, cb) {
        //VideoLog(url);
        var xhr = new XMLHttpRequest;
        xhr.open('get', url);
        xhr.responseType = 'arraybuffer';
        xhr.onload = function () {
            cb(xhr.response);
        };
        xhr.send();
    }
    
    checkBuffer() {
        
        //VideoLog("Checking buffer...");
        
        if (this.video.currentTime >= this.video.buffered.end(0) - this.minBuffer) {
            var logMessage = "Buffer is low, loading ";
            if (this.segNum_audio <= this.maxSegNum_audio && this.segNum_video <= this.maxSegNum_video) {
                logMessage += "new audio and video segment";
            } 
            else {
                if (this.segNum_audio <= this.maxSegNum_audio) {
                    logMessage += "new audio segment";
                }
                if (this.segNum_audio > this.maxSegNum_audio && this.segNum_video > this.maxSegNum_video) {
                    logMessage += "nothing";
                }
            }
            VideoLog(logMessage);
            this.loadSegment();
        }
    }
    
    play() {
        this.video.play();
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

    durationChange(event) {
        //check if the current time is inside the buffer
        for (var i = 0; i < this.sourceBuffer_video.buffered.length; i++) {
            if (this.video.currentTime >= this.sourceBuffer_video.buffered.start(i) && this.video.currentTime <= this.sourceBuffer_video.buffered.end(i)) {
                VideoLog("Current time is inside the buffer");
                return;
            }
            else {
                VideoLog("Current time is outside the buffer");
                //fetch the needed segment
                this.loadSpecificSegment(i + 1);
            }
        }
    }

    parseManifest(jsonObj) {
        return {
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
                                    duration: segment.Duration,
                                    repeatAfter: segment.RepeatAfter
                                };
                            })
                        }
                    };
                })
            }
        };
    }
}

const VideoLog = (...args) => console.log("[VideoPlayer.js]", ...args);

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