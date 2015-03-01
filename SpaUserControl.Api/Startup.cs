using System.Configuration;
using System.Web.Hosting;
using System.Web.Http;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Owin;
using Owin.Loader;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Host.SystemWeb;
using Owin.Builder;
using SpaUserControl.Api.Helpers;
using SpaUserControl.Startup;

namespace SpaUserControl.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            var container = new UnityContainer();
            DependencyResolver.Resolver(container);
            config.DependencyResolver = new UnityResolver(container);

            ConfigureWebApi(config);

            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public static void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{api}",
                defaults: new {id = RouteParameter.Optional }
                );
        }
    }
}