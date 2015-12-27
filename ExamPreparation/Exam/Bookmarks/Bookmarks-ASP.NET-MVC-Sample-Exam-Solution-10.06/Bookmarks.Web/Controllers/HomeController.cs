namespace Bookmarks.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Bookmarks.Web.ViewModels;
    using Common;
    using Infrastructure;

    public class HomeController : BaseController
    {
        private ICacheService cacheService;

        public HomeController(IBookmarksData data, ICacheService cacheService) : base(data)
        {
            this.cacheService = cacheService;
        }

        public ActionResult Index()
        {
            var bookmarks = this.cacheService.Bookmars;
            return this.View(bookmarks);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}