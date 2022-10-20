
var fr_Frame = true;
var frame;
function resizeIframe() {
    console.log("Dans resize Iframe!");
    if (fr_Frame) {
        window.addEventListener('resize', setTimeout(resizeOnResizeOrMouseEnter, 1000));
        // window.onresize += setTimeout(resizeOnResizeOrMouseEnter(), 1000);
        frame = document.getElementById("myframe");

        frame.contentDocument.getElementById("map").addEventListener('mouseenter', setTimeout(resizeOnResizeOrMouseEnter, 1000));
        frame.contentDocument.getElementById("map").addEventListener('mouseleave', setTimeout(resizeOnMouseLeave, 1000));

        setTimeout(resizeOnResizeOrMouseEnter, 500);

        fr_Frame = false;
    }
}

function resizeOnResizeOrMouseEnter() {
    console.log("windows resize");
    frame.style.setProperty("transition", "none");
    frameSet();
}

function resizeOnMouseLeave() {
    frame.style.setProperty("transition", "height .5s");
    frameSet();
}

function frameSet() {
    frame.style.height = 0;
    frame.style.height = frame.contentWindow.document.documentElement.scrollHeight + 'px';
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
        console.log("scaler init");
    }

    if (togScal) {
        console.log("scaler");
        myvarcontal = getComputedStyle(contal).getPropertyValue("--ph_scale-height"); // voir si maj auto
        myvarcp = getComputedStyle(phs).getPropertyValue("--cp_mult-h");
        myvarscale = getComputedStyle(phs).getPropertyValue("--mult_ph-scale");
        myvartrans = getComputedStyle(phs).getPropertyValue("--trans-height");
        maxs = getComputedStyle(phs).getPropertyValue("--max_size");

        //*********************************************



        //phs.style.position = "absolute";   ///                       photo en absolute
        //phcont.style.setProperty("height", "fit-content"); ///      photo cont height en fit content

        //for (var cif of contifra) {
        //    cif.firstChild.style.position = "relative";    ///       iframe legend cont en relative
        //}

        //**********************************************

        phs.style.borderRadius = ".1%";
        //phs.style.zIndex = "3";                         /// voir si encore utile

        car.style.borderRadius = "10%";
        car2.style.borderRadius = "10%";
        car2.style.transformOrigin = "0";
        car2.style.transform = "scale(.5,.1) translate(1.4rem, -2.4rem)";
        car2.style.backgroundColor = "white";
        car2.style.borderColor = "white";
        car2.style.opacity = "1";

        //phs.requestFullscreen();
        window.addEventListener('resize', photosWatchDogScreenAndFullScreen);
        photosWatchDogScreenAndFullScreen();


        togScal = false;
    }


    else {
        console.log("quit scaler");
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
        }
        //*********************************************
        //phs.style.position = "relative";                  /// photo en relative
        //phcont.style.height = "var(--cp_ph-y)";           ///ph cont en mode var css cpphy
        // phs.style.setProperty("--mult_ph-scale", myvarscale);   //// Ancien dim
        //  phs.style.setProperty("--max_size", maxs);             ////   

        //for (var oneimg of allimg) {                
        //    oneimg.style.width = "100%";
        //    oneimg.style.height = "auto";
        //    oneimg.style.maxHeight = "calc(max(var(--photo-width),var(--max_size)) / 2 * .6)";
        //}
        //phs.style.setProperty("width", "calc(max(var(--photo-width),var(--max_size)) / 2 * var(--mult_ph-scale))");
        ////phs.style.setProperty("height", "fit-content");
        ////  phss.style.width = "calc(max(var(--photo - width),var(--max_size)) / 2 * var(--mult_ph-scale))";
        //for (var cif of contifra) {
        //    cif.firstChild.style.position = "absolute";    /// iframe legend cont en absolute
        //}
        //*********************************************
        //window.setTimeout(zin, 1000, true);
        //function zin() {
        //    phs.style.zIndex = "1";                   /// voir si utile
        //}

        togScal = true;
    }
}

var naturalX;
var naturalY;
function imgNatural(img) {
    console.log("Natural!");
    naturalX = img.naturalWidth;
    naturalY = img.naturalHeight;
}

function photosWatchDogScreenAndFullScreen() {
    console.log("photo  watch before togscal");
    console.log("photo  watch");
    console.log("ratio window, img: " + window.screen.availWidth + "/" + window.screen.availHeight + " ... " + naturalX + "/" + naturalY);

    phs.style.position = "absolute";
    //for (var mult of contmlt) {
    //    mult.style.overflowY = "visible";
    //}    
    phcont.style.setProperty("height", "fit-content");
   
    
    if ((window.screen.availWidth / window.screen.availHeight) < (naturalX / naturalY)) {
        console.log("ratio screen < ratio img");

        var yetFullScreen;
        var fse = document.fullscreenElement;
        if (fse === null) {
            yetFullScreen = true;
        }
        else {
            yetFullScreen = false;
        }

        if (document.fullscreenEnabled && phs.requestFullscreen && yetFullScreen) {
            phs.requestFullscreen();
        }

        phs.style.setProperty("width", "95vw");

        for (var oneimg of allimg) {
            oneimg.style.width = "100%";      // mode portrait 
            oneimg.style.height = "auto";
            oneimg.style.maxHeight = "none";
        }

     
       
        console.log("mode portrait hé");

    }
    else {

     //   if ("ontouchstart" in window || window.DocumentTouch && document instanceof DocumentTouch) {
         
            if (document.fullscreenEnabled && phs.requestFullscreen && document.fullscreenElement ? false : true) {
                phs.requestFullscreen();
            }
     //   }
        phs.style.setProperty("width", "auto");

        for (var oneimg of allimg) {
            oneimg.style.width = "auto";                        // mode land 
            oneimg.style.height = "95vh";
            oneimg.style.maxHeight = "none";
        }
       
        console.log("mode land ho");
    }  
    if ((window.screen.availWidth / window.screen.availHeight) > 1) {


        for (var cif of contifra) {
            cif.firstChild.style.marginTop = "-3.6rem";
            cif.firstChild.style.zIndex = "1";
            cif.firstChild.style.position = "absolute";
        }
    }
    else {
        for (var cif of contifra) {
            cif.firstChild.style.marginTop = "auto";
            cif.firstChild.style.zIndex = "2";
            cif.firstChild.style.position = "relative";
        }
}
}
//phs = document.getElementById("photos");
//allimg = phs.getElementsByTagName("img");

//const land = window.matchMedia("(orientation:landscape)");
//const port = window.matchMedia("(orientation:portrait)");

//console.log("land et port: " + land.matches + " " + port.matches);

//if (land.matches) {
//    console.log("after land true");

//    phs.style.setProperty("width", "auto");
//    for (var oneimg of allimg) {
//        oneimg.style.width = "auto";                        // mode land 
//        oneimg.style.height = "95vh";
//        oneimg.style.maxHeight = "none";
//    }
//}
//else if (port.matches) {

//    console.log("after port true");
//    phs.style.setProperty("width", "95vw");

//    for (var oneimg of allimg) {
//        oneimg.style.width = "100%";      // mode portrait si portait reel
//        oneimg.style.height = "auto";
//        oneimg.style.maxHeight = "none";
//    }

//}
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

