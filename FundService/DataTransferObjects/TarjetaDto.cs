using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace FundService.DataTransferObjects
{
    [DataContract]
    public class TarjetaDto
    {
        [DataMember]
        public string NroTarjeta { get; set; }
        [DataMember]
        public short CodTarjeta { get; set; }
        [DataMember]
        public System.DateTime FecVen { get; set; }
        [DataMember]
        public int MontoTarjeta { get; set; }
    }
}