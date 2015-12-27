namespace Bookmarks.Data.UnitOfWork
{
    using System;
    using System.Collections.Generic;

    using Bookmarks.Data.Repositories;
    using Bookmarks.Models;

    public class BookmarksData : IBookmarksData
    {
        private readonly IBookmarksDbContext dbContext;

        private readonly IDictionary<Type, object> repositories;

        public BookmarksData(IBookmarksDbContext context)
        {
            this.dbContext = context;
            this.repositories = new Dictionary<Type, object>();
        }
        public IRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
        }

        public IRepository<Bookmark> Bookmarks
        {
            get
            {
                return this.GetRepository<Bookmark>();
            }
        }
        public IRepository<Vote> Votes
        {
            get
            {
                return this.GetRepository<Vote>();
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                return this.GetRepository<Category>();
            }
        }

        public IRepository<Comment> Comments
        {
            get { return this.GetRepository<Comment>(); }
        }

        public int SaveChanges()
        {
            return this.dbContext.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);
                this.repositories.Add(
                    typeof(T),
                    Activator.CreateInstance(type, this.dbContext));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }
    }
}
