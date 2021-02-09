using EAST_ADL_METRICS.Models;
using EAST_ADL_METRICS.Utils.Searcher;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace EAST_ADL_METRICS.Utils.Categories
{
    public class Architecture
    {
        private Global globalSearcher = new Global();
        private Local localSearcher = new Local();
        private Helper helper = new Helper();

        private Metric parts = new Metric
        {
            Name = "Parts_arch",
            Category = "Size",
            Type = "Architecture",
            Nested = false
        };

        private Metric parts_tc = new Metric
        {
            Name = "Parts_arch_tc",
            Category = "Size",
            Type = "Architecture",
            Nested = true
        };

        private Metric nestingLevels = new Metric
        {
            Name = "NestingLevels_arch",
            Category = "Size",
            Type = "Architecture",
            Nested = false
        };

        private Metric ports = new Metric
        {
            Name = "Ports_arch",
            Category = "Size",
            Type = "Architecture",
            Nested = false
        };

        private Metric connectors = new Metric
        {
            Name = "Connectors_arch",
            Category = "Complexity",
            Type = "Architecture",
            Nested = false
        };

        private Metric functionNodeAllocation = new Metric
        {
            Name = "FunctionNodeAllocation",
            Category = "Complexity",
            Type = "Architecture",
            Nested = false
        };

        /// <summary>
        /// return the parts metric
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="elementName"></param>
        /// <returns></returns>
        public Metric Parts_arch(XDocument xml, string elementName)
        {
            XElement architecture = helper.navigateToNode(xml, elementName);
            // gets the function type of this architecture
            XElement functionType = helper.getFunctionTypeFromArchitecture(xml, architecture);

            string shortName = helper.getShortName(functionType);

            int count = localSearcher.nestedChildElementList(xml, shortName,
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
        public Metric Parts_arch_tc(XDocument xml, string elementName)
        {
            XElement architecture = helper.navigateToNode(xml, elementName);
            // gets the function type of this architecture
            XElement functionType = helper.getFunctionTypeFromArchitecture(xml, architecture);

            string shortName = helper.getShortName(functionType);

            int count = localSearcher.recursiveChildElementList(xml, shortName);

            parts_tc.Value = count;

            return parts_tc;
        }

        /// <summary>
        /// returns the nesting levels of the parts
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public Metric NestingLevels_arch(XDocument xml, string elementName)
        {
            XElement architecture = helper.navigateToNode(xml, elementName);
            // gets the function type of this architecture
            XElement functionType = helper.getFunctionTypeFromArchitecture(xml, architecture);

            string shortName = helper.getShortName(functionType);

            var childElementList = localSearcher.recursiveChildElementList(xml, shortName);
            nestingLevels.Value = localSearcher.getNestingLevel();

            return nestingLevels;
        }

        /// <summary>
        /// returns the ports metric
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public Metric Ports_arch(XDocument xml, string elementName)
        {
            XElement architecture = helper.navigateToNode(xml, elementName);
            // gets the function type of this architecture
            XElement functionType = helper.getFunctionTypeFromArchitecture(xml, architecture);

            string shortName = helper.getShortName(functionType);

            int count = localSearcher.nestedChildElementList(xml, shortName,
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
        public Metric Connectors_arch(XDocument xml, string elementName)
        { 
            XElement architecture = helper.navigateToNode(xml, elementName);
            // gets the function type of this architecture
            XElement functionType = helper.getFunctionTypeFromArchitecture(xml, architecture);

            string shortName = helper.getShortName(functionType);

            int count = localSearcher.nestedChildElementList(xml, shortName,
                                                            "FUNCTION-CONNECTOR",
                                                            "HARDWARE-CONNECTOR");

            connectors.Value = count;

            return connectors;
        }

        /// <summary>
        /// returns the FunctionNodeAllocation metric
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="elementName"></param>
        /// <returns></returns>
        public Metric FunctionNodeAllocation(XDocument xml, string elementName)
        {
            Dictionary<string, int> hardwareNodeCount = new Dictionary<string, int>();

            // gets the architecture
            XElement architecture = helper.navigateToNode(xml, elementName);

            var designLevels = globalSearcher.parentElementList(xml, "DESIGN-LEVEL");
            
            // for each design level in the model it picks out both architectures and 
            // looks if one of them is equal to the given architecture
            foreach(var designLevel in designLevels)
            { 
                XElement functionalDesignArchitecture = designLevel.Descendants()
                                                              .Where(a => a.Name == "FUNCTIONAL-DESIGN-ARCHITECTURE")
                                                              .FirstOrDefault();

                XElement hardwareDesignArchitecture = designLevel.Descendants()
                                                              .Where(a => a.Name == "HARDWARE-DESIGN-ARCHITECTURE")
                                                              .FirstOrDefault();

                // if one of the two architectures are equal, we are in the right design level
                if(functionalDesignArchitecture == architecture || hardwareDesignArchitecture == architecture)
                {
                    // if this design level has allocations
                    if(designLevel.Descendants().Where(a => a.Name == "ALLOCATIONS").FirstOrDefault().HasElements)
                    {
                        // get all allocations within the design level
                        var allocations = designLevel.Descendants().Where(a => a.Name == "FUNCTION-ALLOCATION");

                        foreach(var allocation in allocations)
                        {
                            string reference = helper.getTypeReference(allocation);

                            // if the hardware node has already been seen just increment the value by 1
                            if (hardwareNodeCount.ContainsKey(reference))
                            { 
                                int currentValue = hardwareNodeCount[reference];
                                hardwareNodeCount[reference] = ++currentValue;
                            }
                            // otherwise add the new hardware node into the dictionary with the initial value of 1
                            else
                            {
                                hardwareNodeCount.Add(reference, 1);
                            }
                        }
                    }
                    // an architecture can only be in one level, so if the design level got found just break out of the loop
                    break;
                }
            }
            if (hardwareNodeCount.Count != 0)
            {
                functionNodeAllocation.Value = hardwareNodeCount.Values.Average();
            }
            else
            {
                functionNodeAllocation.Value = 0;
            }

            return functionNodeAllocation;
        }

    }
}
