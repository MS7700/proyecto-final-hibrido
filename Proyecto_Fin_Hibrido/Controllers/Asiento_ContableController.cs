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
    public class Asiento_ContableController : ApiController
    {
        private Proyecto_Fin_HibridoEntities db = new Proyecto_Fin_HibridoEntities();

        // GET: api/Asiento_Contable
        public IQueryable<Asiento_Contable> GetAsiento_Contable()
        {
            return db.Asiento_Contable;
        }

        // GET: api/Asiento_Contable/5
        [ResponseType(typeof(Asiento_Contable))]
        public IHttpActionResult GetAsiento_Contable(int id)
        {
            Asiento_Contable asiento_Contable = db.Asiento_Contable.Find(id);
            if (asiento_Contable == null)
            {
                return NotFound();
            }

            return Ok(asiento_Contable);
        }

        // PUT: api/Asiento_Contable/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAsiento_Contable(int id, Asiento_Contable asiento_Contable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != asiento_Contable.IdAsiento)
            {
                return BadRequest();
            }

            db.Entry(asiento_Contable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Asiento_ContableExists(id))
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

        // POST: api/Asiento_Contable
        [ResponseType(typeof(Asiento_Contable))]
        public IHttpActionResult PostAsiento_Contable(Asiento_Contable asiento_Contable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Asiento_Contable.Add(asiento_Contable);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = asiento_Contable.IdAsiento }, asiento_Contable);
        }

        // DELETE: api/Asiento_Contable/5
        [ResponseType(typeof(Asiento_Contable))]
        public IHttpActionResult DeleteAsiento_Contable(int id)
        {
            Asiento_Contable asiento_Contable = db.Asiento_Contable.Find(id);
            if (asiento_Contable == null)
            {
                return NotFound();
            }

            db.Asiento_Contable.Remove(asiento_Contable);
            db.SaveChanges();

            return Ok(asiento_Contable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Asiento_ContableExists(int id)
        {
            return db.Asiento_Contable.Count(e => e.IdAsiento == id) > 0;
        }
    }
}