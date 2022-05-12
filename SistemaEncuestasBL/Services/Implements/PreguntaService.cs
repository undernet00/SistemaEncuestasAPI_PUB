using SistemaEncuestasBL.Models;
using SistemaEncuestasBL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Services.Implements
{
    public class PreguntaService : GenericService<Pregunta>, IPreguntaService
    {
        private readonly IPreguntaRepository preguntaRepository;
        public PreguntaService(IPreguntaRepository preguntaRepository) : base(preguntaRepository)
        {
            this.preguntaRepository = preguntaRepository;

        }
        public Task<bool> LimpiarPreguntas(ICollection<Pregunta> nuevaLista, ICollection<Pregunta> viejaLista)
        {
            return this.preguntaRepository.LimpiarPreguntas(nuevaLista, viejaLista);
        }
    }

    
}
