﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainClasses.Entities
{
    public class ProductImage
    {
        public virtual Int64 Id { get; set; }
        public virtual Product Product { get; set; }
        [Display(Name ="تصویر ")]
        public virtual String Path { get; set; }
        [ForeignKey("Product")]
        public virtual Int64 ProductId { get; set; }
    }
}