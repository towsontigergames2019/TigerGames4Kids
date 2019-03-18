using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TigerGames4Kids.Startup))]
namespace TigerGames4Kids
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
