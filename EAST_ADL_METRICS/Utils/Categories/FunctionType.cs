using EAST_ADL_METRICS.Models;
using System.Xml.Linq;
using EAST_ADL_METRICS.Utils.Searcher;

namespace EAST_ADL_METRICS.Utils.Categories
{
    public class FunctionType
    {
        private Local localSearcher = new Local();

        private Metric parts = new Metric
        {
            Name = "Parts_fct",
            Category = "Size",
            Type = "FunctionType",
            Nested = false
        };

        private Metric parts_tc = new Metric
        {
            Name = "Parts_fct_tc",
            Category = "Size",
            Type = "FunctionType",
            Nested = true
        };

        private Metric nestingLevels = new Metric
        {
            Name = "NestingLevels_fct",
            Category = "Size",
            Type = "FunctionType",
            Nested = false
        };

        private Metric ports = new Metric
        {
            Name = "Ports_fct",
            Category = "Size",
            Type = "FunctionType",
            Nested = false
        };

        private Metric connectors = new Metric
        {
            Name = "Connectors_fct",
            Category = "Complexity",
            Type = "FunctionType",
            Nested = false
        };

        /// <summary>
        /// returns the parts metric
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public Metric Parts_fct(XDocument xml, string elementName)
        {
            int count = localSearcher.nestedChildElementList(xml, elementName,
                                                 "DESIGN-FUNCTION-PROTOTYPE",
                                                 "ANALYSIS-FUNCTION-PROTOTYPE",
                                                 "HARDWARE-COMPONENT-PROTOTYPE",
                                                 "BASIC-SOFTWARE-FUNCTION-PROTOTYPE");

            parts.Value = count;
            
            return parts;
        }

        /// <summary>
        /// returns the nested parts metric
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public Metric Parts_fct_tc(XDocument xml, string elementName)
        {
            int count = localSearcher.recursiveChildElementList(xml, elementName);

            parts_tc.Value = count;

            return parts_tc;
        }

        /// <summary>
        /// returns the nesting levels of the parts
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public Metric NestingLevels_fct(XDocument xml, string elementName)
        {
            var childElementList = localSearcher.recursiveChildElementList(xml, elementName);
            nestingLevels.Value = localSearcher.getNestingLevel();

            return nestingLevels;
        }

        /// <summary>
        /// returns the ports metric
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public Metric Ports_fct(XDocument xml,  string elementName)
        {
            int count = localSearcher.nestedChildElementList(xml, elementName,
                                                "FUNCTION-FLOW-PORT",
                                                "FUNCTION-CLIENT-SERVER-PORT",
                                                "FUNCTION-POWER-PORT",
                                                "IO-HARDWARE-PIN",
                                                "COMMUNICATION-HARDWARE-PIN");

            ports.Value = count;

            return ports;
        }

        /// <summary>
        /// returns the connectors metric
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public Metric Connectors_fct(XDocument xml, string elementName)
        {
            int count = localSearcher.nestedChildElementList(xml, elementName,
                                                            "FUNCTION-CONNECTOR", 
                                                            "HARDWARE-CONNECTOR");

            connectors.Value = count;

            return connectors;
        }
    }
}
