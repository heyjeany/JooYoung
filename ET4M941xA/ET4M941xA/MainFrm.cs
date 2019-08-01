using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;

using Keysight.KtM941x;
using Ivi.Driver.Interop;
using Keysight.KtHvi;
using Keysight.KtM941xHvi;
using KeysightSD1;
using Agilent.AgM9300.Interop;
using System.Threading;
using Ivi.Visa.Interop;
namespace ET4M941xA
{
    public partial class MainFrm : Form
    {

        #region instrument 
        VXT2HVI vxt2Hvi = new VXT2HVI();
        M320xA m3202A = new M320xA();

        string refRFWaveformName = "Ref";
        double[] IQData;
        double[] awgWave;
        //HVI
        double SystemFrequency = 0;
        double SynchronizeFrequency = 0;
        string moduleInitOptions = "DriverSetup=Initialize=0,UseMemoryMappedIO=0,ShareAllVisaSessions=1";
        double CLKsyncFreqAWG = 10e6, CLKsysFreqAWG = 1e9;

        #endregion

        #region ET Setup predefined. 
        ETSETUP _30_72_Ver3 = new ETSETUP();
        ETSETUP _122_88_Ver3 = new ETSETUP();
        ETSETUP _137_60_Ver3 = new ETSETUP();
        ETSETUP _199_68_Ver3 = new ETSETUP();
        ETSETUP _250_00_Ver3 = new ETSETUP();

        ETSETUP _30_72_Ver4 = new ETSETUP();
        ETSETUP _122_88_Ver4 = new ETSETUP();
        ETSETUP _137_60_Ver4 = new ETSETUP();
        ETSETUP _199_68_Ver4 = new ETSETUP();
        ETSETUP _250_00_Ver4 = new ETSETUP();

        ETSETUP etSetup;
        #endregion

        List<DataList> originalData = new List<DataList>();
        List<DataList> targetData = new List<DataList>();
        double[] IData;
        double[] QData;
        UpSampler upsampler = new UpSampler();
        ControlModule ctrlParameter = new ControlModule();
        int nLogLineNum = 0;
        delegate void _del_UpdateRTB(string strMessage, bool bAppendCR = true);
        void Log(string strMessage, bool bAppendCR = true)
        {
            RichTextBox tRtb;
            tRtb = rbLog;
            if (tRtb.InvokeRequired)
            {
                _del_UpdateRTB recurFunc = new _del_UpdateRTB(Log);
                this.Invoke(recurFunc, new object[] { strMessage, bAppendCR });
            }
            else
            {
                if( nLogLineNum > 300 )
                {
                    nLogLineNum = 0;
                    tRtb.Clear();
                }
                if (strMessage.ToUpper() == "CLEAR")
                {
                    tRtb.Clear();
                }
                else
                {
                    nLogLineNum++;
                    tRtb.AppendText(strMessage);
                    if (bAppendCR)
                        tRtb.AppendText("\r");
                    tRtb.SelectionStart = tRtb.Text.Length;
                    tRtb.ScrollToCaret();
                }
            }
        }
        void Log2(string strMessage, bool bAppendCR = true)
        {
            RichTextBox tRtb;
            tRtb = rbLog2;
            if (tRtb.InvokeRequired)
            {
                _del_UpdateRTB recurFunc = new _del_UpdateRTB(Log2);
                this.Invoke(recurFunc, new object[] { strMessage, bAppendCR });
            }
            else
            {

                if (strMessage.ToUpper() == "CLEAR")
                {
                    tRtb.Clear();
                }
                else
                {
                    tRtb.AppendText(strMessage);
                    if (bAppendCR)
                        tRtb.AppendText("\r");
                    tRtb.SelectionStart = tRtb.Text.Length;
                    tRtb.ScrollToCaret();
                }
            }
        }
        void LogStatus(string strMessage, bool addCR = true)
        {
            string[] splitStr = strMessage.Split('#');

            if (stsMainFrm.InvokeRequired)
            {
                _del_UpdateRTB recurFunc = new _del_UpdateRTB(LogStatus);
                this.Invoke(recurFunc, new object[] { strMessage, addCR });
            }
            else
            {
                //UpdateGraphs();
                this.tslbStatus.Text = strMessage;
            }
        }

