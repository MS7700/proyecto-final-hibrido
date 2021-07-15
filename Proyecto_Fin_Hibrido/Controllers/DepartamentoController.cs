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
        //public IQueryable<Departamento> GetDepartamento(string ids = null)
        //{


        //    if (ids == null)
        //    {
        //        Request.Properties["Count"] = db.Departamento.Count();
        //        return db.Departamento;
        //    }
        //    else
        //    {

        //        var arrayids = ids.Substring(1, ids.Length - 2).Split(',');
        //        List<Departamento> res = new List<Departamento>();
        //        foreach (var id in arrayids)
        //        {
        //            res.Add(db.Departamento.Find(int.Parse(id)));
        //        }
        //        //IQueryable<Departamento> res = db.Departamento.Where(d => arrayids.Contains(d.IdDepartamento.ToString()));
        //        //var res = db.Departamento.Where(d => arrayids.Contains(d.IdDepartamento));
        //        Request.Properties["Count"] = res.Count();
        //        return res.AsQueryable<Departamento>();
        //    }


        public IQueryable<Departamento> GetDepartamento(int[]id = null)
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
                //IQueryable<Departamento> res = db.Departamento.Where(d => arrayids.Contains(d.IdDepartamento.ToString()));
                //var res = db.Departamento.Where(d => arrayids.Contains(d.IdDepartamento));
                Request.Properties["Count"] = res.Count();
                return res.AsQueryable<Departamento>();
            }



            

        }


        //Request.Properties["Count"] = db.Departamento.Count();
        //return db.Departamento;

        //}


        //[Route("api/Departamentos/filter/{ids?}")]
        //public IQueryable<Departamento> GetDepartamento([FromUri] int[] id = null)
        //{

        //    //Request.Properties["Count"] = db.Departamento.Count();
        //    //return db.Departamento;
        //    if (id == null)
        //    {
        //        Request.Properties["Count"] = db.Departamento.Count();
        //        return db.Departamento;
        //    }
        //    else
        //    {
        //        var res = db.Departamento.Where(d => id.Contains(d.IdDepartamento));
        //        Request.Properties["Count"] = res.Count();
        //        return res;
        //    }

        //    }


        //[Route("api/Departamento?filter=id:{id}")]
        //public IQueryable<Departamento> GetDepartamento(int[] id)
        //{
        //    var res = db.Departamento.Where(d => id.Contains(d.IdDepartamento));
        //    Request.Properties["Count"] = res.Count();
        //    return res;
        //    //Request.Properties["Count"] = db.Departamento.Count();
        //    //return db.Departamento;
        //}


        // GET: api/Departamento/5
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

        // PUT: api/Departamento/5
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

        // POST: api/Departamento
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

        // DELETE: api/Departamento/5
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