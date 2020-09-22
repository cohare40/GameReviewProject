$(document).ready(function() {

    /* var scroll = $('.game-feed').infiniteScroll({
         // options
         path: function() {
             return $("#ajaxUrl").val() + '&pageNumber=' + this.pageIndex-1;
         },
         prefill: true
     });*/

    /* window.onload = function() { */
    var nextHandler = function(pageIndex) {
        jQuery.ajax({
            type: "GET",
            url: jQuery("#ajaxUrl").val(),

            success: function(result) {
                jQuery(".game-feed").append(result);
            }
        });

    };


    var ias = new InfiniteAjaxScroll(".game-feed",
        {
            item: "#gameItem",
            next: nextHandler(0),
            pagination: false
        });

});