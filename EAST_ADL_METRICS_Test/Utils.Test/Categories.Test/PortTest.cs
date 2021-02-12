using NUnit.Framework;
using EAST_ADL_METRICS.Utils.Categories;
using System.Xml.Linq;
using EAST_ADL_METRICS.Models;

namespace EAST_ADL_METRICS_Test.Utils.Test.Categories.Test
{
    public class PortTest
    {
        private Port port = new Port();

        [Test]
        public void FunctionPorts_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\FunctionPorts.eaxml");

            string elementName = "DFT";

            Metric expectedMetric = new Metric
            {
                Name = "FunctionPorts",
                Category = "Size",
                Type = "FunctionType",
                Nested = false,
                Value = 4
            };

            Metric actualMetric = port.FunctionPorts(xml, elementName);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }

        [Test]
        public void FunctionFlowPorts_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\FunctionPorts.eaxml");

            string elementName = "DFT";

            Metric expectedMetric = new Metric
            {
                Name = "FunctionFlowPorts",
                Category = "Size",
                Type = "FunctionType",
                Nested = false,
                Value = 1
            };

            Metric actualMetric = port.FunctionFlowPorts(xml, elementName);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }

        [Test]
        public void FunctionPowerPorts_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\FunctionPorts.eaxml");

            string elementName = "DFT";

            Metric expectedMetric = new Metric
            {
                Name = "FunctionPowerPorts",
                Category = "Size",
                Type = "FunctionType",
                Nested = false,
                Value = 2
            };

            Metric actualMetric = port.FunctionPowerPorts(xml, elementName);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }

        [Test]
        public void FunctionClientServerPorts_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\FunctionPorts.eaxml");

            string elementName = "DFT";

            Metric expectedMetric = new Metric
            {
                Name = "FunctionClientServerPorts",
                Category = "Size",
                Type = "FunctionType",
                Nested = false,
                Value = 1
            };

            Metric actualMetric = port.FunctionClientServerPorts(xml, elementName);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }

        [Test]
        public void Operations_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\Operations.eaxml");

            string elementName = "DFT";

            Metric expectedMetric = new Metric
            {
                Name = "Operations",
                Category = "Size",
                Type = "FunctionType",
                Nested = false,
                Value = 6
            };

            Metric actualMetric = port.Operations(xml, elementName);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }

        [Test]
        public void HardwarePorts_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\HardwarePorts.eaxml");

            string elementName = "hda";

            Metric expectedMetric = new Metric
            {
                Name = "HardwarePorts",
                Category = "Size",
                Type = "FunctionType",
                Nested = false,
                Value = 5
            };

            Metric actualMetric = port.HardwarePorts(xml, elementName);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }

        [Test]
        public void PortGroups_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\Portgroups.eaxml");

            string elementName = "DFT";

            Metric expectedMetric = new Metric
            {
                Name = "PortGroups",
                Category = "Size",
                Type = "FunctionType",
                Nested = false,
                Value = 2
            };

            Metric actualMetric = port.PortGroups(xml, elementName);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }

        [Test]
        public void PortGroupSize_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\Portgroups.eaxml");

            string elementName = "DFT";

            Metric expectedMetric = new Metric
            {
                Name = "PortGroupSize",
                Category = "Size",
                Type = "FunctionType",
                Nested = false,
                // average of this is 2
                Value = 2
            };

            Metric actualMetric = port.PortGroupSize(xml, elementName);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }
    }
}
