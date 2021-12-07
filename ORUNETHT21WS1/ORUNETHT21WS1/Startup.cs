using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ORUNETHT21WS1.Startup))]
namespace ORUNETHT21WS1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
