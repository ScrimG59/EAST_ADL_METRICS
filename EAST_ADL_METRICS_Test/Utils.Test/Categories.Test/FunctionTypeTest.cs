using NUnit.Framework;
using EAST_ADL_METRICS.Utils.Categories;
using System.Xml.Linq;
using EAST_ADL_METRICS.Models;

namespace EAST_ADL_METRICS_Test.Utils.Test.Categories.Test
{
    public class FunctionTypeTest
    {
        private FunctionType functionType = new FunctionType();

        [Test]
        public void Parts_fct_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\Parts.eaxml");

            string elementName = "AFT1";

            Metric expectedMetric = new Metric
            {
                Name = "Parts_fct",
                Category = "Size",
                Type = "FunctionType",
                Nested = false,
                Value = 1
            };

            Metric actualMetric = functionType.Parts_fct(xml, elementName);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }

        [Test]
        public void Parts_fct_tc_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\Parts_tc.eaxml");

            string elementName = "AFT1";

            Metric expectedMetric = new Metric
            {
                Name = "Parts_fct_tc",
                Category = "Size",
                Type = "FunctionType",
                Nested = false,
                Value = 2
            };

            Metric actualMetric = functionType.Parts_fct_tc(xml, elementName);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }

        [Test]
        public void NestingLevels_fct_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\Parts_tc.eaxml");

            string elementName = "AFT1";

            Metric expectedMetric = new Metric
            {
                Name = "NestingLevels_fct_tc",
                Category = "Size",
                Type = "FunctionType",
                Nested = true,
                Value = 2
            };

            Metric actualMetric = functionType.NestingLevels_fct(xml, elementName);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }

        [Test]
        public void Ports_fct_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\Ports.eaxml");

            string elementName = "AFT1";

            Metric expectedMetric = new Metric
            {
                Name = "Ports_fct",
                Category = "Size",
                Type = "FunctionType",
                Nested = false,
                Value = 2
            };

            Metric actualMetric = functionType.Ports_fct(xml, elementName);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }

        [Test]
        public void Connectors_fct_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\Connectors.eaxml");

            string elementName = "DFT1";

            Metric expectedMetric = new Metric
            {
                Name = "Connectors_fct",
                Category = "Complexity",
                Type = "FunctionType",
                Nested = false,
                Value = 1
            };

            Metric actualMetric = functionType.Connectors_fct(xml, elementName);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }
    }
}
