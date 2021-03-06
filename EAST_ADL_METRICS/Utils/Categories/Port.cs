﻿using EAST_ADL_METRICS.Models;
using EAST_ADL_METRICS.Utils.Searcher;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace EAST_ADL_METRICS.Utils.Categories
{
    public class Port
    {
        private Local localSearcher = new Local();
        private Helper helper = new Helper();

        private Metric functionPorts = new Metric
        {
            Name = "FunctionPorts",
            Category = "Size",
            Type = "FunctionType",
            Nested = false
        };

        private Metric functionFlowPorts = new Metric
        {
            Name = "FunctionFlowPorts",
            Category = "Size",
            Type = "FunctionType",
            Nested = false
        };

        private Metric functionPowerPorts = new Metric
        {
            Name = "FunctionPowerPorts",
            Category = "Size",
            Type = "FunctionType",
            Nested = false
        };

        private Metric functionClientServerPorts = new Metric
        {
            Name = "FunctionClientServerPorts",
            Category = "Size",
            Type = "FunctionType",
            Nested = false
        };

        private Metric operations = new Metric
        {
            Name = "Operations",
            Category = "Size",
            Type = "FunctionType",
            Nested = false
        };

        private Metric hardwarePorts = new Metric
        {
            Name = "HardwarePorts",
            Category = "Size",
            Type = "FunctionType",
            Nested = false
        };

        private Metric portGroups = new Metric
        {
            Name = "PortGroups",
            Category = "Size",
            Type = "FunctionType",
            Nested = false
        };

        private Metric portGroupSize = new Metric
        {
            Name = "PortGroupSize",
            Category = "Size",
            Type = "FunctionType",
            Nested = false
        };

        public Metric FunctionPorts(XDocument xml, string elementName)
        {
            XElement possibleArchitecture = helper.navigateToNode(xml, elementName);

            if (possibleArchitecture.Name.ToString().Contains("ARCHITECTURE"))
            {
                // gets the function type of this architecture
                XElement possibleFunctionType = helper.getFunctionTypeFromArchitecture(xml, possibleArchitecture);

                string shortName = helper.getShortName(possibleFunctionType);

                int count = localSearcher.nestedChildElementList(xml, shortName,
                                                                "FUNCTION-FLOW-PORT",
                                                                "FUNCTION-CLIENT-SERVER-PORT",
                                                                "FUNCTION-POWER-PORT");

                functionPorts.Value = count;
            }    
            else
            {
                int count = localSearcher.nestedChildElementList(xml, elementName,
                                                                "FUNCTION-FLOW-PORT",
                                                                "FUNCTION-CLIENT-SERVER-PORT",
                                                                "FUNCTION-POWER-PORT");

                functionPorts.Value = count;
            }
            

            return functionPorts;
        }

        public Metric FunctionFlowPorts(XDocument xml, string elementName)
        {
            XElement possibleArchitecture = helper.navigateToNode(xml, elementName);

            if (possibleArchitecture.Name.ToString().Contains("ARCHITECTURE"))
            {
                // gets the function type of this architecture
                XElement possibleFunctionType = helper.getFunctionTypeFromArchitecture(xml, possibleArchitecture);

                string shortName = helper.getShortName(possibleFunctionType);

                int count = localSearcher.nestedChildElementList(xml, shortName, "FUNCTION-FLOW-PORT");

                functionFlowPorts.Value = count;
            }
            else
            {
                int count = localSearcher.nestedChildElementList(xml, elementName, "FUNCTION-FLOW-PORT");

                functionFlowPorts.Value = count;
            }

            return functionFlowPorts;
        }

        public Metric FunctionPowerPorts(XDocument xml, string elementName)
        {
            XElement possibleArchitecture = helper.navigateToNode(xml, elementName);

            if (possibleArchitecture.Name.ToString().Contains("ARCHITECTURE"))
            {
                // gets the function type of this architecture
                XElement possibleFunctionType = helper.getFunctionTypeFromArchitecture(xml, possibleArchitecture);

                string shortName = helper.getShortName(possibleFunctionType);

                int count = localSearcher.nestedChildElementList(xml, shortName, "FUNCTION-POWER-PORT");

                functionPowerPorts.Value = count;
            }
            else
            {
                int count = localSearcher.nestedChildElementList(xml, elementName, "FUNCTION-POWER-PORT");

                functionPowerPorts.Value = count;
            }
            
            return functionPowerPorts;
        }

        public Metric FunctionClientServerPorts(XDocument xml, string elementName)
        {
            XElement possibleArchitecture = helper.navigateToNode(xml, elementName);

            if (possibleArchitecture.Name.ToString().Contains("ARCHITECTURE"))
            {
                // gets the function type of this architecture
                XElement possibleFunctionType = helper.getFunctionTypeFromArchitecture(xml, possibleArchitecture);

                string shortName = helper.getShortName(possibleFunctionType);

                int count = localSearcher.nestedChildElementList(xml, shortName, "FUNCTION-CLIENT-SERVER-PORT");

                functionClientServerPorts.Value = count;
            } 
            else
            {
                int count = localSearcher.nestedChildElementList(xml, elementName, "FUNCTION-CLIENT-SERVER-PORT");

                functionClientServerPorts.Value = count;
            }

            return functionClientServerPorts;
        }

        public Metric Operations(XDocument xml, string elementName)
        {
            int count = 0;
            XElement possibleArchitecture = helper.navigateToNode(xml, elementName);

            if (possibleArchitecture.Name.ToString().Contains("ARCHITECTURE"))
            {
                // gets the function type of this architecture
                XElement possibleFunctionType = helper.getFunctionTypeFromArchitecture(xml, possibleArchitecture);

                // get all function client server ports within the function type
                var functionClientServerPorts = possibleFunctionType.Descendants()
                                                            .Where(a => a.Name == "FUNCTION-CLIENT-SERVER-PORT");

                // iterate through all function client server ports
                foreach (var functionClientServerPort in functionClientServerPorts)
                {
                    string reference = helper.getTypeReference(functionClientServerPort);

                    var functionClientServerInterface = helper.navigateToNode(xml, reference);

                    var operations = functionClientServerInterface.Descendants()
                                                                  .Where(a => a.Name == "OPERATION");

                        count += operations.Count();
                }

                operations.Value = count;
            }
            else
            {
                // get the function type
                XElement functionType = helper.navigateToNode(xml, elementName);
                // get all function client server ports within the function type
                var functionClientServerPorts = functionType.Descendants()
                                                            .Where(a => a.Name == "FUNCTION-CLIENT-SERVER-PORT");

                // iterate through all function client server ports
                foreach (var functionClientServerPort in functionClientServerPorts)
                {
                    string reference = helper.getTypeReference(functionClientServerPort);

                    var functionClientServerInterface = helper.navigateToNode(xml, reference);

                    var operations = functionClientServerInterface.Descendants()
                                                                  .Where(a => a.Name == "OPERATION");

                    count += operations.Count();
                }

                operations.Value = count;
            }

            return operations;
        }

        public Metric HardwarePorts(XDocument xml, string elementName)
        {
            XElement possibleArchitecture = helper.navigateToNode(xml, elementName);

            if (possibleArchitecture.Name.ToString().Contains("ARCHITECTURE"))
            {
                // gets the function type of this architecture
                XElement possibleFunctionType = helper.getFunctionTypeFromArchitecture(xml, possibleArchitecture);

                string shortName = helper.getShortName(possibleFunctionType);

                int count = localSearcher.nestedChildElementList(xml, shortName,
                                                            "IO-HARDWARE-PIN",
                                                            "COMMUNICATION-HARDWARE-PIN",
                                                            "POWER-HARDWARE-PIN",
                                                            "HARDWARE-PORT");

                hardwarePorts.Value = count;
            }
            else
            {
                int count = localSearcher.nestedChildElementList(xml, elementName,
                                                            "IO-HARDWARE-PIN",
                                                            "COMMUNICATION-HARDWARE-PIN",
                                                            "POWER-HARDWARE-PIN",
                                                            "HARDWARE-PORT");

                hardwarePorts.Value = count;
            }

            return hardwarePorts;
        }

        public Metric PortGroups(XDocument xml, string elementName)
        {
            int count = 0;
            XElement possibleArchitecture = helper.navigateToNode(xml, elementName);

            if (possibleArchitecture.Name.ToString().Contains("ARCHITECTURE"))
            {
                // gets the function type of this architecture
                XElement possibleFunctionType = helper.getFunctionTypeFromArchitecture(xml, possibleArchitecture);

                string shortName = helper.getShortName(possibleFunctionType);

                var portGroup = possibleFunctionType.Descendants().Where(a => a.Name == "PORT-GROUPS").FirstOrDefault();

                // if the function type has port groups then count it
                if (portGroup != null && portGroup.HasElements)
                {
                    count = localSearcher.nestedChildElementList(xml, shortName, "PORT-GROUP");
                }

                portGroups.Value = count;
            }
            else
            {
                var functionType = helper.navigateToNode(xml, elementName);

                var portGroup = functionType.Descendants().Where(a => a.Name == "PORT-GROUPS").FirstOrDefault();

                // if the function type has port groups then count it
                if (portGroup != null && portGroup.HasElements)
                {
                    count = localSearcher.nestedChildElementList(xml, elementName, "PORT-GROUP");
                }

                portGroups.Value = count;
            }

            return portGroups;
        }

        public Metric PortGroupSize(XDocument xml, string elementName)
        {
            Dictionary<string, int> portGroupCount = new Dictionary<string, int>();

            XElement possibleArchitecture = helper.navigateToNode(xml, elementName);

            if (possibleArchitecture.Name.ToString().Contains("ARCHITECTURE"))
            {
                // gets the function type of this architecture
                XElement possibleFunctionType = helper.getFunctionTypeFromArchitecture(xml, possibleArchitecture);

                var portGroup = possibleFunctionType.Descendants().Where(a => a.Name == "PORT-GROUPS").FirstOrDefault();

                // if the function type doesn't have any portgroups, then just return
                if (portGroup != null && !portGroup.HasElements)
                {
                    portGroupSize.Value = 0;
                    return portGroupSize;
                }

                var portGroups = portGroup.Descendants().Where(a => a.Name == "PORT-GROUP");

                foreach (var pg in portGroups)
                {
                    var portRefs = pg.Descendants().Where(a => a.Name == "PORT-REFS").FirstOrDefault();

                    if (portRefs != null && portRefs.HasElements)
                    {
                        int count = portRefs.Descendants().Where(a => a.Name == "PORT-REF").ToList().Count;
                        portGroupCount.Add(helper.getShortName(pg), count);
                    }
                }

                if (portGroupCount.Count != 0)
                {
                    portGroupSize.Value = portGroupCount.Values.Average();
                }
                else
                {
                    portGroupSize.Value = 0;
                }
            }
            else
            {
                var functionType = helper.navigateToNode(xml, elementName);
                var portGroup = functionType.Descendants().Where(a => a.Name == "PORT-GROUPS").FirstOrDefault();

                // if the function type doesn't have any portgroups, then just return
                if (!portGroup.HasElements)
                {
                    portGroupSize.Value = 0;
                    return portGroupSize;
                }

                var portGroups = portGroup.Descendants().Where(a => a.Name == "PORT-GROUP");

                foreach (var pg in portGroups)
                {
                    var portRefs = pg.Descendants().Where(a => a.Name == "PORT-REFS").FirstOrDefault();

                    if (portRefs != null && portRefs.HasElements)
                    {
                        int count = portRefs.Descendants().Where(a => a.Name == "PORT-REF").ToList().Count;
                        portGroupCount.Add(helper.getShortName(pg), count);
                    }
                }

                if (portGroupCount.Count != 0)
                {
                    portGroupSize.Value = portGroupCount.Values.Average();
                }
                else
                {
                    portGroupSize.Value = 0;
                }
            }

            return portGroupSize;
        }
    }
}
