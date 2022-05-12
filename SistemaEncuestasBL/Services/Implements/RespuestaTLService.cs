using SistemaEncuestasBL.Models;
using SistemaEncuestasBL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Services.Implements
{
    public class RespuestaTLService : GenericService<RespuestaTL>,IRespuestaTLService
    {
        private readonly IRespuestaTLRepository respuestaTextoLibreRepository;
        public RespuestaTLService(IRespuestaTLRepository respuestaTextoLibreRepository) : base(respuestaTextoLibreRepository) {
            this.respuestaTextoLibreRepository = respuestaTextoLibreRepository;
        }

        public ICollection<RespuestaTL> GetRespuestasSinAnalizar()
        {
            return this.respuestaTextoLibreRepository.GetRespuestasSinAnalizar();

        }

        public ICollection<string> GetResultadosRanking(Pregunta pregunta)
        {
            return this.respuestaTextoLibreRepository.GetResultadosRanking(pregunta);
        }

        public Task<ICollection<RespuestaTL>> RespuestasPorPregunta(int preguntaID)
        {
            return this.respuestaTextoLibreRepository.RespuestasPorPregunta(preguntaID);
        }
    }
}
