
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

function scaler(dotNet) {

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
      
        myvarcontal = getComputedStyle(contal).getPropertyValue("--ph_scale-height"); // voir si maj auto
        myvarcp = getComputedStyle(phs).getPropertyValue("--cp_mult-h");
        myvarscale = getComputedStyle(phs).getPropertyValue("--mult_ph-scale");
        myvartrans = getComputedStyle(phs).getPropertyValue("--trans-height");
        maxs = getComputedStyle(phs).getPropertyValue("--max_size");

        //  phs.style.position = "absolute";
        phs.style.setProperty("--max_size", "none");    // enlever ?  
        car.style.width = "max(5vw, 25px)";
        car.style.height = "max(5vw, 25px)";
        car.style.right = "1rem";
    //    car.style.borderRadius = "10%";
     //   car2.style.borderRadius = "10%";
        //  car2.style.transformOrigin = "0";
        //  car2.style.top = "0";
        // car2.style.left = "50%";
        car2.style.transform = "scale(.5,.1) ";
        car2.style.backgroundColor = "white";
      //  car2.style.borderColor = "white";
        car2.style.opacity = "1";
        //if (sourceElements.length > 0) {
        //    sourceElements.forEach((sourceElement) => {
        //        sourceElement.setAttribute('sizes', '(max-width: 600px) 100vw, (max-width: 900px) 50vw, 33vw');
        //    });
        //}
        for (var cif of contifra) {
           // cif.firstChild.style.backgroundColor = "white";
            cif.firstChild.firstChild.style.color = "white";
            //  cif.firstChild.style.width = "fit-content";
            //  cif.firstChild.style.margin = "auto";
            //    cif.firstChild.style.position = "absolute";
            //    cif.firstChild.style.bottom = ".7rem";
            //  cif.before.setProperty.width = "fit-content";
        }
        if (sourceElements.length > 0) {
            sourceElements.forEach((sourceElement) => {
                sourceElement.setAttribute('sizes', '100w');
            });
        }    
            window.addEventListener('resize', photosWatchDogScreenAndFullScreen);
            photosWatchDogScreenAndFullScreen();

            togScal = false;      
    }

    else {

        window.removeEventListener('resize', photosWatchDogScreenAndFullScreen);
        document.exitFullscreen();

        setTimeout(() => {
            phs.style.setProperty("--max_size", `${maxs}`);
            car.style.width = "8%";
            car.style.height = "var(--chemin_width_carre)";
            car.style.right = ".25rem";
           // car.style.borderRadius = "0 50% 0 0";
            car2.style.transform = "scale(1,1)";
            car2.style.backgroundColor = "";
            car2.style.opacity = ".5";
           // car2.style.borderColor = "green";
           // car2.style.opacity = ".5";
           // car2.style.borderRadius = "0 50% 0 0";
            //   car2.style.top = "0";
            //  car2.style.left = "0";
            //  phs.style.borderRadius = "5%";
            //  phs.style.position = "relative";
            //   phs.style.setProperty("width", "calc(max(var(--photo-width),var(--max_size)) / 2 * var(--mult_ph-scale))");
            //  phcont.style.setProperty("height", "calc(max(var(--photo-width),var(--max_size)) /2 )");

            //if (sourceElements.length > 0) {
            //    sourceElements.forEach((sourceElement) => {
            //        sourceElement.setAttribute('sizes', '100w');
            //    });
            //}
            for (var oneimg of allimg) {
                oneimg.style.width = "100%";      // BACK 
                oneimg.style.height = "auto";
                //   oneimg.style.maxHeight = "calc(max(var(--photo-width),var(--max_size)) / 2 * .6)";
            }
            for (var cif of contifra) {
               // cif.firstChild.style.backgroundColor = "white";
                cif.firstChild.firstChild.style.color = "black";
                //  cif.firstChild.style.width = "fit-content";
                //  cif.firstChild.style.margin = "auto";
                //    cif.firstChild.style.position = "absolute";
                //    cif.firstChild.style.bottom = ".7rem";
                //  cif.before.setProperty.width = "fit-content";
            }
            if (sourceElements.length > 0) {
                sourceElements.forEach((sourceElement) => {
                    sourceElement.setAttribute('sizes', 'min(calc(100vw * .855),30rem)');
                });
            }
            // contal.style.setProperty("--before_width", "70%");
            //  contal.style.setProperty("--before_bottom", "0");
       
            //for (var cif of contifra) {
            //    //  cif.firstChild.style.position = "absolute";
            //    //   cif.firstChild.style.width = "100%";
            //    // cif.firstChild.style.bottom = "unset";
            //    //  cif.firstChild.style.position = "absolute";
            //    //cif.firstChild.style.backgroundColor = "red";
            //    //cif.firstChild.style.margin = "auto";
            //}

            togScal = true;           
           
        }, 1000);
    }
}