        private void btnLoadWaveformIntoMemory_Click(object sender, EventArgs e)
        {
            try
            {
                // 1msec loading..
                double time2Load = 1e-3;
                originalData.Clear();
                string testWaveformDir = @".\testwaveform\";
                string dataI = testWaveformDir + (this.cbWaveformSelection.SelectedIndex + 1).ToString() + ".i";
                string dataQ = testWaveformDir + (this.cbWaveformSelection.SelectedIndex + 1).ToString() + ".q";
                int nDataLength = GetTotalLine.TotalLines(dataI);
                int ETOSFactor = ctrlParameter.OSRFactor;
                double tOrigSRate = ctrlParameter.RFSamplingRateInMhz * 1e6; // 100Mhz as temp.
                int nData2Load = (int)(tOrigSRate * time2Load);
                nDataLength = nData2Load;
                Log("Total RF Sample Number is " + nDataLength.ToString());
                int ntotalDataNum = GetLineNum.TotalLines(testWaveformDir + (this.cbWaveformSelection.SelectedIndex + 1).ToString() + ".i");
                Log("Total Data Number in file is " + ntotalDataNum.ToString());
                Log("If total number is mismatched, data loaded will be trimmed and the rest will be filled with 0");
                double TargetSRate = tOrigSRate * ETOSFactor;
                IData = new double[nDataLength];
                QData = new double[nDataLength];
                Log("Read IData from " + dataI);
                FileStream fi = new FileStream(dataI, FileMode.Open);
                StreamReader si = new StreamReader(fi);
                FileStream fq = new FileStream(dataQ, FileMode.Open);
                StreamReader sq = new StreamReader(fq);
                for (int i = 0; i < ( ( ntotalDataNum < nDataLength ) ? ntotalDataNum : nDataLength ); i++)
                {
                    IData[i] = Convert.ToDouble(si.ReadLine()) / 8192;
                    QData[i] = Convert.ToDouble(sq.ReadLine()) / 8192;
                    var amp = Math.Sqrt(IData[i] * IData[i] + QData[i] * QData[i]);
                    originalData.Add(new DataList(i / tOrigSRate, amp));
                }
                Log("Waveform " + (this.cbWaveformSelection.SelectedIndex + 1).ToString() + " read done!!");
                Log(" > RF Waveform Length Loaded is " + (nDataLength / tOrigSRate * 1e3).ToString(".0000") + " mSec");
                si.Close(); sq.Close(); fi.Close(); fq.Close();

                // Build IQpair
                IQData = new double[nDataLength * 2];

                for (int i = 0; i < IQData.Length / 2; i++)
                {
                    IQData[2 * i] = IData[i];
                    IQData[2 * i + 1] = QData[i];

                }
                // Upconvert
                if (!ctrlParameter.Use1GhzFixedSamplingRate)
                {
                    Log("    -- Upconvert Envelop with factor " + ETOSFactor.ToString());
                    targetData = upsampler.UpSample(originalData, tOrigSRate, TargetSRate);
                }
                else
                {
                    Log("    -- Upconvert Envelop to 1Ghz. ");// + ETOSFactor.ToString());
                    targetData = upsampler.UpSample(originalData, tOrigSRate, 1e9);
                }
                //#endregion
                awgWave = new double[targetData.Count];
                for (int i = 0; i < awgWave.Length; i++)
                {
                    awgWave[i] = targetData[i].y;
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }

        private void connectInstrumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Log("Initialize start..");
                m3202A.InitializeAWG(ctrlParameter.slotNumber);
                if (!chkAWGOnlyInit.Checked)
                {
                    vxt2Hvi.InitializeM9300A();
                    vxt2Hvi.InitializeVXT2(ctrlParameter);
                    vxt2Hvi.InitializeHVI();
                }
                Log("done!");
            }
            catch( Exception ex )
            {
                Log(ex.Message);
            }
        }

        public MainFrm()
        {
            InitializeComponent();
            for (int i = 1; i <= 10; i++)
            {
                cbWaveformSelection.Items.Add("testWaveform" + i.ToString());
            }
            cbWaveformSelection.SelectedIndex = 8;
            this.pgConnectInitStatus.SelectedObject = ctrlParameter;
            btnLoadWaveformIntoMemory_Click(null, null);

            this.vxt2Hvi.updateLog += new BaseClass.UpdateLog(Log);
            this.m3202A.updateLog += new BaseClass.UpdateLog(Log);
            SynchronizeFrequency = -999;
            SystemFrequency = -999;

            // predefined setup found.. Subject to change...
            _30_72_Ver3.AWGTriggerDelay = 25;
            _30_72_Ver3.RFSamplingRateInMhz = 30.72;

            _122_88_Ver3.AWGTriggerDelay = 24;
            _122_88_Ver3.RFSamplingRateInMhz = 122.88;

            _137_60_Ver3.AWGTriggerDelay = 21;
            _137_60_Ver3.RFSamplingRateInMhz = 137.6;

            _199_68_Ver3.AWGTriggerDelay = 47;
            _199_68_Ver3.RFSamplingRateInMhz = 199.68;

            _250_00_Ver3.AWGTriggerDelay = 46;
            _250_00_Ver3.RFSamplingRateInMhz = 250;
            _250_00_Ver3.OSFactor = 4;

            _30_72_Ver4.RFSamplingRateInMhz = 30.72;
            _30_72_Ver4.AWGTriggerDelay = 26;

            _122_88_Ver4.RFSamplingRateInMhz = 122.88;
            _122_88_Ver4.AWGTriggerDelay = 0;
            _122_88_Ver4.BasebandDelay = 13;
            _122_88_Ver4.UseDummyWaveform = true;

            _137_60_Ver4.RFSamplingRateInMhz = 137.6;
            _137_60_Ver4.UseDummyWaveform = true;
            _137_60_Ver4.AWGTriggerDelay = 8;

            _199_68_Ver4.BasebandDelay = 34;

            _250_00_Ver4.AWGTriggerDelay = 18;
            _250_00_Ver4.UseDummyWaveform = true;
            _250_00_Ver4.OSFactor = 4;


            cbAWGHWVer.SelectedIndex = 0;
            rb30_72Mhz.Checked = true;
            etSetup = _30_72_Ver3;
            pgETSetup.SelectedObject = etSetup;
        }

