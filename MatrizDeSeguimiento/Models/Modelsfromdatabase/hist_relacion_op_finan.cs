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
    
    public partial class hist_relacion_op_finan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public hist_relacion_op_finan()
        {
            this.hist_ingresos_gastos = new HashSet<hist_ingresos_gastos>();
        }
    
        public int añomescierre { get; set; }
        public int oportunidad_id_oportunidad { get; set; }
        public int financista_id_financista { get; set; }
        public Nullable<int> id_estado { get; set; }
        public string fecha_entrega_propuesta { get; set; }
        public string fecha_estimada_adjudicada { get; set; }
        public string codigo { get; set; }
        public string comentario { get; set; }
    
        public virtual hist_estado_oportunidad hist_estado_oportunidad { get; set; }
        public virtual hist_financista hist_financista { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hist_ingresos_gastos> hist_ingresos_gastos { get; set; }
        public virtual hist_oportunidad hist_oportunidad { get; set; }
    }
}