//var naturalX;
//var naturalY;

function photosWatchDogScreenAndFullScreen() {
    //console.log("ratio window, img: " + document.body.offsetWidth + "/" + document.documentElement.clientHeight + " ... " + naturalX + "/" + naturalY);
    //if (sourceElements.length > 0) {
    //    sourceElements.forEach((sourceElement) => {
    //        sourceElement.setAttribute('sizes', '100w');
    //    });
    //}

    // phcont.style.setProperty("height", "fit-content");
  /*  console.log(window.screen.width + " stop " + window.screen.height);*/
    if ((document.body.offsetWidth / document.documentElement.clientHeight) > (1.1)) {   // vp land reel

        //if ((window.screen.width / window.screen.height) > 1) { //for (var cif of contifra) {
        //    //cif.firstChild.style.backgroundColor = "blue";
        //    //cif.firstChild.style.margin = "-3.6rem auto auto auto";
        //    //cif.firstChild.style.position = "absolute";
        //}
        //if ((naturalX / naturalY) > 1) {   // photo land



        //}
        //else {

        //}

        //if (sourceElements.length > 0) {
        //    sourceElements.forEach((sourceElement) => {
        //        sourceElement.setAttribute('sizes', '100w');
        //    });
        //}

        for (var oneimg of allimg) {
            oneimg.style.width = "auto";
            oneimg.style.height = `${.80 * document.documentElement.clientHeight}px`; //document.documentElement.clientHeight + "px";
            //  oneimg.style.maxHeight = "none";
        }

        //if (sourceElements.length > 0) {
        //    sourceElements.forEach((sourceElement) => {
        //        sourceElement.setAttribute('sizes', '100w');
        //    });
        //}
        //contal.style.setProperty("--before_width", "60%");
        //   contal.style.setProperty("--before_bottom", ".7rem");
     
    }
    else {                                                       // vp portrait reel
        //for (var cif of contifra) {
        //    //cif.firstChild.style.backgroundColor = "red";
        //    //cif.firstChild.style.margin = "auto";
        //    //cif.firstChild.style.position = "relative";
        //}
        //if (sourceElements.length > 0) {
        //    sourceElements.forEach((sourceElement) => {
        //        sourceElement.setAttribute('sizes', '100w');
        //    });
        //}
        //if ((naturalX / naturalY) > 1) {   // photo land
        for (var oneimg of allimg) {
            oneimg.style.width = "100%";      // mode portrait 
            oneimg.style.height = "auto";
            //   oneimg.style.maxHeight = "calc(max(var(--photo-width),var(--max_size)) / 2 * .6)";
        }
  
        //if (sourceElements.length > 0) {
        //    sourceElements.forEach((sourceElement) => {
        //        sourceElement.setAttribute('sizes', '100w');
        //    });
        //}
        //  contal.style.setProperty("--before_width", "70%");
      /*  var x = contal.offsetWidth;*/
        //  contal.style.setProperty("--before_bottom", "unset");
        //for (var cif of contifra) {
        //    //  cif.firstChild.style.backgroundColor = "red";
        //    // cif.firstChild.style.width = "100%";
        //    //  cif.firstChild.style.margin = "auto";
        //    //  cif.firstChild.style.position = "relative";
        //    //            cif.firstChild.style.bottom = "0";
        //    //  cif.before.style.width = "70%";
        //}

        //}
        //else {

        //}

    }
    //if (sourceElements.length > 0) {
    //    sourceElements.forEach((sourceElement) => {
    //        sourceElement.setAttribute('sizes', '100w');
    //    });
    //}

    setTimeout(() => {
        if (document.fullscreenEnabled && phs.requestFullscreen && document.fullscreenElement ? false : true) {
            phs.requestFullscreen(); 
            dotNet.invokeMethodAsync("PauseAutoDefil");
        }
    }, 1000); // Instructions à exécuter après le rafraîchissement du DOM
   
    //if (sourceElements.length > 0) {
    //    sourceElements.forEach((sourceElement) => {
    //        sourceElement.setAttribute('sizes', '100w');
    //    });
    //}

    //if ((document.body.offsetWidth / document.documentElement.clientHeight) < (naturalX / naturalY)) {  // l'ecran est proportionnellement moins large que la largeur reele de la photo
    //    console.log("ratio screen < ratio img");

    //     phs.style.setProperty("width", "95vw");

    //for (var oneimg of allimg) {
    //    //oneimg.style.width = "100%";      // mode portrait 
    //    //oneimg.style.height = "auto";
    //  //  oneimg.style.maxHeight = "none";
    //}


    /*console.log("mode portrait!");*/
    //}
    //else {
    //    phs.style.setProperty("width", "auto");

    //    for (var oneimg of allimg) {
    //        oneimg.style.width = "auto";                        // mode land 
    //        oneimg.style.height = .95 * document.documentElement.clientHeight + "px";//  "95vh";
    //        oneimg.style.maxHeight = "none";
    //    }

    //setTimeout(() => {
    //    if ("ontouchstart" in window || window.DocumentTouch && document instanceof DocumentTouch) {

    //        if (document.fullscreenEnabled && phs.requestFullscreen && document.fullscreenElement ? false : true) {
    //            phs.requestFullscreen();
    //        }
    //    }
    //    console.log("mode land ho");
    //}, 1200);

    //}
}

