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
using Proyecto_Fin_Hibrido.Dto;

namespace Proyecto_Fin_Hibrido.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-Total-Count,Content-Range")]
    [CountHeaderFilter]
    public class NominasController : ApiController
    {
        private Proyecto_Fin_HibridoEntities db = new Proyecto_Fin_HibridoEntities();

        // GET: api/Nominas
        public IEnumerable<Nomina> GetNomina([FromUri] NominaDto dto = null)
        {
            List<Nomina> res = db.Nomina.ToList<Nomina>();
            if (dto == null)
            {
                Request.Properties["Count"] = res.Count();
                return res;
            }
            else
            {

                if (dto.filter != null)
                {
                    dto.FilterList(res);
                }
                if (dto.sort != null)
                {
                    dto.SortList<Nomina>(res);
                }
                if(dto.range != null)
                {
                    dto.RangeList<Nomina>(res);
                }
            }
            Request.Properties["Count"] = res.Count();
            return res;

        }




        // GET: api/Nominas/5
        [ResponseType(typeof(Nomina))]
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
        public IHttpActionResult DeleteNomina(int id)
        {
            Nomina nomina = db.Nomina.Find(id);
            if (nomina == null)
            {
                return NotFound();
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