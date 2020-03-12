using System;

namespace MethodDeprecation
{
    public class XmlReaderFactory
    {
        [Obsolete("CreateXml() method is deprecated. Use CreateXmlReader instead.", false)]
        public void CreateXml()
        {
        }

        // Using this method will cause compilation error
        [Obsolete("Create() method is deprecated. Use CreateXmlReader instead.", true)]
        public void Create()
        {
        }

        public void CreateXmlReader()
        {
        }
    }
}
