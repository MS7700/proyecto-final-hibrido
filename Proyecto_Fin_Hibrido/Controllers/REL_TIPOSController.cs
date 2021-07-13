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
    public class REL_TIPOSController : ApiController
    {
        private Proyecto_Fin_HibridoEntities db = new Proyecto_Fin_HibridoEntities();

        // GET: api/REL_TIPOS
        public IQueryable<REL_TIPOS> GetREL_TIPOS()
        {
            return db.REL_TIPOS;
        }

        // GET: api/REL_TIPOS/5
        [ResponseType(typeof(REL_TIPOS))]
        public IHttpActionResult GetREL_TIPOS(int id)
        {
            REL_TIPOS rEL_TIPOS = db.REL_TIPOS.Find(id);
            if (rEL_TIPOS == null)
            {
                return NotFound();
            }

            return Ok(rEL_TIPOS);
        }

        // PUT: api/REL_TIPOS/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutREL_TIPOS(int id, REL_TIPOS rEL_TIPOS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rEL_TIPOS.IdTipo)
            {
                return BadRequest();
            }

            db.Entry(rEL_TIPOS).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!REL_TIPOSExists(id))
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

        // POST: api/REL_TIPOS
        [ResponseType(typeof(REL_TIPOS))]
        public IHttpActionResult PostREL_TIPOS(REL_TIPOS rEL_TIPOS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.REL_TIPOS.Add(rEL_TIPOS);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rEL_TIPOS.IdTipo }, rEL_TIPOS);
        }

        // DELETE: api/REL_TIPOS/5
        [ResponseType(typeof(REL_TIPOS))]
        public IHttpActionResult DeleteREL_TIPOS(int id)
        {
            REL_TIPOS rEL_TIPOS = db.REL_TIPOS.Find(id);
            if (rEL_TIPOS == null)
            {
                return NotFound();
            }

            db.REL_TIPOS.Remove(rEL_TIPOS);
            db.SaveChanges();

            return Ok(rEL_TIPOS);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool REL_TIPOSExists(int id)
        {
            return db.REL_TIPOS.Count(e => e.IdTipo == id) > 0;
        }
    }
}