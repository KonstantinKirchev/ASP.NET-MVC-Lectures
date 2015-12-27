namespace Identity.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Identity.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<IdentityContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(IdentityContext context)
        {
            if (!context.Users.Any(u => u.UserName == "admin@admin.com"))
            {
                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);

                var user = new User { Email = "admin@admin.com", UserName = "admin@admin.com" };
                userManager.Create(user, "admina");

                var role = new IdentityRole { Name = "Admin" };
                context.Roles.Add(role);

                var userRole = new IdentityUserRole { RoleId = role.Id, UserId = user.Id };
                role.Users.Add(userRole);

                context.SaveChanges();
            }
        }
    }
}