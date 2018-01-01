using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SoftwareGrid.iTestApp.Startup))]
namespace SoftwareGrid.iTestApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
