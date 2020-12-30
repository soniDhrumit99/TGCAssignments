using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Security.Startup))]
namespace Security
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
