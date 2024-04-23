const BetterPages = {
    mainBeforeLoad: function (callback) {},
    mainAfterLoad: function (callback) {},
    mainBeforeUnload: function (callback) {},
};

function waitForObject(obj, callback, interval = 100) {
    if (typeof obj !== 'undefined') {
        callback(obj);
    } else {
        setTimeout(function () {
            waitForObject(obj, callback, interval);
        }, interval);
    }
}