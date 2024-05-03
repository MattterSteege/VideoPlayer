//need to implement the following functions
// - First fetch the amount of segments (video and audio) from the server /api/Video/{videoId}
// - turn into a 'VideoPlayer' class

var videoId;

var mimeType_video = "video/mp4";
var codecs_video = "avc1.4D401F";
var mimeCodec_video = mimeType_video + ';codecs = "' + codecs_video + '"';

var mimeType_audio = "audio/mp4";
var codecs_audio = "mp4a.40.2";
var mimeCodec_audio = mimeType_audio + ';codecs = "' + codecs_audio + '"';

var mediaSource = new MediaSource();
var sourceBuffer_video;
var sourceBuffe_audio;
var segNum = 1;
var maxSegNum = 100;

function startup(video_id) {
    if (MediaSource.isTypeSupported(mimeCodec_video)) {
        mediaSource.addEventListener("webkitsourceopen", sourceOpen, false);
        mediaSource.addEventListener('sourceopen', sourceOpen, false);
        video = document.querySelector('video');
        video.src = URL.createObjectURL(mediaSource);
        videoId = video_id;
    } else {
        console.error('Unsupported MIME type or codec: ', mimeCodec_video);
    }
}

function sourceOpen(e) {
    sourceBuffer_video = mediaSource.addSourceBuffer(mimeCodec_video);
    sourceBuffe_audio = mediaSource.addSourceBuffer(mimeCodec_audio);

    fetch("api/Video/" + videoId + "/video/init.mp4", function(buf) {
        sourceBuffer_video.appendBuffer(buf);
    });

    fetch("api/Video/" + videoId + "/audio/init.mp4", function(buf) {
        sourceBuffe_audio.appendBuffer(buf);
    });

    sourceBuffer_video.addEventListener('updateend', loadSegment);
};

function loadSegment() {
    if (segNum < maxSegNum) {
        getAudioSegment(segNum);
        getVedioSegment(segNum);
        segNum++;
    }
}

function getVedioSegment(segNum) {
    fetch("api/Video/" + videoId + "/video/seg-" + segNum + ".m4s", function(buf) {
        console.log("mp4/output/video/avc1/seg-" + segNum + ".m4s");
        sourceBuffer_video.appendBuffer(buf);
    });
}

function getAudioSegment(segNum) {
    fetch("api/Video/" + videoId + "/audio/seg-" + segNum + ".m4s", function(buf) {
        console.log("mp4/output/audio/und/mp4a.40.2/seg-" + segNum + ".m4s");
        sourceBuffe_audio.appendBuffer(buf);
    });

}

function fetch(url, cb) {
    console.log(url);
    var xhr = new XMLHttpRequest;
    xhr.open('get', url);
    xhr.responseType = 'arraybuffer';
    xhr.onload = function() {
        cb(xhr.response);
    };
    xhr.send();
};