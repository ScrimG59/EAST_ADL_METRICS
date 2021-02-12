using EAST_ADL_METRICS.Models;
using EAST_ADL_METRICS.Utils.Searcher;
using System.Linq;
using System.Xml.Linq;

namespace EAST_ADL_METRICS.Utils.Categories
{
    public class Constraint
    {
        private Global globalSearcher = new Global();
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

                //then get all safety constraints in the model
                var safetyConstraintList = globalSearcher.parentElementList(xml, "SAFETY-CONSTRAINT");

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
                                                               .Where(a => a.Name == "FUNCTION-PROTOTYPE-TARGET-REF")
                                                               .FirstOrDefault()
                                                               .Value;

                    // gets the function prototype itself
                    XElement functionProtoType = helper.navigateToNode(xml, functionPrototypeRef);

                    // gets the function type reference
                    string functionTypeRef = helper.getTypeReference(functionProtoType);

                    // gets the function type itself
                    XElement functionType = helper.navigateToNode(xml, functionTypeRef);

                    // if the function type is equal to the function type the user wanted the metrics from
                    if (possibleFunctionType == functionType)
                    {
                        count++;
                    }
                }

                // for each safety constraint do the following
                foreach (var safetyConstraint in safetyConstraintList)
                {
                    // get all refs to FaultFailure-elements
                    var constrainedFaultFailureRefList = safetyConstraint.Descendants()
                                                                         .Where(a => a.Name == "CONSTRAINED-FAULT-FAILURE-REF");

                    foreach(var constrainedFaultFailureRef in constrainedFaultFailureRefList)
                    {
                        // get the FaultFailureElement
                        var constrainedFaultFailure = helper.navigateToNode(xml, constrainedFaultFailureRef.Value);

                        // get the error-model-prototype ref
                        var errorModelPrototypeRefList = constrainedFaultFailure.Descendants()
                                                                         .Where(a => a.Name == "ERROR-MODEL-PROTOTYPE-REF");

                        foreach(var errorModelPrototypeRef in errorModelPrototypeRefList)
                        {
                            // get the ErrorModelPrototype
                            var errorModelPrototype = helper.navigateToNode(xml, errorModelPrototypeRef.Value);

                            // get the ErrorModelType of the given ErrorModelPrototype
                            var errorModelType = helper.navigateToNode(xml, helper.getTypeReference(errorModelPrototype));

                            // get the target and the hardware target
                            var targetRef = errorModelType.Descendants().Where(a => a.Name == "TARGET-REF").FirstOrDefault();
                            if(targetRef != null)
                            {
                                var target = helper.navigateToNode(xml, targetRef.Value);
                                // if the function type is equal to the function type the user wanted the metrics from
                                if (target == possibleFunctionType)
                                {
                                    count++;
                                }
                            }
                            var hwTargetRef = errorModelType.Descendants().Where(a => a.Name == "HW-TARGET-REF").FirstOrDefault();
                            if(hwTargetRef != null)
                            {
                                var hwTarget = helper.navigateToNode(xml, hwTargetRef.Value);
                                // if the function type is equal to the function type the user wanted the metrics from
                                if (hwTarget == possibleFunctionType)
                                {
                                    count++;
                                }
                            }  
                        }
                    }
                }

                constraints.Value = count;
            }
            else
            {
                // first get all timing constraints in the model
                var periodicConstraintList = globalSearcher.parentElementList(xml, "PERIODIC-CONSTRAINT");

                // then get all safety constraint in the model 
                var safetyConstraintList = globalSearcher.parentElementList(xml, "SAFETY-CONSTRAINT");

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
                                                               .Where(a => a.Name == "FUNCTION-PROTOTYPE-TARGET-REF")
                                                               .FirstOrDefault()
                                                               .Value;

                    // gets the function prototype itself
                    XElement functionProtoType = helper.navigateToNode(xml, functionPrototypeRef);

                    // gets the function type reference
                    string functionTypeRef = helper.getTypeReference(functionProtoType);

                    // gets the function type itself
                    XElement functionType = helper.navigateToNode(xml, functionTypeRef);


                    // if the function type is equal to the function type the user wanted the metrics from
                    if (helper.getShortName(functionType) == elementName)
                    {
                        count++;
                    }
                }

                // for each safety constraint do the following
                foreach (var safetyConstraint in safetyConstraintList)
                {
                    // get all refs to FaultFailure-elements
                    var constrainedFaultFailureRefList = safetyConstraint.Descendants()
                                                                         .Where(a => a.Name == "CONSTRAINED-FAULT-FAILURE-REF");

                    foreach (var constrainedFaultFailureRef in constrainedFaultFailureRefList)
                    {
                        // get the FaultFailureElement
                        var constrainedFaultFailure = helper.navigateToNode(xml, constrainedFaultFailureRef.Value);

                        // get the error-model-prototype ref
                        var errorModelPrototypeRefList = constrainedFaultFailure.Descendants()
                                                                         .Where(a => a.Name == "ERROR-MODEL-PROTOTYPE-REF");

                        foreach (var errorModelPrototypeRef in errorModelPrototypeRefList)
                        {
                            // get the ErrorModelPrototype
                            var errorModelPrototype = helper.navigateToNode(xml, errorModelPrototypeRef.Value);

                            // get the ErrorModelType of the given ErrorModelPrototype
                            var errorModelType = helper.navigateToNode(xml, helper.getTypeReference(errorModelPrototype));

                            // get the target and the hardware target
                            var targetRef = errorModelType.Descendants().Where(a => a.Name == "TARGET-REF").FirstOrDefault();
                            if(targetRef != null)
                            {
                                var target = helper.navigateToNode(xml, targetRef.Value);
                                // if the function type is equal to the function type the user wanted the metrics from
                                if (helper.getShortName(target) == elementName)
                                {
                                    count++;
                                }
                            }
                            var hwTargetRef = errorModelType.Descendants().Where(a => a.Name == "HW-TARGET-REF").FirstOrDefault();
                            if(hwTargetRef != null)
                            {
                                var hwTarget = helper.navigateToNode(xml, hwTargetRef.Value);
                                // if the function type is equal to the function type the user wanted the metrics from
                                if (helper.getShortName(hwTarget) == elementName)
                                {
                                    count++;
                                }
                            } 
                        }
                    }
                }

                constraints.Value = count;
            }

            return constraints;
        }
    }
}
