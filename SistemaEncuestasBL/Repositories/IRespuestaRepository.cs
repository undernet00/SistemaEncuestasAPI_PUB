using SistemaEncuestasBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Repositories
{
    public interface IRespuestaRepository : IGenericRepository<Respuesta>
    {

        ICollection<Resultado> GetResultadosRanking(Pregunta pregunta);

        ICollection<Resultado> GetResultadosRankingEtiquetas(Pregunta pregunta);

        
    }
}
