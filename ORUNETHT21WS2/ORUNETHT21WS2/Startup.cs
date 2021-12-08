using Microsoft.Owin;
using ORUNETHT21WS2;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace ORUNETHT21WS2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
