namespace Bookmarks.App.Controllers
{
    using System.Web.Mvc;

    using Bookmarks.Data.UnitOfWork;

    public abstract class BaseController : Controller
    {
        protected BaseController(IBookmarksData data)
        {
            this.Data = data;
        }

        protected IBookmarksData Data { get; private set; }
    }
}