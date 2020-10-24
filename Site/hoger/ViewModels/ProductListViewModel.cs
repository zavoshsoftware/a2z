using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class ProductListViewModel:BaseViewModel
    {
        public ProductGroup ProductGroup { get; set; }
        public List<Categories> Categories { get; set; }
        public List<Product> products { get; set; }
    }

    public class Categories
    {
        public ProductGroup Parent { get; set; }
        public List<ProductGroup> Child { get; set; }
    }
}