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
    public class PreguntasOMController : ApiController
    {
        private readonly PreguntaOMService preguntaOMService = new PreguntaOMService(new PreguntaOMRepository(SistemaEncuestasContext.Crear()));

        private IMapper mapper;

        public PreguntasOMController()
        {
            this.mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {

            var preguntasOM = await preguntaOMService.GetAll();
            var preguntasOMDTO = preguntasOM.Select(x => mapper.Map<PreguntaOMDTO>(x));

            return Ok(preguntasOMDTO);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {

            var preguntaOM = await preguntaOMService.GetById(id);
            if (preguntaOM == null) return NotFound();

            var preguntaOMDTO = mapper.Map<PreguntaTextoLibreDTO>(preguntaOM);

            return Ok(preguntaOMDTO);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(PreguntaOMDTO preguntaOMDTO)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var preguntaOM = mapper.Map<PreguntaOM>(preguntaOMDTO);
            try
            {
                preguntaOM = await preguntaOMService.Insert(preguntaOM);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put(PreguntaOMDTO preguntaOMDTO, int id)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (preguntaOMDTO.PreguntaID != id) return BadRequest();

            var buscado = await preguntaOMService.GetById(id);
            if (buscado == null) return NotFound();

            try
            {
                var preguntaOM = mapper.Map<PreguntaOM>(preguntaOMDTO);
                preguntaOM = await preguntaOMService.Update(preguntaOM);
                return Ok(preguntaOM);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);

            }
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var buscado = await preguntaOMService.GetById(id);
            if (buscado == null) return NotFound();

            try
            {
                await preguntaOMService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);

            }

        }
    }
}
