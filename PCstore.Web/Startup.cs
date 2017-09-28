using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(PCstore.Web.Startup))]

namespace PCstore.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
