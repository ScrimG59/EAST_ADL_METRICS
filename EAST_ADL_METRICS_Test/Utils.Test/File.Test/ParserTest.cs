using NUnit.Framework;
using System.Xml.Linq;
using EAST_ADL_METRICS.Utils.Parser;

namespace EAST_ADL_METRICS_Test.Utils.Test.File.Test
{
    public class ParserTest
    {
        private Parser parser = new Parser();
        private XDocument expectedXml = new XDocument();
        private XDocument actualXml = new XDocument();

        [SetUp]
        public void LoadXML_Setup()
        {
            expectedXml = XDocument.Parse(
                "<EAXML></EAXML>");
        }

        [Test]
        public void LoadXML_Test()
        {
            actualXml = parser.LoadXML("C:\\Users\\maxei\\Desktop\\Test\\LoadXML.eaxml");
            Assert.AreEqual(expectedXml.ToString(), actualXml.ToString());
        }
    }
}
