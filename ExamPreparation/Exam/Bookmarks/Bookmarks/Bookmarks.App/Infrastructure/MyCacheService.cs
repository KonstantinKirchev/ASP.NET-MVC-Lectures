namespace Bookmarks.App.Infrastructure
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper.QueryableExtensions;

    using Bookmarks.App.Models.ViewModels;
    using Bookmarks.Common;
    using Bookmarks.Data.UnitOfWork;

    public class MyCacheService : BaseCacheService, ICacheService
    {
        private IBookmarksData data;

        public MyCacheService(IBookmarksData data)
        {
            this.data = data;
        }

        public IList<BookmarkViewModel> Bookmars
        {
            get
            {
                return this.Get<IList<BookmarkViewModel>>("homePageBookmars", () =>
                {
                    return this.data.Bookmarks
                        .All()
                        .OrderByDescending(x => x.Votes.Count())
                        .Take(GlobalConstants.HomePageNumberOfBookmarks)
                        .ProjectTo<BookmarkViewModel>()
                        .ToList();
                });
            }
        }
    }
}