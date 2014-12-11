namespace RaceControl.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;
    using RaceControl.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<RaceControl.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            DefaultRoles = new List<string> { "AppAdmin", "ClubAdmin", "User" };
            DefaultUsers = new List<Tuple<string, string>> { new Tuple<string, string>("SysAdmin", "Ferrari73!") };
            AutomaticMigrationsEnabled = true;
        }

        private List<string> DefaultRoles { get; set; }
        private List<Tuple<String, String>> DefaultUsers { get; set; }

        protected override void Seed(RaceControl.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.


            // I don't use 'addorupdate' as it can mess with existing data. This solution
            // is more verbose, but works 100% of the time

            // Add the roles the application will need
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            foreach (var dr in DefaultRoles)
            {
                if (!context.Roles.Any(r => r.Name == dr))
                {
                    roleManager.Create(new IdentityRole(dr));
                }
            }

            // Add default user, empty profile and assign to roles
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            foreach (var du in DefaultUsers)
            {
                if (!context.Users.Any(u => u.UserName == du.Item1))
                {
                    var user = new ApplicationUser { UserName = du.Item1 };
                    userManager.Create(user, du.Item2);
                    userManager.AddToRole(user.Id, "AppAdmin");
                    userManager.AddToRole(user.Id, "User");

                    var profile = new ProfileModel();
                    profile.user = user;
                    var dbContext = DataDBContext.Create();
                    dbContext.Profiles.Add(profile);
                    dbContext.SaveChanges();
                }
            }
        }
    }
}