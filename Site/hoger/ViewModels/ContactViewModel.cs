using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class ContactViewModel:BaseViewModel
    {
       public ContactInfo ContactInfo { get; set; }
    }
    public class ContactInfo
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Telegram { get; set; }
        public string Phone { get; set; }
    }
}