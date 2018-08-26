using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Client_Application.Startup))]
namespace Client_Application
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
