using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WS1.Startup))]
namespace WS1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
