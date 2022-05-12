using SistemaEncuestasBL.Models;
using SistemaEncuestasBL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Services.Implements
{
    public class FraseClaveService : GenericService<FraseClave>, IFraseClaveService
    {
        private readonly IFraseClaveRepository fraseClaveRepository;
        public FraseClaveService(IFraseClaveRepository fraseClaveRepository) : base(fraseClaveRepository)
        {
            this.fraseClaveRepository = fraseClaveRepository;

        }
    }
}
