using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MatrizDeSeguimiento.Controllers
{
    public class FormularioViewModelsController : Controller
    {
        //private Proyecto.Models.FormularioDBContext db = new FormularioDBContext();
        private Models.Modelsfromdatabase.FCHmatrizdeseguimientoEntities1 db1 = new Models.Modelsfromdatabase.FCHmatrizdeseguimientoEntities1();
        protected static Proyecto.Models.Users.User usuario_conectado;
        protected static string nombreop;
        protected static int idfinan;
        protected static int idinicia;
        public ActionResult CerrarSesion()
        {
            return RedirectToAction("SignIn", "Home");
        }
        // GET: FormularioViewModel
        public ActionResult Index(string areadato)
        {
            string nombre_usuario = Session["nombre_usuario"].ToString();
            string pass_usuario = Session["contra"].ToString();
            List<FormularioViewModel> formularios = new List<FormularioViewModel>();
            //var filasformulario = db1.relacion_op_finan.ToList();
            var filasformulario = db1.MatrizSeguimiento.ToList();
            //Buscar Usuario 
            var usuario = db1.usuario.Where(x => x.nombre_usuario == nombre_usuario).First();
            Session["id_perfil_usuario"] = usuario.id_perfil;
            Session["id_area_perfil"] = usuario.area_id_area;
            //Si es controller y no es la area que le corresponde
            var id_perfilcontroller = db1.perfil.Where(x => x.nombre == "CONTROLLER").Select(x => x.id_perfil).First();
            var id_perfiladmin = db1.perfil.Where(x => x.nombre == "ADMIN").Select(x => x.id_perfil).First();
            var id_perfilvisualizador = db1.perfil.Where(x => x.nombre == "VISUALIZADOR").Select(x => x.id_perfil).First();
            //Lista de nombre de areas
            var areaLst = new List<string>();
            var areasQry = from d in db1.area
                           orderby d.nombre_area
                           select d.nombre_area;
            if (id_perfilcontroller == usuario.id_perfil)
            {
                areasQry = from d in db1.area
                           orderby d.nombre_area
                           where d.id_area == usuario.area_id_area
                           select d.nombre_area;
            }
            areaLst.AddRange(areasQry.Distinct());
            ViewBag.areadato = new SelectList(areaLst);

            foreach (var fila in filasformulario)
            {
                var id_area = db1.area.Where(x => x.nombre_area == fila.Area).Select(x => x.id_area).First();
                if ((id_perfilcontroller == usuario.id_perfil && id_area != usuario.area_id_area))
                {//Ir a otra fila 
                    continue;
                }
                //Si es controller y le corresponde el area  O si es admin 
                else
                {
                    FormularioViewModel form = new FormularioViewModel();
                    form.Area = fila.Area;
                    form.Linea = fila.Linea;
                    form.Iniciativa = fila.Iniciativa;
                    form.Oportunidad = fila.Oportunidad;
                    form.Financista = fila.Financista;
                    form.Tipo = fila.Tipo;
                    form.Estado = fila.Estado;
                    //Fecha de Entrega
                    form.FechaEntregaPropuesta = fila.FechaEntregaPropuesta;
                    //Fecha Adjudicada
                    form.FechaAdjudicacionPropuesta = fila.FechaAdjudicacionPropuesta;
                    //Codigo
                    if (fila.Codigo != null)
                    {
                        form.Codigo = fila.Codigo;
                    }
                    if (fila.Comentario != null)
                    {
                        form.Comentario = fila.Comentario;
                    }
                    form.Ingreso = fila.Ingreso;
                    form.GastoHH = fila.GastoHH;
                    form.GastoOp = fila.GastoOP;
                    form.Margen = fila.Margen;
                    //Selecciona datos del año siguiente
                    form.IngresoSig = fila.IngresoSig;
                    form.GastoHHSig = fila.GastoHHSig;
                    form.GastoOpSig = fila.GastoOPSig;
                    form.MargenSig = fila.MargenSig;
                    //Selecciona datos del año subsiguiente
                    form.IngresoSubS = fila.IngresoSubS;
                    form.GastoHHSubS = fila.GastoHHSubS;
                    form.GastoOpSubS = fila.GastoOPSubS;
                    form.MargenSubS = form.MargenSubS;

                    formularios.Add(form);
                }
            }
            if (!string.IsNullOrEmpty(areadato))
            {
                formularios = formularios.Where(x => x.Area == areadato).ToList();
            }
            return View(formularios);
        }

        [HttpPost]
        public string Index(FormCollection fc, string searchString)
        {

            return "<h3> From [HttpPost]Index: " + searchString + "</h3>";
        }


        // GET: FormularioViewModel/Create
        public ActionResult Create(string linea)
        {
            int id_perfilusuario = int.Parse(Session["id_perfil_usuario"].ToString());
            int id_areaperfil = int.Parse(Session["id_area_perfil"].ToString());
            //Si es controller y no es la area que le corresponde
            int id_perfilcontroller = db1.perfil.Where(x => x.nombre == "CONTROLLER").Select(x => x.id_perfil).First();
            int id_perfiladmin = db1.perfil.Where(x => x.nombre == "ADMIN").Select(x => x.id_perfil).First();
            int id_perfilvisualizador = db1.perfil.Where(x => x.nombre == "VISUALIZADOR").Select(x => x.id_perfil).First();
            var formulario = new FormularioViewModel();
            if (id_perfilusuario == id_perfilcontroller)
            {
                var areascontroller = db1.area.Where(x => x.id_area == id_areaperfil).OrderBy(x => x.id_area)
                    .Select(x => new DropDownListViewModel()
                    {
                        Id = x.id_area,
                        Name = x.nombre_area
                    }).ToList();

                formulario.Areas = areascontroller;
            }
            else
            {
                //Lista Areas
                var areas = db1.area
                    .OrderBy(x => x.id_area)
                    .Select(x => new DropDownListViewModel()
                    {
                        Id = x.id_area,
                        Name = x.nombre_area
                    }).ToList();
                formulario.Areas = areas;
            }
            //Lista de Estados
            var estados = db1.estado_oportunidad
                .OrderBy(x => x.id_estado)
                .Select(x => new DropDownListViewModel()
                {
                    Id = x.id_estado,
                    Name = x.nombre_estado
                }).ToList();

            formulario.Estados = estados;
            //Lista Financistas
            var financistas = db1.financista
                .OrderBy(x => x.id_financista)
                .Select(x => new DropDownListViewModel()
                {
                    Id = x.id_financista,
                    Name = x.nombre_financista
                }).ToList();

            formulario.Financistas = financistas;
            return View(formulario);
        }


        // POST: FormularioViewModel/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FormularioViewModel formulario)
        {
            var idini = int.Parse(formulario.Iniciativa);
            var oport = db1.oportunidad.Where(x => x.nombre_oportunidad == formulario.Oportunidad).Where(x=>x.id_iniciativa==idini).FirstOrDefault();
            Models.Modelsfromdatabase.relacion_op_finan op_fin = new Models.Modelsfromdatabase.relacion_op_finan();
            if (oport == null)
            {
                //TABLA OPORTUNIDAD
                Models.Modelsfromdatabase.oportunidad op = new Models.Modelsfromdatabase.oportunidad();
                op.id_iniciativa = int.Parse(formulario.Iniciativa);
                op.nombre_oportunidad = formulario.Oportunidad;
                db1.oportunidad.Add(op);
                await db1.SaveChangesAsync();

            }
            var idOportunidad = db1.oportunidad.Where(x => x.nombre_oportunidad == formulario.Oportunidad).Where(x=>x.id_iniciativa==idini).Select(x=>x.id_oportunidad).First();
            op_fin.oportunidad_id_oportunidad = idOportunidad;
             
            //TABLA RELACION OPORTUNIDAD-FINANCISTA
           

            //Buscar id de estado segun nombre
            op_fin.id_estado = int.Parse(formulario.Estado);

            //Buscar id financista
            op_fin.financista_id_financista = int.Parse(formulario.Financista);
            op_fin.fecha_estimada_adjudicada = formulario.FechaAdjudicacionPropuesta;
            op_fin.fecha_entrega_propuesta = formulario.FechaEntregaPropuesta;
            op_fin.comentario = formulario.Comentario;
            op_fin.codigo = formulario.Codigo;
            db1.relacion_op_finan.Add(op_fin);
            await db1.SaveChangesAsync();
            //TABLA INGRESO-GASTOS
            Models.Modelsfromdatabase.ingresos_gastos ing_gastActual = new Models.Modelsfromdatabase.ingresos_gastos();
            Models.Modelsfromdatabase.ingresos_gastos ing_gastSiguiente = new Models.Modelsfromdatabase.ingresos_gastos();
            Models.Modelsfromdatabase.ingresos_gastos ing_gastSubSiguiente = new Models.Modelsfromdatabase.ingresos_gastos();
            int actualyear = DateTime.Today.Year;
            //Selecciona datos de año actual
            ing_gastActual.año = actualyear;
            ing_gastActual.monto_ingreso = formulario.Ingreso;
            ing_gastActual.monto_gasto_hh = formulario.GastoHH;
            ing_gastActual.monto_gasto = formulario.GastoOp;
            ing_gastActual.Id_oportunidad = idOportunidad;
            ing_gastActual.Id_financista = int.Parse(formulario.Financista);
            db1.ingresos_gastos.Add(ing_gastActual);
            //Selecciona datos del año siguiente
            ing_gastSiguiente.año = actualyear + 1;
            ing_gastSiguiente.monto_ingreso = formulario.IngresoSig;
            ing_gastSiguiente.monto_gasto_hh = formulario.GastoHHSig;
            ing_gastSiguiente.monto_gasto = formulario.GastoOpSig;
            ing_gastSiguiente.Id_oportunidad = idOportunidad;
            ing_gastSiguiente.Id_financista = int.Parse(formulario.Financista);
            db1.ingresos_gastos.Add(ing_gastSiguiente);
            //Selecciona datos del año subsiguiente
            ing_gastSubSiguiente.año = actualyear + 2;
            ing_gastSubSiguiente.monto_ingreso = formulario.IngresoSubS;
            ing_gastSubSiguiente.monto_gasto_hh = formulario.GastoHHSubS;
            ing_gastSubSiguiente.monto_gasto = formulario.GastoOpSubS;
            ing_gastSubSiguiente.Id_oportunidad = idOportunidad;
            ing_gastSubSiguiente.Id_financista = int.Parse(formulario.Financista);
            db1.ingresos_gastos.Add(ing_gastSubSiguiente);
            await db1.SaveChangesAsync();

            return RedirectToAction("Index", usuario_conectado);
        }


        // GET: FormularioViewModel/Details/5
        public async Task<ActionResult> Details(string nombre_financista, string nombre_oportunidad, string nombre_area, string nombre_linea, string nombre_iniciativa)
        {

            if (nombre_financista == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Models.Modelsfromdatabase.MatrizSeguimiento matrizseg = db1.MatrizSeguimiento.Where(x => x.Financista == nombre_financista).Where(x => x.Oportunidad == nombre_oportunidad).Where(x => x.Area == nombre_area).Where(x => x.Linea == nombre_linea).Where(x => x.Iniciativa == nombre_iniciativa).First();
            if (matrizseg == null)
            {
                return HttpNotFound();
            }
            FormularioViewModel formulario = new FormularioViewModel();
            //Traer formulario de la base de datos
            formulario.Area = matrizseg.Area;
            formulario.Linea = matrizseg.Linea;
            formulario.Iniciativa = matrizseg.Iniciativa;
            formulario.Oportunidad = nombre_oportunidad;
            formulario.Financista = nombre_financista;
            formulario.Tipo = matrizseg.Tipo;
            formulario.Estado = matrizseg.Estado;
            //Fecha de Entrega
            formulario.FechaEntregaPropuesta = matrizseg.FechaEntregaPropuesta;
            //Fecha Adjudicada
            formulario.FechaAdjudicacionPropuesta = matrizseg.FechaAdjudicacionPropuesta;
            //Codigo
            if (matrizseg.Codigo != null)
            {
                formulario.Codigo = matrizseg.Codigo;
            }
            if (matrizseg.Comentario != null)
            {
                formulario.Comentario = matrizseg.Comentario;
            }
            formulario.Ingreso = matrizseg.Ingreso;
            formulario.GastoHH = matrizseg.GastoHH;
            formulario.GastoOp = matrizseg.GastoOP;
            formulario.Margen = matrizseg.Margen;
            //Selecciona datos del año siguiente
            formulario.IngresoSig = matrizseg.IngresoSig;
            formulario.GastoHHSig = matrizseg.GastoHHSig;
            formulario.GastoOpSig = matrizseg.GastoOPSig;
            formulario.MargenSig = matrizseg.MargenSig;
            //Selecciona datos del año subsiguiente
            formulario.IngresoSubS = matrizseg.IngresoSubS;
            formulario.GastoHHSubS = matrizseg.GastoHHSubS;
            formulario.GastoOpSubS = matrizseg.GastoOPSubS;
            formulario.MargenSubS = matrizseg.MargenSubS;

            return View(formulario);
        }

        // GET: FormularioViewModel/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(string nombre_financista, string nombre_oportunidad, string nombre_area, string nombre_linea, string nombre_iniciativa)
        {

            if (nombre_financista == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Models.Modelsfromdatabase.MatrizSeguimiento matrizseg = db1.MatrizSeguimiento.Where(x => x.Financista == nombre_financista).Where(x => x.Oportunidad == nombre_oportunidad).Where(x => x.Area == nombre_area).Where(x => x.Linea == nombre_linea).Where(x => x.Iniciativa == nombre_iniciativa).First();
            if (matrizseg == null)
            {
                return HttpNotFound();
            }
            int id_perfilusuario = int.Parse(Session["id_perfil_usuario"].ToString());
            int id_areaperfil = int.Parse(Session["id_area_perfil"].ToString());
            //Si es controller y no es la area que le corresponde
            int id_perfilcontroller = db1.perfil.Where(x => x.nombre == "CONTROLLER").Select(x => x.id_perfil).First();
            int id_perfiladmin = db1.perfil.Where(x => x.nombre == "ADMIN").Select(x => x.id_perfil).First();
            int id_perfilvisualizador = db1.perfil.Where(x => x.nombre == "VISUALIZADOR").Select(x => x.id_perfil).First();
            var formulario = new FormularioViewModel();
            if (id_perfilusuario == id_perfilcontroller)
            {
                var areascontroller = db1.area.Where(x => x.id_area == id_areaperfil).OrderBy(x => x.id_area)
                    .Select(x => new DropDownListViewModel()
                    {
                        Id = x.id_area,
                        Name = x.nombre_area
                    }).ToList();

                formulario.Areas = areascontroller;
            }
            else
            {
                //Lista Areas
                var areas = db1.area
                    .OrderBy(x => x.id_area)
                    .Select(x => new DropDownListViewModel()
                    {
                        Id = x.id_area,
                        Name = x.nombre_area
                    }).ToList();
                formulario.Areas = areas;
            }
            //Lista de Estados
            var estados = db1.estado_oportunidad
                .OrderBy(x => x.id_estado)
                .Select(x => new DropDownListViewModel()
                {
                    Id = x.id_estado,
                    Name = x.nombre_estado
                }).ToList();

            formulario.Estados = estados;
            //Lista Financistas
            var financistas = db1.financista
                .OrderBy(x => x.id_financista)
                .Select(x => new DropDownListViewModel()
                {
                    Id = x.id_financista,
                    Name = x.nombre_financista
                }).ToList();

            formulario.Financistas = financistas;
            //Traer formulario de la base de datos
            //formulario.Area = matrizseg.Area;
            ViewBag.id_area = db1.area.Where(x => x.nombre_area == matrizseg.Area).Select(x => x.id_area).First();
            //formulario.Linea = matrizseg.Linea;
            ViewBag.id_linea = db1.linea.Where(x => x.nombre_linea == matrizseg.Linea).Select(x => x.id_linea).First();
            //formulario.Iniciativa = matrizseg.Iniciativa;
            ViewBag.id_iniciativa = db1.iniciativa.Where(x => x.nombre_iniciativa == matrizseg.Iniciativa).Select(x => x.id_iniciativa).First();
            formulario.Oportunidad = nombre_oportunidad;
            ViewBag.nombreOportunidad = nombre_oportunidad;
            nombreop = nombre_oportunidad;
            idinicia = db1.iniciativa.Where(x => x.nombre_iniciativa == nombre_iniciativa).Select(x => x.id_iniciativa).First();
            idfinan = db1.financista.Where(x => x.nombre_financista == matrizseg.Financista).Select(x => x.id_financista).First();
            //formulario.Financista = nombre_financista;
            ViewBag.id_financista = db1.financista.Where(x => x.nombre_financista == matrizseg.Financista).Select(x => x.id_financista).First();
            //formulario.Tipo = matrizseg.Tipo;
            ViewBag.id_tipo = db1.tipo_financista.Where(x => x.nombre_tipo == matrizseg.Tipo).Select(x => x.id_tipofinancista).First();
            //formulario.Estado = matrizseg.Estado;
            ViewBag.id_estado = db1.estado_oportunidad.Where(x => x.nombre_estado == matrizseg.Estado).Select(x => x.id_estado).First();
            //Fecha de Entrega
            formulario.FechaEntregaPropuesta = matrizseg.FechaEntregaPropuesta;
            //Fecha Adjudicada
            formulario.FechaAdjudicacionPropuesta = matrizseg.FechaAdjudicacionPropuesta;
            //Codigo
            if (matrizseg.Codigo != null)
            {
                formulario.Codigo = matrizseg.Codigo;
            }
            if (matrizseg.Comentario != null)
            {
                formulario.Comentario = matrizseg.Comentario;
            }
            formulario.Ingreso = matrizseg.Ingreso;
            formulario.GastoHH = matrizseg.GastoHH;
            formulario.GastoOp = matrizseg.GastoOP;
            formulario.Margen = matrizseg.Margen;
            //Selecciona datos del año siguiente
            formulario.IngresoSig = matrizseg.IngresoSig;
            formulario.GastoHHSig = matrizseg.GastoHHSig;
            formulario.GastoOpSig = matrizseg.GastoOPSig;
            formulario.MargenSig = matrizseg.MargenSig;
            //Selecciona datos del año subsiguiente
            formulario.IngresoSubS = matrizseg.IngresoSubS;
            formulario.GastoHHSubS = matrizseg.GastoHHSubS;
            formulario.GastoOpSubS = matrizseg.GastoOPSubS;
            formulario.MargenSubS = matrizseg.MargenSubS;
            return View(formulario);
        }

        // POST: FormularioViewModel/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(FormularioViewModel formulario)
        {
            var idini = idinicia;
            //Borrar Registro
            var idfin = idfinan;
            var idopor = db1.oportunidad.Where(x => x.nombre_oportunidad == nombreop).Where(x=>x.id_iniciativa==idini).Select(x => x.id_oportunidad).First();
            int actualyear = DateTime.Today.Year;

            Models.Modelsfromdatabase.ingresos_gastos ing_gastosactual= db1.ingresos_gastos.Where(x => x.Id_oportunidad == idopor).Where(x => x.Id_financista == idfin).Where(x=>x.año==actualyear).FirstOrDefault();
            if (ing_gastosactual!=null) { db1.ingresos_gastos.Remove(ing_gastosactual); }
       
            Models.Modelsfromdatabase.ingresos_gastos ing_gastossig = db1.ingresos_gastos.Where(x => x.Id_oportunidad == idopor).Where(x => x.Id_financista == idfin).Where(x => x.año == actualyear+1).FirstOrDefault();

            if (ing_gastossig!= null) { db1.ingresos_gastos.Remove(ing_gastossig); }
                Models.Modelsfromdatabase.ingresos_gastos ing_gastossubs = db1.ingresos_gastos.Where(x => x.Id_oportunidad == idopor).Where(x => x.Id_financista == idfin).Where(x => x.año == actualyear+2).FirstOrDefault();
            if (ing_gastossubs != null) { db1.ingresos_gastos.Remove(ing_gastossubs); }
            
            await db1.SaveChangesAsync();

            Models.Modelsfromdatabase.relacion_op_finan op_fin =db1.relacion_op_finan.Where(x => x.oportunidad_id_oportunidad==idopor).Where(x=>x.financista_id_financista==idfin).FirstOrDefault();
            db1.relacion_op_finan.Remove(op_fin);
            await db1.SaveChangesAsync();

            //Agregar nuevo registro
            int id_iniciativa = int.Parse(formulario.Iniciativa);
            var oport = db1.oportunidad.Where(x => x.nombre_oportunidad == formulario.Oportunidad).Where(x=>x.id_iniciativa==id_iniciativa).FirstOrDefault();
            var idOportunidad = 0;
            
            if (oport == null){
                //TABLA OPORTUNIDAD
                Models.Modelsfromdatabase.oportunidad op = new Models.Modelsfromdatabase.oportunidad();
                op.id_iniciativa = id_iniciativa;
                op.nombre_oportunidad = formulario.Oportunidad;
                db1.oportunidad.Add(op);
                await db1.SaveChangesAsync();
                idOportunidad = db1.oportunidad.Where(x => x.nombre_oportunidad == formulario.Oportunidad).Where(x=>x.id_iniciativa==id_iniciativa).Select(x => x.id_oportunidad).First();
            }
            else{
                idOportunidad = db1.oportunidad.Where(x => x.nombre_oportunidad == formulario.Oportunidad).Where(x=>x.id_iniciativa==id_iniciativa).Select(x => x.id_oportunidad).First();
            }

            //TABLA RELACION OPORTUNIDAD-FINANCISTA
            Models.Modelsfromdatabase.relacion_op_finan nueva_op_fin = new Models.Modelsfromdatabase.relacion_op_finan();

            //Buscar id de estado segun nombre
            nueva_op_fin.id_estado = int.Parse(formulario.Estado);


            //Buscar id de oportunidad segun nombre

            nueva_op_fin.oportunidad_id_oportunidad = idOportunidad;

            //Buscar id financista
            nueva_op_fin.financista_id_financista = int.Parse(formulario.Financista);
            nueva_op_fin.fecha_estimada_adjudicada = formulario.FechaAdjudicacionPropuesta;
            nueva_op_fin.fecha_entrega_propuesta = formulario.FechaEntregaPropuesta;
            nueva_op_fin.comentario = formulario.Comentario;
            nueva_op_fin.codigo = formulario.Codigo;
            db1.relacion_op_finan.Add(nueva_op_fin);
            await db1.SaveChangesAsync();
            //TABLA INGRESO-GASTOS
            Models.Modelsfromdatabase.ingresos_gastos ing_gastActual = new Models.Modelsfromdatabase.ingresos_gastos();
            Models.Modelsfromdatabase.ingresos_gastos ing_gastSiguiente = new Models.Modelsfromdatabase.ingresos_gastos();
            Models.Modelsfromdatabase.ingresos_gastos ing_gastSubSiguiente = new Models.Modelsfromdatabase.ingresos_gastos();
            //Selecciona datos de año actual
            ing_gastActual.año = actualyear;
            ing_gastActual.monto_ingreso = formulario.Ingreso;
            ing_gastActual.monto_gasto_hh = formulario.GastoHH;
            ing_gastActual.monto_gasto = formulario.GastoOp;
            ing_gastActual.Id_oportunidad = idOportunidad;
            ing_gastActual.Id_financista = int.Parse(formulario.Financista);
            db1.ingresos_gastos.Add(ing_gastActual);
            //Selecciona datos del año siguiente
            ing_gastSiguiente.año = actualyear + 1;
            ing_gastSiguiente.monto_ingreso = formulario.IngresoSig;
            ing_gastSiguiente.monto_gasto_hh = formulario.GastoHHSig;
            ing_gastSiguiente.monto_gasto = formulario.GastoOpSig;
            ing_gastSiguiente.Id_oportunidad = idOportunidad;
            ing_gastSiguiente.Id_financista = int.Parse(formulario.Financista);
            db1.ingresos_gastos.Add(ing_gastSiguiente);
            //Selecciona datos del año subsiguiente
            ing_gastSubSiguiente.año = actualyear + 2;
            ing_gastSubSiguiente.monto_ingreso = formulario.IngresoSubS;
            ing_gastSubSiguiente.monto_gasto_hh = formulario.GastoHHSubS;
            ing_gastSubSiguiente.monto_gasto = formulario.GastoOpSubS;
            ing_gastSubSiguiente.Id_oportunidad = idOportunidad;
            ing_gastSubSiguiente.Id_financista = int.Parse(formulario.Financista);
            db1.ingresos_gastos.Add(ing_gastSubSiguiente);
            await db1.SaveChangesAsync();


            return RedirectToAction("Index");
        }


        /* // GET: FormularioViewModel/Delete/5
         public async Task<ActionResult> Delete(int? id)
         {
             if (id == null)
             {
                 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }
             FormularioViewModel formulario = await db.Formulario.FindAsync(id);
             if (formulario == null)
             {
                 return HttpNotFound();
             }
             return View(formulario);
         }

         // POST: FormularioViewModel/Delete/5
         [HttpPost, ActionName("Delete")]
         [ValidateAntiForgeryToken]
         public async Task<ActionResult> DeleteConfirmed(int id)
         {
             FormularioViewModel formulario = await db.Formulario.FindAsync(id);
             db.Formulario.Remove(formulario);
             await db.SaveChangesAsync();
             return RedirectToAction("Index");
         }
         */
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult GetLineaList(int id_area)
        {
            List<Models.Modelsfromdatabase.linea> lineas = db1.linea.Where(x => x.id_area == id_area).ToList();
            ViewBag.LineaList = new SelectList(lineas, "id_linea", "nombre_linea");
            return PartialView("DisplayLineas");
        }


        public ActionResult GetIniciativaList(int id_linea)
        {
            List<Models.Modelsfromdatabase.iniciativa> iniciativas = db1.iniciativa.Where(x => x.linea_id_linea == id_linea).ToList();
            ViewBag.IniciativaList = new SelectList(iniciativas, "id_iniciativa", "nombre_iniciativa");
            return PartialView("DisplayIniciativas");
        }

        public ActionResult GetTipoFinancista(int id_financista)
        {
            int id_tipofinancista = db1.financista.Where(x => x.id_financista == id_financista).Select(x => x.Id_tipo).First();
            List<Models.Modelsfromdatabase.tipo_financista> tipofinancista = db1.tipo_financista.Where(x => x.id_tipofinancista == id_tipofinancista).ToList();
            ViewBag.TipoFinancistaList = new SelectList(tipofinancista, "id_tipofinancista", "nombre_tipo");
            return PartialView("DisplayTipoFinancista");
        }


    }
}