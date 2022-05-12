using AutoMapper;
using SistemaEncuestasBL.Data;
using SistemaEncuestasBL.DTOs;
using SistemaEncuestasBL.Models;
using SistemaEncuestasBL.Repositories.Implements;
using SistemaEncuestasBL.Services;
using SistemaEncuestasBL.Services.Implements;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace SistemaEncuestasAPI.Controllers
{
    [Authorize]
    public class RespuestasController : ApiController
    {
        private readonly IPreguntaService preguntaService = new PreguntaService(new PreguntaRepository(SistemaEncuestasContext.Crear()));
        private readonly IRespuestaTLService respuestaTLService = new RespuestaTLService(new RespuestaTLRepository(SistemaEncuestasContext.Crear()));
        private readonly IPersonaService personaService = new PersonaService(new PersonaRepository(SistemaEncuestasContext.Crear()));

        private const string nombreControladora = "Respuestas";
        private IMapper mapper;

        public RespuestasController()
        {
            this.mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }

        /// <summary>
        /// Obtiene todas las respuestas de una Pregunta de Texto Libre dada.
        /// </summary>
        /// <param name="PreguntaID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/" + nombreControladora + "/respuestasTLPorPregunta")]
        public async Task<IHttpActionResult> RespuestasTL(int PreguntaID)
        {

            if (PreguntaID == 0) return BadRequest("PreguntaID no puede ser 0.");
            if (await this.preguntaService.GetById(PreguntaID) == null) return BadRequest("La pregunta de Texto Libre con ID " + PreguntaID.ToString() + " no existe.");



            ICollection<RespuestaTL> respuestas = await this.respuestaTLService.RespuestasPorPregunta(PreguntaID);

            ICollection<RespuestaTLDTO> respuestasDTO = new List<RespuestaTLDTO>();


            foreach (RespuestaTL rTl in respuestas)
            {
                RespuestaTLDTO rTLdto = mapper.Map<RespuestaTLDTO>(rTl);
                if (rTl.PersonaID != 0)
                {
                    Persona p = await this.personaService.GetById(rTl.PersonaID);
                    rTLdto.Persona = new PersonaDTO(p.PersonaId,p.Nombre,p.Correo,p.Celular);

                    
                }
                respuestasDTO.Add(rTLdto);
            }




            return Ok(respuestasDTO);
        }

        /// <summary>
        /// Retorna los datos personales de los participantes de las encuestas. 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/" + nombreControladora + "/datosPersonales")]
        public async Task<IHttpActionResult> GetDatosPersonales()
        {

            var respuesta = await this.personaService.GetAll();

            return Ok(respuesta);
        }
    }

}