﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAST_ADL_METRICS.Models
{
    public class Metric
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public double AvgValue { get; set; }
        public Boolean Nested { get; set; }
    }
}