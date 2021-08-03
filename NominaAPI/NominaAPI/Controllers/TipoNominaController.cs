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
    builder.EntitySet<TipoNomina>("TipoNomina");

    builder.EntitySet<Empleado>("Empleado"); 


    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

    */

    public class TipoNominaController : ODataController
    {
        private Proyecto_Fin_Hibrido2Entities1 db = new Proyecto_Fin_Hibrido2Entities1();

        // GET: odata/TipoNomina
        [EnableQuery]
        public IQueryable<TipoNomina> Get()
        {
            return db.TipoNomina;
        }

        // GET: odata/TipoNomina(5)
        [EnableQuery]
        public SingleResult<TipoNomina> Get([FromODataUri] int key)
        {
            return SingleResult.Create(db.TipoNomina.Where(tipoNomina => tipoNomina.id == key));
        }

        // PUT: odata/TipoNomina(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, TipoNomina update)
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
                if (!TipoNominaExists(key))
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

        // POST: odata/TipoNomina
        public async Task<IHttpActionResult> Post(TipoNomina tipoNomina)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TipoNomina.Add(tipoNomina);


            try
            {

                db.SaveChanges();

            }
            catch (DbUpdateException)
            {
                if (TipoNominaExists(tipoNomina.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }


            return Created(tipoNomina);
        }

        // PATCH: odata/TipoNomina(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<TipoNomina> patch)
        {
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TipoNomina tipoNomina = await db.TipoNomina.FindAsync(key);

            if (tipoNomina == null)
            {
                return NotFound();
            }

            patch.Patch(tipoNomina);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoNominaExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(tipoNomina);
        }

        // DELETE: odata/TipoNomina(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            TipoNomina tipoNomina = await db.TipoNomina.FindAsync(key);
            if (tipoNomina == null)
            {
                return NotFound();
            }

            db.TipoNomina.Remove(tipoNomina);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }


        // GET: odata/TipoNomina(5)/Empleado
        [EnableQuery]

        public SingleResult<Empleado> GetEmpleado([FromODataUri] int key)

        {

            return SingleResult.Create(db.TipoNomina.Where(m => m.id == key).Select(m => m.Empleado));

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipoNominaExists(int key)
        {
            return db.TipoNomina.Any(e => e.id == key);
        }
    }
}
