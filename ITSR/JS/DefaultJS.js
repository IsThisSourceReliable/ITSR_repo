function OpenLeftMenu() {
    alert("test");
    $('.page-wrap').animate({
        left: '+=322px'
    });
    $('.menu-left').animate({
        left: '+=322px'
    });
    //document.getElementById('LeftMenu').onclick = null;
    document.getElementById('LeftMenu').style.pointerEvents = 'none';
}

function CloseLeftMenu() {
    alert("hej");
    $('.page-wrap').animate({
        left: '-=322px'
    });
    $('.menu-left').animate({
        left: '-=322px'
    });
    document.getElementById('LeftMenu').style.pointerEvents = 'auto';
}

function OpenRightMenu() {
    alert("hej");
    $('.page-wrap').animate({
        left: '-=322px'
    });
    $('.menu-right').animate({
        right: '+=322px'
    });
    document.getElementById('RightMenu').style.pointerEvents = 'none';
}

function CloseRightMenu() {
    alert("hej");
    $('.page-wrap').animate({
        left: '+=322px'
    });
    $('.menu-right').animate({
        right: '-=322px'
    });
    document.getElementById('RightMenu').style.pointerEvents = 'auto';
}