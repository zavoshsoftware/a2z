using Helper;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using ViewModels;

namespace hoger.Controllers
{
    public class CartController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        private MenuHelper menu = new MenuHelper();
        // GET: Cart
        [Route("cart")]
        public ActionResult Index(string qty, string code, string color)
        {
            SetCookie(code, qty, color);
            return RedirectToAction("Basket");
        }
        public void SetCookie(string code, string quantity, string color)
        {
            string cookievalue = null;

            if (Request.Cookies["basket"] != null)
            {
                bool changeCurrentItem = false;

                cookievalue = Request.Cookies["basket"].Value;

                string[] coockieItems = cookievalue.Split('/');

                for (int i = 0; i < coockieItems.Length - 1; i++)
                {
                    string[] coockieItem = coockieItems[i].Split('^');

                    if (coockieItem[2] == color)
                    {
                        coockieItem[1] = (Convert.ToInt32(coockieItem[1]) + Convert.ToInt32(quantity)).ToString();
                        changeCurrentItem = true;
                        coockieItems[i] = coockieItem[0] + "^" + coockieItem[1] + "^" +coockieItem[2];
                        break;
                    }
                }

                if (changeCurrentItem)
                {
                    cookievalue = null;
                    for (int i = 0; i < coockieItems.Length - 1; i++)
                    {
                        cookievalue = cookievalue + coockieItems[i] + "/";
                    }

                }
                else
                    cookievalue = cookievalue + code + "^" + quantity + "^" + color+"/";

            }
            else
                cookievalue = code + "^" + quantity + "^" + color + "/";

            HttpContext.Response.Cookies.Set(new HttpCookie("basket")
            {
                Name = "basket",
                Value = cookievalue,
                Expires = DateTime.Now.AddDays(1)
            });
        }

        public string[] GetCookie()
        {
            if (Request.Cookies["basket"] != null)
            {
                string cookievalue = Request.Cookies["basket"].Value;

                string[] basketItems = cookievalue.Split('/');

                return basketItems;
            }

            return null;
        }

        public decimal GetSubtotal(List<ProductInCart> orderDetails)
        {
            decimal subTotal = 0;

            foreach (ProductInCart orderDetail in orderDetails)
            {
                decimal amount = orderDetail.Product.Amount;

                subTotal = subTotal + (amount * orderDetail.Quantity);
            }

            return subTotal;
        }

        public decimal GetShippment(List<ProductInCart> orderDetails)
        {
            decimal subTotal = GetSubtotal(orderDetails);

            decimal shippingLimit =
                Convert.ToDecimal(
                    WebConfigurationManager.AppSettings["ShippingLimit"]);

            if (subTotal >= shippingLimit)
                return 0;

            return Convert.ToDecimal(
                WebConfigurationManager.AppSettings["ShippingAmount"]);
        }

        public List<ProductInCart> GetProductInBasketByCoockie()
        {
            List<ProductInCart> productInCarts = new List<ProductInCart>();

            string[] basketItems = GetCookie();

            for (int i = 0; i < basketItems.Length - 1; i++)
            {
                string[] productItem = basketItems[i].Split('^');

                string productCode = productItem[0];

                Guid productColorId = new Guid(productItem[2]);

                Product product =
                    db.Products.FirstOrDefault(current => current.IsDeleted == false && current.Code == productCode);

                Product_Color product_Color = db.ProductColors.Where(current => current.Id == productColorId).FirstOrDefault();

                productInCarts.Add(new ProductInCart()
                {
                    Product = product,
                    Quantity = Convert.ToInt32(productItem[1]),
                    Color= db.Colors.Where(current=>current.Id==product_Color.ColorId).FirstOrDefault().Title
                });
            }

            return productInCarts;
        }

        [Route("Basket")]
        public ActionResult Basket()
        {
            CartViewModel cart = new CartViewModel();

            cart.BaseInfo = menu.ReturnMenu();

            List<ProductInCart> productInCarts = GetProductInBasketByCoockie();

            cart.Products = productInCarts;

            decimal subTotal = GetSubtotal(productInCarts);

            cart.SubTotal = subTotal.ToString("n0") + " تومان";

            decimal shippment = GetShippment(productInCarts);
            if (shippment != 0)
            {
                cart.ShippingAmount = shippment.ToString("n0") + " تومان";
            }
            else
            {
                cart.ShippingAmount = "رایگان";
            }
            decimal discountAmount = GetDiscount();

            cart.DiscountAmount = discountAmount.ToString("n0") + " تومان";

            cart.Total = (subTotal + shippment - discountAmount).ToString("n0");
            //ViewBag.PageId = "product-cart";
            ViewBag.BodyClass = "product-cart checkout-cart";
            return View(cart);
        }

        public decimal GetDiscount()
        {
            if (Request.Cookies["discount"] != null)
            {
                try
                {
                    string cookievalue = Request.Cookies["discount"].Value;

                    string[] basketItems = cookievalue.Split('/');
                    return Convert.ToDecimal(basketItems[0]);
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            return 0;
        }

        [Route("Basket/remove/{code}")]
        public ActionResult RemoveFromBasket(string code)
        {
            string[] coockieItems = GetCookie();


            for (int i = 0; i < coockieItems.Length - 1; i++)
            {
                string[] coockieItem = coockieItems[i].Split('^');

                if (coockieItem[0] == code)
                {
                    string removeArray = coockieItem[0] + "^" + coockieItem[1] + "^" + coockieItem[2];
                    coockieItems = coockieItems.Where(current => current != removeArray).ToArray();
                    break;
                }
            }

            string cookievalue = null;
            for (int i = 0; i < coockieItems.Length - 1; i++)
            {
                cookievalue = cookievalue + coockieItems[i] + "/";
            }

            HttpContext.Response.Cookies.Set(new HttpCookie("basket")
            {
                Name = "basket",
                Value = cookievalue,
                Expires = DateTime.Now.AddDays(1)
            });

            return RedirectToAction("Basket");
        }
    }
}