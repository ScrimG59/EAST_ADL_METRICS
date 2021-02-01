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
    public class Requirement
    {
        private Global globalSearcher = new Global();
        private Local localSearcher = new Local();
        private Helper helper = new Helper();

        private Metric subReqts = new Metric
        {
            Name = "SubReqts",
            Category = "Size",
            Type = "Requirement",
            Nested = true
        };

        private Metric nestingLevel = new Metric
        {
            Name = "NestingLevel",
            Category = "Size",
            Type = "Requirement",
            Nested = false
        };

        private Metric satisfiers = new Metric
        {
            Name = "Satisfiers",
            Category = "Size",
            Type = "Requirement",
            Nested = false
        };

        private Metric verifiers = new Metric
        {
            Name = "Verifiers",
            Category = "Size",
            Type = "Requirement",
            Nested = false
        };

        private Metric derivatives = new Metric
        {
            Name = "Derivatives",
            Category = "Size",
            Type = "Requirement",
            Nested = false
        };

        public Metric SubReqts(XDocument xml, string elementName)
        {
            /*if (mode)
            {
                var parentList = globalSearcher.parentElementList(xml, "REQUIREMENTS-HIERARCHY");

                var childElementList = globalSearcher.nestedChildElementList(parentList, "REQUIREMENTS-HIERARCHY");

                subReqts.MaxValue = childElementList.Values.Max();
                subReqts.MinValue = childElementList.Values.Min();
                subReqts.AvgValue = childElementList.Values.Average();
            }*/

            int count = localSearcher.subRequirementElementList(xml, elementName);

            subReqts.Value = count;

            return subReqts;
        }

        public Metric NestingLevel(XDocument xml, string elementName)
        {
            /*if (mode)
            {
                var parentList = globalSearcher.parentElementList(xml, "REQUIREMENTS-HIERARCHY");

                var nestingLevels = globalSearcher.getNestingLevels(parentList);

                nestingLevel.MaxValue = nestingLevels.Values.Max();
                nestingLevel.MinValue = nestingLevels.Values.Min();
                nestingLevel.AvgValue = nestingLevels.Values.Average();
            }*/

            int count = localSearcher.getNestingLevels(xml, elementName);

            nestingLevel.Value = count;

            return nestingLevel;
        }

        public Metric Satisfiers(XDocument xml, string elementName)
        {
            int count = 0;

            XElement requirement = helper.navigateToNode(xml, elementName);

            List<XElement> allFunctionTypes = globalSearcher.parentElementList(xml,
                                                            "DESIGN-FUNCTION-TYPE",
                                                            "ANALYSIS-FUNCTION-TYPE",
                                                            "HARDWARE-FUNCTION-TYPE",
                                                            "BASIC-SOFTWARE-FUNCTION-TYPE");

            foreach(var functionType in allFunctionTypes)
            {
                var reference = functionType.Descendants()
                                            .Where(a => a.Name == "SATISFIED-REQUIREMENT-REFS")
                                            .FirstOrDefault();

                if(reference != null)
                {
                    var referenceList = reference.Elements();

                    foreach(var r in referenceList)
                    {
                        if(helper.navigateToNode(xml, r.Value) == requirement)
                        {
                            count++;
                        }
                    }
                }
            }

            satisfiers.Value = count;

            return satisfiers;
        }

        public Metric Verifiers(XDocument xml, string elementName)
        {
            int count = 0;

            XElement requirement = helper.navigateToNode(xml, elementName);

            List<XElement> allFunctionTypes = globalSearcher.parentElementList(xml,
                                                            "DESIGN-FUNCTION-TYPE",
                                                            "ANALYSIS-FUNCTION-TYPE",
                                                            "HARDWARE-FUNCTION-TYPE",
                                                            "BASIC-SOFTWARE-FUNCTION-TYPE");

            foreach (var functionType in allFunctionTypes)
            {
                var reference = functionType.Descendants()
                                            .Where(a => a.Name == "VERIFIED-REQUIREMENT-REFS")
                                            .FirstOrDefault();

                if (reference != null)
                {
                    var referenceList = reference.Elements();

                    foreach (var r in referenceList)
                    {
                        if (helper.navigateToNode(xml, r.Value) == requirement)
                        {
                            int vvCaseCount = functionType.Descendants()
                                                          .Where(a => a.Name == "VERIFIED-BY-CASE-REFS")
                                                          .Count();

                            int vvProcedureCount = functionType.Descendants()
                                                               .Where(a => a.Name == "VERIFIED-BY-PROCEDURE-REFS")
                                                               .Count();

                            count += vvCaseCount + vvProcedureCount; 
                        }
                    }
                }
            }

            verifiers.Value = count;

            return verifiers;
        }

        public Metric Derivatives(XDocument xml, string elementName)
        {
            return derivatives;
        }
    }
}
