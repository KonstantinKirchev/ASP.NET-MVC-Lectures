using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bookmarks.App.Startup))]
namespace Bookmarks.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
