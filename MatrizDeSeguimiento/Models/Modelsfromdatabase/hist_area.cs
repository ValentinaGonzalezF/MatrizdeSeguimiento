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
    
    public partial class hist_area
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public hist_area()
        {
            this.hist_linea = new HashSet<hist_linea>();
        }
    
        public int añomescierre { get; set; }
        public int id_area { get; set; }
        public string nombre_area { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hist_linea> hist_linea { get; set; }
    }
}
