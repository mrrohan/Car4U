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
    public class CarStatusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CarStatus
        public ActionResult Index()
        {
            ViewBag.Cars = db.Cars;
            var carStatus = db.CarStatus.Include(c => c.Car).Include(c => c.Status);
            ViewBag.reserva = new List<Reservation>(db.Reservations);
            return View(carStatus.ToList());
        }

        // GET: CarStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarStatus carStatus = db.CarStatus.Find(id);
            if (carStatus == null)
            {
                return HttpNotFound();
            }
            return View(carStatus);
        }

        // GET: CarStatus/Create
        public ActionResult Create()
        {
            ViewBag.CarID = new SelectList(db.Cars, "ID", "LicensePlate");
            ViewBag.StatusID = new SelectList(db.Status, "ID", "Description");
            return View();
        }

        // POST: CarStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Observation,DeliveryPlace,ReturnPlace,Outside,BeginDate,BeginHour,FinishDate,FinishHour,CarID,StatusID")] CarStatus carStatus)
        {
            if (ModelState.IsValid)
            {

                carStatus.Outside = false;
                db.CarStatus.Add(carStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarID = new SelectList(db.Cars, "ID", "LicensePlate", carStatus.CarID);
            ViewBag.StatusID = new SelectList(db.Status, "ID", "Description", carStatus.StatusID);
            return View(carStatus);
        }

        // GET: CarStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarStatus carStatus = db.CarStatus.Find(id);

            if (carStatus == null)
            {
                return HttpNotFound();
            }

            ViewBag.CarID = new SelectList(db.Cars, "ID", "LicensePlate", carStatus.CarID);
            ViewBag.StatusID = new SelectList(db.Status, "ID", "Description", carStatus.StatusID);
            return View(carStatus);
        }

        // POST: CarStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Observation,DeliveryPlace,ReturnPlace,BeginDate,BeginHour,FinishDate,FinishHour,CarID,StatusID")] CarStatus carStatus)
        {
            if (ModelState.IsValid)
            {
                CarStatus carstas = db.CarStatus.Find(carStatus.ID);

                carstas.BeginDate =carStatus.BeginDate;
                carstas.CarID = carStatus.CarID;
                carstas.DeliveryPlace = carStatus.DeliveryPlace;
                carstas.FinishDate = carStatus.FinishDate;
                carstas.Observation = carStatus.Observation;
                carstas.Outside = carStatus.Outside;
                carstas.ReturnPlace = carStatus.ReturnPlace;
                carstas.StatusID = carStatus.StatusID;

                db.Entry(carstas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarID = new SelectList(db.Cars, "ID", "LicensePlate", carStatus.CarID);
            ViewBag.StatusID = new SelectList(db.Status, "ID", "Description", carStatus.StatusID);
            return View(carStatus);
        }

        // GET: CarStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarStatus carStatus = db.CarStatus.Find(id);
            if (carStatus == null)
            {
                return HttpNotFound();
            }
            return View(carStatus);
        }

        // POST: CarStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarStatus carStatus = db.CarStatus.Find(id);
            db.CarStatus.Remove(carStatus);
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
