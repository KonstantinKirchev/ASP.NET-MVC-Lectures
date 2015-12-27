namespace Bookmarks.App.Controllers
{
    using System.Web.Mvc;
    using Bookmarks.Data.UnitOfWork;

    [Authorize(Roles = "Admin")]
    public abstract class AdminController : BaseController
    {
        protected AdminController(IBookmarksData data)
            : base(data)
        {
        }
        
    }
}