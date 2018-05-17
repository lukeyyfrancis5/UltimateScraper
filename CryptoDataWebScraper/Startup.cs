using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CryptoDataWebScraper.Startup))]
namespace CryptoDataWebScraper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
