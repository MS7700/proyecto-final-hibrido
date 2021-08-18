
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
    builder.EntitySet<Transaccion>("Transaccion");

    builder.EntitySet<Empleado>("Empleado"); 

    builder.EntitySet<NominaDetalle>("NominaDetalle"); 

    builder.EntitySet<TipoDeduccion>("TipoDeduccion"); 

    builder.EntitySet<TipoIngreso>("TipoIngreso"); 


    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

    */

    public class TransaccionController : ODataController
    {
        private Proyecto_Fin_Hibrido2Entities1 db = new Proyecto_Fin_Hibrido2Entities1();

        // GET: odata/Transaccion
        [EnableQuery]
        public IQueryable<Transaccion> Get()
        {
            return db.Transaccion;
        }

        // GET: odata/Transaccion(5)
        [EnableQuery]
        public SingleResult<Transaccion> Get([FromODataUri] int key)
        {
            return SingleResult.Create(db.Transaccion.Where(transaccion => transaccion.id == key));
        }

        // PUT: odata/Transaccion(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Transaccion update)
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
                if (!TransaccionExists(key))
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

        // POST: odata/Transaccion
        public async Task<IHttpActionResult> Post(Transaccion transaccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Transaccion.Add(transaccion);

            await db.SaveChangesAsync();


            return Created(transaccion);
        }

        // PATCH: odata/Transaccion(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Transaccion> patch)
        {
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Transaccion transaccion = await db.Transaccion.FindAsync(key);

            if (transaccion == null)
            {
                return NotFound();
            }

            patch.Patch(transaccion);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransaccionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(transaccion);
        }

        // DELETE: odata/Transaccion(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Transaccion transaccion = await db.Transaccion.FindAsync(key);
            if (transaccion == null)
            {
                return NotFound();
            }

            db.Transaccion.Remove(transaccion);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }


        // GET: odata/Transaccion(5)/Empleado
        [EnableQuery]

        public SingleResult<Empleado> GetEmpleado([FromODataUri] int key)

        {

            return SingleResult.Create(db.Transaccion.Where(m => m.id == key).Select(m => m.Empleado));

        }


        // GET: odata/Transaccion(5)/NominaDetalle
        [EnableQuery]

        public IQueryable<NominaDetalle> GetNominaDetalle([FromODataUri] int key)

        {

            return db.Transaccion.Where(m => m.id == key).SelectMany(m => m.NominaDetalle);

        }


        // GET: odata/Transaccion(5)/TipoDeduccion
        [EnableQuery]

        public SingleResult<TipoDeduccion> GetTipoDeduccion([FromODataUri] int key)

        {

            return SingleResult.Create(db.Transaccion.Where(m => m.id == key).Select(m => m.TipoDeduccion));

        }


        // GET: odata/Transaccion(5)/TipoIngreso
        [EnableQuery]

        public SingleResult<TipoIngreso> GetTipoIngreso([FromODataUri] int key)

        {

            return SingleResult.Create(db.Transaccion.Where(m => m.id == key).Select(m => m.TipoIngreso));

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TransaccionExists(int key)
        {
            return db.Transaccion.Any(e => e.id == key);
        }
    }
}
