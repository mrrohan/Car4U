using Car4U.DAL;
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
            ViewBag.MeetingPointID = new SelectList(db.MeetingPoints, "ID", "Place");
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName");
            return View();
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