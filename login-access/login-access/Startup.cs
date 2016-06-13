using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(login_access.Startup))]
namespace login_access
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
