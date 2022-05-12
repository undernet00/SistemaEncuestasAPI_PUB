using SistemaEncuestasBL.Data;
using SistemaEncuestasBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SistemaEncuestasBL.Repositories.Implements
{
    public class RespuestaRepository : GenericRepository<Respuesta>, IRespuestaRepository
    {
        private SistemaEncuestasContext sistemaEncuestasContext;
        public RespuestaRepository(SistemaEncuestasContext encuestaContext) : base(encuestaContext)
        {
            this.sistemaEncuestasContext = encuestaContext;
        }

        public ICollection<Resultado> GetResultadosRanking(Pregunta pregunta)
        {

            switch (pregunta.Tipo)
            {
                case TipoPregunta.TEXTOLIBRE:
                    var listaTL = this.sistemaEncuestasContext.RespuestasTL

                    .Where(r => r.PreguntaID == pregunta.PreguntaID && r.Tipo == TipoPregunta.TEXTOLIBRE && r.FechaHoraAnalisis != null)
                    .GroupBy(r => r.Sentimiento)
                    .Select(g => new { Texto = g.Key.ToString(), Total = g.Count() })
                    .OrderByDescending(r => r.Total).ToList() //Query 
                    .Select(r => new Resultado { Texto = r.Texto, Total = r.Total }).ToList();

                    return listaTL;
                    break;

                case TipoPregunta.OPCIONMULTIPLE:

                    var listaOM = this.sistemaEncuestasContext.RespuestasOM
                        .Where(r => r.PreguntaID == pregunta.PreguntaID && r.Tipo == TipoPregunta.OPCIONMULTIPLE)
                        .SelectMany(r => r.OpcionesSeleccionadas, (r, o) => new { o.OpcionID, o.OpcionTexto })
                        .GroupBy(o => o.OpcionTexto)
                        .Select(g => new { Texto = g.Key.ToString(), Total = g.Count() })
                        .OrderByDescending(g => g.Total)
                        .Select(r => new Resultado { Texto = r.Texto, Total = r.Total }).ToList();


                    return listaOM;
                    break;
                case TipoPregunta.OPCIONSIMPLE:
                    var query = from respuesta in this.sistemaEncuestasContext.RespuestasOS
                                where respuesta.PreguntaID == pregunta.PreguntaID
                                join opcion in this.sistemaEncuestasContext.Opciones
                                on new { respuesta.OpcionSeleccionada.OpcionID }
                                equals new { opcion.OpcionID }
                                select new { opcion.OpcionID, opcion.OpcionTexto }; ;

                    var listaOS = query
                        .GroupBy(o => o.OpcionTexto)
                        .Select(g => new { Texto = g.Key, Total = g.Count() })
                        .OrderByDescending(g => g.Total)
                        .Select(r => new Resultado { Texto = r.Texto, Total = r.Total }).ToList();


                    return listaOS;
                    break;

                default:
                    return null;
            }




        }

        public ICollection<Resultado> GetResultadosRankingEtiquetas(Pregunta pregunta)
        {
            if (pregunta.Tipo == TipoPregunta.TEXTOLIBRE)
            {
                var query = from respuesta in this.sistemaEncuestasContext.RespuestasTL
                            where respuesta.PreguntaID == pregunta.PreguntaID
                            join etiqueta in this.sistemaEncuestasContext.Etiquetas
                            on new { respuesta.RespuestaID }
                            equals new { etiqueta.RespuestaID }
                            select new { respuesta.Sentimiento, etiqueta.Nombre };

                var lista = query
                   .GroupBy(o => new { o.Sentimiento, o.Nombre }, (key, group) => new
                   {
                       Key1 = key.Sentimiento,
                       Key2 = key.Nombre,
                       Result = group.ToList()
                   })
                   .Select(g => new { Texto = g.Key1, Etiqueta = g.Key2, Total = g.Result.Count() })
                   .OrderBy(g => g.Texto).ThenByDescending(g2 => g2.Total)
                   .Select(r => new Resultado { Texto = r.Texto.ToString(), Etiqueta = r.Etiqueta, Total = r.Total }).ToList();

                return lista;
            }
            else return null;

        }
    }
}
