//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication4.BaseDatos
{
    using System;
    using System.Collections.Generic;
    
    public partial class criterios_evaluacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public criterios_evaluacion()
        {
            this.atractivo_has_criterios_evaluacion = new HashSet<atractivo_has_criterios_evaluacion>();
        }
    
        public int id_Criterios_evaluacion { get; set; }
        public string nombre { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<atractivo_has_criterios_evaluacion> atractivo_has_criterios_evaluacion { get; set; }
    }
}
