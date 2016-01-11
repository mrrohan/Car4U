using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Car4U.DAL;
using Car4U.Models;

namespace Car4U.Controllers
{
     [Authorize(Roles = "Admin, Employee")]
    public class ExtraItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ExtraItems
        public ActionResult Index()
        {
            var extraItems = db.ExtraItems.Include(e => e.ExtraModel);
            return View(extraItems.ToList());
        }

        // GET: ExtraItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraItem extraItem = db.ExtraItems.Find(id);
            if (extraItem == null)
            {
                return HttpNotFound();
            }
            return View(extraItem);
        }

        // GET: ExtraItems/Create
        public ActionResult Create()
        {
            ViewBag.ExtraModelID = new SelectList(db.ExtraModels, "ID", "Model");
            return View();
        }

        // POST: ExtraItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ExtraModelID")] ExtraItem extraItem)
        {
            if (ModelState.IsValid)
            {
                db.ExtraItems.Add(extraItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExtraModelID = new SelectList(db.ExtraModels, "ID", "Model", extraItem.ExtraModelID);
            return View(extraItem);
        }

        // GET: ExtraItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraItem extraItem = db.ExtraItems.Find(id);
            if (extraItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExtraModelID = new SelectList(db.ExtraModels, "ID", "Model", extraItem.ExtraModelID);
            return View(extraItem);
        }

        // POST: ExtraItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ExtraModelID")] ExtraItem extraItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(extraItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExtraModelID = new SelectList(db.ExtraModels, "ID", "Model", extraItem.ExtraModelID);
            return View(extraItem);
        }

        // GET: ExtraItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraItem extraItem = db.ExtraItems.Find(id);
            if (extraItem == null)
            {
                return HttpNotFound();
            }
            return View(extraItem);
        }

        // POST: ExtraItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExtraItem extraItem = db.ExtraItems.Find(id);
            db.ExtraItems.Remove(extraItem);
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
