using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web.Security;
using ViewModels;
using System.Text.RegularExpressions;
using Models;
using Helper;
//using Helper;
namespace Controllers
{

    public class AccountController : Controller
    {
        private MenuHelper menu = new MenuHelper();
        private DatabaseContext db = new DatabaseContext();
        //MenuHelper menu = new MenuHelper();
        public ActionResult Login(string ReturnUrl = "")
        {
            ViewBag.Message = "";
            ViewBag.ReturnUrl = ReturnUrl;
            LoginViewModel loginViewModel = new LoginViewModel();
            //loginViewModel.Menu = menu.ReturnMenu();
            //loginViewModel.FooterLink = menu.GetFooterLink();
            //loginViewModel.Username = menu.ReturnUsername();
            loginViewModel.BaseInfo = menu.ReturnMenu();
            ViewBag.PageId = "product-detail";
            return View(loginViewModel);

        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User oUser = db.Users.Where(a => a.CellNum == model.Username && a.Password == model.Password).FirstOrDefault();

                if (oUser != null)
                {
                    Role role = db.Roles.Find(oUser.RoleId);

                    var ident = new ClaimsIdentity(
                      new[] { 
              // adding following 2 claim just for supporting default antiforgery provider
              new Claim(ClaimTypes.NameIdentifier, oUser.CellNum),
              new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),

              new Claim(ClaimTypes.Name,oUser.CellNum),

              // optionally you could add roles if any
              new Claim(ClaimTypes.Role, role.Name),

                      },
                      DefaultAuthenticationTypes.ApplicationCookie);

                    //HttpContext.GetOwinContext().Authentication.SignIn(
                    //   new AuthenticationProperties { IsPersistent = true }, ident);
                    //



                    HttpContext.GetOwinContext().Authentication.SignIn(
                       new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTimeOffset.Now.AddDays(1) }, ident);
                    return RedirectToLocal(returnUrl, role.Name); // auth succeed 
                }
                else
                {
                    // invalid username or password
                    TempData["WrongPass"] = "نام کاربری و یا کلمه عبور وارد شده صحیح نمی باشد.";
                }
            }
            // If we got this far, something failed, redisplay form
            LoginPageViewModel login = new LoginPageViewModel();
            login.login = model;
            LoginViewModel loginViewModel = new LoginViewModel();
            //loginViewModel.Menu = menu.ReturnMenu();
            //loginViewModel.FooterLink = menu.GetFooterLink();
            return View(loginViewModel);

        }

        private ActionResult RedirectToLocal(string returnUrl, string role)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            //else
            //{
            //    return RedirectToAction("Index", "Users");
            if (role.ToLower().Contains("admin"))
                return RedirectToAction("Index", "Users");
            else
                return Redirect("/");
            //}
        }
        public ActionResult LogOff()
        {
            if (User.Identity.IsAuthenticated)
            {
                HttpContext.GetOwinContext().Authentication.SignOut();
            }
            return Redirect("/");
        }
        
        //public ActionResult Register()
        //{
        //    RegisterViewModel register = new RegisterViewModel();
        //    register.Menu = menu.ReturnMenu();
        //    register.FooterLink = menu.GetFooterLink();
        //    return View(register);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[AllowAnonymous]
        //public ActionResult Register(User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        user.IsDeleted = false;
        //        user.CreationDate = DateTime.Now;
        //        user.Id = Guid.NewGuid();
        //        user.RoleId = new Guid("5812d0c0-264f-4a9b-96bb-42dfc70538e6");
        //        user.IsActive = true;
        //        user.Code = RandomCode();
        //        db.Users.Add(user);
        //        db.SaveChanges();


        //        Role role = db.Roles.Find(user.RoleId);

        //        var ident = new ClaimsIdentity(
        //          new[] { 
        //      // adding following 2 claim just for supporting default antiforgery provider
        //      new Claim(ClaimTypes.NameIdentifier, user.CellNum),
        //      new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),

        //      new Claim(ClaimTypes.Name,user.CellNum),

        //      // optionally you could add roles if any
        //      new Claim(ClaimTypes.Role, role.Name),

        //          },
        //          DefaultAuthenticationTypes.ApplicationCookie);

        //        //HttpContext.GetOwinContext().Authentication.SignIn(
        //        //   new AuthenticationProperties { IsPersistent = true }, ident);
        //        //



        //        HttpContext.GetOwinContext().Authentication.SignIn(
        //           new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTimeOffset.Now.AddDays(1) }, ident);
        //        return Redirect("/");
        //    }
        //    return View(user);
        //}
        public int RandomCode()
        {
            Random generator = new Random();
            String r = generator.Next(0, 100000).ToString("D5");
            return Convert.ToInt32(r);
        }
    }
}
