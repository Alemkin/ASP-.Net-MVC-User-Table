using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FirstDemoProjectForRPbyAL.Startup))]
namespace FirstDemoProjectForRPbyAL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
