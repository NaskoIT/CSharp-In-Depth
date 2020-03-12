namespace MethodDeprecation
{
    class Program
    {
        static void Main(string[] args)
        {
            var xmlReaderFactory = new XmlReaderFactory();
            xmlReaderFactory.CreateXmlReader();

            // Compilation warning:
            // xmlReaderFactory.CreateXml();

            // Compilation error:
            // xmlReaderFactory.Create();
        }
    }
}
