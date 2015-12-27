namespace Bookmarks.Data
{
    using System.Data.Entity;
    
    using Bookmarks.Models;

    using Microsoft.AspNet.Identity.EntityFramework;
    using Migrations;

    public class BookmarksDbContext : IdentityDbContext<User>, IBookmarksDbContext
    {
        public BookmarksDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BookmarksDbContext, Configuration>());
        }

        public static BookmarksDbContext Create()
        {
            return new BookmarksDbContext();
        }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<Vote> Votes { get; set; }

        public IDbSet<Bookmark> Bookmarks { get; set; }

        public IDbSet<Category> Categories { get; set; }
    }
}
