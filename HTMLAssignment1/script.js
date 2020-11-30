const navbar = document.querySelector('.navbar');
const navbarCollapse = document.querySelector('.navbar-collapse.collapse');
const toggler = document.querySelector('.navbar-toggler');
const hamburger = document.querySelector('#hamburger');
const home = document.getElementById('banner-section').offsetTop;
const about = document.getElementById('about').offsetTop;
const services = document.getElementById('services').offsetTop;
const contact = document.getElementById('contact').offsetTop;
const homeLink = document.getElementById('home-link');
const aboutLink = document.getElementById('about-link');
const serviceLink = document.getElementById('service-link');
const contactLink = document.getElementById('contact-link');

window.onscroll = e => {
    navbar.style.backgroundColor = "rgba(71, 71, 71, " + (window.scrollY / window.innerHeight) + ")";

    if(window.scrollY < about - 100){
        aboutLink.parentElement.classList.remove('active');
        serviceLink.parentElement.classList.remove('active');
        contactLink.parentElement.classList.remove('active');
        homeLink.parentElement.classList.add('active');
    }
    else if(window.scrollY > (about-100) && window.scrollY < (services-200)){
        homeLink.parentElement.classList.remove('active');
        serviceLink.parentElement.classList.remove('active');
        contactLink.parentElement.classList.remove('active');
        aboutLink.parentElement.classList.add('active');
    }
    else if(window.scrollY > (services-200) && window.scrollY < (window.scrollMaxY-20)){
        aboutLink.parentElement.classList.remove('active');
        homeLink.parentElement.classList.remove('active');
        contactLink.parentElement.classList.remove('active');
        serviceLink.parentElement.classList.add('active');
    }
    else if(Math.ceil(window.scrollY) >= window.scrollMaxY){
        aboutLink.parentElement.classList.remove('active');
        serviceLink.parentElement.classList.remove('active');
        homeLink.parentElement.classList.remove('active');
        contactLink.parentElement.classList.add('active');
    }
}

toggler.onclick = e => {
    if(navbarCollapse.classList.contains('show')){
        hamburger.classList.toggle('fa-bars');
        hamburger.classList.toggle('fa-times');
        setTimeout(
            () => {
                navbar.classList.toggle('navbar-bg');
            }, 300
        )
    }
    else{
        hamburger.classList.toggle('fa-bars');
        hamburger.classList.toggle('fa-times');
        navbar.classList.toggle('navbar-bg');
    }
}

function transfer(e) {
    e.preventDefault();
    let targetHref = null;
    if(e.target.tagName != "A"){
        targetHref = e.target.parentElement.attributes[0].value;    
    }
    else{
        targetHref = e.target.attributes[0].value;
    }
    if(targetHref === "#"){
        window.scrollTo(0,0);
    }
    else{
        window.scrollTo(0, (document.querySelector(targetHref).offsetTop-80));
    }
}