namespace Twitter.App.Models.ViewModels
{
    using PagedList;

    public class UserAndTweetsViewModel
    {
        public UserViewModel User { get; set; }

        public IPagedList<TweetViewModel> TweetViewModels { get; set; }
    }
}