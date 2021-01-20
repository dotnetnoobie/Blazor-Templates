using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using System;

namespace BlazorOIDCB2C.Services
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

        public void SigninAndSignup() => GreateUrl(configuration["SigninAndSignUp"], "authentication/login-callback");

        public void Signin() => GreateUrl(configuration["SignIn"], "authentication/login-callback");

        public void Signup() => GreateUrl(configuration["SignUp"], "");

        public void PasswordReset() => GreateUrl(configuration["PasswordReset"], "");

        public void ProfileEditing() => GreateUrl(configuration["ProfileEditing"], "");

        private void GreateUrl(string userFlow, string redirect)
        {
            //Console.WriteLine(userFlow);

            //var clientId = configuration["ClientId"];
            //var domain = configuration["Domain"];

            //var baseUrl = $"https://{domain}.b2clogin.com/{domain}.onmicrosoft.com/oauth2/v2.0/authorize";
            //redirect = $"{environment.BaseAddress}{redirect}";

            //var url = $"{baseUrl}?client_id={clientId}&redirect_uri={redirect}&response_mode=query&response_type=id_token&scope=openid&nonce={Guid.NewGuid()}&state=12345&p={userFlow}";

            var url = $"https://{configuration["Domain"]}.b2clogin.com/{configuration["Domain"]}.onmicrosoft.com/oauth2/v2.0/authorize";

            url += $"?client_id={configuration["ClientId"]}";
            url += $"&redirect_uri={environment.BaseAddress}{redirect}";
            url += $"&response_mode=query";
            url += $"&response_type={configuration["ResponseType"]}";
            url += $"&scope=openid";
            url += $"&nonce={Guid.NewGuid()}";
            url += $"&state=12345";
            url += $"&p={userFlow}";
            url += $"&code_challenge=YTFjNjI1OWYzMzA3MTI4ZDY2Njg5M2RkNmVjNDE5YmEyZGRhOGYyM2IzNjdmZWFhMTQ1ODg3NDcxY2Nl";
            url += $"&code_challenge_method=S256";
            url += $"&prompt=login";

            navigation.NavigateTo(url);
        }
    }
}