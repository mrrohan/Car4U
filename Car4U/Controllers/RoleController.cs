using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using System.Net;
using Car4U.Models;
using Car4U.DAL;
using Car4U;

namespace Car4U.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        ApplicationDbContext context = new ApplicationDbContext();
        //

        // GET: /Employees/
        public ActionResult AllUsers()
        {
            var allUsers = context.Users.Where(l => l.Roles.Select(c => c.RoleId).Contains("2")).ToList();
            return View(allUsers);
        }

        //
        // GET: Create Employee
        [AllowAnonymous]
        public ActionResult RegisterEmployee()
        {
            ViewBag.Country = new SelectList(context.Countries, "ID", "Name");
            return View();
        }

        //
        //Post: Create Employee
        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterEmployee(RegisterEmployeeViewModel model)
        {
                if (ModelState.IsValid)
            {
           
                //var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name,
                    Address = model.Address,
                    BI = model.BI,
                    License = model.License,
                    CountryID = model.Country,
                    PostalCode = model.PostalCode
                };
                ViewBag.Country = new SelectList(context.Countries, "ID", "Name", user.CountryID);
                IdentityResult result = UserManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    var roleStore = new RoleStore<IdentityRole>(context);
                    var roleManager = new RoleManager<IdentityRole>(roleStore);

                    if (!roleManager.RoleExists("Employee"))
                    {
                        roleManager.Create(new IdentityRole { Id = "2", Name = "Employee" });
                    }

              
                    var roleresult = UserManager.AddToRole(user.Id, "Employee");

                    return RedirectToAction("AllUsers", "Role");
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        //GET: delete user
        [Authorize]
        public ActionResult DeleteUser(string id)
        {
            ViewBag.Message = "Your contact page.";
            string userid = id;
            var currentuser = context.Users.SingleOrDefault(u => u.Id == userid);
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

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

            return RedirectToAction("AllUsers");
        }

        //
        //delete role from user
         [Authorize]
        public ActionResult DeleteUserRole(string id)
        {
            string userid = id;
            var currentuser = context.Users.SingleOrDefault(u => u.Id == userid);
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (UserManager.IsInRole(currentuser.Id, "Employee"))
            {
                UserManager.RemoveFromRole(currentuser.Id, "Employee");
                ViewBag.ResultMessage = "Role removed from this user successfully !";
            }
            else
            {
                ViewBag.ResultMessage = "This user doesn't belong to selected role.";
            }
            return View("AllUsers");
        }


        //
        // GET: /Roles/
        public ActionResult Index()
        {
            var roles = context.Roles.ToList();
            return View(roles);
        }

        //
        // GET: /Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Roles/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = collection["RoleName"]
                });
                context.SaveChanges();
                ViewBag.ResultMessage = "Role created successfully !";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Roles/Edit/5
        public ActionResult Edit(string roleName)
        {
            var thisRole = context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            return View(thisRole);
        }

        //
        // POST: /Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Microsoft.AspNet.Identity.EntityFramework.IdentityRole role)
        {
            try
            {
                context.Entry(role).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Roles/Delete/5
        public ActionResult Delete(string RoleName)
        {
            var thisRole = context.Roles.Where(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            context.Roles.Remove(thisRole);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ManageUserRoles()
        {
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;
            return View();
        }

        //
        //add role to user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string UserName, string RoleName)
        {

            ApplicationUser user = context.Users.FirstOrDefault(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase));
            if (user != null) UserManager.AddToRole(user.Id, RoleName);

            ViewBag.ResultMessage = "Role created successfully !";

            // prepopulat roles for the view dropdown
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            return View("ManageUserRoles");
        }

        //
        //get roles from user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetRoles(string UserName)
        {
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                ApplicationUser user = context.Users.FirstOrDefault(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase));

                ViewBag.RolesForThisUser = UserManager.GetRoles(user.Id);

                // prepopulat roles for the view dropdown
                var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = list;
            }

            return View("ManageUserRoles");
        }

        //
        //delete role from user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string UserName, string RoleName)
        {

            ApplicationUser user = context.Users.FirstOrDefault(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase));

            if (UserManager.IsInRole(user.Id, RoleName))
            {
                UserManager.RemoveFromRole(user.Id, RoleName);
                ViewBag.ResultMessage = "Role removed from this user successfully !";
            }
            else
            {
                ViewBag.ResultMessage = "This user doesn't belong to selected role.";
            }
            // prepopulat roles for the view dropdown
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            return View("ManageUserRoles");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}