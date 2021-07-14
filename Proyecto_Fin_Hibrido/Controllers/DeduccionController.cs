using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Proyecto_Fin_Hibrido.Filters;
using System.Web.Http.Cors;

using Proyecto_Fin_Hibrido;

namespace Proyecto_Fin_Hibrido.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-Total-Count")]
    [CountHeaderFilter]
    public class DeduccionController : ApiController
    {
        private Proyecto_Fin_HibridoEntities db = new Proyecto_Fin_HibridoEntities();

        // GET: api/Deduccion
        public IQueryable<TipoDeduccion> GetTipoDeduccion()
        {
            Request.Properties["Count"] = db.TipoDeduccion.Count();
            return db.TipoDeduccion;
        }

        // GET: api/Deduccion/5
        [ResponseType(typeof(TipoDeduccion))]
        public IHttpActionResult GetTipoDeduccion(int id)
        {
            TipoDeduccion tipoDeduccion = db.TipoDeduccion.Find(id);
            if (tipoDeduccion == null)
            {
                return NotFound();
            }

            return Ok(tipoDeduccion);
        }

        // PUT: api/Deduccion/5
        [ResponseType(typeof(TipoDeduccion))]
        public IHttpActionResult PutTipoDeduccion(int id, TipoDeduccion tipoDeduccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoDeduccion.IdDeduccion)
            {
                return BadRequest();
            }

            db.Entry(tipoDeduccion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoDeduccionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(tipoDeduccion);
        }

        // POST: api/Deduccion
        [ResponseType(typeof(TipoDeduccion))]
        public IHttpActionResult PostTipoDeduccion(TipoDeduccion tipoDeduccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TipoDeduccion.Add(tipoDeduccion);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TipoDeduccionExists(tipoDeduccion.IdDeduccion))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(tipoDeduccion);
        }

        // DELETE: api/Deduccion/5
        [ResponseType(typeof(TipoDeduccion))]
        public IHttpActionResult DeleteTipoDeduccion(int id)
        {
            TipoDeduccion tipoDeduccion = db.TipoDeduccion.Find(id);
            if (tipoDeduccion == null)
            {
                return NotFound();
            }

            db.TipoDeduccion.Remove(tipoDeduccion);
            db.SaveChanges();

            return Ok(tipoDeduccion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipoDeduccionExists(int id)
        {
            return db.TipoDeduccion.Count(e => e.IdDeduccion == id) > 0;
        }
    }
}