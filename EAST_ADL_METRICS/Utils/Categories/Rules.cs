using EAST_ADL_METRICS.Models;
using EAST_ADL_METRICS.Utils.Searcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EAST_ADL_METRICS.Utils.Categories
{
    public class Rules
    {
        private Global globalSearcher = new Global();
        private Helper helper = new Helper();

        private Rule portConnectorAllocation = new Rule
        {
            Name = "PortConnectorAllocation"
        };

        private Rule unverified = new Rule
        {
            Name = "Unverified"
        };

        private Rule residualAnomaly = new Rule
        {
            Name = "ResidualAnomaly"
        };

        private Rule reference = new Rule
        {
            Name = "Reference"
        };

        private Rule eventChainPair = new Rule
        {
            Name = "EventChainPair"
        };

        private Rule modeAllocation = new Rule
        {
            Name = "ModeAllocation"
        };

        public Rule PortConnectorAllocation(XDocument xml)
        {
            bool portConnector = false;

            var portList = globalSearcher.parentElementList(xml, "FUNCTION-FLOW-PORT",
                                                                 "FUNCTION-CLIENT-SERVER-PORT",
                                                                 "FUNCTION-POWER-PORT",
                                                                 "IO-HARDWARE-PIN",
                                                                 "COMMUNICATION-HARDWARE-PIN");

            var connectorList = globalSearcher.parentElementList(xml, "FUNCTION-CONNECTOR", "HARDWARE-CONNECTOR");

            // first check, if every connector has exactly two ports connected
            foreach(var connector in connectorList)
            {
                var portRefCount = connector.Descendants().Where(a => a.Name == "PORT-IREF").ToList().Count;

                if(portRefCount != 2)
                {
                    portConnectorAllocation.Fulfilled = false;
                    return portConnectorAllocation;
                }
            }

            // now check, if every port is allocated to one connector 
            foreach(var port in portList)
            {
                portConnector = false;
                foreach(var connector in connectorList)
                {
                    var functionPortRefs = connector.Descendants().Where(a => a.Name == "FUNCTION-PORT-REF" ||
                                                                              a.Name == "HARDWARE-PIN-REF")
                                                                  .ToList();
                    
                    var actualPort1 = helper.navigateToNode(xml, functionPortRefs[0].Value);
                    var actualPort2 = helper.navigateToNode(xml, functionPortRefs[1].Value);

                    if (actualPort1 == port || actualPort2 == port)
                    {
                        //Console.WriteLine($"CONNECTOR: ${helper.getShortName(connector)}, PORT: {helper.getShortName(port)}");
                        portConnector = true;
                        break;       
                    }
                }
            }

            portConnectorAllocation.Fulfilled = portConnector;
            return portConnectorAllocation;
        }

        public Rule Unverified(XDocument xml)
        {
            bool found = false;
            // get all requirement hierarchys
            var requirementHierarchyList = globalSearcher.parentElementList(xml, "REQUIREMENTS-HIERARCHY");

            if(requirementHierarchyList.Count() != 0)
            {
                // iterate through every hierarchy and check if they have children
                foreach(var requirementHierarchy in requirementHierarchyList)
                {
                    var children = requirementHierarchy.Descendants().Where(a => a.Name == "REQUIREMENTS-HIERARCHY");
                    
                    // if they don't have children, they have to get verified
                    if(children.Count() == 0)
                    {
                        found = false;
                        // get the contained requirement
                        var containedRequirement = helper.getContainedRequirement(xml, requirementHierarchy);

                        // get all verify-relations in the file
                        var verifyList = globalSearcher.parentElementList(xml, "VERIFY");

                        if (verifyList.Count() != 0)
                        {
                            // iterate through each verify relation
                            foreach (var verify in verifyList)
                            {
                                if (found)
                                {
                                    break;
                                }
                                // get all verified requirement refs in the verify-relation
                                var referenceList = verify.Descendants().Where(a => a.Name == "VERIFIED-REQUIREMENT-REF");

                                // iterate through each reference and check if the verified requirement equals the selected requirement
                                foreach (var reference in referenceList)
                                {
                                    if (helper.navigateToNode(xml, reference.Value) == containedRequirement)
                                    {
                                        // since there has to be a case verifying the requirement according to the meta model
                                        // check if there's "vvCase"
                                        var vvCase = verify.Descendants().Where(a => a.Name == "VERIFIED-BY-CASE-REF");

                                        if (vvCase.Count() != 0)
                                        {
                                            found = true;
                                            break;
                                        }    
                                    }
                                }
                            }

                            // if he didn't find any case/procedure verifying the requirement, the rule isn't fulfilled
                            if (!found)
                            {
                                unverified.Fulfilled = found;
                                return unverified;
                            }
                        }
                    }
                }
            }
            unverified.Fulfilled = found;
            return unverified;
        }

        public Rule ResidualAnomaly(XDocument xml)
        {
            var anomalyRefs = globalSearcher.parentElementList(xml, "ANOMALY-IREF");

            if(anomalyRefs.Count() != 0)
            {
                residualAnomaly.Fulfilled = true;
            }
            else
            {
                residualAnomaly.Fulfilled = false;
            }

            return residualAnomaly;
        }

        public Rule Reference(XDocument xml)
        {
            var protoTypeList = globalSearcher.parentElementList(xml, 
                                                     "DESIGN-FUNCTION-PROTOTYPE",
                                                     "ANALYSIS-FUNCTION-PROTOTYPE",
                                                     "HARDWARE-COMPONENT-PROTOTYPE",
                                                     "BASIC-SOFTWARE-FUNCTION-PROTOTYPE");

            foreach(var protoType in protoTypeList)
            {
                var typeRef = protoType.Descendants().Where(a => a.Name == "TYPE-TREF").FirstOrDefault();

                // if typeRef is null or the value is null or he doesn't find the node (because the path is possibly wrong)
                if(typeRef == null || typeRef.Value == "" || helper.navigateToNode(xml, typeRef.Value).Name == "Dummy")
                {
                    reference.Fulfilled = false;
                    return reference;
                }
            }

            reference.Fulfilled = true;
            return reference;
        }

        public Rule EventChainPair(XDocument xml)
        {
            var eventChainList = globalSearcher.parentElementList(xml, "EVENT-CHAIN");

            foreach(var eventChain in eventChainList)
            {
                var stimulus = eventChain.Descendants().Where(a => a.Name == "STIMULUS-REF").FirstOrDefault();
                var response = eventChain.Descendants().Where(a => a.Name == "RESPONSE-REF").FirstOrDefault();

                if(stimulus == null || response == null)
                {
                    eventChainPair.Fulfilled = false;
                    return eventChainPair;
                }
            }

            eventChainPair.Fulfilled = true;
            return eventChainPair;
        }

        public Rule ModeAllocation(XDocument xml)
        {
            // get every mode in the file
            var modeList = globalSearcher.parentElementList(xml, "MODE");
            var functionTriggerList = globalSearcher.parentElementList(xml, "FUNCTION-TRIGGER");

            // if there are modes but no function triggers the rule isn't fulfilled
            if((modeList.Count() != 0 && functionTriggerList.Count() == 0))
            {
                modeAllocation.Fulfilled = false;
                return modeAllocation;
            }

            if (modeList.Count() != 0)
            {
                // iterate through each mode and check if there's a function ref of a function trigger referencing to the given mode
                foreach(var mode in modeList)
                {
                    foreach(var functionTrigger in functionTriggerList)
                    {
                        var modeRefList = functionTrigger.Descendants().Where(a => a.Name == "MODE-REF");

                        if(modeRefList.Count() != 0)
                        {
                            foreach(var modeRef in modeRefList)
                            {
                                // if theres a mode-ref referencing to the given mode, the rule is fulfilled
                                if (helper.navigateToNode(xml, modeRef.Value) == mode)
                                {
                                    modeAllocation.Fulfilled = true;
                                    return modeAllocation;
                                }
                            }
                        } 
                    }
                }
            }
            else
            {
                modeAllocation.Fulfilled = true;
                return modeAllocation;
            }

            modeAllocation.Fulfilled = false;
            return modeAllocation;
        }
    }
}
