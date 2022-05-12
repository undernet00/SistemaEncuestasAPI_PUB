using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;
using SistemaEncuestasBL.Data;
using SistemaEncuestasBL.Models;

namespace SistemaEncuestasBL.Repositories.Implements
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {

        private readonly SistemaEncuestasContext encuestaContext;
        public UsuarioRepository(SistemaEncuestasContext encuestaContext) : base(encuestaContext)
        {
            this.encuestaContext = encuestaContext;

        }

        public async Task<Usuario> GetUsuarioByEmail(string email)
        {

            return await encuestaContext.Usuarios.FirstOrDefaultAsync<Usuario>(u => u.Email == email); ;

        }
    }
}
