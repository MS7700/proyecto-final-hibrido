﻿

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace NominaAPI.Models
{

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class Proyecto_Fin_Hibrido2Entities1 : DbContext
{
    public Proyecto_Fin_Hibrido2Entities1()
        : base("name=Proyecto_Fin_Hibrido2Entities1")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<AsientoContable> AsientoContable { get; set; }

    public virtual DbSet<Cuenta> Cuenta { get; set; }

    public virtual DbSet<Departamento> Departamento { get; set; }

    public virtual DbSet<Empleado> Empleado { get; set; }

    public virtual DbSet<Nomina> Nomina { get; set; }

    public virtual DbSet<NominaDetalle> NominaDetalle { get; set; }

    public virtual DbSet<NominaResumen> NominaResumen { get; set; }

    public virtual DbSet<Puesto> Puesto { get; set; }

    public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

    public virtual DbSet<TipoDeduccion> TipoDeduccion { get; set; }

    public virtual DbSet<TipoIngreso> TipoIngreso { get; set; }

    public virtual DbSet<TipoNomina> TipoNomina { get; set; }

    public virtual DbSet<Transaccion> Transaccion { get; set; }

}

}

