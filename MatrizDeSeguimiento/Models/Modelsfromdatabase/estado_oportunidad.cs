//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MatrizDeSeguimiento.Models.Modelsfromdatabase
{
    using System;
    using System.Collections.Generic;
    
    public partial class estado_oportunidad
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public estado_oportunidad()
        {
            this.relacion_op_finan = new HashSet<relacion_op_finan>();
        }
    
        public string nombre_estado { get; set; }
        public int id_estado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<relacion_op_finan> relacion_op_finan { get; set; }
    }
}