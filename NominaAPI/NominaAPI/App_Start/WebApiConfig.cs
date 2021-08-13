using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
//using System.Web.Http.OData.Builder;
////using System.Web.Http.OData.Extensions;
////using Microsoft.AspNet.OData.Builder;
//using Microsoft.AspNet.OData.Extensions;
////using Microsoft.AspNetCore.Cors;
using NominaAPI.Models;

namespace NominaAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Puesto>("Puesto");
            builder.EntitySet<Empleado>("Empleado");
            builder.EntitySet<Departamento>("Departamento");
            builder.EntitySet<Nomina>("Nomina");
            builder.EntitySet<AsientoContable>("AsientoContable");
            builder.EntitySet<Cuenta>("Cuenta");
            builder.EntitySet<TipoNomina>("TipoNomina");
            builder.EntitySet<Transaccion>("Transaccion");
            builder.EntitySet<TipoDeduccion>("TipoDeduccion");
            builder.EntitySet<TipoIngreso>("TipoIngreso");
            builder.EntitySet<NominaResumen>("NominaResumen");
            builder.EntitySet<NominaDetalle>("NominaDetalle");
            builder.Namespace = "Contabilidad";
            builder.EntityType<AsientoContable>().Action("EnviarAsiento").ReturnsFromEntitySet<AsientoContable>("AsientoContable");
            config.SetTimeZoneInfo(TimeZoneInfo.Utc);
            config.MapODataServiceRoute("ODataRoute", null, builder.GetEdmModel());
            config.Select().Expand().Filter().OrderBy().MaxTop(null).Count();
            // Configuración y servicios de API web
            // New code:
            //ODataModelBuilder builder = new ODataConventionModelBuilder();
            //builder.EntitySet<Puesto>("Puesto");
            //builder.EntitySet<Empleado>("Empleado");
            //builder.EntitySet<Departamento>("Departamento");
            //builder.EntitySet<Nomina>("Nomina");
            //config.MapODataServiceRoute(
            //    routeName: "ODataRoute",
            //    routePrefix: null,
            //    model: builder.GetEdmModel());

            //ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            //builder.EntitySet<Empleado>("Empleado");
            //builder.EntitySet<Departamento>("Departamento");
            //builder.EntitySet<Nomina>("Nomina");
            //builder.EntitySet<Puesto>("Puesto");
            //config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
            // Rutas de API web
            var cors = new EnableCorsAttribute(
            "*",
            "*",
            "*",
            "DataServiceVersion, MaxDataServiceVersion"
            );
            config.EnableCors(cors);
            // config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
