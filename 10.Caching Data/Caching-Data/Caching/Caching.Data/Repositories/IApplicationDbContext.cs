namespace Caching.Data.Repositories
{
    using System;

    public interface IApplicationDbContext : IDisposable
    {
        int SaveChanges();
    }
}