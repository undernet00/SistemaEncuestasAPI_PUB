using awsComprehend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwsComprehend.Services
{
    public interface IComprehendService
    {
        Task<List<AwsComentario>> CompletarDatosDeComentarios(List<AwsComentario> comentariosIncompletos);
        Task<AwsComentario> AnalizarComentario(AwsComentario comentarioIncompleto);

    }
}
