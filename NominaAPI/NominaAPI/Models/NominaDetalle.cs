
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
    using System.Collections.Generic;
    
public partial class NominaDetalle
{

    public int id { get; set; }

    public int NominaResumenID { get; set; }

    public int TransaccionID { get; set; }

    public string Tipo { get; set; }

    public string Descripcion { get; set; }

    public double Monto { get; set; }



    public virtual NominaResumen NominaResumen { get; set; }

    public virtual Transaccion Transaccion { get; set; }

}

}
