using SistemaEncuestasBL.Data;
using SistemaEncuestasBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Repositories.Implements
{
    public class FraseClaveRepository : GenericRepository<FraseClave>, IFraseClaveRepository
    {
        private readonly SistemaEncuestasContext encuestaContext;
        public FraseClaveRepository(SistemaEncuestasContext encuestaContext) : base(encuestaContext)
        {
            this.encuestaContext = encuestaContext;

        }
    }
}
