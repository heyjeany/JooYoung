using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Polenter.Serialization;
using System.Threading;
//using Agilent.SignalStudio.N7617;
using Ivi.Visa.Interop;

using System.IO;

using ZedGraph;
namespace KeysightDynamicEVM
{
    public enum TESTSTATUS
    {
        READY,
        TESTING,
        ERROR,
        TESTDONE,
        TESTABORTED,
        BLANK
    }
    public enum PROGRESSSET
    {
        MAX,
        MIN,
        STATE
    }
    public partial class Form1 : Form
    {
        bool bThreadRun = false;
        Thread LogUpdate;
        MessageQueue mQ = new MessageQueue();

        SGControl iSgControl;
        MXG mxg = new MXG();
        PSG psg = new PSG();
        XSA xSA = new XSA();
        N6700x n6700x = new N6700x();

        ZedGraphControl graph = new ZedGraphControl();
        void Logging()
        {
            string strMessage = "";
            do
            {
                if (mQ.Count == 0)
                {
                    continue;
                }
                else
                {
                    Log(mQ.Dequeue());
                }
                Thread.Sleep(10);
            } while (bThreadRun);
        }

        #region Log Update

        bool bLoggingEverything = true;

        delegate void _del_UpdateLog(string strMessage, bool addCR = true);
        // Log Update
        void Log(string strMessage, bool addCR = true)
        {
            string[] splitStr = strMessage.Split('#');
            RichTextBox tRtb;
            tRtb = rbLog;
            if (tRtb.InvokeRequired)
            {
                _del_UpdateLog recurFunc = new _del_UpdateLog(Log);
                this.Invoke(recurFunc, new object[] { strMessage, addCR });
            }
            else
            {
                if (!bLoggingEverything)
                    return;
                if (splitStr[0].ToUpper() == "CLEAR")
                {
                    tRtb.Clear();
                }
                else
                {
                    if (addCR)
                        strMessage += "\r";
                    tRtb.AppendText(strMessage);//
                    tRtb.SelectionStart = tRtb.Text.Length;
                    tRtb.ScrollToCaret();
                }
            }
        }
        // Result Update
        void LogStatus(string strMessage, bool addCR = true)
        {
            string[] splitStr = strMessage.Split('#');

            if (stsMainFrm.InvokeRequired)
            {
                _del_UpdateLog recurFunc = new _del_UpdateLog(LogStatus);
                this.Invoke(recurFunc, new object[] { strMessage, addCR });
            }
            else
            {
                //UpdateGraphs();
                this.tslbState.Text = strMessage;
            }
        }

        delegate void _del_SetProgressBar(PROGRESSSET prgSet, int nNum);
        void SetProgressBar(PROGRESSSET prgSet, int nNum)
        {
            if (stsMainFrm.InvokeRequired)
            {
                _del_SetProgressBar recurFunc = new _del_SetProgressBar(SetProgressBar);
                this.Invoke(recurFunc, new object[] { prgSet, nNum });
            }
            else
            {
                switch (prgSet)
                {
                    case PROGRESSSET.MAX:
                        tsPrgressBar.Maximum = nNum;
                        break;
                    case PROGRESSSET.MIN:
                        tsPrgressBar.Minimum = nNum;
                        break;
                    case PROGRESSSET.STATE:
                        tsPrgressBar.Value = nNum;
                        break;
                }
            }
        }

