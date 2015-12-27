namespace VGGLinkedIn.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;

    using VGGLinkedIn.Data;
    using VGGLinkedIn.Models;

    public abstract class BaseController : Controller
    {
        private IVGGLinkedInData data;

        private User userProfile;

        protected BaseController(IVGGLinkedInData data)
        {
            this.Data = data;
        }

        protected BaseController(IVGGLinkedInData data, User userProfile)
            :this(data)
        {
            this.UserProfile = userProfile;
        }

        protected IVGGLinkedInData Data { get; private set; }

        protected User UserProfile { get; private set; }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            if (requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var username = requestContext.HttpContext.User.Identity.Name;
                var user = this.Data.Users.All().FirstOrDefault(x => x.UserName == username);
                this.UserProfile = user;
            }

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}