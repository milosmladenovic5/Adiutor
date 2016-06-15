using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcIng.Startup))]
namespace MvcIng
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