        delegate void _del_UpdateBtn(TESTSTATUS testStatus);
        void TestStateUpdate(TESTSTATUS testStatus)
        {
            if (bButtonThreadActive == false)
                return;
            if (btnTestStatus.InvokeRequired)
            {
                _del_UpdateBtn recurFunc = new _del_UpdateBtn(TestStateUpdate);
                this.Invoke(recurFunc, new object[] { testStatus });
            }
            else
            {
                switch (testStatus)
                {
                    case TESTSTATUS.READY:
                        btnTestStatus.BackColor = Color.White;
                        btnTestStatus.ForeColor = Color.Black;
                        btnTestStatus.Text = "Press to Start";
                        break;
                    case TESTSTATUS.ERROR:
                        btnTestStatus.BackColor = Color.Red;
                        btnTestStatus.ForeColor = Color.Black;
                        btnTestStatus.Text = "Error While Testing";
                        break;
                    case TESTSTATUS.TESTABORTED:
                        btnTestStatus.BackColor = Color.Red;
                        btnTestStatus.ForeColor = Color.Black;
                        btnTestStatus.Text = "Test Stopped by User";
                        break;
                    case TESTSTATUS.TESTING:
                        btnTestStatus.BackColor = Color.Yellow;
                        btnTestStatus.ForeColor = Color.Black;
                        btnTestStatus.Text = "Testing";
                        break;
                    case TESTSTATUS.TESTDONE:
                        btnTestStatus.BackColor = Color.Blue;
                        btnTestStatus.ForeColor = Color.White;
                        btnTestStatus.Text = "Test Done.";
                        break;
                    case TESTSTATUS.BLANK:
                        btnTestStatus.Text = "";
                        break;
                }
            }
        }

        #endregion
        void DisplayButton()
        {
            do
            {
                if (!bButtonThreadActive) break;
                TestStateUpdate(testState);
                if (!bButtonThreadActive) break;
                Thread.Sleep(1000);
                if (!bButtonThreadActive) break;
                if (testState == TESTSTATUS.TESTING)
                {
                    TestStateUpdate(TESTSTATUS.BLANK);
                    Thread.Sleep(500);
                }

                if (!bButtonThreadActive) break;
            } while (true);
        }


        PARAMETERCLASS parameter = new PARAMETERCLASS();
        //Agilent.SignalStudio.N7617.Wlan11axAPI api;// = new LegacyAPI();
        TESTSTATUS testState = TESTSTATUS.READY;
        bool bButtonThreadActive = false;
        Thread ButtonUpdateThread;

