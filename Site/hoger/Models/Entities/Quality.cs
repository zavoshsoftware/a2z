using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class Quality : BaseEntity
    {
        public Quality()
        {
            Products = new List<Product>();
        }
        [Display(Name = "اولویت")]
        public int Order { get; set; }
        [Display(Name = "کیفیت")]
        public string Title { get; set; }
        [Display(Name = "تصویر")]
        public string ImageUrl { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}