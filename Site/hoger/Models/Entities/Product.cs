using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Models
{
    public class Product : BaseEntity
    {
        public Product()
        {
            OrderDetails = new List<OrderDetail>();
            ProductImages = new List<ProductImage>();
            Rates = new List<Rate>();
            ProductComments = new List<ProductComment>();
            ProductColors = new List<Product_Color>();
            Products = new List<Product>();
        }
        [Display(Name = "Order", ResourceType = typeof(Resources.Models.Product))]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public int Order { get; set; }

        [Display(Name = "Code", ResourceType = typeof(Resources.Models.Product))]
        [StringLength(15, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        public string Code { get; set; }

        [Display(Name = "UrlParam", ResourceType = typeof(Resources.Models.Product))]
        public string UrlParam { get; set; }

        [Display(Name = "Title", ResourceType = typeof(Resources.Models.Product))]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [StringLength(256, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        public string Title { get; set; }

        [Display(Name = "PageTitle", ResourceType = typeof(Resources.Models.Product))]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [StringLength(500, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        public string PageTitle { get; set; }

        [Display(Name = "PageDescription", ResourceType = typeof(Resources.Models.Product))]
        [StringLength(1000, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        [DataType(DataType.MultilineText)]
        public string PageDescription { get; set; }

        [Display(Name = "ImageUrl", ResourceType = typeof(Resources.Models.Product))]
        [StringLength(500, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        public string ImageUrl { get; set; }

        [Display(Name = "Summery", ResourceType = typeof(Resources.Models.Product))]
        [DataType(DataType.MultilineText)]
        public string Summery { get; set; }

        [Display(Name = "Body", ResourceType = typeof(Resources.Models.Product))]
        [DataType(DataType.Html)]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        public string Body { get; set; }

        [Display(Name = "Amount", ResourceType = typeof(Resources.Models.Product))]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public decimal Amount { get; set; }

        [NotMapped]
        public string AmountStr
        {
            get { return Amount.ToString("n0"); }
        }

        [Display(Name = "DiscountAmount", ResourceType = typeof(Resources.Models.Product))]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public decimal DiscountAmount { get; set; }
        [Display(Name = "IsInPromotion", ResourceType = typeof(Resources.Models.Product))]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public bool IsInPromotion { get; set; }

        [Display(Name = "IsInHome", ResourceType = typeof(Resources.Models.Product))]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public bool IsInHome { get; set; }
        [Display(Name = "Title", ResourceType = typeof(Resources.Models.ProductGroup))]
        public Guid ProductGroupId { get; set; }
        public virtual ProductGroup ProductGroup { get; set; }

        [Display(Name = "BrandId", ResourceType = typeof(Resources.Models.Product))]
        public Guid BrandId { get; set; }
        public virtual Brand Brand { get; set; }


        //[Display(Name = "ColorId", ResourceType = typeof(Resources.Models.Product))]
        //public Guid ColorId { get; set; }
        //public virtual Color Color { get; set; }


        [Display(Name = "QualityId", ResourceType = typeof(Resources.Models.Product))]
        public Guid QualityId { get; set; }
        public virtual Quality Quality { get; set; }

        [Display(Name = "SeasonId", ResourceType = typeof(Resources.Models.Product))]
        public Guid SeasonId { get; set; }
        public virtual Season Season { get; set; }



        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Product_Color> ProductColors { get; set; }

        [Display(Name = "موجودی")]
        public int Stock { get; set; }

        [Display(Name = "موجودی اولیه")]
        public int SeedStock { get; set; }

        public bool IsUpseller { get; set; }

        public virtual ICollection<Rate> Rates { get; set; }
        public virtual ICollection<ProductComment> ProductComments { get; set; }
        public int WorsthRate { get; set; }
        public int BestRate { get; set; }
        public decimal AverageRate { get; set; }
        public int RateCount { get; set; }

        [Display(Name = "IsVip", ResourceType = typeof(Resources.Models.Product))]
        public bool IsVip { get; set; }

        [Display(Name = "IsHotSell", ResourceType = typeof(Resources.Models.Product))]
        public bool IsHotSell { get; set; }

       

        public bool HasExtra{get;set;}

        internal class configuration : EntityTypeConfiguration<Product>
        {
            public configuration()
            {
                HasRequired(p => p.ProductGroup).WithMany(t => t.Products).HasForeignKey(p => p.ProductGroupId);
                HasRequired(p => p.Brand).WithMany(t => t.Products).HasForeignKey(p => p.BrandId);
                
                HasRequired(p => p.Quality).WithMany(t => t.Products).HasForeignKey(p => p.QualityId);
                HasRequired(p => p.Season).WithMany(t => t.Products).HasForeignKey(p => p.SeasonId);
               

            }
        }
    }
}