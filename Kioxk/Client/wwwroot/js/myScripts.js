
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
let pictureElements;
let sourceElements;


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
        pictureElements = document.getElementsByClassName('myPicture');
        sourceElements = [];

        for (let picture of pictureElements) {
            const sources = picture.querySelectorAll('source');
            sourceElements = sourceElements.concat(Array.from(sources));
        }

        fr_Scaler = false;
        togScal = true;
    }
     
    if (togScal) {
    
        myvarcontal = getComputedStyle(contal).getPropertyValue("--ph_scale-height");
        myvarcp = getComputedStyle(phs).getPropertyValue("--cp_mult-h");
        myvarscale = getComputedStyle(phs).getPropertyValue("--mult_ph-scale");
        myvartrans = getComputedStyle(phs).getPropertyValue("--trans-height");
        maxs = getComputedStyle(phs).getPropertyValue("--max_size");

        phs.style.setProperty("--max_size", "none");
        car.style.width = "max(5vw, 25px)";
        car.style.height = "max(5vw, 25px)";
        car.style.top = ".25rem";
        car2.style.transform = "scale(.5,.1) ";
        car2.style.backgroundColor = "white";
        car2.style.opacity = "1";

        for (var cif of contifra) {
            cif.firstChild.firstChild.style.color = "white";
        }

        if (sourceElements.length > 0) {
            sourceElements.forEach((sourceElement) => {
                sourceElement.setAttribute('sizes', '100w');
            });
        }

        window.addEventListener('resize', photosWatchDogScreenAndFullScreen);
        photosWatchDogScreenAndFullScreen();

        setTimeout(() => {
            phcont.style.position = "absolute";
            phcont.style.top = "0";
            car.style.position = "fixed";

        }, 1500);

        togScal = false;
    }

    else {

        phcont.style.position = "relative";
        car.style.position = "absolute";

        window.removeEventListener('resize', photosWatchDogScreenAndFullScreen);

        togScal = true;
        document.exitFullscreen();

        setTimeout(() => {
            phs.style.setProperty("--max_size", `${maxs}`);
            car.style.width = "8%";
            car.style.height = "var(--chemin_width_carre)";
            car2.style.transform = "scale(1,1)";
            car2.style.backgroundColor = "";
            car2.style.opacity = ".5";

            for (var oneimg of allimg) {
                oneimg.style.width = "100%";
                oneimg.style.height = "auto";
            }

            for (var cif of contifra) {
                cif.firstChild.firstChild.style.color = "black";
                cif.style.bottom = "unset";
            }

            if (sourceElements.length > 0) {
                sourceElements.forEach((sourceElement) => {
                    sourceElement.setAttribute('sizes', 'min(calc(100vw * .855),30rem)');
                });
            }

        }, 1000);     
    }    
}

function photosWatchDogScreenAndFullScreen() {
    if (document.body.offsetWidth / document.documentElement.clientHeight > 2000 / 1500) {   // vp land reel

        for (var oneimg of allimg) {
            oneimg.style.width = "auto";
            oneimg.style.height = `${1 * document.documentElement.clientHeight}px`;
        }
    }
    else {                                                       // vp portrait reel

        for (var oneimg of allimg) {
            oneimg.style.width = "100%";      // mode portrait 
            oneimg.style.height = "auto";
        }
    }

    if ((document.body.offsetWidth / (document.documentElement.clientHeight - 32) > ((2000 / 1500)))) {   // vp land reel
        for (var cif of contifra) {
            cif.style.bottom = "0";
        }

    }
    else {
        for (var cif of contifra) {
            cif.style.bottom = "unset";
        }
    }

    const fullscreenChange = () => {

        if (document.fullscreenEnabled && phs.requestFullscreen && ((document.fullscreenElement ? true : false) == false) && togScal == false) {
            document.removeEventListener("fullscreenchange", fullscreenChange);
            scaler();          
        }
    }

    setTimeout(() => {
        if (document.fullscreenEnabled && phs.requestFullscreen && (document.fullscreenElement ? false : true)) {

            phs.requestFullscreen();
            document.addEventListener("fullscreenchange", fullscreenChange);
        }
    }, 1000);
}

function getPhotoSize(dotNet) {

    var photo = document.getElementById("contPhAloneJs").parentElement;

    const getCalculatedSize = () => {

        let photoCalculatedSize = window.getComputedStyle(photo).getPropertyValue("width");
        return parseInt(photoCalculatedSize, 10);
    }

    window.addEventListener("resize", () => {

        dotNet.invokeMethodAsync("SetPhotoSize", getCalculatedSize);
    });
    return getCalculatedSize();
}

function configurePhotosObserver(dotNet) {

    const photos = document.getElementById("photos");
    const observerOptions = {
        root: null,
        threshold: .5
    };
    const observer = new IntersectionObserver((entries, observer) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {

                dotNet.invokeMethodAsync('UnPauseAutoDefil');
            } else {
                dotNet.invokeMethodAsync('PauseAutoDefil');
            }
        });
    }, observerOptions);

    observer.observe(photos);
}

function configureLambSvgObserver() {

    const lambs = document.getElementById("lambcontsurv");
    const lamb = document.getElementById("lambcont")

    const observerOptions = {
        root: null,
        rootMargin: '50% 0px 0px 0px',
        threshold: 0
    };
    const observer = new IntersectionObserver((entries, observer) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {

                lamb.style.display = "flex";
            } else {
                lamb.style.display = "none";
            }
        });
    }, observerOptions);

    observer.observe(lambs);
}

