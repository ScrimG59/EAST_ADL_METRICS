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

        // TODO
        public Rule Unverified(XDocument xml)
        {
            unverified.Fulfilled = true;
            return unverified;
        }

        public Rule ResidualAnomaly(XDocument xml)
        {
            var anomalyRefs = globalSearcher.parentElementList(xml, "ANOMALY-IREF");

            if(anomalyRefs != null)
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
                var typeRef = xml.Descendants().Where(a => a.Name == "TYPE-TREF").FirstOrDefault();

                if(typeRef == null || typeRef.Value == "")
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
    }
}
