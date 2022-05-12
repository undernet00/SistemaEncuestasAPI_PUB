using SistemaEncuestasBL.Models;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Repositories
{
    public interface IEncuestaRepository: IGenericRepository<Encuesta>
    {
        Encuesta TraerRelacionados(Encuesta encuesta); //Carga las listas de entidades relacionadas.
        
        Task<Encuesta> ActualizarRelacionados(Encuesta encuesta); //Actualizar los atributos relacionados a la Encuesta.

    }

    
}
