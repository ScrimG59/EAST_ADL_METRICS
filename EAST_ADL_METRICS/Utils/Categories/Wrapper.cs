using EAST_ADL_METRICS.Models;
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

        public List<Metric> calculateMetrics(XDocument xml, bool mode, Item item)
        {
            List<Metric> metricList = new List<Metric>();

            if (mode)
            {
                // first adding every package metric
                metricList.Add(package.Functions_pckg(xml, mode));
                metricList.Add(package.Functions_pckg_tc(xml, mode));
                metricList.Add(package.Reqts_pckg(xml, mode));
                metricList.Add(package.Reqts_pckg_tc(xml, mode));

                // second adding every functiontype metric
                metricList.Add(functionType.Parts_fct(xml, mode));
                metricList.Add(functionType.Parts_fct_tc(xml, mode));
                metricList.Add(functionType.NestingLevels_fct(xml, mode));
                metricList.Add(functionType.Ports_fct(xml, mode));
                metricList.Add(functionType.Connectors_fct(xml, mode));

                //third adding every requirement metric

                // fourth adding every constraint metric
            }
            else if(!mode && item.Type.Contains("TYPE"))
            {
                metricList.Add(functionType.Parts_fct(xml, mode, item.Name));
                metricList.Add(functionType.Parts_fct_tc(xml, mode, item.Name));
                metricList.Add(functionType.NestingLevels_fct(xml, mode, item.Name));
                metricList.Add(functionType.Ports_fct(xml, mode, item.Name));
                metricList.Add(functionType.Connectors_fct(xml, mode, item.Name));
                Console.WriteLine(metricList[0].MaxValue);
                Console.WriteLine(metricList[0].MinValue);
                Console.WriteLine(metricList[0].AvgValue);
                Console.WriteLine(metricList[1].MaxValue);
                Console.WriteLine(metricList[2].MaxValue);
                Console.WriteLine(metricList[3].MaxValue);
                Console.WriteLine(metricList[4].MaxValue);

            }
            else if(!mode && item.Type.Equals("EA-PACKAGE"))
            {
                metricList.Add(package.Functions_pckg(xml, mode, item.Name));
                metricList.Add(package.Functions_pckg_tc(xml, mode, item.Name));
                metricList.Add(package.Reqts_pckg(xml, mode, item.Name));
                metricList.Add(package.Reqts_pckg_tc(xml, mode, item.Name));
                Console.WriteLine(metricList[0].MaxValue);
                Console.WriteLine(metricList[0].MinValue);
                Console.WriteLine(metricList[0].AvgValue);
                Console.WriteLine(metricList[1].MaxValue);
                Console.WriteLine(metricList[2].MaxValue);
                Console.WriteLine(metricList[3].MaxValue);
            }

            return metricList;
        }
    }
}
