using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class Brand:BaseEntity
    {
        public Brand()
        {
            Products=new List<Product>();
        }
        [Display(Name = "Order", ResourceType = typeof(Resources.Models.Brand))]
        public int Order { get; set; }

        [Display(Name = "Title", ResourceType = typeof(Resources.Models.Brand))]
        public string Title { get; set; }

        [Display(Name = "ImageUrl", ResourceType = typeof(Resources.Models.Brand))]
        public string ImageUrl { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}