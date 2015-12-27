namespace Identity.App.Controllers
{
    using System.Web.Mvc;

    using Identity.App.Models;

    using Microsoft.AspNet.Identity;

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            var username = this.User.Identity.GetUserName();
            var model = new AdminViewModel { Username = username };
            return this.View(model);
        }
    }
}