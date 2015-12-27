namespace Caching.App.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Caching.Data.UnitOfWork;
    using Caching.Models;

    public class BaseController : Controller
    {
        public BaseController(IApplicationData data)
        {
            this.ApplicationData = data;
        }

        public BaseController(IApplicationData data, User user)
            : this(data)
        {
            this.UserProfile = user;
        }

        public IApplicationData ApplicationData { get; private set; }

        public User UserProfile { get; set; }

        protected override IAsyncResult BeginExecute(
            RequestContext requestContext,
            AsyncCallback callback,
            object state)
        {
            if (requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var username = requestContext.HttpContext.User.Identity.Name;
                var user = this.ApplicationData.Users.All().FirstOrDefault(u => u.UserName == username);
                this.UserProfile = user;
            }

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}