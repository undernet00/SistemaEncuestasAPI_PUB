using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwsComprehend.Models
{
    public class EtiquetaExterna
    {

        public string Nombre { get; set; }

        public EtiquetaExterna()
        {
            

        }

        public EtiquetaExterna(string nombre)
        {
            this.Nombre = nombre;

        }
    }
}
