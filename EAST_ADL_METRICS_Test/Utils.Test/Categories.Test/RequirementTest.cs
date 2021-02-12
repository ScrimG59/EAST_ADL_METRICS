using NUnit.Framework;
using EAST_ADL_METRICS.Utils.Categories;
using System.Xml.Linq;
using EAST_ADL_METRICS.Models;

namespace EAST_ADL_METRICS_Test.Utils.Test.Categories.Test
{
    public class RequirementTest
    {
        private Requirement requirement = new Requirement();

        [Test]
        public void SubReqts_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\SubReqts.eaxml");

            string elementName = "QReqt1";

            Metric expectedMetric = new Metric
            {
                Name = "SubReqts",
                Category = "Size",
                Type = "Requirement",
                Nested = true,
                Value = 3
            };

            Metric actualMetric = requirement.SubReqts(xml, elementName);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }

        [Test]
        public void NestingLevel_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\SubReqts.eaxml");

            string elementName = "Reqt3";

            Metric expectedMetric = new Metric
            {
                Name = "NestingLevel",
                Category = "Size",
                Type = "Requirement",
                Nested = true,
                Value = 2
            };

            Metric actualMetric = requirement.NestingLevel(xml, elementName);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }

        [Test]
        public void Satisfiers_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\Satisfiers.eaxml");

            string elementName = "Requirement";

            Metric expectedMetric = new Metric
            {
                Name = "Satisfiers",
                Category = "Size",
                Type = "Requirement",
                Nested = false,
                Value = 2
            };

            Metric actualMetric = requirement.Satisfiers(xml, elementName);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }

        [Test]
        public void Verifiers_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\Verifiers.eaxml");

            string elementName = "Requirement";

            Metric expectedMetric = new Metric
            {
                Name = "Verifiers",
                Category = "Size",
                Type = "Requirement",
                Nested = false,
                Value = 4
            };

            Metric actualMetric = requirement.Verifiers(xml, elementName);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }

        [Test]
        public void Derivatives_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\Derivatives.eaxml");

            string elementName = "QReqt1";

            Metric expectedMetric = new Metric
            {
                Name = "Derivatives",
                Category = "Size",
                Type = "Requirement",
                Nested = false,
                Value = 5
            };

            Metric actualMetric = requirement.Derivatives(xml, elementName);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }
    }
}
