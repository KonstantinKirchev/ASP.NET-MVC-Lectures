using Microsoft.Owin;

[assembly: OwinStartup(typeof(Twitter.App.Startup))]

namespace Twitter.App
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            this.ConfigureAuth(app);
        }
    }
}