//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyecto_Fin_Hibrido
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    
    public partial class Nomina
    {
        public Nomina()
        {
            this.Empleado = new HashSet<Empleado>();
        }
    
        public int IdNomina { get; set; }
        public string Nomina1 { get; set; }
        [JsonIgnore]
        public virtual ICollection<Empleado> Empleado { get; set; }
    }
}
