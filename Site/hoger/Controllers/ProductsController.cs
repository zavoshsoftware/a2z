using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;
using System.IO;
using ViewModels;
using Helper;

namespace hoger.Controllers
{
    public class ProductsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        private MenuHelper menu = new MenuHelper();
        [Authorize(Roles = "Administrator")]
        // GET: Products
        public ActionResult Index(Guid? id)
        {
            List<Product> products = new List<Product>();
             products = db.Products.Include(p => p.Brand).Where(p=>p.IsDeleted==false).OrderByDescending(p=>p.CreationDate).Where(p=>p.IsDeleted==false).OrderByDescending(p=>p.CreationDate).Include(p => p.ProductGroup).Where(p=>p.IsDeleted==false).OrderByDescending(p=>p.CreationDate).Include(p => p.Quality).Where(p=>p.IsDeleted==false).OrderByDescending(p=>p.CreationDate).Include(p => p.Season).Where(p=>p.IsDeleted==false).OrderByDescending(p=>p.CreationDate).Where(p=>p.IsDeleted==false).OrderByDescending(p=>p.CreationDate).ToList();
           return View(products);
        }

        // GET: Products/Details/5
        [Route("product/{param}")]
        public ActionResult Details(string param)
        {
            Product product = db.Products.Where(current => current.UrlParam == param).FirstOrDefault();
            ProductDetailViewModel viewModel = new ProductDetailViewModel();
            viewModel.Product = product;
            viewModel.Related = db.Products.Where(current => current.IsDeleted == false && current.IsActive == true && current.ProductGroupId == product.ProductGroupId && current.Id!=product.Id).ToList();
            viewModel.Categories = ReturnCategories();
            viewModel.Colors = db.ProductColors.Include(current => current.Color).Where(current => current.IsDeleted == false && current.IsActive == true && current.ProductId == product.Id).ToList();
            viewModel.Images = db.ProductImages.Where(current => current.IsDeleted == false && current.IsActive == true && current.ProductId == product.Id).ToList();
            viewModel.Comments= db.ProductComments.Where(current => current.IsDeleted == false && current.IsActive == true && current.ProductId == product.Id).ToList();
            viewModel.BaseInfo = menu.ReturnMenu();
            ViewBag.PageId = "product-detail";
            return View(viewModel);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(db.Brands.Where(current=>current.IsDeleted==false && current.IsActive==true), "Id", "Title");
            ViewBag.ColorId = new SelectList(db.Colors.Where(current => current.IsDeleted == false && current.IsActive == true), "Id", "Title");
            ViewBag.ProductGroupId = new SelectList(db.ProductGroups.Where(current => current.IsDeleted == false && current.IsActive == true && current.ParentId != null), "Id", "Title");
            ViewBag.QualityId = new SelectList(db.Qualities.Where(current => current.IsDeleted == false && current.IsActive == true), "Id", "Title");
            ViewBag.SeasonId = new SelectList(db.Seasons.Where(current => current.IsDeleted == false && current.IsActive == true), "Id", "Title");
            //ViewBag.SizeId = new SelectList(db.Sizes.Where(current => current.IsDeleted == false && current.IsActive == true), "Id", "Title");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product,HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                string newFilenameUrl = string.Empty;
                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/product/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    product.ImageUrl = newFilenameUrl;
                }
                #endregion
                product.IsDeleted=false;
				product.CreationDate= DateTime.Now; 
                product.Id = Guid.NewGuid();
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrandId = new SelectList(db.Brands.Where(current => current.IsDeleted == false && current.IsActive == true), "Id", "Title", product.BrandId);
            //ViewBag.ColorId = new SelectList(db.Colors.Where(current => current.IsDeleted == false && current.IsActive == true), "Id", "Title", product.ColorId);
            ViewBag.ProductGroupId = new SelectList(db.ProductGroups.Where(current => current.IsDeleted == false && current.IsActive == true && current.ParentId != null), "Id", "Title", product.ProductGroupId);
            ViewBag.QualityId = new SelectList(db.Qualities.Where(current => current.IsDeleted == false && current.IsActive == true), "Id", "Title", product.QualityId);
            ViewBag.SeasonId = new SelectList(db.Seasons.Where(current => current.IsDeleted == false && current.IsActive == true), "Id", "Title", product.SeasonId);
            //ViewBag.SizeId = new SelectList(db.Sizes.Where(current => current.IsDeleted == false && current.IsActive == true), "Id", "Title", product.SizeId);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(db.Brands.Where(current => current.IsDeleted == false && current.IsActive == true), "Id", "Title", product.BrandId);
            //ViewBag.ColorId = new SelectList(db.Colors.Where(current => current.IsDeleted == false && current.IsActive == true), "Id", "Title", product.ColorId);
            ViewBag.ProductGroupId = new SelectList(db.ProductGroups.Where(current => current.IsDeleted == false && current.IsActive == true && current.ParentId != null), "Id", "Title", product.ProductGroupId);
            ViewBag.QualityId = new SelectList(db.Qualities.Where(current => current.IsDeleted == false && current.IsActive == true), "Id", "Title", product.QualityId);
            ViewBag.SeasonId = new SelectList(db.Seasons.Where(current => current.IsDeleted == false && current.IsActive == true), "Id", "Title", product.SeasonId);
            //ViewBag.SizeId = new SelectList(db.Sizes.Where(current => current.IsDeleted == false && current.IsActive == true), "Id", "Title", product.SizeId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product,HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                string newFilenameUrl = string.Empty;
                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/product/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    product.ImageUrl = newFilenameUrl;
                }
                #endregion
                product.IsDeleted=false;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(db.Brands.Where(current => current.IsDeleted == false && current.IsActive == true), "Id", "Title", product.BrandId);
            //ViewBag.ColorId = new SelectList(db.Colors.Where(current => current.IsDeleted == false && current.IsActive == true), "Id", "Title", product.ColorId);
            ViewBag.ProductGroupId = new SelectList(db.ProductGroups.Where(current => current.IsDeleted == false && current.IsActive == true && current.ParentId != null), "Id", "Title", product.ProductGroupId);
            ViewBag.QualityId = new SelectList(db.Qualities.Where(current => current.IsDeleted == false && current.IsActive == true), "Id", "Title", product.QualityId);
            ViewBag.SeasonId = new SelectList(db.Seasons.Where(current => current.IsDeleted == false && current.IsActive == true), "Id", "Title", product.SeasonId);
            //ViewBag.SizeId = new SelectList(db.Sizes.Where(current => current.IsDeleted == false && current.IsActive == true), "Id", "Title", product.SizeId);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Product product = db.Products.Find(id);
			product.IsDeleted=true;
			product.DeletionDate=DateTime.Now;
 
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [AllowAnonymous]
        [Route("product/list/{param}")]
        public ActionResult List(string param)
        {
            ProductListViewModel viewModel = new ProductListViewModel();
            viewModel.ProductGroup = db.ProductGroups.Where(current => current.UrlParam == param).FirstOrDefault();
            viewModel.Categories = ReturnCategories();
            viewModel.products = db.Products.Where(current => current.IsDeleted == false && current.IsActive == true && current.ProductGroup.UrlParam == param).ToList();
            viewModel.BaseInfo = menu.ReturnMenu();
            ViewBag.PageId = "product-sidebar-left";
            return View(viewModel);
        }

        public List<Categories> ReturnCategories()
        {
            List<ProductGroup> parent = db.ProductGroups.Where(current => current.IsDeleted == false && current.IsActive == true && current.ParentId==null).OrderBy(current=>current.Order).ToList();

            List<Categories> categories = new List<Categories>();
            foreach (ProductGroup group in parent)
            {
                categories.Add(new Categories { Parent = group, Child = db.ProductGroups.Where(current => current.IsDeleted == false && current.IsActive == true && current.ParentId==group.Id).OrderBy(current => current.Order).ToList() });
            }

            return categories;
        }
    }
}
