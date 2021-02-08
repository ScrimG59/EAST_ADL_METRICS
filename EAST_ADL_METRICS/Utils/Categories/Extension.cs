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
    public class Extension
    {
        private Global globalSearcher = new Global();
        private Local localSearcher = new Local();
        private Helper helper = new Helper();

        private Metric variableElements = new Metric
        {
            Name = "VariableElements",
            Category = "Size",
            Type = "Extension",
            Nested = false
        };

        private Metric functionalQualityReqtsRatio = new Metric
        {
            Name = "FunctionalQualityReqtsRatio",
            Category = "Complexity",
            Type = "Extension",
            Nested = false
        };

        private Metric useCaseSatisfactionRatio = new Metric
        {
            Name = "UseCaseSatisfactionRatio",
            Category = "Complexity",
            Type = "Extension",
            Nested = false
        };

        private Metric vvRatio = new Metric
        {
            Name = "VVRatio",
            Category = "Complexity",
            Type = "Extension",
            Nested = false
        };

        public Metric VariableElements(XDocument xml)
        {
            var elements = globalSearcher.parentElementList(xml, "VARIABLE-ELEMENT");
            
            variableElements.Value = elements.Count();

            return variableElements;
        }

        public Metric FunctionalQualityReqtsRatio(XDocument xml)
        {
            var requirements = globalSearcher.parentElementList(xml, "REQUIREMENT");

            var qualityRequirements = globalSearcher.parentElementList(xml, "QUALITY-REQUIREMENT");

            if(qualityRequirements.Count() != 0)
            {
                double result = (double) requirements.Count() / (double) qualityRequirements.Count();
                functionalQualityReqtsRatio.Value = result;
            }
            else if(requirements.Count() == 0)
            {
                functionalQualityReqtsRatio.Value = 0;
            }
            else if(qualityRequirements.Count() == 0)
            {
                functionalQualityReqtsRatio.Value = requirements.Count();
            }

            return functionalQualityReqtsRatio;
        }

        public Metric UseCaseSatisfactionRatio(XDocument xml)
        {
            int satisfiedCount = 0;

            var useCases = globalSearcher.parentElementList(xml, "USE-CASE");

            if(useCases.Count() == 0)
            {
                useCaseSatisfactionRatio.Value = satisfiedCount;
                return useCaseSatisfactionRatio;
            }
            else
            {
                // get all satisfied-use-case references
                var reference = globalSearcher.parentElementList(xml, "SATISFIED-USE-CASE-REF");

                // if there is no reference, then set the value to 0 (satisfiedCount)
                if(reference.Count() == 0)
                {
                    useCaseSatisfactionRatio.Value = satisfiedCount;
                    return useCaseSatisfactionRatio;
                }

                // for each use case go through each satisfied-use-case reference and look if the reference is equal to the use case
                foreach (var useCase in useCases)
                {
                    foreach(var r in reference)
                    {
                        // if it's equal, increment the satisfiedCount and break out of the nested loop
                        // since we found the element that satisfies the usecase
                        if(useCase == helper.navigateToNode(xml, r.Value))
                        {
                            satisfiedCount++;
                            break;
                        }
                    }
                }
            }

            useCaseSatisfactionRatio.Value = (double) satisfiedCount / (double) useCases.Count();

            return useCaseSatisfactionRatio;
        }

        public Metric VVRatio(XDocument xml)
        {
            double ratio = 0;
            double vvCase = 0;
            double vvBoth = 0;

            var verifyTags = xml.Descendants().Where(a => a.Name == "VERIFY");

            if(verifyTags.Count() == 0)
            {
                vvRatio.Value = ratio;
                return vvRatio;
            }

            foreach(var verifyTag in verifyTags)
            {
                var verifiedByProcedureRef = verifyTag.Descendants().Where(a => a.Name == "VERIFIED-BY-PROCEDURE-REF");
                
                if(verifiedByProcedureRef.Count() == 0)
                {
                    vvCase++;
                }
                else
                {
                    vvCase++;
                    vvBoth++;
                }
            }

            if(vvBoth == 0)
            {
                vvRatio.Value = ratio;
                return vvRatio;
            }

            ratio = (double) vvBoth / (double) vvCase;
            vvRatio.Value = ratio;
            return vvRatio;
        }
    }
}
