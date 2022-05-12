using awsComprehend.Models;
using AwsComprehend.Services;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace awsComprehend.Services
{
    public class ComprehendService : IComprehendService
    {
        private List<AwsComentario> textos = new List<AwsComentario>();
        private List<AwsFraseClave> frasesClaves = new List<AwsFraseClave>();
        public string UrlExterna { get; set; } //Si es distinta a "" entonces se consulta y se obtienen tags

        public static List<String> AllowedLangs = new List<string> { "ar", "hi", "ko", "zh - TW", "ja", "zh", "de", "pt", "en", "it", "fr", "es" };

        public ComprehendService()
        {
        }

        //Recorre una lista de comentarios y analiza cada item. 
        public async Task<List<AwsComentario>> CompletarDatosDeComentarios(List<AwsComentario> comentariosIncompletos)
        {
            //lista de todos los comentarios que no tienen frases clave
            List<AwsComentario> respuesta = new List<AwsComentario>();

            foreach (AwsComentario c in comentariosIncompletos)
            {
                if (c.Texto != "")
                {
                    respuesta.Add(await this.AnalizarComentario(c));

                }
            }
            return respuesta;
        }

        //Punto de entrada del proceso de análisis completo de TL
        public async Task<AwsComentario> AnalizarComentario(AwsComentario comentarioIncompleto)
        {
            if (comentarioIncompleto.Texto != "")
            {
                AwsComentario comentarioConSentiment = ComprehendService.GetSentiment(comentarioIncompleto);
                if (comentarioConSentiment != null && comentarioConSentiment.Sentimiento != "")
                {
                    comentarioConSentiment.FrasesClave = ComprehendService.GetKeyPhrases(comentarioConSentiment);
                    comentarioConSentiment.IdAuxiliar = comentarioIncompleto.IdAuxiliar;

                    if (this.UrlExterna != "")
                    {
                        List<string> lista = await EtiquetarTexto(comentarioIncompleto.Texto);

                        if (lista != null) comentarioConSentiment.EtiquetasExternas = lista;
                    }

                    return comentarioConSentiment;
                }
            }
            return null;
        }

        private static AwsComentario GetSentiment(AwsComentario comentario)
        {
            using (var comprehendClient = new Amazon.Comprehend.AmazonComprehendClient(Amazon.RegionEndpoint.USEast1))
            {

                var sentimentRequest = new Amazon.Comprehend.Model.DetectSentimentRequest();
                sentimentRequest.Text = comentario.Texto;
                sentimentRequest.LanguageCode = ComprehendService.GetDominantLanguage(comentario.Texto);

                if (AllowedLangs.Contains(sentimentRequest.LanguageCode))
                {
                    var sentimentResult = comprehendClient.DetectSentimentAsync(sentimentRequest).GetAwaiter().GetResult();

                    // Sentiment: POSITIVE | NEGATIVE | NEUTRAL | MIXED
                    return new AwsComentario(comentario.Texto,
                                             sentimentRequest.LanguageCode,
                                             sentimentResult.Sentiment,
                                             sentimentResult.SentimentScore.Positive,
                                             sentimentResult.SentimentScore.Negative,
                                             sentimentResult.SentimentScore.Neutral,
                                             sentimentResult.SentimentScore.Mixed);

                }
                else
                {
                    return new AwsComentario(comentario.Texto, "", "", 0, 0, 0, 0);
                }


            }
        }

        private static List<AwsFraseClave> GetKeyPhrases(AwsComentario comentario)
        {
            using (var comprehendClient = new Amazon.Comprehend.AmazonComprehendClient(Amazon.RegionEndpoint.USEast1))
            {
                if (AllowedLangs.Contains(comentario.Idioma))
                {
                    var comprehendRequest = new Amazon.Comprehend.Model.DetectKeyPhrasesRequest();
                    comprehendRequest.Text = comentario.Texto;
                    comprehendRequest.LanguageCode = comentario.Idioma;
                    var comprehendResult = comprehendClient.DetectKeyPhrasesAsync(comprehendRequest).GetAwaiter().GetResult();

                    List<AwsFraseClave> frasesClave = new List<AwsFraseClave>();

                    foreach (Amazon.Comprehend.Model.KeyPhrase frase in comprehendResult.KeyPhrases)
                    {
                        AwsFraseClave fraseClave = new AwsFraseClave(frase.Text);
                        frasesClave.Add(fraseClave);
                    }

                    Console.WriteLine(frasesClave.Count());


                    if (frasesClave.Count() == 0)
                    {
                        frasesClave.Add(new AwsFraseClave(comentario.Texto));
                    }

                    return frasesClave;
                }

                return null;
            }

        }

        private static string GetDominantLanguage(string mensaje)
        {
            using (var comprehendClient = new Amazon.Comprehend.AmazonComprehendClient(Amazon.RegionEndpoint.USEast1))
            {
                var comprehendRequest = new Amazon.Comprehend.Model.DetectDominantLanguageRequest();
                comprehendRequest.Text = mensaje;

                var comprehendResponse = comprehendClient.DetectDominantLanguage(comprehendRequest);

                if (AllowedLangs.Contains(comprehendResponse.Languages[0].LanguageCode)) return comprehendResponse.Languages[0].LanguageCode;

                return "";
            }

        }

        private async static Task<List<string>> EtiquetarTextoOld(string Texto)
        {
            List<string> etiquetas = new List<string>();

            if (Texto != "")
            {
                //ICollection<EtiquetaExterna> respuesta = new List<EtiquetaExterna>();

                var client = new RestClient("http://190.64.90.147:5000/modelo/");
                //var client = new RestClient("http://127.0.0.1:5000/modelo/");

                //var request = new RestRequest(HttpUtility.UrlEncode(Texto) + "/");

                var request = new RestRequest(Texto + "/");
                var response = await client.GetAsync(request);
                Trace.WriteLine("RequestURLEncoded: " + HttpUtility.UrlEncode(Texto) + "/");
                Trace.WriteLine("RequestURLSinEncode: " + Texto + "/");
                Trace.WriteLine("Texto: " + Texto);
                Trace.WriteLine("Respuesta: " + response.Content);

                etiquetas = JsonConvert.DeserializeObject<List<string>>(response.Content);



            }
            return etiquetas;
        }

        /// <summary>
        /// Esta nueva verisón permite enviar el texto dentro de un json en el body de la req. Permite menos riesgo al pasar grandes cantidades de texto.
        /// </summary>
        /// <param name="Texto"></param>
        /// <returns></returns>
        private async static Task<List<string>> EtiquetarTexto(string Texto)
        {
            List<string> etiquetas = new List<string>();

            if (Texto != "")
            {
                var client = new RestClient("http://190.64.90.147:5000");
                var request = new RestRequest("/modelo", Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/json");


                request.AddBody(request.AddJsonBody(
                   new
                   {
                       Mensaje = Texto
                   }, "application/json"));


                var response = await client.PostAsync(request);

                Trace.WriteLine(response.Content);
                etiquetas = JsonConvert.DeserializeObject<List<string>>(response.Content);

                return etiquetas.Select(e => e = e.ToUpper()).ToList();
            }
            return null;
        }
    }
}


