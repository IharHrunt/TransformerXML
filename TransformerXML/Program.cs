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
            string path = @"E:\Project\VS\TransformerXML\";

            Transformer tr = new Transformer
            {                
                InputXML = path + @"data\books.xml",
                InputXSD = path + @"data\books.xsd",
                OutputXML = path + @"data\books_new.xml",
                OutputXSD = path + @"data\books_new.xsd",
                TemplateXSL = path + @"data\books.xsl"
            };

            tr.TransformAsync();
            Console.ReadLine();
        }
    }
}
