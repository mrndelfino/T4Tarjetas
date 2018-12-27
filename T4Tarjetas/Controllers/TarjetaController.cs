using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using T4Tarjetas.Models;

namespace T4Tarjetas.Controllers
{
    public class TarjetaController : Controller
    {
        private t4tarjetasEntities t4tarjetas = new t4tarjetasEntities();

        public ActionResult Details(int id)
        {
            //Devuelve la vista con la lista de tarjetas donde la id que tiene como parametro es igual a idusuario       
            return View(t4tarjetas.Tarjetas.Where(x => x.IDUsuario == id).ToList());
        }
        //GET /Tarjeta/Create
        public ActionResult Create(int id)
        {
            var proveedores = t4tarjetas.Proveedors
                .Select(x => new SelectListItem()
                {
                    Value = x.IDProveedor.ToString(),
                    Text = x.NomProveedor
                })
                .ToList();
            ViewBag.Proveedores = proveedores;

            Tarjeta tarjeta = new Tarjeta();
            //agrego valores a parametros
            tarjeta.IDUsuario = id;
            tarjeta.NroTarjeta = tarjeta.generarNro();
            tarjeta.CodTarjeta = (short)tarjeta.generarCod();
            tarjeta.FecCre = DateTime.Now;
            tarjeta.FecVen = DateTime.Now.AddMonths(3);
            //devuelve la vista create
            return View(tarjeta);
        }
        //POST Tarjeta/Create
        [HttpPost]
        public ActionResult Create(int id, Tarjeta tarjeta, FormCollection formCollection)
        {
            //agrego valores a parametros
            tarjeta.FecCre = DateTime.Now;
            tarjeta.FecVen = DateTime.Now.AddMonths(3);

            // pregunto al banco (cuenta bancaria) si tengo saldo
            var saldoUsuario = t4tarjetas.Usuarios
                .FirstOrDefault(x => x.IDUsuario == id);
            if (saldoUsuario.Balance < tarjeta.MontoTarjeta)
            {
                var proveedores = t4tarjetas.Proveedors
                .Select(x => new SelectListItem()
                {
                    Value = x.IDProveedor.ToString(),
                    Text = x.NomProveedor
                })
                .ToList();
                ViewBag.Proveedores = proveedores;

                ViewBag.ExceedMessage = "El monto de la tarjeta supera el balance de la cuenta bancaria.";
                return View(tarjeta);
            }
            else
            {
                // quito plata de la cuenta bancaria.
                saldoUsuario.Balance -= tarjeta.MontoTarjeta;

                //se agrega la tarjeta a la base
                t4tarjetas.Tarjetas.Add(tarjeta);

                //se guardan los cambios
                t4tarjetas.SaveChanges();

                //muestra le mensaje de que se agrego con exito
                ViewBag.SuccessMessage = "Tarjeta registrada con éxito";
                ModelState.Clear();
                //devuelve la vista de detalles
                using (TextWriter logFile = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\logCuenta.txt", true))
                {
                    logFile.WriteLine("Usuario " + id + " realizó un egreso de " + tarjeta.MontoTarjeta + " el día " + DateTime.Now);
                    logFile.WriteLine("");
                    logFile.Flush();
                    logFile.Close();
                }
                return RedirectToAction("Details", "Tarjeta", new { id = tarjeta.IDUsuario });
            }
        }
        //GET Tarjeta/Edit
        public ActionResult Edit(int id)
        {
            //Devuelve la vista donde la id que tiene como parametro es igual a idTarjeta
            return View(t4tarjetas.Tarjetas.Where(x => x.IDUsuario == id).FirstOrDefault());
        }

        //POST Tarjeta/Edit
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Tarjeta tarjeta)
        {
            //Modifica el estado del Tarjeta
            t4tarjetas.Entry(tarjeta).State = EntityState.Modified;
            //guarda cambios
            t4tarjetas.SaveChanges();
            //redirecciona a la vista dashboard
            return RedirectToAction("Dashboard", "Home");
        }

        //GET Tarjeta/Delete
        public ActionResult Delete(int id)
        {
            //Devuelve la vista donde la id que tiene como parametro es igual a idTarjeta
            return View(t4tarjetas.Tarjetas.Where(x => x.IDUsuario == id).FirstOrDefault());
        }
        //POST Tarjeta/Delete
        [HttpPost]
        public ActionResult Delete(int id, FormCollection formcollection)
        {
            //instancia un Tarjeta y le da el valor de la id que sea ese Tarjeta
            Tarjeta tarjeta = t4tarjetas.Tarjetas.Where(x => x.IDUsuario == id).FirstOrDefault();
            //lo elimina
            t4tarjetas.Tarjetas.Remove(tarjeta);
            //guarda cambios
            t4tarjetas.SaveChanges();
            //redirecciona al dashboard
            return RedirectToAction("Dashboard", "Usuario");
        }
    }
}