using EAST_ADL_METRICS.Models;
using System;
using System.Linq;
using System.Xml.Linq;
using EAST_ADL_METRICS.Utils.Searcher;

namespace EAST_ADL_METRICS.Utils.Categories
{
    public class FunctionType
    {
        private Global globalSearcher = new Global();
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
            Category = "Size",
            Type = "FunctionType",
            Nested = false
        };

        /// <summary>
        /// returns the parts metric
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public Metric Parts_fct(XDocument xml, bool mode, string elementName = null)
        {
            // mode checks if the user wants metrics of the whole xml-file or of only one element
            // mode = true (global), mode = false (local)
            if (mode)
            {
                var parentList = globalSearcher.parentElementList(xml,
                                               "DESIGN-FUNCTION-TYPE",
                                               "ANALYSIS-FUNCTION-TYPE",
                                               "HARDWARE-FUNCTION-TYPE",
                                               "BASIC-SOFTWARE-FUNCTION-TYPE");

                var childElementList = globalSearcher.nestedChildElementList(parentList,
                                                     "DESIGN-FUNCTION-PROTOTYPE",
                                                     "ANALYSIS-FUNCTION-PROTOTYPE",
                                                     "HARDWARE-COMPONENT-PROTOTYPE",
                                                     "BASIC-SOFTWARE-FUNCTION-PROTOTYPE");

                parts.MaxValue = childElementList.Values.Max();
                parts.MinValue = childElementList.Values.Min();
                parts.AvgValue = childElementList.Values.Average();
            }
            else
            {
                var childElementList = localSearcher.nestedChildElementList(xml, elementName,
                                                    "DESIGN-FUNCTION-PROTOTYPE",
                                                    "ANALYSIS-FUNCTION-PROTOTYPE",
                                                    "HARDWARE-COMPONENT-PROTOTYPE",
                                                    "BASIC-SOFTWARE-FUNCTION-PROTOTYPE");

                parts.MaxValue = childElementList.Values.Max();
                parts.MinValue = childElementList.Values.Min();
                parts.AvgValue = childElementList.Values.Average();
            }
            return parts;
        }

        /// <summary>
        /// returns the nested parts metric
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public Metric Parts_fct_tc(XDocument xml, bool mode, string elementName = null)
        {
            // mode checks if the user wants metrics of the whole xml-file or of only one element
            // mode = true (global), mode = false (local)
            if (mode)
            {
                var parentList = globalSearcher.parentElementList(xml,
                                               "DESIGN-FUNCTION-TYPE",
                                               "ANALYSIS-FUNCTION-TYPE",
                                               "HARDWARE-FUNCTION-TYPE",
                                               "BASIC-SOFTWARE-FUNCTION-TYPE");

                var childElementList = globalSearcher.recursiveChildElementList(xml, parentList);

                parts_tc.MaxValue = childElementList.Values.Max();
                parts_tc.MinValue = childElementList.Values.Min();
                parts_tc.AvgValue = childElementList.Values.Average();
            }
            else
            {
                var childElementList = localSearcher.recursiveChildElementList(xml, elementName);

                parts_tc.MaxValue = childElementList.Values.Max();
                parts_tc.MinValue = childElementList.Values.Min();
                parts_tc.AvgValue = childElementList.Values.Average();
            }

            return parts_tc;
        }

        /// <summary>
        /// returns the nesting levels of the parts
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public Metric NestingLevels_fct(XDocument xml, bool mode, string elementName = null)
        {
            // mode checks if the user wants metrics of the whole xml-file or of only one element
            // mode = true (global), mode = false (local)
            if (mode)
            {
                var parentList = globalSearcher.parentElementList(xml,
                                               "DESIGN-FUNCTION-TYPE",
                                               "ANALYSIS-FUNCTION-TYPE",
                                               "HARDWARE-FUNCTION-TYPE",
                                               "BASIC-SOFTWARE-FUNCTION-TYPE");

                var childElementList = globalSearcher.recursiveChildElementList(xml, parentList);

            }
            else
            {
                var childElementList = localSearcher.recursiveChildElementList(xml, elementName);
                nestingLevels.AvgValue = localSearcher.getNestingLevel();
            }

            return nestingLevels;
        }

        /// <summary>
        /// returns the ports metric
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public Metric Ports_fct(XDocument xml, bool mode, string elementName = null)
        {
            if (mode)
            {
                var parentList = globalSearcher.parentElementList(xml,
                                               "DESIGN-FUNCTION-TYPE",
                                               "ANALYSIS-FUNCTION-TYPE",
                                               "HARDWARE-FUNCTION-TYPE",
                                               "HARDWARE-COMPONENT-TYPE",
                                               "BASIC-SOFTWARE-FUNCTION-TYPE");

                var childElementList = globalSearcher.nestedChildElementList(parentList,
                                                     "FUNCTION-FLOW-PORT",
                                                     "FUNCTION-CLIENT-SERVER-PORT",
                                                     "FUNCTION-POWER-PORT",
                                                     "IO-HARDWARE-PIN",
                                                     "COMMUNICATION-HARDWARE-PIN");

                ports.MaxValue = childElementList.Values.Max();
                ports.MinValue = childElementList.Values.Min();
                ports.AvgValue = childElementList.Values.Average();
            }
            else
            {
                var childElementList = localSearcher.nestedChildElementList(xml, elementName,
                                                    "FUNCTION-FLOW-PORT",
                                                    "FUNCTION-CLIENT-SERVER-PORT",
                                                    "FUNCTION-POWER-PORT",
                                                    "IO-HARDWARE-PIN",
                                                    "COMMUNICATION-HARDWARE-PIN");

                ports.MaxValue = childElementList.Values.Max();
                ports.MinValue = childElementList.Values.Min();
                ports.AvgValue = childElementList.Values.Average();
            }
            return ports;
        }

        /// <summary>
        /// returns the connectors metric
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public Metric Connectors_fct(XDocument xml, bool mode, string elementName = null)
        {
            if (mode)
            {
                var parentList = globalSearcher.parentElementList(xml,
                                               "DESIGN-FUNCTION-TYPE",
                                               "ANALYSIS-FUNCTION-TYPE",
                                               "HARDWARE-FUNCTION-TYPE",
                                               "BASIC-SOFTWARE-FUNCTION-TYPE");

                var childElementList = globalSearcher.nestedChildElementList(parentList,
                                                     "FUNCTION-CONNECTOR");

                connectors.MaxValue = childElementList.Values.Max();
                connectors.MinValue = childElementList.Values.Min();
                connectors.AvgValue = childElementList.Values.Average();
            }
            else
            {
                var childElementList = localSearcher.nestedChildElementList(xml, elementName,
                                                    "FUNCTION-CONNECTOR");

                connectors.MaxValue = childElementList.Values.Max();
                connectors.MinValue = childElementList.Values.Min();
                connectors.AvgValue = childElementList.Values.Average();
            }

            return connectors;
        }
    }
}
