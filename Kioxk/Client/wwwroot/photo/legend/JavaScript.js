
function resizeIframe(obj) {
console.log("dans resize iframe");
    let f = document.getElementById("myframe");
    
    setTimeout(aftertime, 1000);
    function aftertime() {


        f.style.height = 0; f.style.height = f.contentWindow.document.documentElement.scrollHeight + 'px'; console.log("resize fin");
       

    }
}






