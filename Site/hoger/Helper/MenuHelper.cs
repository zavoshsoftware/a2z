using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using ViewModels;

namespace Helper
{
    public class MenuHelper
    {
        private DatabaseContext db = new DatabaseContext();
        public BaseInfo ReturnMenu()
        {
            BaseInfo viewModel = new BaseInfo();
            viewModel.ProductGroups = ReturnProductGroup();
            viewModel.Footer = ReturnFooter();
            return viewModel;
        }

        public List<ProductGroup> ReturnProductGroup()
        {
            List<ProductGroup> productGroups = db.ProductGroups
                .Where(current => current.IsDeleted == false && current.IsActive == true
                && current.ParentId != null).OrderBy(current=>current.Order).ToList();

            return productGroups;
        }
        public string ReturnFooter()
        {
            string body = db.TextTypes.Where(current => current.IsDeleted == false && current.IsActive == true && current.UrlParam == "footer").FirstOrDefault().Description;

            return body;
        }
    }
}