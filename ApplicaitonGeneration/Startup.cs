using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ApplicationGeneration.Startup))]
namespace ApplicationGeneration
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
