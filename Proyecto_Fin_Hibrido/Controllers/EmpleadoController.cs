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
    public class EmpleadoController : ApiController
    {
        private Proyecto_Fin_HibridoEntities db = new Proyecto_Fin_HibridoEntities();

        // GET: api/Empleado
        public IQueryable<Empleado> GetEmpleado()
        {
            Request.Properties["Count"] = db.Empleado.Count();
            return db.Empleado;
        }

        // GET: api/Empleado/5
        [ResponseType(typeof(Empleado))]
        public IHttpActionResult GetEmpleado(int id)
        {
            Empleado empleado = db.Empleado.Find(id);
            if (empleado == null)
            {
                return NotFound();
            }

            return Ok(empleado);
        }

        // PUT: api/Empleado/5
        [ResponseType(typeof(Empleado))]
        public IHttpActionResult PutEmpleado(int id, Empleado empleado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != empleado.IdEmpleado)
            {
                return BadRequest();
            }

            db.Entry(empleado).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpleadoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(empleado);
        }

        // POST: api/Empleado
        [ResponseType(typeof(Empleado))]
        public IHttpActionResult PostEmpleado(Empleado empleado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Empleado.Add(empleado);
            db.SaveChanges();

            return Ok(empleado);
        }

        // DELETE: api/Empleado/5
        [ResponseType(typeof(Empleado))]
        public IHttpActionResult DeleteEmpleado(int id)
        {
            Empleado empleado = db.Empleado.Find(id);
            if (empleado == null)
            {
                return NotFound();
            }

            db.Empleado.Remove(empleado);
            db.SaveChanges();

            return Ok(empleado);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmpleadoExists(int id)
        {
            return db.Empleado.Count(e => e.IdEmpleado == id) > 0;
        }
    }
}