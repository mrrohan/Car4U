﻿using System;
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
    public class MomentReturnsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MomentReturns
        public ActionResult Index()
        {
            return View(db.MomentReturns.ToList());
        }

        // GET: MomentReturns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MomentReturn momentReturn = db.MomentReturns.SingleOrDefault(u => u.ReservationID == id);
            if (momentReturn == null)
            {
                return RedirectToAction("Create/" + id);
            }
            return View(momentReturn);
        }

        // GET: MomentReturns/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MomentReturn momentReturn = db.MomentReturns.SingleOrDefault(u => u.ReservationID == id);            

            if (momentReturn != null)
            {
                return RedirectToAction("Details/" + id);
            }
            return View();
        }

        // POST: MomentReturns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Date,Observation,ReservationID")] MomentReturn momentReturn, int? id)
        {
            if (ModelState.IsValid)
            {

                if (id != null)
                {
                     momentReturn.ReservationID = id ?? default(int);
                }

                Reservation reserv = db.Reservations.FirstOrDefault(r => r.ID == id);

                if (reserv != null)
                {
                    Car c = db.Cars.FirstOrDefault(ca => ca.ID == reserv.carID);
                    if (c != null)
                    {
                        CarStatus cs = db.CarStatus.FirstOrDefault(cars => cars.CarID == c.ID && cars.BeginDate == reserv.DeliveryDate && cars.FinishDate == reserv.ReturnDate);
                        cs.Outside = false;
                        if (cs.FinishDate > DateTime.Now)
                        {
                            cs.FinishDate = DateTime.Now;
                        }
                        db.Entry(cs).State = EntityState.Modified;
                        db.SaveChanges();
                    }


                    foreach (var item in reserv.ExtraItems)
                    {
                        item.InUse = false;
                        db.Entry(item).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    momentReturn.Date = DateTime.Now;
                }
                db.MomentReturns.Add(momentReturn);
                db.SaveChanges();
                return RedirectToAction("Index", "Reservations");
            }

            return View(momentReturn);
        }

        // GET: MomentReturns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MomentReturn momentReturn = db.MomentReturns.Find(id);
            if (momentReturn == null)
            {
                return HttpNotFound();
            }
            return View(momentReturn);
        }

        // POST: MomentReturns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Date,Observation,ReservationID")] MomentReturn momentReturn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(momentReturn).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Reservations");
            }
            return View(momentReturn);
        }

        // GET: MomentReturns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MomentReturn momentReturn = db.MomentReturns.Find(id);
            if (momentReturn == null)
            {
                return HttpNotFound();
            }
            return View(momentReturn);
        }

        // POST: MomentReturns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MomentReturn momentReturn = db.MomentReturns.Find(id);
            db.MomentReturns.Remove(momentReturn);
            db.SaveChanges();
            return RedirectToAction("Index","Reservations");
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
