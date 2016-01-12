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
    public class ExtraModelTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ExtraModelTypes
        public ActionResult Index()
        {
            return View(db.ExtraModelTypes.ToList());
        }

        // GET: ExtraModelTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraModelType extraModelType = db.ExtraModelTypes.Find(id);
            if (extraModelType == null)
            {
                return HttpNotFound();
            }
            return View(extraModelType);
        }

        // GET: ExtraModelTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExtraModelTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Description")] ExtraModelType extraModelType)
        {
            if (ModelState.IsValid)
            {
                db.ExtraModelTypes.Add(extraModelType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(extraModelType);
        }

        // GET: ExtraModelTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraModelType extraModelType = db.ExtraModelTypes.Find(id);
            if (extraModelType == null)
            {
                return HttpNotFound();
            }
            return View(extraModelType);
        }

        // POST: ExtraModelTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description")] ExtraModelType extraModelType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(extraModelType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(extraModelType);
        }

        // GET: ExtraModelTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraModelType extraModelType = db.ExtraModelTypes.Find(id);
            if (extraModelType == null)
            {
                return HttpNotFound();
            }
            return View(extraModelType);
        }

        // POST: ExtraModelTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExtraModelType extraModelType = db.ExtraModelTypes.Find(id);
            db.ExtraModelTypes.Remove(extraModelType);
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
