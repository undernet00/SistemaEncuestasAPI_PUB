using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Models
{
    public enum TipoPregunta
    {
        TEXTOLIBRE,
        OPCIONMULTIPLE,
        OPCIONSIMPLE
    }
    [Table("Pregunta", Schema = "dbo")]
    public abstract class Pregunta
    {
        #region Atributos
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int PreguntaID { get; set; }
        public string TextoPregunta { get; set; }

        public int Orden { get; set; }

        public TipoPregunta Tipo { get; set; }


        public int EncuestaID { get; set; }
        [ForeignKey("EncuestaID")]

        public Encuesta Encuesta { get; set; }

        public bool Requerida { get; set; }
        
        #endregion

        #region Constructores
        public Pregunta()
        {
            this.Orden = -1;
        }
        #endregion
    }
}
