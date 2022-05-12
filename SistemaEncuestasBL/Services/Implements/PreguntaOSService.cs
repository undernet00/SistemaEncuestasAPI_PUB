using SistemaEncuestasBL.Models;
using SistemaEncuestasBL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Services.Implements
{
    public class PreguntaOSService : GenericService<PreguntaOS>, IPreguntaOSService
    {
        private readonly IPreguntaOSRepository preguntaOSRepository;
        public PreguntaOSService(IPreguntaOSRepository genericRepository) : base(genericRepository)
        {
            this.preguntaOSRepository = genericRepository;
        }

        public Task<bool> ActualizarOpciones(PreguntaOS preguntaEnObjeto, PreguntaOS preguntaEnBase)
        {
            return this.preguntaOSRepository.ActualizarOpciones(preguntaEnObjeto, preguntaEnBase);
        }
    }
}
