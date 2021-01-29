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
        private Requirement requirement = new Requirement();
        private Wrapper wrapper = new Wrapper();
        // true: global, false: local
        // private bool mode = true;

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
            openFileDialog.Filter = "EAST-ADL Files|*.eaxml";
            openFileDialog.DefaultExt = ".eaxml";
            Nullable<bool> dialogOK = openFileDialog.ShowDialog();
            resetFontColors();
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
                SelectWindow selectWindow = new SelectWindow(xml);
                selectWindow.Show();
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

        public void showMetrics(XDocument xml, Item item)
        {
            changeFontColor(item);
            SelectedElement.Text = item.Name;
            //var subReqts = requirement.SubReqts(xml, name);
            //var nestingLevel = requirement.NestingLevel(xml, name);

            //Console.WriteLine(subReqts.AvgValue);
            //Console.WriteLine(nestingLevel.AvgValue);
            
            var metricList = wrapper.calculateMetrics(xml, item);
            /*if (mode)
            {
                Functions_pckg_val.Text = metricList[0].AvgValue.ToString();
                Functions_pckg_tc_val.Text = metricList[1].AvgValue.ToString();
                Reqts_pckg_val.Text = metricList[2].AvgValue.ToString();
                Reqts_pckg_tc_val.Text = metricList[3].AvgValue.ToString();
                Parts_fct_val.Text = metricList[4].AvgValue.ToString();
                Parts_fct_tc_val.Text = metricList[5].AvgValue.ToString();
                NestingLevels_fct_val.Text = metricList[6].AvgValue.ToString();
                Ports_fct_val.Text = metricList[7].AvgValue.ToString();
                Connectors_fct_val.Text = metricList[8].AvgValue.ToString();
                SubReqts_val.Text = metricList[9].AvgValue.ToString();
                NestingLevel_val.Text = metricList[10].AvgValue.ToString();
                Satisfiers_val.Text = metricList[11].AvgValue.ToString();
                Verifiers_val.Text = metricList[12].AvgValue.ToString();
                Derivatives_val.Text = metricList[13].AvgValue.ToString();
                Constraints_cons_val.Text = metricList[14].AvgValue.ToString();
                Parts_cons_val.Text = metricList[15].AvgValue.ToString();
                Parts_cons_tc_val.Text = metricList[16].AvgValue.ToString();
                NestingLevels_cons_val.Text = metricList[17].AvgValue.ToString();
                Connectors_cons_val.Text = metricList[18].AvgValue.ToString();
                Parts_arch_val.Text = metricList[19].AvgValue.ToString();
                Parts_arch_tc_val.Text = metricList[20].AvgValue.ToString();
                NestingLevels_arch_val.Text = metricList[21].AvgValue.ToString();
                Ports_arch_val.Text = metricList[22].AvgValue.ToString();
                Connectors_arch_val.Text = metricList[23].AvgValue.ToString();
                FunctionNodeAllocation_val.Text = metricList[24].AvgValue.ToString();
                FunctionPorts_val.Text = metricList[25].AvgValue.ToString();
                FunctionFlowPorts_val.Text = metricList[26].AvgValue.ToString();
                FunctionPowerPorts_val.Text = metricList[27].AvgValue.ToString();
                FunctionClientServerPorts_val.Text = metricList[28].AvgValue.ToString();
                Operations_val.Text = metricList[29].AvgValue.ToString();
                HardwarePorts_val.Text = metricList[30].AvgValue.ToString();
                Portgroups_val.Text = metricList[31].AvgValue.ToString();
                Portgroupsize_val.Text = metricList[32].AvgValue.ToString();
                OptionalElements_val.Text = metricList[33].AvgValue.ToString();
                Qualityrequirement_Requirement_Ratio_val.Text = metricList[34].AvgValue.ToString();
                UseCaseSatisfaction_val.Text = metricList[35].AvgValue.ToString();
                VVRatio_val.Text = metricList[36].AvgValue.ToString();
            }*/

            //Console.WriteLine($"NAME: {item.Name}\n TYPE: {item.Type}");
        }

        private void ExtractResult_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button works.");
        }

        /*private void Mode_Click(object sender, RoutedEventArgs e)
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
        }*/

        private void changeFontColor(Item item)
        {
            if(item.Type.Equals("EA-PACKAGE"))
            {
                Parts_fct.Foreground = new SolidColorBrush(Colors.Gray);
                Parts_fct_tc.Foreground = new SolidColorBrush(Colors.Gray);
                NestingLevels_fct.Foreground = new SolidColorBrush(Colors.Gray);
                Ports_fct.Foreground = new SolidColorBrush(Colors.Gray);
                Connectors_fct.Foreground = new SolidColorBrush(Colors.Gray);
                SubReqts.Foreground = new SolidColorBrush(Colors.Gray);
                NestingLevel.Foreground = new SolidColorBrush(Colors.Gray);
                Satisfiers.Foreground = new SolidColorBrush(Colors.Gray);
                Verifiers.Foreground = new SolidColorBrush(Colors.Gray);
                Derivatives.Foreground = new SolidColorBrush(Colors.Gray);
                Constraints.Foreground = new SolidColorBrush(Colors.Gray);
                FunctionNodeAllocation.Foreground = new SolidColorBrush(Colors.Gray);
                FunctionPorts.Foreground = new SolidColorBrush(Colors.Gray);
                FunctionFlowPorts.Foreground = new SolidColorBrush(Colors.Gray);
                FunctionPowerPorts.Foreground = new SolidColorBrush(Colors.Gray);
                FunctionClientServerPorts.Foreground = new SolidColorBrush(Colors.Gray);
                Operations.Foreground = new SolidColorBrush(Colors.Gray);
                HardwarePorts.Foreground = new SolidColorBrush(Colors.Gray);
                Portgroups.Foreground = new SolidColorBrush(Colors.Gray);
                Portgroupsize.Foreground = new SolidColorBrush(Colors.Gray);
            }
            else if(item.Type.Contains("FUNCTION-TYPE") || item.Type.Contains("FUNCTIONAL-ANALYSIS-ARCHITECTURE"))
            {
                Functions_pckg.Foreground = new SolidColorBrush(Colors.Gray);
                Functions_pckg_tc.Foreground = new SolidColorBrush(Colors.Gray);
                Reqts_pckg.Foreground = new SolidColorBrush(Colors.Gray);
                Reqts_pckg_tc.Foreground = new SolidColorBrush(Colors.Gray);
                SubReqts.Foreground = new SolidColorBrush(Colors.Gray);
                NestingLevel.Foreground = new SolidColorBrush(Colors.Gray);
                Satisfiers.Foreground = new SolidColorBrush(Colors.Gray);
                Verifiers.Foreground = new SolidColorBrush(Colors.Gray);
                Derivatives.Foreground = new SolidColorBrush(Colors.Gray);
                Constraints.Foreground = new SolidColorBrush(Colors.Gray);
                FunctionNodeAllocation.Foreground = new SolidColorBrush(Colors.Gray);
            }
            else if(item.Type.Contains("REQUIREMENT"))
            {
                Functions_pckg.Foreground = new SolidColorBrush(Colors.Gray);
                Functions_pckg_tc.Foreground = new SolidColorBrush(Colors.Gray);
                Reqts_pckg.Foreground = new SolidColorBrush(Colors.Gray);
                Reqts_pckg_tc.Foreground = new SolidColorBrush(Colors.Gray);
                Parts_fct.Foreground = new SolidColorBrush(Colors.Gray);
                Parts_fct_tc.Foreground = new SolidColorBrush(Colors.Gray);
                NestingLevels_fct.Foreground = new SolidColorBrush(Colors.Gray);
                Ports_fct.Foreground = new SolidColorBrush(Colors.Gray);
                Connectors_fct.Foreground = new SolidColorBrush(Colors.Gray);
                Constraints.Foreground = new SolidColorBrush(Colors.Gray);
                FunctionNodeAllocation.Foreground = new SolidColorBrush(Colors.Gray);
                FunctionPorts.Foreground = new SolidColorBrush(Colors.Gray);
                FunctionFlowPorts.Foreground = new SolidColorBrush(Colors.Gray);
                FunctionPowerPorts.Foreground = new SolidColorBrush(Colors.Gray);
                FunctionClientServerPorts.Foreground = new SolidColorBrush(Colors.Gray);
                Operations.Foreground = new SolidColorBrush(Colors.Gray);
                HardwarePorts.Foreground = new SolidColorBrush(Colors.Gray);
                Portgroups.Foreground = new SolidColorBrush(Colors.Gray);
                Portgroupsize.Foreground = new SolidColorBrush(Colors.Gray);
            }
            else if (item.Type.Contains("DESIGN-ARCHITECTURE"))
            {
                Functions_pckg.Foreground = new SolidColorBrush(Colors.Gray);
                Functions_pckg_tc.Foreground = new SolidColorBrush(Colors.Gray);
                Reqts_pckg.Foreground = new SolidColorBrush(Colors.Gray);
                Reqts_pckg_tc.Foreground = new SolidColorBrush(Colors.Gray);
                SubReqts.Foreground = new SolidColorBrush(Colors.Gray);
                NestingLevel.Foreground = new SolidColorBrush(Colors.Gray);
                Satisfiers.Foreground = new SolidColorBrush(Colors.Gray);
                Verifiers.Foreground = new SolidColorBrush(Colors.Gray);
                Derivatives.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void resetFontColors()
        {
            Functions_pckg.Foreground = new SolidColorBrush(Colors.AliceBlue);
            Functions_pckg_tc.Foreground = new SolidColorBrush(Colors.AliceBlue);
            Reqts_pckg.Foreground = new SolidColorBrush(Colors.AliceBlue);
            Reqts_pckg_tc.Foreground = new SolidColorBrush(Colors.AliceBlue);
            Parts_fct.Foreground = new SolidColorBrush(Colors.AliceBlue);
            Parts_fct_tc.Foreground = new SolidColorBrush(Colors.AliceBlue);
            NestingLevels_fct.Foreground = new SolidColorBrush(Colors.AliceBlue);
            Ports_fct.Foreground = new SolidColorBrush(Colors.AliceBlue);
            Connectors_fct.Foreground = new SolidColorBrush(Colors.AliceBlue);
            SubReqts.Foreground = new SolidColorBrush(Colors.AliceBlue);
            NestingLevel.Foreground = new SolidColorBrush(Colors.AliceBlue);
            Satisfiers.Foreground = new SolidColorBrush(Colors.AliceBlue);
            Verifiers.Foreground = new SolidColorBrush(Colors.AliceBlue);
            Derivatives.Foreground = new SolidColorBrush(Colors.AliceBlue);
            Constraints.Foreground = new SolidColorBrush(Colors.AliceBlue);
            FunctionNodeAllocation.Foreground = new SolidColorBrush(Colors.AliceBlue);
            FunctionPorts.Foreground = new SolidColorBrush(Colors.AliceBlue);
            FunctionFlowPorts.Foreground = new SolidColorBrush(Colors.AliceBlue);
            FunctionPowerPorts.Foreground = new SolidColorBrush(Colors.AliceBlue);
            FunctionClientServerPorts.Foreground = new SolidColorBrush(Colors.AliceBlue);
            Operations.Foreground = new SolidColorBrush(Colors.AliceBlue);
            HardwarePorts.Foreground = new SolidColorBrush(Colors.AliceBlue);
            Portgroups.Foreground = new SolidColorBrush(Colors.AliceBlue);
            Portgroupsize.Foreground = new SolidColorBrush(Colors.AliceBlue);
            OptionalElements.Foreground = new SolidColorBrush(Colors.AliceBlue);
            Functional_Quality_Reqts_Ratio.Foreground = new SolidColorBrush(Colors.AliceBlue);
            UseCaseSatisfaction.Foreground = new SolidColorBrush(Colors.AliceBlue);
            VVRatio.Foreground = new SolidColorBrush(Colors.AliceBlue);
        }
    }
}
