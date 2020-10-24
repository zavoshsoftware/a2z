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
    public class Blog : BaseEntity
    {
        public Blog()
        {
            BlogComments=new List<BlogComment>();
        }
        [Display(Name = "Title", ResourceType = typeof(Resources.Models.Blog))]
        public string Title { get; set; }

        [Display(Name = "Summery", ResourceType = typeof(Resources.Models.Blog))]
        [DataType(DataType.MultilineText)]
        public string Summery { get; set; }

        [Display(Name = "ImageUrl", ResourceType = typeof(Resources.Models.Blog))]
        public string ImageUrl { get; set; }

        [Display(Name = "UrlParam", ResourceType = typeof(Resources.Models.Blog))]
        public string UrlParam { get; set; }

        [Display(Name = "Visit", ResourceType = typeof(Resources.Models.Blog))]
        public int Visit { get; set; }
        [Display(Name = "متن")]
        [DataType(DataType.Html)]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        public string Body { get; set; }

        [Display(Name = "BlogGroupId", ResourceType = typeof(Resources.Models.Blog))]
        public Guid BlogGroupId { get; set; }
        public virtual BlogGroup BlogGroup { get; set; }

        public virtual ICollection<BlogComment> BlogComments { get; set; }
        internal class Configuration : EntityTypeConfiguration<Blog>
        {
            public Configuration()
            {
                HasRequired(p => p.BlogGroup)
                    .WithMany(j => j.Blogs)
                    .HasForeignKey(p => p.BlogGroupId);
            }
        }
    }
}