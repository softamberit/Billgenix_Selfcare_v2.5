using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BillingERPSelfCare.Startup))]
namespace BillingERPSelfCare
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
           // app.MapSignalR();
        }
    }
}
