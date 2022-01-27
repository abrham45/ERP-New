// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    $('[data-toggle="tooltip"]').tooltip();
     
    function getNotification() {

        $.ajax({
            url: "/Notification/getNotification",
            method: "GET",
            success: function (result) {
                console.log(result);

            },

            error: function (error) {
                console.log(error);
            }
        });
    }

    getNotification();

}); 

   