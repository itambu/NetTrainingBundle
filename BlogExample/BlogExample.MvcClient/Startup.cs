using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlogExample.MvcClient.Startup))]
namespace BlogExample.MvcClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
