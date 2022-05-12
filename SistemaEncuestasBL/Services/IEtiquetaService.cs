using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaEncuestasBL.Models;

namespace SistemaEncuestasBL.Services
{
    public interface IEtiquetaService : IGenericService<Etiqueta>
    {
        Task<ICollection<Etiqueta>> ConvertirLista(List<string> listaStrings);
        
    }
}
