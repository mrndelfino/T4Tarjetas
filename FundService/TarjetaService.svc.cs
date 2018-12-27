using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using T4Tarjetas.Models;
using System.Data.Entity;
using System.IO;

namespace FundService
{
    public class TarjetaService : ITarjetaService
    {
        //Base de datos
        private static t4tarjetasEntities t4tarjetas = new t4tarjetasEntities();
        //Agrego fondo
        public void AgregarFondo(string NroTarjeta, int Monto)
        {
            //Traigo la tarjeta que tenga el mismo numero de tarjeta que le paso por parametro
            var tarjeta = t4tarjetas.Tarjetas.SingleOrDefault(x => x.NroTarjeta == NroTarjeta);
            //Le agrego el monto
            tarjeta.MontoTarjeta += Monto;
            //Guardo cambios
            t4tarjetas.Entry(tarjeta).State = EntityState.Modified;
            t4tarjetas.SaveChanges();
            //Guardo en el log.txt, registro el ingreso de plata
            using (TextWriter logFile = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\log.txt", true))
            {
                logFile.WriteLine("Tarjeta Nro " + NroTarjeta + " realizó un ingreso de " + Monto + " el día " + DateTime.Now);
                logFile.WriteLine("");
                logFile.Flush();
                logFile.Close();
            }           
        }
        //Booleano que devuelve true or false dependiendo si se cumple o no la condicion
        public bool EsValidaTarjeta(string NroTarjeta, DateTime fechaVencimiento, short codigoSeguridad)
        {
            return t4tarjetas.Tarjetas.Any(x => x.NroTarjeta == NroTarjeta && DbFunctions.TruncateTime(x.FecVen) == fechaVencimiento.Date && x.CodTarjeta == codigoSeguridad);
        }
        //Booleando que se fija si existe la tarjeta
        public bool ExisteTarjeta(string NroTarjeta)
        {
            //Si es nulo se maneja la excepcion
            if (string.IsNullOrWhiteSpace(NroTarjeta))
            {
                throw new ApplicationException("El numero de tarjeta es nulo o vacio.");
            }
            //Si la tarjeta posee mas de 16 numeros maneja la excepcion
            if (NroTarjeta.Length != 16)
            {
                throw new ApplicationException("El numero de tarjeta tiene una cantidad de caracteres invalido.");
            }
            //devuelve true or false dependiendo si se cumple o no la condicion
            return t4tarjetas.Tarjetas.Any(x => x.NroTarjeta == NroTarjeta);
        }
        //Booleano que devuelve si se puede o no sustraer fondos de la tarjeta
        public bool HayFondoParaSustraer(string NroTarjeta, int Monto)
        {
            return t4tarjetas.Tarjetas.Any(x => x.NroTarjeta == NroTarjeta && x.MontoTarjeta <= Monto);
        }
        //metodo para sustraer fondos
        public void SustraerFondo(string NroTarjeta, int Monto)
        {
            //guarda una tarjeta que cumpla la condicion
            var tarjeta = t4tarjetas.Tarjetas.SingleOrDefault(x => x.NroTarjeta == NroTarjeta);
            //sustrae el monto indicado
            tarjeta.MontoTarjeta -= Monto;
            //se guarda el estado en la base y se registra el egreso en el log
            t4tarjetas.Entry(tarjeta).State = EntityState.Modified;
            t4tarjetas.SaveChanges();
            using (TextWriter logFile = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\log.txt", true))
            {
                logFile.WriteLine("Tarjeta Nro " + NroTarjeta + " realizó un egreso de " + Monto + " el día " + DateTime.Now);
                logFile.WriteLine("");
                logFile.Flush();
                logFile.Close();
            }
        }
    }
}
