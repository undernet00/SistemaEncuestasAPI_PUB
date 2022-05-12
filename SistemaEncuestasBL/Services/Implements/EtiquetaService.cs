using SistemaEncuestasBL.Models;
using SistemaEncuestasBL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Services.Implements
{
    public class EtiquetaService : GenericService<Etiqueta>, IEtiquetaService
    {
        private readonly IEtiquetaRepository etiquetaRepository;
        public EtiquetaService(IEtiquetaRepository etiquetaRepository) : base(etiquetaRepository)
        {
            this.etiquetaRepository = etiquetaRepository;
        }

        public Task<ICollection<Etiqueta>> ConvertirLista(List<string> listaStrings)
        {
            return this.etiquetaRepository.ConvertirLista(listaStrings);
        }
    }
}
