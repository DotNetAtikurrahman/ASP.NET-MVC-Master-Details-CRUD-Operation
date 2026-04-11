using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MollahThaiGlassHouse.Startup))]
namespace MollahThaiGlassHouse
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
