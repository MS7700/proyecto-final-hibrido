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
    public class DepartamentosController : ApiController
    {
        private Proyecto_Fin_HibridoEntities db = new Proyecto_Fin_HibridoEntities();

        // GET: api/Departamentos
        public IEnumerable<Departamento> GetDepartamento([FromUri] DepartamentoDto dto = null)
        {
            List<Departamento> res = db.Departamento.ToList<Departamento>();
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
                    dto.SortList<Departamento>(res);
                }
                if(dto.range != null)
                {
                    dto.RangeList<Departamento>(res);
                }
            }
            Request.Properties["Count"] = res.Count();
            return res;

        }




        // GET: api/Departamentos/5
        [ResponseType(typeof(Departamento))]
        public IHttpActionResult GetDepartamento(int id)
        {
            Departamento departamento = db.Departamento.Find(id);
            if (departamento == null)
            {
                return NotFound();
            }

            return Ok(departamento);
        }

        // PUT: api/Departamentos/5
        [ResponseType(typeof(Departamento))]
        public IHttpActionResult PutDepartamento(int id, Departamento departamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != departamento.IdDepartamento)
            {
                return BadRequest();
            }

            db.Entry(departamento).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartamentoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(departamento);
        }

        // POST: api/Departamentos
        [ResponseType(typeof(Departamento))]
        public IHttpActionResult PostDepartamento(Departamento departamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Departamento.Add(departamento);
            db.SaveChanges();

            return Ok(departamento);
        }

        // DELETE: api/Departamentos/5
        [ResponseType(typeof(Departamento))]
        public IHttpActionResult DeleteDepartamento(int id)
        {
            Departamento departamento = db.Departamento.Find(id);
            if (departamento == null)
            {
                return NotFound();
            }

            db.Departamento.Remove(departamento);
            db.SaveChanges();

            return Ok(departamento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DepartamentoExists(int id)
        {
            return db.Departamento.Count(e => e.IdDepartamento == id) > 0;
        }
    }
}