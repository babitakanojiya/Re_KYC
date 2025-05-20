using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KMI.FRMWRK.Web.Startup))]
namespace KMI.FRMWRK.Web
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
