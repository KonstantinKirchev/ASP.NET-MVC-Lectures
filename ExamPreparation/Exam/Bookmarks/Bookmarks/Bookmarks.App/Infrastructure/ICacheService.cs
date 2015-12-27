namespace Bookmarks.App.Infrastructure
{
    using System.Collections.Generic;

    using Bookmarks.App.Models.ViewModels;

    public interface ICacheService
    {
        IList<BookmarkViewModel> Bookmars { get; }
    }
}
