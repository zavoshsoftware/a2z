using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class Season : BaseEntity
    {
        public Season()
        {
            Products=new List<Product>();
        }
        [Display(Name = "اولویت")]
        public int Order { get; set; }
        [Display(Name = "فصل")]
        public string Title { get; set; }
        [Display(Name = "تصویر")]
        public string ImageUrl { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}