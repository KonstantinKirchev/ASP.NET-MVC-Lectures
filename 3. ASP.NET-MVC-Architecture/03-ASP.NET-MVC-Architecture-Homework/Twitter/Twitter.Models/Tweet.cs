namespace Twitter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Tweet
    {
        private ICollection<User> favoritedBy;

        private ICollection<User> retweetedBy;

        private ICollection<Report> reports;

        private ICollection<Tweet> replyTweets;

        public Tweet()
        {
            this.favoritedBy = new HashSet<User>();
            this.retweetedBy = new HashSet<User>();
            this.reports = new HashSet<Report>();
            this.replyTweets = new HashSet<Tweet>();
        }

        [Key]
        public int Id { get; set; }

        public string PageUrl { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public virtual ICollection<User> FavoritedBy
        {
            get
            {
                return this.favoritedBy;
            }

            set
            {
                this.favoritedBy = value;
            }
        }

        public virtual ICollection<User> RetweetedBy
        {
            get
            {
                return this.retweetedBy;
            }

            set
            {
                this.retweetedBy = value;
            }
        }

        public virtual ICollection<Report> Reports
        {
            get
            {
                return this.reports;
            }

            set
            {
                this.reports = value;
            }
        }

        public virtual ICollection<Tweet> ReplyTweets
        {
            get
            {
                return this.replyTweets;
            }

            set
            {
                this.replyTweets = value;
            }
        }
    }
}