using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Models
{
    public class FraseClave
    {
        public int FraseClaveID { get; set; }

        public string Frase { get; set; }

        public int RespuestaID { get; set; }

    }
}
