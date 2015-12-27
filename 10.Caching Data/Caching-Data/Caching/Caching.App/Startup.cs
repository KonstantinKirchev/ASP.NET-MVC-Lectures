using Microsoft.Owin;

[assembly: OwinStartup(typeof(Caching.App.Startup))]

namespace Caching.App
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}