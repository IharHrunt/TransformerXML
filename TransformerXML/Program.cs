using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransformerXML
{
    class Program
    {
        static void Main(string[] args)
        {
            Transformer tr = new Transformer
            {
                InputXML = @"data\books.xml",
                InputXSD = @"data\books.xsd",
                OutputXML = @"data\books_new.xml",
                OutputXSD = @"data\books_new.xsd",
                TemplateXSL = @"data\books.xsl"
            };

            tr.TransformAsync();
            Console.ReadLine();
        }
    }
}
