using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T4Tarjetas.Models
{
    public partial class Banco
    {
        public int generarBalance()
        {
            int min = 0;
            int max = 5000;
            Random rnd = new Random();
            int bal = rnd.Next(min, max);
            return bal;
        }
    }
}