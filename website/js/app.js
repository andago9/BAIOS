(function ($) {
    "use strict";

    $(".navbar-nav a[href^='#']").on("click", function () {
        $(".navbar-collapse").collapse("hide");
    });
})(window.jQuery);
