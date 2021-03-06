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
    public class ExtraModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ExtraModels
        public ActionResult Index()
        {
            var extraModels = db.ExtraModels.Include(e => e.ExtraModelType);
            return View(extraModels.ToList());
        }

        // GET: ExtraModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraModel extraModel = db.ExtraModels.Find(id);
            if (extraModel == null)
            {
                return HttpNotFound();
            }
            return View(extraModel);
        }

        // GET: ExtraModels/Create
        public ActionResult Create()
        {
            ViewBag.ExtraModelTypeID = new SelectList(db.ExtraModelTypes, "ID", "Description");
            return View();
        }

        // POST: ExtraModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Model,Price,Stock,ExtraModelTypeID")] ExtraModel extraModel)
        {
            if (ModelState.IsValid)
            {
                
                db.ExtraModels.Add(extraModel);
                db.SaveChanges();
                for (int c = 1; c <= extraModel.Stock; c++)
                {
                    ExtraItem extritem = new ExtraItem();
                    extritem.ExtraModel = extraModel;
                    extritem.InUse = false;
                    db.ExtraItems.Add(extritem);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            ViewBag.ExtraModelTypeID = new SelectList(db.ExtraModelTypes, "ID", "Description", extraModel.ExtraModelTypeID);
            return View(extraModel);
        }

        // GET: ExtraModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraModel extraModel = db.ExtraModels.Find(id);
            ViewBag.ExtraItems = new List<ExtraItem>(db.ExtraItems);
            if (extraModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExtraModelTypeID = new SelectList(db.ExtraModelTypes, "ID", "Description", extraModel.ExtraModelTypeID);
            return View(extraModel);
        }

        // POST: ExtraModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Model,Price,Stock,ExtraModelTypeID")] ExtraModel extraModel)
        {
            if (ModelState.IsValid)
            {              

                var modelIndb = new ExtraModel();
                modelIndb = db.ExtraModels.First(e => e.ID == extraModel.ID);

                if (modelIndb.Stock != extraModel.Stock)
                {
                    if (modelIndb.Stock > extraModel.Stock)
                    {
                        int deletecount = modelIndb.Stock - extraModel.Stock;
                        var extitem = new ExtraItem();
                        while (deletecount > 0)
                        {
                            extitem = db.ExtraItems.FirstOrDefault(e => e.ExtraModelID == extraModel.ID && e.InUse == false);
                            if (extitem != null)
                            {
                                modelIndb.Stock = extraModel.Stock;
                                db.ExtraItems.Remove(extitem);
                            }
                            deletecount--;

                        }
                    }
                    if (modelIndb.Stock < extraModel.Stock)
                    {
                        int createcount = extraModel.Stock - modelIndb.Stock;                        
                        while (createcount > 0)
                        {
                            var extitem = new ExtraItem();
                            extitem.ExtraModelID = modelIndb.ID;
                            extitem.InUse = false;
                            db.ExtraItems.Add(extitem);                            
                            createcount--;
                        }
                        modelIndb.Stock = extraModel.Stock;

                    }
                }
                modelIndb.Model = extraModel.Model;
                modelIndb.Price = extraModel.Price;
                
                modelIndb.ExtraModelTypeID = extraModel.ExtraModelTypeID;
                
                db.Entry(modelIndb).State = EntityState.Modified;                
                db.SaveChanges();             
                
                return RedirectToAction("Index");
            }
            
            ViewBag.ExtraModelTypeID = new SelectList(db.ExtraModelTypes, "ID", "Description", extraModel.ExtraModelTypeID);
            return View(extraModel);
        }

        // GET: ExtraModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraModel extraModel = db.ExtraModels.Find(id);
            if (extraModel == null)
            {
                return HttpNotFound();
            }
            return View(extraModel);
        }

        // POST: ExtraModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExtraModel extraModel = db.ExtraModels.Find(id);
            db.ExtraModels.Remove(extraModel);
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
