using System.Configuration;
using Cvsite;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace Cvsite {
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
            var settingvalue = ConfigurationManager.AppSettings["MySetting"];
        }
    }
}
