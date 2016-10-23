function _mess(_messResult) {
    $('#_load_result_in_top').children('p').text(_messResult).end().stop(true, false).slideDown('fast').delay(2000).slideUp('slow');
};

// #region Add To Shopping Card

$(function () {
    $('.m_btn_add_to_cart').on('click', function () {
       
        var $$__this_btn_add_to_card = $(this);
        var $$___data_for_add_to_checkout = $(this).data('id-product');
        var $$__urlAddtoCompare = $(this).data('id-url');
        var _w = function () {
            $$__this_btn_add_to_card.append('<i style="vertical-align: bottom;margin-right: 10px;" class="fa fa-refresh fa-spin fa-2x fa-fw" aria-hidden="true"></i>').addClass('process_add_btn');
        }
        var _e = function () {
            $$__this_btn_add_to_card.find('i').fadeOut('slow').end().removeClass('process_add_btn');
        }
        $.ajax({
            url: $$__urlAddtoCompare,
            cache: false,
            async: true,
            type: 'POST',
            data: {
                productId: $$___data_for_add_to_checkout,
                value: 1
            },
            beforeSend: function (xhr) {
                _w();
            },
            statusCode: {
                403: function () {
                    _mess('لطفا وارد حساب کاربری خود شوید');
                    $$__this_btn_add_to_card.removeClass('process_add_btn');
                }
            }
        }).done(function (res) {
            if (res.result) {
                if (res.hasmessage) {
                    _mess(res.message);
                    if (res.hasModel) {
                        var $$_el = ' <li data-id="' + res.value.id + '" class="countNumberShoppingCard_' + res.value.id + '">\
                                            <i class="fa fa-times removeShoppingCartItem" aria-hidden="true"></i>\
                                            <strong>' + res.value.name + '<bdi class="number_persian">(' + res.value.QuanitityItem + ')</bdi></strong>\
                                            <img src="'+ res.value.image + '" />\
                                        </li>';
                        $($$_el).prependTo('#m-cart-info ul');

                        $('#m-cart-item').find('#shoppingCart').text(res.value.totalNumberer);
                    }
                    else {
                        $('#m-cart-item').find('#shoppingCart').text(res.value.totalNumberer);
                        $('.countNumberShoppingCard_' + res.value.id + '').find('bdi').text('(' + res.value.QuanitityItem + ')');
                    }
                }
            }
            else {
                if (res.hasmessage) {
                    _mess(res.mesaage);
                } else {
                    _mess('عملیات با مشکل مواجه گردید. لطفا یکبار دیگر امتحان نمایید');
                }
            }
            _e();
        }).fail(function (res) {
            _mess('عملیات با مشکل مواجه گردید. مشکل در ارسال اطلاعات');
            _e();
        })
    })
})

// #endregion

// #region Remove product from shopping card

$(function () {
    $(document).on('click', '.removeShoppingCartItem', function () {
        var $$__this_remove_shopping = $(this);
        var $$__li_remove_shopping = $$__this_remove_shopping.parents('._parent');
        var $$__data_id_remove_shopping = $$__li_remove_shopping.data('id');
        var $$__data_url_remove_shopping = $$__li_remove_shopping.parents('._parent_shopping_Card').data('url');
        $.ajax({
            url: $$__data_url_remove_shopping,
            async: true,
            cache: false,
            type: 'POST',
            data: {
                shoppingId: $$__data_id_remove_shopping
            },
            statusCode: {
                403: function () {
                    _mess('لطفا وارد حساب کاربری خود شوید');
                }
            }
        }).done(function (res) {
            if (res.result) {
                if (res.hasmessage) {
                    _mess(res.message);
                }
                else {
                    _mess('عملیات انجام شد.');
                }
                $('.countNumberShoppingCard_' + $$__data_id_remove_shopping + '').fadeOut('fast').remove();
                $('#shoppingCart').text(res.count);
            }
            else {
                _mess('عملیات با مشکل مواجه گردید. لطفا یکبار دیگر امتحان نمایید');
            }
        }).fail(function () {
            _mess('مشکل در ارسال اطلاعات به سرور، لطفا مجددا تلاش نمایید');
        });
    })
})

// #endregion

// #region Ajax Methods

function person_detail_order_OnSuccess(res) {
    $('#__loading_el_order').fadeOut('slow', function () {
        $('#_load_complete_order').html(res);
        $.validator.unobtrusive.parse($('#_load_complete_order'));
    });
}

function person_detail_order_OnBegin() {
    $('#_load_complete_order').fadeOut('slow').empty().fadeIn('fast').append('\
                          <div class="__loading_el_order_class" id="__loading_el_order">\
                                   <i class="fa fa-refresh fa-spin fa-2x fa-fw" aria-hidden="true"></i>\
                          </div>');
}

function edit_user_order_OnBegin() {
    $('#_load_complete_order').append('\
                          <div class="__loading_el_order_class" id="__loading_el_confirm_order">\
                                   <i class="fa fa-refresh fa-spin fa-2x fa-fw" aria-hidden="true"></i>\
                          </div>');
}

function edit_user_order_OnSuccess(res) {
    $('#_load_complete_order').empty().html(res);
    $.validator.unobtrusive.parse($('#_load_complete_order'));
}

function complete_order_OnBegin() {
    $('#_confirm_order').fadeIn('fast');
    $('#__complete_order_btn').fadeOut('slow');
    $('#__edit_person_order_btn').fadeOut('slow');
}
// #endregion