using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.AspNet.Identity.EntityFramework;
using FootballManager.Models;
using System.Data.Entity;

namespace FootballManager.DAL
{
    public class FMInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            context.Roles.Add(new IdentityRole("Administrator"));
            context.Roles.Add(new IdentityRole("Champioship Manager"));
            context.Roles.Add(new IdentityRole("Visitor"));

            context.Users.Add(new ApplicationUser(){ UserName = "admin", Email = "admin@fmanager.com", PasswordHash = "asdaskdjbasdfvb" });

            context.SaveChanges();
        }
    }
}