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
            resetAll();
            parser = new Parser();
            //package = new Package();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "EAST-ADL Files|*.eaxml";
            openFileDialog.DefaultExt = ".eaxml";
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
            var ruleList = wrapper.calcualteRules(xml);
            if (item.Type.Contains("EA-PACKAGE"))
            {
                Functions_pckg_val.Text = metricList[0].Value.ToString();
                Functions_pckg_tc_val.Text = metricList[1].Value.ToString();
                Reqts_pckg_val.Text = metricList[2].Value.ToString();
                Reqts_pckg_tc_val.Text = metricList[3].Value.ToString();
                OptionalElements_val.Text = metricList[4].Value.ToString();
                Functional_Quality_Reqts_Ratio_val.Text = metricList[5].Value.ToString();
                UseCaseSatisfaction_val.Text = metricList[6].Value.ToString();
                VVRatio_val.Text = metricList[7].Value.ToString();
            }
            else if(item.Type.Contains("FUNCTION-TYPE") || item.Type.Contains("ANALYSIS-ARCHITECTURE"))
            {
                Parts_fct_val.Text = metricList[0].Value.ToString();
                Parts_fct_tc_val.Text = metricList[1].Value.ToString();
                NestingLevels_fct_val.Text = metricList[2].Value.ToString();
                Ports_fct_val.Text = metricList[3].Value.ToString();
                Connectors_fct_val.Text = metricList[4].Value.ToString();
                Constraints_val.Text = metricList[5].Value.ToString();
                FunctionPorts_val.Text = metricList[6].Value.ToString();
                FunctionFlowPorts_val.Text = metricList[7].Value.ToString();
                FunctionPowerPorts_val.Text = metricList[8].Value.ToString();
                FunctionClientServerPorts_val.Text = metricList[9].Value.ToString();
                Operations_val.Text = metricList[10].Value.ToString();
                Portgroups_val.Text = metricList[11].Value.ToString();
                Portgroupsize_val.Text = metricList[12].Value.ToString();
                OptionalElements_val.Text = metricList[13].Value.ToString();
                UseCaseSatisfaction_val.Text = metricList[14].Value.ToString();
                Functional_Quality_Reqts_Ratio_val.Text = metricList[15].Value.ToString();
                VVRatio_val.Text = metricList[16].Value.ToString();
            }
            else if (item.Type.Equals("HARDWARE-DESIGN-ARCHITECTURE"))
            {
                Parts_fct_val.Text = metricList[0].Value.ToString();
                Parts_fct_tc_val.Text = metricList[1].Value.ToString();
                NestingLevels_fct_val.Text = metricList[2].Value.ToString();
                Ports_fct_val.Text = metricList[3].Value.ToString();
                Connectors_fct_val.Text = metricList[4].Value.ToString();
                HardwarePorts_val.Text = metricList[5].Value.ToString();
                OptionalElements_val.Text = metricList[6].Value.ToString();
                UseCaseSatisfaction_val.Text = metricList[7].Value.ToString();
                Functional_Quality_Reqts_Ratio_val.Text = metricList[8].Value.ToString();
                VVRatio_val.Text = metricList[9].Value.ToString();
            }
            else if (item.Type.Contains("DESIGN-ARCHITECTURE"))
            {
                Parts_fct_val.Text = metricList[0].Value.ToString();
                Parts_fct_tc_val.Text = metricList[1].Value.ToString();
                NestingLevels_fct_val.Text = metricList[2].Value.ToString();
                Ports_fct_val.Text = metricList[3].Value.ToString();
                Connectors_fct_val.Text = metricList[4].Value.ToString();
                Constraints_val.Text = metricList[5].Value.ToString();
                FunctionNodeAllocation_val.Text = metricList[6].Value.ToString();
                FunctionPorts_val.Text = metricList[7].Value.ToString();
                FunctionFlowPorts_val.Text = metricList[8].Value.ToString();
                FunctionPowerPorts_val.Text = metricList[9].Value.ToString();
                FunctionClientServerPorts_val.Text = metricList[10].Value.ToString();
                Operations_val.Text = metricList[11].Value.ToString();
                Portgroups_val.Text = metricList[12].Value.ToString();
                Portgroupsize_val.Text = metricList[13].Value.ToString();
                OptionalElements_val.Text = metricList[14].Value.ToString();
                UseCaseSatisfaction_val.Text = metricList[15].Value.ToString();
                Functional_Quality_Reqts_Ratio_val.Text = metricList[16].Value.ToString();
                VVRatio_val.Text = metricList[17].Value.ToString();
            }
            else if (item.Type.Contains("REQUIREMENT"))
            {
                SubReqts_val.Text = metricList[0].Value.ToString();
                NestingLevel_val.Text = metricList[1].Value.ToString();
                Satisfiers_val.Text = metricList[2].Value.ToString();
                Verifiers_val.Text = metricList[3].Value.ToString();
                Derivatives_val.Text = metricList[4].Value.ToString();
                OptionalElements_val.Text = metricList[5].Value.ToString();
                UseCaseSatisfaction_val.Text = metricList[6].Value.ToString();
                Functional_Quality_Reqts_Ratio_val.Text = metricList[7].Value.ToString();
                VVRatio_val.Text = metricList[8].Value.ToString();
            }
            if (ruleList[0].Fulfilled)
            {
                PortConnectorAllocation.Foreground = new SolidColorBrush(Colors.Green);
            }
            if (ruleList[1].Fulfilled)
            {
                Unverified.Foreground = new SolidColorBrush(Colors.Green);
            }
            if (ruleList[2].Fulfilled)
            {
                ResidualAnomaly.Foreground = new SolidColorBrush(Colors.Green);
            }
            if (ruleList[3].Fulfilled)
            {
                Reference.Foreground = new SolidColorBrush(Colors.Green);
            }
            if (ruleList[4].Fulfilled)
            {
                EventChainPair.Foreground = new SolidColorBrush(Colors.Green);
            }
            if (true)
            {
                PortConnectorDirection.Foreground = new SolidColorBrush(Colors.Green);
            }
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
            else if(item.Type.Contains("FUNCTION-TYPE") || item.Type.Contains("ANALYSIS-ARCHITECTURE"))
            {
                Functions_pckg.Foreground = new SolidColorBrush(Colors.Gray);
                Functions_pckg_tc.Foreground = new SolidColorBrush(Colors.Gray);
                Reqts_pckg.Foreground = new SolidColorBrush(Colors.Gray);
                Reqts_pckg_tc.Foreground = new SolidColorBrush(Colors.Gray);
                SubReqts.Foreground = new SolidColorBrush(Colors.Gray);
                NestingLevel.Foreground = new SolidColorBrush(Colors.Gray);
                HardwarePorts.Foreground = new SolidColorBrush(Colors.Gray);
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
            else if (item.Type.Equals("HARDWARE-DESIGN-ARCHITECTURE") || item.Type.Equals("HARDWARE-COMPONENT-TYPE"))
            {
                Functions_pckg.Foreground = new SolidColorBrush(Colors.Gray);
                Functions_pckg_tc.Foreground = new SolidColorBrush(Colors.Gray);
                Reqts_pckg.Foreground = new SolidColorBrush(Colors.Gray);
                Reqts_pckg_tc.Foreground = new SolidColorBrush(Colors.Gray);
                FunctionNodeAllocation.Foreground = new SolidColorBrush(Colors.Gray);
                SubReqts.Foreground = new SolidColorBrush(Colors.Gray);
                NestingLevel.Foreground = new SolidColorBrush(Colors.Gray);
                Satisfiers.Foreground = new SolidColorBrush(Colors.Gray);
                Verifiers.Foreground = new SolidColorBrush(Colors.Gray);
                Derivatives.Foreground = new SolidColorBrush(Colors.Gray);
                Constraints.Foreground = new SolidColorBrush(Colors.Gray);
                FunctionPorts.Foreground = new SolidColorBrush(Colors.Gray);
                FunctionFlowPorts.Foreground = new SolidColorBrush(Colors.Gray);
                FunctionPowerPorts.Foreground = new SolidColorBrush(Colors.Gray);
                FunctionClientServerPorts.Foreground = new SolidColorBrush(Colors.Gray);
                Operations.Foreground = new SolidColorBrush(Colors.Gray);
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
            PortConnectorAllocation.Foreground = new SolidColorBrush(Colors.IndianRed);
            Unverified.Foreground = new SolidColorBrush(Colors.IndianRed);
            ResidualAnomaly.Foreground = new SolidColorBrush(Colors.IndianRed);
            Reference.Foreground = new SolidColorBrush(Colors.IndianRed);
            EventChainPair.Foreground = new SolidColorBrush(Colors.IndianRed);
            PortConnectorDirection.Foreground = new SolidColorBrush(Colors.IndianRed);
        }

        private void resetValues()
        {
            Functions_pckg_val.Text = "";
            Functions_pckg_tc_val.Text = "";
            Reqts_pckg_val.Text = "";
            Reqts_pckg_tc_val.Text = "";
            Parts_fct_val.Text = "";
            Parts_fct_tc_val.Text = "";
            NestingLevels_fct_val.Text = "";
            Ports_fct_val.Text = "";
            Connectors_fct_val.Text = "";
            Constraints_val.Text = "";
            FunctionNodeAllocation_val.Text = "";
            SubReqts_val.Text = "";
            NestingLevel_val.Text = "";
            Satisfiers_val.Text = "";
            Verifiers_val.Text = "";
            Derivatives_val.Text = "";
            FunctionPorts_val.Text = "";
            FunctionFlowPorts_val.Text = "";
            FunctionPowerPorts_val.Text = "";
            FunctionClientServerPorts_val.Text = "";
            Operations_val.Text = "";
            HardwarePorts_val.Text = "";
            Portgroups_val.Text = "";
            Portgroupsize_val.Text = "";
            OptionalElements_val.Text = "";
            UseCaseSatisfaction_val.Text = "";
            Functional_Quality_Reqts_Ratio_val.Text = "";
            VVRatio_val.Text = "";
        }

        private void resetAll()
        {
            resetFontColors();
            resetValues();
        }
    }
}
