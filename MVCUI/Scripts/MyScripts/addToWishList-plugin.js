﻿// <![CDATA[
(function ($) {
    $.fn.ToWish = function (options) {
        var defaults = {
            postInfoUrl: '/',
            loginUrl: '/Customer/Login',
            errorHandler: null,
            completeHandler: null,
            isInYourWishHandler: null,
            limitationHandler: null,
            authorizationHandler: function () { window.location = options.loginUrl; }
        };
        var options = $.extend(defaults, options);

        return this.each(function() {
            var product = $(this);
            var dataJsonArray = { productId: product.attr('id').replace('toWish-', '') };
            $.ajaxSetup({ cache: false });
            $.ajax({
                type: "POST",
                url: options.postInfoUrl,
                data: JSON.stringify(dataJsonArray),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                complete: function(xhr, status) {
                    var data = xhr.responseText;
                    if (xhr.status == 403) {
                        options.authorizationHandler(this);
                    } else if (status === 'error' || !data) {
                        if (options.errorHandler)
                            options.errorHandler(this);
                    } else if (data == "nok") {
                        if (options.isInYourWishHandler)
                            options.isInYourWishHandler(this);
                    } else if (data == "limit") {
                        if (options.limitationHandler)
                            options.limitationHandler(this);
                    } else {
                        if (options.completeHandler)
                            options.completeHandler(this);
                    }
                }
            });
            return false;
        });
    };
})(jQuery);
// ]]>