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
    public class AsientoContableController : ApiController
    {
        private Proyecto_Fin_HibridoEntities db = new Proyecto_Fin_HibridoEntities();

        // GET: api/AsientoContable
        public IQueryable<Asiento_Contable> GetAsiento_Contable()
        {
            Request.Properties["Count"] = db.Asiento_Contable.Count();
            return db.Asiento_Contable;
        }

        // GET: api/AsientoContable/5
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

        // PUT: api/AsientoContable/5
        [ResponseType(typeof(Asiento_Contable))]
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

            return Ok(asiento_Contable);
        }

        // POST: api/AsientoContable
        [ResponseType(typeof(Asiento_Contable))]
        public IHttpActionResult PostAsiento_Contable(Asiento_Contable asiento_Contable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Asiento_Contable.Add(asiento_Contable);
            db.SaveChanges();

            return Ok(asiento_Contable);
        }

        // DELETE: api/AsientoContable/5
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