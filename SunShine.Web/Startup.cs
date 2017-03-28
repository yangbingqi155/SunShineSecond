using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SunShine.Web.Startup))]
namespace SunShine.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
