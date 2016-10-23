namespace DataLayer.Migrations
{
    using DomainClasses.Entities;
    using DomainClasses.Enums;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Utilities.Security;
    public sealed class Configuration : DbMigrationsConfiguration<Context.ShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Context.ShopDbContext context)
        {

            //context.Roles.AddOrUpdate(
            //    x => new { x.Name, x.Description },
            //     new Role { Name = "user", Description = "مشتری" });

            //context.SiteOptions.AddOrUpdate(op => new { op.Name, op.Value },
            //    new SiteOption { Name = "StoreName", Value = "" },
            //    new SiteOption { Name = "StoreKeyWords", Value = "" },
            //    new SiteOption { Name = "StoreDescription", Value = "" },
            //    new SiteOption { Name = "Tel1", Value = "" },
            //    new SiteOption { Name = "Tel2", Value = "" },
            //    new SiteOption { Name = "Address", Value = "" },
            //    new SiteOption { Name = "PhoneNumber1", Value = "" },
            //    new SiteOption { Name = "PhoneNumber2", Value = "" },
            //    new SiteOption { Name = "CommentModeratorStatus", Value = true.ToString() },
            //    new SiteOption { Name = "Email", Value = "" },
            //     new SiteOption { Name = "Trust", Value = "" },
            //     new SiteOption { Name = "Telegram", Value = "" },
            //     new SiteOption { Name = "Instagram", Value = "" },
            //     new SiteOption { Name = "Facebook", Value = "" },
            //     new SiteOption { Name = "Logo", Value = "@@@@" }
            //    );

            //context.Users.AddOrUpdate(u => u.UserName, new User
            //{
            //    RegisterDate = DateTime.Now,
            //    IsBaned = false,
            //    RegisterType = UserRegisterType.Active,
            //    PhoneNumber = "09338005047",
            //    Password = Encryption.EncryptingPassword("09338005047"),
            //    Role = new Role
            //    {
            //        Name = "admin",
            //        Description = "مدیر"
            //    },
            //    UserName = "admin"
            //});

            //context.Categories.AddOrUpdate(x => x.Id, new Category
            //{
            //    Id = 0,
            //    Description = "توضیحات گروه لاغری",
            //    KeyWords = "کلمات کلیدی گروه لاغری",
            //    Name = "لاغری",
            //    DisplayOrder = 0,
            //    DiscountPercent = 0
            //});

            //context.Categories.AddOrUpdate(x => x.Id, new Category
            //{
            //    Id = 1,
            //    Description = "توضیحات گروه آرایشی و زیبایی",
            //    KeyWords = "کلمات کلیدی گروه آرایشی و زیبایی",
            //    Name = "آرایشی و زیبایی",
            //    DisplayOrder = 1,
            //    DiscountPercent = 0
            //});

            //context.Categories.AddOrUpdate(x => x.Id, new Category
            //{
            //    Id = 3,
            //    Description = "توضیحات گروه جراحی",
            //    KeyWords = "کلمات کلیدی گروه جراحی",
            //    Name = "جراحی",
            //    DisplayOrder = 3,
            //    DiscountPercent = 0
            //});
            //context.Categories.AddOrUpdate(x => x.Id, new Category
            //{
            //    Id = 2,
            //    Description = "توضیحات گروه پزشکی",
            //    KeyWords = "کلمات کلیدی گروه پزشکی",
            //    Name = "پزشکی",
            //    DisplayOrder = 2,
            //    DiscountPercent = 0
            //});

            //context.SaveChanges();
            base.Seed(context);
        }
    }
}
