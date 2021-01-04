using EAST_ADL_METRICS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAST_ADL_METRICS.Utils.Categories
{
    public class Constraint
    {
        private Searcher.Searcher searcher = new Searcher.Searcher();
        private Metric parts = new Metric
        {
            Name = "Parts_fct",
            Category = "Size",
            Type = "FunctionType",
            Nested = false
        };

        private Metric parts_tc = new Metric
        {
            Name = "Parts_fct_tc",
            Category = "Size",
            Type = "FunctionType",
            Nested = true
        };

        private Metric nestingLevels = new Metric
        {
            Name = "NestingLevels_fct",
            Category = "Size",
            Type = "FunctionType",
            Nested = false
        };

        private Metric ports = new Metric
        {
            Name = "Ports_fct",
            Category = "Size",
            Type = "FunctionType",
            Nested = false
        };

        private Metric connectors = new Metric
        {
            Name = "Connectors_fct",
            Category = "Size",
            Type = "FunctionType",
            Nested = false
        };
    }
}
