﻿using DataAnnotationsExtensions;
using DomainClasses.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ViewModel.ViewModel.Admin.Page
{
    public class AddPageViewModel
    {
        [DisplayName("محتوای صفحه")]
        [Required(ErrorMessage = "محتوای صفحه را وارد کنید")]
        [AllowHtml]
        public virtual String Content { get; set; }

        [DisplayName("عنوان صفحه")]
        [Required(ErrorMessage = "عنوان صفحه را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد حروف عنوان صفحه غیر مجاز است")]
        public virtual String Title { get; set; }

        [DisplayName("توضیحات سئو")]
        [Required(ErrorMessage = "توضیحات  را وارد کنید")]
        [MaxLength(200, ErrorMessage = "تعداد حروف توضیحات  غیر مجاز است")]
        [DataType(DataType.MultilineText)]
        public virtual String Description { get; set; }

        [DisplayName("کلمات کلیدی ")]
        [Required(ErrorMessage = "کلمات کلیدی را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کلمات کلیدی  غیر مجاز است")]
        [DataType(DataType.MultilineText)]
        public virtual String KeyWords { get; set; }

        [DisplayName("رتبه نمایش")]
        [Required(ErrorMessage = "رتبه نمایش را وارد کنید")]
        [Integer(ErrorMessage = "فقط از اعداد صحیح استفاده کنید")]
        public virtual Int32 DisplayOrder { get; set; }

        [DisplayName("مکان نمایش")]
        [Required(ErrorMessage = "مکان نمایش را مشخص کنید")]
        public virtual PageShowPlace PageShowPlace { get; set; }

        [DisplayName("تصویر لینک")]
        public virtual String ImagePath { get; set; }
        
    }
}
