























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
    builder.EntitySet<Puesto>("Puesto");

    builder.EntitySet<Empleado>("Empleado"); 


    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

    */

    public class PuestoController : ODataController
    {
        private Proyecto_Fin_Hibrido2Entities1 db = new Proyecto_Fin_Hibrido2Entities1();

        // GET: odata/Puesto
        [EnableQuery]
        public IQueryable<Puesto> Get()
        {
            return db.Puesto;
        }

        // GET: odata/Puesto(5)
        [EnableQuery]
        public SingleResult<Puesto> Get([FromODataUri] int key)
        {
            return SingleResult.Create(db.Puesto.Where(puesto => puesto.id == key));
        }

        // PUT: odata/Puesto(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Puesto update)
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
                if (!PuestoExists(key))
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

        // POST: odata/Puesto
        public async Task<IHttpActionResult> Post(Puesto puesto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Puesto.Add(puesto);

            await db.SaveChangesAsync();


            return Created(puesto);
        }

        // PATCH: odata/Puesto(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Puesto> patch)
        {
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Puesto puesto = await db.Puesto.FindAsync(key);

            if (puesto == null)
            {
                return NotFound();
            }

            patch.Patch(puesto);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PuestoExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(puesto);
        }

        // DELETE: odata/Puesto(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Puesto puesto = await db.Puesto.FindAsync(key);
            if (puesto == null)
            {
                return NotFound();
            }

            db.Puesto.Remove(puesto);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }


        // GET: odata/Puesto(5)/Empleado
        [EnableQuery]

        public IQueryable<Empleado> GetEmpleado([FromODataUri] int key)

        {

            return db.Puesto.Where(m => m.id == key).SelectMany(m => m.Empleado);

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PuestoExists(int key)
        {
            return db.Puesto.Any(e => e.id == key);
        }
    }
}
