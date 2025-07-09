using ESignerDemoWasmApp.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace ESignerDemoWasmApp.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddSingleton<ApiService>();

            await builder.Build().RunAsync();
        }
    }
}