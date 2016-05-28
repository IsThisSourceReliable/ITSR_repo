function showDiv() {
    $(".PasswordDiv").slideToggle("slow", function () {
        
    });
    $("#BtnNewPassword").show();
    $("#Button1").hide();
}
function hideDiv() {
    $(".PasswordDiv").slideToggle("slow", function () {

    });
    $("#Button1").show();
}
function SavedNewData() {
    $(".page-overlay").show();
}
