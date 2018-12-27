using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T4Tarjetas.Models;

namespace T4Tarjetas.Controllers
{
    public class ProveedorController : Controller
    {
        // GET: Proveedor
        t4tarjetasEntities t4tarjetas = new t4tarjetasEntities();

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Proveedor proveedor)
        {
            if (t4tarjetas.Proveedors.Any(x => x.NomProveedor == proveedor.NomProveedor))
            {
                //Muestra mensaje que ese usuario ya existe
                ViewBag.DuplicateMessage = "Ese proveedor ya está registrado";
                //Devuelve ese mismo usuario que se ingreso
                return View(proveedor);
            }
            t4tarjetas.Proveedors.Add(proveedor);
            t4tarjetas.SaveChanges();
            ViewBag.SuccessMessage = "Proveedor registrado con éxito";
            ModelState.Clear();
            return View(new Proveedor());
        }
    }
}