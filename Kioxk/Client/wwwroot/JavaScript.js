
var fr_Frame = true;
var frame;

function resizeIframe() {
    console.log("Dans resize Iframe!");
    if (fr_Frame) {
        frame = document.getElementById("myframe");

        window.addEventListener('resize', () => setTimeout(resizeWatch, 800));
        frame.contentDocument.getElementById("map").addEventListener('mouseenter', () => setTimeout(resizeWatch, 1000));
        frame.contentDocument.getElementById("map").addEventListener('mouseleave', () => setTimeout(resizeWatch, 1000));

        const resizeWatch = () => {
            frame.style.height = (parseInt(frame.contentWindow.getComputedStyle(frame.contentWindow.document.getElementById("cont")).height) * 1.07) + "px";
        }
        resizeWatch();
        fr_Frame = false;
    }
}

var fr_Scaler = true;
var togScal = false;
var phcont
var maxs;
var phs;
var myvarscale;
var contifra;
var ifra;
var myvarcp;
var contmlt;
var allimg;
var contal;
var myvarcontal;
var myvartrans;
var car;
var car2;

function scaler() {

    if (fr_Scaler) {
        phs = document.getElementById("photos");
        phcs = getComputedStyle(phs);
        phcont = phs.firstChild;
        contal = document.getElementById("contPhAloneJs");
        contmlt = document.getElementsByClassName("contPhMultJs");
        allimg = phs.getElementsByTagName("img");
        contifra = document.getElementsByClassName("contIfraJs");
        car = document.getElementById('carreph');
        car2 = document.getElementById('carre2ph');

        fr_Scaler = false;
        togScal = true;
    }

    if (togScal) {
        myvarcontal = getComputedStyle(contal).getPropertyValue("--ph_scale-height"); // voir si maj auto
        myvarcp = getComputedStyle(phs).getPropertyValue("--cp_mult-h");
        myvarscale = getComputedStyle(phs).getPropertyValue("--mult_ph-scale");
        myvartrans = getComputedStyle(phs).getPropertyValue("--trans-height");
        maxs = getComputedStyle(phs).getPropertyValue("--max_size");

        phs.style.borderRadius = ".1%";

        car.style.borderRadius = "10%";
        car2.style.borderRadius = "10%";
        car2.style.transformOrigin = "0";
        car2.style.transform = "scale(.5,.1) translate(1.4rem, -2.4rem)";
        car2.style.backgroundColor = "white";
        car2.style.borderColor = "white";
        car2.style.opacity = "1";

        window.addEventListener('resize', photosWatchDogScreenAndFullScreen);
        photosWatchDogScreenAndFullScreen();

        togScal = false;
    }


    else {
        window.removeEventListener('resize', photosWatchDogScreenAndFullScreen);
        document.exitFullscreen();

        car.style.borderRadius = "0 50% 0 0";
        car2.style.transform = "scale(1,1)";
        car2.style.backgroundColor = "";
        car2.style.borderColor = "red";
        car2.style.opacity = ".5";
        car2.style.borderRadius = "0 50% 0 0";

        phs.style.borderRadius = "5%";
        phs.style.position = "relative";
        phs.style.setProperty("width", "calc(max(var(--photo-width),var(--max_size)) / 2 * var(--mult_ph-scale))");
        phcont.style.setProperty("height", "calc(max(var(--photo-width),var(--max_size)) /2 )");

        for (var oneimg of allimg) {
            oneimg.style.width = "100%";      // mode portrait 
            oneimg.style.height = "auto";
            oneimg.style.maxHeight = "calc(max(var(--photo-width),var(--max_size)) / 2 * .6)";
        }

        for (var cif of contifra) {
            cif.firstChild.style.position = "absolute";
            cif.firstChild.style.backgroundColor = "red";
            cif.firstChild.style.margin = "auto";
        }

        togScal = true;
    }
}

var naturalX;
var naturalY;

function photosWatchDogScreenAndFullScreen() {
    console.log("ratio window, img: " + document.body.offsetWidth + "/" + document.documentElement.clientHeight + " ... " + naturalX + "/" + naturalY);

    phs.style.position = "absolute";
    phcont.style.setProperty("height", "fit-content");

    if ((document.body.offsetWidth / document.documentElement.clientHeight) > 1) {   // land reel
        for (var cif of contifra) {
            cif.firstChild.style.backgroundColor = "blue";
            cif.firstChild.style.margin = "-3.6rem auto auto auto";
            cif.firstChild.style.position = "absolute";
        }
    }
    else {                                                       // portrait reel
        for (var cif of contifra) {
            cif.firstChild.style.backgroundColor = "red";
            cif.firstChild.style.margin = "auto";
            cif.firstChild.style.position = "relative";
        }
    }

    if ((document.body.offsetWidth / document.documentElement.clientHeight) < (naturalX / naturalY)) {  // l'ecran est proportionnellement moins large que la largeur reele de la photo
        console.log("ratio screen < ratio img");

        phs.style.setProperty("width", "95vw");

        for (var oneimg of allimg) {
            oneimg.style.width = "100%";      // mode portrait 
            oneimg.style.height = "auto";
            oneimg.style.maxHeight = "none";
        }

        if (document.fullscreenEnabled && phs.requestFullscreen && document.fullscreenElement ? false : true) {
            phs.requestFullscreen();
        }
        console.log("mode portrait!");
    }
    else {
        phs.style.setProperty("width", "auto");

        for (var oneimg of allimg) {
            oneimg.style.width = "auto";                        // mode land 
            oneimg.style.height = .95 * document.documentElement.clientHeight + "px";//  "95vh";
            oneimg.style.maxHeight = "none";
        }

        setTimeout(() => {
            if ("ontouchstart" in window || window.DocumentTouch && document instanceof DocumentTouch) {

                if (document.fullscreenEnabled && phs.requestFullscreen && document.fullscreenElement ? false : true) {
                    phs.requestFullscreen();
                }
            }
            console.log("mode land ho");
        }, 1200);

    }
}

function imgNatural(img) {
    console.log("Natural!");
    naturalX = img.naturalWidth;
    naturalY = img.naturalHeight;
}

function myscrollTo(el) {   
    var elem = document.getElementById(el);
    window.scrollTo(0, elem.scrollHeight + 50);
}

function scroll(el, sens) {

    var elem = document.getElementById(el);

    if (sens === "bas") {
        setTimeout(() => {
            var coo = elem.scrollHeight;
            elem.scrollTo(0, coo);
        }, 2000)

    } else if (sens === "haut") {
        var coo = elem.scrollHeight;
        elem.scrollTo(0, -coo);
    };
}

var frLamb = true;
var nmbrEl = 0;

function lamb(dotNet) {

    if (frLamb) {
        const ajustLamb = () => {

            if (window.matchMedia("(max-width:500px)").matches) {
                console.log("lamb max width");
                nmbrEl = 10;
            }
            else {
                console.log("lamb else");
                nmbrEl = 20;
            }
            dotNet.invokeMethodAsync('CSharpLamb', nmbrEl);
        };
        ajustLamb();
        window.addEventListener("resize", ajustLamb);

        frLamb = false;
    }
}

function welcome() {
    var pay = document.getElementById("payment");
    console.log("welcome");
    window.onscroll = () => {
        console.log("onscroll: " + document.documentElement.scrollTop + " " + document.documentElement.clientHeight + " " + pay.offsetTop);
        if (document.documentElement.scrollTop + document.documentElement.clientHeight > pay.offsetTop) { console.log("YEP!"); }
    };
}
   
