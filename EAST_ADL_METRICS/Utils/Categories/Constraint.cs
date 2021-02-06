using EAST_ADL_METRICS.Models;
using EAST_ADL_METRICS.Utils.Searcher;
using System.Linq;
using System.Xml.Linq;

namespace EAST_ADL_METRICS.Utils.Categories
{
    public class Constraint
    {
        private Global globalSearcher = new Global();
        private Local localSearcher = new Local();
        private Helper helper = new Helper();

        private Metric constraints  = new Metric
        {
            Name = "Constraints",
            Category = "Size",
            Type = "Constraint",
            Nested = false
        };

        public Metric Constraints(XDocument xml, string elementName)
        {
            int count = 0;
            XElement possibleArchitecture = helper.navigateToNode(xml, elementName);

            if (possibleArchitecture.Name.ToString().Contains("ARCHITECTURE"))
            {
                // gets the function type of this architecture
                XElement possibleFunctionType = helper.getFunctionTypeFromArchitecture(xml, possibleArchitecture);

                // first get all timing constraints in the model
                var periodicConstraintList = globalSearcher.parentElementList(xml, "PERIODIC-CONSTRAINT");

                // for each timing constraint do the following
                foreach (var periodicConstraint in periodicConstraintList)
                {
                    // gets the reference to the event function
                    string eventFunctionRef = periodicConstraint.Descendants()
                                                          .Where(a => a.Name == "EVENT-REF")
                                                          .FirstOrDefault()
                                                          .Value;

                    // gets the event function itself
                    XElement eventFunction = helper.navigateToNode(xml, eventFunctionRef);

                    // gets the reference to the function prototype
                    string functionPrototypeRef = eventFunction.Descendants()
                                                               .Where(a => a.Name == "FUNCTION-PROTOTYPE-CONTEXT-REF")
                                                               .FirstOrDefault()
                                                               .Value;

                    // gets the function prototype itself
                    XElement functionProtoType = helper.navigateToNode(xml, functionPrototypeRef);

                    // gets the function type reference
                    string functionTypeRef = helper.getTypeReference(functionProtoType);

                    // gets the function type itself
                    XElement functionType = helper.navigateToNode(xml, functionTypeRef);

                    // checks if it's a function type from an architecture or a "normal" prototype
                    if (possibleFunctionType != null)
                    {
                        // if the function type is equal to the function type the user wanted the metrics from
                        if (helper.getShortName(possibleFunctionType) == elementName)
                        {
                            count++;
                        }
                    }
                    else
                    {
                        // if the function type is equal to the function type the user wanted the metrics from
                        if (helper.getShortName(functionType) == elementName)
                        {
                            count++;
                        }
                    }
                }

                constraints.Value = count;
            }
            else
            {
                // first get all timing constraints in the model
                var periodicConstraintList = globalSearcher.parentElementList(xml, "PERIODIC-CONSTRAINT");

                // for each timing constraint do the following
                foreach (var periodicConstraint in periodicConstraintList)
                {
                    // gets the reference to the event function
                    string eventFunctionRef = periodicConstraint.Descendants()
                                                          .Where(a => a.Name == "EVENT-REF")
                                                          .FirstOrDefault()
                                                          .Value;

                    // gets the event function itself
                    XElement eventFunction = helper.navigateToNode(xml, eventFunctionRef);

                    // gets the reference to the function prototype
                    string functionPrototypeRef = eventFunction.Descendants()
                                                               .Where(a => a.Name == "FUNCTION-PROTOTYPE-CONTEXT-REF")
                                                               .FirstOrDefault()
                                                               .Value;

                    // gets the function prototype itself
                    XElement functionProtoType = helper.navigateToNode(xml, functionPrototypeRef);

                    // gets the function type reference
                    string functionTypeRef = helper.getTypeReference(functionProtoType);

                    // gets the function type itself
                    XElement functionType = helper.navigateToNode(xml, functionTypeRef);

                    // checks if it's a function type from an architecture or a "normal" prototype
                    if (functionType != null)
                    {
                        // if the function type is equal to the function type the user wanted the metrics from
                        if (helper.getShortName(functionType) == elementName)
                        {
                            count++;
                        }
                    }
                    else
                    {
                        // if the function type is equal to the function type the user wanted the metrics from
                        if (helper.getShortName(functionType) == elementName)
                        {
                            count++;
                        }
                    }
                }

                constraints.Value = count;
            }

            return constraints;
        }
    }
}
