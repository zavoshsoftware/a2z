using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class BaseViewModel
    {
       public BaseInfo BaseInfo { get; set; }
    }
    public class BaseInfo
    {
        public List<ProductGroup> ProductGroups { get; set; }
        public string Footer { get; set; }
    }
}