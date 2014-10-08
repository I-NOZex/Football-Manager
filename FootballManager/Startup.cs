using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FootballManager.Startup))]
namespace FootballManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
