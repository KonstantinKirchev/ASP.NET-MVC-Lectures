namespace Bookmarks.App.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Bookmarks.App.Infrastructure;
    using Bookmarks.App.Models.ViewModels;
    using Bookmarks.Common;
    using Bookmarks.Data.UnitOfWork;

    using WebGrease;

    public class HomeController : BaseController
    {
        private ICacheService cacheService;

        public HomeController(IBookmarksData data, ICacheService cacheService)
            : base(data)
        {
            this.cacheService = cacheService;
        }

        public ActionResult Index()
        {
            var bookmarks = this.cacheService.Bookmars;

            return View(bookmarks);
        }
    }
}