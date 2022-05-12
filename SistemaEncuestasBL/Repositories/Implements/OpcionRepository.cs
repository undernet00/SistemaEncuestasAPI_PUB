using SistemaEncuestasBL.Data;
using SistemaEncuestasBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Repositories.Implements
{
    public class OpcionRepository : GenericRepository<Opcion>, IOpcionRepository
    {
        private readonly SistemaEncuestasContext encuestasContext;
        public OpcionRepository(SistemaEncuestasContext encuestaContext) : base(encuestaContext)
        {
            this.encuestasContext = encuestaContext;

        }

        
    }
}
