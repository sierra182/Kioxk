﻿(function () {  
    var isChrome = !!window.chrome;

    if (isChrome && getChromeVersion() < 100) {

        alert("Il se peut que votre navigateur ne soit pas à jour. Si vous rencontrez des problemes d'affichage ou d'utilisation, veuillez le mettre à jour ou utilisez Chrome à jour..!");
        // window.location.reload();
    }
    if (!isChrome) {
        alert("Cette Application est optimisée pour les navigateurs basés sur Chromium: ( Chrome, Edge, Opera...). Si vous rencontrez des problemes d'affichage ou d'utilisation, téléchargez un navigateur basé sur Chromium.");
        //window.location.reload();
    }
}
)();

function getChromeVersion() {
    var raw = navigator.userAgent.match(/Chrom(e|ium)\/([0-9]+)\./);
    return raw ? parseInt(raw[2], 10) : 0;
}

function cookies() { //on work...

    if (localStorage.getItem("yetconnect")) {

        alert('Une mise à jour a été téléchargée.'); window.location.reload();
    }
    else {

        localStorage.setItem("yetconnect", true);

    }
}

function uni() {
   // alert("ok");
    var container = document.querySelector("#unity-container");
    var canvas = document.querySelector("#unity-canvas");
    var loadingBar = document.querySelector("#unity-loading-bar");
    var progressBarFull = document.querySelector("#unity-progress-bar-full");
    var fullscreenButton = document.querySelector("#unity-fullscreen-button");
    var warningBanner = document.querySelector("#unity-warning");

    // Shows a temporary message banner/ribbon for a few seconds, or
    // a permanent error message on top of the canvas if type=='error'.
    // If type=='warning', a yellow highlight color is used.
    // Modify or remove this function to customize the visually presented
    // way that non-critical warnings and error messages are presented to the
    // user.
    function unityShowBanner(msg, type) {
        function updateBannerVisibility() {
            warningBanner.style.display = warningBanner.children.length ? 'block' : 'none';
        }
        var div = document.createElement('div');
        div.innerHTML = msg;
        warningBanner.appendChild(div);
        if (type == 'error') div.style = 'background: red; padding: 10px;';
        else {
            if (type == 'warning') div.style = 'background: yellow; padding: 10px;';
            setTimeout(function () {
                warningBanner.removeChild(div);
                updateBannerVisibility();
            }, 5000);
        }
        updateBannerVisibility();
    }

    var buildUrl = "Build";
    var loaderUrl = buildUrl + "/Desktop.loader.js";
    var config = {
        dataUrl: buildUrl + "/Desktop.data.gz",
        frameworkUrl: buildUrl + "/Desktop.framework.js.gz",
        codeUrl: buildUrl + "/Desktop.wasm.gz",
        streamingAssetsUrl: "StreamingAssets",
        companyName: "DefaultCompany",
        productName: "chalet",
        productVersion: "0.1",
        showBanner: unityShowBanner,
    };

    // By default Unity keeps WebGL canvas render target size matched with
    // the DOM size of the canvas element (scaled by window.devicePixelRatio)
    // Set this to false if you want to decouple this synchronization from
    // happening inside the engine, and you would instead like to size up
    // the canvas DOM size and WebGL render target sizes yourself.
    // config.matchWebGLToCanvasSize = false; test12

    if (/iPhone|iPad|iPod|Android/i.test(navigator.userAgent)) {
        container.className = "unity-mobile";
        // Avoid draining fillrate performance on mobile devices,
        // and default/override low DPI mode on mobile browsers.
        config.devicePixelRatio = 1;
        unityShowBanner('WebGL builds are not supported on mobile devices.');
    } else {
        canvas.style.width = "960px";
        canvas.style.height = "600px";
    }
    loadingBar.style.display = "block";

    var script = document.createElement("script");
    script.src = loaderUrl;
    script.onload = () => {
        createUnityInstance(canvas, config, (progress) => {
            progressBarFull.style.width = 100 * progress + "%";
        }).then((unityInstance) => {
            loadingBar.style.display = "none";
            fullscreenButton.onclick = () => {
                unityInstance.SetFullscreen(1);
            };
        }).catch((message) => {
            alert(message);
        });
    };
    document.body.appendChild(script);
}