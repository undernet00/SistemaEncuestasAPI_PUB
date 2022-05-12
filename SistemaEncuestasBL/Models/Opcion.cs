using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Models
{
    public class Opcion
    {
        public int OpcionID { get; set; }
        public string OpcionTexto { get; set; }
        
        
        public ICollection<RespuestaOM> RespuestasOM { get; set; }
        public Opcion()
        {

        }

        public Opcion(int opcionID, string opcionTexto)
        {
            this.OpcionID = opcionID;
            this.OpcionTexto = opcionTexto;
        }
    }

    
}
