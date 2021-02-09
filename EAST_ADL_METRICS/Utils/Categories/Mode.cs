using EAST_ADL_METRICS.Models;
using EAST_ADL_METRICS.Utils.Searcher;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace EAST_ADL_METRICS.Utils.Categories
{
    public class Mode
    {
        private Global globalSearcher = new Global();
        private Helper helper = new Helper();

        private Metric allocatedFunctionTypes = new Metric
        {
            Name = "AllocatedFuntionTypes",
            Category = "Size",
            Type = "Mode",
            Nested = false
        };

        public Metric AllocatedFunctionTypes(XDocument xml, string elementName)
        {
            XElement mode = helper.navigateToNode(xml, elementName);
            List<XElement> functionTypeList = new List<XElement>();

            //get all function trigger in the file
            var functionTriggerList = globalSearcher.parentElementList(xml, "FUNCTION-TRIGGER");

            // if there are triggers
            if(functionTriggerList.Count() != 0)
            {
                // iterate through each function trigger
                foreach(var functionTrigger in functionTriggerList)
                {
                    // get the mode refs
                    var referenceList = functionTrigger.Descendants().Where(a => a.Name == "MODE-REF");

                    // iterate through every mode ref and check if it's the right mode
                    foreach(var reference in referenceList)
                    {
                        // if it's the right mode, add the functiontype into the functiontype list and break through the loop
                        if(helper.navigateToNode(xml, reference.Value) == mode)
                        {
                            var functionReference = functionTrigger.Descendants()
                                                                   .Where(a => a.Name == "FUNCTION-REF")
                                                                   .FirstOrDefault();
                            functionTypeList.Add(helper.navigateToNode(xml, functionReference.Value));
                            break;
                        }
                    }
                }

                functionTypeList = functionTypeList.Distinct().ToList();
            }

            allocatedFunctionTypes.Value = functionTypeList.Count();
            return allocatedFunctionTypes;
        }
    }
}
