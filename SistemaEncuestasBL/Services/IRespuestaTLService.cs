using SistemaEncuestasBL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Services
{
    public interface IRespuestaTLService : IGenericService<RespuestaTL>
    {
        ICollection<RespuestaTL> GetRespuestasSinAnalizar();
        ICollection<string> GetResultadosRanking(Pregunta pregunta);

        Task<ICollection<RespuestaTL>> RespuestasPorPregunta(int preguntaID);
    }
}
