using ESignerDemoWasmApp.Client.Dto;
using ESignerDemoWasmApp.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace ESignerDemoWasmApp.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddSingleton<ESignerService>();

            builder.Services.AddSingleton(s =>
            {
                var clientId = builder.Configuration["ClientId"];
                var clientSecret = builder.Configuration["ClientSecret"];

                return new Settings
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret,
                };
            });

            await builder.Build().RunAsync();
        }
    }
}