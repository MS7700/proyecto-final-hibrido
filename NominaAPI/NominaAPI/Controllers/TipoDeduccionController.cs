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
    Puede que la clase WebApiConfig requiera cambios adicionales para agregar una ruta para este controlador. Combine estas instrucciones en el método Register de la clase WebApiConfig según corresponda. Tenga en cuenta que las direcciones URL de OData distinguen mayúsculas de minúsculas.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using NominaAPI.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<TipoDeduccion>("TipoDeduccion");
    builder.EntitySet<Transaccion>("Transaccion"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class TipoDeduccionController : ODataController
    {
        private Proyecto_Fin_Hibrido2Entities1 db = new Proyecto_Fin_Hibrido2Entities1();

        // GET: odata/TipoDeduccion
        [EnableQuery]
        public IQueryable<TipoDeduccion> Get()
        {
            return db.TipoDeduccion;
        }

        // GET: odata/TipoDeduccion(5)
        [EnableQuery]
        public SingleResult<TipoDeduccion> Get([FromODataUri] int key)
        {
            return SingleResult.Create(db.TipoDeduccion.Where(tipoDeduccion => tipoDeduccion.id == key));
        }

        // PUT: odata/TipoDeduccion(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, TipoDeduccion update)
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
                if (!TipoDeduccionExists(key))
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

        // POST: odata/TipoDeduccion
        public async Task<IHttpActionResult> Post(TipoDeduccion tipoDeduccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TipoDeduccion.Add(tipoDeduccion);
            await db.SaveChangesAsync();

            return Created(tipoDeduccion);
        }

        // PATCH: odata/TipoDeduccion(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<TipoDeduccion> patch)
        {
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TipoDeduccion tipoDeduccion = await db.TipoDeduccion.FindAsync(key);

            if (tipoDeduccion == null)
            {
                return NotFound();
            }

            patch.Patch(tipoDeduccion);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoDeduccionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(tipoDeduccion);
        }

        // DELETE: odata/TipoDeduccion(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            TipoDeduccion tipoDeduccion = await db.TipoDeduccion.FindAsync(key);
            if (tipoDeduccion == null)
            {
                return NotFound();
            }

            db.TipoDeduccion.Remove(tipoDeduccion);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/TipoDeduccion(5)/Transaccion
        [EnableQuery]
        public IQueryable<Transaccion> GetTransaccion([FromODataUri] int key)
        {
            return db.TipoDeduccion.Where(m => m.id == key).SelectMany(m => m.Transaccion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipoDeduccionExists(int key)
        {
            return db.TipoDeduccion.Any(e => e.id == key);
        }
    }
}
