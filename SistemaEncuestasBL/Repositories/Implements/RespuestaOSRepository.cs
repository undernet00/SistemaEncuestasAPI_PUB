using SistemaEncuestasBL.Data;
using SistemaEncuestasBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Repositories.Implements
{
    public class RespuestaOSRepository : GenericRepository<RespuestaOS>, IRespuestaOSRepository
    {
        private readonly SistemaEncuestasContext sistemaEncuestasContext;

        public RespuestaOSRepository(SistemaEncuestasContext encuestaContext) : base(encuestaContext)
        {
            this.sistemaEncuestasContext = encuestaContext;
        }

        public void CargarRelacionadas(RespuestaOS respuesta)
        {
            sistemaEncuestasContext.Respuestas.Attach(respuesta);
            sistemaEncuestasContext.Entry(respuesta).Reference(p => p.OpcionSeleccionada).Load();
        }
    }
}
