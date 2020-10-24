using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;

namespace hoger.Controllers
{
    public class Product_ColorController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Product_Color
        public ActionResult Index(Guid id)
        {
            var productColors = db.ProductColors.Include(p => p.Color).Where(p=>p.IsDeleted==false && p.ProductId==id).OrderByDescending(p=>p.CreationDate).Include(p => p.Product).Where(p=>p.IsDeleted==false).OrderByDescending(p=>p.CreationDate);
            return View(productColors.ToList());
        }

        // GET: Product_Color/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Color product_Color = db.ProductColors.Find(id);
            if (product_Color == null)
            {
                return HttpNotFound();
            }
            return View(product_Color);
        }

        // GET: Product_Color/Create
        public ActionResult Create(Guid id)
        {
            ViewBag.ColorId = new SelectList(db.Colors, "Id", "Title");
            ViewBag.ProductId = id;
            return View();
        }

        // POST: Product_Color/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product_Color product_Color,Guid id)
        {
            if (ModelState.IsValid)
            {
				product_Color.IsDeleted=false;
				product_Color.CreationDate= DateTime.Now; 
                product_Color.Id = Guid.NewGuid();
                product_Color.ProductId = id;
                db.ProductColors.Add(product_Color);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = product_Color.ProductId });
            }

            ViewBag.ColorId = new SelectList(db.Colors, "Id", "Title", product_Color.ColorId);
            ViewBag.ProductId = id;
            return View(product_Color);
        }

        // GET: Product_Color/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Color product_Color = db.ProductColors.Find(id);
            if (product_Color == null)
            {
                return HttpNotFound();
            }
            ViewBag.ColorId = new SelectList(db.Colors, "Id", "Title", product_Color.ColorId);
            ViewBag.ProductId = product_Color.ProductId;
            return View(product_Color);
        }

        // POST: Product_Color/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product_Color product_Color)
        {
            if (ModelState.IsValid)
            {
				product_Color.IsDeleted=false;
                db.Entry(product_Color).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = product_Color.ProductId });
            }
            ViewBag.ColorId = new SelectList(db.Colors, "Id", "Title", product_Color.ColorId);
            ViewBag.ProductId = product_Color.ProductId;
            return View(product_Color);
        }

        // GET: Product_Color/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Color product_Color = db.ProductColors.Find(id);
            if (product_Color == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = product_Color.ProductId;
            return View(product_Color);
        }

        // POST: Product_Color/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Product_Color product_Color = db.ProductColors.Find(id);
			product_Color.IsDeleted=true;
			product_Color.DeletionDate=DateTime.Now;
 
            db.SaveChanges();
            return RedirectToAction("Index",new { id=product_Color.ProductId});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
