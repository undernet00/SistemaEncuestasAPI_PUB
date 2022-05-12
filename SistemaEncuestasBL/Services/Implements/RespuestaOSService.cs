using SistemaEncuestasBL.Models;
using SistemaEncuestasBL.Repositories;
using SistemaEncuestasBL.Repositories.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Services.Implements
{
    public class RespuestaOSService : GenericService<RespuestaOS>, IRespuestaOSService
    {
        private readonly IRespuestaOSRepository respuestaOSRepository;
        public RespuestaOSService(IRespuestaOSRepository genericRepository) : base(genericRepository)
        {
            this.respuestaOSRepository = genericRepository;

        }

        public void CargarRelacionadas(RespuestaOS respuesta)
        {
            respuestaOSRepository.CargarRelacionadas(respuesta);
        }
    }
}


