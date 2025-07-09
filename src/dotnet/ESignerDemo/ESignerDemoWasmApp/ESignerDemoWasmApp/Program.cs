using ESignerDemoWasmApp.Components;
using ESignerDemoWasmApp.Model;
using System.Text.Json;
using System.Text.Json.Serialization;
using ESignerDemoWasmApp.Services;

namespace ESignerDemoWasmApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveWebAssemblyComponents();

            builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase))); ;

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapControllers();

            app.MapRazorComponents<App>()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

            app.Run();
        }

        public static void AddServices(WebApplicationBuilder builder)
        {
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
        }
    }
}
