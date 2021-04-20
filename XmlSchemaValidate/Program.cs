using System;
using System.Xml;
using System.Xml.Schema;

namespace XmlSchemaValidate {
    class Program {
        static void Main(string[] args) {
            XmlDocument document = new XmlDocument();
            document.Load("test.xml");

            XmlTextReader reader = new XmlTextReader("test.xsd");
            XmlSchema xmlschema = XmlSchema.Read(reader, null);
            XmlSchemaSet schemaSet = new XmlSchemaSet();
            schemaSet.Add(xmlschema);
            document.Schemas.Add(schemaSet);

            try {
                ValidationEventHandler eventHandler = new ValidationEventHandler(ValidationEventHandler);
                document.Validate(eventHandler);
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
        static void ValidationEventHandler(object sender, ValidationEventArgs e) {
            switch (e.Severity) {
                case XmlSeverityType.Error:
                    Console.WriteLine("Error: {0}", e.Message);
                    break;
                case XmlSeverityType.Warning:
                    Console.WriteLine("Warning {0}", e.Message);
                    break;
            }
        }
    }
}
