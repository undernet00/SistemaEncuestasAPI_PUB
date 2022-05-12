using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaEncuestasBL.Models;

namespace SistemaEncuestasBL.Services
{
    public interface IPersonaService : IGenericService<Persona>
    {

        Task<Persona> GetByEmail(string correo);
        Task<int> BuscarEInsertarPersona(Persona persona);
    }
}
