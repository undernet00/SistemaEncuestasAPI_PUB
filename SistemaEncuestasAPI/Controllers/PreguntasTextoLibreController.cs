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
    //[RoutePrefix("api/PreguntasTextoLibre")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PreguntasTextoLibreController : ApiController
    {
        private readonly PreguntaTextoLibreService preguntasTextoLibreService = new PreguntaTextoLibreService(new PreguntaTLRepository(SistemaEncuestasContext.Crear()));

        private IMapper mapper;

        public PreguntasTextoLibreController()
        {
            this.mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {

            var preguntasTextoLibre = await preguntasTextoLibreService.GetAll();
            var preguntaTextoLibreDTO = preguntasTextoLibre.Select(x => mapper.Map<PreguntaTextoLibreDTO>(x));
            
            return Ok(preguntaTextoLibreDTO);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {

            var preguntaTextoLibre = await preguntasTextoLibreService.GetById(id);
            if (preguntaTextoLibre == null) return NotFound();

            var preguntaTextoLibreDTO = mapper.Map<PreguntaTextoLibreDTO>(preguntaTextoLibre);

            return Ok(preguntaTextoLibreDTO);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(PreguntaTextoLibreDTO preguntaTextoLibreDTO)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var preguntaTextoLibre = mapper.Map<PreguntaTL>(preguntaTextoLibreDTO);
            try
            {
                preguntaTextoLibre = await preguntasTextoLibreService.Insert(preguntaTextoLibre);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put(PreguntaTextoLibreDTO preguntaTextoLibreDTO, int id)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (preguntaTextoLibreDTO.PreguntaID != id) return BadRequest();

            var buscado = await preguntasTextoLibreService.GetById(id);
            if (buscado == null) return NotFound();

            try
            {
                var preguntaTextoLibre = mapper.Map<PreguntaTL>(preguntaTextoLibreDTO);
                preguntaTextoLibre = await preguntasTextoLibreService.Update(preguntaTextoLibre);
                return Ok(preguntaTextoLibre);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);

            }
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var buscado = await preguntasTextoLibreService.GetById(id);
            if (buscado == null) return NotFound();

            try
            {
                await preguntasTextoLibreService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);

            }

        }
    }
}
