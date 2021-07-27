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
    builder.EntitySet<Nomina>("Nomina");
    builder.EntitySet<Empleado>("Empleado"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class NominaController : ODataController
    {
        private Proyecto_Fin_Hibrido2Entities1 db = new Proyecto_Fin_Hibrido2Entities1();

        // GET: odata/Nomina
        [EnableQuery]
        public IQueryable<Nomina> GetNomina()
        {
            return db.Nomina;
        }

        // GET: odata/Nomina(5)
        [EnableQuery]
        public SingleResult<Nomina> GetNomina([FromODataUri] int key)
        {
            return SingleResult.Create(db.Nomina.Where(nomina => nomina.ID == key));
        }

        // PUT: odata/Nomina(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Nomina> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Nomina nomina = db.Nomina.Find(key);
            if (nomina == null)
            {
                return NotFound();
            }

            patch.Put(nomina);

            try
            {
                db.SaveChanges();
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

        // POST: odata/Nomina
        public IHttpActionResult Post(Nomina nomina)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Nomina.Add(nomina);
            db.SaveChanges();

            return Created(nomina);
        }

        // PATCH: odata/Nomina(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Nomina> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Nomina nomina = db.Nomina.Find(key);
            if (nomina == null)
            {
                return NotFound();
            }

            patch.Patch(nomina);

            try
            {
                db.SaveChanges();
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
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Nomina nomina = db.Nomina.Find(key);
            if (nomina == null)
            {
                return NotFound();
            }

            db.Nomina.Remove(nomina);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Nomina(5)/Empleado
        [EnableQuery]
        public IQueryable<Empleado> GetEmpleado([FromODataUri] int key)
        {
            return db.Nomina.Where(m => m.ID == key).SelectMany(m => m.Empleado);
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
            return db.Nomina.Count(e => e.ID == key) > 0;
        }
    }
}