var frLamb = true;
var nmbrEl = 0;
function lamb(dotNet) {

    if (frLamb) {
        const ajustLamb = () => {

            if (window.matchMedia("(max-width:500px)").matches) {
                nmbrEl = 9;
            }
            else {
                nmbrEl = 20;
            }
            dotNet.invokeMethodAsync('CSharpLamb', nmbrEl);
        };
        ajustLamb();
        window.addEventListener("resize", ajustLamb);

        frLamb = false;
    }
}

let dotNetDict = {};
function subscriptionOnTouch(dotNet, elRef) {

    const addSubscriptionTouchEvent = (elref) => {
        var exHoverElement;
        elref.addEventListener("touchmove", (e) => {

            var hoverElement = document.elementFromPoint(e.touches[0].clientX, e.touches[0].clientY).parentElement.parentElement;
            if (exHoverElement && hoverElement.id == exHoverElement.id) {
                return;
            } else {
                exHoverElement = hoverElement;
            }
            if (hoverElement && dotNetDict.hasOwnProperty(hoverElement.id)) {
                dotNetDict[hoverElement.id].invokeMethodAsync("RiseEventCallBack");
            }
        });
    };

    if (elRef.parentElement.parentElement.classList == "element") {

        dotNetDict[elRef.parentElement.parentElement.id] = dotNet;
        addSubscriptionTouchEvent(elRef);
    }
}

function configurePanneauObserver() {

    const monthOverlay = document.getElementById("month_overlay");
    const panneau = document.getElementById("panneau");
    const panneauOverlay = document.getElementById("panneau_overlay");

    const onPanneauClick = () => {

        monthOverlay.style.opacity = "0";
        panneauOverlay.style.opacity = "0";

        setTimeout(() => {
            monthOverlay.style.display = "none";
            panneauOverlay.style.display = "none";
        }, 500);

    };

    monthOverlay.addEventListener("click", onPanneauClick);
    panneauOverlay.addEventListener("click", onPanneauClick);

    const observerOptions = {
        root: null,
        rootMargin: '0px 0px 0px 0px',
        threshold: 0
    };
    const observer = new IntersectionObserver((entries, observer) => {
        entries.forEach(entry => {

            if (!entry.isIntersecting) {
                monthOverlay.style.opacity = "1";
                panneauOverlay.style.opacity = "1";
                monthOverlay.style.display = "initial";
                panneauOverlay.style.display = "initial";
            }
        })

    }, observerOptions);
    observer.observe(panneau);
}

function configureBackgroundObserver() {

    const lamb = document.getElementById("lambcontsurv");
    const photos = document.getElementById("photos");
    const pretext = document.getElementById("pretext");
    const help = document.getElementById("hlp");
    const agenda = document.getElementById("agenda");
    const payment = document.getElementById("payment");
    const reserv = document.getElementById("reserv");

    const observerOptions = {
        root: null,
        rootMargin: '0px 0px 5% 100%',
        threshold: 0
    };
    const observer = new IntersectionObserver((entries, observer) => {
        entries.forEach(entry => {

            if (entry.isIntersecting) {

                switch (entry.target.id) {

                    case "lambcontsurv":
                        document.body.style.backgroundColor = "#FFC75F";
                        break;

                    case "photos":
                        pretext.style.setProperty("--my_background-color", "#FF8066");
                        document.body.style.backgroundColor = "#FF8066";
                        break;

                    case "pretext":
                        pretext.style.setProperty("--my_background-color", "#FF7F50");
                        document.body.style.backgroundColor = "#FF7F50";
                        break;

                    case "hlp":
                        help.classList.add('welcome_hlp');
                        pretext.style.setProperty("--my_background-color", "#FF6F91");
                        document.body.style.backgroundColor = "#FF6F91";
                        break;

                    case "agenda":
                        pretext.style.setProperty("--my_background-color", "#20D2AC");
                        document.body.style.backgroundColor = "#20D2AC";
                        break;

                    case "payment":
                        document.body.style.backgroundColor = "#FFAC6D";
                        break;

                    case "reserv":
                        reserv.style.setProperty("--input_color", "#FFC75F");
                        document.body.style.backgroundColor = "#FFC75F";
                        break;
                    default:
                }
            }
        });
    }, observerOptions);

    observer.observe(lamb);
    observer.observe(photos);
    observer.observe(pretext);
    observer.observe(help);
    observer.observe(agenda);
    observer.observe(payment);
    observer.observe(reserv);
}

function welcome() {
    configureBackgroundObserver();
    configurePanneauObserver()
}

function scroll(sens) {

    var elem = window;
    var coo = document.documentElement.scrollHeight - document.documentElement.clientHeight;
    if (sens === "bas") {
        elem.scrollTo({ top: coo, behavior: 'smooth' });
    } else if (sens === "haut") {
        var coo = elem.scrollHeight;
        elem.scrollTo({ top: 0, behavior: 'smooth' });
    };
}

//function scroll(el, sens) {

//    var elem = document.getElementById(el);

//    if (sens === "bas") {
//        setTimeout(() => {
//            var coo = elem.scrollHeight;
//            elem.scrollTo(0, coo);
//        }, 2000)

//    } else if (sens === "haut") {
//        var coo = elem.scrollHeight;
//        elem.scrollTo(0, -coo);
//    };
//}

//function spinScrolling() {
//    var go = document.getElementById("go");
//    var stack;

//    window.addEventListener('scroll', () => {
//        stack = document.documentElement.scrollTop;
//        go.style.transform = `rotate(${stack/2}deg)`;
//        // console.log("SPIN " + document.body.scrollTop + " " + document.body.scrollY);
//    });
//}
