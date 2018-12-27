using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FundService.Test
{
    [TestClass]
    public class TarjetaUnitTest
    {
        [TestMethod]
        public void PruebaCompleta()
        {
            // Arrange
            var nroTarjeta = "0000006096719570";
            var fVec = new DateTime(2019, 1, 23);
            short codSeg = 6643;
            var serv = new TarjetaService();

            // Act
            var existeTajeta = serv.ExisteTarjeta(nroTarjeta);
            // Assert
            Assert.IsTrue(existeTajeta);

            var valido = serv.EsValidaTarjeta(nroTarjeta, fVec, codSeg);
            Assert.IsTrue(valido);

            var fondo = serv.HayFondoParaSustraer(nroTarjeta, 100);
            Assert.IsTrue(fondo);

            try
            {
                serv.SustraerFondo(nroTarjeta, 100);
            }
            catch (Exception)
            {
                Assert.Fail();
            }

            

        }
    }
}
