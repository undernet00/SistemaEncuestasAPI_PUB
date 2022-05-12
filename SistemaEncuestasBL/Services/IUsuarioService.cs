using SistemaEncuestasBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Services
{
    public interface IUsuarioService : IGenericService<Usuario>
    {
        Task<Usuario> GetUsuarioByEmail(string email);
    }
}
