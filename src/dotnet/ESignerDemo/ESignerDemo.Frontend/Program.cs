using ESignerDemo.Common;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace ESignerDemo.Frontend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddSingleton<ClientApiService>();

            builder.Services.AddSingleton(s => new Settings
            {
                BackendBaseUrl = builder.Configuration["BackendBaseUrl"],
                RedirectUrl = builder.Configuration["RedirectUrl"],
            });

            await builder.Build().RunAsync();
        }
    }
}
