using SistemaEncuestasBL.Models;
using SistemaEncuestasBL.Repositories;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Services.Implements
{
    public class PreguntaTextoLibreService: GenericService<PreguntaTL> , IPreguntaTextoLibreService
    {
        private readonly IPreguntaTLRepository preguntaTextoLibreRepository;
        public PreguntaTextoLibreService(IPreguntaTLRepository preguntaTextoLibreRepository) : base(preguntaTextoLibreRepository)
        {
            this.preguntaTextoLibreRepository = preguntaTextoLibreRepository;
        }

        public async Task<bool> DeleteCheckOnEntity(int id)
        {
            return await preguntaTextoLibreRepository.DeleteCheckOnEntity(id);
        }
    }
}
