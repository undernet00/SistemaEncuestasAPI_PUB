using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.DTOs
{

    public class PersonaDTO
    {
        public int PersonaId { get; set; }

        public string Nombre { get; set; }

        public string Correo { get; set; }

        public string Celular { get; set; }


        public PersonaDTO()
        {

        }

        public PersonaDTO(int PersonaId, string Nombre, string Correo, string Celular)
        {
            this.PersonaId = PersonaId;
            this.Nombre = Nombre;
            this.Correo = Correo;
            this.Celular = Celular;

        }
    }
}
