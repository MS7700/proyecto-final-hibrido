using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using NominaAPI.Models;

namespace NominaAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web
            // New code:
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Empleado>("Empleado");
            builder.EntitySet<Departamento>("Departamento");
            builder.EntitySet<Nomina>("Nomina");
            builder.EntitySet<Puesto>("Puesto");
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
