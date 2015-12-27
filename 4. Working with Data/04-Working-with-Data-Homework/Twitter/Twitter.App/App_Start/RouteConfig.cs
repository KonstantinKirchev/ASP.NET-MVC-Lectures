namespace Twitter.App
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Users", "{username}", new { controller = "Tweets", action = "Index" });

            routes.MapRoute("Tweets", "{username}/Tweets/{action}", new { controller = "Tweets", action = "Index" });

            routes.MapRoute("Friends", "{username}/Users/{action}", new { controller = "Users", action = "Index" });

            routes.MapRoute(
                "Notifications",
                "{username}/Notifications/{action}/{id}",
                new { controller = "Notifications", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}