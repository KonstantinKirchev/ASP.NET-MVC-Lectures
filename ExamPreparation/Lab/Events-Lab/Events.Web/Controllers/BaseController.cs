using System.Web.Mvc;
using Events.Data;
using Microsoft.AspNet.Identity;

namespace Events.Web.Controllers
{
    public class BaseController : Controller
    {
        protected ApplicationDbContext db = new ApplicationDbContext();

        public bool isAdmin()
        {
            var currentUserId = User.Identity.GetUserId();
            var isAdmin = (currentUserId != null && User.IsInRole("Administrator"));

            return isAdmin;
        }
    }
}