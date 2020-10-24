using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class ProductInCart
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public string Color { get; set; }
    }
}