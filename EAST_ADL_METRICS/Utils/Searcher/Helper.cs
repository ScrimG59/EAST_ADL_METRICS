using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace EAST_ADL_METRICS.Utils.Searcher
{
    public class Helper
    {
        /// <summary>
        /// gets the short-name or id of the xml-tag
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public List<string> getName(XElement node)
        {
            try {
                XName id = "UUID";
                List<string> idNamePair = new List<string>();

                if (node.HasAttributes && node.Attribute(id).Value != "")
                {
                    idNamePair.Add(node.Attribute(id).Value);
                }
                if (node.Element("SHORT-NAME").Value != "")
                {
                    idNamePair.Add(node.Element("SHORT-NAME").Value);
                }
                else
                {
                    Console.WriteLine("ERROR: Cannot find name or ID!");
                }

                return idNamePair;
            }
            catch
            {
                throw new Exception("COULDN'T FIND NAME.");
            }
        }

        /// <summary>
        /// gets the SHORT-NAME of an element
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public string getShortName(XElement node)
        {
            try
            {
                return node.Element("SHORT-NAME").Value;
            }
            catch
            {
                throw new Exception("Couldn't find name!");
            }
        }

        /// <summary>
        /// gets the node from the given name string
        /// </summary>
        /// <param name="node">the whole xml-file as a node</param>
        /// <param name="name"></param>
        /// <returns></returns>
        public XElement getNodeFromName(XElement node, string name)
        {
            List<XElement> nodeList = node.Descendants().ToList();
            XElement matchingElement = new XElement("Dummy");

            foreach (var nodeElement in nodeList)
            {
                if (nodeElement.Element("SHORT-NAME") != null && nodeElement.Element("SHORT-NAME").Value == name)
                {
                    matchingElement = nodeElement;
                    break;
                }
                else if (nodeElement.Element("SHORT-NAME") == null)
                {
                    continue;
                }
            }
            return matchingElement;
        }

        /// <summary>
        /// checks if the given functiontype node has parts in it (prototypes)
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public Boolean hasParts(XElement node)
        {
            if (node.Name.ToString().Contains("FUNCTION-TYPE") && node.Element("PARTS").HasElements)
            {
                //Console.WriteLine($"NODE: {getShortName(node)}");
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// gets all the prototypes of a given functiontype
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public List<XElement> getParts(XElement node)
        {
            List<XElement> partList = new List<XElement>();
            XElement partNode = new XElement("PartNode");

            if (!hasParts(node))
            {
                return partList;
            }
            else
            {
                partNode = node.Element("PARTS");
                partList = partNode.Descendants()
                                   .Where(a => a.Name.ToString().Contains("PROTOTYPE"))
                                   .ToList();
                return partList;
            }
        }

        /// <summary>
        /// gets the value of the REF tag of the given node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public string getTypeReference(XElement node)
        {
            var type = node.Descendants()
                              .Where(child => child.Name == "TYPE-TREF" ||
                                              child.Name == "ALLOCATION-TARGET-REF")
                              .FirstOrDefault();

            if(type != null)
            {
                return type.Value;
            }

            return "";
        }

        /// <summary>
        /// gets the type of the node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public string getType(XElement node)
        {
            return node.Name.ToString();
        }

        /// <summary>
        /// navigates to the Node that is given by the TYPE-TREF tag
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="tref"></param>
        /// <returns></returns>
        public XElement navigateToNode(XDocument xml, string tref)
        {
            if (tref.StartsWith("/"))
            {
                tref = tref.Substring(1);
            }

            string[] seperatedTrefs = tref.Split('/');
            XElement node = xml.Descendants()
                               .Where(a => a.Name == "EAXML")
                               .FirstOrDefault();

            for (int i = 0; i < seperatedTrefs.Count(); i++)
            {
                node = getNodeFromName(node, seperatedTrefs[i]);
            }

            return node;
        }

        /// <summary>
        /// gets all functiontypes in the given xml-file
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public List<XElement> getAllElements(XDocument xml)
        {
            var nodeList = xml.Descendants()
                              .Where(node => node.Name == "DESIGN-FUNCTION-TYPE" ||
                                             node.Name == "ANALYSIS-FUNCTION-TYPE" ||
                                             node.Name == "HARDWARE-FUNCTION-TYPE" ||
                                             node.Name == "BASIC-SOFTWARE-FUNCTION-TYPE" ||
                                             node.Name == "REQUIREMENT" ||
                                             node.Name == "QUALITY-REQUIREMENT" ||
                                             node.Name == "EA-PACKAGE" || 
                                             node.Name == "FUNCTIONAL-ANALYSIS-ARCHITECTURE" ||
                                             node.Name == "HARDWARE-DESIGN-ARCHITECTURE" ||
                                             node.Name == "FUNCTIONAL-DESIGN-ARCHITECTURE" ||
                                             node.Name == "MODE")
                              .ToList();

            return nodeList;
        }

        /// <summary>
        /// gets the contained requirment within a requirment hierarchy
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public XElement getContainedRequirement(XDocument xml, XElement node)
        {
            string reference = node.Element("CONTAINED-REQUIREMENT-REF").Value;
            XElement reqtsNode = navigateToNode(xml, reference);

            return reqtsNode;
        }

        /// <summary>
        /// gets the function type of the given architecture
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public XElement getFunctionTypeFromArchitecture(XDocument xml, XElement node)
        {
            return navigateToNode(xml, getTypeReference(node));
        }
    }
}
