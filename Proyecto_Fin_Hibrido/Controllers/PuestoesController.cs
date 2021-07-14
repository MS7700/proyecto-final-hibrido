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
    public class PuestoesController : ApiController
    {
        private Proyecto_Fin_HibridoEntities db = new Proyecto_Fin_HibridoEntities();

        // GET: api/Puestoes
        public IQueryable<Puesto> GetPuesto()
        {
            return db.Puesto;
        }

        // GET: api/Puestoes/5
        [ResponseType(typeof(Puesto))]
        public IHttpActionResult GetPuesto(int id)
        {
            Puesto puesto = db.Puesto.Find(id);
            if (puesto == null)
            {
                return NotFound();
            }

            return Ok(puesto);
        }

        // PUT: api/Puestoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPuesto(int id, Puesto puesto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != puesto.IdPuesto)
            {
                return BadRequest();
            }

            db.Entry(puesto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PuestoExists(id))
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

        // POST: api/Puestoes
        [ResponseType(typeof(Puesto))]
        public IHttpActionResult PostPuesto(Puesto puesto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Puesto.Add(puesto);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = puesto.IdPuesto }, puesto);
        }

        // DELETE: api/Puestoes/5
        [ResponseType(typeof(Puesto))]
        public IHttpActionResult DeletePuesto(int id)
        {
            Puesto puesto = db.Puesto.Find(id);
            if (puesto == null)
            {
                return NotFound();
            }

            db.Puesto.Remove(puesto);
            db.SaveChanges();

            return Ok(puesto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PuestoExists(int id)
        {
            return db.Puesto.Count(e => e.IdPuesto == id) > 0;
        }
    }
}