        Thread testThread;
        bool bMainTestStarted = false;
        void MainTest()
        {
            try
            {
                testState = TESTSTATUS.TESTING;
                string[] t = txtFreqToTest.Text.Split(',');
                double[] FreqToTest = new double[t.Length];
                int i = 0;
                foreach (string strNum in t)
                {
                    FreqToTest[i++] = Convert.ToDouble(strNum);
                }
                t = txtVoltChange.Text.Split(',');
                double[] VoltLevelToTest = new double[t.Length];
                i = 0;
                foreach (string strNum in t)
                {
                    VoltLevelToTest[i++] = Convert.ToDouble(strNum);
                }
                int nRepeat = Convert.ToInt16(txtRepeatTime.Text);
                t = txtMXGPowerLevelToTest.Text.Split(',');
                double[] PowerlevelToTest = new double[t.Length];
                i = 0;
                foreach (string strNum in t)
                {
                    PowerlevelToTest[i++] = Convert.ToDouble(strNum);
                }

                #region Setup MXG
                iSgControl.SetupSG(parameter.T1, parameter.T2, parameter.DutyCycle, parameter.SGFreqInMhz, PowerlevelToTest[0], parameter.WaveformFileName);
                #endregion
                #region Setup MXA
                // Select WLAN App
                xSA.SendCommand("INST:SEL WLAN");
                // Select 11ax 80Mhz
                xSA.SendCommand("RAD:STAN AX80");
                // configure EVM measurement
                xSA.SendCommand("CONF:EVM");
                // set External trigger to EXT1
                xSA.SendCommand("TRIG:EVM:SOUR EXT1");
                // Set input range 
                xSA.SendCommand("POWER:RANGE " + (PowerlevelToTest[0] + parameter.Range).ToString());
                // turn off average
                xSA.SendCommand("EVM:AVER:STAT OFF");
                // Equalizer training setting
                xSA.SendCommand("CALC:EVM:EQU:TMOD SDATA");
                #endregion
                #region Power On
                n6700x.SendCommand("SENSE:FUNC \"VOLT\",(@1)");
                #endregion
                string strRead = "";
                Log(" >Measure EVM");
                int nTotalLoop = FreqToTest.Length * VoltLevelToTest.Length * nRepeat * PowerlevelToTest.Length;
                SetProgressBar(PROGRESSSET.MAX, nTotalLoop);
                SetProgressBar(PROGRESSSET.MIN, 0);
                SetProgressBar(PROGRESSSET.STATE, 0);
                nTotalLoop = 0;
                // Main Test
                List<string> Result = new List<string>();

                for (int PowerlevelLoop = 0; PowerlevelLoop < PowerlevelToTest.Length; PowerlevelLoop++)
                {
                    // Set MXG Power Level. 
                    iSgControl.SendCommand("POWER " + PowerlevelToTest[PowerlevelLoop].ToString(".00") + "DBM");
                   
                    // added for auto range clippnig : Jooyoung 
                    xSA.SendCommand("SENSe:POWer:RF:RANGe:OPTimize IMMediate");
                    //xSA.SendCommand("POWER:ATT 15");
                    xSA.SetAttenuator(PowerlevelToTest[PowerlevelLoop] + parameter.Range);
                    //xSA.SendCommand("POWER:ATT " + ((int)((PowerlevelToTest[PowerlevelLoop] + parameter.Range))).ToString());
                    //xSA.SendCommand("POWER:RANGE " + (PowerlevelToTest[PowerlevelLoop] + parameter.Range).ToString());
                    Thread.Sleep(1000);
                    for (int FreqLoop = 0; FreqLoop < FreqToTest.Length; FreqLoop++)
                    {
                        xSA.SendCommand("FREQ:CENT " + FreqToTest[FreqLoop].ToString() + "MHZ");
                        iSgControl.SendCommand("FREQ " + FreqToTest[FreqLoop].ToString() + "MHZ");
                        for (int VoltLoop = 0; VoltLoop < VoltLevelToTest.Length; VoltLoop++)
                        {
                           

                            n6700x.SendCommand("SOUR:VOLT:LEVEL:IMM:AMP " + VoltLevelToTest[VoltLoop].ToString() + ",(@1)");
                            n6700x.SendCommand("OUTPUT:STATE 1" + ",(@1)");
                            Thread.Sleep(parameter.GapBtwMeasure);
                            for (int repeat = 0; repeat < nRepeat; repeat++)
                            {
                                LogStatus("Testing at " + PowerlevelToTest[PowerlevelLoop].ToString() + " dBm/ " + FreqToTest[FreqLoop].ToString() + " Mhz / " + VoltLevelToTest[VoltLoop].ToString() + " Volts ( Repeat " + (repeat + 1).ToString() + " )");
                                SetProgressBar(PROGRESSSET.STATE, ++nTotalLoop);
                                xSA.SendCommand("READ:EVM?");
                                Thread.Sleep(500);
                                xSA.Read(ref strRead);
                                Result.Add(PowerlevelToTest[PowerlevelLoop].ToString() + "," + FreqToTest[FreqLoop].ToString() + "," + VoltLevelToTest[VoltLoop].ToString() + "," + (repeat + 1).ToString() + "," + strRead.Split(',')[0]);
                                Thread.Sleep(parameter.GapBtwMeasure);
                                if (!bMainTestStarted) throw new Exception("Test Aborted..");
                            }
                            n6700x.SendCommand("OUTPUT:STATE 0" + ",(@1)");
                            Thread.Sleep(parameter.GapBtwMeasure);
                        }
                    }
                }
                iSgControl.SendCommand("output off");
                n6700x.SendCommand("OUTPUT:STATE 0" + ",(@1)");
                Log("Test Done. ");

                this.bMainTestStarted = false;
                testState = TESTSTATUS.TESTDONE;
                double[] data = new double[Result.Count];
                int nIndex = 0;
                bool tLogSet = bLoggingEverything;
                bLoggingEverything = true;
                DateTimeStamp stamp = new DateTimeStamp();

                FileStream fs = new FileStream(".\\Report_" + stamp.GetNowTime() +".txt", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("POWER, FREQ, VOLT,REPEAT,RESULT");

                foreach (string tStr in Result)
                {
                    Log(tStr);
                    data[nIndex++] = Convert.ToDouble(tStr.Split(',')[4]);
                    sw.WriteLine(tStr);
                }

                sw.Close();fs.Close();
                bLoggingEverything = tLogSet;
                UpdateGraph(data);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Test Aborted..")
                    testState = TESTSTATUS.TESTABORTED;
                else
                    testState = TESTSTATUS.ERROR;
                Log(ex.Message);
            }
        }
        public Form1()
        {

            InitializeComponent();
            ButtonUpdateThread = new Thread(new ThreadStart(DisplayButton));
            bButtonThreadActive = true;
            ButtonUpdateThread.Start();
            // splitContainer3.SplitterDistance = 350;
            this.pgParameter.SelectedObject = parameter;
            //LogUpdate = new Thread(new ThreadStart(Logging));
            //LogUpdate.Start();
            mxg.updateLog += new InstrumentControl.UpdateLog(Log);
            psg.updateLog += new InstrumentControl.UpdateLog(Log);
            xSA.updateLog += new InstrumentControl.UpdateLog(Log);
            n6700x.updateLog += new InstrumentControl.UpdateLog(Log);
            this.splitContainer4.Panel2.Controls.Add(graph);
            graph.Dock = DockStyle.Fill;
            graph.GraphPane.Title.Text = "EVM Result";

            n6700x.bIsSimulationMode = true;

            //iSgControl = (SGControl)psg;

        }

        private void loadSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "Setup File(*.setup) |*.setup";
                if (dlg.ShowDialog() == DialogResult.Cancel)
                    return;
                var serializer = new SharpSerializer();
                parameter = (PARAMETERCLASS)serializer.Deserialize(dlg.FileName);
                this.pgParameter.SelectedObject = parameter;
                this.pgParameter.Refresh();

            }
            catch (Exception ex)
            {
                mQ.Enqueue(ex.Message);
            }
        }

