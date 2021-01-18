using EAST_ADL_METRICS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EAST_ADL_METRICS.Utils.Categories
{
    public class Package
    {
        private Searcher.Searcher searcher = new Searcher.Searcher();
        private Metric functions = new Metric
        {
            Name = "Functions_pckg",
            Category = "Size",
            Type = "Package",
            Nested = false
        };

        private Metric functions_tc = new Metric
        {
            Name = "Functions_pckg",
            Category = "Size",
            Type = "Package",
            Nested = true
        };

        private Metric reqts = new Metric
        {
            Name = "Reqts_pckg",
            Category = "Size",
            Type = "Package",
            Nested = false
        };

        private Metric reqts_tc = new Metric
        {
            Name = "Reqts_pckg",
            Category = "Size",
            Type = "Package",
            Nested = true
        };

        /// <summary>
        /// FunctionType package metric
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public Metric Functions_pckg(XDocument xml)
        {
            var parentList = searcher.parentElementList(xml, "EA-PACKAGE");
            var childElementList = searcher
                                    .childElementList(parentList,
                                    "DESIGN-FUNCTION-TYPE",
                                    "ANALYSIS-FUNCTION-TYPE",
                                    "HARDWARE-FUNCTION-TYPE",
                                    "HARDWARE-COMPONENT-TYPE",
                                    "BASIC-SOFTWARE-FUNCTION-TYPE");
            
            /*var parentList = searcher.parentElementList(xml, "book");
            var childElementList = searcher.childElementList(xml, parentList, "title");*/

            functions.MaxValue = childElementList.Values.Max();
            functions.MinValue = childElementList.Values.Min();
            functions.AvgValue = childElementList.Values.Average();

            return functions;
        }

        /// <summary>
        /// FunctionType package nested metric
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public Metric Functions_pckg_tc(XDocument xml)
        {
            var parentList = searcher.parentElementList(xml, "EA-PACKAGE");
            var childElementList = searcher
                                    .nestedChildElementList(parentList,
                                    "DESIGN-FUNCTION-TYPE",
                                    "ANALYSIS-FUNCTION-TYPE",
                                    "HARDWARE-FUNCTION-TYPE",
                                    "HARDWARE-COMPONENT-TYPE",
                                    "BASIC-SOFTWARE-FUNCTION-TYPE");

            functions_tc.MaxValue = childElementList.Values.Max();
            functions_tc.MinValue = childElementList.Values.Min();
            functions_tc.AvgValue = childElementList.Values.Average();

            return functions_tc;

        }

        /// <summary>
        /// Requirement package metric
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public Metric Reqts_pckg(XDocument xml)
        {
            var parentList = searcher.parentElementList(xml, "EA-PACKAGE");
            var childElementList = searcher
                                   .childElementList(parentList, "REQUIREMENT");

            reqts.MaxValue = childElementList.Values.Max();
            reqts.MinValue = childElementList.Values.Min();
            reqts.AvgValue = childElementList.Values.Average();

            return reqts; 
        }

        /// <summary>
        /// Requirement package nested metric
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public Metric Reqts_pckg_tc(XDocument xml)
        {
            var parentList = searcher.parentElementList(xml, "EA-PACKAGE");
            var childElementList = searcher
                                    .nestedChildElementList(parentList, "REQUIREMENT");

            reqts_tc.MaxValue = childElementList.Values.Max();
            reqts_tc.MinValue = childElementList.Values.Min();
            reqts_tc.AvgValue = childElementList.Values.Average();

            return reqts_tc;
        }
    }
}
