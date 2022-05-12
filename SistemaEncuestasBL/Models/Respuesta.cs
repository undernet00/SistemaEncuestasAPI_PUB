using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Models
{
    
    public abstract class Respuesta 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RespuestaID { get; set; }

        public DateTime? FechaHoraContestada { get; set; }

        public TipoPregunta Tipo { get; set; }

        public int PersonaID { get; set; }

        public int PreguntaID { get; set; }
        [ForeignKey("PreguntaID")]
        public PreguntaTL Pregunta { get; set; }

        

    }
}
