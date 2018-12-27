using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics.Tracing;
using T4Tarjetas.Models;
using System.Diagnostics;
using System.IO;

namespace T4Tarjetas.Controllers
{
    public class UsuarioController : Controller
    {
        //instancio la databasecontext
        private t4tarjetasEntities t4tarjetas = new t4tarjetasEntities();

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            //Devuelve la vista donde la id que tiene como parametro es igual a idusuario
            return View(t4tarjetas.Usuarios.Where(x => x.IDUsuario == id).FirstOrDefault());
        }
        //GET /Usuario/Create
        public ActionResult Create()
        {
            //devuelve la vista create
            return View();
        }
        //POST Usuario/Create
        [HttpPost]
        public ActionResult Create(Usuario usuario)
        {
            //Pregunto si hay algun email en la base que sea igual al email que se introduce en el modelo
            if (t4tarjetas.Usuarios.Any(x => x.Email == usuario.Email))
            {
                //Muestra mensaje que ese usuario ya existe
                ViewBag.DuplicateMessage = "Ese usuario ya está registrado";
                //Devuelve ese mismo usuario que se ingreso
                return View(usuario);
            }
            //se agrega el usuario a la base
            t4tarjetas.Usuarios.Add(usuario);
            //se guardan los cambios
            t4tarjetas.SaveChanges();
            //muestra le mensaje de que se agrego con exito
            ViewBag.SuccessMessage = "Usuario registrado con éxito";
            //devuelve la misma vista pero con un usuario nuevo
            ModelState.Clear();
            return View(new Usuario());
        }
        //GET Usuario/Edit
        public ActionResult Edit(int id)
        {
            //Devuelve la vista donde la id que tiene como parametro es igual a idusuario
            return View(t4tarjetas.Usuarios.Where(x => x.IDUsuario == id).FirstOrDefault());
        }

        //POST Usuario/Edit
        [HttpPost]
        public ActionResult Edit(int id, Usuario usuario)
        {
            //Modifica el estado del usuario
            t4tarjetas.Entry(usuario).State = EntityState.Modified;
            //guarda cambios
            t4tarjetas.SaveChanges();
            //redirecciona a la vista login
            return RedirectToAction("Login");
        }

        //GET Usuario/Delete
        public ActionResult Delete(int id)
        {
            //Devuelve la vista donde la id que tiene como parametro es igual a idusuario
            return View(t4tarjetas.Usuarios.Where(x => x.IDUsuario == id).FirstOrDefault());
        }
        //POST Usuario/Delete
        [HttpPost]
        public ActionResult Delete(int id, FormCollection formcollection)
        {
            //instancia un usuario y le da el valor de la id que sea ese usuario
            Usuario usuario = t4tarjetas.Usuarios.Where(x => x.IDUsuario == id).FirstOrDefault();
            //lo elimina
            t4tarjetas.Usuarios.Remove(usuario);
            //guarda cambios
            t4tarjetas.SaveChanges();
            //redirecciona al dashboard         
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            //creo una variable donde guardo el email y el password
            var usuarioDetails = t4tarjetas.Usuarios.Where(x => x.Email == usuario.Email && x.Password == usuario.Password).FirstOrDefault();
            //si devuelve null desde la base
            if (usuarioDetails == null)
            {
                //manejo del null
                ViewBag.ErrorMessage = "Usuario y/o contraseña incorrectos";
                return View("Login", usuario);
            }
            //caso contrario
            else
            {
                //guardo en Session el id y el email para mostrar luego en el dashboard
                Session["IDUsuario"] = usuarioDetails.IDUsuario;
                Session["Email"] = usuarioDetails.Email;
                return RedirectToAction("Dashboard", "Usuario");
            }
        }

        public ActionResult Logout()
        {
            //creo una variable y le paso la id de la Session
            var IDUsuario = Session["IDUsuario"];
            //cierro la session
            Session.Abandon();
            return RedirectToAction("Login", "Usuario");
        }

        public ActionResult AddFund(int id)
        {
            var cuentasDelUsuario = t4tarjetas.Bancoes
                .Where(x => x.IDUsuario == id)
                .Select(x => new SelectListItem()
                {
                    Value = x.IDBanco.ToString(),
                    Text = x.NomBanco
                })
                .ToList();
            ViewBag.CuentasBancarias = cuentasDelUsuario;

            return View(t4tarjetas.Usuarios.Where(x => x.IDUsuario == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult AddFund(int id, Usuario usuario)
        {
            var cuentasDelUsuario = t4tarjetas.Bancoes
                .Where(x => x.IDUsuario == id)
                .Select(x => new SelectListItem()
                {
                    Value = x.IDBanco.ToString(),
                    Text = x.NomBanco
                })
                .ToList();
            ViewBag.CuentasBancarias = cuentasDelUsuario;

            if (usuario.FondoACargar < 1)
            {
                // tirar error de que no se puede devolver plata.
                return View(usuario);
            }

            // obtengo cuenta del banco
            var usuarioDb = t4tarjetas.Usuarios.SingleOrDefault(x => x.IDUsuario == id);
            if (usuarioDb == null)
            {
                // mostrart error de que el usuario no existe.
                return View(usuario);
            }

            var cuentaBancaria = t4tarjetas.Bancoes.SingleOrDefault(x => x.IDUsuario == id && x.IDBanco == usuario.IDBancoSelecionado);
            if (cuentaBancaria == null)
            {
                // tirar error de que no existe cuenta.
                ViewBag.NoAccount = "No existe la cuenta";              
                return View(usuario);
            }

            if (cuentaBancaria.Balance < usuario.FondoACargar)
            {
                // tirar error de que quiere sacar mas plata  de que la tiene el banco.
                ViewBag.ExceedFunds = "La plata solicitada supera lo que hay en la cuenta bancaria.";
                return View(usuario);
            }

            // saco plata de la cuenta.
            cuentaBancaria.Balance -= usuario.FondoACargar;
            //modifico el balance del usuario
            usuarioDb.Balance = usuarioDb.Balance.GetValueOrDefault(default(int)) + usuario.FondoACargar;

            // guardo los cambios y guardo en el log
            t4tarjetas.SaveChanges();
            using (TextWriter logFile = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\logCuenta.txt", true))
            {
                logFile.WriteLine("Usuario " + id + " realizó un ingreso de " + usuario.FondoACargar + " el día " + DateTime.Now);
                logFile.WriteLine("");
                logFile.Flush();
                logFile.Close();
            }
            // me voy  al dashboard
            return RedirectToAction("Dashboard", "Usuario");
        }
    }
}