function getPhotoSize(dotNet) {

    var photo = document.getElementById("contPhAloneJs").parentElement;

    const getCalculatedSize = () => {

        let photoCalculatedSize = window.getComputedStyle(photo).getPropertyValue("width");       
        return parseInt(photoCalculatedSize, 10);
    }

    window.addEventListener("resize",()=> {

        dotNet.invokeMethodAsync("SetPhotoSize", getCalculatedSize);        
    });
   return getCalculatedSize();
}

//function imgNatural(img) {
//    console.log("Natural!");
//    naturalX = img.naturalWidth;
//    naturalY = img.naturalHeight;
//}

//function myscrollTo(el) {
//    var elem = document.getElementById(el);
//    window.scrollTo(0, elem.offsetTop + elem.offsetHeight / 2 + document.documentElement.clientHeight / 2);
//}

//function autoDefilSurveillor(dotNet) {
//    window.addEventListener('resize', () => {
//        dotNet.invokeMethodAsync('StopAndRestartAutoDefil');
//    });
//}
    //protected override async Task OnAfterRenderAsync(bool firstRender)
    //{
    //    if (firstRender)
    //        await js.InvokeVoidAsync("autoDefilSurveillor", DotNetObjectReference.Create(this));
    //}

    //[JSInvokable]
    //public void StopAndRestartAutoDefil()
    //{
    //    c.WriteLine("STOPANDRESTART");
    //}

//function myscrollTo(el) {
//    var elem = document.getElementById(el);
//    var y = window.pageYOffset + elem.getBoundingClientRect().top;
//    var elementHeight = elem.offsetHeight;
//    var middle = y - (window.innerHeight / 2) + (elementHeight / 2);
//    window.scrollTo({ top: middle, behavior: 'smooth' });
//}

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

function configureLambSvgObserver() {
   
    const lambs = document.getElementById("lambcontsurv");
    const lamb = document.getElementById("lambcont")   
  
    const observerOptions = {
        root: null,
        rootMargin: '50% 0px 0px 0px', 
        threshold: 1
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

var frLamb = true;
var nmbrEl = 0;
function lamb(dotNet) {
   
    if (frLamb) {
        const ajustLamb = () => {         
         
            if (window.matchMedia("(max-width:500px)").matches) {
                console.log("lamb max width");
                nmbrEl = 8; 
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

function welcome() { // en travaux...
    console.log("welcome");
    var main = document.getElementById("maincont");
    var pay = document.getElementById("payment");
    var hlp = document.getElementById("hlp");
    var pre = document.getElementById("pretext");

    window.addEventListener('scroll', () => {

        //if (document.documentElement.scrollTop + document.documentElement.clientHeight > pay.offsetTop + main.offsetTop) {            
        //    pay.classList.add('welcome_pay');
        // } 
        if (document.documentElement.scrollTop + document.documentElement.clientHeight > hlp.offsetTop + main.offsetTop + .1 * hlp.offsetTop) {
            hlp.classList.add('welcome_hlp');
        }
        //if (document.documentElement.scrollTop + document.documentElement.clientHeight > pre.offsetTop + main.offsetTop) {
        //    console.log("YEP!");
        //    pre.classList.add('welcome_pre');
        //} 
    }, { passive: true });
    // spinScrolling();
}

//function spinScrolling() {
//    var go = document.getElementById("go");
//    var stack;

//    window.addEventListener('scroll', () => {
//        stack = document.documentElement.scrollTop;
//        go.style.transform = `rotate(${stack/2}deg)`;
//        // console.log("SPIN " + document.body.scrollTop + " " + document.body.scrollY);
//    });
//}
