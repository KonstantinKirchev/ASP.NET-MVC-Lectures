namespace Twitter.App.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class EditProfileBindingModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(32)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public HttpPostedFileBase ProfileImage { get; set; }

        public HttpPostedFileBase CoverImage { get; set; }
    }
}