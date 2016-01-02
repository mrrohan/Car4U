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
    public class MomentDeliveriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MomentDeliveries
        public ActionResult Index()
        {
            return View(db.MomentDeliveries.ToList());
        }

        // GET: MomentDeliveries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MomentDelivery momentDelivery = db.MomentDeliveries.SingleOrDefault(u => u.ReservationID == id);
            if (momentDelivery == null)
            {
                return RedirectToAction("Create/" + id);
            }
            return View(momentDelivery);
        }

        // GET: MomentDeliveries/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MomentDelivery momentDelivery = db.MomentDeliveries.SingleOrDefault(u => u.ReservationID == id);
            if (momentDelivery != null)
            {
                return RedirectToAction("Details/" + id);
            }
            return View();
        }

        // POST: MomentDeliveries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Date,Observation,ReservationID")] MomentDelivery momentDelivery, int? id)
        {
            if (ModelState.IsValid)
            {
                    if (id != null)
                    {
                        momentDelivery.ReservationID = id ?? default(int);
                    }

                    momentDelivery.Date = db.Reservations.Find(momentDelivery.ID).DeliveryDate;
                    db.MomentDeliveries.Add(momentDelivery);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Reservations");
            }

            return View(momentDelivery);
        }

        // GET: MomentDeliveries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MomentDelivery momentDelivery = db.MomentDeliveries.Find(id);
            if (momentDelivery == null)
            {
                return HttpNotFound();
            }
            return View(momentDelivery);
        }

        // POST: MomentDeliveries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Date,Observation,ReservationID")] MomentDelivery momentDelivery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(momentDelivery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(momentDelivery);
        }

        // GET: MomentDeliveries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MomentDelivery momentDelivery = db.MomentDeliveries.Find(id);
            if (momentDelivery == null)
            {
                return HttpNotFound();
            }
            return View(momentDelivery);
        }

        // POST: MomentDeliveries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MomentDelivery momentDelivery = db.MomentDeliveries.Find(id);
            db.MomentDeliveries.Remove(momentDelivery);
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
