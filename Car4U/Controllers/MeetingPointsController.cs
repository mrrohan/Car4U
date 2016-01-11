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
    public class MeetingPointsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MeetingPoints
        public ActionResult Index()
        {
            return View(db.MeetingPoints.ToList());
        }

        // GET: MeetingPoints/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeetingPoint meetingPoint = db.MeetingPoints.Find(id);
            if (meetingPoint == null)
            {
                return HttpNotFound();
            }
            return View(meetingPoint);
        }

        // GET: MeetingPoints/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MeetingPoints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Place")] MeetingPoint meetingPoint)
        {
            if (ModelState.IsValid)
            {
                db.MeetingPoints.Add(meetingPoint);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(meetingPoint);
        }

        // GET: MeetingPoints/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeetingPoint meetingPoint = db.MeetingPoints.Find(id);
            if (meetingPoint == null)
            {
                return HttpNotFound();
            }
            return View(meetingPoint);
        }

        // POST: MeetingPoints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Place")] MeetingPoint meetingPoint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meetingPoint).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(meetingPoint);
        }

        // GET: MeetingPoints/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeetingPoint meetingPoint = db.MeetingPoints.Find(id);
            if (meetingPoint == null)
            {
                return HttpNotFound();
            }
            return View(meetingPoint);
        }

        // POST: MeetingPoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MeetingPoint meetingPoint = db.MeetingPoints.Find(id);
            db.MeetingPoints.Remove(meetingPoint);
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
