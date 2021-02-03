using EAST_ADL_METRICS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EAST_ADL_METRICS.Utils.Searcher;

namespace EAST_ADL_METRICS.Utils.Categories
{
    public class Package
    {
        private Global globalSearcher = new Global();
        private Local localSearcher = new Local();

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
        public Metric Functions_pckg(XDocument xml, string elementName)
        {       
            int count = localSearcher.childElementList(xml, elementName, 
                                                "DESIGN-FUNCTION-TYPE",
                                                "ANALYSIS-FUNCTION-TYPE",
                                                "HARDWARE-FUNCTION-TYPE",
                                                "HARDWARE-COMPONENT-TYPE",
                                                "BASIC-SOFTWARE-FUNCTION-TYPE");

            functions.Value = count;

            return functions;
        }

        /// <summary>
        /// FunctionType package nested metric
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public Metric Functions_pckg_tc(XDocument xml, string elementName)
        {      
            int count = localSearcher.nestedChildElementList(xml, elementName,
                                                "DESIGN-FUNCTION-TYPE",
                                                "ANALYSIS-FUNCTION-TYPE",
                                                "HARDWARE-FUNCTION-TYPE",
                                                "HARDWARE-COMPONENT-TYPE",
                                                "BASIC-SOFTWARE-FUNCTION-TYPE");

            functions_tc.Value = count;

            return functions_tc;

        }

        /// <summary>
        /// Requirement package metric
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public Metric Reqts_pckg(XDocument xml, string elementName)
        { 
            int count = localSearcher.childElementList(xml, elementName, "REQUIREMENT",
                                                                         "QUALITY-REQUIREMENT");

            reqts.Value = count;

            return reqts; 
        }

        /// <summary>
        /// Requirement package nested metric
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public Metric Reqts_pckg_tc(XDocument xml, string elementName)
        {
            int count = localSearcher.nestedChildElementList(xml, elementName, "REQUIREMENT",
                                                                               "QUALITY-REQUIREMENT");

            reqts_tc.Value = count;

            return reqts_tc;
        }
    }
}
