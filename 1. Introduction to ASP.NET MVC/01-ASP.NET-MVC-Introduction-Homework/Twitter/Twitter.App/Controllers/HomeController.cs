namespace Twitter.App.Controllers
{
    using System.Web.Mvc;

    using Twitter.Data.Contracts;

    public class HomeController : BaseController
    {
        public HomeController()
        {
        }

        public HomeController(ITwitterData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult About()
        {
            this.ViewBag.Message = "Your application description page.";

            return this.View();
        }

        public ActionResult Contact()
        {
            this.ViewBag.Message = "Your contact page.";

            return this.View();
        }
    }
}