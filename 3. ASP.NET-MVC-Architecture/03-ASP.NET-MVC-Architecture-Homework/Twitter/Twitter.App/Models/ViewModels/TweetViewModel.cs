namespace Twitter.App.Models.ViewModels
{
    using System;
    using System.Linq.Expressions;

    using Twitter.App.Utilities;
    using Twitter.Models;

    public class TweetViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string Uri { get; set; }

        public DateTime TweetedOn { get; set; }

        public UserViewModel Author { get; set; }

        public static Expression<Func<Tweet, TweetViewModel>> Create()
        {
            return tweet => new TweetViewModel
            {
                Id = tweet.Id,
                Content = tweet.Content,
                Uri = tweet.PageUrl,
                TweetedOn = tweet.Date,
                Author = new UserViewModel
                {
                    Username = tweet.Author.UserName,
                    ProfileImage = tweet.Author.ProfileImage ?? DefaultValues.DefaultProfileImage
                }
            };
        }
    }
}