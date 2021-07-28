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
    builder.EntitySet<Nomina>("Nomina");
    builder.EntitySet<Empleado>("Empleado"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class NominaController : ODataController
    {
        private Proyecto_Fin_Hibrido2Entities1 db = new Proyecto_Fin_Hibrido2Entities1();

        // GET: odata/Nomina
        [EnableQuery]
        public IQueryable<Nomina> Get()
        {
            return db.Nomina;
        }

        // GET: odata/Nomina(5)
        [EnableQuery]
        public SingleResult<Nomina> Get([FromODataUri] int key)
        {
            return SingleResult.Create(db.Nomina.Where(nomina => nomina.id == key));
        }

        // PUT: odata/Nomina(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Nomina update)
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
                if (!NominaExists(key))
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

        // POST: odata/Nomina
        public async Task<IHttpActionResult> Post(Nomina nomina)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Nomina.Add(nomina);
            await db.SaveChangesAsync();

            return Created(nomina);
        }

        // PATCH: odata/Nomina(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Nomina> patch)
        {
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Nomina nomina = await db.Nomina.FindAsync(key);

            if (nomina == null)
            {
                return NotFound();
            }

            patch.Patch(nomina);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NominaExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(nomina);
        }

        // DELETE: odata/Nomina(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Nomina nomina = await db.Nomina.FindAsync(key);
            if (nomina == null)
            {
                return NotFound();
            }

            db.Nomina.Remove(nomina);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Nomina(5)/Empleado
        [EnableQuery]
        public IQueryable<Empleado> GetEmpleado([FromODataUri] int key)
        {
            return db.Nomina.Where(m => m.id == key).SelectMany(m => m.Empleado);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NominaExists(int key)
        {
            return db.Nomina.Any(e => e.id == key);
        }
    }
}
