(function() {

    $("#newTag").click(function (e) {
        e.preventDefault();
        $(".overlayArea").show();

        $("#editTagForm input[name=TagId]").val("");
        $("#editTagForm input[name=TagTitle]").val("");
        $("#editTagForm input[name=TagUrlSlug]").val("");
    });

    $(".editTag").click(function(e) {
        e.preventDefault();
        $(".overlayArea").show();

        var id = $(this).siblings(".trId").text();
        var title = $(this).closest("tr").find(".trTitle").text();
        var slug = $(this).closest("tr").find(".trSlug").text();

        $("#editTagForm input[name=TagId]").val(id);
        $("#editTagForm input[name=TagTitle]").val(title);
        $("#editTagForm input[name=TagUrlSlug]").val(slug);
    });

    $(".overlayAreaBackgroundOverlay").click(function() {
        $(".overlayArea").hide();
    });

    $(".overlayAreaCloseButton").click(function() {
        $(".overlayArea").hide();
    });

})();