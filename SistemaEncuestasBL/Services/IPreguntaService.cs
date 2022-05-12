using SistemaEncuestasBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Services
{
    public interface IPreguntaService : IGenericService<Pregunta>
    {
        Task<bool> LimpiarPreguntas(ICollection<Pregunta> nuevaLista, ICollection<Pregunta> viejaLista);
    }
}
