using Exam.Data.Repositories;
using Exam.Models;

namespace Exam.Data.UnitOfWork
{
    public interface IExamData
    {
        IRepository<User> Users { get; }

        int SaveChanges();
    }
}
