using SistemaEncuestasBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Repositories
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        Task<Usuario> GetUsuarioByEmail(string email);
    }
}



