using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaEncuestasBL.Models
{
    public enum EstadoEncuesta
    {
        Borrador,
        Publicada,
        NoPublicada
    }
    public class Encuesta
    {

        #region Atributos
        [Key]
        public int EncuestaID { get; set; }

        [Required]
        public string Denominacion { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public int CantidadEncuestados { get; set; }

        public EstadoEncuesta Estado { get; set; }

        public string Objetivo { get; set; }
        
        
        public ICollection<Pregunta> Preguntas { get; set; }

        #endregion

        #region Constructores
        public Encuesta()
        {


        }

        public Encuesta(string denominacion, DateTime fechaInicio, DateTime fechaFin, EstadoEncuesta estado, string objetivo)
        {
            this.Denominacion = denominacion;
            this.FechaInicio = fechaInicio;
            this.FechaFin = fechaFin;
            this.Estado = estado;
            this.Objetivo = objetivo;


        }


        #endregion

    }
}
