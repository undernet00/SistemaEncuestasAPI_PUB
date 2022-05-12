using SistemaEncuestasBL.Data;
using SistemaEncuestasBL.Models;
using System.Data.Entity;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Repositories.Implements
{
    public class PreguntaTLRepository : GenericRepository<PreguntaTL>,IPreguntaTLRepository
    {
        private readonly SistemaEncuestasContext sistemaEncuestasContext;
        public PreguntaTLRepository(SistemaEncuestasContext encuestasContext) : base (encuestasContext)
        {
            this.sistemaEncuestasContext = encuestasContext;
        }

        public async Task<bool> DeleteCheckOnEntity(int id)
        {
            //TODO: Implementar una búsqueda de respuestas de pregunta, de forma de avisar si existen para que no se borre la pregunta.
            var buscado = await sistemaEncuestasContext.Preguntas.AnyAsync(x => x.PreguntaID == id);
            return buscado;
        }
    }
}
