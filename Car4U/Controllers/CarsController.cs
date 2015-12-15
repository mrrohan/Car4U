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
    public class CarsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cars
        public ActionResult Index()
        {
            var cars = db.Cars.Include(c => c.carModel).Include(c => c.category).Include(c => c.fuelType);
            return View(cars.ToList());
        }

        // GET: Cars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            ViewBag.CarModelID = new SelectList(db.CarModels, "ID", "Description");
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName");
            ViewBag.FuelTypeID = new SelectList(db.FuelTypes, "ID", "Description");
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LicensePlate,RegisterDate,NDoors,NLuggage,Engine,HorsePower,CategoryID,FuelTypeID,CarModelID")] Car car, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var photo = new FilePath
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Photo
                    };
                    car.FilePaths = new List<FilePath>();
                    upload.SaveAs(System.IO.Path.Combine(Server.MapPath("~/images"), photo.FileName)); 
                    car.FilePaths.Add(photo);    
                }

                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarModelID = new SelectList(db.CarModels, "ID", "Description", car.CarModelID);
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName", car.CategoryID);
            ViewBag.FuelTypeID = new SelectList(db.FuelTypes, "ID", "Description", car.FuelTypeID);
            return View(car);
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarModelID = new SelectList(db.CarModels, "ID", "Description", car.CarModelID);
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName", car.CategoryID);
            ViewBag.FuelTypeID = new SelectList(db.FuelTypes, "ID", "Description", car.FuelTypeID);
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,LicensePlate,RegisterDate,NDoors,NLuggage,Engine,HorsePower,CategoryID,FuelTypeID,CarModelID")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarModelID = new SelectList(db.CarModels, "ID", "Description", car.CarModelID);
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName", car.CategoryID);
            ViewBag.FuelTypeID = new SelectList(db.FuelTypes, "ID", "Description", car.FuelTypeID);
            return View(car);
        }

        // GET: Cars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Car car = db.Cars.Find(id);
            db.Cars.Remove(car);
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
