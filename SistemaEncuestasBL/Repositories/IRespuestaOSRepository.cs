using SistemaEncuestasBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Repositories
{
    public interface IRespuestaOSRepository : IGenericRepository<RespuestaOS> 
    {
        void CargarRelacionadas(RespuestaOS respuesta);
    }
}
