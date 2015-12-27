namespace Identity.App.Controllers
{
    using System.Web.Mvc;

    using Identity.App.Models;

    using Microsoft.AspNet.Identity;

    [Authorize]
    public class UsersController : Controller
    {
        public ActionResult Index()
        {
            var username = this.User.Identity.GetUserName();
            var model = new UserViewModel { Username = username };
            return this.View(model);
        }
    }
}