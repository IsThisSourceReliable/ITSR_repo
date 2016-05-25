function showHistoryBox() {
    $(".history-box").slideToggle("slow", function () { });
    $(".roles-box").hide();
}

function showRolesBox() {
    $(".roles-box").slideToggle("slow", function () { });
    $(".history-box").hide();
}

function MakeBtnSmaller() {
    $(".MakeHalfBox").animate({ width: "73%" }, function () {
        $('.HiddenBtn').slideToggle("fast"), function () {

        };
    });
}

function ShowSearchResult() {
    $(".listView").fadeIn("xslow", function () { });

}

function MakeBtnBigger() {
        $(".listView").fadeOut("xslow", function () {
            $(".HiddenBtn").slideToggle("fast", function () {
                $(".MakeHalfBox").animate({ width: "100%" }, function () { });
            });
        });
        $(".tbSearch").val("");
 
}
function HideBtn() {
    $(".HiddenBtn").hide();
}