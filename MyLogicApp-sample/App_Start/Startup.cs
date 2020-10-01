using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using MyLogicApp_sample.Controllers.WebApi.Auth;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(MyLogicApp_sample.Startup))]
namespace MyLogicApp_sample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Account/Login"),
                LogoutPath = new PathString("/Account/Logout")
            });
            ConfigureOAuth(app);
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            var oAuthAuthorizationServerOptions = new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(9999),
                AllowInsecureHttp = true,
                Provider = new Provider()
            };

            app.UseOAuthAuthorizationServer(oAuthAuthorizationServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
