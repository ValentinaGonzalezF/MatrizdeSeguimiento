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
    
    public partial class hist_linea
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public hist_linea()
        {
            this.hist_iniciativa = new HashSet<hist_iniciativa>();
        }
    
        public int añomescierre { get; set; }
        public int id_linea { get; set; }
        public string nombre_linea { get; set; }
        public int id_area { get; set; }
    
        public virtual hist_area hist_area { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hist_iniciativa> hist_iniciativa { get; set; }
        public virtual hist_ppto_ingresos_gastos hist_ppto_ingresos_gastos { get; set; }
    }
}