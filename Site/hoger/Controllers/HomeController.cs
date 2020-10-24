using Helper;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace hoger.Controllers
{
    public class HomeController : Controller
    {
        private MenuHelper menu = new MenuHelper();
        private DatabaseContext db = new DatabaseContext();
        // GET: Home
        public ActionResult Index()
        {
            HomeViewModel viewModel = new HomeViewModel();
            viewModel.BaseInfo = menu.ReturnMenu();


            viewModel.RecentProducts = db.Products.Where(current => current.IsDeleted == false && current.IsActive == true).OrderByDescending(current => current.CreationDate).ToList();
            viewModel.VipProducts = db.Products.Where(current => current.IsDeleted == false && current.IsActive == true && current.IsVip==true).OrderBy(current => current.Order).ToList();
            viewModel.UpSellerProducts = db.Products.Where(current => current.IsDeleted == false && current.IsUpseller == true).OrderBy(current => current.Order).ToList();
            viewModel.ProductGroupCategory = db.ProductGroups.Where(current => current.IsDeleted == false && current.IsActive == true && current.ParentId != null).OrderBy(current => current.Order).ToList();
            viewModel.BestSeller = db.Products.Where(current => current.IsDeleted == false && current.IsUpseller == true).OrderBy(current => current.Order).ToList();
            viewModel.Blogs = db.Blogs.Where(current => current.IsDeleted == false && current.IsActive == true).OrderByDescending(current => current.CreationDate).ToList();
            viewModel.MiddleSliderText = db.Texts.Where(current => current.IsDeleted == false && current.IsActive == true && current.TextType.UrlParam == "middle-slider-text").ToList();
            viewModel.SliderMenu = ReturnSliderMenu();
            ViewBag.PageId = "home";
            return View(viewModel);
        }
        public ActionResult About()
        {
            AboutViewModel viewModel = new AboutViewModel();
            viewModel.BaseInfo = menu.ReturnMenu();
            viewModel.About = db.Texts.Where(current => current.TextType.UrlParam == "about").FirstOrDefault();
            ViewBag.PageId = "about-us";
            return View(viewModel);
        }
        public ActionResult Contact()
        {
            ContactViewModel viewModel = new ContactViewModel();
            viewModel.BaseInfo = menu.ReturnMenu();
            viewModel.ContactInfo = ReturnContactInfo();
            ViewBag.PageId = "contact";
            return View(viewModel);
        }
        public List<SliderMenu> ReturnSliderMenu()
        {
            List<SliderMenu> sliderMenu = new List<SliderMenu>();

            List<ProductGroup> Parents = db.ProductGroups.Where(current => current.IsDeleted == false && current.IsActive == true && current.ParentId == null).ToList();
            foreach(ProductGroup parent in Parents)
            {
                sliderMenu.Add(new SliderMenu
                {
                    Parent = parent,
                    Child = db.ProductGroups.Where(current => current.IsDeleted == false && current.IsActive == true && current.ParentId == parent.Id).ToList()
                });
            }
            return sliderMenu;
        }

        public ContactInfo ReturnContactInfo()
        {
            ContactInfo info = new ContactInfo();

            info.Address1 = db.Texts.Where(current => current.Summery == "address1").FirstOrDefault().Body;
            info.Address2 = db.Texts.Where(current => current.Summery == "address2").FirstOrDefault().Body;
            info.Telegram = db.Texts.Where(current => current.Summery == "telegram").FirstOrDefault().Body;
            info.Phone = db.Texts.Where(current => current.Summery == "phone").FirstOrDefault().Body;

            return info;
        }


    }
}