using NUnit.Framework;
using EAST_ADL_METRICS.Utils.Categories;
using System.Xml.Linq;
using EAST_ADL_METRICS.Models;

namespace EAST_ADL_METRICS_Test.Utils.Test.Categories.Test
{
    public class PackageTest
    {
        private Package package = new Package();

        [Test]
        public void Functions_pckg()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\Functions_pckg.eaxml");

            string elementName = "Package1";

            Metric expectedMetric = new Metric
            {
                Name = "Functions_pckg",
                Category = "Size",
                Type = "Package",
                Nested = false,
                Value = 2
            };

            Metric actualMetric = package.Functions_pckg(xml, elementName);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }

        [Test]
        public void Function_pckg_tc()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\Functions_pckg_tc.eaxml");

            string elementName = "EARoot";

            Metric expectedMetric = new Metric
            {
                Name = "Functions_pckg_tc",
                Category = "Size",
                Type = "Package",
                Nested = true,
                Value = 2
            };

            Metric actualMetric = package.Functions_pckg_tc(xml, elementName);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }

        [Test]
        public void Reqts_pckg()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\Reqts_pckg.eaxml");

            string elementName = "Package1";

            Metric expectedMetric = new Metric
            {
                Name = "Reqts_pckg",
                Category = "Size",
                Type = "Package",
                Nested = false,
                Value = 3
            };

            Metric actualMetric = package.Reqts_pckg(xml, elementName);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }

        [Test]
        public void Reqts_pckg_tc()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\Reqts_pckg_tc.eaxml");

            string elementName = "EARoot";

            Metric expectedMetric = new Metric
            {
                Name = "Reqts_pckg_tc",
                Category = "Size",
                Type = "Package",
                Nested = true,
                Value = 2
            };

            Metric actualMetric = package.Reqts_pckg_tc(xml, elementName);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }
    }
}
