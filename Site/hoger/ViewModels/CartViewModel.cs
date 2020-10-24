using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class CartViewModel:BaseViewModel
    {
        public List<ProductInCart> Products { get; set; }
        public string SubTotal { get; set; }
        public string ShippingAmount { get; set; }
        public string DiscountAmount { get; set; }
        public string Total { get; set; }


    }
}