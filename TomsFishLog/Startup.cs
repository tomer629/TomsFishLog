using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TomsFishLog.Startup))]
namespace TomsFishLog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
