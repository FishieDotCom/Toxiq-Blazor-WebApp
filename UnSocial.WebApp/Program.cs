using CurrieTechnologies.Razor.Clipboard;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;
using Unsocial.WebApp.Services;
using UnSocial.WebApp.Helpers;
using UnSocial.WebApp.Services;

namespace UnSocial.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Toxiq WEB_APP";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddScoped<AppState>();
            builder.Services.AddMasaBlazor(options =>
            {
                options.ConfigureTheme(theme =>
                {
                    theme.Dark = true;
                });
            });
            builder.Services.AddFluentUIComponents();
            builder.Services.AddClipboard();
            builder.Services.AddHttpClient();

            builder.Services.AddFluentUIComponents();


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

            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value?.Equals("/.well-known/apple-app-site-association", StringComparison.OrdinalIgnoreCase) == true)
                {
                    var filePath = Path.Combine(app.Environment.WebRootPath, ".well-known", "apple-app-site-association");

                    if (File.Exists(filePath))
                    {
                        context.Response.ContentType = "application/json";
                        await context.Response.SendFileAsync(filePath);
                        return;
                    }
                    else
                    {
                        // If the file is missing, return a 404
                        context.Response.StatusCode = StatusCodes.Status404NotFound;
                        return;
                    }
                }
                await next();
            });


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

#if DEBUG
            //   app.Run("http://10.0.10.3:9454");
            app.Run();
#else
            app.Run();
#endif
        }
    }
}
