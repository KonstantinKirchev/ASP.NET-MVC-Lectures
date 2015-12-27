using System;
using System.Collections.Generic;
using Events.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Events.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class DbMigrationsConfig : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public DbMigrationsConfig()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                var adminEmail = "admin@admin.com";
                var adminUsername = adminEmail;
                var adminFullName = "System Administrator";
                var adminPassword = adminEmail;
                string adminRole = "Administrator";

                this.CreateAdminUser(
                    context,
                    adminEmail,
                    adminUsername,
                    adminFullName,
                    adminPassword,
                    adminRole);
            }

            if (!context.Events.Any())
            {
                this.CreateSeveralEvents(context);
            }
        }

        private void CreateSeveralEvents(ApplicationDbContext context)
        {
            context.Events.Add(
                new Event()
                {
                    Title = "Party @ SoftUni",
                    StartDateTime = DateTime.Now.Date.AddDays(5).AddHours(21).AddMinutes(30),
                    Author = context.Users.First()
                });

            context.Events.Add(
                new Event()
                {
                    Title = "Passed Event <Anonymous>",
                    StartDateTime = DateTime.Now.Date.AddDays(-2).AddHours(10).AddMinutes(30),
                    Duration = TimeSpan.FromHours(1.5),
                    Comments = new HashSet<Comment>()
                            {
                                new Comment() { Text = "<Anonymous> comment" },
                                new Comment() { Text = "User comment", Author = context.Users.First() }
                            }
                });

            context.SaveChanges();
        }

        private void CreateAdminUser(
            ApplicationDbContext context,
            string adminEmail,
            string adminUsername,
            string adminFullName,
            string adminPassword,
            string adminRole)
        {
            var adminUser = new User()
            {
                UserName = adminUsername,
                FullName = adminFullName,
                Email = adminEmail
            };

            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);

            userManager.PasswordValidator = new PasswordValidator()
            {
                RequiredLength = 1,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            var userCreateResult = userManager.Create(adminUser, adminPassword);
            if (!userCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", userCreateResult.Errors));
            }

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var roleCreateResult = roleManager.Create(new IdentityRole(adminRole));
            if (!roleCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", roleCreateResult.Errors));
            }

            var addAdminRolesResult = userManager.AddToRole(adminUser.Id, adminRole);
            if (!addAdminRolesResult.Succeeded)
            {
                throw new Exception(string.Join("; ", addAdminRolesResult.Errors));
            }

            context.SaveChanges();
        }
    }
}
