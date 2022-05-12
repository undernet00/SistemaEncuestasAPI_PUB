using NUnit.Framework;
using SistemaEncuestasBL.Models;


namespace SistemaEncuestasTEST
{
    public class ModelTests
    {
        private RespuestaOM r1;

        [SetUp]
        public void Setup()
        {
            //Se ejecuta cada vez que comienza un Test
            r1 = new RespuestaOM();

        }

        //[Test]
        //public void RespuestaOpcionMultipleSeleccionarNuevo()
        //{
        //    //Arrange

        //    this.r1.SeleccionarOpcion("Papas Bravas");
        //    //Act
        //    this.r1.SeleccionarOpcion("Huevos Rotos");

        //    //Assert
        //    Assert.AreEqual(2, this.r1.CantidadOpcionesSeleccionadas());
        //}

        //[Test]
        //public void RespuestaOpcionMultipleSeleccionarRepetido()
        //{
        //    //Arrange

        //    this.r1.SeleccionarOpcion("Huevos Rotos");
        //    //Act
        //    this.r1.SeleccionarOpcion("Huevos Rotos");

        //    //Assert
        //    Assert.AreEqual(1, this.r1.CantidadOpcionesSeleccionadas());
        //}


        //public void RespuestaOpcionMultipleSeleccionarNull()
        //{
        //    //Arrange

        //    //Act
        //    this.r1.SeleccionarOpcion(null);

        //    //Assert
        //    Assert.AreEqual(0, this.r1.CantidadOpcionesSeleccionadas());

        //}

        //[Test]
        //public void RespuestaOpcionMultipleBuscarOpcion()
        //{
        //    //Arrange
        //    this.r1.SeleccionarOpcion("Papas Bravas");
        //    this.r1.SeleccionarOpcion("Huevos Rotos");
        //    this.r1.SeleccionarOpcion("Croquetas de Jamón");
        //    //Act

        //    //Assert
        //    Assert.AreEqual(true, this.r1.ExisteOpcionSeleccionada("croquetas de jamón   "));
        //    Assert.AreEqual(true, this.r1.ExisteOpcionSeleccionada("croqueTas dE jamón"));

        //}

        //[Test]
        //public void RespuestaOpcionMultipleDeseleccionar1()
        //{
        //    //Arrange
        //    this.r1.SeleccionarOpcion("Papas Bravas");
        //    this.r1.SeleccionarOpcion("Huevos Rotos");
        //    this.r1.SeleccionarOpcion("Croquetas de Jamón");
        //    //Act
        //    this.r1.DeseleccionarOpcion("Huevos Rotos");
        //    //Assert
        //    Assert.AreEqual(2, this.r1.CantidadOpcionesSeleccionadas());



        //}
        //[Test]
        //public void RespuestaOpcionMultipleDeseleccionar2()
        //{
        //    //Arrange
        //    this.r1.SeleccionarOpcion("Papas Bravas");
        //    this.r1.SeleccionarOpcion("Huevos Rotos");
        //    this.r1.SeleccionarOpcion("Croquetas de Jamón");
        //    //Act
        //    this.r1.DeseleccionarOpcion("croquetaS de jamón    ");
        //    //Assert
        //    Assert.AreEqual(2, this.r1.CantidadOpcionesSeleccionadas());
        //}

        //[Test]
        //public void RespuestaOpcionMultipleDeseleccionar3()
        //{
        //    //Arrange
        //    this.r1.SeleccionarOpcion("Papas Bravas");
        //    this.r1.SeleccionarOpcion("Huevos Rotos");
        //    this.r1.SeleccionarOpcion("Croquetas de Jamón");
        //    //Act
        //    this.r1.DeseleccionarOpcion("croquetaS de jamón a  ");
        //    //Assert
        //    Assert.AreEqual(3, this.r1.CantidadOpcionesSeleccionadas());
        //}
        //[Test]
        //public void RespuestaOpcionMultipleBuscar()
        //{
        //    //Arrange
        //    this.r1.SeleccionarOpcion("Papas Bravas");
        //    this.r1.SeleccionarOpcion("Huevos Rotos");
        //    this.r1.SeleccionarOpcion("Croquetas de Jamón");
        //    //Act
            
        //    //Assert
        //    Assert.AreEqual(1, this.r1.BuscarOpcionSeleccionada("huEvos roTos"));
        //    Assert.AreEqual(2, this.r1.BuscarOpcionSeleccionada("croquetas de jamón    "));
        //    Assert.AreEqual(-1, this.r1.BuscarOpcionSeleccionada("croquetas de jamón   a12 "));
        //}


    }
}