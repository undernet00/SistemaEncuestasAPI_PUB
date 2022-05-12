using SistemaEncuestasBL.Models;
using SistemaEncuestasBL.Repositories;
using System.Threading.Tasks;

namespace SistemaEncuestasBL.Services.Implements
{
    public class EncuestaService : GenericService<Encuesta>, IEncuestaService
    {
        private readonly IEncuestaRepository encuestaRepository;
        public EncuestaService(IEncuestaRepository encuestaRepository) : base(encuestaRepository)
        {
            this.encuestaRepository = encuestaRepository;
        }

        public Encuesta TraerRelacionados(Encuesta encuesta)
        {
            return encuestaRepository.TraerRelacionados(encuesta);
        }

        //public Encuesta HacerEditablesLasEntidades(Encuesta encuesta) {
        //    return this.encuestaRepository.HacerEditablesLasEntidades(encuesta);

        //}

        public Task<Encuesta> ActualizarRelacionados(Encuesta encuesta) {
            return this.encuestaRepository.ActualizarRelacionados(encuesta);
        
        }
    }
}