namespace Twitter.App.Hubs
{
    using System.Collections.Generic;

    using Microsoft.AspNet.SignalR;

    public class TwitterHub : Hub
    {
        public void Tweet(IList<string> followersUserIds, int tweetId)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<TwitterHub>();

            //// Cannot sent to specific users :(
            // hubContext.Clients.Users(new List<string>(followersUserIds)).receiveTweet(tweetId);
            hubContext.Clients.All.receiveTweet(tweetId);
        }

        public void Notification(string userId)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<TwitterHub>();

            hubContext.Clients.User(userId).receiveNotification();
        }
    }
}