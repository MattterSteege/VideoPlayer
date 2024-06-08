document.addEventListener('DOMContentLoaded', () => {
    const navbarToggler = document.querySelector('.navbar-toggler');
    const navbarNav = document.querySelector('.navbar-nav');

    // Toggle mobile menu
    navbarToggler.addEventListener('click', () => {
        navbarNav.style.right = navbarNav.style.right === '0px' ? '-410px' : '0';
    });
});
