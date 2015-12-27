namespace VGGLinkedIn.Data
{
    using VGGLinkedIn.Data.Repositories;
    using VGGLinkedIn.Models;

    public interface IVGGLinkedInData
    {
        IRepository<User> Users { get; }
        IRepository<Certification> Certifications { get; }
        IRepository<Discussion> Discussions { get; }
        IRepository<Experience> Experiences { get; }
        IRepository<Group> Groups { get; }
        IRepository<Project> Projects { get; }
        IRepository<Skill> Skills { get; }
        IRepository<Endorcement> Endorcements { get; }
        IRepository<AdministrationLog> AdministrationLogs { get; }
        int SaveChanges();
    }
}
