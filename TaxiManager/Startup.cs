using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TaxiManager.Startup))]
namespace TaxiManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
