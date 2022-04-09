const ul = document.querySelector('.navul');
window.onscroll = function () {
    var top = window.scrollY;
    console.log(top);
    if (top > 1) {
        ul.classList.add('active')
    }
    else {
        ul.classList.remove('active')
    }
}

var i = 0;
var images = [];
var time = 3000;

images[0] = '/images/bestseller1.jpg';
images[1] = '/images/bestseller2.jpg';
images[2] = '/images/bestseller3.jpg';

function changeImg() {
    document.getElementById("homeImage").src = images[i];

    if (i < images.length - 1) {
        i++;
    }
    else {
        i = 0;
    }

    setTimeout("changeImg()", time);
}

window.onload = changeImg;