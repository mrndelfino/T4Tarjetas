﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebPrueba.TarjetaServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TarjetaServiceReference.ITarjetaService")]
    public interface ITarjetaService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITarjetaService/ExisteTarjeta", ReplyAction="http://tempuri.org/ITarjetaService/ExisteTarjetaResponse")]
        bool ExisteTarjeta(string NroTarjeta);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITarjetaService/ExisteTarjeta", ReplyAction="http://tempuri.org/ITarjetaService/ExisteTarjetaResponse")]
        System.Threading.Tasks.Task<bool> ExisteTarjetaAsync(string NroTarjeta);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITarjetaService/EsValidaTarjeta", ReplyAction="http://tempuri.org/ITarjetaService/EsValidaTarjetaResponse")]
        bool EsValidaTarjeta(string NroTarjeta, System.DateTime fechaVencimiento, short codigoSeguridad);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITarjetaService/EsValidaTarjeta", ReplyAction="http://tempuri.org/ITarjetaService/EsValidaTarjetaResponse")]
        System.Threading.Tasks.Task<bool> EsValidaTarjetaAsync(string NroTarjeta, System.DateTime fechaVencimiento, short codigoSeguridad);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITarjetaService/HayFondoParaSustraer", ReplyAction="http://tempuri.org/ITarjetaService/HayFondoParaSustraerResponse")]
        bool HayFondoParaSustraer(string NroTarjeta, int Monto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITarjetaService/HayFondoParaSustraer", ReplyAction="http://tempuri.org/ITarjetaService/HayFondoParaSustraerResponse")]
        System.Threading.Tasks.Task<bool> HayFondoParaSustraerAsync(string NroTarjeta, int Monto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITarjetaService/AgregarFondo", ReplyAction="http://tempuri.org/ITarjetaService/AgregarFondoResponse")]
        void AgregarFondo(string NroTarjeta, int Monto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITarjetaService/AgregarFondo", ReplyAction="http://tempuri.org/ITarjetaService/AgregarFondoResponse")]
        System.Threading.Tasks.Task AgregarFondoAsync(string NroTarjeta, int Monto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITarjetaService/SustraerFondo", ReplyAction="http://tempuri.org/ITarjetaService/SustraerFondoResponse")]
        void SustraerFondo(string NroTarjeta, int Monto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITarjetaService/SustraerFondo", ReplyAction="http://tempuri.org/ITarjetaService/SustraerFondoResponse")]
        System.Threading.Tasks.Task SustraerFondoAsync(string NroTarjeta, int Monto);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITarjetaServiceChannel : WebPrueba.TarjetaServiceReference.ITarjetaService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TarjetaServiceClient : System.ServiceModel.ClientBase<WebPrueba.TarjetaServiceReference.ITarjetaService>, WebPrueba.TarjetaServiceReference.ITarjetaService {
        
        public TarjetaServiceClient() {
        }
        
        public TarjetaServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TarjetaServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TarjetaServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TarjetaServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool ExisteTarjeta(string NroTarjeta) {
            return base.Channel.ExisteTarjeta(NroTarjeta);
        }
        
        public System.Threading.Tasks.Task<bool> ExisteTarjetaAsync(string NroTarjeta) {
            return base.Channel.ExisteTarjetaAsync(NroTarjeta);
        }
        
        public bool EsValidaTarjeta(string NroTarjeta, System.DateTime fechaVencimiento, short codigoSeguridad) {
            return base.Channel.EsValidaTarjeta(NroTarjeta, fechaVencimiento, codigoSeguridad);
        }
        
        public System.Threading.Tasks.Task<bool> EsValidaTarjetaAsync(string NroTarjeta, System.DateTime fechaVencimiento, short codigoSeguridad) {
            return base.Channel.EsValidaTarjetaAsync(NroTarjeta, fechaVencimiento, codigoSeguridad);
        }
        
        public bool HayFondoParaSustraer(string NroTarjeta, int Monto) {
            return base.Channel.HayFondoParaSustraer(NroTarjeta, Monto);
        }
        
        public System.Threading.Tasks.Task<bool> HayFondoParaSustraerAsync(string NroTarjeta, int Monto) {
            return base.Channel.HayFondoParaSustraerAsync(NroTarjeta, Monto);
        }
        
        public void AgregarFondo(string NroTarjeta, int Monto) {
            base.Channel.AgregarFondo(NroTarjeta, Monto);
        }
        
        public System.Threading.Tasks.Task AgregarFondoAsync(string NroTarjeta, int Monto) {
            return base.Channel.AgregarFondoAsync(NroTarjeta, Monto);
        }
        
        public void SustraerFondo(string NroTarjeta, int Monto) {
            base.Channel.SustraerFondo(NroTarjeta, Monto);
        }
        
        public System.Threading.Tasks.Task SustraerFondoAsync(string NroTarjeta, int Monto) {
            return base.Channel.SustraerFondoAsync(NroTarjeta, Monto);
        }
    }
}
