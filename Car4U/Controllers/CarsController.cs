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
using Microsoft.AspNet.Identity;
using System.IO;
using Car4U.ViewModels;

namespace Car4U.Controllers
{
    public class CarsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cars
        public ActionResult Index()
        {
            var cars = db.Cars.Include(c => c.carModel).Include(c => c.category).Include(c => c.fuelType).Include(c => c.Gear);
            return View(cars.ToList());
        }

        // GET: Cars
        public ActionResult PublicIndex(int[] selectedCats)
        {
            var viewModel = new CarIndex();
            viewModel.Cars = db.Cars.OrderBy(i => i.carModel.brand.Description).ToList();

            ViewBag.MPDeliveryID = new SelectList(db.MeetingPoints, "ID", "Place");
            ViewBag.MPReturnID = new SelectList(db.MeetingPoints, "ID", "Place");
            ViewBag.categories = db.Categories.ToList();

            if (selectedCats != null)
            {
                ViewBag.catid = selectedCats.ToList();
                ViewBag.show = 0;
            }
            else
            {
                ViewBag.catid = new int[1];
                ViewBag.show = 1;
            }


            

           return View(viewModel);
        }

        //Post Index
        [HttpPost]
        public ActionResult PublicIndex([Bind(Include = "BeginDate,BeginHour,EndDate,EndHour,CategoryID,MPDeliveryID,MPReturnID")] InfoSender info2, CarIndex info, int? carid)
        {

            if (carid == null)
            {
                return View();
            }
            ViewBag.MPDeliveryID = new SelectList(db.MeetingPoints, "ID", "Place", info2.MPDeliveryID);
            ViewBag.MPReturnID = new SelectList(db.MeetingPoints, "ID", "Place", info2.MPReturnID);
            //ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName", info.CategoryID);

            return RedirectToAction("CreateTeste", "Reservations", new { mpreliveryid = info2.MPDeliveryID, mpreturnid = info2.MPReturnID, categotyid = info.Infosender.CategoryID, begindate = info.Infosender.BeginDate.ToString("yyyy-MM-dd"), beginhour = info.Infosender.BeginHour.ToString("HH:mm"), enddate = info.Infosender.EndDate.ToString("yyyy-MM-dd"), endhour = info.Infosender.EndHour.ToString("HH:mm"), carid=carid });
        }

        //GET: SearchView
        public ActionResult SearchView(int? mpreliveryid, int? mpreturnid, int? categotyid, DateTime? begindate, DateTime? beginhour, DateTime? enddate, DateTime? endhour)
        {

            if (mpreliveryid != null)
            {
                ViewBag.mpdelivery = mpreliveryid ?? default(int);
            }
            if (mpreturnid != null)
            {
                ViewBag.mpreturn = mpreturnid ?? default(int);
            }
            if (begindate != null)
            {
                ViewBag.begidate = begindate ?? default(DateTime);
            }
            if (categotyid != null)
            {
                ViewBag.category = categotyid ?? default(int);
            }
            if (beginhour != null)
            {
                ViewBag.begihour = beginhour ?? default(DateTime);
            }
            if (enddate != null)
            {
                ViewBag.enddate = enddate ?? default(DateTime);
        }
            if (endhour != null)
        {
                ViewBag.endhour = endhour ?? default(DateTime);
            }


            var Cars = db.Cars.Include(c => c.carModel).Include(c => c.category).Include(c => c.fuelType).Include(c => c.Gear).Where(l=>l.CategoryID== categotyid).ToList();
            return View(Cars);
        }

        //GET: Cars/Details/5
        public ActionResult Details(int? id, int? view)
        {
            if (id == null || view == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (view != null)
            {
                ViewBag.View = view;
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
            ViewBag.GearID = new SelectList(db.Gears, "ID", "Description");
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
        public ActionResult Create([Bind(Include = "ID,LicensePlate,RegisterDate,NDoors,NLuggage,Engine,HorsePower,GearID,CategoryID,FuelTypeID,CarModelID")] Car car, HttpPostedFileBase upload)
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
                    car.FilePaths.Add(photo);
                    upload.SaveAs(Path.Combine(Server.MapPath("~/images"), photo.FileName));
                }

                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GearID = new SelectList(db.Gears, "ID", "Description", car.GearID);
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
            ViewBag.GearID = new SelectList(db.Gears, "ID", "Description", car.GearID);
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
        public ActionResult Edit([Bind(Include = "ID,LicensePlate,RegisterDate,NDoors,NLuggage,Engine,HorsePower,GearID,CategoryID,FuelTypeID,CarModelID")] Car car)
        { 
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GearID = new SelectList(db.Gears, "ID", "Description", car.GearID);
            ViewBag.CarModelID = new SelectList(db.CarModels, "ID", "Description", car.CarModelID);
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName", car.CategoryID);
            ViewBag.FuelTypeID = new SelectList(db.FuelTypes, "ID", "Description", car.FuelTypeID);
            return View(car);
        }

        // GET: Cars/EditPhoto/5
        public ActionResult EditPhoto(int? id)
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

        // POST: Cars/EditPhoto/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPhoto(HttpPostedFileBase upload, int? id)
        {
            Car car = db.Cars.Find(id);
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    if (car.FilePaths.Any(f => f.FileType == FileType.Photo))
                    {
                        db.FilePaths.Remove(car.FilePaths.First(f => f.FileType == FileType.Photo));
                    }
                    var photo = new FilePath
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Photo
                    };
                    car.FilePaths = new List<FilePath>();
                    car.FilePaths.Add(photo);
                    upload.SaveAs(Path.Combine(Server.MapPath("~/images"), photo.FileName));
                }
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details/" + id);
            }
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


