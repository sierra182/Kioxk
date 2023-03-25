

function loadScript(src) {
    return new Promise((resolve, reject) => {
        const script = document.createElement("script");
        script.src = src;
        script.onload = () => resolve();
        script.onerror = () => reject(new Error(`Failed to load ${src}`));
        document.head.appendChild(script);
    });
}

window.loadUnity = async (canvasId, loaderJsPath) => {
    //await loadScript(loaderJsPath);

    return new Promise((resolve, reject) => {
        //if (typeof UnityLoader === "undefined") {
        //    reject("UnityLoader is not defined. Make sure the Desktop.loader.js file is loaded.");
        //    return;
        //}

        const gameInstance = UnityLoader.instantiate(canvasId, loaderJsPath, {
            onProgress: UnityProgress,
        });

        resolve(gameInstance);
    });
};
