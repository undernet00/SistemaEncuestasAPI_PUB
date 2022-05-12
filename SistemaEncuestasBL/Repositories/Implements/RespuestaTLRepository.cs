using SistemaEncuestasBL.Data;
using SistemaEncuestasBL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Repositories.Implements
{
    public class RespuestaTLRepository : GenericRepository<RespuestaTL>, IRespuestaTLRepository
    {
        SistemaEncuestasContext sistemaEncuestasContext;
        public RespuestaTLRepository(SistemaEncuestasContext sistemaEncuestasContext) : base(sistemaEncuestasContext)
        {

            this.sistemaEncuestasContext = sistemaEncuestasContext;
        }

        public ICollection<RespuestaTL> GetRespuestasSinAnalizar()
        {
            IQueryable<RespuestaTL> linqConsulta = from r in sistemaEncuestasContext.RespuestasTL where (r.Tipo == TipoPregunta.TEXTOLIBRE && r.FechaHoraAnalisis == null) select r;
            ICollection<RespuestaTL> respuesta = linqConsulta.ToList<RespuestaTL>();

            return respuesta;

        }

        public ICollection<string> GetResultadosRanking(Pregunta pregunta)
        {
            var lista = this.sistemaEncuestasContext.RespuestasTL

                    .Where(r => r.PreguntaID == pregunta.PreguntaID && r.Tipo == TipoPregunta.TEXTOLIBRE && r.FechaHoraAnalisis != null)
                    .GroupBy(r => r.Sentimiento)
                    .Select(g => new { Texto = g.Key.ToString(), Total = g.Count() })
                    .OrderByDescending(r => r.Total).ToList() //Query 
                        .Select(i => i.Texto + ": " + i.Total).ToList(); //Conversión a lista de String

            return lista;
        }

        public async Task<ICollection<RespuestaTL>> RespuestasPorPregunta(int preguntaID)
        {
            var respuesta = await this.sistemaEncuestasContext.Set<RespuestaTL>()
                .Where(r => r.PreguntaID == preguntaID && r.Tipo == TipoPregunta.TEXTOLIBRE && r.FechaHoraAnalisis != null)
                .OrderByDescending(r => r.FechaHoraContestada)
                .Include(r => r.Etiquetas)
                .ToListAsync();

            return respuesta;

        }
    }
}
