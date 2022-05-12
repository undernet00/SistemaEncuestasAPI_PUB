using SistemaEncuestasBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Services
{
    public interface IPreguntaOSService : IGenericService<PreguntaOS>
    {
        Task<bool> ActualizarOpciones(PreguntaOS preguntaEnObjeto, PreguntaOS preguntaEnBase);
    }
}
