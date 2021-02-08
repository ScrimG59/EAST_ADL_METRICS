using EAST_ADL_METRICS.Models;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace EAST_ADL_METRICS.Utils.Categories
{
    public class Wrapper
    {
        private FunctionType functionType = new FunctionType();
        private Package package = new Package();
        private Requirement requirement = new Requirement();
        private Constraint constraint = new Constraint();
        private Architecture architecture = new Architecture();
        private Port port = new Port();
        private Extension extension = new Extension();
        private Rules rules= new Rules();
        private Mode mode = new Mode();

        public List<Metric> calculateMetrics(XDocument xml, Item item)
        {
            List<Metric> metricList = new List<Metric>();

            if(item.Type.Contains("FUNCTION-TYPE"))
            {
                metricList.Add(functionType.Parts_fct(xml, item.Name));
                metricList.Add(functionType.Parts_fct_tc(xml, item.Name));
                metricList.Add(functionType.NestingLevels_fct(xml, item.Name));
                metricList.Add(functionType.Ports_fct(xml, item.Name));
                metricList.Add(functionType.Connectors_fct(xml, item.Name));
                metricList.Add(constraint.Constraints(xml, item.Name));
                metricList.Add(port.FunctionPorts(xml, item.Name));
                metricList.Add(port.FunctionFlowPorts(xml, item.Name));
                metricList.Add(port.FunctionPowerPorts(xml, item.Name));
                metricList.Add(port.FunctionClientServerPorts(xml, item.Name));
                metricList.Add(port.Operations(xml, item.Name));
                metricList.Add(port.Portgroups(xml, item.Name));
                metricList.Add(port.PortGroupSize(xml, item.Name));
                metricList.Add(extension.VariableElements(xml));
                metricList.Add(extension.UseCaseSatisfactionRatio(xml));
                metricList.Add(extension.FunctionalQualityReqtsRatio(xml));
                metricList.Add(extension.VVRatio(xml));
                Console.WriteLine($"PARTS: {metricList[0].Value}");
                Console.WriteLine($"PARTS_TC: {metricList[1].Value}");
                Console.WriteLine($"NESTINGLEVELS: {metricList[2].Value}");
                Console.WriteLine($"PORTS: {metricList[3].Value}");
                Console.WriteLine($"CONNECTORS: {metricList[4].Value}");
                Console.WriteLine($"CONSTRAINTS: {metricList[5].Value}");
                Console.WriteLine($"FunctionPorts: {metricList[6].Value}");
                Console.WriteLine($"FunctionFlowPorts: {metricList[7].Value}");
                Console.WriteLine($"FunctionPowerPorts: {metricList[8].Value}");
                Console.WriteLine($"FunctionClientServerPorts: {metricList[9].Value}");
                Console.WriteLine($"Operations: {metricList[10].Value}");
                Console.WriteLine($"PortGroups: {metricList[11].Value}");
                Console.WriteLine($"PortGroupSize: {metricList[12].Value}");
                Console.WriteLine($"VariableElement: {metricList[13].Value}");
                Console.WriteLine($"UseCaseSatisFaction: {metricList[14].Value}");
                Console.WriteLine($"FunctionalQualityReqtsRatio: {metricList[15].Value}");
                Console.WriteLine($"VVRatio: {metricList[16].Value}");
            }
            else if(item.Type.Equals("EA-PACKAGE"))
            {
                metricList.Add(package.Functions_pckg(xml, item.Name));
                metricList.Add(package.Functions_pckg_tc(xml, item.Name));
                metricList.Add(package.Reqts_pckg(xml, item.Name));
                metricList.Add(package.Reqts_pckg_tc(xml, item.Name));
                metricList.Add(extension.VariableElements(xml));
                metricList.Add(extension.UseCaseSatisfactionRatio(xml));
                metricList.Add(extension.FunctionalQualityReqtsRatio(xml));
                metricList.Add(extension.VVRatio(xml));
                Console.WriteLine($"FUNCTIONS_pckg: {metricList[0].Value}");
                Console.WriteLine($"FUNCTIONS_pckg_tc: {metricList[1].Value}");
                Console.WriteLine($"Reqts_pckg: {metricList[2].Value}");
                Console.WriteLine($"Reqts_pckg_tc: {metricList[3].Value}");
                Console.WriteLine($"VariableElement: {metricList[4].Value}");
                Console.WriteLine($"UseCaseSatisFaction: {metricList[5].Value}");
                Console.WriteLine($"FunctionalQualityReqtsRatio: {metricList[6].Value}");
                Console.WriteLine($"VVRatio: {metricList[7].Value}");
            }
            else if (item.Type.Contains("REQUIREMENT"))
            {
                metricList.Add(requirement.SubReqts(xml, item.Name));
                metricList.Add(requirement.NestingLevel(xml, item.Name));
                metricList.Add(requirement.Satisfiers(xml, item.Name));
                metricList.Add(requirement.Verifiers(xml, item.Name));
                metricList.Add(requirement.Derivatives(xml, item.Name));
                metricList.Add(extension.VariableElements(xml));
                metricList.Add(extension.UseCaseSatisfactionRatio(xml));
                metricList.Add(extension.FunctionalQualityReqtsRatio(xml));
                metricList.Add(extension.VVRatio(xml));
                Console.WriteLine($"SubReqts: {metricList[0].Value}");
                Console.WriteLine($"NestingLevel: {metricList[1].Value}");
                Console.WriteLine($"Satisfiers: {metricList[2].Value}");
                Console.WriteLine($"Verifiers: {metricList[3].Value}");
                Console.WriteLine($"Derivatives: {metricList[4].Value}");
                Console.WriteLine($"Variable Elements: {metricList[5].Value}");
                Console.WriteLine($"UseCaseSatisfaction: {metricList[6].Value}");
                Console.WriteLine($"FunctionalQualityReqtsRatio: {metricList[7].Value}");
                Console.WriteLine($"VVRatio: {metricList[8].Value}");
            }
            else if (item.Type.Contains("ANALYSIS-ARCHITECTURE"))
            {
                metricList.Add(architecture.Parts_arch(xml, item.Name));
                metricList.Add(architecture.Parts_arch_tc(xml, item.Name));
                metricList.Add(architecture.NestingLevels_arch(xml, item.Name));
                metricList.Add(architecture.Ports_arch(xml, item.Name));
                metricList.Add(architecture.Connectors_arch(xml, item.Name));
                metricList.Add(constraint.Constraints(xml, item.Name));
                metricList.Add(port.FunctionPorts(xml, item.Name));
                metricList.Add(port.FunctionFlowPorts(xml, item.Name));
                metricList.Add(port.FunctionPowerPorts(xml, item.Name));
                metricList.Add(port.FunctionClientServerPorts(xml, item.Name));
                metricList.Add(port.Operations(xml, item.Name));
                metricList.Add(port.Portgroups(xml, item.Name));
                metricList.Add(port.PortGroupSize(xml, item.Name));
                metricList.Add(extension.VariableElements(xml));
                metricList.Add(extension.UseCaseSatisfactionRatio(xml));
                metricList.Add(extension.FunctionalQualityReqtsRatio(xml));
                metricList.Add(extension.VVRatio(xml));
                Console.WriteLine($"PARTS: {metricList[0].Value}");
                Console.WriteLine($"PARTS_TC: {metricList[1].Value}");
                Console.WriteLine($"NESTINGLEVELS: {metricList[2].Value}");
                Console.WriteLine($"PORTS: {metricList[3].Value}");
                Console.WriteLine($"CONNECTORS: {metricList[4].Value}");
                Console.WriteLine($"CONSTRAINTS: {metricList[5].Value}");
                Console.WriteLine($"FunctionPorts: {metricList[6].Value}");
                Console.WriteLine($"FunctionFlowPorts: {metricList[7].Value}");
                Console.WriteLine($"FunctionFlowPorts: {metricList[8].Value}");
                Console.WriteLine($"FunctionClientServerPorts: {metricList[9].Value}");
                Console.WriteLine($"Operations: {metricList[10].Value}");
                Console.WriteLine($"Portgroups: {metricList[11].Value}");
                Console.WriteLine($"PortgroupSize: {metricList[12].Value}");
                Console.WriteLine($"VariableElement: {metricList[13].Value}");
                Console.WriteLine($"UseCaseSatisFaction: {metricList[14].Value}");
                Console.WriteLine($"FunctionalQualityReqtsRatio: {metricList[15].Value}");
                Console.WriteLine($"VVRatio: {metricList[16].Value}");
            }
            else if (item.Type.Equals("HARDWARE-DESIGN-ARCHITECTURE"))
            {
                metricList.Add(architecture.Parts_arch(xml, item.Name));
                metricList.Add(architecture.Parts_arch_tc(xml, item.Name));
                metricList.Add(architecture.NestingLevels_arch(xml, item.Name));
                metricList.Add(architecture.Ports_arch(xml, item.Name));
                metricList.Add(architecture.Connectors_arch(xml, item.Name));
                metricList.Add(architecture.FunctionNodeAllocation(xml, item.Name));
                metricList.Add(port.HardwarePorts(xml, item.Name));
                metricList.Add(extension.VariableElements(xml));
                metricList.Add(extension.UseCaseSatisfactionRatio(xml));
                metricList.Add(extension.FunctionalQualityReqtsRatio(xml));
                metricList.Add(extension.VVRatio(xml));
                Console.WriteLine($"Parts: {metricList[0].Value}");
                Console.WriteLine($"Parts_tc: {metricList[1].Value}");
                Console.WriteLine($"NestingLevels: {metricList[2].Value}");
                Console.WriteLine($"Ports: {metricList[3].Value}");
                Console.WriteLine($"Connectors: {metricList[4].Value}");
                Console.WriteLine($"FunctionNodeAllocation: {metricList[5].Value}");
                Console.WriteLine($"HardwarePorts: {metricList[6].Value}");
                Console.WriteLine($"VariableElement: {metricList[7].Value}");
                Console.WriteLine($"UseCaseSatisFaction: {metricList[8].Value}");
                Console.WriteLine($"FunctionalQualityReqtsRatio: {metricList[9].Value}");
                Console.WriteLine($"VVRatio: {metricList[10].Value}");
            }
            else if (item.Type.Contains("DESIGN-ARCHITECTURE"))
            {
                metricList.Add(architecture.Parts_arch(xml, item.Name));
                metricList.Add(architecture.Parts_arch_tc(xml, item.Name));
                metricList.Add(architecture.NestingLevels_arch(xml, item.Name));
                metricList.Add(architecture.Ports_arch(xml, item.Name));
                metricList.Add(architecture.Connectors_arch(xml, item.Name));
                metricList.Add(constraint.Constraints(xml, item.Name));
                metricList.Add(architecture.FunctionNodeAllocation(xml, item.Name));
                metricList.Add(port.FunctionPorts(xml, item.Name));
                metricList.Add(port.FunctionFlowPorts(xml, item.Name));
                metricList.Add(port.FunctionPowerPorts(xml, item.Name));
                metricList.Add(port.FunctionClientServerPorts(xml, item.Name));
                metricList.Add(port.Operations(xml, item.Name));
                metricList.Add(port.Portgroups(xml, item.Name));
                metricList.Add(port.PortGroupSize(xml, item.Name));
                metricList.Add(extension.VariableElements(xml));
                metricList.Add(extension.UseCaseSatisfactionRatio(xml));
                metricList.Add(extension.FunctionalQualityReqtsRatio(xml));
                metricList.Add(extension.VVRatio(xml));
                Console.WriteLine($"PARTS: {metricList[0].Value}");
                Console.WriteLine($"PARTS_TC: {metricList[1].Value}");
                Console.WriteLine($"NESTINGLEVELS: {metricList[2].Value}");
                Console.WriteLine($"PORTS: {metricList[3].Value}");
                Console.WriteLine($"CONNECTORS: {metricList[4].Value}");
                Console.WriteLine($"CONSTRAINTS: {metricList[5].Value}");
                Console.WriteLine($"FUNCTIONNODEALLOCATION: {metricList[6].Value}");
                Console.WriteLine($"FunctionPorts: {metricList[7].Value}");
                Console.WriteLine($"FunctionFlowPorts: {metricList[8].Value}");
                Console.WriteLine($"FunctionFlowPorts: {metricList[9].Value}");
                Console.WriteLine($"FunctionClientServerPorts: {metricList[10].Value}");
                Console.WriteLine($"Operations: {metricList[11].Value}");
                Console.WriteLine($"Portgroups: {metricList[12].Value}");
                Console.WriteLine($"PortgroupSize: {metricList[13].Value}");
                Console.WriteLine($"VariableElement: {metricList[14].Value}");
                Console.WriteLine($"UseCaseSatisFaction: {metricList[15].Value}");
                Console.WriteLine($"FunctionalQualityReqtsRatio: {metricList[16].Value}");
                Console.WriteLine($"VVRatio: {metricList[17].Value}");
            }
            else if (item.Type.Contains("MODE"))
            {
                metricList.Add(mode.AllocatedFunctionTypes(xml, item.Name));
                metricList.Add(extension.VariableElements(xml));
                metricList.Add(extension.UseCaseSatisfactionRatio(xml));
                metricList.Add(extension.FunctionalQualityReqtsRatio(xml));
                metricList.Add(extension.VVRatio(xml));
            }

            return metricList;
        }

        public List<Rule> calcualteRules(XDocument xml)
        {
            List<Rule> ruleList = new List<Rule>();

            ruleList.Add(rules.PortConnectorAllocation(xml));
            ruleList.Add(rules.Unverified(xml));
            ruleList.Add(rules.ResidualAnomaly(xml));
            ruleList.Add(rules.Reference(xml));
            ruleList.Add(rules.EventChainPair(xml));
            ruleList.Add(rules.ModeAllocation(xml));

            return ruleList;
        }
    }
}
