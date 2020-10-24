using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class BlogGroup : BaseEntity
    {
        [Display(Name = "Title", ResourceType = typeof(Resources.Models.BlogGroup))]
        public string Title { get; set; }
        [Display(Name = "Summery", ResourceType = typeof(Resources.Models.BlogGroup))]
        public string Summery { get; set; }
        [Display(Name = "ImageUrl", ResourceType = typeof(Resources.Models.BlogGroup))]
        public string ImageUrl { get; set; }
        [Display(Name = "UrlParam", ResourceType = typeof(Resources.Models.BlogGroup))]
        public string UrlParam { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }

    }
}