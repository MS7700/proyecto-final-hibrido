























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
    builder.EntitySet<NominaDetalle>("NominaDetalle");

    builder.EntitySet<NominaResumen>("NominaResumen"); 

    builder.EntitySet<Transaccion>("Transaccion"); 


    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

    */

    public class NominaDetalleController : ODataController
    {
        private Proyecto_Fin_Hibrido2Entities1 db = new Proyecto_Fin_Hibrido2Entities1();

        // GET: odata/NominaDetalle
        [EnableQuery]
        public IQueryable<NominaDetalle> Get()
        {
            return db.NominaDetalle;
        }

        // GET: odata/NominaDetalle(5)
        [EnableQuery]
        public SingleResult<NominaDetalle> Get([FromODataUri] int key)
        {
            return SingleResult.Create(db.NominaDetalle.Where(nominaDetalle => nominaDetalle.id == key));
        }

        // PUT: odata/NominaDetalle(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, NominaDetalle update)
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
                if (!NominaDetalleExists(key))
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

        // POST: odata/NominaDetalle
        public async Task<IHttpActionResult> Post(NominaDetalle nominaDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NominaDetalle.Add(nominaDetalle);

            await db.SaveChangesAsync();


            return Created(nominaDetalle);
        }

        // PATCH: odata/NominaDetalle(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<NominaDetalle> patch)
        {
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            NominaDetalle nominaDetalle = await db.NominaDetalle.FindAsync(key);

            if (nominaDetalle == null)
            {
                return NotFound();
            }

            patch.Patch(nominaDetalle);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NominaDetalleExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(nominaDetalle);
        }

        // DELETE: odata/NominaDetalle(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            NominaDetalle nominaDetalle = await db.NominaDetalle.FindAsync(key);
            if (nominaDetalle == null)
            {
                return NotFound();
            }

            db.NominaDetalle.Remove(nominaDetalle);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }


        // GET: odata/NominaDetalle(5)/NominaResumen
        [EnableQuery]

        public SingleResult<NominaResumen> GetNominaResumen([FromODataUri] int key)

        {

            return SingleResult.Create(db.NominaDetalle.Where(m => m.id == key).Select(m => m.NominaResumen));

        }


        // GET: odata/NominaDetalle(5)/Transaccion
        [EnableQuery]

        public SingleResult<Transaccion> GetTransaccion([FromODataUri] int key)

        {

            return SingleResult.Create(db.NominaDetalle.Where(m => m.id == key).Select(m => m.Transaccion));

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NominaDetalleExists(int key)
        {
            return db.NominaDetalle.Any(e => e.id == key);
        }
    }
}
