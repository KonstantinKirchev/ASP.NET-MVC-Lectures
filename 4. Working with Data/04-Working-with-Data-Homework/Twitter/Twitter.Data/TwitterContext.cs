namespace Twitter.Data
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Twitter.Data.Contracts;
    using Twitter.Data.Migrations;
    using Twitter.Models;

    public class TwitterContext : IdentityDbContext<User>, ITwitterContext
    {
        public TwitterContext()
            : base("name=TwitterContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TwitterContext, Configuration>());
        }

        public virtual IDbSet<Tweet> Tweets { get; set; }

        public virtual IDbSet<Notification> Notifications { get; set; }

        public virtual IDbSet<Message> Messages { get; set; }

        public virtual IDbSet<Report> Reports { get; set; }

        public static TwitterContext Create()
        {
            return new TwitterContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<User>()
                        .HasMany(u => u.SentMessages)
                        .WithRequired(m => m.Receiver)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                        .HasMany(u => u.ReceivedMessages)
                        .WithRequired(m => m.Sender)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>().HasMany(u => u.Followers).WithMany(u => u.Following).Map(
                m =>
                    {
                        m.MapLeftKey("FollowerId");
                        m.MapRightKey("FollowingId");
                        m.ToTable("FollowerFollowing");
                    });

            modelBuilder.Entity<Tweet>().HasMany(u => u.ReplyTweets).WithMany().Map(
                m =>
                    {
                        m.MapLeftKey("TweetId");
                        m.MapRightKey("ReplyTweetId");
                        m.ToTable("ReplyTweets");
                    });

            modelBuilder.Entity<Tweet>().HasMany(t => t.FavoritedBy).WithMany(u => u.FavoriteTweets).Map(
                m =>
                    {
                        m.MapLeftKey("UserId");
                        m.MapRightKey("TweetId");
                        m.ToTable("UsersFavoriteTweets");
                    });

            modelBuilder.Entity<Tweet>().HasMany(t => t.RetweetedBy).WithMany().Map(
                m =>
                    {
                        m.MapLeftKey("TweetId");
                        m.MapRightKey("UserId");
                        m.ToTable("UsersReTweets");
                    });

            base.OnModelCreating(modelBuilder);
        }
    }
}