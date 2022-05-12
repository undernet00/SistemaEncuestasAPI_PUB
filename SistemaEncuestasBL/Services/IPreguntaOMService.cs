using SistemaEncuestasBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Services
{
    public interface IPreguntaOMService : IGenericService<PreguntaOM>
    {
        PreguntaOM TraerRelacionados(PreguntaOM preguntaOM);

        Task<bool> ActualizarOpciones(PreguntaOM preguntaEnObjeto, PreguntaOM preguntaEnBase);
    }
}
