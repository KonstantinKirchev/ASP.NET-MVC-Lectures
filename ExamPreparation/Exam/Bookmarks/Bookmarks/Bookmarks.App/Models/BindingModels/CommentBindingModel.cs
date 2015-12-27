namespace Bookmarks.App.Models.BindingModels
{
    using Bookmarks.Common.Mappings;
    using Bookmarks.Models;

    public class CommentBindingModel : IMapTo<Comment>
    {
        public int BookmarkId { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }
    }
}