using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorApp.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            var baseAddress = builder.Configuration["BaseAddress"] ?? builder.HostEnvironment.BaseAddress;
            builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(baseAddress) });

            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("Google", options.ProviderOptions);

                // options.ProviderOptions.DefaultScopes.Add("https://www.googleapis.com/auth/youtube");
            });

            //builder.Services.AddOidcAuthentication(options =>
            //{
            //    options.ProviderOptions.MetadataUrl = "https://accounts.google.com/.well-known/openid-configuration";
            //    options.ProviderOptions.ResponseType = "id_token token";
            //    options.ProviderOptions.Authority = "https://accounts.google.com";
            //    options.ProviderOptions.PostLogoutRedirectUri = $"{builder.HostEnvironment.BaseAddress}/authentication/logout-callback";
            //    options.ProviderOptions.ClientId = "10126427864-o1t3qcc9lgdr2lalclhu9h8170ea4dg9.apps.googleusercontent.com";
            //    options.ProviderOptions.DefaultScopes.Add("https://www.googleapis.com/auth/youtube");
            //});

            await builder.Build().RunAsync();
        }
    }
}