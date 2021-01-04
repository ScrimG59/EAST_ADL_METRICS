using EAST_ADL_METRICS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EAST_ADL_METRICS.Utils.Categories
{
    public class FunctionType
    {
        private Searcher.Searcher searcher = new Searcher.Searcher();
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
            Category = "Size",
            Type = "FunctionType",
            Nested = false
        };

        /// <summary>
        /// returns the parts metric
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public Metric Parts_fct(XDocument xml)
        {
            var parentList = searcher.parentElementList(xml, 
                                    "DESIGN-FUNCTION-TYPE",
                                    "ANALYSIS-FUNCTION-TYPE",
                                    "HARDWARE-FUNCTION-TYPE",
                                    "BASIC-SOFTWARE-FUNCTION-TYPE");
            var childElementList = searcher
                                    .nestedChildElementList(xml, parentList,
                                    "DESIGN-FUNCTION-PROTOTYPE",
                                    "ANALYSIS-FUNCTION-PROTOTYPE",
                                    "HARDWARE-COMPONENT-PROTOTYPE",
                                    "BASIC-SOFTWARE-FUNCTION-PROTOTYPE");

            parts.MaxValue = childElementList.Values.Max();
            parts.MinValue = childElementList.Values.Min();
            parts.AvgValue = childElementList.Values.Average();

            return parts;
        }

        /// <summary>
        /// returns the nested parts metric
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public Metric Parts_fct_tc(XDocument xml)
        {
            return parts_tc;
        }

        /// <summary>
        /// returns the nesting levels of the parts
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public Metric NestingLevels_fct(XDocument xml)
        {
            return NestingLevels;
        }

        /// <summary>
        /// returns the ports metric
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public Metric Ports_fct(XDocument xml)
        {
            var parentList = searcher.parentElementList(xml,
                                    "DESIGN-FUNCTION-TYPE",
                                    "ANALYSIS-FUNCTION-TYPE",
                                    "HARDWARE-FUNCTION-TYPE",
                                    "BASIC-SOFTWARE-FUNCTION-TYPE");
            var childElementList = searcher
                                    .nestedChildElementList(xml, parentList,
                                    "FUNCTION-FLOW-PORT",
                                    "FUNCTION-CLIENT-SERVER-PORT",
                                    "FUNCTION-POWER-PORT");

            ports.MaxValue = childElementList.Values.Max();
            ports.MinValue = childElementList.Values.Min();
            ports.AvgValue = childElementList.Values.Average();

            return ports;

        }

        /// <summary>
        /// returns the connectors metric
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public Metric Connectors_fct(XDocument xml)
        {
            var parentList = searcher.parentElementList(xml,
                                    "DESIGN-FUNCTION-TYPE",
                                    "ANALYSIS-FUNCTION-TYPE",
                                    "HARDWARE-FUNCTION-TYPE",
                                    "BASIC-SOFTWARE-FUNCTION-TYPE");
            var childElementList = searcher
                                    .nestedChildElementList(xml, parentList,
                                    "FUNCTION-CONNECTOR");

            connectors.MaxValue = childElementList.Values.Max();
            connectors.MinValue = childElementList.Values.Min();
            connectors.AvgValue = childElementList.Values.Average();

            return connectors;
        }
    }
}