        //
        // GET: /FollowCar/5
        [Authorize]
        public ActionResult FollowCar(int? carModel)
        {
            string userid = User.Identity.GetUserId();
            var currentUser = db.Users.SingleOrDefault(u => u.Id == userid);

            if (carModel != null)
            {
                int Id = carModel ?? default(int);

                var thisCar = db.Cars.SingleOrDefault(u => u.ID == Id);

                //thisCar.UserID = userid;
                currentUser.cars.Add(thisCar);
                db.SaveChanges();

                ViewBag.ResultMessage = "A seguir carro com sucesso!";
            }
            else
            {
                ViewBag.ResultMessage = "Não deu para seguir o carro!";
            }
            

            return RedirectToAction("Details/"+carModel);
        }

        // GET: Cars that are leaving
        //public ActionResult LeavingCars()
        //{
        //    var DateAndTime = DateTime.Now;
        //    var today = DateAndTime.Date;
        //    var cars = db.Cars.Include(c => c.carModel).Include(c => c.category).Include(c => c.fuelType).Where(l => l.CarStatus.Select(c => c.BeginDate).Contains(today)).Where(l => l.CarStatus.Select(c => c.Outside).Contains(false));
        //    return View(cars.ToList());
        //}

        // GET: Leaving Cars Index
        public ActionResult LeavingCarsIndex(int? id)
        {
            var DateAndTime = DateTime.Now;
            var today = DateAndTime.Date;
            var cars = db.Cars.Include(c => c.carModel).Include(c => c.category).Include(c => c.fuelType).Where(l => l.CarStatus.Count(c => c.BeginDate == today) > 0).Where(l => l.CarStatus.Count(c => c.Status.Description.Contains("Disponivel")) <= 0);

            if (id != null)
            {
                ViewBag.ID = id;
            }


            return View(cars.ToList());
        }

        //
        //GET : CarStatus.Outside = true
        public ActionResult Outside(int? id)
        {
            var carstatustoupdate = db.CarStatus.SingleOrDefault(u => u.ID == id);

            carstatustoupdate.Outside = true;

            db.Entry(carstatustoupdate).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("LeavingCarsIndex");
        }

        //
        //GET : CarStatus.Outside = false
        public ActionResult Inside(int? id)
        {
            var carstatustoupdate = db.CarStatus.SingleOrDefault(u => u.ID == id);

            carstatustoupdate.Outside = false;

            db.Entry(carstatustoupdate).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("LeavingCarsIndex");
        }

        // GET: Cars that are entering
        public ActionResult EnteringCars(int? id)
        {
            var DateAndTime = DateTime.Now;
            var today = DateAndTime.Date;
            var cars = db.Cars.Include(c => c.carModel).Include(c => c.category).Include(c => c.fuelType).Where(l => l.CarStatus.Count(c => c.FinishDate == today) > 0).Where(l => l.CarStatus.Count(c => c.Status.Description.Contains("Disponivel")) <= 0);
           
            if (id != null)
            {
                ViewBag.ID = id;
            }
            return View(cars.ToList());
        }

        //
        //GET : CarStatus.Outside = true
        public ActionResult Outside2(int? id)
        {
            var carstatustoupdate = db.CarStatus.Find(id);

            carstatustoupdate.Outside = true;

            db.Entry(carstatustoupdate).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("EnteringCars");
        }

        //
        //GET : CarStatus.Outside = false
        public ActionResult Inside2(int? id)
        {
            var carstatustoupdate = db.CarStatus.SingleOrDefault(u => u.ID == id);

            carstatustoupdate.Outside = false;

            db.Entry(carstatustoupdate).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("EnteringCars");
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
