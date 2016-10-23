using DomainClasses.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainClasses.Entities
{
    public class Product
    {
        public virtual Int64 Id { get; set; }
        public virtual DateTime AddedDate { get; set; }
        public virtual String Name { get; set; }
        public virtual String MetaDescription { get; set; }
        public virtual String Description { get; set; }
        public virtual String MetaKeywords { get; set; }
        public virtual String PrincipleImagePath { get; set; }
        public virtual bool IsFreeShipping { get; set; }
        public virtual Decimal Stock { get; set; }
        public virtual Decimal Reserve { get; set; }
        public virtual Decimal NotificationStockMinimum { get; set; }
        public virtual Decimal SellCount { get; set; }
        public virtual Int32 ViewCount { get; set; }
        public virtual Int64 Price { get; set; }
        public virtual Int64 PriceAfterDiscount { get; set; }
        public virtual Boolean Deleted { get; set; }
        public virtual Boolean ComingSoon { get; set; }
        public virtual Rate Rate { get; set; }
        public virtual Decimal Ratio { get; set; }
        public virtual Boolean ApplyCategoryDiscount { get; set; }
        public virtual Category Category { get; set; }
        public virtual Int64 CategoryId { get; set; }
        public virtual Int32 DiscountPercent { get; set; }
        public virtual ProductType Type { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
        public virtual Byte[] RowVersion { get; set; }

        [InverseProperty("ProductsFavorite")]
        public virtual ICollection<User> UsersFavorite { get; set; }
        public virtual ICollection<Value> Values { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ProductImage> Images { get; set; }

        public virtual ICollection<User> LikedUser { get; set; }
        public virtual Boolean HomePage { get; set; }
        public virtual Boolean SpecialSell { get; set; }

    }
}