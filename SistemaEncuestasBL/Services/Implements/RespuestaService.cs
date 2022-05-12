using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaEncuestasBL.Models;
using SistemaEncuestasBL.Repositories;

namespace SistemaEncuestasBL.Services.Implements
{
    public class RespuestaService : GenericService<Respuesta>, IRespuestaService
    {
        private readonly IRespuestaRepository respuestaRepository;
        public RespuestaService(IRespuestaRepository respuestaRepository) : base(respuestaRepository)
        {
            this.respuestaRepository = respuestaRepository;
        }


        public ICollection<Resultado> GetResultadosRanking(Pregunta pregunta)
        {
            return this.respuestaRepository.GetResultadosRanking(pregunta);
        }

        public ICollection<Resultado> GetResultadosRankingEtiquetas(Pregunta pregunta)
        {
            return this.respuestaRepository.GetResultadosRankingEtiquetas(pregunta);

        }
    }
}
