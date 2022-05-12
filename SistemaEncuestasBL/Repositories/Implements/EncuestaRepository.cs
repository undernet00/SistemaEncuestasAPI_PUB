using SistemaEncuestasBL.Data;
using SistemaEncuestasBL.Models;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Repositories.Implements
{
    public class EncuestaRepository : GenericRepository<Encuesta>, IEncuestaRepository
    {

        private readonly SistemaEncuestasContext sistemaEncuestasContext;
        public EncuestaRepository(SistemaEncuestasContext encuestaContext) : base(encuestaContext)
        {
            this.sistemaEncuestasContext = encuestaContext;
        }

        public Encuesta TraerRelacionados(Encuesta encuesta)
        {
            if (encuesta == null) throw new ArgumentNullException();
            if (this.sistemaEncuestasContext.Entry(encuesta) == null) throw new ArgumentException("No se encuentra la Encuesta solicitada.");

            sistemaEncuestasContext.Entry(encuesta).Collection(e => e.Preguntas).Load();



            foreach (Pregunta p in encuesta.Preguntas)
            {

                switch (p.Tipo)
                {
                    case TipoPregunta.OPCIONMULTIPLE:
                        sistemaEncuestasContext.Entry((PreguntaOM)p).Collection(o => o.Opciones).Load();
                        break;
                    case TipoPregunta.OPCIONSIMPLE:
                        sistemaEncuestasContext.Entry((PreguntaOS)p).Collection(o => o.Opciones).Load();
                        break;
                }
            }

            return encuesta;

        }

        public async Task<Encuesta> ActualizarRelacionados(Encuesta encuestaEnObjeto)
        {
            Encuesta encuestaEnBase = this.sistemaEncuestasContext.Encuestas.Find(encuestaEnObjeto.EncuestaID);

            if (encuestaEnBase == null) return null;

            this.TraerRelacionados(encuestaEnBase);

            foreach (Pregunta preguntaEnObjeto in encuestaEnObjeto.Preguntas)
            {
                //**Modifico las Preguntas
                var preguntaEnBase = await this.sistemaEncuestasContext.Preguntas.FirstOrDefaultAsync(p => p.PreguntaID == preguntaEnObjeto.PreguntaID);
                if (preguntaEnBase != null)
                {

                    if (preguntaEnObjeto.Tipo == TipoPregunta.OPCIONMULTIPLE)
                    {
                        IPreguntaOMRepository preguntaOMRepository = new PreguntaOMRepository(this.sistemaEncuestasContext);
                        bool a = await preguntaOMRepository.ActualizarOpciones((PreguntaOM)preguntaEnObjeto, (PreguntaOM)preguntaEnBase);
                    }

                    if (preguntaEnObjeto.Tipo == TipoPregunta.OPCIONSIMPLE)
                    {
                        IPreguntaOSRepository preguntaOSRepository = new PreguntaOSRepository(this.sistemaEncuestasContext);
                        bool a = await preguntaOSRepository.ActualizarOpciones((PreguntaOS)preguntaEnObjeto, (PreguntaOS)preguntaEnBase);
                    }

                    this.sistemaEncuestasContext.Entry(preguntaEnBase).CurrentValues.SetValues(preguntaEnObjeto);
                }
                else
                {
                    // No se encontró la pregunta. Hay que agregarla
                    this.sistemaEncuestasContext.Preguntas.Add(preguntaEnObjeto);
                }
            }

            //Limpiar preguntas
            IPreguntaRepository preguntaRepository = new PreguntaRepository(sistemaEncuestasContext);
            await preguntaRepository.LimpiarPreguntas(encuestaEnObjeto.Preguntas, encuestaEnBase.Preguntas);

            this.sistemaEncuestasContext.SaveChanges();
            return encuestaEnObjeto;

        }

    }


}
