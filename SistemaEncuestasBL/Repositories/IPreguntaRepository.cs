using SistemaEncuestasBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Repositories
{
    public interface IPreguntaRepository : IGenericRepository<Pregunta>
    {
        Task<bool> LimpiarPreguntas(ICollection<Pregunta> nuevaLista, ICollection<Pregunta> viejaLista);

        
        
    }
}
