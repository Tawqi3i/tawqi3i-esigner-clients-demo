using ESignerDemo.Common;

namespace ESignerDemo.Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            AddServices(builder);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy => { policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); });
            });

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }

        public static void AddServices(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<ESignerService>();

            builder.Services.AddScoped<ClientApiService>(); // for prerendering

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
        }
    }
}
