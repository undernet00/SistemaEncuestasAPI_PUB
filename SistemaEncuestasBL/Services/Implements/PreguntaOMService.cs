using SistemaEncuestasBL.Models;
using SistemaEncuestasBL.Repositories;
using System.Threading.Tasks;
namespace SistemaEncuestasBL.Services.Implements
{
    public class PreguntaOMService : GenericService<PreguntaOM>, IPreguntaOMService
    {
        private readonly IPreguntaOMRepository preguntaOMRepository;
        public PreguntaOMService(IPreguntaOMRepository preguntaOMRepository) : base(preguntaOMRepository)
        {
            this.preguntaOMRepository = preguntaOMRepository;
        }

        public Task<bool> ActualizarOpciones(PreguntaOM preguntaEnObjeto, PreguntaOM preguntaEnBase)
        {
            return this.preguntaOMRepository.ActualizarOpciones(preguntaEnObjeto, preguntaEnBase);
        }

        public PreguntaOM TraerRelacionados(PreguntaOM preguntaOM)
        {
            return this.TraerRelacionados(preguntaOM);
        }
    }
}
