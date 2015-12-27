namespace Twitter.App.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    using Twitter.App.Models.ViewModels;

    public class PostTweetBindingModel
    {
        public UserViewModel Author { get; set; }

        [Required]
        public string Content { get; set; }
    }
}