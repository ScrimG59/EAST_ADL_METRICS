using NUnit.Framework;
using EAST_ADL_METRICS.Utils.Categories;
using System.Xml.Linq;
using EAST_ADL_METRICS.Models;

namespace EAST_ADL_METRICS_Test.Utils.Test.Categories.Test
{
    public class ExtensionTest
    {
        private Extension extension = new Extension();

        [Test]
        public void VariableElements_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\Desktop\Test\VariableElements.eaxml");

            Metric expectedMetric = new Metric
            {
                Name = "VariableElements",
                Category = "Size",
                Type = "Extension",
                Nested = false,
                Value = 2
            };

            Metric actualMetric = extension.VariableElements(xml);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }

        [Test]
        public void FunctionalQualityReqtsRatio_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\Desktop\Test\FunctionalQualityReqtsRatio.eaxml");

            Metric expectedMetric = new Metric
            {
                Name = "FunctionalQualityReqtsRatio",
                Category = "Size",
                Type = "Extension",
                Nested = false,
                Value = 0.5
            };

            Metric actualMetric = extension.FunctionalQualityReqtsRatio(xml);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }

        [Test]
        public void UseCaseSatisfactionRatio_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\Desktop\Test\UseCaseSatisfactionRatio.eaxml");

            Metric expectedMetric = new Metric
            {
                Name = "UseCaseSatisfacionRatio",
                Category = "Complexity",
                Type = "Extension",
                Nested = false,
                Value = 0.25
            };

            Metric actualMetric = extension.UseCaseSatisfactionRatio(xml);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }

        [Test]
        public void VVRatio_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\Desktop\Test\VVRatio.eaxml");

            Metric expectedMetric = new Metric
            {
                Name = "VVRatio",
                Category = "Complexity",
                Type = "Extension",
                Nested = false,
                Value = 0.5
            };

            Metric actualMetric = extension.VVRatio(xml);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }
    }
}
