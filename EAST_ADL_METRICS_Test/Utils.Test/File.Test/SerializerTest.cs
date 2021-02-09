using NUnit.Framework;
using System.Xml.Linq;
using EAST_ADL_METRICS.Utils.Parser;
using EAST_ADL_METRICS.Utils.Categories;
using EAST_ADL_METRICS.Models;
using System.IO;

namespace EAST_ADL_METRICS_Test.Utils.Test.File.Test
{
    public class SerializerTest
    {
        private Serializer serializer = new Serializer();
        private Wrapper wrapper = new Wrapper();

        [Test]
        public void WriteResultsIntoFile_Test()
        {
            XDocument xml = XDocument.Load(@"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\Parts.eaxml");

            var expectedOutput = "{\"Name\":\"AFT1\",\"Type\":\"ANALYSIS-FUNCTION-TYPE\"}" +
                "[{\"Name\":\"Parts_fct\",\"Category\":\"Size\",\"Type\":\"FunctionType\",\"Value\":1.0,\"Nested\":false}," +
                "{\"Name\":\"Parts_fct_tc\",\"Category\":\"Size\",\"Type\":\"FunctionType\",\"Value\":1.0,\"Nested\":true}," +
                "{\"Name\":\"NestingLevels_fct\",\"Category\":\"Size\",\"Type\":\"FunctionType\",\"Value\":1.0,\"Nested\":false}," +
                "{\"Name\":\"Ports_fct\",\"Category\":\"Size\",\"Type\":\"FunctionType\",\"Value\":0.0,\"Nested\":false}," +
                "{\"Name\":\"Connectors_fct\",\"Category\":\"Complexity\",\"Type\":\"FunctionType\",\"Value\":0.0,\"Nested\":false}," +
                "{\"Name\":\"Constraints\",\"Category\":\"Size\",\"Type\":\"Constraint\",\"Value\":0.0,\"Nested\":false}," +
                "{\"Name\":\"FunctionPorts\",\"Category\":\"Size\",\"Type\":\"FunctionType\",\"Value\":0.0,\"Nested\":false}," +
                "{\"Name\":\"FunctionFlowPorts\",\"Category\":\"Size\",\"Type\":\"FunctionType\",\"Value\":0.0,\"Nested\":false}," +
                "{\"Name\":\"FunctionPowerPorts\",\"Category\":\"Size\",\"Type\":\"FunctionType\",\"Value\":0.0,\"Nested\":false}," +
                "{\"Name\":\"FunctionClientServerPorts\",\"Category\":\"Size\",\"Type\":\"FunctionType\",\"Value\":0.0,\"Nested\":false}," +
                "{\"Name\":\"Operations\",\"Category\":\"Size\",\"Type\":\"FunctionType\",\"Value\":0.0,\"Nested\":false}," +
                "{\"Name\":\"PortGroups\",\"Category\":\"Size\",\"Type\":\"FunctionType\",\"Value\":0.0,\"Nested\":false}," +
                "{\"Name\":\"PortGroupSize\",\"Category\":\"Size\",\"Type\":\"FunctionType\",\"Value\":0.0,\"Nested\":false}," +
                "{\"Name\":\"VariableElements\",\"Category\":\"Size\",\"Type\":\"Extension\",\"Value\":0.0,\"Nested\":false}," +
                "{\"Name\":\"UseCaseSatisfactionRatio\",\"Category\":\"Complexity\",\"Type\":\"Extension\",\"Value\":0.0,\"Nested\":false}," +
                "{\"Name\":\"FunctionalQualityReqtsRatio\",\"Category\":\"Complexity\",\"Type\":\"Extension\",\"Value\":0.0,\"Nested\":false}," +
                "{\"Name\":\"VVRatio\",\"Category\":\"Complexity\",\"Type\":\"Extension\",\"Value\":0.0,\"Nested\":false}]" +
                "[{\"Name\":\"PortConnectorAllocation\",\"Fulfilled\":false}," +
                "{\"Name\":\"Unverified\",\"Fulfilled\":false}," +
                "{\"Name\":\"ResidualAnomaly\",\"Fulfilled\":false}," +
                "{\"Name\":\"Reference\",\"Fulfilled\":true}," +
                "{\"Name\":\"EventChainPair\",\"Fulfilled\":true}," +
                "{\"Name\":\"ModeAllocation\",\"Fulfilled\":true}]";

            Item item = new Item
            {
                Name = "AFT1",
                Type = "ANALYSIS-FUNCTION-TYPE"
            };

            var metricList = wrapper.calculateMetrics(xml, item);
            var ruleList = wrapper.calcualteRules(xml);

            string path = @"C:\Users\maxei\source\repos\EAST_ADL_METRICS\EAST_ADL_METRICS_Test\TestFiles\Results.txt";
            serializer.WriteResultsIntoFile(metricList, ruleList, item, path);

            string actualOutput;
            using (StreamReader streamReader = new StreamReader(path))
            {
                actualOutput = streamReader.ReadToEnd();
            }

            Assert.AreEqual(expectedOutput.ToString(), actualOutput.ToString());
        }
    }
}
