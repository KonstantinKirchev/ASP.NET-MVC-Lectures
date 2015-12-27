namespace Caching.Data.UnitOfWork
{
    using Caching.Data.Repositories;
    using Caching.Models;

    public interface IApplicationData
    {
        IRepository<User> Users { get; }

        void SaveChanges();
    }
}