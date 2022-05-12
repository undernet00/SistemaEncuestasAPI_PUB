using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Models
{
    public class PreguntaOM : Pregunta
    {
        #region Atributos

        public ICollection<Opcion> Opciones { get; set; }
        public ICollection<Respuesta> Respuestas { get; set; }


        
        //public int PreguntaID { get; set; }
        //public Pregunta Pregunta { get; set; }

        #endregion

        #region Constructores
        public PreguntaOM()
        {

        }
        #endregion
    }

}
