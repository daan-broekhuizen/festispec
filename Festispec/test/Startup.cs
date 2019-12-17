using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(test.Startup))]
namespace test
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
