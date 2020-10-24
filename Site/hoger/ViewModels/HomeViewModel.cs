using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class HomeViewModel:BaseViewModel
    {
        public List<Product> RecentProducts { get; set; }
        public List<Product> UpSellerProducts { get; set; }
        public List<Product> VipProducts { get; set; }
        public List<ProductGroup> ProductGroupCategory { get; set; }

        public List<Product> BestSeller { get; set; }

        public List<Blog> Blogs { get; set; }
        public List<Text> MiddleSliderText { get; set; }

        public List<SliderMenu> SliderMenu { get; set; }
    }

    public class SliderMenu
    {
        public ProductGroup Parent { get; set; }
        public List<ProductGroup> Child { get; set; }
    }
}