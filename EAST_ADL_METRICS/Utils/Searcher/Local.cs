using System.Collections.Generic;
using System.Linq;
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
        /// <param name="parentName">the name of the parent node</param>
        /// <param name="keyword">an child element which gets searched within the parent node</param>
        /// <returns></returns>
        public int nestedChildElementList(XDocument xml, string parentName, string firstKeyword, string secondKeyword = null, string thirdKeyword = null, string fourthKeyword = null, string fifthKeyword = null, string sixthKeyword = null)
        {
            XElement parent = helper.navigateToNode(xml, parentName);

            if (parent != null && parent.HasElements)
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
                    return matchingChildNodes.Count;
                }
                else
                {
                    return 0;
                }
            }

            return 0;
        }

        /// <summary>
        /// only searches in the first nesting level
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="parentList"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public int childElementList(XDocument xml, string parentName, string firstKeyword, string secondKeyword = null, string thirdKeyword = null, string fourthKeyword = null, string fifthKeyword = null, string sixthKeyword = null)
        {
            XElement parent = helper.navigateToNode(xml, parentName);

            XElement element = parent.Elements()
                                     .Where(child => child.Name == "ELEMENTS")
                                     .FirstOrDefault();

            if (element != null && element.HasElements)
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
                    return matchingChildNodes.Count;
                }
                else
                {
                    return 0;
                }
            }

            return 0;
        }

        /// <summary>
        /// this method is only used for the "Parts_fct_tc" metric because it needs the "constructTree()"-method
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="parentList"></param>
        /// <returns></returns>
        public int recursiveChildElementList(XDocument xml, string parentName)
        {
            XElement parent = helper.navigateToNode(xml, parentName);

            List<XElement> tree = new List<XElement>();
            nestingLevel = 0;
            currentMax = 0;
            tree = constructTree(xml, parent, tree);
            if (tree == null)
            {
                return 0;
            }
            else
            {
                return tree.Count;
            }
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
                    var functionType = helper.navigateToNode(xml, helper.getTypeReference(part));
                    //Console.WriteLine($"INITIAL FUNCTIONTYPE: {helper.getName(node)[0]}\nFUNCTIONTYPE: {helper.getName(functionType)[0]}\n NESTINGLEVEL: {nestingLevel}\n PROTOTYPE: {part.ToString()}");
                    constructTree(xml, functionType, tree);
                }
                nestingLevel--;
                return tree;
            }
        }

        /// <summary>
        /// gets the nestinglevel for functiontypes (parts_tc)
        /// </summary>
        /// <returns></returns> 
        public int getNestingLevel()
        {
            return currentMax;
        }

    }
}
