
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
    builder.EntitySet<Empleado>("Empleado");

    builder.EntitySet<Departamento>("Departamento"); 

    builder.EntitySet<Puesto>("Puesto"); 

    builder.EntitySet<TipoNomina>("TipoNomina"); 

    builder.EntitySet<NominaResumen>("NominaResumen"); 

    builder.EntitySet<Transaccion>("Transaccion"); 


    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

    */

    public class EmpleadoController : ODataController
    {
        private Proyecto_Fin_Hibrido2Entities1 db = new Proyecto_Fin_Hibrido2Entities1();

        // GET: odata/Empleado
        [EnableQuery]
        public IQueryable<Empleado> Get()
        {
            return db.Empleado;
        }

        // GET: odata/Empleado(5)
        [EnableQuery]
        public SingleResult<Empleado> Get([FromODataUri] int key)
        {
            return SingleResult.Create(db.Empleado.Where(empleado => empleado.id == key));
        }

        // PUT: odata/Empleado(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Empleado update)
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
                if (!EmpleadoExists(key))
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

        // POST: odata/Empleado
        public async Task<IHttpActionResult> Post(Empleado empleado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Empleado.Add(empleado);

            await db.SaveChangesAsync();


            return Created(empleado);
        }

        // PATCH: odata/Empleado(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Empleado> patch)
        {
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Empleado empleado = await db.Empleado.FindAsync(key);

            if (empleado == null)
            {
                return NotFound();
            }

            patch.Patch(empleado);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpleadoExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(empleado);
        }

        // DELETE: odata/Empleado(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Empleado empleado = await db.Empleado.FindAsync(key);
            if (empleado == null)
            {
                return NotFound();
            }

            db.Empleado.Remove(empleado);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }


        // GET: odata/Empleado(5)/Departamento
        [EnableQuery]

        public SingleResult<Departamento> GetDepartamento([FromODataUri] int key)

        {

            return SingleResult.Create(db.Empleado.Where(m => m.id == key).Select(m => m.Departamento));

        }


        // GET: odata/Empleado(5)/Puesto
        [EnableQuery]

        public SingleResult<Puesto> GetPuesto([FromODataUri] int key)

        {

            return SingleResult.Create(db.Empleado.Where(m => m.id == key).Select(m => m.Puesto));

        }


        // GET: odata/Empleado(5)/TipoNomina
        [EnableQuery]

        public SingleResult<TipoNomina> GetTipoNomina([FromODataUri] int key)

        {

            return SingleResult.Create(db.Empleado.Where(m => m.id == key).Select(m => m.TipoNomina));

        }


        // GET: odata/Empleado(5)/NominaResumen
        [EnableQuery]

        public IQueryable<NominaResumen> GetNominaResumen([FromODataUri] int key)

        {

            return db.Empleado.Where(m => m.id == key).SelectMany(m => m.NominaResumen);

        }


        // GET: odata/Empleado(5)/Transaccion
        [EnableQuery]

        public IQueryable<Transaccion> GetTransaccion([FromODataUri] int key)

        {

            return db.Empleado.Where(m => m.id == key).SelectMany(m => m.Transaccion);

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmpleadoExists(int key)
        {
            return db.Empleado.Any(e => e.id == key);
        }
    }
}
