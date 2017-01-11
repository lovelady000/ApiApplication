namespace Application.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Application.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Application.Data.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //CreateAccount(context);
        }

        private void CreateAccount(ApplicationDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "administrator",
                Email = "nguyendunghn0109@gmail.com",
                EmailConfirmed = true,
                BirthDay = DateTime.Now,
                FullName = "Nguyễn Năng Dũng",
                PhoneNumber = "0983007974",
                PhoneNumberConfirmed = true,
            };

            manager.Create(user, "123654$");

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new ApplicationRole { Name = "Admin" });
                roleManager.Create(new ApplicationRole { Name = "User" });
            }

            var adminUser = manager.FindByEmail("nguyendunghn0109@gmail.com");

            manager.AddToRoles(adminUser.Id, new string[] { "Admin" });
        }
    }
}
