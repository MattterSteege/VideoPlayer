const BetterPages = {
    mainBeforeLoad: function(callback) {},
    mainAfterLoad: function(callback) {},
    mainBeforeUnload: function(callback) {},
    loadingScreenDelay: 0
};

let main;
let content;
let isRunning = false;

function InitBetterPages() {
    waitForObject(BetterPages, function () {
        main = document.getElementById("main");
        content = document.getElementById("content");

        history.replaceState(null, null, "/");
        ReplacePage((document.cookie.split(';').find(c => c.includes("page_to_load")).split('=')[1]));
    }, 500);
}


function ReplacePage(url) {
    if (isRunning) {
        return;
    }

    //check if the url is the same as the current url
    if (url === window.location.pathname) {
        return;
    }

    BetterPages.mainBeforeUnload();
    BetterPages.mainBeforeUnload = () => {};

    // Define a function to append a script to either the head or body
    function appendScript(element, scriptToWorkWith, isSrc) {
        const script = document.createElement("script");
        if (isSrc) {
            script.src = scriptToWorkWith.src;
            if (script.integrity)
                script.integrity = scriptToWorkWith.integrity;
            if (script.crossOrigin)
                script.crossOrigin = scriptToWorkWith.crossOrigin;
            if (script.referrerPolicy)
                script.referrerPolicy = scriptToWorkWith.referrerPolicy;
        } else {
            script.innerHTML = scriptToWorkWith.innerHTML;
        }

        if (scriptToWorkWith.defer) {
            script.defer = true;
        }

        script.id = "added-by-fetch";
        element.appendChild(script);
    }

    // Define a function to append a stylesheet link to the head
    function appendStylesheet(href) {
        const link = document.createElement("link");
        link.rel = "stylesheet";
        link.href = href;
        link.id = "added-by-fetch";
        document.head.appendChild(link);
        return link;
    }

    //add query "no-framework" to the url (check if the url already has a query)
    if (url.includes("?")) {
        url += "&no-framework";
    } else {
        url += "?no-framework";
    }

    //urldecode the url
    url = decodeURIComponent(url);

    // Make an HTTP request
    isRunning = true;
    fetch(url, {
        method: 'GET',
        cache: 'no-cache',
    })
        .then(response => {
            if (!response.ok) {


                // if (response.status === 401) {
                //     isRunning = false;
                //     ReplacePage("/Login");
                // }

                handleFetchError();
                return Promise.reject('Fetch failed');
            }
            return response.text();
        })
        .then(data => {
            if (data.startsWith('<!DOCTYPE html>')) {
                isRunning = false;
                handleFetchError(); // The page is not a valid page
                return;
            }


            //add a delay from here till the closing of this function if you want to show a loading screen      
            setTimeout(() => {

                // Remove all items with id added-by-fetch (scripts and stylesheets used in the previous page)
                const elements = document.querySelectorAll("#added-by-fetch");
                elements.forEach(element => element.remove());

                // Update main content
                main.outerHTML = '<main id="main"></main>';
                main = document.getElementById("main");
                main.innerHTML = stripTag("link", stripTag("script", data));

                const html = new DOMParser().parseFromString(data, 'text/html');
                const scripts = html.getElementsByTagName("script");

                // Append all scripts to the head or body
                for (let i = 0; i < scripts.length; i++) {
                    if (scripts[i].src) {
                        appendScript(document.head, scripts[i], true);
                    } else {
                        appendScript(document.body, scripts[i], false);
                    }
                }

                // Append all stylesheets to the head
                const links = html.getElementsByTagName("link");
                let amountOfStylesheets = links.length;
                for (let i = 0; i < links.length; i++) {
                    if (links[i].rel === "stylesheet") {
                        const link = appendStylesheet(links[i].href);
                        watchForLoading(link);
                    }
                }

                function watchForLoading(link) {
                    const interval = setInterval(function() {
                        try {
                            if (link.sheet != null && link.sheet.cssRules.length > 0) {
                                clearInterval(interval);
                                amountOfStylesheets--;
                            }
                        } catch (e) {}
                    }, 50);
                }

                //wait till all stylesheets are loaded
                const interval = setInterval(function() {
                    if (amountOfStylesheets === 0) {
                        clearInterval(interval);
                    }
                }, 50);

                // Update the URL to be [base]/#/path
                window.location.hash = url.replace("?no-framework", "").replace("&no-framework", "");
                BetterPages.mainBeforeLoad();
                BetterPages.mainBeforeLoad = () => {};

                isRunning = false;

                setTimeout(() => {
                    //add an delay arround here if you want to show a loading screen
                    BetterPages.mainAfterLoad();
                    BetterPages.mainAfterLoad = () => {};
                }, BetterPages.loadingScreenDelay);
            }, BetterPages.loadingScreenDelay);

        })
        .catch(error => {
            console.error('Fetch error:', error);
        });


    function handleFetchError() {
        console.error('Fetch failed');
        //add any error handling here (like ReplacePage("/error") or something else)
    }

    function stripTag(tag, data) {
        const div = document.createElement('div');
        div.innerHTML = data;
        const scripts = div.getElementsByTagName(tag);
        let i = scripts.length;
        while (i--) {
            scripts[i].parentNode.removeChild(scripts[i]);
        }
        return div.innerHTML;
    }


}

window.onbeforeunload = function() {
    //save the current url in a cookie so we can go back to it
    document.cookie = "this_session_last_page=" + window.location.hash.slice(1) + ";path=/";
}

window.onpopstate = function() {
    //if the hash is empty, don't replace the page
    if (window.location.hash === "") return;
    ReplacePage(window.location.hash.replace("#", ""));
}

function waitForObject(obj, callback, interval = 100) {
    if (typeof obj !== 'undefined') {
        callback(obj);
    } else {
        setTimeout(function() {
            waitForObject(obj, callback, interval);
        }, interval);
    }
}