using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Models
{
    public class ProductImage : BaseEntity
    {
        public string ImageUrl { get; set; }
        public string ThumbImageUrl { get; set; }
        public string Alt { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }

        internal class configuration : EntityTypeConfiguration<ProductImage>
        {
            public configuration()
            {
                HasRequired(p => p.Product).WithMany(t => t.ProductImages).HasForeignKey(p => p.ProductId);
            }
        }

    }
}