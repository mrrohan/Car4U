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
    public class ReservationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reservations
        public ActionResult Index()
        {
            var reservations = db.Reservations.Include(r => r.Category).Include(r => r.Country).Include(r => r.MomentDelivery).Include(r => r.MomentReturn).Include(r => r.MPDelivery).Include(r => r.MPReturn);
            return View(reservations.ToList());
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }       

        // GET: Reservations/Create
        public ActionResult Create()
        {   
                    
            ViewBag.ExtraItemsID = new SelectList(db.ExtraModels, "ID", "Model");
            ViewBag.ExtraModels = new List<ExtraModel>(db.ExtraModels);
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName");
            ViewBag.CountryID = new SelectList(db.Countries, "ID", "Name");
            ViewBag.MomentDeliveryID = new SelectList(db.MomentDeliveries, "ID", "Observation");
            ViewBag.MomentReturnID = new SelectList(db.MomentReturns, "ID", "Observation");
            ViewBag.MPDeliveryID = new SelectList(db.MeetingPoints, "ID", "Place");
            ViewBag.MPReturnID = new SelectList(db.MeetingPoints, "ID", "Place");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Address,PostalCode,Telephone,Email,License,BI,DateOfBirth,ReservationDate,ReturnDate,DeliveryDate,FinalPrice,CountryID,CategoryID,MPDeliveryID,MPReturnID")] Reservation reservation, string[] selectedExtraModels)
        {
            if (ModelState.IsValid)
            {
                //get model ID from slectedcheckbox and search for the 1st available item of that model and add it to the reservation. 
                int extid;
                ExtraItem extritem = new ExtraItem();
                reservation.ExtraItems = new List<ExtraItem>();

                int s = selectedExtraModels.Count();

                for (int count=0; count<s;count++)
                {
                    extid =Convert.ToInt32(selectedExtraModels[count]);
                    extritem = db.ExtraItems.First(e => e.ExtraModelID == extid);
                    reservation.ExtraItems.Add(extritem);
                }


                //get price and add to FinalPrice from category of car
                int catid = reservation.CategoryID;
                Category cat = db.Categories.First(c => c.ID == catid);

                //add price of ExtraItems to FinalPrice
                foreach (ExtraItem i in reservation.ExtraItems)
                {
                    int modelid = i.ExtraModelID;
                    ExtraModel extrmodel = db.ExtraModels.First(m => m.ID==modelid);
                    reservation.FinalPrice += extrmodel.Price;
                }
                var span = reservation.ReturnDate.Subtract(reservation.DeliveryDate);
                int ndaysres = span.Days;
                              
                reservation.FinalPrice += cat.Price*ndaysres;
                reservation.ReservationDate = DateTime.Now;
                               
                db.Reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName", reservation.CategoryID);
            ViewBag.CountryID = new SelectList(db.Countries, "ID", "Name", reservation.CountryID);            
            ViewBag.MPDeliveryID = new SelectList(db.MeetingPoints, "ID", "Place", reservation.MPDeliveryID);
            ViewBag.MPReturnID = new SelectList(db.MeetingPoints, "ID", "Place", reservation.MPReturnID);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName", reservation.CategoryID);
            ViewBag.CountryID = new SelectList(db.Countries, "ID", "Name", reservation.CountryID);            
            ViewBag.MPDeliveryID = new SelectList(db.MeetingPoints, "ID", "Place", reservation.MPDeliveryID);
            ViewBag.MPReturnID = new SelectList(db.MeetingPoints, "ID", "Place", reservation.MPReturnID);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Address,PostalCode,Telephone,Email,License,BI,DateOfBirth,ReservationDate,ReturnDate,DeliveryDate,FinalPrice,CountryID,CategoryID,MPDeliveryID,MPReturnID,ExtraItemsID,MomentDeliveryID,MomentReturnID")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName", reservation.CategoryID);
            ViewBag.CountryID = new SelectList(db.Countries, "ID", "Name", reservation.CountryID);            
            ViewBag.MPDeliveryID = new SelectList(db.MeetingPoints, "ID", "Place", reservation.MPDeliveryID);
            ViewBag.MPReturnID = new SelectList(db.MeetingPoints, "ID", "Place", reservation.MPReturnID);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
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
