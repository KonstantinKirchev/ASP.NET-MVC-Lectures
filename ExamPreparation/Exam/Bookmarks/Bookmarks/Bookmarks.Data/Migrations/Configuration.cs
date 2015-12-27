namespace Bookmarks.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Bookmarks.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<Bookmarks.Data.BookmarksDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
            ContextKey = "Bookmarks.Data.ApplicationDbContext";
        }

        protected override void Seed(Bookmarks.Data.BookmarksDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "Blueeagle"))
            {
                var store = new UserStore<User>(context);
                var manager = new UserManager<User>(store);
                var user = new User { UserName = "Blueeagle", Email = "kosta1234@abv.bg" };

                manager.Create(user, "kosta1234");
                manager.AddToRole(user.Id, "Admin");
            }
        }
    }
}
