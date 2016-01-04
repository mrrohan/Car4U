using Car4U.DAL;
using Car4U.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Car4U.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            //var locais = db.MeetingPoints.ToList();
            ViewBag.MPDeliveryID = new SelectList(db.MeetingPoints, "ID", "Place");
            ViewBag.MPReturnID = new SelectList(db.MeetingPoints, "ID", "Place");
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName");
            return View();
        }

        //Post Index
         [HttpPost]
        public ActionResult Index([Bind(Include = "BeginDate,BeginHour,EndDate,EndHour,CategoryID,MPDeliveryID,MPReturnID")] InfoSender info)
        {
    

            ViewBag.MPDeliveryID = new SelectList(db.MeetingPoints, "ID", "Place", info.MPDeliveryID);
            ViewBag.MPReturnID = new SelectList(db.MeetingPoints, "ID", "Place", info.MPReturnID);
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName", info.CategoryID);

            return RedirectToAction("Teste", "Home", new { mpreliveryid = info.MPDeliveryID, mpreturnid = info.MPReturnID, categotyid = info.CategoryID, begindate = info.BeginDate.ToString("yyyy-MM-dd"), beginhour = info.BeginHour.ToString("HH:mm"), enddate = info.EndDate.ToString("yyyy-MM-dd"), endhour = info.EndHour.ToString("HH:mm") });
        }

         public ActionResult Teste(InfoSender info, int? mpreliveryid, int? mpreturnid, int? categotyid, DateTime? begindate, DateTime? beginhour, DateTime? enddate, DateTime? endhour)
         {
             if (mpreliveryid != null)
             {
                 info.MPDeliveryID = mpreliveryid ?? default(int);
             }
             if (mpreturnid != null)
             {
                 info.MPReturnID = mpreturnid ?? default(int);
             }
             if (categotyid != null)
             {
                 info.CategoryID = categotyid ?? default(int);
             }

             if (begindate != null)
             {
                 info.BeginDate = begindate ?? default(DateTime);
             }


             if (beginhour != null)
             {
                 info.BeginHour = beginhour ?? default(DateTime);
             }

             if (enddate != null)
             {
                 info.EndDate = enddate ?? default(DateTime);
             }

             if (endhour != null)
             {
                 info.EndHour = endhour ?? default(DateTime);
             }

             return View(info);
         }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Frota()
        {
            return View();
        }

        public ActionResult BackOffice()
        {
            return View();
        }

    }
}