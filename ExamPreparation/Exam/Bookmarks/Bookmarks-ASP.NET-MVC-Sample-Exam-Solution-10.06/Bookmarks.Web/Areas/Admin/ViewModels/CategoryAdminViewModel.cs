namespace Bookmarks.Web.Areas.Admin.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using Common.Mappings;
    using Bookmarks.Models;

    public class CategoryAdminViewModel : IMapFrom<Category>, IMapTo<Category>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}