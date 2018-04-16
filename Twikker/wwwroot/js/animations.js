$(document).ready(function () {
    $(".post").mouseenter(function () {
        console.log("hover");
        $(".delete-button").fadeOut();
    });
});