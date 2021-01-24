using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using EAST_ADL_METRICS.Models;
using EAST_ADL_METRICS.Utils.Searcher;

namespace EAST_ADL_METRICS.View
{
    public partial class SelectWindow : Window
    {
        private XDocument _xml = new XDocument();
        private Helper helper = new Helper();
        private List<XElement> nodeList = new List<XElement>();
        private List<Item> nodes = new List<Item>();

        public SelectWindow(XDocument xml)
        {
            InitializeComponent();
            _xml = xml;
            nodeList = helper.getAllElements(_xml);
            foreach (var node in nodeList)
            {
                if (helper.getName(node).Count == 1)
                {
                    nodes.Add(new Item
                    {
                        Name = helper.getName(node)[0],
                        Type = helper.getType(node)
                    });
                }
                else
                {
                    nodes.Add(new Item
                    {
                        Name = helper.getName(node)[1],
                        Type = helper.getType(node)
                    });
                }
            }
            ListViewFunctionTypes.ItemsSource = nodes;
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = (dynamic)ListViewFunctionTypes.SelectedItem;
            if (selectedItem != null)
            {
                ((MainWindow)Application.Current.MainWindow).showMetrics(_xml, new Item
                { 
                  Name = selectedItem.Name,
                  Type = selectedItem.Type,
                });
                this.Close();
            }
        }
    }
}
