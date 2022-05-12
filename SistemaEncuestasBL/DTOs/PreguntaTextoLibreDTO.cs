using SistemaEncuestasBL.Models;
using System.ComponentModel.DataAnnotations;

namespace SistemaEncuestasBL.DTOs
{
    public class PreguntaTextoLibreDTO
    {

        [Required(ErrorMessage = "El campo ID es requerido.")]
        public int PreguntaID { get; set; }

        [Required(ErrorMessage = "El campo TextoPregunta es requerido.")]
        public string TextoPregunta { get; set; }

        [Required]
        public int Orden { get; set; }

        [Required]
        public string Tipo { get; set; }

        [Required]
        public int EncuestaID { get; set; }
    }
}
