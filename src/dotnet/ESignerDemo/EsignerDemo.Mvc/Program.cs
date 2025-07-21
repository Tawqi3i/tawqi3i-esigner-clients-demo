using ESignerDemo.Common;

namespace EsignerDemo.Mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            AddServices(builder);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        public static void AddServices(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<ESignerService>();

            builder.Services.AddScoped<ClientApiService>(); // for prerendering

            builder.Services.AddSingleton(s => new Settings
            {
                ClientId = builder.Configuration["ClientId"],
                ClientSecret = builder.Configuration["ClientSecret"],
                ESignerBaseUrl = builder.Configuration["ESignerBaseUrl"],
                RedirectUrl = builder.Configuration["RedirectUrl"],
                SignPageUrl = builder.Configuration["SignPageUrl"],
            });
        }
    }
}
