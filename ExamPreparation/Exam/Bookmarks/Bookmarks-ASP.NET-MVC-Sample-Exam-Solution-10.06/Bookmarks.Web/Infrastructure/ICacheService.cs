using Bookmarks.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookmarks.Web.Infrastructure
{
    public interface ICacheService
    {
        IList<BookmarkViewModel> Bookmars { get; }
    }
}