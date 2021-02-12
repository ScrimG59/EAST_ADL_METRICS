using NUnit.Framework;
using EAST_ADL_METRICS.Utils.Categories;
using System.Xml.Linq;
using EAST_ADL_METRICS.Models;

namespace EAST_ADL_METRICS_Test
{
    public class ArchitectureTest
    {
        private Architecture architecture = new Architecture();

        [Test]
        public void Parts_arch_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\Parts_arch.eaxml");

            string elementName = "Architecture1";

            Metric expectedMetric = new Metric
            {
                Name = "Parts_arch",
                Category = "Size",
                Type = "Architecture",
                Nested = false,
                Value = 3
            };

            Metric actualMetric = architecture.Parts_arch(xml, elementName);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }

        [Test]
        public void Parts_arch_tc_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\Parts_arch_tc.eaxml");

            string elementName = "Architecture1";

            Metric expectedMetric = new Metric
            {
                Name = "Parts_arch_tc",
                Category = "Size",
                Type = "Architecture",
                Nested = true,
                Value = 2
            };

            Metric actualMetric = architecture.Parts_arch_tc(xml, elementName);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }

        [Test]
        public void NestingLevels_arch_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\Parts_arch_tc.eaxml");

            string elementName = "Architecture1";

            Metric expectedMetric = new Metric
            {
                Name = "NestingLevels_arch",
                Category = "Size",
                Type = "Architecture",
                Nested = false,
                Value = 2
            };

            Metric actualMetric = architecture.NestingLevels_arch(xml, elementName);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }

        [Test]
        public void Port_arch_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\Ports_arch.eaxml");

            string elementName = "Architecture1";

            Metric expectedMetric = new Metric
            {
                Name = "Ports_arch",
                Category = "Size",
                Type = "Architecture",
                Nested = false,
                Value = 1
            };

            Metric actualMetric = architecture.Ports_arch(xml, elementName);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }

        [Test]
        public void Connectors_arch_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\Connectors_arch.eaxml");

            string elementName = "Architecture1";

            Metric expectedMetric = new Metric
            {
                Name = "Connectors_arch",
                Category = "Complexity",
                Type = "Architecture",
                Nested = false,
                Value = 1
            };

            Metric actualMetric = architecture.Connectors_arch(xml, elementName);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }

        [Test]
        public void FunctionNodeAllocation_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\FunctionNodeAllocation.eaxml");

            string elementName = "fda";

            Metric expectedMetric = new Metric
            {
                Name = "FunctionNodeAllocation",
                Category = "Complexity",
                Type = "Architecture",
                Nested = false,
                Value = 2
            };

            Metric actualMetric = architecture.FunctionNodeAllocation(xml, elementName);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }
    }
}