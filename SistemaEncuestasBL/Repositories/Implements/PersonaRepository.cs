using SistemaEncuestasBL.Data;
using SistemaEncuestasBL.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Repositories.Implements
{
    public class PersonaRepository : GenericRepository<Persona>, IPersonaRepository
    {
        private readonly SistemaEncuestasContext sistemaEncuestasContext;
        public PersonaRepository(SistemaEncuestasContext encuestaContext) : base(encuestaContext)
        {
            this.sistemaEncuestasContext = encuestaContext;
        }

        public async Task<int> BuscarEInsertarPersona(Persona persona)
        {
            if (persona.Correo != null) //Tiene Correo
            {
                Persona personaEnBase = await this.GetByEmail(persona.Correo.ToLower().Trim());

                if (personaEnBase != null) //Encontré persona y devuelvo su Id
                {
                    return personaEnBase.PersonaId;
                }
                else //No encontré a la persona e Inserto Persona
                {
                    Persona nuevaPersona = await this.Insert(persona);

                    return nuevaPersona.PersonaId;

                }

            }
            return 0;
        }

        public async Task<Persona> GetByEmail(string correo)
        {
            return await this.sistemaEncuestasContext.Personas.FirstOrDefaultAsync<Persona>(p => p.Correo == correo.ToLower().Trim());
        }
    }
}
