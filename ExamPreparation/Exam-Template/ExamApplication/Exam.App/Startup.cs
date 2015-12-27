using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Exam.App.Startup))]
namespace Exam.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
