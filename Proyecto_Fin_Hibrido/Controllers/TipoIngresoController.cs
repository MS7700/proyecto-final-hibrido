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
using Proyecto_Fin_Hibrido;

namespace Proyecto_Fin_Hibrido.Controllers
{
    public class TipoIngresoController : ApiController
    {
        private Proyecto_Fin_HibridoEntities db = new Proyecto_Fin_HibridoEntities();

        // GET: api/TipoIngreso
        public IQueryable<TipoIngreso> GetTipoIngreso()
        {
            return db.TipoIngreso;
        }

        // GET: api/TipoIngreso/5
        [ResponseType(typeof(TipoIngreso))]
        public IHttpActionResult GetTipoIngreso(int id)
        {
            TipoIngreso tipoIngreso = db.TipoIngreso.Find(id);
            if (tipoIngreso == null)
            {
                return NotFound();
            }

            return Ok(tipoIngreso);
        }

        // PUT: api/TipoIngreso/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTipoIngreso(int id, TipoIngreso tipoIngreso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoIngreso.IdIngreso)
            {
                return BadRequest();
            }

            db.Entry(tipoIngreso).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoIngresoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TipoIngreso
        [ResponseType(typeof(TipoIngreso))]
        public IHttpActionResult PostTipoIngreso(TipoIngreso tipoIngreso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TipoIngreso.Add(tipoIngreso);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tipoIngreso.IdIngreso }, tipoIngreso);
        }

        // DELETE: api/TipoIngreso/5
        [ResponseType(typeof(TipoIngreso))]
        public IHttpActionResult DeleteTipoIngreso(int id)
        {
            TipoIngreso tipoIngreso = db.TipoIngreso.Find(id);
            if (tipoIngreso == null)
            {
                return NotFound();
            }

            db.TipoIngreso.Remove(tipoIngreso);
            db.SaveChanges();

            return Ok(tipoIngreso);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipoIngresoExists(int id)
        {
            return db.TipoIngreso.Count(e => e.IdIngreso == id) > 0;
        }
    }
}