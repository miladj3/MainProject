$(function () {
    $(document).on('click', '._addToPopulator', function () {
        var $_productId_P = $(this).data('productid'),
            $__url = $(this).data('url'),
            _messResult;
        function _mess() {
            $('#_load_result_in_top').children('p').text(_messResult).end().stop(true, false).slideDown('fast').delay(2000).slideUp('slow');
        };
        $.ajax({
            type: 'POST',
            traditional: true,
            async: true,
            cache: false,
            url: '' + $__url + '',
            data: {
                productId: $_productId_P
            },
            statusCode: {
                403: function () {
                    _messResult = 'برای ثبت امتیاز، به حساب کاربری خود وارد شوید';
                    _mess();
                    return false;
                }
            }
        }).done(function (result) {
            if (result.result) {
                if (result.hasMessage) {
                    _messResult = result.message;
                    _mess();
                }
            } else {
                if (result.hasMessage) {
                    _messResult = result.message;
                    _mess();
                }
            }
        }).fail(function () {
            _messResult = 'مشکل در ارسال اطلاعات.';
            _mess();
        })
    })
})
