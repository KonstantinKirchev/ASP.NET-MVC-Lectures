namespace Twitter.App.Models.ViewModels
{
    using PagedList;

    public class UserAndFollowersViewModel
    {
        public UserViewModel User { get; set; }

        public IPagedList<UserViewModel> Followers { get; set; }
    }
}