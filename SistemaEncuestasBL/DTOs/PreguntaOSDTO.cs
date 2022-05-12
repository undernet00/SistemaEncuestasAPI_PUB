using SistemaEncuestasBL.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SistemaEncuestasBL.DTOs
{
    public class PreguntaOSDTO
    {
        [Required(ErrorMessage = "El campo ID es requerido.")]
        public int PreguntaID { get; set; }

        [Required(ErrorMessage = "El campo TextoPregunta es requerido.")]
        public string TextoPregunta { get; set; }

        [Required]
        public int Orden { get; set; }

        [Required]
        public string Tipo { get; set; }

        public ICollection<Opcion> Opciones { get; set; }

        [Required]
        public int EncuestaID { get; set; }
    }
}
