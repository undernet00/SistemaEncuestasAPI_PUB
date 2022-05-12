using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaEncuestasBL.Models
{
    public enum Sentimiento
    {
        POSITIVO,
        NEGATIVO,
        NEUTRO,
        MIXTO
    }

    public class RespuestaTL : Respuesta
    {
        #region Atributos

        public string TextoRespuesta { get; set; }

        [Column(TypeName = "float")]
        public float Positivo { get; set; }

        [Column(TypeName = "float")]
        public float Negativo { get; set; }

        [Column(TypeName = "float")]
        public float Neutral { get; set; }

        [Column(TypeName = "float")]
        public float Mixto { get; set; }

        public Sentimiento Sentimiento { get; set; }

        public string Idioma { get; set; }

        public ICollection<FraseClave> FrasesClave { get; set; }

        public DateTime? FechaHoraAnalisis { get; set; }

        public ICollection<Etiqueta> Etiquetas {get;set;}

        #endregion


        #region Constructores
        public RespuestaTL()
        {

            //this.FrasesClave = new List<string>();
            ////Los atributos fecha deben ser inicializados con valores. Para evitar el null llegando a la base. 
            //this.FechaHoraAnalisis = new DateTime(1900, 01, 01, 0, 0, 0);
            //this.FechaHoraContestada = new DateTime(1900, 01, 01, 0, 0, 0);
            this.FrasesClave = new List<FraseClave>();
        }

        #endregion


    }
}

