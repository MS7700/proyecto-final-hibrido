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
using Microsoft.AspNet.OData.Routing;
using System.Security.Claims;
using NominaAPI.Attributes;

namespace NominaAPI.Controllers
{
    /*
    Puede que la clase WebApiConfig requiera cambios adicionales para agregar una ruta para este controlador. Combine estas instrucciones en el método Register de la clase WebApiConfig según corresponda. Tenga en cuenta que las direcciones URL de OData distinguen mayúsculas de minúsculas.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using NominaAPI.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Usuario>("Usuario");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class UsuarioController : ODataController
    {
        private Proyecto_Fin_Hibrido2Entities1 db = new Proyecto_Fin_Hibrido2Entities1();

        [HttpGet]
        [ODataRoute("Autenticar")]
        public IHttpActionResult Autenticar()
        {

            if (RequestContext.Principal.Identity.IsAuthenticated)
            {
                int id = int.Parse(ClaimsPrincipal.Current.Identities.First().Claims.Where(c => c.Type.Equals("id")).FirstOrDefault().Value);
                Usuario l = new Usuario();
                l = db.Usuario.Find(id);
                l.Password = "XXXXXXXXXX";
                return Ok(l);
            }
            else
            {
                return Unauthorized();
            }
        }

        // GET: odata/Usuarios
        [EnableQuery]
        public IQueryable<Usuario> Get()
        {
            List<Usuario> encriptado = db.Usuario.ToList();
            encriptado.ForEach(p => p.Password = "XXXXXXXXX");
            return encriptado.AsQueryable();
        }

        // GET: odata/Usuarios(5)
        [EnableQuery]
        public SingleResult<Usuario> Get([FromODataUri] int key)
        {
            List<Usuario> encriptado = db.Usuario.Where(usuario => usuario.id == key).ToList();
            encriptado.ForEach(p => p.Password = "XXXXXXXXX");
            return SingleResult.Create(encriptado.AsQueryable());
        }

        // PUT: odata/Usuarios(5)
        [BasicAuthorize(Roles = "admin")]
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Usuario update)
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
                if (!UsuarioExists(key))
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

        // POST: odata/Usuarios
        [BasicAuthorize(Roles = "admin")]
        public async Task<IHttpActionResult> Post(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Usuario.Add(usuario);
            await db.SaveChangesAsync();

            return Created(usuario);
        }

        // PATCH: odata/Usuarios(5)
        [AcceptVerbs("PATCH", "MERGE")]
        [BasicAuthorize(Roles = "admin")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Usuario> patch)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Usuario usuario = await db.Usuario.FindAsync(key);

            if (usuario == null)
            {
                return NotFound();
            }

            patch.Patch(usuario);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(usuario);
        }

        // DELETE: odata/Usuarios(5)
        [BasicAuthorize(Roles = "admin")]
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Usuario usuario = await db.Usuario.FindAsync(key);
            if (usuario == null)
            {
                return NotFound();
            }

            db.Usuario.Remove(usuario);
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

        private bool UsuarioExists(int key)
        {
            return db.Usuario.Any(e => e.id == key);
        }
    }
}
