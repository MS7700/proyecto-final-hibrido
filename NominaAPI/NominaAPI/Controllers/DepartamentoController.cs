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
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
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
        public IQueryable<Departamento> GetDepartamento()
        {
            return db.Departamento;
        }

        // GET: odata/Departamento(5)
        [EnableQuery]
        public SingleResult<Departamento> GetDepartamento([FromODataUri] int key)
        {
            return SingleResult.Create(db.Departamento.Where(departamento => departamento.ID == key));
        }

        // PUT: odata/Departamento(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Departamento> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Departamento departamento = db.Departamento.Find(key);
            if (departamento == null)
            {
                return NotFound();
            }

            patch.Put(departamento);

            try
            {
                db.SaveChanges();
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

        // POST: odata/Departamento
        public IHttpActionResult Post(Departamento departamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Departamento.Add(departamento);
            db.SaveChanges();

            return Created(departamento);
        }

        // PATCH: odata/Departamento(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Departamento> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Departamento departamento = db.Departamento.Find(key);
            if (departamento == null)
            {
                return NotFound();
            }

            patch.Patch(departamento);

            try
            {
                db.SaveChanges();
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
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Departamento departamento = db.Departamento.Find(key);
            if (departamento == null)
            {
                return NotFound();
            }

            db.Departamento.Remove(departamento);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Departamento(5)/Empleado
        [EnableQuery]
        public IQueryable<Empleado> GetEmpleado([FromODataUri] int key)
        {
            return db.Departamento.Where(m => m.ID == key).SelectMany(m => m.Empleado);
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
            return db.Departamento.Count(e => e.ID == key) > 0;
        }
    }
}
