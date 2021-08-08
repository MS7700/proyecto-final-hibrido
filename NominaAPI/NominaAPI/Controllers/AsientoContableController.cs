﻿
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
    builder.EntitySet<AsientoContable>("AsientoContable");

    builder.EntitySet<Cuenta>("Cuenta"); 


    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

    */

    public class AsientoContableController : ODataController
    {
        private Proyecto_Fin_Hibrido2Entities1 db = new Proyecto_Fin_Hibrido2Entities1();

        // GET: odata/AsientoContable
        [EnableQuery]
        public IQueryable<AsientoContable> Get()
        {
            return db.AsientoContable;
        }

        // GET: odata/AsientoContable(5)
        [EnableQuery]
        public SingleResult<AsientoContable> Get([FromODataUri] int key)
        {
            return SingleResult.Create(db.AsientoContable.Where(asientoContable => asientoContable.id == key));
        }

        // PUT: odata/AsientoContable(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, AsientoContable update)
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
                if (!AsientoContableExists(key))
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

        // POST: odata/AsientoContable
        public async Task<IHttpActionResult> Post(AsientoContable asientoContable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AsientoContable.Add(asientoContable);

            await db.SaveChangesAsync();


            return Created(asientoContable);
        }

        // PATCH: odata/AsientoContable(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<AsientoContable> patch)
        {
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AsientoContable asientoContable = await db.AsientoContable.FindAsync(key);

            if (asientoContable == null)
            {
                return NotFound();
            }

            patch.Patch(asientoContable);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsientoContableExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(asientoContable);
        }

        // DELETE: odata/AsientoContable(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            AsientoContable asientoContable = await db.AsientoContable.FindAsync(key);
            if (asientoContable == null)
            {
                return NotFound();
            }

            db.AsientoContable.Remove(asientoContable);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }


        // GET: odata/AsientoContable(5)/Cuenta
        [EnableQuery]

        public SingleResult<Cuenta> GetCuenta([FromODataUri] int key)

        {

            return SingleResult.Create(db.AsientoContable.Where(m => m.id == key).Select(m => m.Cuenta));

        }


        // GET: odata/AsientoContable(5)/Cuenta1
        [EnableQuery]

        public SingleResult<Cuenta> GetCuenta1([FromODataUri] int key)

        {

            return SingleResult.Create(db.AsientoContable.Where(m => m.id == key).Select(m => m.Cuenta1));

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AsientoContableExists(int key)
        {
            return db.AsientoContable.Any(e => e.id == key);
        }
    }
}
