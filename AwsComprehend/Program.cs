using awsComprehend.Models;
using awsComprehend.Services;
using AwsComprehend.Services;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace awsComprehend
{
    class Program
    {
        static async Task Main(string[] args)
        {

            IComprehendService comprehendService = new ComprehendService();
            IComprehendService servicio = comprehendService;

            AwsComentario c = new AwsComentario("El ambiente era muy lindo. Los huevos rotos estaban muy ricos. Nunca había probado una sangría tan rica. Lo recomiendo.");


            var comentarioCompleto = await servicio.AnalizarComentario(c);



            

            

            //Console.WriteLine(c.ToString());

            ///*c = ComprehendController.GetSentiment("Este es el mejor vino carmenere que probé en mi vida. La verdad que los felicito.", "es");
            //c.FrasesClave = ComprehendService.GetKeyPhrases(c);*/

            ////Console.WriteLine(c.ToString());

            Console.ReadLine();

        }
    }
}
