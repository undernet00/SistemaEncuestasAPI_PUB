using SistemaEncuestasBL.Models;
using SistemaEncuestasBL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Services.Implements
{
    public class UsuarioService : GenericService<Usuario>, IUsuarioService

    {

        private readonly IUsuarioRepository usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository) : base(usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> GetUsuarioByEmail(string email)
        {
            return await this.usuarioRepository.GetUsuarioByEmail(email);
        }
    }
}
