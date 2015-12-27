namespace Twitter.App.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using Twitter.App.Models.ViewModels;
    using Twitter.Data.Contracts;

    public class NotificationsController : BaseController
    {
        public NotificationsController(ITwitterData data)
            : base(data)
        {
        }

        public ActionResult Index(string username)
        {
            var userId = this.User.Identity.GetUserId();
            var notifications =
                this.Data.Notifications.GetAll()
                    .Where(n => n.ReceiverId == userId)
                    .Select(NotificationViewModel.Create());

            return this.View(notifications);
        }

        [Route("{username}/Notifications/View/{id}")]
        public ActionResult View(int id)
        {
            var notification =
                this.Data.Notifications.GetAll()
                    .Where(n => n.Id == id)
                    .Select(NotificationViewModel.Create())
                    .FirstOrDefault();

            return this.View(notification);
        }
    }
}