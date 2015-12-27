namespace Twitter.App.Models.ViewModels
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Twitter.App.Utilities;
    using Twitter.Models;

    public class UserViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string ProfileImage { get; set; }

        public string CoverImage { get; set; }

        public bool IsFollowing { get; set; }

        public static Expression<Func<User, UserViewModel>> Create(string loggedUserId)
        {
            return
                u =>
                new UserViewModel
                    {
                        Id = u.Id,
                        Username = u.UserName,
                        ProfileImage = u.ProfileImage ?? DefaultValues.DefaultProfileImage,
                        CoverImage = u.CoverImage ?? DefaultValues.DefaultCoverImage,
                        IsFollowing = u.Following.Any(f => f.Id.Equals(loggedUserId))
                    };
        }
    }
}