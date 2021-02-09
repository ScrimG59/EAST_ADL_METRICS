using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace EAST_ADL_METRICS.Utils.Searcher
{
    public class Global
    {
        /// <summary>
        /// Returns a list of the nodes with the given keyword
        /// </summary>
        /// <param name="xml">the xml file</param>
        /// <param name="keyword">an element which gets searched, for example "package"</param>
        /// <returns></returns>
        public List<XElement> parentElementList(XDocument xml, string firstKeyword, string secondKeyword = null, string thirdKeyword = null, string fourthKeyword = null, string fifthKeyword = null, string sixthKeyword = null)
        {
            List<XElement> parentList = xml.Descendants()
                                            .Where(node => node.Name == firstKeyword
                                                        || node.Name == secondKeyword
                                                        || node.Name == thirdKeyword
                                                        || node.Name == fourthKeyword
                                                        || node.Name == fifthKeyword
                                                        || node.Name == sixthKeyword).ToList();

            return parentList;
        }
    }
}
