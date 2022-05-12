using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaEncuestasBL.DTOs
{
    public class RespuestaDTO
    {
        public int RespuestaID { get; set; }

        public DateTime? FechaHoraContestada { get; set; }

        [Required]
        public string Tipo { get; set; }

        [Required]
        public int PreguntaID { get; set; }


        public string TextoRespuesta { get; set; }

        public OpcionDTO OpcionSeleccionada { get; set; }

        public ICollection<OpcionDTO> OpcionesSeleccionadas { get; set; }

        PersonaDTO Persona { get; set; }
    }

}
