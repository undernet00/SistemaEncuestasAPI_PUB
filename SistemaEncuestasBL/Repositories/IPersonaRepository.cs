using SistemaEncuestasBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Repositories
{
    public interface IPersonaRepository : IGenericRepository<Persona>
    {
        Task<Persona> GetByEmail(string correo);
        Task<int> BuscarEInsertarPersona(Persona persona);

    }
}
