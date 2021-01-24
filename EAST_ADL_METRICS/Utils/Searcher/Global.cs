using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace EAST_ADL_METRICS.Utils.Searcher
{
    public class Global
    {
        private int nestingLevel = 0;
        private int currentMax = 0;
        private Helper helper = new Helper();
        private List<int> nestingLevels = new List<int>();

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
                                                            || child.Name == fifthKeyword
                                                            || child.Name == sixthKeyword)
                                                            .ToList();

                    if (matchingChildNodes.Count != 0)
                    {
                        var name = helper.getName(node);
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
                        var name = helper.getName(node);
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
                    var name = helper.getName(node);
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
                        var name = helper.getName(node);
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
                        var name = helper.getName(node);
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
                nestingLevel = 0;
                tree = constructTree(xml, node, tree);
                nestingLevels.Add(nestingLevel);
                if (tree == null)
                {
                    var name = helper.getName(node);
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
                    var name = helper.getName(node);
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
            Console.WriteLine($"NESTINGLEVEL:{currentMax}");
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
            
            if(nestingLevel > currentMax)
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
                foreach(var part in parts)
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

        public List<int> getNestingLevels()
        {
            return nestingLevels;
        }

    }
}
