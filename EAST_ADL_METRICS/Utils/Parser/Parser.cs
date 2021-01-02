using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EAST_ADL_METRICS.Utils.Parser
{
    public class Parser
    {
        private XDocument xml = new XDocument();

        public XDocument LoadXML(string path)
        {

            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);

            try
            {
                xml = XDocument.Load(fs);
            } 
            catch(Exception ex)
            {
                Console.WriteLine($"Error while reading XML-file: {ex.Message}");
                xml = null;
            }
            return xml;
        }

        public Boolean Loaded()
        {
            if(xml != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
