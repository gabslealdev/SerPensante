
// header 
window.onscroll = () => transparentHeader()

let header = document.getElementById("header"); 

function transparentHeader() {
    if(window.scrollY > header.scrollTop ){
        header.classList.add("scrollTop");
    } else {
        header.classList.remove("scrollTop")
    }
}





