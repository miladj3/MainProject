using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ViewModel.Admin.SlideShow
{
    public class EditSlideShowViewModel
    {
        public Int64 Id { get; set; }

        [DisplayName("عنوان")]
        [MaxLength(30, ErrorMessage = "تعداد حروف عنوان غیر مجاز است")]
        [Required(ErrorMessage = "عنوان نباید خالی باشد")]
        public String Title { get; set; }

        [DisplayName("آدرس تصویر")]
        [Required(ErrorMessage = "آدرس تصویر نباید خالی باشد")]
        public String ImagePath { get; set; }

        [DisplayName("متن جایگزین")]
        [Required(ErrorMessage = "متن جایگزین نباید خالی باشد")]
        [MaxLength(30, ErrorMessage = "تعداد حروف متن جایگزین غیر مجاز است")]
        public String ImageAltText { get; set; }

        [DisplayName("لینک")]
        [Required(ErrorMessage = "لینک نباید خالی باشد")]
        public String Link { get; set; }

        [DisplayName("توضیحات")]
        [MaxLength(300, ErrorMessage = "تعداد حروف توضیحات غیر مجاز است")]
        [Required(ErrorMessage = "توضیحات نباید خالی باشد")]
        [DataType(DataType.MultilineText)]
        public String Description { get; set; }

        [DisplayName("مکان توضیحات")]
        [Required(ErrorMessage = "مکان توضیخات را انتخاب کنید")]
        public virtual String Position { get; set; }

        [DisplayName("جهت ظاهر شدن")]
        [Required(ErrorMessage = "جهت ظاهر شدن را انتخاب کنید")]
        public virtual String ShowTransition { get; set; }

        [DisplayName("جهت محو شدن")]
        [Required(ErrorMessage = " جهت محو شدن را انتخاب کنید")]
        public virtual String HideTransition { get; set; }

        [DisplayName("فاصله عمودی")]
        [Required(ErrorMessage = "فاصله عمودی کادر توضیحات را وارد کنید")]
        [Range(0, 100, ErrorMessage = "درصد تخفیف میبایست 0 تا 100 انتخاب شود")]
        [Integer(ErrorMessage = "فقط از اعداد صحیح استفاده کنید")]
        public virtual Int32 DataVertical { get; set; }

        [DisplayName("فاصله افقی")]
        [Required(ErrorMessage = "فاصله افقی توضیحات را وارد کنید")]
        [Range(0, 100, ErrorMessage = "درصد تخفیف میبایست 0 تا 100 انتخاب شود")]
        [Integer(ErrorMessage = "فقط از اداد صحیح استفاده کنید")]
        public virtual Int32 DataHorizontal { get; set; }
    }
}
