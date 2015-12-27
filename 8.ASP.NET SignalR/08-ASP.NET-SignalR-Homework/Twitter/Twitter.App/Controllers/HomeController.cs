namespace Twitter.App.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using PagedList;

    using Twitter.App.Models.ViewModels;
    using Twitter.App.Utilities;
    using Twitter.Data.Contracts;

    public class HomeController : BaseController
    {
        public HomeController(ITwitterData data)
            : base(data)
        {
        }

        // ALL TWEETS
        [AllowAnonymous]
        public ActionResult Index(int? page)
        {
            bool currentUserIsLogged = System.Web.HttpContext.Current.User != null
                                       && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;

            IQueryable<TweetViewModel> tweets;

            if (currentUserIsLogged)
            {
                string currentUserName = System.Web.HttpContext.Current.User.Identity.GetUserName();

                tweets =
                    this.Data.Tweets.GetAll()
                        .Where(t => t.Author.Following.Any(u => u.UserName == currentUserName))
                        .OrderByDescending(t => t.Date)
                        .Select(TweetViewModel.Create());
            }
            else
            {
                tweets = this.Data.Tweets.GetAll().OrderByDescending(t => t.Date).Select(TweetViewModel.Create());
            }

            int pageSize = DefaultValues.DefaultPageSize;
            int pageNumber = page ?? DefaultValues.DefaultStartPage;

            return this.View(tweets.ToPagedList(pageNumber, pageSize));
        }
    }
}