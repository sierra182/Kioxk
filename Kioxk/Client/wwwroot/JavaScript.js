
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

var togScal = false;
var phcont
var maxs;
var phs;
var myvarscale;
var contifra;
var ifra;
var myvarcp;
var contmlt;
var contal;
var myvarcontal;
var myvartrans;
var car;
var car2;

function scaler(docu, carre, carre2, xx, yy, x, y) {

    phs = document.getElementById("photos");
    phcont = phs.firstChild;
    contifra = document.getElementsByClassName("contIfraJs");
    contmlt = document.getElementsByClassName("contPhMultJs");
    contal = document.getElementById("contPhAloneJs");
    car = document.getElementById(carre);
    car2 = document.getElementById(carre2);

    if (!togScal) {

        togScal = true;

        function res() {

            if (togScal) {

                myvarcontal = getComputedStyle(contal).getPropertyValue("--ph_scale-height");
                myvarcp = getComputedStyle(phs).getPropertyValue("--cp_mult-h");
                myvarscale = getComputedStyle(phs).getPropertyValue("--mult_ph-scale");
                myvartrans = getComputedStyle(phs).getPropertyValue("--trans-height");
                maxs = getComputedStyle(phs).getPropertyValue("--max_size");
                phs.style.position = "absolute";
                phs.style.setProperty("--mult_ph-scale", myvarscale * 3.5);
                phs.style.setProperty("--max_size", "0px");
                phcont.style.setProperty("height", "fit-content");

                for (var cif of contifra) {
                    cif.firstChild.style.position = "relative";
                }               
            }
        }
        res();

        phs.style.borderRadius = ".1%";
        phs.style.zIndex = "3";

        car.style.borderRadius = "10%";
        car2.style.borderRadius = "10%";
        car2.style.transformOrigin = "0";
        car2.style.transform = "scale(.5,.1) translate(1.4rem, -2.4rem)";
        car2.style.backgroundColor = "white";
        car2.style.borderColor = "white";
        car2.style.opacity = "1";
    }
    else {

        togScal = false;

        phs.style.borderRadius = "5%";
        phs.style.position = "relative";

        car.style.borderRadius = "0 50% 0 0";
        car2.style.transform = "scale(1,1)";
        car2.style.backgroundColor = "";
        car2.style.borderColor = "red";
        car2.style.opacity = ".5";
        car2.style.borderRadius = "0 50% 0 0";
       
        for (var cif of contifra) {
            cif.firstChild.style.position = "absolute";
        }
        phcont.style.height = "var(--cp_ph-y)";
        phs.style.setProperty("--mult_ph-scale", myvarscale);
        phs.style.setProperty("--max_size", maxs);
      
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

