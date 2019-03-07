using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Proyecto.Models
{
    public class FormularioViewModel
    {

        public int ID { get; set; }

        [Display(Name = "Área")]
        [Required]
        public string Area { get; set; }

        [Display(Name = "Línea")]
        //[Required]
        public string Linea { get; set; }

        //[Required]
        public string Iniciativa { get; set; }

        [Required]
        public string Oportunidad { get; set; }
        public string Financista { get; set; }

        public string Tipo { get; set; }

        public string Estado { get; set; }

        [Display(Name = "Fecha Entr")]
        public string FechaEntregaPropuesta { get; set; }
        [Display(Name = "Fecha Adj")]
        public string FechaAdjudicacionPropuesta { get; set; }
        [Display(Name = "Ingreso")]
        public decimal Ingreso { get; set; }
        [Display(Name = "Gasto HH")]
        public decimal GastoHH { get; set; }
        [Display(Name = "Gasto Op")]

        public decimal GastoOp { get; set; }
        [Display(Name = "Margen")]
        public decimal Margen { get; set; }

        [Display(Name = "Ingreso")]
        public decimal IngresoSig { get; set; }
        [Display(Name = "Gasto HH")]
        public decimal GastoHHSig { get; set; }
        [Display(Name = "Gasto Op")]
        public decimal GastoOpSig { get; set; }
        [Display(Name = "Margen")]
        public decimal MargenSig { get; set; }

        [Display(Name = "Ingreso")]
        public decimal IngresoSubS { get; set; }
        [Display(Name = "Gasto HH")]
        public decimal GastoHHSubS { get; set; }
        [Display(Name = "Gasto Op")]
        public decimal GastoOpSubS { get; set; }
        [Display(Name = "Margen")]
        public decimal MargenSubS { get; set; }

        [Display(Name = "Ingreso Total")]
        public decimal IngresoTotal => Ingreso + IngresoSig + IngresoSubS;
        [Display(Name = "Gasto Total")]
        public decimal GastoTotal => GastoHH + GastoOp + GastoHHSig + GastoOpSig + GastoOpSubS + GastoHHSubS;

        [Display(Name = "Código")]
        [StringLength(10)]
        public string Codigo { get; set; }
        [StringLength(50)]
        public string Comentario { get; set; }

        public IList<DropDownListViewModel> Estados { get; set; }
        public IList<DropDownListViewModel> Areas { get; set; }
        public IList<DropDownListViewModel> Lineas { get; set; }
        public IList<DropDownListViewModel> Iniciativas { get; set; }
        public IList<DropDownListViewModel> Financistas { get; set; }
        public IList<DropDownListViewModel> Tipos { get; set; }
    }

    public class FormularioDBContext : DbContext
    {
        public DbSet<FormularioViewModel> Formulario { get; set; }
    }
}