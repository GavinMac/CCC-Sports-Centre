using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CCCSports.Web.Startup))]
namespace CCCSports.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
