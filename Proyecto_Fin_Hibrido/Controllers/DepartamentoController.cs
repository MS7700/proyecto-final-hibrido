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
    public class DepartamentosController : ApiController
    {
        private Proyecto_Fin_HibridoEntities db = new Proyecto_Fin_HibridoEntities();

        // GET: api/Departamento
        [Route("api/Departamentos")]
        public IQueryable<Departamento> GetDepartamento()
        {

                Request.Properties["Count"] = db.Departamento.Count();
                return db.Departamento;
            

        }

        // GET: api/Departamento?id[0]=1&id[1]=2
        [Route("api/Departamentos/{id?}")]
        public IQueryable<Departamento> GetDepartamento([FromUri] int[]id)
        {

            if (id == null)
            {
                Request.Properties["Count"] = db.Departamento.Count();
                return db.Departamento;
            }
            else
            {

                
                List<Departamento> res = new List<Departamento>();
                foreach (int uid in id)
                {
                    res.Add(db.Departamento.Find(uid));
                }
                Request.Properties["Count"] = res.Count();
                return res.AsQueryable<Departamento>();
            }

        }



        // GET: api/Departamento/5
        [ResponseType(typeof(Departamento))]
        [Route("api/Departamentos/{id}")]
        public IHttpActionResult GetDepartamento(int id)
        {
            Departamento departamento = db.Departamento.Find(id);
            if (departamento == null)
            {
                return NotFound();
            }

            return Ok(departamento);
        }

        // PUT: api/Departamento/5
        [ResponseType(typeof(Departamento))]
        [Route("api/Departamentos/{id}")]
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

        // POST: api/Departamento
        [ResponseType(typeof(Departamento))]
        [Route("api/Departamentos")]
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

        // DELETE: api/Departamento/5
        [ResponseType(typeof(Departamento))]
        [Route("api/Departamentos/{id}")]
        public IHttpActionResult DeleteDepartamento(int id)
        {
            Departamento departamento = db.Departamento.Find(id);
            if (departamento == null)
            {
                return NotFound();
            }

            if (db.Empleado.Count(e => e.IdDepartamento == id) > 0)
            {
                return Conflict();
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