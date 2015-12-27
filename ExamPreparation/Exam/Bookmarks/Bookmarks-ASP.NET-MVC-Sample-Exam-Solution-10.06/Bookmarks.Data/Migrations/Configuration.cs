namespace Bookmarks.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Bookmarks.Data.BookmarksDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.ContextKey = "Bookmarks.Data.ApplicationDbContext";
            this.AutomaticMigrationDataLossAllowed = false;
        }
    }
}
