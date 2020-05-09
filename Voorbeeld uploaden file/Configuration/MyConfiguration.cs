using HelloWorld.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;

namespace HelloWorld.Configuration
{
    class MyConfiguration
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultAPI",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            #region MultipleRoutes
            config.Routes.MapHttpRoute(
                name: "PlaystationAPIv1",
                routeTemplate: "api/v1/",
                defaults: new { controller = "playstation", action = "BombV1" }
                );

            config.Routes.MapHttpRoute(
                name: "PlaystationAPIv2",
                routeTemplate: "api/v2/{action}",
                defaults: new { controller = "playstation" }
                );

            config.Routes.MapHttpRoute(
                name: "PlaystationAPIv3",
                routeTemplate: "api/v3/{controller}",
                defaults: new { controller = "playstation", action = "BombV2" }
                );

            // You may be attempt to do this...
            // it will not work because there is no defined controller.
            config.Routes.MapHttpRoute(
                name: "PlaystationAPIv4",
                routeTemplate: "api/v4/playstation",
                defaults: new { action = "LaunchKimJongUnBomb" }
                );
            #endregion

            #region CharacterV3aModelBinder
            var provider = new SimpleModelBinderProvider(
                typeof(CharacterV3a), new CharacterV3aModelBinder());

            config.Services.Insert(typeof(ModelBinderProvider), 0, provider);
            #endregion 
        }
    }
}
