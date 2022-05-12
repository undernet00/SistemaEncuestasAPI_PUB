using SistemaEncuestasBL.Models;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Services
{
    public interface IEncuestaService : IGenericService<Encuesta>
    {
        Encuesta TraerRelacionados(Encuesta encuesta);
        
    }
}
