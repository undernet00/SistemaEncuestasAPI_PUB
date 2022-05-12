using SistemaEncuestasBL.Data;
using SistemaEncuestasBL.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Repositories.Implements
{
    public class EtiquetaRepository : GenericRepository<Etiqueta>, IEtiquetaRepository
    {
        private readonly SistemaEncuestasContext sistemaEncuestasContext;
        public EtiquetaRepository(SistemaEncuestasContext encuestaContext) : base(encuestaContext)
        {
            this.sistemaEncuestasContext = encuestaContext;
        }

        public async Task<ICollection<Etiqueta>> ConvertirLista(List<string> listaStrings)
        {
            List<Etiqueta> respuesta = new List<Etiqueta>();

            foreach (string ee in listaStrings)
            {
                respuesta.Add(new Etiqueta(ee.ToUpper()));
            }

            return respuesta;
        }
    }
}
