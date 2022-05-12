using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SistemaEncuestasBL.Models
{

    public class RespuestaOM: Respuesta
    {

        #region Atributos

      
        public ICollection<Opcion> OpcionesSeleccionadas { get; set; }

        #endregion

        #region Constructores
        public RespuestaOM()
        {

            
        }

        #endregion




    }

}
