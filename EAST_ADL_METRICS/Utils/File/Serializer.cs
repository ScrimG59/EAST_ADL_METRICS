using EAST_ADL_METRICS.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAST_ADL_METRICS.Utils.Parser
{
    public class Serializer
    {
        public void WriteResultsIntoFile(List<Metric> metricList, List<Rule> ruleList, Item selectedElement, string path)
        {
            using (StreamWriter file = File.CreateText($@"{path}"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, selectedElement);
                serializer.Serialize(file, metricList);
                serializer.Serialize(file, ruleList);
            }
        }
    }
}
