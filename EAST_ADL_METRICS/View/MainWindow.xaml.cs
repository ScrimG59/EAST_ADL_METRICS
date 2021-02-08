using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using EAST_ADL_METRICS.Utils.Parser;
using EAST_ADL_METRICS.Utils.Categories;
using System.Xml.Linq;
using EAST_ADL_METRICS.View;
using EAST_ADL_METRICS.Models;
using System.IO;

namespace EAST_ADL_METRICS
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Parser parser;
        private Serializer serializer = new Serializer();
        private Wrapper wrapper = new Wrapper();
        private bool resultsReady = false;
        private List<Metric> metricList = new List<Metric>();
        private List<Rule> ruleList = new List<Rule>();
        private Item selectedItem = new Item();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectProject_Click(object sender, RoutedEventArgs e)
        {
            resetAll();
            parser = new Parser();
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
                Console.WriteLine("Closed opendialog without selecting file");
                return;
            }

            if(parser.Loaded() == true)
            {
                MessageBox.Show("EAXML-file successfully loaded!");
                SelectWindow selectWindow = new SelectWindow(xml);
                selectWindow.Show();
            }
            else
            {
                MessageBox.Show("Loading of EAXML-file failed! Please try again!");
            }
        }

        public void showMetrics(XDocument xml, Item item)
        {
            changeFontColor(item);
            selectedItem = item;
            SelectedElement.Text = item.Name;

            metricList = wrapper.calculateMetrics(xml, item);
            ruleList = wrapper.calcualteRules(xml);

            if (item.Type.Contains("EA-PACKAGE"))
            {
                Functions_pckg_val.Text = metricList[0].Value.ToString();
                Functions_pckg_tc_val.Text = metricList[1].Value.ToString();
                Reqts_pckg_val.Text = metricList[2].Value.ToString();
                Reqts_pckg_tc_val.Text = metricList[3].Value.ToString();
                OptionalElements_val.Text = metricList[4].Value.ToString();
                Functional_Quality_Reqts_Ratio_val.Text = metricList[5].Value.ToString();
                UseCaseSatisfactionRatio_val.Text = metricList[6].Value.ToString();
                VVRatio_val.Text = metricList[7].Value.ToString();
            }
            else if(item.Type.Contains("FUNCTION-TYPE"))
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
                UseCaseSatisfactionRatio_val.Text = metricList[14].Value.ToString();
                Functional_Quality_Reqts_Ratio_val.Text = metricList[15].Value.ToString();
                VVRatio_val.Text = metricList[16].Value.ToString();
            }
            else if (item.Type.Contains("ANALYSIS-ARCHITECTURE"))
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
                UseCaseSatisfactionRatio_val.Text = metricList[14].Value.ToString();
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
                FunctionNodeAllocation_val.Text = metricList[5].Value.ToString();
                HardwarePorts_val.Text = metricList[6].Value.ToString();
                OptionalElements_val.Text = metricList[7].Value.ToString();
                UseCaseSatisfactionRatio_val.Text = metricList[8].Value.ToString();
                Functional_Quality_Reqts_Ratio_val.Text = metricList[9].Value.ToString();
                VVRatio_val.Text = metricList[10].Value.ToString();
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
                UseCaseSatisfactionRatio_val.Text = metricList[15].Value.ToString();
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
                UseCaseSatisfactionRatio_val.Text = metricList[6].Value.ToString();
                Functional_Quality_Reqts_Ratio_val.Text = metricList[7].Value.ToString();
                VVRatio_val.Text = metricList[8].Value.ToString();
            }
            else if (item.Type.Equals("MODE"))
            {
                AllocatedFunctionTypes_val.Text = metricList[0].Value.ToString();
                OptionalElements_val.Text = metricList[1].Value.ToString();
                UseCaseSatisfactionRatio_val.Text = metricList[2].Value.ToString();
                Functional_Quality_Reqts_Ratio_val.Text = metricList[3].Value.ToString();
                VVRatio_val.Text = metricList[4].Value.ToString();
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
            if (ruleList[5].Fulfilled)
            {
                ModeAllocation.Foreground = new SolidColorBrush(Colors.Green);
            }
            resultsReady = true;
        }

        private void ExtractResult_Click(object sender, RoutedEventArgs e)
        {
            if (resultsReady)
            {
                Stream myStream;
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                string path = "";

                saveFileDialog.Filter = "txt files (*.txt)|*.txt";

                if (saveFileDialog.ShowDialog() == true)
                {
                    if ((myStream = saveFileDialog.OpenFile()) != null)
                    {
                        path = saveFileDialog.FileName;
                        myStream.Close();
                    }

                    serializer.WriteResultsIntoFile(metricList, ruleList, selectedItem, path);
                    MessageBox.Show("Successfully extracted results!");
                    resultsReady = false;
                }
                else
                {
                    Console.WriteLine("Closed savedialog without selecting/creating file");
                    return;
                }
            }
        }

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
                AllocatedFunctionTypes.Foreground = new SolidColorBrush(Colors.Gray);
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
                FunctionNodeAllocation.Foreground = new SolidColorBrush(Colors.Gray);
                AllocatedFunctionTypes.Foreground = new SolidColorBrush(Colors.Gray);
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
                AllocatedFunctionTypes.Foreground = new SolidColorBrush(Colors.Gray);
            }
            else if (item.Type.Equals("HARDWARE-DESIGN-ARCHITECTURE"))
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
                FunctionPorts.Foreground = new SolidColorBrush(Colors.Gray);
                FunctionFlowPorts.Foreground = new SolidColorBrush(Colors.Gray);
                FunctionPowerPorts.Foreground = new SolidColorBrush(Colors.Gray);
                FunctionClientServerPorts.Foreground = new SolidColorBrush(Colors.Gray);
                Operations.Foreground = new SolidColorBrush(Colors.Gray);
                Portgroups.Foreground = new SolidColorBrush(Colors.Gray);
                Portgroupsize.Foreground = new SolidColorBrush(Colors.Gray);
                AllocatedFunctionTypes.Foreground = new SolidColorBrush(Colors.Gray);
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
                AllocatedFunctionTypes.Foreground = new SolidColorBrush(Colors.Gray);
            }
            else if (item.Type.Equals("MODE"))
            {
                Parts_fct.Foreground = new SolidColorBrush(Colors.Gray);
                Parts_fct_tc.Foreground = new SolidColorBrush(Colors.Gray);
                NestingLevels_fct.Foreground = new SolidColorBrush(Colors.Gray);
                Ports_fct.Foreground = new SolidColorBrush(Colors.Gray);
                Connectors_fct.Foreground = new SolidColorBrush(Colors.Gray);
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
                HardwarePorts.Foreground = new SolidColorBrush(Colors.Gray);
                Portgroups.Foreground = new SolidColorBrush(Colors.Gray);
                Portgroupsize.Foreground = new SolidColorBrush(Colors.Gray);
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
            UseCaseSatisfactionRatio.Foreground = new SolidColorBrush(Colors.AliceBlue);
            VVRatio.Foreground = new SolidColorBrush(Colors.AliceBlue);
            PortConnectorAllocation.Foreground = new SolidColorBrush(Colors.IndianRed);
            Unverified.Foreground = new SolidColorBrush(Colors.IndianRed);
            ResidualAnomaly.Foreground = new SolidColorBrush(Colors.IndianRed);
            Reference.Foreground = new SolidColorBrush(Colors.IndianRed);
            EventChainPair.Foreground = new SolidColorBrush(Colors.IndianRed);
            ModeAllocation.Foreground = new SolidColorBrush(Colors.IndianRed);
            AllocatedFunctionTypes.Foreground = new SolidColorBrush(Colors.AliceBlue);
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
            UseCaseSatisfactionRatio_val.Text = "";
            Functional_Quality_Reqts_Ratio_val.Text = "";
            VVRatio_val.Text = "";
            AllocatedFunctionTypes_val.Text = "";
        }

        private void resetAll()
        {
            resultsReady = false;
            metricList = new List<Metric>();
            ruleList = new List<Rule>();
            resetFontColors();
            resetValues();
        }
    }
}
