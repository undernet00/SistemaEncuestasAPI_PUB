using SistemaEncuestasBL.Data;
using SistemaEncuestasBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Repositories.Implements
{
    public class RespuestaOMRepository : GenericRepository<RespuestaOM>, IRespuestaOMRepository
    {
        private readonly SistemaEncuestasContext sistemaEncuestasContext;
        public RespuestaOMRepository(SistemaEncuestasContext encuestaContext) : base(encuestaContext)
        {
            this.sistemaEncuestasContext = encuestaContext;
        }

        public void CargarRelacionadas(RespuestaOM respuesta)
        {
            sistemaEncuestasContext.Respuestas.Attach(respuesta);
            sistemaEncuestasContext.Entry(respuesta).Collection(r => r.OpcionesSeleccionadas).Load();

        }
    }
}
