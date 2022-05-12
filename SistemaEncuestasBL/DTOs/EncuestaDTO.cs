using SistemaEncuestasBL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaEncuestasBL.DTOs
{
    public class EncuestaDTO
    {

        public int EncuestaID { get; set; }

        public string Denominacion { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public int CantidadEncuestados { get; set; }

        public string Estado { get; set; }

        public string Objetivo { get; set; }
        public ICollection<PreguntaDTO> Preguntas { get; set; }

               
    }
}
