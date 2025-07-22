using ESignerDemo.Common;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace ESignerDemoWasmApp.Client
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddSingleton<ClientApiService>();

            builder.Services.AddSingleton(s => new Settings
            {
                BackendBaseUrl = builder.Configuration["BackendBaseUrl"],
                RedirectUrl = builder.Configuration["RedirectUrl"],
            });

            Console.WriteLine("ESignerDemoWasmApp.Client:" + builder.HostEnvironment.BaseAddress);

            builder.Services.AddBlazorPdfViewer();

            await builder.Build().RunAsync();
        }
    }
}