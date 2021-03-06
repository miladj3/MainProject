﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ViewModel.Admin.Setting
{
    public class EditSettingViewModel
    {
        [DisplayName("نام فروشگاه")]
        public String StoreName { get; set; }

        [DisplayName("کلمات کلیدی")]
        [DataType(DataType.MultilineText)]
        public String StoreKeyWords { get; set; }

        [DisplayName("توضیحات سئو")]
        [DataType(DataType.MultilineText)]
        public String StoreDescription { get; set; }

        [DisplayName("شماره تماس 1")]
        public String Tel1 { get; set; }

        [DisplayName("شماره تماس 2")]
        public String Tel2 { get; set; }

        [DisplayName("آدرس فروشگاه")]
        public String Address { get; set; }

        [DisplayName("شماره موبایل 1")]
        public String PhoneNumber1 { get; set; }

        [DisplayName("شماره موبایل 1")]
        public String PhoneNumber2 { get; set; }

        [EmailAddress]
        [DisplayName("ایمیل")]
        public String Email { get; set; }

        [DisplayName("نشان اعتماد الکتریکی")]
        public Boolean Trust { get; set; }

        [DisplayName("لوگوی سایت")]
        public String Logo { get; set; }

        [DisplayName("مدیریت نظرات")]
        public Boolean CommentModeratorStatus { get; set; }

        [DisplayName("تلگرام")]
        public String Telegram { get; set; }

        [DisplayName("اینستاگرام")]
        public String Instagram { get; set; }

        [DisplayName("فیسبوک")]
        public String Facebook { get; set; }
        
    }
}