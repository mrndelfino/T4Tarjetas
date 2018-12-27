using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T4Tarjetas.Models
{
    public partial class Tarjeta
    {
        public string generarNro()
        {
            Random rnd = new Random();
            string nro = ((long)rnd.Next(0, 100000) * (long)rnd.Next(0, 100000)).ToString().PadLeft(16, '0').ToString();
            return nro;
        }

        public int generarCod()
        {
            int min = 1000;
            int max = 9999;
            Random rnd = new Random();
            int nro = rnd.Next(min, max);
            return nro;
        }
    }
}