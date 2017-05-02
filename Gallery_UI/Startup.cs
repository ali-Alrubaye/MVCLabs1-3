using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Gallery_UI.Startup))]
namespace Gallery_UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
