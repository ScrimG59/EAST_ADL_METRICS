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
    public class Wrapper
    {
        private FunctionType functionType = new FunctionType();
        private Package package = new Package();
        private Requirement requirement = new Requirement();
        private Constraint constraint = new Constraint();
        private Architecture architecture = new Architecture();
        private Port port = new Port();
        private Extension extension = new Extension();

        public List<Metric> calculateMetrics(XDocument xml, Item item)
        {
            List<Metric> metricList = new List<Metric>();

            if(item.Type.Contains("TYPE") || item.Type.Contains("ANALYSIS-ARCHITECTURE"))
            {
                metricList.Add(functionType.Parts_fct(xml, item.Name));
                metricList.Add(functionType.Parts_fct_tc(xml, item.Name));
                metricList.Add(functionType.NestingLevels_fct(xml, item.Name));
                metricList.Add(functionType.Ports_fct(xml, item.Name));
                metricList.Add(functionType.Connectors_fct(xml, item.Name));
                metricList.Add(constraint.Constraints(xml, item.Name));
                metricList.Add(port.FunctionPorts(xml, item.Name));
                metricList.Add(port.FunctionFlowPorts(xml, item.Name));
                metricList.Add(port.FunctionClientServerPorts(xml, item.Name));
                metricList.Add(port.Operations(xml, item.Name));
                metricList.Add(port.HardwarePorts(xml, item.Name));
                metricList.Add(port.Portgroups(xml, item.Name));
                metricList.Add(port.PortgroupSize(xml, item.Name));
                metricList.Add(extension.VariableElements(xml));
                metricList.Add(extension.UseCaseSatisfaction(xml));
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
                Console.WriteLine($"FunctionClientServerPorts: {metricList[8].Value}");
                Console.WriteLine($"Operations: {metricList[9].Value}");
                Console.WriteLine($"HardwarePorts: {metricList[10].Value}");
                Console.WriteLine($"Portgroups: {metricList[11].Value}");
                Console.WriteLine($"PortgroupSize: {metricList[12].Value}");
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
                Console.WriteLine(metricList[0].Value);
                Console.WriteLine(metricList[1].Value);
                Console.WriteLine(metricList[2].Value);
                Console.WriteLine(metricList[3].Value);
            }
            else if (item.Type.Contains("REQUIREMENT"))
            {
                metricList.Add(requirement.SubReqts(xml, item.Name));
                //metricList.Add(requirement.NestingLevel(xml, item.Name));
                metricList.Add(requirement.Satisfiers(xml, item.Name));
                metricList.Add(requirement.Verifiers(xml, item.Name));
                metricList.Add(requirement.Derivatives(xml, item.Name));
                Console.WriteLine(metricList[0].Value);
                Console.WriteLine(metricList[1].Value);
                Console.WriteLine(metricList[2].Value);
                Console.WriteLine(metricList[3].Value);
                //Console.WriteLine(metricList[4].Value);
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
                metricList.Add(port.FunctionClientServerPorts(xml, item.Name));
                metricList.Add(port.Operations(xml, item.Name));
                metricList.Add(port.HardwarePorts(xml, item.Name));
                metricList.Add(port.Portgroups(xml, item.Name));
                metricList.Add(port.PortgroupSize(xml, item.Name));
                Console.WriteLine($"PARTS: {metricList[0].Value}");
                Console.WriteLine($"PARTS_TC: {metricList[1].Value}");
                Console.WriteLine($"NESTINGLEVELS: {metricList[2].Value}");
                Console.WriteLine($"PORTS: {metricList[3].Value}");
                Console.WriteLine($"CONNECTORS: {metricList[4].Value}");
                Console.WriteLine($"CONSTRAINTS: {metricList[5].Value}");
                Console.WriteLine($"FUNCTIONNODEALLOCATION: {metricList[6].Value}");
                Console.WriteLine($"FunctionPorts: {metricList[7].Value}");
                Console.WriteLine($"FunctionFlowPorts: {metricList[8].Value}");
                Console.WriteLine($"FunctionClientServerPorts: {metricList[9].Value}");
                Console.WriteLine($"Operations: {metricList[10].Value}");
                Console.WriteLine($"HardwarePorts: {metricList[11].Value}");
                Console.WriteLine($"Portgroups: {metricList[12].Value}");
                Console.WriteLine($"PortgroupSize: {metricList[13].Value}");
            }

            return metricList;
        }
    }
}
