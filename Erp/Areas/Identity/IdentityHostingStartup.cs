using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Erp.Areas.Identity.IdentityHostingStartup))]
namespace Erp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {

            builder.ConfigureServices((context, services) => {
            });
        }
    }
}