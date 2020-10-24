using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class Color : BaseEntity
    {
        public Color()
        {
            ProductColors = new List<Product_Color>();
        }
        [Display(Name = "Order", ResourceType = typeof(Resources.Models.Color))]
        public int Order { get; set; }
        [Display(Name = "Title", ResourceType = typeof(Resources.Models.Color))]
        public string Title { get; set; }
        [Display(Name = "ImageUrl", ResourceType = typeof(Resources.Models.Color))]
        public string ImageUrl { get; set; }
        public virtual ICollection<Product_Color> ProductColors { get; set; }

    }
}