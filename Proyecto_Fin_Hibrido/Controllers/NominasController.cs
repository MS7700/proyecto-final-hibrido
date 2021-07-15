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
    public class NominasController : ApiController
    {
        private Proyecto_Fin_HibridoEntities db = new Proyecto_Fin_HibridoEntities();

        // GET: api/Nominas
        [Route("api/Nominas")]
        public IQueryable<Nomina> GetNomina()
        {
            Request.Properties["Count"] = db.Nomina.Count();
            return db.Nomina;
        }

        // GET: api/Nominas?id[0]=1&id[1]=2
        [Route("api/Nominas/{id?}")]
        public IQueryable<Nomina> GetNomina([FromUri] int[]id)
        {

            if (id == null)
            {
                Request.Properties["Count"] = db.Nomina.Count();
                return db.Nomina;
            }
            else
            {

                
                List<Nomina> res = new List<Nomina>();
                foreach (int uid in id)
                {
                    res.Add(db.Nomina.Find(uid));
                }
                Request.Properties["Count"] = res.Count();
                return res.AsQueryable<Nomina>();
            }

        }




        // GET: api/Nominas/5
        [ResponseType(typeof(Nomina))]
        [Route("api/Nominas/{id}")]
        public IHttpActionResult GetNomina(int id)
        {
            Nomina nomina = db.Nomina.Find(id);
            if (nomina == null)
            {
                return NotFound();
            }

            return Ok(nomina);
        }

        // PUT: api/Nominas/5
        [ResponseType(typeof(Nomina))]
        [Route("api/Nominas/{id}")]
        public IHttpActionResult PutNomina(int id, Nomina nomina)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nomina.IdNomina)
            {
                return BadRequest();
            }

            db.Entry(nomina).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NominaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(nomina);
        }

        // POST: api/Nominas
        [ResponseType(typeof(Nomina))]
        [Route("api/Nominas")]
        public IHttpActionResult PostNomina(Nomina nomina)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Nomina.Add(nomina);
            db.SaveChanges();

            return Ok(nomina);
        }

        // DELETE: api/Nominas/5
        [ResponseType(typeof(Nomina))]
        [Route("api/Nominas/{id}")]
        public IHttpActionResult DeleteNomina(int id)
        {
            Nomina nomina = db.Nomina.Find(id);
            if (nomina == null)
            {
                return NotFound();
            }

            if(db.Empleado.Count(e => e.IdNomina == id) > 0)
            {
                return Conflict();
            }
            db.Nomina.Remove(nomina);
            db.SaveChanges();

            return Ok(nomina);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NominaExists(int id)
        {
            return db.Nomina.Count(e => e.IdNomina == id) > 0;
        }
    }
}