        private void saveSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Setup File(*.setup) |*.setup";
                if (dlg.ShowDialog() == DialogResult.Cancel)
                    return;
                string strFileName = dlg.FileName;
                if (!strFileName.EndsWith(".setup"))
                    strFileName = strFileName + ".setup";
                var serializer = new SharpSerializer();
                serializer.Serialize(parameter, strFileName);
                //this.UpdatePropertyGrid();

            }
            catch (Exception ex)
            {
                mQ.Enqueue(ex.Message);
            }
        }

        private void connectInstrumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                InstrumentControl sgTest = new InstrumentControl();
                string id = sgTest.IdentifySignalGenerator(parameter.SGVISAAddress);
                if( id.ToUpper().IndexOf("N5182") != -1 )
                {
                    iSgControl = (SGControl)mxg;
                    Log("MXG found!");
                }
                else
                {
                    iSgControl = (SGControl)psg;
                    Log("ESG/PSG found");
                }


                if (!iSgControl.GetInitializedStatus())
                {
                    Log("Initialize SG!");
                    iSgControl.Initialize(parameter.SGVISAAddress);
                }
                if (!xSA.Initialized)
                {
                    Log("Initialize xSA!");
                    xSA.Initialize(parameter.xSAVISAAddress);
                    xSA.SendCommand("inst:sel wlan");
                    Thread.Sleep(2000);
                    //xSA.SendCommand("INST:SEL WLAN");
                    // Select 11ax 80Mhz
                    xSA.SendCommand("RAD:STAN AX80");
                    // configure EVM measurement
                    xSA.SendCommand("CONF:EVM");
                }
                if (!n6700x.Initialized)
                {
                    Log("Initialize Power Supply.");
                    n6700x.Initialize(parameter.N6700VISAAddress);
                }
                // Add MXA Initialization code here. 


            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }

        private void btnTestWithMXG_Click(object sender, EventArgs e)
        {
            try
            {
                InstrumentControl sgTest = new InstrumentControl();
                string id = sgTest.IdentifySignalGenerator(parameter.SGVISAAddress);
                if (id.ToUpper().IndexOf("N5182") != -1)
                {
                    iSgControl = (SGControl)mxg;
                    Log("MXG found!");
                }
                else
                {
                    iSgControl = (SGControl)psg;
                    Log("ESG/PSG found");
                }

                if (!iSgControl.GetInitializedStatus())
                {
                    Log("Connect to Signal Generator first.");
                    iSgControl.Initialize(parameter.SGVISAAddress);
                }
                string strWaveformToDownload = parameter.WaveformFileName;
                string strRead = "";
                //Log("Download Waveform with name TEST");
                //iSgControl.DownloadWaveform(strWaveformToDownload);

                Log("Setup SG");
                iSgControl.SetupSG(parameter.T1, parameter.T2, parameter.DutyCycle, parameter.SGFreqInMhz, parameter.SGPowerLevel, strWaveformToDownload, parameter.T1Adjustment, parameter.T2Adjustment);

                //iSgControl.SendCommand("sour:rad:arb:wav " + "\"TEST\"");
                //double WaveformSamplingRate = 0;
                //iSgControl.SendCommand("sour:rad:arb:scl:rate?");
                //iSgControl.Read(ref strRead);
                //WaveformSamplingRate = Convert.ToDouble(strRead);
                //int nTotalPoint = 0;
                //iSgControl.SendCommand("arb:waveform:poin?");
                //iSgControl.Read(ref strRead);
                //nTotalPoint = Convert.ToInt32(strRead);



                ////
                ////Waveform structure 1usec 132usec 36usec total 169usec. 
                //byte[] Marker1 = new byte[nTotalPoint];
                //int nFirstOffNum = (int)((1e-6 - parameter.T1) * WaveformSamplingRate);
                ////int nSignalNum = (int)(132e-6 * WaveformSamplingRate);

                //double totalLength = nTotalPoint / WaveformSamplingRate;
                //double offtimeLegnth = totalLength * (1 - parameter.DutyCycle);

                //int nLastOffNum = (int)((offtimeLegnth - 1e-6 - parameter.T2) * WaveformSamplingRate);

                //Marker1.Initialize();
                //for (int i = nFirstOffNum; i < (nTotalPoint - nLastOffNum); i++)
                //    Marker1[i] = 1;

                //Log("Marker Setting!");
                //iSgControl.DownloadMarker(Marker1, 1);
                //iSgControl.SendCommand("SOUR:RAD:ARB:MARK:SET \"TEST\",2,1,100,0");

                //iSgControl.SendCommand("ROUTE:CONN:EVENT1 M1");
                //iSgControl.SendCommand("ROUTE:CONN:bbtr M2");

                //iSgControl.SendCommand("ARB:state on");
                //iSgControl.SendCommand("output:mod:state on");

                //iSgControl.SendCommand("FREQ " + parameter.SGFreqInMhz.ToString(".000") + "MHZ");
                //iSgControl.SendCommand("RADio:ARB:MARKer:RFBLank OFF");
                //iSgControl.SendCommand("POWER " + parameter.SGPowerLevel.ToString(".00") + "DBM");
                //iSgControl.SendCommand("output on");

                Log("Download and play..");
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }

        private void btn6700OutputOn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!n6700x.Initialized)
                {
                    Log("Initialize Power Supply.");
                    n6700x.Initialize(parameter.N6700VISAAddress);
                }
                n6700x.SendCommand("SENSE:FUNC \"VOLT\",(@1)");
                n6700x.SendCommand("SOUR:VOLT:LEVEL:IMM:AMP " + parameter.Voltage.ToString() + ",(@1)");
                n6700x.SendCommand("OUTPUT:STATE 1" + ",(@1)");
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }

        private void btn6700OutputOff_Click(object sender, EventArgs e)
        {
            if (!n6700x.Initialized)
            {
                Log("Initialize Power Supply.");
                n6700x.Initialize(parameter.N6700VISAAddress);
            }
            n6700x.SendCommand("OUTPUT:STATE 0" + ",(@1)");

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.bButtonThreadActive = false;
            this.ButtonUpdateThread.Abort();
            Thread.Sleep(1000);
        }

        private void btnTestStatus_Click(object sender, EventArgs e)
        {
            if (bMainTestStarted)
            {
                bMainTestStarted = false;
                testState = TESTSTATUS.TESTABORTED;
            }
            else
            {
                bMainTestStarted = true;
                this.testThread = new Thread(new ThreadStart(MainTest));
                testThread.Start();
            }
        }

        private void btnSetupMXA_Click(object sender, EventArgs e)
        {
            try
            {
                // Select WLAN App
                xSA.SendCommand("INST:SEL WLAN");
                // Select 11ax 80Mhz
                xSA.SendCommand("RAD:STAN AX80");
                // configure EVM measurement
                xSA.SendCommand("CONF:EVM");
                // set External trigger to EXT1
                xSA.SendCommand("TRIG:EVM:SOUR EXT1");

                xSA.SendCommand("FREQ:CENT " + parameter.SGFreqInMhz + "MHZ");
                // Set input range 
                xSA.SendCommand("POWER:RANGE " + parameter.Range.ToString());


                //xSA.SendCommand("POWER:RANGE " + (PowerlevelToTest[PowerlevelLoop] + parameter.Range).ToString());
                // added for auto range clippnig : Jooyoung 
                xSA.SendCommand("SENSe:POWer:RF:RANGe:OPTimize IMMediate");
                // turn off average

                xSA.SendCommand("EVM:AVER:STAT OFF");
                // Equalizer training setting
                xSA.SendCommand("CALC:EVM:EQU:TMOD SDATA");

                //xSA.SendCommand("");
                //xSA.SendCommand("");

            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }

        private void btnMeasureEVM_Click(object sender, EventArgs e)
        {
            try
            {
                // Select Set Center Frequency
                string strRead = "";
                xSA.SendCommand("FREQ:CENT " + parameter.SGFreqInMhz.ToString() + "MHZ");
                xSA.SendCommand("READ:EVM?");
                xSA.Read(ref strRead);
                Log(" >EVM Measured : " + strRead.Split(',')[0]);

            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }

        void UpdateGraph(double[] Data)
        {
            //this.graph.GraphPane.Title.Text = "IMD Trace";
            //this.graph.GraphPane.XAxis.Title.Text = "Frequency";
            //this.graph.GraphPane.YAxis.Title.Text = "Amp";
            PointPairList result = new PointPairList();
            for (int i = 0; i < Data.Length; i++)
            {
                result.Add(i, Data[i]); // us unit display.
            }
            graph.GraphPane.CurveList.Clear();
            LineItem mycurve = graph.GraphPane.AddCurve("", result, Color.Red, SymbolType.None);
            mycurve.Line.Width = 3;
            graph.IsAutoScrollRange = true;
            graph.AxisChange();
            graph.Invalidate();
        }

        private void tsLogSetting_Click(object sender, EventArgs e)
        {
            if (bLoggingEverything)
            {
                bLoggingEverything = false;
                this.tsLogSetting.Text = "Enable";
                mxg.updateLog += new InstrumentControl.UpdateLog(Log);
                psg.updateLog += new InstrumentControl.UpdateLog(Log);
                xSA.updateLog += new InstrumentControl.UpdateLog(Log);
                n6700x.updateLog += new InstrumentControl.UpdateLog(Log);
            }
            else
            {
                bLoggingEverything = true;
                this.tsLogSetting.Text = "Disable";
                mxg.updateLog -= new InstrumentControl.UpdateLog(Log);
                psg.updateLog -= new InstrumentControl.UpdateLog(Log);
                xSA.updateLog -= new InstrumentControl.UpdateLog(Log);
                n6700x.updateLog -= new InstrumentControl.UpdateLog(Log);
            }
        }
    }
}
