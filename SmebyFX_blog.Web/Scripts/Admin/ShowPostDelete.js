(function () {

    $("#deletePost").click(function (e) {
        e.preventDefault();
        $(".overlayArea").show();
    });

    $(".overlayAreaBackgroundOverlay").click(function () {
        $(".overlayArea").hide();
    });

    $(".overlayAreaCloseButton").click(function () {
        $(".overlayArea").hide();
    });

    $("#cancelButton").click(function (e) {
        e.preventDefault();
        $(".overlayArea").hide();
    });

})();