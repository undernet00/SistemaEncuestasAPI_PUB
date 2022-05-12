
using Microsoft.Owin;
using Owin;
using SistemaEncuestasBL.Data;

[assembly: OwinStartup(typeof(SistemaEncuestasAPI.Startup))]

namespace SistemaEncuestasAPI
{
    public partial class Startup
    {

        // Para obtener más información sobre cómo configurar la aplicación, visite https://go.microsoft.com/fwlink/?LinkID=316888

        public void ConfigureAuth(IAppBuilder app)
        {
            //Una única instancia por Request.
            app.CreatePerOwinContext(SistemaEncuestasContext.Crear);

            
        }

        


    }
}
