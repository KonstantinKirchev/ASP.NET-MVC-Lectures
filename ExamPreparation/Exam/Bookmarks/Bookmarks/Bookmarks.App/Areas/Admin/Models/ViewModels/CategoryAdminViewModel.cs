namespace Bookmarks.App.Areas.Admin.Models.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    using Bookmarks.Common.Mappings;
    using Bookmarks.Models;

    public class CategoryAdminViewModel : IMapFrom<Category>, IMapTo<Category>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}