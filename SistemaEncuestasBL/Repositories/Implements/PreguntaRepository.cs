using SistemaEncuestasBL.Data;
using SistemaEncuestasBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Repositories.Implements
{
    public class PreguntaRepository : GenericRepository<Pregunta>, IPreguntaRepository

    {
        private SistemaEncuestasContext sistemaEncuestasContext;
        public PreguntaRepository(SistemaEncuestasContext encuestaContext) : base(encuestaContext)
        {
            this.sistemaEncuestasContext = encuestaContext;
        }

        public async Task<bool> LimpiarPreguntas(ICollection<Pregunta> nuevaLista, ICollection<Pregunta> viejaLista)
        {
            List<int> itemsParaBorrar = new List<int>();

            foreach (Pregunta pEnBase in viejaLista)
            {
                if (nuevaLista.FirstOrDefault(p => p.PreguntaID == pEnBase.PreguntaID) == null) //si no encuentro la pregunta en el nuevo objeto de encuesta
                {
                    itemsParaBorrar.Add(pEnBase.PreguntaID);
                }
            }

            IPreguntaRepository preguntaRepository = new PreguntaRepository(sistemaEncuestasContext);
            foreach (int id in itemsParaBorrar)
            {
                await preguntaRepository.Delete(id);
            }
            return true;
        }
    }

}