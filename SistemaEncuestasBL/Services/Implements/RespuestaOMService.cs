using SistemaEncuestasBL.Models;
using SistemaEncuestasBL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Services.Implements
{
    public class RespuestaOMService : GenericService<RespuestaOM>, IRespuestaOMService
    {
        private readonly IRespuestaOMRepository respuestaOMRepository;
        public RespuestaOMService(IRespuestaOMRepository respuestaOMRepository) : base(respuestaOMRepository)
        {
            this.respuestaOMRepository = respuestaOMRepository;
        }

        public void CargarRelacionadas(RespuestaOM respuesta)
        {
            respuestaOMRepository.CargarRelacionadas(respuesta);
        }
    }
}
