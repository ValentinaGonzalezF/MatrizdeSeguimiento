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
    
    public partial class relacion_op_finan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public relacion_op_finan()
        {
            this.ingresos_gastos = new HashSet<ingresos_gastos>();
        }
    
        public int oportunidad_id_oportunidad { get; set; }
        public int financista_id_financista { get; set; }
        public Nullable<int> id_estado { get; set; }
        public string fecha_entrega_propuesta { get; set; }
        public string fecha_estimada_adjudicada { get; set; }
        public string codigo { get; set; }
        public string comentario { get; set; }
    
        public virtual estado_oportunidad estado_oportunidad { get; set; }
        public virtual financista financista { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ingresos_gastos> ingresos_gastos { get; set; }
        public virtual oportunidad oportunidad { get; set; }
    }
}