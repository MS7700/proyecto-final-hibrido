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
    
    public partial class TipoDeduccion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoDeduccion()
        {
            this.Transaccion = new HashSet<Transaccion>();
        }
    
        public int id { get; set; }
        public string Nombre { get; set; }
        public bool Automatico { get; set; }
        public bool Porcentual { get; set; }
        public Nullable<double> Cantidad { get; set; }
        public bool Estado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaccion> Transaccion { get; set; }
    }
}
