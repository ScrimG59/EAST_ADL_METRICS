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
        private Global global = new Global();

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

        public int subRequirementElementList(XDocument xml, string parentName)
        {
            // navigates to the node of the parentName-string
            var requirement = helper.navigateToNode(xml, parentName);
            // gets all requirements-hierarchy
            var hierarchies = global.parentElementList(xml, "REQUIREMENTS-HIERARCHY");

            foreach(var hierarchy in hierarchies)
            {
                // if the right hierarchy got found
                if(requirement == helper.getContainedRequirement(xml, hierarchy))
                {
                    // count the amount of subrequirementhierarchies
                    int count = hierarchy.Descendants().Where(a => a.Name == "REQUIREMENTS-HIERARCHY").ToList().Count;
                    return count;
                }
                else
                {
                    return 0;
                }
            }
            return 0;
        }

        /// <summary>
        /// only for requirement metrics in this case
        /// </summary>
        /// <returns></returns>
        public int getNestingLevels(XDocument xml, string parentName)
        {
            // navigates to the node of the parentName-string
            var requirement = helper.navigateToNode(xml, parentName);
            // gets all requirements-hierarchy
            var hierarchies = global.parentElementList(xml, "REQUIREMENTS-HIERARCHY");

            foreach (var hierarchy in hierarchies)
            {
                // if the right hierarchy got found
                if (requirement == helper.getContainedRequirement(xml, hierarchy))
                {
                    var tempHierarchy = hierarchy;
                    int currentNestingLevel = 0;

                    while (true)
                    {
                        var currentNode = xml.Descendants()
                                             .Where(a => a.Name == tempHierarchy.Name)
                                             .Where(b => helper.getShortName(b) == helper.getShortName(tempHierarchy))
                                             .Select(c => c.Parent).Select(d => d.Parent).ToList();

                        // if another requirement got found as the parent, increment the nesting level
                        if(currentNode[0].Name == "REQUIREMENTS-HIERARCHY")
                        {
                            tempHierarchy = currentNode[0];
                            currentNestingLevel++;
                        }
                        // this is the intermediate step to get to another requirements hierarchy
                        else if(currentNode[0].Name == "CHILD-HIERARCHYS")
                        {
                            //node = currentNode;
                            continue;
                        }
                        // if he doesn't find any more requirements as parents break out of the while-loop
                        else
                        {
                            break;
                        }  
                    }

                    // return the nesting level
                    return currentNestingLevel;
                }
            }

            return 0;
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
