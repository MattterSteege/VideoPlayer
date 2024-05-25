function toggleFullScreen() {
    //if main#main has class full
    if (document.getElementById('main').classList.contains('full')) {
        //remove class full
        document.getElementById('main').classList.remove('full');
        document.querySelector('.sidebar').style.display = 'block';
    }
    
    //if main#main does not have class full
    else {
        //add class full
        document.getElementById('main').classList.add('full');
        document.querySelector('.sidebar').style.display = 'none';
    }
}