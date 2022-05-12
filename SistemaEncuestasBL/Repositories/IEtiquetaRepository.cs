using SistemaEncuestasBL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Repositories
{
    public interface IEtiquetaRepository : IGenericRepository<Etiqueta>
    {
        Task<ICollection<Etiqueta>> ConvertirLista(List<string> listaStrings);
    }
}


