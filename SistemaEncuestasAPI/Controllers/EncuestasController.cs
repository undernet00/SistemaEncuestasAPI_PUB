using AutoMapper;
using awsComprehend.Models;
using awsComprehend.Services;
using AwsComprehend.Services;
using SistemaEncuestasBL.Data;
using SistemaEncuestasBL.DTOs;
using SistemaEncuestasBL.Models;
using SistemaEncuestasBL.Repositories.Implements;
using SistemaEncuestasBL.Services.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace SistemaEncuestasAPI.Controllers
{
    /// <summary>
    /// Permite interactuar con las encuestas. Consultarlas, responderlas, etc.
    /// </summary>
    [Authorize]
    public class EncuestasController : ApiController
    {
        private readonly EncuestaService encuestaService = new EncuestaService(new EncuestaRepository(SistemaEncuestasContext.Crear()));
        private readonly PreguntaService preguntaService = new PreguntaService(new PreguntaRepository(SistemaEncuestasContext.Crear()));
        private readonly RespuestaService respuestaService = new RespuestaService(new RespuestaRepository(SistemaEncuestasContext.Crear()));
        private readonly RespuestaTLService respuestaTLService = new RespuestaTLService(new RespuestaTLRepository(SistemaEncuestasContext.Crear()));
        private readonly RespuestaOMService respuestaOMService = new RespuestaOMService(new RespuestaOMRepository(SistemaEncuestasContext.Crear()));
        private readonly RespuestaOSService respuestaOSService = new RespuestaOSService(new RespuestaOSRepository(SistemaEncuestasContext.Crear()));
        private readonly IComprehendService comprehendService = new ComprehendService();
        private readonly EtiquetaService etiquetaService = new EtiquetaService(new EtiquetaRepository(SistemaEncuestasContext.Crear()));
        private readonly PersonaService personaService = new PersonaService(new PersonaRepository(SistemaEncuestasContext.Crear()));

        private IMapper mapper;

        public EncuestasController()
        {
            this.mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }

        /// <summary>
        /// Lista todas las encuestas disponibles.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            var encuestas = await encuestaService.GetAll();

            var encuestasDTO = encuestas.Select(x => mapper.Map<EncuestaDTO>(encuestaService.TraerRelacionados(x)));

            return Ok(encuestasDTO);
        }

        /// <summary>
        /// Consulta una encuesta conociendo su ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetByID(int id)
        {
            var encuesta = await encuestaService.GetById(id);

            if (encuesta == null) return NotFound();

            var encuestasDTO = mapper.Map<EncuestaDTO>(encuestaService.TraerRelacionados(encuesta));

            return Ok(encuestasDTO);
        }

        /// <summary>
        /// Consulta una encuesta conociendo su ID sin necesidad de Token.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("api/EncuestasOpen")]
        public async Task<IHttpActionResult> GetByIDOpen(int id)
        {
            return await GetByID(id);
        }

        /// <summary>
        /// Crea una encuesta. Se le pasa un JSON con la estructura completa de Encuesta/Preguntas.
        /// </summary>
        /// <param name="encuesta"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> CrearEncuesta(EncuestaDTO encuesta)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Encuesta encuestaNueva = mapper.Map<Encuesta>(encuesta);

            await this.encuestaService.Insert(encuestaNueva);

            return Ok();
        }




        /// <summary>
        /// Permite modificar una encuesta ya existente.
        /// </summary>
        /// <param name="encuestaDTO"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IHttpActionResult> ModificarEncuesta(EncuestaDTO encuestaDTO, int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (encuestaDTO.EncuestaID != id) return BadRequest("La Encuesta no coincide con el id enviado.");
            //TODO: Controlar que no se estén cambiando los tipos de pregunta


            Encuesta encuestaBuscada = await encuestaService.GetById(id);

            if (encuestaBuscada == null) return NotFound();
            //if (encuestaBuscada.CantidadEncuestados != 0) return BadRequest("No se puede modificar una encuesta que tiene respuestas.");

            try
            {
                var encuestaModificada = mapper.Map<Encuesta>(encuestaDTO);
                encuestaModificada.CantidadEncuestados = encuestaBuscada.CantidadEncuestados; //Bug #15: Para no perder la cantidad de encuestados al modificar una encuesta.
                encuestaModificada = await encuestaService.Update(encuestaModificada);

                await encuestaService.ActualizarRelacionados(encuestaModificada);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        /// <summary>
        /// Permite contestar una encuesta.
        /// </summary>
        /// <param name="respuestasEncuesta"></param>
        /// <returns></returns>
        [Route("api/Contestar")]
        [HttpPost]
        public async Task<IHttpActionResult> ContestarEncuesta(EncuestaRespuestaDTO respuestasEncuesta)
        {
            return await ContestarEncuestaPrivate(respuestasEncuesta);

        }

        /// <summary>
        /// Permite contestar una encuesta sin necesidad de un Token.
        /// </summary>
        /// <param name="respuestasEncuesta"></param>
        /// <returns></returns>
        [Route("api/ContestarOpen")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> ContestarEncuestaOpen(EncuestaRespuestaDTO respuestasEncuesta)
        {
            return await ContestarEncuestaPrivate(respuestasEncuesta);

        }


        private async Task<IHttpActionResult> ContestarEncuestaPrivate(EncuestaRespuestaDTO respuestasEncuesta)
        {
            if (!ModelState.IsValid || respuestasEncuesta.EncuestaID == 0 || respuestasEncuesta.EncuestaID == -1) return BadRequest(ModelState);

            var encuesta = await encuestaService.GetById(respuestasEncuesta.EncuestaID);
            if (encuesta == null) return NotFound();
            if (encuesta.Estado != EstadoEncuesta.Publicada) return BadRequest("La encuesta no tiene el estado PUBLICADA.");

            //Valido Requeridas
            foreach (RespuestaDTO rDto in respuestasEncuesta.Respuestas)
            {
                string requeridos = await ValidarPregunta(rDto);
                if (requeridos != "") return BadRequest(requeridos);
            }

            int personaId = await BuscarEInsertarPersona(respuestasEncuesta.Encuestado);

            try
            {
                foreach (RespuestaDTO r in respuestasEncuesta.Respuestas)
                {
                    TipoPregunta tipo = mapper.Map<TipoPregunta>(r.Tipo);


                    switch (tipo)
                    {
                        case TipoPregunta.TEXTOLIBRE:

                            if (r.TextoRespuesta.Trim() != "")
                            {

                                AwsComentario awsComentario = await this.AnalizarRespuesta(r);
                                var respuestaTL = mapper.Map<RespuestaTL>(r);

                                if (awsComentario != null)
                                {
                                    //Solamente cargo los datos que vienen del análisis de AWS Comprehend
                                    respuestaTL.Idioma = awsComentario.Idioma;
                                    respuestaTL.Mixto = awsComentario.PuntajeMixto;
                                    respuestaTL.Negativo = awsComentario.PuntajeNegativo;
                                    respuestaTL.Neutral = awsComentario.PuntajeNeutral;
                                    respuestaTL.Positivo = awsComentario.PuntajePositivo;
                                    respuestaTL.Sentimiento = mapper.Map<Sentimiento>(awsComentario.Sentimiento);
                                    respuestaTL.FechaHoraAnalisis = DateTime.Now;

                                    respuestaTL.FrasesClave = awsComentario.FrasesClave.Select(f => new FraseClave { Frase = f.Frase }).ToList();
                                    respuestaTL.PersonaID = personaId;

                                    if (awsComentario.EtiquetasExternas != null)
                                    {
                                        ICollection<Etiqueta> etiquetas = await this.etiquetaService.ConvertirLista(awsComentario.EtiquetasExternas);
                                        respuestaTL.Etiquetas = etiquetas;
                                    }


                                }

                                respuestaTL = await respuestaTLService.Insert(respuestaTL);
                            }

                            break;

                        case TipoPregunta.OPCIONMULTIPLE:
                            var respuestaOM = mapper.Map<RespuestaOM>(r);
                            respuestaOMService.CargarRelacionadas(respuestaOM);
                            respuestaOM.PersonaID = personaId;
                            respuestaOM = await respuestaOMService.Insert(respuestaOM);

                            break;

                        case TipoPregunta.OPCIONSIMPLE:
                            var respuestaOS = mapper.Map<RespuestaOS>(r);
                            respuestaOSService.CargarRelacionadas(respuestaOS);
                            respuestaOS.PersonaID = personaId;
                            respuestaOS = await respuestaOSService.Insert(respuestaOS);
                            break;


                    }
                }
                encuesta.CantidadEncuestados += 1;
                encuesta = await encuestaService.Update(encuesta);

                return Ok();

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        private async Task<int> BuscarEInsertarPersona(PersonaDTO personaDTO)
        {

            if (personaDTO != null)
            {
                Persona persona = mapper.Map<Persona>(personaDTO);
                return await this.personaService.BuscarEInsertarPersona(persona);
            }

            return 0;

        }

        /// <summary>
        /// Permite obtener los resultados de una encuesta conociendo su ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/Resultados")]
        [HttpGet]
        public async Task<IHttpActionResult> GetResultado(int id)
        {
            var encuesta = await encuestaService.GetById(id);

            if (encuesta == null) return NotFound();

            encuesta = encuestaService.TraerRelacionados(encuesta);

            var encuestasDTO = mapper.Map<EncuestaDTO>(encuesta);


            foreach (Pregunta p in encuesta.Preguntas)
            {

                PreguntaDTO pregDTO = encuestasDTO.Preguntas.First(pD => pD.PreguntaID == p.PreguntaID); //Encuentro la PreguntaDTO dentro le EncuestaDTO
                pregDTO.Resultados = this.respuestaService.GetResultadosRanking(p).Select(x => mapper.Map<ResultadoDTO>(x)).ToList();  //Le cargo los resultados

                if (p.Tipo == TipoPregunta.TEXTOLIBRE)
                {
                    pregDTO.ResultadosML = this.respuestaService.GetResultadosRankingEtiquetas(p).Select(x => mapper.Map<ResultadoDTO>(x)).ToList();
                }

            }

            //A pedido de MO para que llegue limpio al front.
            foreach (PreguntaDTO p in encuestasDTO.Preguntas)
            {
                if (p.Tipo == "OPCIONMULTIPLE" || p.Tipo == "OPCIONSIMPLE")
                {
                    p.Opciones = null;
                }
            }

            return Ok(encuestasDTO);

        }


        private async Task<AwsComentario> AnalizarRespuesta(RespuestaDTO respuestaSinAnalizar)
        {
            if (respuestaSinAnalizar is null) throw new ArgumentNullException(nameof(respuestaSinAnalizar));


            AwsComentario awsComentario = new AwsComentario(respuestaSinAnalizar.TextoRespuesta);
            awsComentario.IdAuxiliar = respuestaSinAnalizar.RespuestaID;

            return await comprehendService.AnalizarComentario(awsComentario);


        }

        //Validación a nivel de DTO de la integridad de respuesta.
        private async Task<string> ValidarPregunta(RespuestaDTO respuestaDTO)
        {


            switch (respuestaDTO.Tipo)
            {
                case "TEXTOLIBRE":

                    Pregunta pTL = await this.preguntaService.GetById(respuestaDTO.PreguntaID);
                    if (pTL.Requerida && respuestaDTO.TextoRespuesta == "") return "La pregunta de Texto Libre de ID " + pTL.PreguntaID.ToString() + " es requerida.";
                    break;
                case "OPCIONMULTIPLE":

                    PreguntaOM pOM = (PreguntaOM)(await this.preguntaService.GetById(respuestaDTO.PreguntaID));
                    if (pOM.Requerida && (respuestaDTO.OpcionesSeleccionadas == null || respuestaDTO.OpcionesSeleccionadas.Count == 0)) return "La pregunta de Opción Múltiple de ID " + pOM.PreguntaID.ToString() + " es requerida.";
                    break;

                case "OPCIONSIMPLE":

                    PreguntaOS pOS = (PreguntaOS)(await this.preguntaService.GetById(respuestaDTO.PreguntaID));
                    if (pOS.Requerida && respuestaDTO.OpcionSeleccionada == null) return "La pregunta de Opción Simple de ID " + pOS.PreguntaID.ToString() + " es requerida.";
                    break;
            }

            return "";
        }
    }
}
