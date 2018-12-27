using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPrueba.Models;
using WebPrueba.TarjetaServiceReference;

namespace WebPrueba.Controllers
{
    public class HomeController : Controller
    {
        //se instancia el cliente
        TarjetaServiceClient client = new TarjetaServiceClient();
        //trae la vista index
        public ActionResult Index()
        {
            return View();
        }
        //metodo post del index
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            //trae los valores de la form
            string NroTarjeta = form.Get("NroTarjeta");
            var fv = form.Get("FecVen");
            DateTime FecVen = DateTime.Parse(fv);
            short CodTarjeta = Convert.ToInt16(form.Get("CodTarjeta"));
            //Si la tarjeta no existe, devuelve la vista y el mensaje de error con viewbag
            if (!client.ExisteTarjeta(NroTarjeta))
            {
                ViewBag.NotFound = "La tarjeta no existe";
                return View();
                //throw new ApplicationException("No existe.");
            }
            //si la tarjeta no es válida, devuelve la vista y el mensaje de error con viewbag
            if (!client.EsValidaTarjeta(NroTarjeta, FecVen, CodTarjeta))
            {
                ViewBag.NotValid = "La tarjeta no es válida";
                return View();
            }
            //agarra el valor de la operacion a realizar del selector
            var operacion = form.Get("operacion");
            //Si es nulo o vacío devuelve la vista
            if (string.IsNullOrWhiteSpace(operacion))
            {           
                return View();
            }
            //Redirecciona dependiendo de si la operacion es añadir o sustraer
            if (operacion == "a")
            {
                return RedirectToAction("Index3", "Home", new { NroTarjeta });
            }
            else
            {
                return RedirectToAction("Index2", "Home", new { NroTarjeta });
            }
        }
        //trae la vista con el model y el numero de tarjeta
        public ActionResult Index2(string NroTarjeta)
        {
            var model = new Index2ViewModel();
            model.NroTarjeta = NroTarjeta;
            return View(model);
        }
        //post del index2
        [HttpPost]
        public ActionResult Index2(string NroTarjeta, int Monto)
        {
            //Se maneja el caso de que no haya fondos
            if (!client.HayFondoParaSustraer(NroTarjeta, Monto))
            {
                ViewBag.InsufficentMoney = "El fondo de la tarjeta es insuficiente";
                return View();
            }
            //se ejecuta el sustraer fondo
            client.SustraerFondo(NroTarjeta, Monto);
            ViewBag.SuccessSubstract = "Se realizo la sustracción correctamente";
            //se devuelve la vista con modelo nuevo y el numero de tarjeta
            return View(new Index2ViewModel() { NroTarjeta = NroTarjeta });
        }
        //se trae la vista con el modelo
        public ActionResult Index3(string NroTarjeta)
        {
            var model = new Index2ViewModel();
            model.NroTarjeta = NroTarjeta;
            return View(model);
        }
        //post del index3
        [HttpPost]
        public ActionResult Index3(string NroTarjeta, int monto)
        {
            //se agrega el monto a la tarjeta y se muestra mensaje
            client.AgregarFondo(NroTarjeta, monto);
            ViewBag.SuccessAdd = "Se añadio el monto correctamente";
            //se devuelve la vista con un modelo nuevo y el numero de tarjeta
            return View(new Index2ViewModel() { NroTarjeta = NroTarjeta });
        }
    }
}