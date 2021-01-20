using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using System;

namespace BlazorApp.Client.Services
{
    public class OidcService
    {
        private IConfiguration configuration;
        private IWebAssemblyHostEnvironment environment;
        private NavigationManager navigation;

        public OidcService(IConfiguration configuration, IWebAssemblyHostEnvironment environment, NavigationManager navigation)
        {
            this.configuration = configuration.GetSection("AzureB2C");
            this.environment = environment;
            this.navigation = navigation;
        }

        public void SigninAndSignup() => GreateUrl(configuration["SigninAndSignup"], "authentication/login-callback");

        public void Signin() => GreateUrl(configuration["Signin"], "authentication/login-callback");

        public void Signup() => GreateUrl(configuration["Signup"], "");

        public void PasswordReset() => GreateUrl(configuration["PasswordReset"], "");

        public void ProfileEditing() => GreateUrl(configuration["ProfileEditing"], "");

        private void GreateUrl(string userFlow, string redirect)
        {
            Console.WriteLine(userFlow);

            var clientId = configuration["ClientId"];
            var domain = configuration["Domain"];

            var baseUrl = $"https://{domain}.b2clogin.com/{domain}.onmicrosoft.com/oauth2/v2.0/authorize";
            redirect = $"{environment.BaseAddress}{redirect}";

            var url = $"{baseUrl}?client_id={clientId}&redirect_uri={redirect}&response_mode=query&response_type=id_token&scope=openid&nonce={Guid.NewGuid()}&state=12345&p={userFlow}";

            navigation.NavigateTo(url);
        }
    }
}