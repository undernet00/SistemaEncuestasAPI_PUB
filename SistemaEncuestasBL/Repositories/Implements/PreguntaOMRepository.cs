using SistemaEncuestasBL.Data;
using SistemaEncuestasBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Repositories.Implements
{

    public class PreguntaOMRepository : GenericRepository<PreguntaOM>, IPreguntaOMRepository
    {
        private readonly SistemaEncuestasContext sistemaEncuestasContext;
        public PreguntaOMRepository(SistemaEncuestasContext encuestaContext) : base(encuestaContext)
        {
            this.sistemaEncuestasContext = encuestaContext;
        }

        public PreguntaOM TraerRelacionados(PreguntaOM preguntaOM)
        {
            sistemaEncuestasContext.Entry(preguntaOM).Collection(p => p.Opciones).Load();

            return preguntaOM;
        }

        public void CargarRelacionados(PreguntaOM preguntaOM)
        {
            sistemaEncuestasContext.Entry(preguntaOM).Collection(p => p.Opciones).Load();
        }

        public async Task<bool> ActualizarOpciones(PreguntaOM preguntaEnObjeto, PreguntaOM preguntaEnBase)
        {
            IOpcionRepository opcionRepository = new OpcionRepository(sistemaEncuestasContext);

            //Editar Contenido de Opciones
            foreach (Opcion o in preguntaEnObjeto.Opciones)
            {
                if (o.OpcionID > 0) //Opción existente
                {
                    this.sistemaEncuestasContext.Entry(this.sistemaEncuestasContext.Opciones.Find(o.OpcionID)).CurrentValues.SetValues(o);
                }
                else //Nueva Opción
                {
                    var opcion = await opcionRepository.Insert(o);
                    o.OpcionID = opcion.OpcionID;
                    this.sistemaEncuestasContext.Opciones.Attach(opcion);
                    preguntaEnBase.Opciones.Add(opcion);
                    this.sistemaEncuestasContext.SaveChanges();
                }
            }

            //Quitar Opciones que no se necesitan más
            List<int> listaParaEliminar = new List<int>();

            foreach (Opcion oEnBase in preguntaEnBase.Opciones)
            {
                if (preguntaEnObjeto.Opciones.FirstOrDefault(o => o.OpcionID == oEnBase.OpcionID) == null) listaParaEliminar.Add(oEnBase.OpcionID);
            }

            
            foreach (int id in listaParaEliminar)
            {
                await opcionRepository.Delete(id);
            }


            return true;
            //TODO: quizás hay que elegir otros retornos con otros resultados :P
            


        }

    }
}
