using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Models
{
    public class Product_Color : BaseEntity
    {
       
        public Guid ColorId { get; set; }
        public Color Color { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        [Display(Name = "Amount", ResourceType = typeof(Resources.Models.Product))]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public decimal Amount { get; set; }

        internal class configuration : EntityTypeConfiguration<Product_Color>
        {
            public configuration()
            {
                HasRequired(p => p.Product).WithMany(t => t.ProductColors).HasForeignKey(p => p.ProductId);
                HasRequired(p => p.Color).WithMany(t => t.ProductColors).HasForeignKey(p => p.ColorId);
            }
        }
    }
}