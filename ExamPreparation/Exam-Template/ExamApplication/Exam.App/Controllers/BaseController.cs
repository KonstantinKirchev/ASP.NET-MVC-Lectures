namespace Exam.App.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Routing;
    using Exam.Data.UnitOfWork;
    using Exam.Models;
    using System.Web.Mvc;

    public class BaseController : Controller
    {
        public BaseController(IExamData data)
        {
            this.Data = data;
        }

        public BaseController(IExamData data, User user)
            : this(data)
        {
            this.UserProfile = user;
        }

        public IExamData Data { get; private set; }

        public User UserProfile { get; set; }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            if (requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var username = requestContext.HttpContext.User.Identity.Name;
                var user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);
                this.UserProfile = user;
            }
            return base.BeginExecute(requestContext, callback, state);
        }
    }
}