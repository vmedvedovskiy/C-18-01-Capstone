using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(C_18_01_Capstone.Web.Startup))]
namespace C_18_01_Capstone.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
