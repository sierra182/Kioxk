
var togaf;
function resizeIframe(obj) {

    let f = document.getElementById("myframe");
    f.contentDocument.getElementById("map").addEventListener('mouseenter', () => {
        setTimeout(aftertime, 1000);
    });
    f.contentDocument.getElementById("map").addEventListener('mouseleave', () => {
        setTimeout(aftertime, 1000);
    })
    setTimeout(aftertime, 500);

    function aftertime() {

        f.style.setProperty("transition", "none");
        window.addEventListener('resize', () => {
            console.log("RESIZE");
            setTimeout(() => {
                f.style.setProperty("transition", "none");
                f.style.height = 0; f.style.height = f.contentWindow.document.documentElement.scrollHeight + 'px';

                aftergardPhoto();
                const doc = document.documentElement;
                doc.style.setProperty('--doc-height', `${window.innerHeight}px`);
            }, 1000);
        });

        if (togaf) {
            f.style.setProperty("transition", "height .5s");
            togaf = false;
        } else {
            togaf = true;
        }

        f.style.height = 0; f.style.height = f.contentWindow.document.documentElement.scrollHeight + 'px'; console.log("resize fin");
    }
}

function aftergardPhoto() {
    if (togScal) {

        phs = document.getElementById("photos");
        allimg = phs.getElementsByTagName("img");

        const land = window.matchMedia("(orientation:landscape)");
        const port = window.matchMedia("(orientation:portrait)");

                                                             console.log("land et port: " + land.matches + " " + port.matches);
        if (land.matches) {
            console.log("after land true");

            phs.style.setProperty("width", "auto");
            for (var oneimg of allimg) {
                oneimg.style.width = "auto";
                oneimg.style.height = "95vh";

            }

            var imgcs;
            for (var oneimg of allimg) {

                imgcs = getComputedStyle(oneimg);
            }
            const phss = getComputedStyle(phs);

                                                                     console.log("phss: " + phss.width + "imgcs: " + imgcs.width);
            var copimg = Math.round(parseFloat(imgcs.width));
            var copph = Math.round(parseFloat(phss.width));
                                                             console.log("ROUNDED phss: " + copph + "imgcs: " + copimg);
            if (copph < copimg) {
                                                                     console.log("cont est plus petit que img");

                                                                           console.log("forced portrait: " + copph + " " + copimg);
                phs.style.setProperty("width", "95vw");
                for (var oneimg of allimg) {
                    oneimg.style.width = "100%";
                    oneimg.style.height = "auto";

                }

            }
            else {
                console.log("cont n'est pas plus petit que img");
            }

        }

        else if (port.matches) {
                                                         console.log("after port true");
            phs.style.setProperty("width", "95vw");

            for (var oneimg of allimg) {
                oneimg.style.width = "100%";
                oneimg.style.height = "auto";                
            }
        }  

      
    }
}

function resizeBody() {
   
    //    const doc = document.documentElement
    //doc.style.setProperty('--doc-height', `${window.innerHeight}px`);
    
    //window.addEventListener(‘resize’, documentHeight)
    //documentHeight()
}

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

function scaler(docu, carre, carre2, xx, yy, x, y) {

    phs = document.getElementById("photos");
    phss = getComputedStyle(phs);
    phcont = phs.firstChild;
    contifra = document.getElementsByClassName("contIfraJs");
    contmlt = document.getElementsByClassName("contPhMultJs");
    allimg = phs.getElementsByTagName("img");
    contal = document.getElementById("contPhAloneJs");
    car = document.getElementById(carre);
    car2 = document.getElementById(carre2);

    if (!togScal) {

        togScal = true;

        myvarcontal = getComputedStyle(contal).getPropertyValue("--ph_scale-height");
        myvarcp = getComputedStyle(phs).getPropertyValue("--cp_mult-h");
        myvarscale = getComputedStyle(phs).getPropertyValue("--mult_ph-scale");
        myvartrans = getComputedStyle(phs).getPropertyValue("--trans-height");
        maxs = getComputedStyle(phs).getPropertyValue("--max_size");

        phs.style.position = "absolute";

        const land = window.matchMedia("(orientation:landscape)");
        const port = window.matchMedia("(orientation:portrait)");
        if (land.matches) {
            console.log("land true");
            phs.style.setProperty("width", "auto");
            for (var oneimg of allimg) {
                oneimg.style.width = "auto";
                oneimg.style.height = "95vh";
                oneimg.style.maxHeight = "none";
            }

        }
        else if (port.matches) {
            console.log("port true");
            phs.style.setProperty("width", "95vw");
            for (var oneimg of allimg) {
                oneimg.style.width = "100%";
                oneimg.style.height = "auto";
                oneimg.style.maxHeight = "none";
            }
        }

        phcont.style.setProperty("height", "fit-content");

        for (var cif of contifra) {
            cif.firstChild.style.position = "relative";
        }


        phs.style.borderRadius = ".1%";
        phs.style.zIndex = "3";

        car.style.borderRadius = "10%";
        car2.style.borderRadius = "10%";
        car2.style.transformOrigin = "0";
        car2.style.transform = "scale(.5,.1) translate(1.4rem, -2.4rem)";
        car2.style.backgroundColor = "white";
        car2.style.borderColor = "white";
        car2.style.opacity = "1";

        phs.requestFullscreen();
       // allimg.firstChild.requestFullscreen();
        //for (var oneimg of allimg) {
        //    oneimg.firstChild.requestFullscreen();
        //}
    }


    else {

        document.exitFullscreen();
        togScal = false;

        phs.style.borderRadius = "5%";
        phs.style.position = "relative";

        car.style.borderRadius = "0 50% 0 0";
        car2.style.transform = "scale(1,1)";
        car2.style.backgroundColor = "";
        car2.style.borderColor = "red";
        car2.style.opacity = ".5";
        car2.style.borderRadius = "0 50% 0 0";


        phcont.style.height = "var(--cp_ph-y)";
        // phs.style.setProperty("--mult_ph-scale", myvarscale);
        //  phs.style.setProperty("--max_size", maxs);

        for (var oneimg of allimg) {
            oneimg.style.width = "100%";
            oneimg.style.height = "auto";
            oneimg.style.maxHeight = "calc(max(var(--photo-width),var(--max_size)) / 2 * .6)";
        }
        phs.style.setProperty("width", "calc(max(var(--photo-width),var(--max_size)) / 2 * var(--mult_ph-scale))");
        //phs.style.setProperty("height", "fit-content");
        //  phss.style.width = "calc(max(var(--photo - width),var(--max_size)) / 2 * var(--mult_ph-scale))";
        for (var cif of contifra) {
            cif.firstChild.style.position = "absolute";
        }
        window.setTimeout(zin, 1000, true);
        function zin() {
            phs.style.zIndex = "0";
        }
    }
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

