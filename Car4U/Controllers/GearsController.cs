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
    public class GearsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Gears
        public ActionResult Index()
        {
            return View(db.Gears.ToList());
        }

        // GET: Gears/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gear gear = db.Gears.Find(id);
            if (gear == null)
            {
                return HttpNotFound();
            }
            return View(gear);
        }

        // GET: Gears/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Gears/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Description")] Gear gear)
        {
            if (ModelState.IsValid)
            {
                db.Gears.Add(gear);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gear);
        }

        // GET: Gears/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gear gear = db.Gears.Find(id);
            if (gear == null)
            {
                return HttpNotFound();
            }
            return View(gear);
        }

        // POST: Gears/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description")] Gear gear)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gear).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gear);
        }

        // GET: Gears/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gear gear = db.Gears.Find(id);
            if (gear == null)
            {
                return HttpNotFound();
            }
            return View(gear);
        }

        // POST: Gears/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gear gear = db.Gears.Find(id);
            db.Gears.Remove(gear);
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
