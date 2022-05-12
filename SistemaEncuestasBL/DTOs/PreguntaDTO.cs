using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaEncuestasBL.DTOs
{
    public class PreguntaDTO
    {
        public int PreguntaID { get; set; }

        [Required]
        public string TextoPregunta { get; set; }

        [Required]
        public string Tipo { get; set; }

        [Required]
        public int Orden { get; set; }

        [Required]
        public int EncuestaID { get; set; }

        [Required]
        public bool Requerida { get; set; }

        public ICollection<OpcionDTO> Opciones { get; set; }

        public ICollection<ResultadoDTO> Resultados { get; set; }

        public ICollection<ResultadoDTO> ResultadosML { get; set; }


    }
}
