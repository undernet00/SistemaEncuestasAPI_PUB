using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Models
{
    public class PreguntaOS: Pregunta
    {

        public ICollection<Opcion> Opciones { get; set; }
        public ICollection<Respuesta> Respuestas { get; set; }

        public PreguntaOS()
        {
            this.Opciones = new List<Opcion>();
        }
    }
}
