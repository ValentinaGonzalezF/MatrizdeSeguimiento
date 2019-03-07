using Proyecto.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MatrizDeSeguimiento.Controllers
{
    public class HomeController : Controller
    {
        private Models.Modelsfromdatabase.FCHmatrizdeseguimientoEntities1 db1 = new Models.Modelsfromdatabase.FCHmatrizdeseguimientoEntities1();


        public ActionResult SignIn()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(User user)
        {
            if (ModelState.IsValid)
            {
                var usuario = db1.usuario.Where(x => x.nombre_usuario == user.Usuario);
                if (usuario.Count() > 0)
                {
                    if (!(usuario.Select(x => x.nombre).First() is null))
                    {
                        string contraseña = usuario.Select(x => x.password).First();
                        if (contraseña == user.Contraseña)
                        {
                            Session["nombre_usuario"] = user.Usuario;
                            Session["contra"] = user.Contraseña;
                            return RedirectToAction("Index", "FormularioViewModels");
                        }
                        ModelState.AddModelError("Contraseña", "Usuario y/o Contraseña incorrecto");
                        return View();
                    }
                    ModelState.AddModelError("Contraseña", "Usuario y/o Contraseña incorrecto");
                    return View();
                }
                ModelState.AddModelError("Contraseña", "Usuario y/o Contraseña incorrecto");
                return View();
            }
            else
            {
                return View();
            }
        }

    }
}