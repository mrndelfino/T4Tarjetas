using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T4Tarjetas.Models;

namespace T4Tarjetas.Controllers
{
    public class BancoController : Controller
    {
        private t4tarjetasEntities t4tarjetas = new t4tarjetasEntities();

        public ActionResult Details(int id)
        {
            return View(t4tarjetas.Bancoes.Where(x => x.IDUsuario == id).ToList());
        }
        // GET: Banco
        public ActionResult Create(int id)
        {
            var bancos = t4tarjetas.Bancoes.Where(x => x.IDUsuario == id);
            if (bancos.Any())
            {
                ViewBag.TooMuchBanks = "Ha excedido la creacion de bancos.";
                return RedirectToAction("Details", "Banco", new { Id = id});
            }

            Banco banco = new Banco();
            banco.IDUsuario = id;
            banco.Balance = banco.generarBalance();
            return View(banco);
        }

        [HttpPost]
        public ActionResult Create(int id, Banco banco)
        {
            banco.IDUsuario = id;
            Session["IDBanco"] = id;
            t4tarjetas.Bancoes.Add(banco);
            t4tarjetas.SaveChanges();
            return RedirectToAction("Dashboard", "Usuario");
        }

        public ActionResult Delete(int id)
        {
            return View(t4tarjetas.Bancoes.Where(x => x.IDUsuario == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection formCollection)
        {
            Banco banco = t4tarjetas.Bancoes.Where(x => x.IDUsuario == id).FirstOrDefault();
            t4tarjetas.Bancoes.Remove(banco);
            t4tarjetas.SaveChanges();
            return RedirectToAction("Dashboard", "Usuario");
        }
    }
}