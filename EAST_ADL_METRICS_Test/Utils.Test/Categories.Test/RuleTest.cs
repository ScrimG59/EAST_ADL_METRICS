using NUnit.Framework;
using EAST_ADL_METRICS.Utils.Categories;
using System.Xml.Linq;
using EAST_ADL_METRICS.Models;

namespace EAST_ADL_METRICS_Test.Utils.Test.Categories.Test
{
    public class RuleTest
    {
        private Rules rules = new Rules();

        [Test]
        public void PortConnectorAllocation_True_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\PortConnectorAllocation_t.eaxml");

            Rule expectedRule = new Rule
            {
                Name = "PortConnectorAllocation",
                Fulfilled = true
            };

            Rule actualRule = rules.PortConnectorAllocation(xml);
            Assert.AreEqual(expectedRule.Fulfilled, actualRule.Fulfilled);
        }

        [Test]
        public void PortConnectorAllocation_False_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\PortConnectorAllocation_f.eaxml");

            Rule expectedRule = new Rule
            {
                Name = "PortConnectorAllocation",
                Fulfilled = false
            };

            Rule actualRule = rules.PortConnectorAllocation(xml);
            Assert.AreEqual(expectedRule.Fulfilled, actualRule.Fulfilled);
        }

        [Test]
        public void Unverified_True_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\Unverified_t.eaxml");

            Rule expectedRule = new Rule
            {
                Name = "Unverified",
                Fulfilled = true
            };

            Rule actualRule = rules.Unverified(xml);
            Assert.AreEqual(expectedRule.Fulfilled, actualRule.Fulfilled);
        }

        [Test]
        public void Unverified_False_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\Unverified_f.eaxml");

            Rule expectedRule = new Rule
            {
                Name = "Unverified",
                Fulfilled = false
            };

            Rule actualRule = rules.Unverified(xml);
            Assert.AreEqual(expectedRule.Fulfilled, actualRule.Fulfilled);
        }

        [Test]
        public void ResidualAnomaly_True_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\ResidualAnomaly_t.eaxml");

            Rule expectedRule = new Rule
            {
                Name = "ResidualAnomaly",
                Fulfilled = true
            };

            Rule actualRule = rules.ResidualAnomaly(xml);
            Assert.AreEqual(expectedRule.Fulfilled, actualRule.Fulfilled);
        }

        [Test]
        public void ResidualAnomaly_False_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\ResidualAnomaly_f.eaxml");

            Rule expectedRule = new Rule
            {
                Name = "ResidualAnomaly",
                Fulfilled = false
            };

            Rule actualRule = rules.ResidualAnomaly(xml);
            Assert.AreEqual(expectedRule.Fulfilled, actualRule.Fulfilled);
        }

        [Test]
        public void Reference_True_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\Reference_t.eaxml");

            Rule expectedRule = new Rule
            {
                Name = "Reference",
                Fulfilled = true
            };

            Rule actualRule = rules.Reference(xml);
            Assert.AreEqual(expectedRule.Fulfilled, actualRule.Fulfilled);
        }

        [Test]
        public void Reference_False_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\Reference_f.eaxml");

            Rule expectedRule = new Rule
            {
                Name = "Reference",
                Fulfilled = false
            };

            Rule actualRule = rules.Reference(xml);
            Assert.AreEqual(expectedRule.Fulfilled, actualRule.Fulfilled);
        }

        [Test]
        public void EvenChainPair_True_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\EventChainPair_t.eaxml");

            Rule expectedRule = new Rule
            {
                Name = "EventChainPair",
                Fulfilled = true
            };

            Rule actualRule = rules.EventChainPair(xml);
            Assert.AreEqual(expectedRule.Fulfilled, actualRule.Fulfilled);
        }

        [Test]
        public void EvenChainPair_False_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\EventChainPair_f.eaxml");

            Rule expectedRule = new Rule
            {
                Name = "EventChainPair",
                Fulfilled = false
            };

            Rule actualRule = rules.EventChainPair(xml);
            Assert.AreEqual(expectedRule.Fulfilled, actualRule.Fulfilled);
        }

        [Test]
        public void ModeAllocation_True_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\ModeAllocation_t.eaxml");

            Rule expectedRule = new Rule
            {
                Name = "ModeAllocation",
                Fulfilled = true
            };

            Rule actualRule = rules.ModeAllocation(xml);
            Assert.AreEqual(expectedRule.Fulfilled, actualRule.Fulfilled);
        }

        [Test]
        public void ModeAllocation_False_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\ModeAllocation_f.eaxml");

            Rule expectedRule = new Rule
            {
                Name = "ModeAllocation",
                Fulfilled = false
            };

            Rule actualRule = rules.ModeAllocation(xml);
            Assert.AreEqual(expectedRule.Fulfilled, actualRule.Fulfilled);
        }
    }
}
