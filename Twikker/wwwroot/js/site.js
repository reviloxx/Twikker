$(document).ready(function () {
    $(".deletePostButton").click(function (e) {
        var id = $(this).attr('id')

        $.ajax({
            type: "POST",
            url: "/Home/DeletePost",
            data: {
                'id': id
            },
            async: true,
            success: function (msg) {
            },
            error: function () {
                return "error";
            }
        });
    });
});