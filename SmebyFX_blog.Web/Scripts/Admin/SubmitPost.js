(function () {

    $(".submitPostButton").click(function (e) {
        e.preventDefault();
        var selectedTags = $("#tagCheckBoxes").find("input[type=checkbox]:checked");

        var selectedTagsValues = [];
        for (var i = 0; i < selectedTags.length; i++) {
            selectedTagsValues.push(selectedTags[i].value);
        }

        $("input[name=SelectedTags]").val(selectedTagsValues.join('-'));
        $(".submitPostForm").submit();
    });

})();