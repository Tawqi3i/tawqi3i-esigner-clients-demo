using ESignerDemoWasmApp.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace ESignerDemoWasmApp.Client
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddSingleton<ApiService>();

            Console.WriteLine("ESignerDemoWasmApp.Client:" + builder.HostEnvironment.BaseAddress);

            await builder.Build().RunAsync();
        }
    }
}