namespace Twitter.App.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class EditProfileBindingModel
    {
        public string Username { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}