﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses.Enums
{
    #region User
    public enum UserOperations
    {
        [Display(Name = "کارهای دسته جمعی")]
        AllWorks,
        [Display(Name = "حذف")]
        DeleteSelectedUses,
        [Display(Name = "قفل ")]
        BanedSelectedUsers,
        [Display(Name = "فعال")]
        UnBanedSelectedUsers
    }
    public enum Roles
    {
        [Display(Name = "تغییر نقش")]
        NoRole,
        [Display(Name = "مدیر سایت")]
        Admin,
        [Display(Name = "مشتری")]
        Customer
    }
    public enum UserOrderBy
    {

        [Display(Name = "نام کاربری")]
        UserName,
        [Display(Name = "تاریخ عضویت")]
        RegisterDate,
        [Display(Name = "تعداد خرید")]
        OrderCount
    }
    public enum UserSearchBy
    {
        [Display(Name = "نام کاربری")]
        UserName,
        [Display(Name = "شماره همراه")]
        PhoneNumber,
        [Display(Name = "آی پی")]
        Ip,
        [Display(Name = "نقش")]
        RoleDescription
    }
    public enum VerifyUserStatus
    {
        VerifiedSuccessfully,
        UserIsBaned,
        VerifiedFaild
    }
    public enum AddUserStatus
    {
        UserNameExist,
        PhoneNumberExist,
        AddingUserSuccessfully
    }
    public enum EditedUserStatus
    {
        UserNameExist,
        PhoneNumberExist,
        UpdatingUserSuccessfully
    }
    public enum ChangePasswordResult
    {
        ChangedSuccessfully,
        ChangedFaild
    }
    #endregion

    #region Public
    public enum Order
    {
        [Display(Name = "صعودی")]
        Asscending,
        [Display(Name = "نزولی")]
        Descending
    }
    public enum PageCount
    {
        [Display(Name = "10")]
        Count10 = 10,
        [Display(Name = "30")]
        Count30 = 30,
        [Display(Name = "50")]
        Count50 = 50
    }
    #endregion

    #region product
    public enum ProductOrderBy
    {
        [Display(Name = "نام")]
        Name,
        [Display(Name = "تعداد در انبار")]
        StockCount,
        [Display(Name = "تعداد فروش")]
        SellCount,
        [Display(Name = "تعداد مشاهده")]
        ViewCount,
        [Display(Name = "تعداد رزرو شده در کارت")]
        ReserveCount,
        [Display(Name = "قیمت")]
        Price,
        [Display(Name = "درصد تخفیف")]
        DiscountPercent,
        [Display(Name = "مینیمم مقدار هشدار")]
        NotificationStockMinimun
    }
    #endregion

    #region Comment & Rate & Post & Label
    public enum CommentSectionType
    {
        Post,
        Page,
        Product
    }
    public enum RateSectionType
    {
        Comment,
        Post,
        Product
    }
    public enum PostOrderBy
    {
        ById,
        ByTitle,
        ByVisitedNumber,
        CommentCount
    }
    public enum LabelSearchBy
    {
        Name,
        Description
    }
    public enum LabelOrderBy
    {
        Id,
        Name,
        Description,
        PostCount
    }
    public enum UpdatePostStatus
    {
        Successfull,
        Faild
    }
    public enum CommentOrderBy
    {
        AddDate,
        IsApproved,
        LikeCount,
        UserName
    }
    public enum CommentSearchBy
    {
        UserName,
        Content
    }
    #endregion

    #region Cache Control
    public enum CacheControlType
    {
        [Description("public")]
        Public,
        [Description("private")]
        Private,
        [Description("no-cache")]
        Nocache,
        [Description("no-store")]
        Nostore
    }
    #endregion

    #region Other
    public enum PSFilter
    {
        All,
        New,
        MoreSell,
        MoreView,
        Beloved,
        HasDiscount,
        FreeSend,
        IsInStock
    }
    public enum PageShowPlace
    {
        [Display(Name = "فوتر سایت")]
        Footer,
        [Display(Name = "داخل بدنه سایت")]
        Body
    }
    public enum OrderStatus
    {
        Delivered,
        Posted,
        Seen,
        NoSeen
    }
    public enum PeymentType
    {
        Online,
        AtHome
    }
    public enum ProductType
    {
        [Display(Name ="تعدادی")]
        Packing,
       [Display(Name ="کیلویی")]
        NoPacking,
        [Display(Name = "همه")]
        All = 5
    }
    public enum UserRegisterType
    {
        Active,
        NotActive
    }
    public enum AttributeType
    {
        Checkbox,
        Text
    }
    #endregion

    #region Contact
    public enum ContactType
    {
        [Display(Name = "مشکل")]
        Problem,
        [Display(Name = "سفارش")]
        Order,
        [Display(Name = "پیشنهاد")]
        Offer,
        [Display(Name = "انتقاد")]
        Criticism,
        [Display(Name = "تماس با ما")]
        All
    }
    
    #endregion

    #region Page
    public enum PageSearchBy
    {
        Title
    }
    public enum PageOrderBy
    {
        Title,
        Date,
        CommentCount,
        CommentStatus,
    }
    #endregion

    #region Category
    public enum CategorySearchBy
    {
        Name,
        Description
    }
    public enum CategoryOrderBy
    {
        Id,
        Name,
        DisplayOrder
    }
    public enum AddCategoryStatus
    {
        AddSuccessfully,
        CategoryNameExist
    }
    public enum EditCategoryStatus
    {
        EditSuccessfully,
        CategoryNameExist
    }
    #endregion

    #region Currency
    public enum Currency
    {
        [Display(Name ="ریال")]
        Rial,
        [Display(Name ="تومان")]
        Toman
    }
    #endregion

    #region SiteMap
    public enum ChangeFreq
    {
        Always,
        Hourly,
        Daily,
        Weekly,
        Monthly,
        Yearly,
        Never
    }
    #endregion
}
