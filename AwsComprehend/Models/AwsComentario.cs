using AwsComprehend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace awsComprehend.Models
{
    public class AwsComentario
    {
        public int IdAuxiliar { get; set; }
        public String Texto { get; set; }
        public String Idioma { get; set; }
        public String Sentimiento { get; set; }
        public float PuntajePositivo { get; set; }
        public float PuntajeNegativo { get; set; }
        public float PuntajeNeutral { get; set; }
        public float PuntajeMixto { get; set; }
        public List<AwsFraseClave> FrasesClave { get; set; }

        public List<string> EtiquetasExternas { get; set; }

        public AwsComentario(String texto, String idioma, String sentimiento, float positivo, float negativo, float neutral, float mixto)
        {
            this.Texto = texto;
            this.Idioma = idioma;
            this.Sentimiento = sentimiento;
            this.PuntajePositivo = positivo;
            this.PuntajeNegativo = negativo;
            this.PuntajeNeutral = neutral;
            this.PuntajeMixto = mixto;
            this.FrasesClave = new List<AwsFraseClave>();
            this.EtiquetasExternas= new List<string>();
        }

        public AwsComentario(string texto)
        {
            Texto = texto;
        }

        public override string ToString()
        {
            String retorno = "";

            retorno += "IdAuxiliar: " + this.IdAuxiliar.ToString()+ "\n";
            retorno += "Texto: " + this.Texto + "\n";
            retorno += "Idioma: " + this.Idioma + "\n";
            retorno += "Sentimiento: " + this.Sentimiento + "\n";
            retorno += "Puntaje Positivo: " + this.PuntajePositivo.ToString() + "\n";
            retorno += "Puntaje Negativo: " + this.PuntajeNegativo.ToString() + "\n";
            retorno += "Puntaje Neutro: " + this.PuntajeNeutral.ToString() + "\n";
            retorno += "Puntaje Mixto: " + this.PuntajeMixto.ToString() + "\n";


            retorno += "Frases Clave: \n";
            foreach (AwsFraseClave frase in this.FrasesClave)
            {
                retorno += " -" + frase.Frase + "\n";
            }



            return retorno;
        }
    }
}
