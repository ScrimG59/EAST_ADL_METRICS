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
            var elements = xml.Descendants().Where(a => a.Name == "VARIABLE-ELEMENT");

            if(elements != null)
            {
                variableElements.Value = elements.Count();
            }
            else
            {
                variableElements.Value = 0;
            }

            return variableElements;
        }

        public Metric FunctionalQualityReqtsRatio(XDocument xml)
        {
            var requirements = xml.Descendants().Where(a => a.Name == "REQUIREMENT");

            var qualityRequirements = xml.Descendants().Where(a => a.Name == "QUALITY-REQUIREMENT");

            if(requirements != null && qualityRequirements != null && qualityRequirements.Count() != 0)
            {
                double result = requirements.Count() / qualityRequirements.Count();
                functionalQualityReqtsRatio.Value = result;
            }
            else if(requirements == null)
            {
                functionalQualityReqtsRatio.Value = 0;
            }
            else if(qualityRequirements == null)
            {
                functionalQualityReqtsRatio.Value = requirements.Count();
            }

            return functionalQualityReqtsRatio;
        }

        public Metric UseCaseSatisfaction(XDocument xml)
        {
            int count = 0;

            var useCases = xml.Descendants().Where(a => a.Name == "USE-CASE");

            if(useCases == null)
            {
                useCaseSatisfaction.Value = count;
                return useCaseSatisfaction;
            }
            else
            {
                var reference = xml.Descendants().Where(a => a.Name == "SATISFIED-USE-CASE-REF");

                if(reference == null)
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

            if(verifyTags == null)
            {
                vvRatio.Value = ratio;
                return vvRatio;
            }

            foreach(var verifyTag in verifyTags)
            {
                var verifiedByProcedureRef = verifyTag.Descendants().Where(a => a.Name == "VERIFIED-BY-PROCEDURE-REF");
                
                if(verifiedByProcedureRef == null)
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
