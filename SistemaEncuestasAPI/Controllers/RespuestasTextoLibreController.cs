using AutoMapper;
using SistemaEncuestasBL.Data;
using SistemaEncuestasBL.DTOs;
using SistemaEncuestasBL.Models;
using SistemaEncuestasBL.Repositories.Implements;
using SistemaEncuestasBL.Services.Implements;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SistemaEncuestasAPI.Controllers
{
    //[Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class RespuestasTextoLibreController : ApiController
    {
        private readonly RespuestaTLService respuestasTextoLibreService = new RespuestaTLService(new RespuestaTLRepository(SistemaEncuestasContext.Crear()));


        private IMapper mapper;

        public RespuestasTextoLibreController()
        {
            this.mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {

            var respuestasTextoLibre = await respuestasTextoLibreService.GetAll();
            var respuestasTextoLibreDTO = respuestasTextoLibre.Select(x => mapper.Map<RespuestaTLDTO>(x));

            return Ok(respuestasTextoLibreDTO);
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {

            var respuestaTextoLibre = await respuestasTextoLibreService.GetById(id);
            if (respuestaTextoLibre == null) return NotFound();

            var RespuestaTextoLibreDTO = mapper.Map<RespuestaTLDTO>(respuestaTextoLibre);

            return Ok(RespuestaTextoLibreDTO);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(RespuestaTLDTO RespuestaTextoLibreDTO)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (RespuestaTextoLibreDTO.PreguntaID == 0 || RespuestaTextoLibreDTO.PreguntaID == -1) return BadRequest(ModelState);

            var respuestaTextoLibre = mapper.Map<RespuestaTL>(RespuestaTextoLibreDTO);
            try
            {

                respuestaTextoLibre.PreguntaID = RespuestaTextoLibreDTO.PreguntaID;
                respuestaTextoLibre = await respuestasTextoLibreService.Insert(respuestaTextoLibre);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put(RespuestaTLDTO RespuestaTextoLibreDTO, int id)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (RespuestaTextoLibreDTO.RespuestaTextoLibreID != id) return BadRequest();

            var buscado = await respuestasTextoLibreService.GetById(id);
            if (buscado == null) return NotFound();

            try
            {
                var respuestaTextoLibre = mapper.Map<RespuestaTL>(RespuestaTextoLibreDTO);
                respuestaTextoLibre = await respuestasTextoLibreService.Update(respuestaTextoLibre);
                return Ok(respuestaTextoLibre);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);

            }
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var buscado = await respuestasTextoLibreService.GetById(id);
            if (buscado == null) return NotFound();

            try
            {
                await respuestasTextoLibreService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);

            }

        }
    }
}

