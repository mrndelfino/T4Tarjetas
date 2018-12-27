using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T4Tarjetas.Models
{
    public partial class Usuario
    {
        public Nullable<int> IDBancoSelecionado { get; set; }
        public int FondoACargar { get; set; }
    }
}