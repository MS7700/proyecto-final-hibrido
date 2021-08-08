























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


namespace NominaAPI.Controllers
{


    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;

    using System.Web.Http.OData.Extensions;


    using NominaAPI.Models;

    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Cuenta>("Cuenta");

    builder.EntitySet<AsientoContable>("AsientoContable"); 


    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

    */

    public class CuentaController : ODataController
    {
        private Proyecto_Fin_Hibrido2Entities1 db = new Proyecto_Fin_Hibrido2Entities1();

        // GET: odata/Cuenta
        [EnableQuery]
        public IQueryable<Cuenta> Get()
        {
            return db.Cuenta;
        }

        // GET: odata/Cuenta(5)
        [EnableQuery]
        public SingleResult<Cuenta> Get([FromODataUri] int key)
        {
            return SingleResult.Create(db.Cuenta.Where(cuenta => cuenta.id == key));
        }

        // PUT: odata/Cuenta(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Cuenta update)
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
                if (!CuentaExists(key))
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

        // POST: odata/Cuenta
        public async Task<IHttpActionResult> Post(Cuenta cuenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cuenta.Add(cuenta);

            await db.SaveChangesAsync();


            return Created(cuenta);
        }

        // PATCH: odata/Cuenta(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Cuenta> patch)
        {
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Cuenta cuenta = await db.Cuenta.FindAsync(key);

            if (cuenta == null)
            {
                return NotFound();
            }

            patch.Patch(cuenta);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuentaExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(cuenta);
        }

        // DELETE: odata/Cuenta(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Cuenta cuenta = await db.Cuenta.FindAsync(key);
            if (cuenta == null)
            {
                return NotFound();
            }

            db.Cuenta.Remove(cuenta);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }


        // GET: odata/Cuenta(5)/AsientoContable
        [EnableQuery]

        public IQueryable<AsientoContable> GetAsientoContable([FromODataUri] int key)

        {

            return db.Cuenta.Where(m => m.id == key).SelectMany(m => m.AsientoContable);

        }


        // GET: odata/Cuenta(5)/AsientoContable1
        [EnableQuery]

        public IQueryable<AsientoContable> GetAsientoContable1([FromODataUri] int key)

        {

            return db.Cuenta.Where(m => m.id == key).SelectMany(m => m.AsientoContable1);

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CuentaExists(int key)
        {
            return db.Cuenta.Any(e => e.id == key);
        }
    }
}
