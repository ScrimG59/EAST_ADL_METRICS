using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EAST_ADL_METRICS.Utils.Searcher
{
    public class Searcher
    {
        /// <summary>
        /// Returns a list of the nodes with the given keyword
        /// </summary>
        /// <param name="xml">the xml file</param>
        /// <param name="keyword">an element which gets searched, for example "package"</param>
        /// <returns></returns>
        public List<XElement> parentElementList(XDocument xml, string firstKeyword, string secondKeyword = null, string thirdKeyword = null, string fourthKeyword = null, string fifthKeyword = null, string sixthKeyword = null)
        {
            List<XElement> parentList = xml.Descendants()
                                            .Where(node => node.Name == firstKeyword
                                                        || node.Name == secondKeyword
                                                        || node.Name == thirdKeyword
                                                        || node.Name == fourthKeyword
                                                        || node.Name == fifthKeyword
                                                        || node.Name == sixthKeyword).ToList();

            return parentList;
        }

        /// <summary>
        /// Returns pairs of the parent node and the count of the keyword child nodes
        /// </summary>
        /// <param name="xml">the xml file</param>
        /// <param name="parentList">a list of parent nodes, for example a list of all packages</param>
        /// <param name="keyword">an child element which gets searched within the parent node</param>
        /// <returns></returns>
        public Dictionary<string, int> nestedChildElementList(List<XElement> parentList, string firstKeyword, string secondKeyword = null, string thirdKeyword = null, string fourthKeyword = null, string fifthKeyword = null, string sixthKeyword = null)
        {
            Dictionary<string, int> parentChildCountPair = new Dictionary<string, int>();

            foreach (var node in parentList)
            {
                if (node.HasElements)
                {
                    // Elements == 1st level, Descendants == All levels
                    List<XElement> matchingChildNodes = node.Descendants()
                                                            .Where(child => child.Name == firstKeyword 
                                                            || child.Name == secondKeyword 
                                                            || child.Name == thirdKeyword 
                                                            || child.Name == fourthKeyword
                                                            || node.Name == fifthKeyword
                                                            || node.Name == sixthKeyword)
                                                            .ToList();

                    if (matchingChildNodes.Count != 0)
                    {
                        Console.WriteLine("Matching child nodes!");
                        var name = getName(node);
                        if (!parentChildCountPair.ContainsKey(name[0]))
                        {
                            parentChildCountPair.Add(name[0], matchingChildNodes.Count);
                        }
                        else
                        {
                            parentChildCountPair.Add(name[1], matchingChildNodes.Count);
                        }
                    }
                    else
                    {
                        var name = getName(node);
                        if (!parentChildCountPair.ContainsKey(name[0]))
                        {
                            parentChildCountPair.Add(name[0], 0);
                        }
                        else
                        {
                            parentChildCountPair.Add(name[1], 0);
                        }
                        Console.WriteLine("No matching child nodes!");
                    }
                }
                else
                {
                    continue;
                }
            }

            return parentChildCountPair;
        }

        /// <summary>
        /// Does the same thing as the method "childElementList" but it also considers the nested child nodes
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="parentList"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public Dictionary<string, int> childElementList(List<XElement> parentList, string firstKeyword, string secondKeyword = null, string thirdKeyword = null, string fourthKeyword = null, string fifthKeyword = null, string sixthKeyword = null)
        {
            Dictionary<string, int> parentChildCountPair = new Dictionary<string, int>();

            foreach(var node in parentList)
            {
                XElement element = node.Elements()
                                       .Where(child => child.Name == "ELEMENTS")
                                       .FirstOrDefault();

                if(element == null)
                {
                    var name = getName(node);
                    if (!parentChildCountPair.ContainsKey(name[0]))
                    {
                        parentChildCountPair.Add(name[0], 0);
                    }
                    else
                    {
                        parentChildCountPair.Add(name[1], 0);
                    }
                }

                else if (element.HasElements)
                {
                    List<XElement> matchingChildNodes = element.Elements()
                                                               .Where(child => child.Name == firstKeyword
                                                               || child.Name == secondKeyword
                                                               || child.Name == thirdKeyword
                                                               || child.Name == fourthKeyword
                                                               || node.Name == fifthKeyword
                                                               || node.Name == sixthKeyword)
                                                               .ToList();
                    if (matchingChildNodes.Count != 0)
                    {
                        Console.WriteLine("Matching child nodes!");
                        var name = getName(node);
                        if (!parentChildCountPair.ContainsKey(name[0]))
                        {
                            parentChildCountPair.Add(name[0], matchingChildNodes.Count);
                        }
                        else
                        {
                            parentChildCountPair.Add(name[1], matchingChildNodes.Count);
                        }
                    }
                    else
                    {
                        var name = getName(node);
                        if (!parentChildCountPair.ContainsKey(name[0]))
                        {
                            parentChildCountPair.Add(name[0], 0);
                        }
                        else
                        {
                            parentChildCountPair.Add(name[1], 0);
                        }
                        Console.WriteLine("No matching child nodes!");
                    }

                }
                else
                {
                    continue;
                }
            }

            return parentChildCountPair;
        }
        
        /// <summary>
        /// this method is only used for the "Parts_fct_tc" metric because it needs the "constructTree()"-method
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="parentList"></param>
        /// <returns></returns>
        public Dictionary<string, int> recursiveChildElementList(XDocument xml, List<XElement> parentList)
        {
            Dictionary<string, int> parentChildCountPair = new Dictionary<string, int>();

            foreach(var node in parentList)
            {
                List<XElement> tree = new List<XElement>();
                tree = constructTree(xml, node, tree);
                if (tree == null)
                {
                    var name = getName(node);
                    if (!parentChildCountPair.ContainsKey(name[0]))
                    {
                        parentChildCountPair.Add(name[0], 0);
                    }
                    else
                    {
                        parentChildCountPair.Add(name[1], 0);
                    }
                }
                else
                {
                    var name = getName(node);
                    if (!parentChildCountPair.ContainsKey(name[0]))
                    {
                        parentChildCountPair.Add(name[0], tree.Count);
                    }
                    else
                    {
                        parentChildCountPair.Add(name[1], tree.Count);
                    }
                }
            }
            return parentChildCountPair;
        }

        /// <summary>
        /// gets the short-name or id of the xml-tag
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private List<string> getName (XElement node)
        {
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

        /// <summary>
        /// gets the node from the given name string
        /// </summary>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private XElement getNodeFromName(XElement node, string name)
        {
            List<XElement> nodeList = node.Descendants().ToList();
            XElement matchingElement = new XElement("Dummy");

            foreach(var nodeElement in nodeList)
            {
                if (nodeElement.Element("SHORT-NAME") != null && nodeElement.Element("SHORT-NAME").Value == name)
                {
                    matchingElement = nodeElement;
                    break;
                }
                else if(nodeElement.Element("SHORT-NAME") == null)
                {
                    continue;
                }
                else
                {
                    Console.WriteLine("ERROR: Couldn't find node!");
                }
            }
            return matchingElement;
        }

        /// <summary>
        /// checks if the given functiontype node has parts in it (prototypes)
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private Boolean hasParts(XElement node)
        {
            if(node.Element("PARTS").HasElements)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// gets all the prototype of a given functiontype
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private List<XElement> getParts(XElement node)
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
        /// gets the value of the TYPE-TREF tag of the given node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public string getType(XElement node)
        {
            string type = node.Descendants()
                              .Where(child => child.Name == "TYPE-TREF")
                              .FirstOrDefault()
                              .Value;

            return type;          
        }

        /// <summary>
        /// navigates to the Node that is given by the TYPE-TREF tag
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="tref"></param>
        /// <returns></returns>
        private XElement navigateToNode(XDocument xml, string tref)
        {
            tref = tref.Substring(1);
            string[] seperatedTrefs = tref.Split('/');
            XElement node = xml.Descendants()
                               .Where(a => a.Name == "EAXML")
                               .FirstOrDefault();
            for(int i = 0; i < seperatedTrefs.Count(); i++)
            {
                node = getNodeFromName(node, seperatedTrefs[i]);
            }

            return node;
        }

        /// <summary>
        /// recursivly constructs a node-tree with the helper methods above
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="node"></param>
        /// <param name="tree"></param>
        /// <returns></returns>
        public List<XElement> constructTree(XDocument xml, XElement node, List<XElement> tree)
        {
            /*if(node != null && (node.Name == "DESIGN-FUNCTION-TYPE" ||
                               node.Name == "ANALYSIS-FUNCTION-TYPE" ||
                               node.Name == "HARDWARE-FUNCTION-TYPE" ||
                               node.Name == "HARDWARE-COMPONENT-TYPE" ||
                               node.Name == "BASIC-SOFTWARE-FUNCTION-TYPE"))
            {}*/
            if (!hasParts(node))
            {
                return tree;
            }
            else
            {
                List<XElement> parts = getParts(node);
                foreach(var part in parts)
                {
                    tree.Add(part);
                    var functionType = navigateToNode(xml, getType(part));
                    constructTree(xml, functionType, tree);
                }
                return tree;
            }
        }

    }
}
