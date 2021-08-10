﻿using System;
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
using System.Data.Entity.Validation;
using System.Data.Entity.Core;

namespace NominaAPI.Controllers
{


    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;

    using System.Web.Http.OData.Extensions;


    using NominaAPI.Models;

    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Nomina>("Nomina");

    builder.EntitySet<TipoNomina>("TipoNomina"); 

    builder.EntitySet<NominaResumen>("NominaResumen"); 


    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

    */

    public class NominaController : ODataController
    {
        private Proyecto_Fin_Hibrido2Entities1 db = new Proyecto_Fin_Hibrido2Entities1();

        // GET: odata/Nomina
        [EnableQuery]
        public IQueryable<Nomina> Get()
        {
            return db.Nomina;
        }

        // GET: odata/Nomina(5)
        [EnableQuery]
        public SingleResult<Nomina> Get([FromODataUri] int key)
        {
            return SingleResult.Create(db.Nomina.Where(nomina => nomina.id == key));
        }

        // PUT: odata/Nomina(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Nomina update)
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
                if (!NominaExists(key))
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

        // POST: odata/Nomina
        public async Task<IHttpActionResult> Post(Nomina nomina)
        {
            NominaResumen nominaResumen = new NominaResumen();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string prueba = "0";
            var date = nomina.Fecha;
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;

            if (month < 10)
            {
                prueba = prueba + month.ToString();
            }
            else{
                prueba = month.ToString();
            }
            System.Diagnostics.Debug.WriteLine(nomina.id + " " + nomina.TipoNominaID + " " + nomina.Fecha + " " + nomina.Contabilizado);
                //Arreglar formato
                nomina.Periodo = string.Format("{0}" + prueba, year);
                System.Diagnostics.Debug.WriteLine(nomina.Periodo);

            db.Nomina.Add(nomina);

            try
            {
                nominaResumen.NominaID = nomina.id;
                foreach (var e in db.Empleado.Where(a => a.TipoNominaID == nomina.TipoNominaID && a.Estado == true).ToList())
                {
                    System.Diagnostics.Debug.WriteLine(e.id + " " + e.Salario);
                    nominaResumen.EmpleadoID = e.id;
                    nominaResumen.SueldoBruto = e.Salario;
                    nominaResumen.SueldoDevengado = BuscarDevengado(e.id, e.Salario, nomina.Fecha);
                    db.NominaResumen.Add(nominaResumen);

                    await db.SaveChangesAsync();
                    await CrearNominaDetalleAsync(nominaResumen.id, e.id);
                }

            }

            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }

            }

            //   await db.SaveChangesAsync();


            return Created(nomina);
        }

        // PATCH: odata/Nomina(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Nomina> patch)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Nomina nomina = await db.Nomina.FindAsync(key);

            if (nomina == null)
            {
                return NotFound();
            }

            patch.Patch(nomina);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NominaExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(nomina);
        }

        // DELETE: odata/Nomina(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Nomina nomina = await db.Nomina.FindAsync(key);
            if (nomina == null)
            {
                return NotFound();
            }

            db.Nomina.Remove(nomina);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }


        // GET: odata/Nomina(5)/TipoNomina
        [EnableQuery]

        public SingleResult<TipoNomina> GetTipoNomina([FromODataUri] int key)

        {

            return SingleResult.Create(db.Nomina.Where(m => m.id == key).Select(m => m.TipoNomina));

        }


        // GET: odata/Nomina(5)/NominaResumen
        [EnableQuery]

        public IQueryable<NominaResumen> GetNominaResumen([FromODataUri] int key)

        {

            return db.Nomina.Where(m => m.id == key).SelectMany(m => m.NominaResumen);

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NominaExists(int key)
        {
            return db.Nomina.Any(e => e.id == key);
        }

        //Función donde se busca y calcula el sueldo devengado de cada empleado.
        private double BuscarDevengado(int id, double sueldo, DateTime fecha) {
            double deduccion = 0;
            double ingreso = 0;

            foreach (var e in db.Transaccion.Where(a => a.EmpleadoID == id 
                                                   && a.Fecha == fecha 
                                                   && a.TipoIngresoID != null))   
            {
                ingreso += e.Monto;
                e.Contabilizado = true;
            }

            foreach (var e in db.Transaccion.Where(a => a.EmpleadoID == id 
                                                   && a.Fecha == fecha 
                                                   && a.TipoDeduccionID != null)) 
            { 
                deduccion += e.Monto;
                e.Contabilizado = true;
            }

            return (sueldo + ingreso) - deduccion;
        }

        private async Task CrearNominaDetalleAsync(int id, int Emid) {
            NominaDetalle nominaDetalle = new NominaDetalle();

            nominaDetalle.NominaResumenID = id;

            foreach (var e in db.Transaccion.Where(a => a.EmpleadoID == Emid).ToList()) {

                if (e.TipoIngresoID != null)
                {

                    foreach (var f in db.TipoIngreso.Where(a => a.id == e.TipoIngresoID).ToList()) {
                        nominaDetalle.Descripción = f.Nombre;
                        System.Diagnostics.Debug.WriteLine(nominaDetalle.Descripción);
                        }
                    nominaDetalle.Tipo = "Ingreso";
                   
                    
                }
                else {

                    nominaDetalle.Tipo = "Deducción";
                   foreach (var f in db.TipoDeduccion.Where(a => a.id == e.TipoDeduccionID).ToList())
                    {
                        nominaDetalle.Descripción = f.Nombre;
                        System.Diagnostics.Debug.WriteLine(nominaDetalle.Descripción);
                    }
                }

                nominaDetalle.TransaccionID = e.id;
                nominaDetalle.Monto = e.Monto;
                db.NominaDetalle.Add(nominaDetalle);

                await db.SaveChangesAsync();
            }
            
        }
    }
}



/* try
               {
                   await db.SaveChangesAsync();
               }
               catch (DbEntityValidationException ex)
               {
                   foreach (var entityValidationErrors in ex.EntityValidationErrors)
                   {
                       foreach (var validationError in entityValidationErrors.ValidationErrors)
                       {
                           System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                       }
                   }
               }*/
