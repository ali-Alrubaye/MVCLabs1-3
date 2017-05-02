/// <reference path="jquery-3.1.1.js" />

$(document).ready(function () {
    debugger;
    var form = $("form");
    form.submit(function (e) {
        e.preventDefault();
      
        $.ajax({
            
            method: "POST",
            url: "/Photo/Upload",
            data: new FormData(document.getElementById("form")[0]),
            success: function(data) {
                $("div#result").html(data);
            },
            processData: false,
            contentType: false
        });
    });
});