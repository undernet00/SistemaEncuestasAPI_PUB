using NUnit.Framework;
using SistemaEncuestasBL.Models;
using SistemaEncuestasBL.Data;
using System;
using System.Data;
using System.Threading.Tasks;
using SistemaEncuestasBL.Repositories;
using SistemaEncuestasBL.Repositories.Implements;

namespace SistemaEncuestasTEST
{
    internal class EncuestaRepositoryTest
    {
        readonly IEncuestaRepository encuestaRepository = new EncuestaRepository(SistemaEncuestasContext.Crear());

        [SetUp]
        public void Setup()
        {


        }


        [Test]
        public void TraerRelacionadosEncuestaArgumentoEsNull()
        {
            //Arrange
            Encuesta e = null;

            ////Act + Assert
            Assert.Throws(Is.TypeOf<ArgumentNullException>(),
              () => this.encuestaRepository.TraerRelacionados(e));
        }


        [Test]
        public async Task TraerRelacionadosEncuestaQueNoExiste()
        {
            //Arrange
            Encuesta e = null;
            Exception exception = null;

            //Act
            try
            {
                e = await this.encuestaRepository.GetById(9999999);
            }
            catch (Exception ex)
            {
                exception = ex;

            }


            //Assert
            Assert.AreEqual("No existe Entidad con ese ID.", exception.Message);
            
        }

        [Test]
        public async Task InsertDeUnaEntidadNULL()
        {
            //Arrange
            Encuesta e = null;
            Exception exception = null;

            //Act
            try
            {
                e = await this.encuestaRepository.Insert (e);
            }
            catch (Exception ex)
            {
                exception = ex;

            }


            //Assert
            Assert.AreEqual("La Entidad no puede ser NULL.", exception.Message);

        }


        [Test]
        public async Task UpdateDeUnaEntidadNULL()
        {
            //Arrange
            Encuesta e = null;
            Exception exception = null;

            //Act
            try
            {
                e = await this.encuestaRepository.Update(e);
            }
            catch (Exception ex)
            {
                exception = ex;

            }


            //Assert
            Assert.AreEqual("La Entidad no puede ser NULL.", exception.Message);

        }

        [Test]
        public async Task DeleteDeUnaEntidadQueNoExiste()
        {
            //Arrange
            int id = 999999999;
            Exception exception = null;

            //Act
            try
            {
                await this.encuestaRepository.Delete(id);
            }
            catch (Exception ex)
            {
                exception = ex;

            }


            //Assert
            Assert.AreEqual("No existe Entidad con ese ID.", exception.Message);

        }

    }

}

