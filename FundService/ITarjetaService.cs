using FundService.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace FundService
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface ITarjetaService
    {
        [OperationContract]
        bool ExisteTarjeta(string NroTarjeta);

        [OperationContract]
        bool EsValidaTarjeta(string NroTarjeta, DateTime fechaVencimiento, short codigoSeguridad);

        [OperationContract]
        bool HayFondoParaSustraer(string NroTarjeta, int Monto);

        [OperationContract]
        void AgregarFondo(string NroTarjeta, int Monto);

        [OperationContract]
        void SustraerFondo(string NroTarjeta, int Monto);
    }
}
