using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using FootballManager.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace FootballManager.DAL
{
    public class FMInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            const string name = "Admin";
            const string password = "Admin@123456";
            const string email = "admin@fmanager.pt";

            const string roleAdmin = "Admin";
            const string roleUser = "ChampionshipManager";

            //Create Role Admin if it does not exist
            var role1 = roleManager.FindByName(roleAdmin);
            var role2 = roleManager.FindByName(roleUser);

            if (role1 == null)
            {
                role1 = new IdentityRole(roleAdmin);
                role2 = new IdentityRole(roleUser);
                var roleresult1 = roleManager.Create(role1);
                var roleresult2 = roleManager.Create(role2);
            }

            var user = userManager.FindByName(name);

            if (user == null)
            {
                user = new ApplicationUser { UserName = name , Email = email, EmailConfirmed = true};
                var result = userManager.Create(user, password);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);

            if (!rolesForUser.Contains(role1.Name))
            {
                var result = userManager.AddToRole(user.Id, role1.Name);
            }
        }
    }
}