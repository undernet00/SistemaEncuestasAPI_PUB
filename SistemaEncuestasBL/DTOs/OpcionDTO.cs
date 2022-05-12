using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SistemaEncuestasBL.DTOs
{
    public class OpcionDTO
    {
        [Required]
        public int OpcionID{ get; set; }
        
        public string OpcionTexto { get; set; }

        
        public OpcionDTO()
        {

        }

        public OpcionDTO(int opcionID, string opcionTexto)
        {
            this.OpcionID = opcionID;
            this.OpcionTexto = opcionTexto;


        }
    }
}
