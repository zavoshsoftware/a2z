using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class ProductDetailViewModel:BaseViewModel
    {
        public Product Product { get; set; }
        public List<Product_Color> Colors { get; set; }
        public List<Categories> Categories { get; set; }
        public List<Product> Related { get; set; }
        public List<ProductImage> Images { get; set; }
        public List<ProductComment> Comments { get; set; }
    }
    
}