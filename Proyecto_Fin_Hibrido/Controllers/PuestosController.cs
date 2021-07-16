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
    public class PuestosController : ApiController
    {
        private Proyecto_Fin_HibridoEntities db = new Proyecto_Fin_HibridoEntities();

        

        // GET: api/Puestos?id[0]=1&id[1]=2
        //[Route("api/Puestos/{id?}")]
        public IEnumerable<Puesto> GetPuesto([FromUri] PuestoDto dto = null)
        {
            List<Puesto> res = db.Puesto.ToList<Puesto>();
            if (dto == null)
            {
                Request.Properties["Count"] = res.Count();
                Console.WriteLine("No value");
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
                    dto.SortList<Puesto>(res);
                }
                if(dto.range != null)
                {
                    dto.RangeList<Puesto>(res);
                }
            }
            Request.Properties["Count"] = res.Count();
            return res;

        }


        // GET: api/Puestos/5
        [ResponseType(typeof(Puesto))]
        //[Route("api/Puestos/{id}")]
        public IHttpActionResult GetPuesto(int id)
        {
            Puesto puesto = db.Puesto.Find(id);
            if (puesto == null)
            {
                return NotFound();
            }

            return Ok(puesto);
        }

        // PUT: api/Puestos/5
        [ResponseType(typeof(Puesto))]
        //[Route("api/Puestos/{id}")]
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

            return Ok(puesto);
        }

        // POST: api/Puestos
        [ResponseType(typeof(Puesto))]
        //[Route("api/Puestos")]
        public IHttpActionResult PostPuesto(Puesto puesto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Puesto.Add(puesto);
            db.SaveChanges();

            return Ok(puesto);
        }

        // DELETE: api/Puestos/5
        [ResponseType(typeof(Puesto))]
        //[Route("api/Puestos/{id}")]
        public IHttpActionResult DeletePuesto(int id)
        {
            Puesto puesto = db.Puesto.Find(id);
            if (puesto == null)
            {
                return NotFound();
            }
            if (db.Empleado.Count(e => e.IdPuesto == id) > 0)
            {
                return Conflict();
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