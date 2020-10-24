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

namespace hoger.Controllers
{
    public class QualitiesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        [Authorize(Roles = "Administrator")]
        // GET: Qualities
        public ActionResult Index()
        {
            return View(db.Qualities.Where(a=>a.IsDeleted==false).OrderByDescending(a=>a.CreationDate).ToList());
        }

        // GET: Qualities/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quality quality = db.Qualities.Find(id);
            if (quality == null)
            {
                return HttpNotFound();
            }
            return View(quality);
        }

        // GET: Qualities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Qualities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Quality quality,HttpPostedFileBase fileUpload)
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

                    newFilenameUrl = "/Uploads/quality/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    quality.ImageUrl = newFilenameUrl;
                }
                #endregion
                quality.IsDeleted=false;
				quality.CreationDate= DateTime.Now; 
                quality.Id = Guid.NewGuid();
                db.Qualities.Add(quality);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quality);
        }

        // GET: Qualities/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quality quality = db.Qualities.Find(id);
            if (quality == null)
            {
                return HttpNotFound();
            }
            return View(quality);
        }

        // POST: Qualities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Quality quality,HttpPostedFileBase fileUpload)
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

                    newFilenameUrl = "/Uploads/quality/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    quality.ImageUrl = newFilenameUrl;
                }
                #endregion
                quality.IsDeleted=false;
                db.Entry(quality).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quality);
        }

        // GET: Qualities/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quality quality = db.Qualities.Find(id);
            if (quality == null)
            {
                return HttpNotFound();
            }
            return View(quality);
        }

        // POST: Qualities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Quality quality = db.Qualities.Find(id);
			quality.IsDeleted=true;
			quality.DeletionDate=DateTime.Now;
 
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
    }
}
