(function () {  
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