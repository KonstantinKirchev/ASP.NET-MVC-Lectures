namespace VGGLinkedIn.Web.Controllers
{
    using VGGLinkedIn.Data;
    using System.Web.Mvc;

    using System.Web.Mvc.Expressions;

    public class HomeController : BaseController
    {
        public HomeController(IVGGLinkedInData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            if (this.UserProfile != null)
            {
                this.ViewBag.Username = this.UserProfile.UserName;
            }
            
            return View();
        }

        public ActionResult About()
        {
            //return this.RedirectToAction(x => x.Contact());
            return this.View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}