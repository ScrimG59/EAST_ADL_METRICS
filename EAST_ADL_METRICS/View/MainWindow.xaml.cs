using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using EAST_ADL_METRICS.Utils.Parser;
using EAST_ADL_METRICS.Utils.Categories;
using System.Xml.Linq;
using EAST_ADL_METRICS.View;
using System.Threading;
using EAST_ADL_METRICS.Models;

namespace EAST_ADL_METRICS
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Parser parser;
        private Package package = new Package();
        private FunctionType functionType = new FunctionType();
        private Wrapper wrapper = new Wrapper();
        private bool mode = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectProject_Click(object sender, RoutedEventArgs e)
        {
            parser = new Parser();
            //package = new Package();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "XML Files|*.xml|EAST -ADL Files|*.eaxml";
            openFileDialog.DefaultExt = ".xml";
            Nullable<bool> dialogOK = openFileDialog.ShowDialog();
            XDocument xml = null;

            if (dialogOK == true)
            {
                xml = parser.LoadXML(openFileDialog.FileName);
            }
            else
            {
                MessageBox.Show("Something went wrong. Try it again!");
            }

            if(parser.Loaded() == true)
            {
                MessageBox.Show("XML-file successfully loaded!");
                if (mode)
                {
                    showMetrics(xml);
                }
                else
                {
                    SelectWindow selectWindow = new SelectWindow(xml);
                    selectWindow.Show();
                }
            }
            else
            {
                MessageBox.Show("Loading of XML-file failed! Please try again!");
            }

            /*Console.WriteLine("Functions_pckg:");
            var functions_pckg = package.Functions_pckg(xml);
            Console.WriteLine(functions_pckg.MaxValue);
            Console.WriteLine(functions_pckg.MinValue);
            Console.WriteLine(functions_pckg.AvgValue);

            Console.WriteLine("Functions_pckg_tc:");
            var functions_pckg_tc = package.Functions_pckg_tc(xml);
            Console.WriteLine(functions_pckg_tc.MaxValue);
            Console.WriteLine(functions_pckg_tc.MinValue);
            Console.WriteLine(functions_pckg_tc.AvgValue);

            Console.WriteLine("Parts_fct_tc:");
            var parts_fct_tc = functionType.Parts_fct_tc(xml);
            Console.WriteLine(parts_fct_tc.MaxValue);
            Console.WriteLine(parts_fct_tc.MinValue);
            Console.WriteLine(parts_fct_tc.AvgValue);*/

        }

        public void showMetrics(XDocument xml, Item item = null)
        {
            wrapper.calculateMetrics(xml, mode, item);
            Console.WriteLine($"NAME: {item.Name}\n TYPE: {item.Type}");
        }

        private void ExtractResult_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button works.");
        }

        private void Mode_Click(object sender, RoutedEventArgs e)
        {
            if (mode)
            {
                btnModeText.Text = "Local";
                mode = false;
            }
            else
            {
                btnModeText.Text = "Global";
                mode = true;
            }
        }
    }
}
