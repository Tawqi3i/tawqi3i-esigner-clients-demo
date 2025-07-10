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

            Console.WriteLine("ESignerDemoWasmApp.Client:" + builder.HostEnvironment.BaseAddress);

            await builder.Build().RunAsync();
        }
    }
}