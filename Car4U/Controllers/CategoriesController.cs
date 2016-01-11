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
using Car4U.ViewModels;

namespace Car4U.Controllers
{
     [Authorize(Roles = "Admin, Employee")]
    public class CategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cars
        public ActionResult PrivateIndex()
        {
            var Cats = db.Categories.OrderBy(i => i.CategoryName);
            return View(Cats.ToList());
        }

        //// GET: Categories
        //public ActionResult Index(int? id)
        //{
        //    var viewModel = new FleetIndex();
        //    viewModel.Categories = db.Categories.OrderBy(i => i.CategoryName);

        //    if (id != null)
        //    {
        //        ViewBag.ID = id.Value;
        //        viewModel.Cars = viewModel.Categories.FirstOrDefault(c => c.ID == id).Cars;
        //    }

        //    ViewBag.MPDeliveryID = new SelectList(db.MeetingPoints, "ID", "Place");
        //    ViewBag.MPReturnID = new SelectList(db.MeetingPoints, "ID", "Place");
        //    ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName");

        //    return View(viewModel);
        //}

        ////Post Index
        //[HttpPost]
        //public ActionResult Index([Bind(Include = "BeginDate,BeginHour,EndDate,EndHour,CategoryID,MPDeliveryID,MPReturnID")] InfoSender info2, FleetIndex info, int? id )
        //{

        //    if (id != null)
        //    {
        //        info.Infosender.CategoryID = id ?? default(int);
        //    }
        //    ViewBag.MPDeliveryID = new SelectList(db.MeetingPoints, "ID", "Place", info2.MPDeliveryID);
        //    ViewBag.MPReturnID = new SelectList(db.MeetingPoints, "ID", "Place", info2.MPReturnID);
        //    //ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName", info.CategoryID);

        //    return RedirectToAction("Teste", "Home", new { mpreliveryid = info2.MPDeliveryID, mpreturnid = info2.MPReturnID, categotyid = info.Infosender.CategoryID, begindate = info.Infosender.BeginDate.ToString("yyyy-MM-dd"), beginhour = info.Infosender.BeginHour.ToString("HH:mm"), enddate = info.Infosender.EndDate.ToString("yyyy-MM-dd"), endhour = info.Infosender.EndHour.ToString("HH:mm") });
        //}

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CategoryName,Price,Warranty")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("PrivateIndex");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CategoryName,Price,Warranty")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("PrivateIndex");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("PrivateIndex");
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
