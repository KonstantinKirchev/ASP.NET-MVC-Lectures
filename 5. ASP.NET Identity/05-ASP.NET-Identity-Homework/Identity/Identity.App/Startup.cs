using Microsoft.Owin;

[assembly: OwinStartup(typeof(Identity.App.Startup))]

namespace Identity.App
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