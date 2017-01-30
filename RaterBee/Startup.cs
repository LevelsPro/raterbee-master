using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RaterBee.Startup))]
namespace RaterBee
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
