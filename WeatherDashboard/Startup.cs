using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WeatherDashboard.Startup))]
namespace WeatherDashboard
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
