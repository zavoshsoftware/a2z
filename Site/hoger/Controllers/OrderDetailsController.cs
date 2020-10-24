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
    public class OrderDetailsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        [Authorize(Roles = "Administrator")]
        // GET: OrderDetails
        public ActionResult Index(Guid id)
        {
            var orderDetails = db.OrderDetails.Include(o => o.Order).Where(o=>o.IsDeleted==false && o.OrderId==id).OrderByDescending(o=>o.CreationDate).Include(o => o.Product).Where(o=>o.IsDeleted==false).OrderByDescending(o=>o.CreationDate);
            return View(orderDetails.ToList());
        }

        // GET: OrderDetails/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // GET: OrderDetails/Create
        public ActionResult Create(Guid id)
        {
            ViewBag.OrderId = new SelectList(db.Orders.Where(current=>current.IsDeleted==false && current.IsActive==true), "Id", "Address");
            ViewBag.ProductId = new SelectList(db.Products.Where(current => current.IsDeleted == false && current.IsActive == true), "Id", "Code");
            ViewBag.id = id;
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
				orderDetail.IsDeleted=false;
				orderDetail.CreationDate= DateTime.Now; 
                orderDetail.Id = Guid.NewGuid();
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                return RedirectToAction("Index",new { id=orderDetail.OrderId});
            }

            ViewBag.OrderId = new SelectList(db.Orders, "Id", "Address", orderDetail.OrderId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Code", orderDetail.ProductId);
            ViewBag.id = orderDetail.OrderId;
            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "Address", orderDetail.OrderId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Code", orderDetail.ProductId);
            ViewBag.id = orderDetail.OrderId;
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
				orderDetail.IsDeleted=false;
                db.Entry(orderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index",new { id=orderDetail.OrderId});
            }
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "Address", orderDetail.OrderId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Code", orderDetail.ProductId);
            ViewBag.id = orderDetail.OrderId;
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = orderDetail.OrderId;
            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            OrderDetail orderDetail = db.OrderDetails.Find(id);
			orderDetail.IsDeleted=true;
			orderDetail.DeletionDate=DateTime.Now;
 
            db.SaveChanges();
            return RedirectToAction("Index",new { id=orderDetail.OrderId});
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
