namespace VGGLinkedIn.Data
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using VGGLinkedIn.Data.Migrations;
    using VGGLinkedIn.Models;

    public class VGGLinkedInContext : IdentityDbContext<User>, IVGGLinkedInContext
    {
        public VGGLinkedInContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<VGGLinkedInContext, Configuration>());
        }

        public static VGGLinkedInContext Create()
        {
            return new VGGLinkedInContext();
        }

        public IDbSet<Certification> Certifications { get; set; }

        public IDbSet<Discussion> Discussions { get; set; }

        public IDbSet<Experience> Experiences { get; set; }

        public IDbSet<Group> Groups { get; set; }

        public IDbSet<UserLanguage> Languages { get; set; }

        public IDbSet<Project> Projects { get; set; }

        public IDbSet<Skill> Skills { get; set; }

        public IDbSet<Endorcement> Endorcements { get; set; }

        public IDbSet<AdministrationLog> AdministrationLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Endorcement>().HasRequired(x => x.UserSkill).WithMany(x => x.Endorcements).WillCascadeOnDelete(false);
            modelBuilder.Entity<Experience>().HasRequired(x => x.User).WithMany(x => x.Experiences).WillCascadeOnDelete(false);
            modelBuilder.Entity<Group>().HasRequired(x => x.Owner).WithOptional().WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }
    }
}
