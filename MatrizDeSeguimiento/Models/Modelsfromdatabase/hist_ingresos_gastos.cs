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
    
    public partial class hist_ingresos_gastos
    {
        public int añomescierre { get; set; }
        public int año { get; set; }
        public decimal monto_ingreso { get; set; }
        public decimal monto_gasto_hh { get; set; }
        public decimal monto_gasto { get; set; }
        public int Id_oportunidad { get; set; }
        public int Id_financista { get; set; }
    
        public virtual hist_relacion_op_finan hist_relacion_op_finan { get; set; }
    }
}
