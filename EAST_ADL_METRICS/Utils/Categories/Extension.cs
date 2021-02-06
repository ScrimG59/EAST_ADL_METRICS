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
            Type = "Global",
            Nested = false
        };

        private Metric functionalQualityReqtsRatio = new Metric
        {
            Name = "FunctionalQualityReqtsRatio",
            Category = "Complexity",
            Type = "Global",
            Nested = false
        };

        private Metric useCaseSatisfaction = new Metric
        {
            Name = "UseCaseSatisfaction",
            Category = "Size",
            Type = "Global",
            Nested = false
        };

        private Metric vvRatio = new Metric
        {
            Name = "VVRatio",
            Category = "Complexity",
            Type = "Global",
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
                double result = requirements.Count() / qualityRequirements.Count();
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

        public Metric UseCaseSatisfaction(XDocument xml)
        {
            int count = 0;

            var useCases = globalSearcher.parentElementList(xml, "USE-CASE");

            if(useCases.Count() == 0)
            {
                useCaseSatisfaction.Value = count;
                return useCaseSatisfaction;
            }
            else
            {
                var reference = globalSearcher.parentElementList(xml, "SATISFIED-USE-CASE-REF");

                if(reference.Count() == 0)
                {
                    useCaseSatisfaction.Value = count;
                    return useCaseSatisfaction;
                }

                foreach (var useCase in useCases)
                {
                    foreach(var r in reference)
                    {
                        if(useCase == helper.navigateToNode(xml, r.Value))
                        {
                            count++;
                        }
                    }
                }
            }

            useCaseSatisfaction.Value = count;

            return useCaseSatisfaction;
        }

        public Metric VVRatio(XDocument xml)
        {
            int ratio = 0;
            int vvCaseOnly = 0;
            int vvBoth = 0;

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
                    vvCaseOnly++;
                }
                else
                {
                    vvBoth++;
                }
            }

            if(vvBoth == 0)
            {
                vvRatio.Value = ratio;
                return vvRatio;
            }

            ratio = vvCaseOnly / vvBoth;
            vvRatio.Value = ratio;
            return vvRatio;
        }
    }
}
