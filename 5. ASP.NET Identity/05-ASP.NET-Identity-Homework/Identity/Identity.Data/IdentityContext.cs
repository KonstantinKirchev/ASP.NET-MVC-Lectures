namespace Identity.Data
{
    using System.Data.Entity;

    using Identity.Data.Migrations;
    using Identity.Models;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class IdentityContext : IdentityDbContext<User>
    {
        public IdentityContext()
            : base("name=IdentityContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<IdentityContext, Configuration>());
        }

        public static IdentityContext Create()
        {
            return new IdentityContext();
        }
    }
}