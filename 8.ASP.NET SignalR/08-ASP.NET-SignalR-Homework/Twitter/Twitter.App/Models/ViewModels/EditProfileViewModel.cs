namespace Twitter.App.Models.ViewModels
{
    using System.Web;

    public class EditProfileViewModel
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public HttpPostedFileBase ProfileImage { get; set; }

        public HttpPostedFileBase CoverImage { get; set; }
    }
}