using Microsoft.AspNet.Identity.EntityFramework;
using FootballManager.DAL;
using FootballManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MvcIdentity.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Roles/ManageUserRoles
        public ActionResult ManageUserRoles()
        {
            // prepopulat roles for the view dropdown
            var list = db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;
            return View();
        }

        // POST: /Roles/RoleAddToUser
        [HttpPost]
        [ValidateAntiForgeryToken]
         
        public ActionResult RoleAddToUser(string UserName, string RoleName)
        {
            // prepopulat roles for the view dropdown
            var list = db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(RoleName))
            {
                ViewBag.ResultMessage = "Username and Role Name are required.";
            }
            else
            {
                ApplicationUser user = db.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                if (user != null)
                {
                    var account = new FootballManager.Controllers.AccountController();
                    account.UserManager.AddToRoleAsync(user.Id, RoleName);

                    ViewBag.Sucess = true;
                    ModelState.Clear();
                    ViewBag.ResultMessage = "Role add to user with success!";
                }
                else
                {
                    ViewBag.Sucess = false;
                    ViewBag.ResultMessage = "Unrecognized User.";
                }
               
            }            
            return View("ManageUserRoles");
        }

        // POST: /Roles/GetRoles
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GetRoles(string UserName)
        {
            // prepopulat roles for the view dropdown
            var list = db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            if (!string.IsNullOrWhiteSpace(UserName))
            {
                ApplicationUser user = db.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                if (user != null)
                {
                    var account = new FootballManager.Controllers.AccountController();

                    var rolesForUser = await account.UserManager.GetRolesAsync(user.Id);
                    if (rolesForUser != null)
                    {
                        ViewBag.RolesForThisUser = rolesForUser;
                    }
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.Sucess = false;
                    ViewBag.ResultMessage = "Unrecognized User.";
                }               
            }
            else
            {
                ViewBag.ResultMessage = "Username is required.";
            }

            return View("ManageUserRoles");
        }

        // POST: /Roles/DeleteRoleForUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteRoleForUser(string UserName, string RoleName)
        {
            // prepopulat roles for the view dropdown
            var list = db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(RoleName))
            {
                ViewBag.ResultMessage = "Username and Role Name are required.";
            }
            else
            {
                var account = new FootballManager.Controllers.AccountController();
                ApplicationUser user = db.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                if (user != null)
                {
                    var isInRole = await account.UserManager.IsInRoleAsync(user.Id, RoleName);

                if (isInRole == true)
                {
                    await account.UserManager.RemoveFromRoleAsync(user.Id, RoleName);
                    ViewBag.Sucess = true;
                    ViewBag.ResultMessage = "Role removed from this user successfully !";
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.Sucess = false;
                    ViewBag.ResultMessage = "This user doesn't belong to selected role.";
                }

                }
                  else
                {
                    ViewBag.Sucess = false;
                    ViewBag.ResultMessage = "Unrecognized User.";
                }               
            }
            return View("ManageUserRoles");
        }
    }
}