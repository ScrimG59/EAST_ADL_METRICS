using NUnit.Framework;
using EAST_ADL_METRICS.Utils.Categories;
using System.Xml.Linq;
using EAST_ADL_METRICS.Models;

namespace EAST_ADL_METRICS_Test.Utils.Test.Categories.Test
{
    public class ModeTest
    {
        private Mode mode = new Mode();

        [Test]
        public void AllocatedFunctionTypes_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\AllocatedFunctionTypes.eaxml");

            string elementName = "Mode1";

            Metric expectedMetric = new Metric
            {
                Name = "AllocatedFunctionTypes",
                Category = "Size",
                Type = "Mode",
                Nested = false,
                Value = 2
            };

            Metric actualMetric = mode.AllocatedFunctionTypes(xml, elementName);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }
    }
}
