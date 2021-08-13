
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData.Routing;
using Microsoft.AspNet.OData;
using System.Threading.Tasks;

using NominaAPI.Models;
using Microsoft.AspNet.OData.Routing;
using System.Data.Entity.Validation;
using System.Text;
using Microsoft.OData.Edm;

namespace NominaAPI.Controllers
{


    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;

    using System.Web.Http.OData.Extensions;


    using NominaAPI.Models;

    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<AsientoContable>("AsientoContable");

    builder.EntitySet<Cuenta>("Cuenta"); 


    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

    */

    public class AsientoContableController : ODataController
    {
        private Proyecto_Fin_Hibrido2Entities1 db = new Proyecto_Fin_Hibrido2Entities1();

        // GET: odata/AsientoContable
        [EnableQuery]
        public IQueryable<AsientoContable> Get()
        {
            return db.AsientoContable;
        }

        // GET: odata/AsientoContable(5)
        [EnableQuery]
        public SingleResult<AsientoContable> Get([FromODataUri] int key)
        {
            return SingleResult.Create(db.AsientoContable.Where(asientoContable => asientoContable.id == key));
        }

        // PUT: odata/AsientoContable(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, AsientoContable update)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != update.id)
            {
                return BadRequest();
            }
            db.Entry(update).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsientoContableExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(update);

        }

        private string BuildJson(AsientoContable asientoContable)
        {

            var date = asientoContable.Fecha;
            var converteddate = date.ToString("o");
            System.Diagnostics.Debug.WriteLine(converteddate);
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;
            //var converteddate = new Date(year, month, day).ToString();

            StringBuilder datajson = new StringBuilder();
            datajson.Append("{");
            datajson.Append("\"descripcion\":\"" + asientoContable.Descripcion + "\",");
            datajson.Append("\"catalogoAuxiliarId\":" + asientoContable.Auxiliar + ",");
            //datajson.Append("\"Fecha\":\"" + year + "-" + month + "-" + day + "T00:00:00\",");
            datajson.Append("\"fecha\":\"" + converteddate + "\",");
            datajson.Append("\"monedasId\":1,");
            datajson.Append("\"transacciones\":[{");

            datajson.Append("\"cuentasContablesId\":" + asientoContable.Cuentadb + ",");
            datajson.Append("\"tipoMovimientoId\":2" + ",");
            datajson.Append("\"monto\":" + asientoContable.Monto + "},{");

            datajson.Append("\"cuentasContablesId\":" + asientoContable.Cuentacr + ",");
            datajson.Append("\"tipoMovimientoId\":1" + ",");
            datajson.Append("\"monto\":" + asientoContable.Monto + "}]}");

            System.Diagnostics.Debug.WriteLine(datajson.ToString());

            return datajson.ToString();
        }


        [HttpPost]
        public async Task<IHttpActionResult> EnviarAsiento([FromODataUri] int key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AsientoContable asientoContable = await db.AsientoContable.FindAsync(key);

            if (asientoContable.Contabilizado)
            {
                return BadRequest("El asiento ya está contabilizado");
            }
            //Unir con API contabilidad
            string json = BuildJson(asientoContable);
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://contabilidad2021.azurewebsites.net/");

                // serialize your json using newtonsoft json serializer then add it to the StringContent
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // method address would be like api/callUber:SomePort for example
                var result = await client.PostAsync("api/Asientos", content);
                string resultContent = await result.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine(result.StatusCode);
                System.Diagnostics.Debug.WriteLine(resultContent);

                if (result.StatusCode.ToString() == "200")
                {
                    //Si es existoso, cambiar Contabilizado a true
                    asientoContable.Contabilizado = true;
                    await db.SaveChangesAsync();
                    return Ok(asientoContable);
                }
                else
                {
                    return BadRequest("Ha ocurrido un error en el envío.");
                }
            }
        
            /*
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AsientoContable asientoContable = await db.AsientoContable.FindAsync(key);
            if (asientoContable.Contabilizado)
            {
                return BadRequest("El asiento ya está contabilizado");
            }
            //Unir con API contabilidad


            //Si es existoso, cambiar Contabilizado a true
            asientoContable.Contabilizado = true;
            await db.SaveChangesAsync();


            return Ok(asientoContable);
            */
        }


        /*
        [HttpPost]
        [ODataRoute("EnviarAsiento")]
        public async Task<IHttpActionResult> EnviarAsiento(AsientoContable asientoContable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AsientoContable.Add(asientoContable);

            await db.SaveChangesAsync();


            return Created(asientoContable);
        }
        */
        // POST: odata/AsientoContable
        public async Task<IHttpActionResult> Post(AsientoContable asientoContable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            asientoContable.Estado = "R";
            asientoContable.Auxiliar = 2;
            asientoContable.Cuentacr = 70;
            asientoContable.Cuentadb = 71;
            

            foreach (var nomina in db.Nomina.Where(a => a.Contabilizado == false && a.Fecha <= asientoContable.Fecha).ToList()) {

                foreach (var nominaResumen in db.NominaResumen.Where(a => a.NominaID == nomina.id)) {

                    asientoContable.Monto += nominaResumen.SueldoDevengado;
                }
                nomina.Contabilizado = true;

            }

            db.AsientoContable.Add(asientoContable);
           // await db.SaveChangesAsync();
             try
               {
                   await db.SaveChangesAsync();
               }
               catch (DbEntityValidationException ex)
               {
                   foreach (var entityValidationErrors in ex.EntityValidationErrors)
                   {
                       foreach (var validationError in entityValidationErrors.ValidationErrors)
                       {
                           System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                       }
                   }
               }


            return Created(asientoContable);
        }

        // PATCH: odata/AsientoContable(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<AsientoContable> patch)
        {
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AsientoContable asientoContable = await db.AsientoContable.FindAsync(key);

            if (asientoContable == null)
            {
                return NotFound();
            }

            patch.Patch(asientoContable);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsientoContableExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(asientoContable);
        }

        // DELETE: odata/AsientoContable(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            AsientoContable asientoContable = await db.AsientoContable.FindAsync(key);
            if (asientoContable == null)
            {
                return NotFound();
            }

            foreach (var e in db.Nomina.Where(a => a.Periodo.Substring(0,5) == asientoContable.Descripcion.Substring(0,5))) {
                e.Contabilizado = false;
            }

            db.AsientoContable.Remove(asientoContable);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }


        // GET: odata/AsientoContable(5)/Cuenta
        [EnableQuery]

        public SingleResult<Cuenta> GetCuenta([FromODataUri] int key)

        {

            return SingleResult.Create(db.AsientoContable.Where(m => m.id == key).Select(m => m.Cuenta));

        }


        // GET: odata/AsientoContable(5)/Cuenta1
        [EnableQuery]

        public SingleResult<Cuenta> GetCuenta1([FromODataUri] int key)

        {

            return SingleResult.Create(db.AsientoContable.Where(m => m.id == key).Select(m => m.Cuenta1));

        }

        [EnableQuery]

        public IQueryable<Nomina> GetNomina([FromODataUri] int key)

        {

            return db.AsientoContable.Where(m => m.id == key).SelectMany(m => m.Nomina);

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AsientoContableExists(int key)
        {
            return db.AsientoContable.Any(e => e.id == key);
        }
    }
}
