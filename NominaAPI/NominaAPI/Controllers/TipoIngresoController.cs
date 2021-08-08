























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
    builder.EntitySet<TipoIngreso>("TipoIngreso");

    builder.EntitySet<Transaccion>("Transaccion"); 


    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

    */

    public class TipoIngresoController : ODataController
    {
        private Proyecto_Fin_Hibrido2Entities1 db = new Proyecto_Fin_Hibrido2Entities1();

        // GET: odata/TipoIngreso
        [EnableQuery]
        public IQueryable<TipoIngreso> Get()
        {
            return db.TipoIngreso;
        }

        // GET: odata/TipoIngreso(5)
        [EnableQuery]
        public SingleResult<TipoIngreso> Get([FromODataUri] int key)
        {
            return SingleResult.Create(db.TipoIngreso.Where(tipoIngreso => tipoIngreso.id == key));
        }

        // PUT: odata/TipoIngreso(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, TipoIngreso update)
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
                if (!TipoIngresoExists(key))
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

        // POST: odata/TipoIngreso
        public async Task<IHttpActionResult> Post(TipoIngreso tipoIngreso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TipoIngreso.Add(tipoIngreso);

            await db.SaveChangesAsync();


            return Created(tipoIngreso);
        }

        // PATCH: odata/TipoIngreso(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<TipoIngreso> patch)
        {
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TipoIngreso tipoIngreso = await db.TipoIngreso.FindAsync(key);

            if (tipoIngreso == null)
            {
                return NotFound();
            }

            patch.Patch(tipoIngreso);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoIngresoExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(tipoIngreso);
        }

        // DELETE: odata/TipoIngreso(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            TipoIngreso tipoIngreso = await db.TipoIngreso.FindAsync(key);
            if (tipoIngreso == null)
            {
                return NotFound();
            }

            db.TipoIngreso.Remove(tipoIngreso);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }


        // GET: odata/TipoIngreso(5)/Transaccion
        [EnableQuery]

        public IQueryable<Transaccion> GetTransaccion([FromODataUri] int key)

        {

            return db.TipoIngreso.Where(m => m.id == key).SelectMany(m => m.Transaccion);

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipoIngresoExists(int key)
        {
            return db.TipoIngreso.Any(e => e.id == key);
        }
    }
}
