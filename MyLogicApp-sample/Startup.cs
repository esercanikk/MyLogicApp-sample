using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyLogicApp_sample.Startup))]
namespace MyLogicApp_sample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
