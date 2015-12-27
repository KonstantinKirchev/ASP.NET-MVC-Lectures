namespace Twitter.App.Models.ViewModels
{
    using System;
    using System.Linq.Expressions;

    using Twitter.Models;
    using Twitter.Models.Enumerations;

    public class NotificationViewModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Content { get; set; }

        public NotificationType Type { get; set; }

        public static Expression<Func<Notification, NotificationViewModel>> Create()
        {
            return
                n =>
                new NotificationViewModel
                {
                    Id = n.Id, Date = n.Date,
                    Content = n.Content,
                    Type = n.NotificationType
                };
        }
    }
}