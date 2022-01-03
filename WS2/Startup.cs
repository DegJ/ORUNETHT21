using Data;
using Microsoft.Owin;
using Owin;
using WS2;

[assembly: OwinStartup(typeof(Startup))]
namespace WS2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.CreatePerOwinContext(() => new ApplicationDbContext());
        }
    }
}
