using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(hoger.Startup))]
namespace hoger

{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
