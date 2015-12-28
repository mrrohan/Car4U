using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Car4U.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Car4U.Models;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Car4U.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();



        [Authorize]
        public ActionResult Details()
        {
            ViewBag.Message = "Your contact page.";
            string userid = User.Identity.GetUserId();

            var currentuser = db.Users.SingleOrDefault(u => u.Id == userid);

            return View(currentuser);
        }

        //
        //GET : edit user
        [Authorize]
        public ActionResult Edit()
        {
            ViewBag.CountryID = new SelectList(db.Countries, "ID", "Name");
            ViewBag.Message = "Your contact page.";
            string userid = User.Identity.GetUserId();
            var currentuser = db.Users.SingleOrDefault(u => u.Id == userid);
            return View(currentuser);
        }

        //
        //POST: edit user
        [Authorize]
        [HttpPost]
        public ActionResult Edit(ApplicationUser model)
        {
            ViewBag.Message = "Your contact page.";
            string userid = User.Identity.GetUserId();
            var currentuser = db.Users.SingleOrDefault(u => u.Id == userid);
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            currentuser.UserName = model.Email;
            currentuser.Email = model.Email;
            currentuser.Name = model.Name;
            currentuser.Address = model.Address;
            currentuser.BI = model.BI;
            currentuser.License = model.License;
            currentuser.CountryID = model.CountryID;
            currentuser.PostalCode = model.PostalCode;


            var result = UserManager.Update(currentuser);
            db.Entry(currentuser).State = EntityState.Modified;
            db.SaveChanges();

            ViewBag.CountryID = new SelectList(db.Countries, "ID", "Name", currentuser.CountryID);
            return RedirectToAction("Details");
        }

        //
        //GET: delete user
        [Authorize]
        public ActionResult Delete()
        {
            ViewBag.Message = "Your contact page.";
            string userid = User.Identity.GetUserId();
            var currentuser = db.Users.SingleOrDefault(u => u.Id == userid);
            return View(currentuser);
        }

        //
        //POST: Delete user
        [Authorize]
        [HttpPost]
        public ActionResult Delete(ApplicationUser model)
        {
            ViewBag.Message = "Your contact page.";
            string userid = User.Identity.GetUserId();
            var currentuser = db.Users.SingleOrDefault(u => u.Id == userid);
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var logins = currentuser.Logins;

            foreach (var login in logins.ToList())
            {
                UserManager.RemoveLogin(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
            }

            var rolesForUser = UserManager.GetRoles(userid);

            if (rolesForUser.Count() > 0)
            {
                foreach (var item in rolesForUser.ToList())
                {
                    // item should be the name of the role
                    var result = UserManager.RemoveFromRole(currentuser.Id, item);
                }
            }

            //Codigo que se segue é para apagar coisas associadas ao user
            //var ComentsForUser = currentuser.Comments;

            //foreach (var item in ComentsForUser.ToList())
            //{
            //    db.Comments.Remove(item);
            //}

            UserManager.Delete(currentuser);

            return RedirectToAction("AfterDelete");
        }

        //
        //GET: delete user
        [Authorize]
        public ActionResult AfterDelete()
        {
            return View();
        }

        //
        //get Follow cars
        [Authorize]
        public ActionResult FollowedCars()
        {
            ViewBag.Message = "Your contact page.";
            string userid = User.Identity.GetUserId();

            var currentuser = db.Users.SingleOrDefault(u => u.Id == userid);

            //var cars = db.Cars;

            var Cars = db.Cars.OrderBy(r => r.carModel.Description).Include(c => c.carModel).Include(c => c.category).Include(c => c.fuelType).Where(l => l.users.Select(c => c.Id).Contains(userid));

            return View(Cars);
        }

        //
        // GET: /Unfollowcar
        public ActionResult UnfollowCar(int? id)
        {
            string userid = User.Identity.GetUserId();
            var currentUser = db.Users.SingleOrDefault(u => u.Id == userid);

            if (id != null)
            {
                int Id = id ?? default(int);

                var thisCar = db.Cars.SingleOrDefault(u => u.ID == Id);

               
                currentUser.cars.Remove(thisCar);
                db.SaveChanges();

                ViewBag.ResultMessage = "Car unfolloed!";
            }
            else
            {
                ViewBag.ResultMessage = "Operation failed!";
            }


            return RedirectToAction("FollowedCars");
        }
    }
}