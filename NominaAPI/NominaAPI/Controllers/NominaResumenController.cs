
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
    builder.EntitySet<NominaResumen>("NominaResumen");

    builder.EntitySet<Empleado>("Empleado"); 

    builder.EntitySet<Nomina>("Nomina"); 

    builder.EntitySet<NominaDetalle>("NominaDetalle"); 


    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

    */

    public class NominaResumenController : ODataController
    {
        private Proyecto_Fin_Hibrido2Entities1 db = new Proyecto_Fin_Hibrido2Entities1();

        // GET: odata/NominaResumen
        [EnableQuery]
        public IQueryable<NominaResumen> Get()
        {
            return db.NominaResumen;
        }

        // GET: odata/NominaResumen(5)
        [EnableQuery]
        public SingleResult<NominaResumen> Get([FromODataUri] int key)
        {
            return SingleResult.Create(db.NominaResumen.Where(nominaResumen => nominaResumen.id == key));
        }

        // PUT: odata/NominaResumen(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, NominaResumen update)
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
                if (!NominaResumenExists(key))
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

        // POST: odata/NominaResumen
        public async Task<IHttpActionResult> Post(NominaResumen nominaResumen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NominaResumen.Add(nominaResumen);

            await db.SaveChangesAsync();


            return Created(nominaResumen);
        }

        // PATCH: odata/NominaResumen(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<NominaResumen> patch)
        {
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            NominaResumen nominaResumen = await db.NominaResumen.FindAsync(key);

            if (nominaResumen == null)
            {
                return NotFound();
            }

            patch.Patch(nominaResumen);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NominaResumenExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(nominaResumen);
        }

        // DELETE: odata/NominaResumen(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            NominaResumen nominaResumen = await db.NominaResumen.FindAsync(key);
            if (nominaResumen == null)
            {
                return NotFound();
            }

            db.NominaResumen.Remove(nominaResumen);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }


        // GET: odata/NominaResumen(5)/Empleado
        [EnableQuery]

        public SingleResult<Empleado> GetEmpleado([FromODataUri] int key)

        {

            return SingleResult.Create(db.NominaResumen.Where(m => m.id == key).Select(m => m.Empleado));

        }


        // GET: odata/NominaResumen(5)/Nomina
        [EnableQuery]

        public SingleResult<Nomina> GetNomina([FromODataUri] int key)

        {

            return SingleResult.Create(db.NominaResumen.Where(m => m.id == key).Select(m => m.Nomina));

        }


        // GET: odata/NominaResumen(5)/NominaDetalle
        [EnableQuery]

        public IQueryable<NominaDetalle> GetNominaDetalle([FromODataUri] int key)

        {

            return db.NominaResumen.Where(m => m.id == key).SelectMany(m => m.NominaDetalle);

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NominaResumenExists(int key)
        {
            return db.NominaResumen.Any(e => e.id == key);
        }
    }
}
