function showHistoryBox() {
    $(".history-box").slideToggle("slow", function () { });
    $(".roles-box").hide();
}

function showRolesBox() {
    $(".roles-box").slideToggle("slow", function () { });
    $(".history-box").hide();
}