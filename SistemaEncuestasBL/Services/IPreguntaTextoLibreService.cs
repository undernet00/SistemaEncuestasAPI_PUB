using SistemaEncuestasBL.Models;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Services
{
    public interface IPreguntaTextoLibreService: IGenericService<PreguntaTL>
    {
        Task<bool> DeleteCheckOnEntity(int id);
    }
}
