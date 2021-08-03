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


namespace NominaAPI.Controllers
{


    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

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

        // GET: odata/Login
        [EnableQuery]
        public IQueryable<Login> Get()
        {
            return db.Login;
        }

        // GET: odata/Login(5)
        [EnableQuery]
        public SingleResult<Login> Get([FromODataUri] int key)
        {
            return SingleResult.Create(db.Login.Where(login => login.id == key));
        }

        // PUT: odata/Login(5)
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
