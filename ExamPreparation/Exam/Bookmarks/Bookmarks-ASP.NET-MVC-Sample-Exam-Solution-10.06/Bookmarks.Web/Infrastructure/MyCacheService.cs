using Bookmarks.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookmarks.Web.Infrastructure
{
    using AutoMapper.QueryableExtensions;
    using Common;
    using Data;

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
                        .Project()
                        .To<BookmarkViewModel>()
                        .ToList();
                });
            }
        }
    }
}