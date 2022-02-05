using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_1262228_Arosh.Startup))]
namespace _1262228_Arosh
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
