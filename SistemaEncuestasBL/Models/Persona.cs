using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Models
{
    public class Persona
    {
        #region Atributos
        public int PersonaId { get; set; }

        public string Nombre { get; set; }

        public string Correo { get; set; }

        public string Celular { get; set; }

                
        #endregion

        #region Constructores
        public Persona()
        {

        }

        public Persona(string nombre, string correo, string celular)
        {
            this.Nombre = nombre;
            this.Correo = correo;
            this.Celular = celular;

        }
        #endregion


    }
}
