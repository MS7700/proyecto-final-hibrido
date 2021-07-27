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
    builder.EntitySet<Puesto>("Puesto");
    builder.EntitySet<Empleado>("Empleado"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class PuestoController : ODataController
    {
        private Proyecto_Fin_Hibrido2Entities1 db = new Proyecto_Fin_Hibrido2Entities1();

        // GET: odata/Puesto
        [EnableQuery]
        public IQueryable<Puesto> GetPuesto()
        {
            return db.Puesto;
        }

        // GET: odata/Puesto(5)
        [EnableQuery]
        public SingleResult<Puesto> GetPuesto([FromODataUri] int key)
        {
            return SingleResult.Create(db.Puesto.Where(puesto => puesto.ID == key));
        }

        // PUT: odata/Puesto(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Puesto> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Puesto puesto = db.Puesto.Find(key);
            if (puesto == null)
            {
                return NotFound();
            }

            patch.Put(puesto);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PuestoExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(puesto);
        }

        // POST: odata/Puesto
        public IHttpActionResult Post(Puesto puesto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Puesto.Add(puesto);
            db.SaveChanges();

            return Created(puesto);
        }

        // PATCH: odata/Puesto(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Puesto> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Puesto puesto = db.Puesto.Find(key);
            if (puesto == null)
            {
                return NotFound();
            }

            patch.Patch(puesto);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PuestoExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(puesto);
        }

        // DELETE: odata/Puesto(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Puesto puesto = db.Puesto.Find(key);
            if (puesto == null)
            {
                return NotFound();
            }

            db.Puesto.Remove(puesto);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Puesto(5)/Empleado
        [EnableQuery]
        public IQueryable<Empleado> GetEmpleado([FromODataUri] int key)
        {
            return db.Puesto.Where(m => m.ID == key).SelectMany(m => m.Empleado);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PuestoExists(int key)
        {
            return db.Puesto.Count(e => e.ID == key) > 0;
        }
    }
}
