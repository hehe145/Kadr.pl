using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjektPZ.Startup))]
namespace ProjektPZ
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
