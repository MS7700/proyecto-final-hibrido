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
    public class TransaccionController : ApiController
    {
        private Proyecto_Fin_HibridoEntities db = new Proyecto_Fin_HibridoEntities();

        // GET: api/Transaccion
        public IQueryable<RegistroTransacciones> GetRegistroTransacciones()
        {
            Request.Properties["Count"] = db.RegistroTransacciones.Count();
            return db.RegistroTransacciones;
        }

        // GET: api/Transaccion/5
        [ResponseType(typeof(RegistroTransacciones))]
        public IHttpActionResult GetRegistroTransacciones(int id)
        {
            RegistroTransacciones registroTransacciones = db.RegistroTransacciones.Find(id);
            if (registroTransacciones == null)
            {
                return NotFound();
            }

            return Ok(registroTransacciones);
        }

        // PUT: api/Transaccion/5
        [ResponseType(typeof(RegistroTransacciones))]
        public IHttpActionResult PutRegistroTransacciones(int id, RegistroTransacciones registroTransacciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != registroTransacciones.IdTransaccion)
            {
                return BadRequest();
            }

            db.Entry(registroTransacciones).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistroTransaccionesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(registroTransacciones);
        }

        // POST: api/Transaccion
        [ResponseType(typeof(RegistroTransacciones))]
        public IHttpActionResult PostRegistroTransacciones(RegistroTransacciones registroTransacciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RegistroTransacciones.Add(registroTransacciones);
            db.SaveChanges();

            return Ok(registroTransacciones);
        }

        // DELETE: api/Transaccion/5
        [ResponseType(typeof(RegistroTransacciones))]
        public IHttpActionResult DeleteRegistroTransacciones(int id)
        {
            RegistroTransacciones registroTransacciones = db.RegistroTransacciones.Find(id);
            if (registroTransacciones == null)
            {
                return NotFound();
            }

            db.RegistroTransacciones.Remove(registroTransacciones);
            db.SaveChanges();

            return Ok(registroTransacciones);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RegistroTransaccionesExists(int id)
        {
            return db.RegistroTransacciones.Count(e => e.IdTransaccion == id) > 0;
        }
    }
}