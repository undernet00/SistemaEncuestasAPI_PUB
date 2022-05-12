using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaEncuestasBL.Models;

namespace SistemaEncuestasBL.Services
{
    public interface IRespuestaService : IGenericService<Respuesta>
    {
        ICollection<Resultado> GetResultadosRanking(Pregunta pregunta);
        ICollection<Resultado> GetResultadosRankingEtiquetas(Pregunta pregunta);

    }
}
