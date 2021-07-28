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
    builder.EntitySet<Departamento>("Departamento");
    builder.EntitySet<Empleado>("Empleado"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class DepartamentoController : ODataController
    {
        private Proyecto_Fin_Hibrido2Entities1 db = new Proyecto_Fin_Hibrido2Entities1();

        // GET: odata/Departamento
        [EnableQuery]
        public IQueryable<Departamento> Get()
        {
            return db.Departamento;
        }

        // GET: odata/Departamento(5)
        [EnableQuery]
        public SingleResult<Departamento> Get([FromODataUri] int key)
        {
            return SingleResult.Create(db.Departamento.Where(departamento => departamento.id == key));
        }

        // PUT: odata/Departamento(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Departamento update)
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
                if (!DepartamentoExists(key))
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

        // POST: odata/Departamento
        public async Task<IHttpActionResult> Post(Departamento departamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Departamento.Add(departamento);
            await db.SaveChangesAsync();

            return Created(departamento);
        }

        // PATCH: odata/Departamento(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Departamento> patch)
        {
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Departamento departamento = await db.Departamento.FindAsync(key);

            if (departamento == null)
            {
                return NotFound();
            }

            patch.Patch(departamento);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartamentoExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(departamento);
        }

        // DELETE: odata/Departamento(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Departamento departamento = await db.Departamento.FindAsync(key);
            if (departamento == null)
            {
                return NotFound();
            }

            db.Departamento.Remove(departamento);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Departamento(5)/Empleado
        [EnableQuery]
        public IQueryable<Empleado> GetEmpleado([FromODataUri] int key)
        {
            return db.Departamento.Where(m => m.id == key).SelectMany(m => m.Empleado);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DepartamentoExists(int key)
        {
            return db.Departamento.Any(e => e.id == key);
        }
    }
}
