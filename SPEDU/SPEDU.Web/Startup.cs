using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SPEDU.Web.Startup))]
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Web.config", Watch = true)]
namespace SPEDU.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
