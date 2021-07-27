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
    builder.EntitySet<Empleado>("Empleado");
    builder.EntitySet<Departamento>("Departamento"); 
    builder.EntitySet<Nomina>("Nomina"); 
    builder.EntitySet<Puesto>("Puesto"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class EmpleadoController : ODataController
    {
        private Proyecto_Fin_Hibrido2Entities1 db = new Proyecto_Fin_Hibrido2Entities1();

        // GET: odata/Empleado
        [EnableQuery]
        public IQueryable<Empleado> GetEmpleado()
        {
            return db.Empleado;
        }

        // GET: odata/Empleado(5)
        [EnableQuery]
        public SingleResult<Empleado> GetEmpleado([FromODataUri] int key)
        {
            return SingleResult.Create(db.Empleado.Where(empleado => empleado.ID == key));
        }

        // PUT: odata/Empleado(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Empleado> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Empleado empleado = db.Empleado.Find(key);
            if (empleado == null)
            {
                return NotFound();
            }

            patch.Put(empleado);

            try
            {
                db.SaveChanges();
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

        // POST: odata/Empleado
        public IHttpActionResult Post(Empleado empleado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Empleado.Add(empleado);
            db.SaveChanges();

            return Created(empleado);
        }

        // PATCH: odata/Empleado(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Empleado> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Empleado empleado = db.Empleado.Find(key);
            if (empleado == null)
            {
                return NotFound();
            }

            patch.Patch(empleado);

            try
            {
                db.SaveChanges();
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
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Empleado empleado = db.Empleado.Find(key);
            if (empleado == null)
            {
                return NotFound();
            }

            db.Empleado.Remove(empleado);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Empleado(5)/Departamento
        [EnableQuery]
        public SingleResult<Departamento> GetDepartamento([FromODataUri] int key)
        {
            return SingleResult.Create(db.Empleado.Where(m => m.ID == key).Select(m => m.Departamento));
        }

        // GET: odata/Empleado(5)/Nomina
        [EnableQuery]
        public SingleResult<Nomina> GetNomina([FromODataUri] int key)
        {
            return SingleResult.Create(db.Empleado.Where(m => m.ID == key).Select(m => m.Nomina));
        }

        // GET: odata/Empleado(5)/Puesto
        [EnableQuery]
        public SingleResult<Puesto> GetPuesto([FromODataUri] int key)
        {
            return SingleResult.Create(db.Empleado.Where(m => m.ID == key).Select(m => m.Puesto));
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
            return db.Empleado.Count(e => e.ID == key) > 0;
        }
    }
}
