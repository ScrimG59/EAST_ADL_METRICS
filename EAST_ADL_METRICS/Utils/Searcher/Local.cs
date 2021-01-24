using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EAST_ADL_METRICS.Utils.Searcher
{
    public class Local
    {
        private int nestingLevel = 0;
        private int currentMax = 0;
        private Helper helper = new Helper();

        /// <summary>
        /// Returns pair of the parent node and the count of the keyword child nodes
        /// </summary>
        /// <param name="xml">the xml file</param>
        /// <param name="parentList">a list of parent nodes, for example a list of all packages</param>
        /// <param name="keyword">an child element which gets searched within the parent node</param>
        /// <returns></returns>
        public Dictionary<string, int> nestedChildElementList(XDocument xml, string parentName, string firstKeyword, string secondKeyword = null, string thirdKeyword = null, string fourthKeyword = null, string fifthKeyword = null, string sixthKeyword = null)
        {
            Dictionary<string, int> parentChildCountPair = new Dictionary<string, int>();
            XElement parent = helper.navigateToNode(xml, parentName);

            if (parent.HasElements)
            {
                // Elements == 1st level, Descendants == All levels
                List<XElement> matchingChildNodes = parent.Descendants()
                                                          .Where(child => child.Name == firstKeyword
                                                          || child.Name == secondKeyword
                                                          || child.Name == thirdKeyword
                                                          || child.Name == fourthKeyword
                                                          || child.Name == fifthKeyword
                                                          || child.Name == sixthKeyword)
                                                          .ToList();

                if (matchingChildNodes.Count != 0)
                {
                    var name = helper.getName(parent);
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
                    var name = helper.getName(parent);
                    if (!parentChildCountPair.ContainsKey(name[0]))
                    {
                        parentChildCountPair.Add(name[0], 0);
                    }
                    else
                    {
                        parentChildCountPair.Add(name[1], 0);
                    }
                }
            }
            else{}

            return parentChildCountPair;
        }

        /// <summary>
        /// Does the same thing as the method "childElementList" but it also considers the nested child nodes
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="parentList"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public Dictionary<string, int> childElementList(XDocument xml, string parentName, string firstKeyword, string secondKeyword = null, string thirdKeyword = null, string fourthKeyword = null, string fifthKeyword = null, string sixthKeyword = null)
        {
            Dictionary<string, int> parentChildCountPair = new Dictionary<string, int>();
            XElement parent = helper.navigateToNode(xml, parentName);

            XElement element = parent.Elements()
                                     .Where(child => child.Name == "ELEMENTS")
                                     .FirstOrDefault();

            if (element == null)
            {
                var name = helper.getName(parent);
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
                                                           || child.Name == fifthKeyword
                                                           || child.Name == sixthKeyword)
                                                           .ToList();
                if (matchingChildNodes.Count != 0)
                {
                    var name = helper.getName(parent);
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
                    var name = helper.getName(parent);
                    if (!parentChildCountPair.ContainsKey(name[0]))
                    {
                        parentChildCountPair.Add(name[0], 0);
                    }
                    else
                    {
                        parentChildCountPair.Add(name[1], 0);
                    }
                }

            }
            else{}

            return parentChildCountPair;
        }

        /// <summary>
        /// this method is only used for the "Parts_fct_tc" metric because it needs the "constructTree()"-method
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="parentList"></param>
        /// <returns></returns>
        public Dictionary<string, int> recursiveChildElementList(XDocument xml, string parentName)
        {
            Dictionary<string, int> parentChildCountPair = new Dictionary<string, int>();
            XElement parent = helper.navigateToNode(xml, parentName);

            List<XElement> tree = new List<XElement>();
            nestingLevel = 0;
            tree = constructTree(xml, parent, tree);
            if (tree == null)
            {
                var name = helper.getName(parent);
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
                var name = helper.getName(parent);
                if (!parentChildCountPair.ContainsKey(name[0]))
                {
                    parentChildCountPair.Add(name[0], tree.Count);
                }
                else
                {
                    parentChildCountPair.Add(name[1], tree.Count);
                }
            }
            return parentChildCountPair;
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

            if (nestingLevel > currentMax)
            {
                currentMax = nestingLevel;
            }
            if (!helper.hasParts(node))
            {
                return tree;
            }
            else
            {
                nestingLevel++;
                List<XElement> parts = helper.getParts(node);
                foreach (var part in parts)
                {
                    tree.Add(part);
                    var functionType = helper.navigateToNode(xml, helper.getType(part));
                    //Console.WriteLine($"INITIAL FUNCTIONTYPE: {helper.getName(node)[0]}\nFUNCTIONTYPE: {helper.getName(functionType)[0]}\n NESTINGLEVEL: {nestingLevel}\n PROTOTYPE: {part.ToString()}");
                    constructTree(xml, functionType, tree);
                }
                nestingLevel--;
                return tree;
            }
        }

        public int getNestingLevel()
        {
            return nestingLevel;
        }

    }
}
