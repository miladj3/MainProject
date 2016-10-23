var $_v = false,
$this,
$_i,
$__val,
$_rate,
$__url,
$__productId,
$_input,
$_rateNow;

$('.xx_starRating > i').hover(
     function (e) {
         $this = $(this).parents('.xx_starRating');
         $_v = $this.find('input').data('set');
         $_i = $this.find('i');
         $_rate = $this.find('input').attr('data-rate');

         if ($_i.hasClass('fa-star')) {
             $_i.removeClass('fa-star').addClass('fa-star-o');
         }
         $(this).removeClass('fa-star-o').addClass('fa-star').prevAll().removeClass('fa-star-o').addClass('fa-star');
     },
     function () {
         if ($_rate > 0) {
             $_v = true;
             this.parentNode.getElementsByTagName("INPUT")[0].setAttribute('data-set', true);
             _set($_rate);
             return;
         }
         if (!$_v) {
             $(this).removeClass('fa-star').addClass('fa-star-o').prevAll().removeClass('fa-star').addClass('fa-star-o');
         }
         else {
             _set();
         }
     }
)
$('.xx_starRating > i').click(function () {
    var $th = $(this);
    var thJS = this;
    $__val = $th.data('val');
    $_i.removeClass('fa-star').addClass('fa-star-o');
    $th.removeClass('fa-star-o').addClass('fa-star').prevAll().removeClass('fa-star-o').addClass('fa-star');
    $_v = true;
    $_rate = $__val;
    $__url = $this.find('input').data('url');
    $__productId = $this.find('input').data('productid');
    var _messResult;
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
            productId: $__productId,
            value: $__val,
            sectionType: 'PRODUCT'
        },
        statusCode: {
            403: function () {
                _messResult = 'برای ثبت امتیاز، به حساب کاربری خود وارد شوید';
                _mess();
                $_rate = $this.find('input').data('rate');
                _set($_rate);
                return false;
            }
        }
    }).done(function (result) {
        if (result.result) {
            if (result.hasMessage) {
                _messResult = result.message;
                _mess();
                _set($_rate);
                var j = thJS.parentNode.getElementsByTagName("INPUT")[0];
                j.setAttribute('data-set', true);
                j.setAttribute('data-rate-now', $__val);
                j.setAttribute('data-rate', $__val);
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

function _set(p) {
    var _tx = $__val;
    if (p || p != 0) {
        _tx = Math.round(p);
    }
    $_i.removeClass('fa-star').addClass('fa-star-o');
    $this.find('[data-val="' + _tx + '"]').removeClass('fa-star-o').addClass('fa-star').prevAll().removeClass('fa-star-o').addClass('fa-star');
}

