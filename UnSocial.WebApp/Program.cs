using Microsoft.JSInterop;
using Unsocial.WebApp.Services;
using UnSocial.WebApp.Data;
using UnSocial.WebApp.Services;

namespace UnSocial.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();

            builder.Services.AddMasaBlazor(options =>
            {
                options.ConfigureTheme(theme =>
                {
                    theme.Dark = true;
                });
            });


            builder.Services.AddScoped<ITelegramJsInterop>(sp =>
            {
                var jsRuntime = sp.GetRequiredService<IJSRuntime>();
                return new TelegramJsInterop(jsRuntime);
            });

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSession();

            builder.Services.AddScoped<TokenService>();
            builder.Services.AddTransient<CustomHttpClientHandler>();
            builder.Services.AddHttpClient("ApiClient").ConfigurePrimaryHttpMessageHandler<CustomHttpClientHandler>();




            builder.Services.AddScoped<IApiService>(sp =>
            {
                var telegramJsInterop = sp.GetRequiredService<ITelegramJsInterop>();
                var http = sp.GetRequiredService<IHttpClientFactory>();
                var token = sp.GetRequiredService<TokenService>();
                return new ApiService(telegramJsInterop, http, token);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}
