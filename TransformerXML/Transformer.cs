using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Xsl;


namespace TransformerXML
{
    public class Transformer
    {
        public string InputXML { get; set; }
        public string InputXSD { get; set; }
        public string OutputXML { get; set; }
        public string OutputXSD { get; set; }
        public string TemplateXSL { get; set; }

        public async void TransformAsync()
        {
            if (!(await Task.Run(() => ValidateSchema(InputXSD, InputXML))))
                return;
            if (!(await Task.Run(() => TransformFile(TemplateXSL, InputXML, OutputXML))))
                return;
            if (!(await Task.Run(() => ValidateSchema(OutputXSD, OutputXML))))
                return;
        }

        private bool TransformFile(string xsl, string input, string output)
        {
            if (!File.Exists(xsl))
            {
                Console.WriteLine("WARNING: TransformFile. File {0} does not exist!", xsl);
                return false;
            }
            try
            {
                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load(xsl);
                xslt.Transform(input, output);
                Console.WriteLine("OK: TransformFile");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: TransformFile. " + ex.Message);
                return false;
            }
        }

        private bool ValidateSchema(string xsd, string xml)
        {
            if (!File.Exists(xsd))
            {
                Console.WriteLine("WARNING: ValidateSchema. File {0} does not exist!", xsd);
                return false;
            }
            if (!File.Exists(xml))
            {
                Console.WriteLine("WARNING: ValidateSchema. File {0} does not exist!", xml);
                return false;
            }
            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.Schemas.Add(null, xsd);
                settings.ValidationType = ValidationType.Schema;
                settings.ValidationEventHandler += new ValidationEventHandler(ValidationEventHandler);
                XmlReader reader = XmlReader.Create(xml, settings);
                XmlDocument document = new XmlDocument();
                document.Load(reader);

                Console.WriteLine("OK: ValidateSchema {0}", xsd);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: ValidateSchema {0}, {1}. ", xsd, ex.Message);
                return false;
            }
        }

        private void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                Console.WriteLine("WARNING: ValidationEventHandler. " + e.Message);
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                Console.WriteLine("ERROR: ValidationEventHandler. " + e.Message);
            }
        }
    }
}
