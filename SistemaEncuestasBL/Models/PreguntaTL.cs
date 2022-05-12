using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaEncuestasBL.Models

{
    public class PreguntaTL : Pregunta
    {
        #region Atributos


        public ICollection<RespuestaTL> Respuestas { get; set; }

        #endregion

        #region Constructores
        public PreguntaTL():base()
        {
            
        }
        #endregion
    }
}
