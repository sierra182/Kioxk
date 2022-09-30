////async function unloadPer(numper) {
////    console.log("unload " + numper);
////    await new Promise(resolve => setTimeout(resolve, 5000));
////    // setTimeout(async , 5000);
////    console.log("timeoutfin");
////    //var per = document.getElementsByClassName("jsDivPer");
////    //for (var enf of per) {
////    //    enf.addEventListener("beforeunload", () => { console.log("onmous"); });
////    //}
////    //var child = per.firstChild;
////    //var pc = document.getElementById(child);

////    //if (child != null && pc!= null) {

////    //    child.addEventListener("onmouseover", () => { console.log("onmous"); });

////    //}
////}


function resizeIframe(obj) {

    let f = document.getElementById("myframe");
    // let g = f.parentElement;
    setTimeout(aftertime, 1000);
    function aftertime() {


        f.style.height = 0; f.style.height = f.contentWindow.document.documentElement.scrollHeight + 'px'; console.log("resize fin");
        void f.offsetWidth;
        window.addEventListener('resize', () => {
            console.log("RESIZE");
            f.style.height = 0; f.style.height = f.contentWindow.document.documentElement.scrollHeight + 'px';
            //  g.style.height = 0; g.style.height = f.contentWindow.document.documentElement.scrollHeight + 'px'; console.log("resize fin");
        });

        console.log(f.id + "idee");
        f.style.height = 0; f.style.height = f.contentWindow.document.documentElement.scrollHeight + 'px'; console.log("resize fin");

    }
}




var togScal = false;
var phcont
var maxs;
var phs;
var myvarscale;
var contifra;
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
            console.log("RES");
            if (togScal) { // a tej

                if (window.innerWidth > 100) {

                    myvarcontal = getComputedStyle(contal).getPropertyValue("--ph_scale-height");
                    myvarcp = getComputedStyle(phs).getPropertyValue("--cp_mult-h");
                    myvarscale = getComputedStyle(phs).getPropertyValue("--mult_ph-scale");
                    myvartrans = getComputedStyle(phs).getPropertyValue("--trans-height");
                    maxs = getComputedStyle(phs).getPropertyValue("--max_size");
                    contal.style.position = "relative";
                    phs.style.setProperty("--mult_ph-scale", myvarscale * 3.5);
                    phs.style.setProperty("--max_size", "0px");
                 
                    phcont.style.setProperty("height", "fit-content");
                   
                    //phs.style.setProperty("--cp_mult-h", myvarcp * 2.6);
                 //   phcont.style.setProperty("height", phcont.clientHeight + 60 + "px");
                    //phs.style.setProperty("--max_size", ".8rem");
                    /*   void phs.offsetWidth;*/
                    //setTimeout(() => {
                        
                    //}, 4000);
                    phs.style.position = "absolute";
                  //  phs.style.setProperty("align-self", "flex-start");
                 //   phs.style.left = "0";
                 //   phs.style.right = "0";
                  //  phs.style.margin = "0 auto";
                 ////   phs.style.display = "block";

                   // phs.style.transform = `translate(${-87}%,${0}%)`;/// ass !!!
                    //  for (var cif of contifra) {
                    //    cif.classList.add("scalePh");
                    //}
                }
                else {
                    // doc.style.transform = `translate(${-}%,${-yy}%)`; // x y

                    // obtenir une variable par ailleurs
                    // let myvar = getComputedStyle(doc).getPropertyValue("--photo-width");

                    // définir une variable dans un style en ligne

                    // doc.style.zoom = "100%";
                    //doc.style.innerWidth = "100%";
                    //doc.style.innerHeight = "100%";
                }
            }
        }
        res();
        //window.addEventListener("resize", () => { res(); });

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
        phs.style.position = "relative"; //...


        car.style.borderRadius = "0 50% 0 0";
        car2.style.transform = "scale(1,1)";
        car2.style.backgroundColor = "";
        car2.style.borderColor = "red";
        car2.style.opacity = ".5";
        car2.style.borderRadius = "0 50% 0 0";

        //for (var cif of contifra) {
        //    cif.classList.remove("scalePh");
        //}

      //  phs.style.setProperty("--cp_ph-y", myvarcp);
        contal.style.position = "absolute";
        phs.style.setProperty("--mult_ph-scale", myvarscale);
         phs.style.setProperty("--max_size", maxs);
        phcont.style.height = "var(--cp_ph-y)";
        phs.style.transform = "";
        window.setTimeout(zin, 1000, true);

        function zin() {

            phs.style.zIndex = "0";
        }
    }


}

function scroll(el, sens) {
    console.log("scroll DEb2");
    var elem = document.getElementById(el);

    if (sens === "bas") {
        console.log("bas");
        setTimeout(() => {
            var coo = elem.scrollHeight;
            elem.scrollTo(0, coo);
            console.log("bas fin");
        }, 2000)

    } else if (sens === "haut") {
        console.log("haut");
        var coo = elem.scrollHeight;
        elem.scrollTo(0, -coo);
        console.log("haut fin");
    };

    console.log("scroll FIN");
    // void elem.offsetWidth;
}

