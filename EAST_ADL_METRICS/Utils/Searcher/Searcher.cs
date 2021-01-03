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
        public List<XElement> parentElementList(XDocument xml, string keyword)
        {
            List<XElement> parentList = xml.Descendants()
                                            .Where(node => node.Name == keyword).ToList();

            return parentList;
        }

        /// <summary>
        /// Returns pairs of the parent node and the count of the keyword child nodes
        /// </summary>
        /// <param name="xml">the xml file</param>
        /// <param name="parentList">a list of parent nodes, for example a list of all packages</param>
        /// <param name="keyword">an child element which gets searched within the parent node</param>
        /// <returns></returns>
        public Dictionary<string, int> nestedChildElementList(XDocument xml, List<XElement> parentList, string firstKeyword, string secondKeyword = null, string thirdKeyword = null, string fourthKeyword = null)
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
                                                            || child.Name == fourthKeyword)
                                                            .ToList();

                    if (matchingChildNodes.Count != 0)
                    {
                        Console.WriteLine("Matching child nodes!");
                        parentChildCountPair.Add(getName(node), matchingChildNodes.Count);
                    }
                    else
                    {
                        parentChildCountPair.Add(getName(node), 0);
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
        public Dictionary<string, int> childElementList(XDocument xml, List<XElement> parentList, string firstKeyword, string secondKeyword = null, string thirdKeyword = null, string fourthKeyword = null)
        {
            Dictionary<string, int> parentChildCountPair = new Dictionary<string, int>();

            foreach(var node in parentList)
            {
                XElement element = node.Elements()
                                       .Where(child => child.Name == "ELEMENTS")
                                       .FirstOrDefault();

                if(element == null)
                {
                    parentChildCountPair.Add(getName(node), 0);
                }

                else if (element.HasElements)
                {
                    List<XElement> matchingChildNodes = element.Elements()
                                                               .Where(child => child.Name == firstKeyword
                                                               || child.Name == secondKeyword
                                                               || child.Name == thirdKeyword
                                                               || child.Name == fourthKeyword)
                                                               .ToList();
                    if (matchingChildNodes.Count != 0)
                    {
                        Console.WriteLine("Matching child nodes!");
                        parentChildCountPair.Add(getName(node), matchingChildNodes.Count);
                    }
                    else
                    {
                        parentChildCountPair.Add(getName(node), 0);
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
        /// gets the short-name or id of the xml-tag
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private string getName (XElement node)
        {
            if(node.Element("SHORT-NAME").Value != "")
            {
                return node.Element("SHORT-NAME").Value;
            }
            else
            {
                return "";
            }
        }

    }
}
