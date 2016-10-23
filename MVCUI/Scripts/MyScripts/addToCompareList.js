 function _mess(_messResult) {
            $('#_load_result_in_top').children('p').text(_messResult).end().stop(true, false).slideDown('fast').delay(2000).slideUp('slow');
        };

// #region Add To Compare
$(function () {
    $(document).on('click', '._addToCompare', function () {
        var $_thisCompare = $(this);
        var $_IdCompare = $_thisCompare.data('id');
        var $$__urlAddToComapre = $(this).data('url');
        $.ajax({
            url: $$__urlAddToComapre,
            async: true,
            cache: false,
            type: 'Post',
            data: {
                productId: $_IdCompare
            }
        }).done(function (res) {
            if (res.result) {
                if (res.hasMessage) {
                    _mess(res.message);

                    var $_elcompare = $('#m-compare').find('ul');
                    var $_content_compare = '<li id="compare_el_' + res.el.id + '">\
                                <i class="fa fa-times" data-id="' + res.el.id + '" aria-hidden="true"></i>\
                                <strong>'+ res.el.name + '</strong>\
                                <img src="' + res.el.pic + '" />\
                        </li>';
                    $($_content_compare).prependTo($_elcompare);
                }
            } else {
                if (res.hasMessage) {
                    _mess(res.message);
                }
            }
        }).fail(function (res) {
            _mess('عملیات با مشکل مواجه گردید. لطفا یکبار دیگر امتحان نمایید');
        })
    })

    $(document).on('click', '#m-container-popup-menu i', function () {
        var $_this_remove_compare = $(this);
        var id = $_this_remove_compare.data('id');
        $.ajax({
            url: '@Url.Action(MVC.Product.ActionNames.removeProductCompareFromHeader, MVC.Product.Name)',
            async: true,
            cache: false,
            type: 'Post',
            data: {
                productId: id
            },
            success: function () {
                $_this_remove_compare.parent('li').fadeOut().delay(400).remove();
            }
        })
    })
})

// #endregion