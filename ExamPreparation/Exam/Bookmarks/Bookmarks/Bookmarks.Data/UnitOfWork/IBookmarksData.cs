namespace Bookmarks.Data.UnitOfWork
{
    using Bookmarks.Data.Repositories;
    using Bookmarks.Models;

    public interface IBookmarksData
    {
        IRepository<User> Users { get; }

        IRepository<Bookmark> Bookmarks { get; }

        IRepository<Category> Categories { get; }

        IRepository<Comment> Comments { get; }

        IRepository<Vote> Votes { get; }

        int SaveChanges();

    }
}
