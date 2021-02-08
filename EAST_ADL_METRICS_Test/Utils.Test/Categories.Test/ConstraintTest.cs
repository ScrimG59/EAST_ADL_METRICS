using NUnit.Framework;
using EAST_ADL_METRICS.Utils.Categories;
using System.Xml.Linq;
using EAST_ADL_METRICS.Models;

namespace EAST_ADL_METRICS_Test.Utils.Test.Categories.Test
{
    public class ConstraintTest
    {
        private Constraint constraint = new Constraint();

        [Test]
        public void Constraints_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\Desktop\Test\Constraints.eaxml");

            string elementName = "FunctionType2";

            Metric expectedMetric = new Metric
            {
                Name = "Constraints",
                Category = "Size",
                Type = "FUNCTION-TYPE",
                Nested = false,
                Value = 1
            };

            Metric actualMetric = constraint.Constraints(xml, elementName);
            Assert.AreEqual(expectedMetric.Value, actualMetric.Value);
        }
    }
}
