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

            // get the requirement the user selected
            XElement requirement = helper.navigateToNode(xml, elementName);

            // get all satisfied requirement refs in the file
            var referenceList = xml.Descendants().Where(a => a.Name == "SATISFIED-REQUIREMENT-REF");

            if (referenceList.Count() != 0)
            {
                // iterate through each reference and check if the satisfied requirement equals the selected requirement
                foreach (var reference in referenceList)
                {
                    if (helper.navigateToNode(xml, reference.Value) == requirement)
                    {
                        count++;
                    }
                }
            }

            satisfiers.Value = count;

            return satisfiers;
        }

        public Metric Verifiers(XDocument xml, string elementName)
        {
            int count = 0;

            // get the requirement the user selected
            XElement requirement = helper.navigateToNode(xml, elementName);

            // get all verify-relations in the file
            var verifyList = xml.Descendants().Where(a => a.Name == "VERIFY");

            if(verifyList.Count() != 0)
            {
                foreach(var verify in verifyList)
                {
                    // get all verified requirement refs in the verify-relation
                    var referenceList = verify.Descendants().Where(a => a.Name == "VERIFIED-REQUIREMENT-REF");

                    // iterate through each reference and check if the verified requirement equals the selected requirement
                    foreach (var reference in referenceList)
                    {
                        if (helper.navigateToNode(xml, reference.Value) == requirement)
                        {
                            count += verify.Descendants()
                                           .Where(a => a.Name == "VERIFIED-BY-CASE-REF" ||
                                                       a.Name == "VERIFIED-BY-PROCEDURE-REF")
                                           .Count();
                        }
                    }
                }   
            }

            verifiers.Value = count;

            return verifiers;
        }

        public Metric Derivatives(XDocument xml, string elementName)
        {
            int count = 0;
            XElement requirement = helper.navigateToNode(xml, elementName);

            // get all derive Requirment relations in the file
            var deriveRequirementList = xml.Descendants().Where(a => a.Name == "DERIVE-REQUIREMENT");

            // if the list isnt null
            if(deriveRequirementList.Count() != 0)
            {
                // iterate through every relation
                foreach(var deriveRequirement in deriveRequirementList)
                {
                    // get the "derived from refs"
                    var referenceList = deriveRequirement.Descendants().Where(a => a.Name == "DERIVED-FROM-REF");
                    
                    if(referenceList.Count() != 0)
                    {
                        foreach(var reference in referenceList)
                        {
                            // if one derive-from ref equals the current requirement, get the count of the derived requirement refs
                            if(helper.navigateToNode(xml, reference.Value) == requirement)
                            {
                                count += deriveRequirement.Descendants().Where(a => a.Name == "DERIVED-REF").ToList().Count;
                                break;
                            }
                        }
                    }
                }
            }

            derivatives.Value = count;
            return derivatives;
        }
    }
}
