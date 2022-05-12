using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaEncuestasBL.Models;

namespace SistemaEncuestasBL.DTOs
{
    public class EncuestaRespuestaDTO
    {
        [Required]
        public int EncuestaID { get; set; }

        public ICollection<RespuestaDTO> Respuestas { get; set; }

        public PersonaDTO Encuestado { get; set; }

    }
}
