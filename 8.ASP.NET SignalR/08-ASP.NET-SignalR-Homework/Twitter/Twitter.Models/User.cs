namespace Twitter.Models
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        private ICollection<Tweet> ownTweets;

        private ICollection<Tweet> favoriteTweets;

        private ICollection<Report> reportedTweets;

        private ICollection<User> followers;

        private ICollection<User> following;

        private ICollection<Message> sentMessages;

        private ICollection<Message> receivedMessages;

        private ICollection<Notification> notifications;

        public User()
        {
            this.ownTweets = new HashSet<Tweet>();
            this.favoriteTweets = new HashSet<Tweet>();
            this.reportedTweets = new HashSet<Report>();
            this.followers = new HashSet<User>();
            this.following = new HashSet<User>();
            this.sentMessages = new HashSet<Message>();
            this.receivedMessages = new HashSet<Message>();
            this.notifications = new HashSet<Notification>();
        }

        public string ProfileImage { get; set; }

        public string CoverImage { get; set; }

        public virtual ICollection<Tweet> OwnTweets
        {
            get
            {
                return this.ownTweets;
            }

            set
            {
                this.ownTweets = value;
            }
        }

        public virtual ICollection<Tweet> FavoriteTweets
        {
            get
            {
                return this.favoriteTweets;
            }

            set
            {
                this.favoriteTweets = value;
            }
        }

        public virtual ICollection<Report> ReportedTweets
        {
            get
            {
                return this.reportedTweets;
            }

            set
            {
                this.reportedTweets = value;
            }
        }

        public virtual ICollection<User> Followers
        {
            get
            {
                return this.followers;
            }

            set
            {
                this.followers = value;
            }
        }

        public virtual ICollection<User> Following
        {
            get
            {
                return this.following;
            }

            set
            {
                this.following = value;
            }
        }

        public virtual ICollection<Message> SentMessages
        {
            get
            {
                return this.sentMessages;
            }

            set
            {
                this.sentMessages = value;
            }
        }

        public virtual ICollection<Message> ReceivedMessages
        {
            get
            {
                return this.receivedMessages;
            }

            set
            {
                this.receivedMessages = value;
            }
        }

        public virtual ICollection<Notification> Notifications
        {
            get
            {
                return this.notifications;
            }

            set
            {
                this.notifications = value;
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}