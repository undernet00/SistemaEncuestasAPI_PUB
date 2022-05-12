using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.DTOs
{
    public class UsuarioDTO
    {
        [Required]
        public int UsuarioID { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Clave { get; set; }

        public string RolSeguridad { get; set; }

        public string Nombre { get; set; }

    }
}
