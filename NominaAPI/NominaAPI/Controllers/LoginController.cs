using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData.Routing;
using Microsoft.AspNet.OData;
using System.Threading.Tasks;
using NominaAPI.Models;
using NominaAPI.Attributes;
using Microsoft.AspNet.OData.Routing;
using System.Security.Claims;

namespace NominaAPI.Controllers
{
    /*
    Puede que la clase WebApiConfig requiera cambios adicionales para agregar una ruta para este controlador. Combine estas instrucciones en el método Register de la clase WebApiConfig según corresponda. Tenga en cuenta que las direcciones URL de OData distinguen mayúsculas de minúsculas.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using NominaAPI.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Login>("Login");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class LoginController : ODataController
    {
        private Proyecto_Fin_Hibrido2Entities1 db = new Proyecto_Fin_Hibrido2Entities1();

        [HttpGet]
        [ODataRoute("Autenticar")]
        public IHttpActionResult Autenticar()
        {
            
            if (RequestContext.Principal.Identity.IsAuthenticated)
            {
                int id = int.Parse(ClaimsPrincipal.Current.Identities.First().Claims.Where(c => c.Type.Equals("id")).FirstOrDefault().Value);
                Login l = new Login();
                l = db.Login.Find(id);
                l.Password = "XXXXXXXXXX";
                return Ok(l);
            }
            else
            {
                return Unauthorized();
            }
        }


        // GET: odata/Login
        [EnableQuery]
        public IQueryable<Login> Get()
        {
            List<Login> encriptado = db.Login.ToList();
            encriptado.ForEach(p => p.Password = "XXXXXXXXX");
            return encriptado.AsQueryable();
        }

        // GET: odata/Login(5)
        [EnableQuery]
        public SingleResult<Login> Get([FromODataUri] int key)
        {
            List<Login> encriptado = db.Login.Where(login => login.id == key).ToList();
            encriptado.ForEach(p => p.Password = "XXXXXXXXX");
            return SingleResult.Create(encriptado.AsQueryable());
        }

        // PUT: odata/Login(5)
        [BasicAuthorize(Roles="admin")]
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Login update)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != update.id)
            {
                return BadRequest();
            }
            db.Entry(update).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoginExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(update);

        }

        // POST: odata/Login
        [BasicAuthorize(Roles = "admin")]
        public async Task<IHttpActionResult> Post(Login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Login.Add(login);
            await db.SaveChangesAsync();

            return Created(login);
        }

        // PATCH: odata/Login(5)
        [AcceptVerbs("PATCH", "MERGE")]
        [BasicAuthorize(Roles = "admin")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Login> patch)
        {
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Login login = await db.Login.FindAsync(key);

            if (login == null)
            {
                return NotFound();
            }

            patch.Patch(login);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoginExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(login);
        }

        // DELETE: odata/Login(5)
        [BasicAuthorize(Roles = "admin")]
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Login login = await db.Login.FindAsync(key);
            if (login == null)
            {
                return NotFound();
            }

            db.Login.Remove(login);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LoginExists(int key)
        {
            return db.Login.Any(e => e.id == key);
        }
    }
}
