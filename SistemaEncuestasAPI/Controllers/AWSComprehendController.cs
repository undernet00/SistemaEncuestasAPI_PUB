using AutoMapper;
using awsComprehend.Models;
using awsComprehend.Services;
using AwsComprehend.Services;
using SistemaEncuestasBL.Data;
using SistemaEncuestasBL.Models;
using SistemaEncuestasBL.Repositories.Implements;
using SistemaEncuestasBL.Services;
using SistemaEncuestasBL.Services.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SistemaEncuestasAPI.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AWSComprehendController : ApiController
    {
        private readonly IRespuestaTLService respuestaTLService = new RespuestaTLService(new RespuestaTLRepository(SistemaEncuestasContext.Crear()));


        //[HttpGet]
        public async Task<IHttpActionResult> CompletarComentariosFaltantes()
        {
            IMapper mapper = WebApiApplication.MapperConfiguration.CreateMapper(); ;
            IComprehendService comprehendService = new ComprehendService();

            List<AwsComentario> comentariosIncompletos = new List<AwsComentario>();

            ICollection<RespuestaTL> respuestasTLincompletas = respuestaTLService.GetRespuestasSinAnalizar();


            foreach (RespuestaTL rTL in respuestasTLincompletas)
            {
                comentariosIncompletos.Add(mapper.Map<AwsComentario>(rTL));
            }

            List<AwsComentario> comentariosCompletos = await comprehendService.CompletarDatosDeComentarios(comentariosIncompletos);


            foreach (AwsComentario awsC in comentariosCompletos)
            {

                RespuestaTL rTemp = respuestasTLincompletas.FirstOrDefault(re => re.RespuestaID == awsC.IdAuxiliar);
                rTemp.FechaHoraAnalisis = null;

                //Frases Clave
                foreach (AwsFraseClave f in awsC.FrasesClave)
                {
                    rTemp.FrasesClave.Add(mapper.Map<FraseClave>(f));
                }

                rTemp.Idioma = awsC.Idioma;
                rTemp.Mixto = awsC.PuntajeMixto;
                rTemp.Negativo = awsC.PuntajeNegativo;
                rTemp.Neutral = awsC.PuntajeNeutral;
                rTemp.Positivo = awsC.PuntajePositivo;
                rTemp.Sentimiento = mapper.Map<Sentimiento>(awsC.Sentimiento);
                rTemp.FechaHoraAnalisis = DateTime.Now;

                if (awsC.EtiquetasExternas != null)
                {
                    if (rTemp.Etiquetas == null) rTemp.Etiquetas = new List<Etiqueta>();
                    
                    foreach (string etiqueta in awsC.EtiquetasExternas)
                    {
                        
                        rTemp.Etiquetas.Add(new Etiqueta(etiqueta));
                    }
                }
                await respuestaTLService.Update(rTemp);
            }

            return Ok();
        }




    }


}