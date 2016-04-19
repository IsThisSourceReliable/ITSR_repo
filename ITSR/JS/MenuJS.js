//Function evaluates hidden label to see wheter or not 
//left menu is open or closed in order to open or close said menu
function LeftMenuOpenClose()
{
    var str = $('.left-menu-lbl').text();

    if (str == 0)
    {
        OpenLeftMenu();
        $('.left-menu-lbl').text("1");
    }
    else
    {
        CloseLeftMenu()
        $('.left-menu-lbl').text("0");
    }

}

//Function evaluates hidden label to see wheter or not 
//right menu is open or closed in order to open or close said menu
function RightMenuOpenClose()
{
    var str = $('.right-menu-lbl').text();

    if (str == 0) {
        OpenRightMenu();
        $('.right-menu-lbl').text("1");
    }
    else {
        CloseRightMenu();
        $('.right-menu-lbl').text("0");
    }
}

//Opens left menu
function OpenLeftMenu() {
        $('.container').animate({
            left: '+=322px'
        });
        $('.menu-left').animate({
            left: '+=322px'
        });
        $('.books-background').animate({
            left: '+=322px'
        });
}

//Closes left menu
function CloseLeftMenu() {
    $('.container').animate({
        left: '-=322px'
    });
    $('.menu-left').animate({
        left: '-=322px'
    });
    $('.books-background').animate({
        left: '-=322px'
    });
}

//Opens right menu
function OpenRightMenu() {
    $('.container').animate({
        left: '-=322px'
    });
    $('.books-background').animate({
        left: '-=322px'
    });
    $('.menu-right').animate({
        right: '+=322px'
    });
}

//Closes right menu
function CloseRightMenu() {
    $('.container').animate({
        left: '+=322px'
    });
    $('.books-background').animate({
        left: '+=322px'
    });
    $('.menu-right').animate({
        right: '-=322px'
    });
}