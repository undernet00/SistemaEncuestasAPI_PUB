using System.Collections.Generic;

namespace SistemaEncuestasBL.Models
{
    public class Etiqueta
    {
        public int EtiquetaId { get; set; }

        public string Nombre { get; set; }

        public int RespuestaID { get; set; }

        public Etiqueta()
        {

        }

        public Etiqueta(string nombre)
        {
            this.Nombre = nombre;
        }
        public override string ToString()
        {

            return this.Nombre;
        }
    }
}


