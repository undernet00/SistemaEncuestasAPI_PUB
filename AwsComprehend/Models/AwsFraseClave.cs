using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace awsComprehend.Models
{
    public class AwsFraseClave
    {
        public String Frase { get; set; }

        public AwsFraseClave(String frase)
        {
            this.Frase = frase;
        }

    }
}
