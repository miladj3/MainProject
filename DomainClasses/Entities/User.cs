using DomainClasses.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainClasses.Entities
{
    public class User
    {
        public virtual Int64 id { get; set; }
        public virtual String FirstName { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual String LastName { get; set; }
        public virtual String AvatarPath { get; set; }
        public virtual DateTime? BirthDay { get; set; }

        [Index(IsUnique =true)]
        public virtual String UserName { get; set; }
        public virtual String Password { get; set; }
        public virtual String Email { get; set; }

        [InverseProperty("UsersFavorite")]
        public virtual ICollection<Product> ProductsFavorite { get; set; }
        public virtual UserRegisterType RegisterType { get; set; }
        [Index(IsUnique =true)]
        public virtual String PhoneNumber { get; set; }
        public virtual Address Address { get; set; }
        public virtual String IP { get; set; }
        public virtual Boolean IsBaned { get; set; }
        public virtual DateTime RegisterDate { get; set; }
        public virtual DateTime? BanedDate { get; set; }
        public virtual DateTime? LastLoginDate { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Product> LikedProducts { get; set; }
        public virtual ICollection<Comment> LikedComments { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual Byte[] RowVersion { get; set; }
    }
}