        private void btnDebugQueueWFM_Click(object sender, EventArgs e)
        {
            try
            {
                Log("Queue Waveform");
                if( !chkAWGOnlyInit.Checked)
                vxt2Hvi.VXT2QueueWaveform(IQData, ctrlParameter);
                SystemFrequency = ctrlParameter.RFSamplingRateInMhz * 1e6 * ctrlParameter.OSRFactor;
                SynchronizeFrequency = -999;
                m3202A.AWGQueueWaveform(SystemFrequency, ref SynchronizeFrequency, awgWave, ctrlParameter);
            }
            catch( Exception ex )
            {
                Log(ex.Message);
            }
        }

        private void btnDebugSynchronizeModule_Click(object sender, EventArgs e)
        {
            try
            {
                Log("Sync Module");
                if( SynchronizeFrequency < 0 )
                {
                    Log("Sync Freq is abnormal : " + SynchronizeFrequency.ToString());
                    return;
                }
                SynchronizeModule();
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }

        private void btnDebugStartPlayback_Click(object sender, EventArgs e)
        {
            try
            {
                Log("Start play");
                if (!chkAWGOnlyInit.Checked)
                    vxt2Hvi.PlayWaveform(ctrlParameter);
                m3202A.PlayWaveform();

            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }

        private void btnDebugStop_Click(object sender, EventArgs e)
        {
            try
            {
                Log("Stop and ReArm");
                if (!chkAWGOnlyInit.Checked)
                    vxt2Hvi.Stop(ctrlParameter.SourceExtlTrigEnabled, chkReStartWaveformAfterStop.Checked);
                m3202A.Stop(chkReStartWaveformAfterStop.Checked);
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }

        private void btnDebugGenPXI0_2_Click(object sender, EventArgs e)
        {
            try
            {
                Log("Generate PXI0 Trigger");
                vxt2Hvi.GenerateTrigger();
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }

        private void btnAWGSyncFreqCheck_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(() => SyncFreqListUp());
            t.Start();
        }

        void SyncFreqListUp()
        {
            try
            {
                FileStream fs = new FileStream( ".\\System_Sync_Freq.dat", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("System Fequency, Sync Frequency");
                Log("AWG Init");
                m3202A.InitializeAWG( ctrlParameter.slotNumber);
                double startAWGSystemFreq = 400e6;
                double stopAWGSystemFreq = 1e9;
                double stepAWGSystemFreq = 0.01e6;
                double syncFreq = 0;
                for( double systemFreq = startAWGSystemFreq; systemFreq <= stopAWGSystemFreq; systemFreq += stepAWGSystemFreq)
                {
                    m3202A.FreqTest(systemFreq, ref syncFreq);
                    sw.WriteLine((systemFreq).ToString() +","+ (syncFreq ).ToString());
                    Log("*",false);
                }
                Log("Test done!");
                sw.Close();fs.Close();

            }
            catch( Exception ex )
            {
                Log(ex.Message);
            }
        }

        double ClockSyncFrequency = -999;

        private void btnStartWaveform_Click(object sender, EventArgs e)
        {
            //Log("Remove Event..");
            //this.vxt2Hvi.updateLog -= new BaseClass.UpdateLog(Log);
            //this.m3202A.updateLog -= new BaseClass.UpdateLog(Log);
            try
            {

                TickTime timer = new TickTime();
                //vxt2Hvi.Stop(false, false);
                m3202A.Stop(false);
                double tLoadWaveformTime;
                double tSyncTime = 0;
                double tFirstPlayTime = 0;
                double tStopTime = 0;
                double tSecondPlayTime = 0;

                vxt2Hvi.SetSyncOutputTriggering(ctrlParameter);
                // Load waveform into Memory. 
                this.btnLoadWaveformIntoMemory_Click(sender, e);
                ctrlParameter.PlaybackMode = chkVXTSingle.Checked ? ModulationPlaybackMode.Single : ModulationPlaybackMode.Continuous;
                ctrlParameter.queueMode = chkAWGSingle.Checked ? SD_QueueMode.ONE_SHOT : SD_QueueMode.CYCLIC;
                if (chkAWGSingle.Checked)
                    ctrlParameter.repeatCycle = 2;
                else
                    ctrlParameter.repeatCycle = 0;
                // QueueWaveform first
                //Log("Queue Waveform");
                //if (!chkAWGOnlyInit.Checked)
                SystemFrequency = ctrlParameter.RFSamplingRateInMhz * 1e6 * ctrlParameter.OSRFactor;

                timer.Start();
                vxt2Hvi.VXT2QueueWaveform(IQData, ctrlParameter);
                m3202A.AWGQueueWaveform((ctrlParameter.Use1GhzFixedSamplingRate ? 1e9 : SystemFrequency), ref SynchronizeFrequency, awgWave, ctrlParameter, ctrlParameter.DummyWaveformLength, chkUseDummyWaveform.Checked);
                tLoadWaveformTime = timer.GetElapsedTime();
                timer.Start();
                bool bSyncNeeded = true;
                if (ClockSyncFrequency == -999)
                {
                    ClockSyncFrequency = SynchronizeFrequency;
                }
                else
                {
                    if (ClockSyncFrequency != SynchronizeFrequency)
                        bSyncNeeded = true;
                    else
                        bSyncNeeded = false;
                }
                //bSyncNeeded = true;
                // Synchronize Frequency if needed. 
                bSyncNeeded = true;
                if (bSyncNeeded)
                {
                    //Log("Sync Module");
                    ClockSyncFrequency = SynchronizeFrequency;
                    if (SynchronizeFrequency < 0)
                    {
                        Log("Sync Freq is abnormal : " + SynchronizeFrequency.ToString());
                        return;
                    }
                    SynchronizeModule();
                    tSyncTime = timer.GetElapsedTime();
                }
                else
                {
                    m3202A.SynchronizeHVIModule();
                    tSyncTime = timer.GetElapsedTime();
                }
                // Start Playback
                //Log("Start play");
                timer.Start();
                //if (!chkAWGOnlyInit.Checked)
                vxt2Hvi.PlayWaveform(ctrlParameter);
                m3202A.PlayWaveform();

                // Generate Trigger 
                //Log("Generate PXI0 Trigger");
                //Thread.Sleep(100);
                vxt2Hvi.GenerateTrigger();
                
                if (SynchronizeFrequency == 80e3)
                {
                    Thread.Sleep(30);
                    //Thread.Sleep(ctrlParameter.SyncDelay);
                    vxt2Hvi.Stop();
                    m3202A.Stop(true);
                    vxt2Hvi.PlayWaveform(ctrlParameter);
                    m3202A.PlayWaveform();

                    // Generate Trigger 
                    //Log("Generate PXI0 Trigger");
                    //Thread.Sleep(1000);

                    vxt2Hvi.GenerateTrigger();
                    Thread.Sleep(30);
                }
                tFirstPlayTime = timer.GetElapsedTime();
                timer.Start();
                if (chkStopStartAfterQueue.Checked)
                {
                    // Additional code for stability
                    //vxt2Hvi.Stop(true,true);
                    //m3202A.Stop();
                    //if (SynchronizeFrequency == 80e3)
                    //{
                    //    //Thread.Sleep(30);
                    //    vxt2Hvi.Stop(true, true);
                    //    vxt2Hvi.PlayWaveform(ctrlParameter);
                    //    m3202A.PlayWaveform();

                    //    // Generate Trigger 
                    //    //Log("Generate PXI0 Trigger");
                    //    //Thread.Sleep(1000);
                    //    vxt2Hvi.GenerateTrigger();
                    //}
                    vxt2Hvi.Stop(true, false);

                    m3202A.Stop(false);
                    tStopTime = timer.GetElapsedTime();
                    // Start Playback
                    //Log("Start play");
                    //ctrlParameter.PlaybackMode = chkVXTSingle.Checked ? ModulationPlaybackMode.Single : ModulationPlaybackMode.Continuous;
                    //ctrlParameter.queueMode = chkAWGSingle.Checked ? SD_QueueMode.ONE_SHOT : SD_QueueMode.CYCLIC;
                    //if (!chkAWGOnlyInit.Checked)
                    vxt2Hvi.PlayWaveform(ctrlParameter);
                    m3202A.PlayWaveform();

                    // Generate Trigger 
                    //Log("Generate PXI0 Trigger");
                    //Thread.Sleep(1000);
                    vxt2Hvi.GenerateTrigger();
                    tSecondPlayTime = timer.GetElapsedTime();
                }

                Log("Loading Waveform : " + tLoadWaveformTime.ToString() + " mSec");
                Log("First Play Time : " + tFirstPlayTime.ToString() + " mSec");
                Log("Stop Time : " + tStopTime.ToString() + " mSec");
                Log("Second Play Time : " + tSecondPlayTime.ToString() + " mSec");
                Log("Total Time Taken : " + (tLoadWaveformTime + tFirstPlayTime + tStopTime + tSecondPlayTime).ToString() + " mSec");

            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
            //Log("Connect Event..");
            //this.vxt2Hvi.updateLog += new BaseClass.UpdateLog(Log);
            //this.m3202A.updateLog += new BaseClass.UpdateLog(Log);

        }

        private void btnStopWaveform_Click(object sender, EventArgs e)
        {
            try
            {
                Log("Stop and ReArm");
                if (!chkAWGOnlyInit.Checked)
                    vxt2Hvi.Stop(ctrlParameter.SourceExtlTrigEnabled, chkReStartWaveformAfterStop.Checked);
                m3202A.Stop(chkReStartWaveformAfterStop.Checked);
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }

        bool bRunThread = false;
        Thread collectThread = null;
        private void btnGaterOffset_Click(object sender, EventArgs e)
        {

            try
            {
                string strDir = @".\TestWaveform\";
                for( int i = 1; i <= 13; i++ )
                {
                    string[] allLines = File.ReadAllLines(strDir + i.ToString() + ".I");
                    FileStream fs = new FileStream(strDir + i.ToString() + ".I", FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);
                    foreach ( string t in allLines )
                    {
                        sw.WriteLine(t);
                    }
                    foreach (string t in allLines)
                    {
                        sw.WriteLine(t);
                    }
                    sw.Close(); fs.Close();
                    allLines = File.ReadAllLines(strDir + i.ToString() + ".Q");
                     fs = new FileStream(strDir + i.ToString() + ".Q", FileMode.Create);
                     sw = new StreamWriter(fs);
                    foreach (string t in allLines)
                    {
                        sw.WriteLine(t);
                    }
                    foreach (string t in allLines)
                    {
                        sw.WriteLine(t);
                    }
                    sw.Close(); fs.Close();
                }
            }
            catch( Exception ex )
            {
                Log(ex.Message);
                return;
            }


            if (bRunThread == true)
            {
                bRunThread = false;
            }
            else
            {
                collectThread = new Thread(() => CollectOffset());
                collectThread.Start();
                bRunThread = true;
            }
        }

        private void LoadSquareWaveform( double RFSamplingRateInMhz)
        {
            try
            {

                // 1msec loading..
                double time2Load = 1e-3;
                Log("Filling Waveform with constant value. ");
                //originalData.Clear();
                //string testWaveformDir = @".\testwaveform\";
                //string dataI = testWaveformDir + (this.cbWaveformSelection.SelectedIndex + 1).ToString() + ".i";
                //string dataQ = testWaveformDir + (this.cbWaveformSelection.SelectedIndex + 1).ToString() + ".q";
                int nDataLength;// = GetTotalLine.TotalLines(dataI);
                int ETOSFactor = ctrlParameter.OSRFactor;
                double tOrigSRate = RFSamplingRateInMhz * 1e6; // 100Mhz as temp.
                int nData2Load = (int)(tOrigSRate * time2Load);
                nDataLength = nData2Load;

                double TargetSRate = tOrigSRate * ETOSFactor;
                IData = new double[nDataLength];
                QData = new double[nDataLength];
                //Log("Read IData from " + dataI);
                //FileStream fi = new FileStream(dataI, FileMode.Open);
                //StreamReader si = new StreamReader(fi);
                //FileStream fq = new FileStream(dataQ, FileMode.Open);
                //StreamReader sq = new StreamReader(fq);
                originalData.Clear();
                for (int i = 0; i < nDataLength; i++)
                {
                    IData[i] = 0.5;// Convert.ToDouble(si.ReadLine()) / 8192;
                    QData[i] = 0.5;// Convert.ToDouble(sq.ReadLine()) / 8192;
                    var amp = Math.Sqrt(IData[i] * IData[i] + QData[i] * QData[i]);
                    originalData.Add(new DataList(i / tOrigSRate, amp));
                }
                //Log("Waveform " + (this.cbWaveformSelection.SelectedIndex + 1).ToString() + " read done!!");
                Log("Waveform Length Loaded is " + (nDataLength / tOrigSRate * 1e3).ToString(".0000") + " mSec");
                //si.Close(); sq.Close(); fi.Close(); fq.Close();

                // Build IQpair
                IQData = new double[nDataLength * 2];

                for (int i = 0; i < IQData.Length / 2; i++)
                {
                    IQData[2 * i] = IData[i];
                    IQData[2 * i + 1] = QData[i];

                }
                // Upconvert
                Log("Upconvert Envelop with factor " + ETOSFactor.ToString());
                targetData = upsampler.UpSample(originalData, tOrigSRate, TargetSRate);
                //#endregion
                awgWave = new double[IQData.Length / 2 * ETOSFactor];
                for (int i = 0; i < awgWave.Length; i++)
                {
                    awgWave[i] = targetData[i].y;
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }


        void CollectOffset()
        {
            // Waveform is a kind of square waveform for just 1 msec

            FormattedIO488 io488 = null;
            ResourceManager rm = null;
            if (io488 == null)
            {
                io488 = new FormattedIO488();
            }
            if (rm == null)
            {
                rm = new ResourceManager();
            }
            try
            {
                io488.IO = (IMessage)rm.Open("TCPIP0::10.112.39.165::inst0::INSTR", AccessMode.NO_LOCK, 99999, "");
                io488.IO.Timeout = 1000;
                io488.IO.WriteString("*IDN?\n");
                string strMessage = io488.ReadString();
                if (strMessage.Length > 1)
                {

                    Log("Scope connection Done.");
                    //bScopeConnected = true;
                }
                else
                {
                    Log("Scope connection Failed!");
                    bRunThread = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                bRunThread = false;
                Log(ex.Message);
                return;
            }
            io488.IO.WriteString("MEASURE:clear\n");
            io488.IO.WriteString("MEASURE:SOURCE CHANNEL2,CHANNEL3\n");
            io488.IO.WriteString("MESURE: DEFIN DELTATIME: RISING, 1, MIDDLE, RISING, 1, MIDDLE\n");
            io488.IO.WriteString("MEASURE:DELTATIME\n");

            FileStream fs; StreamWriter sw;
            List<string> result = new List<string>();

            #region AWG Continuous and VXT2 Continuous
            // AWG Continuous Mode measurement
            ctrlParameter.PlaybackMode = ModulationPlaybackMode.Continuous;// : ModulationPlaybackMode.Continuous;
            ctrlParameter.queueMode = SD_QueueMode.CYCLIC;// SD_QueueMode.ONE_SHOT;// : SD_QueueMode.CYCLIC;
            ctrlParameter.repeatCycle = 0; //2;
                                           //ctrlParameter.repeatCycle = 0;
                                           // QueueWaveform first

            //if (!chkAWGOnlyInit.Checked)

            int nTotalRepeat = (int)((200 - 80) / 0.01)*2;
            int nCurrentPos = 0;
            TickTime timer = new TickTime();
            double elapsedTime = 0;
            for ( double freqToTest = 80; freqToTest < 200; freqToTest += 0.01 )
            {
                timer.Start();
                ctrlParameter.RFSamplingRateInMhz = freqToTest;
                LoadSquareWaveform(freqToTest);
                Log("Queue Waveform");
                
                vxt2Hvi.VXT2QueueWaveform(IQData, ctrlParameter);
                SystemFrequency = freqToTest * 1e6 * ctrlParameter.OSRFactor;
                SynchronizeFrequency = -999;
                m3202A.AWGQueueWaveform(SystemFrequency, ref SynchronizeFrequency, awgWave, ctrlParameter);

                bool bSyncNeeded = true;
                if (ClockSyncFrequency == -999)
                {
                    ClockSyncFrequency = SynchronizeFrequency;
                }
                else
                {
                    if (ClockSyncFrequency != SynchronizeFrequency)
                        bSyncNeeded = true;
                    else
                        bSyncNeeded = false;
                }

                // Synchronize Frequency if needed. 
                if (bSyncNeeded)
                {
                    Log("Sync Module");
                    ClockSyncFrequency = SynchronizeFrequency;
                    if (SynchronizeFrequency < 0)
                    {
                        Log("Sync Freq is abnormal : " + SynchronizeFrequency.ToString());
                        return;
                    }
                    SynchronizeModule();
                }
                // Start Playback
                Log("Start play");
                //if (!chkAWGOnlyInit.Checked)
                vxt2Hvi.PlayWaveform(ctrlParameter);
                m3202A.PlayWaveform();
                // Generate Trigger 
                //Log("Generate PXI0 Trigger");
                vxt2Hvi.GenerateTrigger();
                vxt2Hvi.Stop();
                m3202A.Stop();
                m3202A.PlayWaveform();
                vxt2Hvi.GenerateTrigger();
                //Thread.Sleep(500);
                // Measure from Scope. Using Existing setting
                try
                {
                  
                    Thread.Sleep(50);
                    io488.IO.WriteString("MEASURE:DELTATIME?\n");
                    string returnStr = io488.ReadString();
                    //Thread.Sleep(50);
                    Log2((SystemFrequency / 1e6).ToString(".000") + "=" + returnStr, false );
                    result.Add((SystemFrequency / 1e6).ToString(".000") + " : " + Math.Abs(Convert.ToDouble(returnStr)).ToString());
                    Log((SystemFrequency / 1e6).ToString(".000") + "=" + returnStr, false);
                }
                catch( Exception ex )
                {
                    Log("Scope control Error. Keep working on!! - " + (SystemFrequency / 1e6).ToString(".000"));
                    Log2((SystemFrequency / 1e6).ToString(".000") + "=0.000");
                }
                //break;
                if (bRunThread == false)
                {
                    Log("Test Interruped by User..");
                    return;
                }

                nCurrentPos++;
                elapsedTime = timer.GetElapsedTime();
                double EstimatedTimeLeft = elapsedTime / 1e3 * (nTotalRepeat - nCurrentPos);
                LogStatus(nCurrentPos.ToString() + "/" + nTotalRepeat.ToString() + " : Estimate time left " + EstimatedTimeLeft.ToString(".00") + "Sec");


            }

            fs = new FileStream(".\\VXT2_C_AWG_C.dat", FileMode.Create);
            sw = new StreamWriter(fs);
            sw.WriteLine("[AWGClockFrequency Vs Offset]");
            sw.WriteLine("//AWG Continuous, VXT2 Continuous");

            foreach (string t in result)
            {
                sw.WriteLine(t);
            }
            sw.Close(); fs.Close();

            #endregion



            #region AWG Single 2 play and VXT2 Continuous
            // AWG Continuous Mode measurement
            ctrlParameter.PlaybackMode = ModulationPlaybackMode.Continuous;// : ModulationPlaybackMode.Continuous;
            ctrlParameter.queueMode =  SD_QueueMode.ONE_SHOT;// : SD_QueueMode.CYCLIC;
            ctrlParameter.repeatCycle = 2;
                                           //ctrlParameter.repeatCycle = 0;
                                           // QueueWaveform first

            //if (!chkAWGOnlyInit.Checked)


            for (double freqToTest = 80; freqToTest < 200; freqToTest += 0.01)
            {
                timer.Start();
                ctrlParameter.RFSamplingRateInMhz = freqToTest;
                LoadSquareWaveform(freqToTest);
                Log("Queue Waveform");

                vxt2Hvi.VXT2QueueWaveform(IQData, ctrlParameter);
                SystemFrequency = freqToTest * 1e6 * ctrlParameter.OSRFactor;
                SynchronizeFrequency = -999;
                m3202A.AWGQueueWaveform(SystemFrequency, ref SynchronizeFrequency, awgWave, ctrlParameter);

                bool bSyncNeeded = true;
                if (ClockSyncFrequency == -999)
                {
                    ClockSyncFrequency = SynchronizeFrequency;
                }
                else
                {
                    if (ClockSyncFrequency != SynchronizeFrequency)
                        bSyncNeeded = true;
                    else
                        bSyncNeeded = false;
                }

                // Synchronize Frequency if needed. 
                if (bSyncNeeded)
                {
                    Log("Sync Module");
                    ClockSyncFrequency = SynchronizeFrequency;
                    if (SynchronizeFrequency < 0)
                    {
                        Log("Sync Freq is abnormal : " + SynchronizeFrequency.ToString());
                        return;
                    }
                    SynchronizeModule();
                }
                // Start Playback
                Log("Start play");
                //if (!chkAWGOnlyInit.Checked)
                vxt2Hvi.PlayWaveform(ctrlParameter);
                m3202A.PlayWaveform();
                // Generate Trigger 
                Log("Generate PXI0 Trigger");
                vxt2Hvi.GenerateTrigger();
                vxt2Hvi.Stop();
                m3202A.Stop();
                m3202A.PlayWaveform();
                vxt2Hvi.GenerateTrigger();
                //Thread.Sleep(500);
                // Measure from Scope. Using Existing setting
                try
                {
                    //io488.IO.WriteString("MEASURE:clear\n");
                    //io488.IO.WriteString("MEASURE:SOURCE CHANNEL2,CHANNEL3\n");
                    //io488.IO.WriteString("MESURE: DEFIN DELTATIME: RISING, 1, MIDDLE, RISING, 1, MIDDLE\n");
                    //io488.IO.WriteString("MEASURE:DELTATIME\n");
                    Thread.Sleep(50);
                    io488.IO.WriteString("MEASURE:DELTATIME?\n");
                    string returnStr = io488.ReadString();
                    //Thread.Sleep(50);
                    Log2((SystemFrequency / 1e6).ToString(".000") + "=" + returnStr, false);
                    result.Add((SystemFrequency / 1e6).ToString(".000") + " : " + Math.Abs(Convert.ToDouble(returnStr)).ToString());
                    Log((SystemFrequency / 1e6).ToString(".000") + "=" + returnStr, false);
                }
                catch (Exception ex)
                {
                    Log("Scope control Error. Keep working on!! - " + (SystemFrequency / 1e6).ToString(".000"));
                    Log2((SystemFrequency / 1e6).ToString(".000") + "=0.000");
                }
                //break;
                if (bRunThread == false)
                {
                    Log("Test Interruped by User..");
                    return;
                }

                nCurrentPos++;
                elapsedTime = timer.GetElapsedTime();
                double EstimatedTimeLeft = elapsedTime / 1e3 * (nTotalRepeat - nCurrentPos);
                LogStatus(nCurrentPos.ToString() + "/" + nTotalRepeat.ToString() + " : Estimate time left " + EstimatedTimeLeft.ToString(".00") + "Sec");

            }
            fs = new FileStream(".\\VXT2_C_AWG_S2.dat", FileMode.Create);
            sw = new StreamWriter(fs);
            sw.WriteLine("[AWGClockFrequency Vs Offset]");
            sw.WriteLine("//AWG Single, VXT2 Continuous");
            foreach (string t in result)
            {
                sw.WriteLine(t);
            }
            sw.Close(); fs.Close();
            #endregion

            Log("Test Finised..");
            bRunThread = false;
        }

        private void btnSetVXTBBDelay_Click(object sender, EventArgs e)
        {
            try
            {
                vxt2Hvi.SetBasebandDelay(Convert.ToDouble(txtVXTBBDelay.Text) * 1e-9);
            }
            catch( Exception ex )
            {
                Log(ex.Message);
            }
        }

        private void txtVXTBBDelay_ValueChanged(object sender, EventArgs e)
        {
            btnSetVXTBBDelay_Click(sender, e);
        }

        private void btnRestartWaveform_Click(object sender, EventArgs e)
        {
            try
            {
                //// Load waveform into Memory. 
                //this.btnLoadWaveformIntoMemory_Click(sender, e);
                //ctrlParameter.PlaybackMode = chkVXTSingle.Checked ? ModulationPlaybackMode.Single : ModulationPlaybackMode.Continuous;
                //ctrlParameter.queueMode = chkAWGSingle.Checked ? SD_QueueMode.ONE_SHOT : SD_QueueMode.CYCLIC;
                //if (chkAWGSingle.Checked)
                //    ctrlParameter.repeatCycle = 2;
                //else
                //    ctrlParameter.repeatCycle = 0;
                //// QueueWaveform first
                //Log("Queue Waveform");
                ////if (!chkAWGOnlyInit.Checked)
                //vxt2Hvi.VXT2QueueWaveform(IQData, ctrlParameter);
                //SystemFrequency = ctrlParameter.RFSamplingRateInMhz * 1e6 * ctrlParameter.OSRFactor;


                //m3202A.AWGQueueWaveform((ctrlParameter.Use1GhzFixedSamplingRate ? 1e9 : SystemFrequency), ref SynchronizeFrequency, awgWave, ctrlParameter, ctrlParameter.DummyWaveformLength, chkUseDummyWaveform.Checked);

                //bool bSyncNeeded = true;
                //if (ClockSyncFrequency == -999)
                //{
                //    ClockSyncFrequency = SynchronizeFrequency;
                //}
                //else
                //{
                //    if (ClockSyncFrequency != SynchronizeFrequency)
                //        bSyncNeeded = true;
                //    else
                //        bSyncNeeded = false;
                //}

                //// Synchronize Frequency if needed. 
                //if (bSyncNeeded)
                //{
                //    Log("Sync Module");
                //    ClockSyncFrequency = SynchronizeFrequency;
                //    if (SynchronizeFrequency < 0)
                //    {
                //        Log("Sync Freq is abnormal : " + SynchronizeFrequency.ToString());
                //        return;
                //    }
                //    SynchronizeModule();
                //}
                vxt2Hvi.Stop(true,false);
                m3202A.Stop(false);
                // Start Playback
                Log("Start play");
                ctrlParameter.PlaybackMode = chkVXTSingle.Checked ? ModulationPlaybackMode.Single : ModulationPlaybackMode.Continuous;
                ctrlParameter.queueMode = chkAWGSingle.Checked ? SD_QueueMode.ONE_SHOT : SD_QueueMode.CYCLIC;
                //if (!chkAWGOnlyInit.Checked)
                vxt2Hvi.PlayWaveform(ctrlParameter);
                m3202A.PlayWaveform();

                // Generate Trigger 
                Log("Generate PXI0 Trigger");
                //Thread.Sleep(1000);
                vxt2Hvi.GenerateTrigger();

            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }

        private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            vxt2Hvi.VXT2Close();
        }

        private void Event_ETSetupChange(object sender, EventArgs e)
        {
            switch( cbAWGHWVer.SelectedIndex )
            {
                case 0:
                    if (rb30_72Mhz.Checked)
                        etSetup = _30_72_Ver3;
                    if (rb122_88Mhz.Checked)
                        etSetup = _122_88_Ver3;
                    if (rb137_60Mhz.Checked)
                        etSetup = _137_60_Ver3;
                    if (rb199_68Mhz.Checked)
                        etSetup = _199_68_Ver3;
                    if (rb250Mhz.Checked)
                        etSetup = _250_00_Ver3;
                    break;
                case 1:
                    if (rb30_72Mhz.Checked)
                        etSetup = _30_72_Ver4;
                    if (rb122_88Mhz.Checked)
                        etSetup = _122_88_Ver4;
                    if (rb137_60Mhz.Checked)
                        etSetup = _137_60_Ver4;
                    if (rb199_68Mhz.Checked)
                        etSetup = _199_68_Ver4;
                    if (rb250Mhz.Checked)
                        etSetup = _250_00_Ver4;
                    break;
            }
            this.pgETSetup.SelectedObject = etSetup;
        }

        private void btnApplySetup_Click(object sender, EventArgs e)
        {
            ctrlParameter.RFSamplingRateInMhz = etSetup.RFSamplingRateInMhz;
            ctrlParameter.AWGTriggerDelay = etSetup.AWGTriggerDelay;
            ctrlParameter.OSRFactor = etSetup.OSFactor;
            chkUseDummyWaveform.Checked = etSetup.UseDummyWaveform;
            txtVXTBBDelay.Value = (decimal)etSetup.BasebandDelay;

            this.pgConnectInitStatus.Refresh();
        }

        private void btnPXI0FromRef_Click(object sender, EventArgs e)
        {
            try
            {
                vxt2Hvi.GeneratePXI0();
            }
            catch( Exception ex )
            {
                Log(ex.Message);
            }
        }

        public void SynchronizeModule()
        {
            try
            {
                Log("HVI module set.");
                vxt2Hvi.SynchronizeHVIModule(SynchronizeFrequency);
                Log("AWG Set.");
                m3202A.SynchronizeHVIModule();
                Log("Sync by PXI0 Trigger.");
                Thread.Sleep(1000);
                vxt2Hvi.GenerateTrigger();
            }
            catch( Exception ex )
            {
                Log(ex.Message);
            }
        }



    }

}


