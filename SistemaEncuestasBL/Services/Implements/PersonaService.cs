using SistemaEncuestasBL.Models;
using SistemaEncuestasBL.Repositories;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Services.Implements
{
    public class PersonaService : GenericService<Persona>, IPersonaService
    {
        private readonly IPersonaRepository personaRepository;
        public PersonaService(IPersonaRepository personaRepository) : base(personaRepository)
        {
            this.personaRepository = personaRepository;
        }

        public async Task<int> BuscarEInsertarPersona(Persona persona)
        {
            return await this.personaRepository.BuscarEInsertarPersona(persona);
        }

        public async Task<Persona> GetByEmail(string correo)
        {
            return await this.personaRepository.GetByEmail(correo);
        }
    }
}