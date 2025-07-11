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

            builder.Services.AddSingleton(s =>
            {
                var clientId = builder.Configuration["ClientId"];
                var clientSecret = builder.Configuration["ClientSecret"];
                var baseUrl = builder.Configuration["ESignerBaseUrl"];

                return new Settings
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret,
                    ESignerBaseUrl = baseUrl
                };
            });

            await builder.Build().RunAsync();
        }
    }
}
