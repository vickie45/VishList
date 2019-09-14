using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VishList.Startup))]
namespace VishList
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
