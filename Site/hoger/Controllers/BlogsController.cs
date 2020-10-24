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
    public class BlogsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        private MenuHelper menu = new MenuHelper();
        [Authorize(Roles = "Administrator")]
        // GET: Blogs
        public ActionResult Index()
        {
            var blogs = db.Blogs.Include(b => b.BlogGroup).Where(b => b.IsDeleted == false).OrderByDescending(b => b.CreationDate);
            return View(blogs.ToList());
        }
        // GET: Blogs/Create
        public ActionResult Create()
        {
            ViewBag.BlogGroupId = new SelectList(db.BlogGroups.Where(current => current.IsDeleted == false), "Id", "Title");
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Blog blog, HttpPostedFileBase fileUpload)
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

                    newFilenameUrl = "/Uploads/blog/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    blog.ImageUrl = newFilenameUrl;
                }
                #endregion
                blog.IsDeleted = false;
                blog.CreationDate = DateTime.Now;
                blog.Id = Guid.NewGuid();
                db.Blogs.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BlogGroupId = new SelectList(db.BlogGroups.Where(current => current.IsDeleted == false), "Id", "Title", blog.BlogGroupId);
            return View(blog);
        }

        // GET: Blogs/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlogGroupId = new SelectList(db.BlogGroups, "Id", "Title", blog.BlogGroupId);
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Blog blog, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed

                string newFileNameUrl = blog.ImageUrl;

                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);

                    string newFileName = Guid.NewGuid().ToString().Replace("-", string.Empty) +
                         Path.GetExtension(filename);

                    newFileNameUrl = "/uploads/blog/" + newFileName;

                    string physicalFilename = Server.MapPath(newFileNameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    blog.ImageUrl = newFileNameUrl;
                }
                #endregion

                blog.IsDeleted = false;
                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BlogGroupId = new SelectList(db.BlogGroups, "Id", "Title", blog.BlogGroupId);
            return View(blog);
        }

        // GET: Blogs/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Blog blog = db.Blogs.Find(id);
            blog.IsDeleted = true;
            blog.DeletionDate = DateTime.Now;

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
        // GET: Blogs/Details/5
        [Route("blog/{param}")]
        public ActionResult Details(string param)
        {
            Blog blog = db.Blogs.Include(current => current.BlogGroup).Where(current => current.UrlParam == param).FirstOrDefault();

            BlogDetailViewModel viewModel = new BlogDetailViewModel();

            viewModel.BaseInfo = menu.ReturnMenu();
            viewModel.blog = blog;
            viewModel.Categories = ReturnCategories();
            viewModel.Recent = db.Blogs.Include(current => current.BlogComments).Where(current => current.IsDeleted == false && current.IsActive == true && current.Id != blog.Id).OrderByDescending(current => current.CreationDate).Take(3).ToList();
            viewModel.Related = db.Blogs.Where(current => current.IsDeleted == false && current.IsActive == true && current.BlogGroupId == blog.BlogGroupId && current.Id != blog.Id).OrderByDescending(current => current.CreationDate).Take(3).ToList();
            viewModel.Comments = db.BlogComments.Where(current => current.IsDeleted == false && current.IsActive == true && current.BlogId == blog.Id).ToList();
            ViewBag.PageId = "blog-detail";

            return View(viewModel);
        }

        [Route("blog/list/{param}")]
        public ActionResult List(string param)
        {
            BlogGroup blogGroup= db.BlogGroups.Where(current => current.UrlParam == param).FirstOrDefault();

            BlogListViewModel viewModel = new BlogListViewModel();

            viewModel.BaseInfo = menu.ReturnMenu();
            viewModel.BlogGroup = blogGroup;
            viewModel.Blogs = db.Blogs.Where(current => current.BlogGroupId == blogGroup.Id).OrderByDescending(current=>current.CreationDate).ToList();

            ViewBag.PageId = "blog-grid-full-width";

            return View(viewModel);
        }

        public List<BlogCategory> ReturnCategories()
        {
            List<BlogCategory> categories = new List<BlogCategory>();

            List<BlogGroup> blogGroups = db.BlogGroups.Where(current => current.IsActive == true && current.IsDeleted == false).ToList();

            foreach (BlogGroup group in blogGroups)
            {
                categories.Add(new BlogCategory
                {
                    BlogGroup = group,
                    Blogs = db.Blogs.Where(current => current.BlogGroupId == group.Id).ToList()
                });
            }

            return categories;
        }
}
}
