namespace Exam.Data
{
    using Exam.Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ExamDbContext : IdentityDbContext<User>
    {
        public ExamDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ExamDbContext Create()
        {
            return new ExamDbContext();
        }
    }
}
