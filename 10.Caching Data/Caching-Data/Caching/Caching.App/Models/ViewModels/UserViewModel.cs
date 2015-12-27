namespace Caching.App.Models.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    using Caching.App.Infrastructure.Mapping;
    using Caching.Models;

    public class UserViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string Email { get; set; }
    }
}