namespace Twitter.App.Controllers
{
    using System.Web.Mvc;

    using Twitter.Data;
    using Twitter.Data.Contracts;

    public class BaseController : Controller
    {
        public BaseController()
            : this(new TwitterData())
        {
        }

        public BaseController(ITwitterData data)
        {
            this.Data = data;
        }

        protected ITwitterData Data { get; }
    }
}