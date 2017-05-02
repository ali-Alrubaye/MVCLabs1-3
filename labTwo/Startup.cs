using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(labTwo.Startup))]
namespace labTwo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
