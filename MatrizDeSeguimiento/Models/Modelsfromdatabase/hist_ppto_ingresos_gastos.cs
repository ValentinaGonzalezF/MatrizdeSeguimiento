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
    
    public partial class hist_ppto_ingresos_gastos
    {
        public int añomescierre { get; set; }
        public int id_linea { get; set; }
        public int año { get; set; }
        public decimal monto_ppto_hh { get; set; }
        public decimal monto_ppto_gasto { get; set; }
        public decimal monto_ppto_ingreso { get; set; }
    
        public virtual hist_linea hist_linea { get; set; }
    }
}
