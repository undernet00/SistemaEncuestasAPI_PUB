using SistemaEncuestasBL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaEncuestasBL.DTOs
{

    public class RespuestaTLDTO
    {
        //[Required(ErrorMessage = "El campo ID es requerido.")]
        public int RespuestaTextoLibreID { get; set; }

        [Required(ErrorMessage = "El campo TextoRespuesta es requerido.")]
        public String TextoRespuesta { get; set; }

        public float Positivo { get; set; }

        public float Negativo { get; set; }

        public float Neutral { get; set; }

        public float Mixto { get; set; }

        public string Idioma { get; set; }

        public string Sentimiento { get; set; }
        public string FechaHoraAnalisis { get; set; }

        public string FechaHoraContestada { get; set; }

        [Required]
        public int PreguntaID { get; set; }

        public PersonaDTO Persona { get; set; }


        public ICollection<string> Etiquetas;

    }
}
