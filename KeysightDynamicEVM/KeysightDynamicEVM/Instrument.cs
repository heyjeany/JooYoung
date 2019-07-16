using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ivi.Visa.Interop;
using System.Threading;
namespace KeysightDynamicEVM
{
    public interface SGControl
    {
        void DownloadWaveform(string strFileName);
        void DownloadMarker(byte[] MarkerBlock, int nMarker);
        void SetupSG(double T1, double T2, double DutyCycle, double freqInMhz, double pwrLevel, string str2Download, double T1Adjustment = 0, double T2Adjustment = 0);
        void SendCommand(string strcmd);
        void Read(ref string returnStr);
        bool Initialize(string strVISAAddress);
        bool GetInitializedStatus();
        void Status(string Message);
        void Log(string strMessage, bool bAppendCR = true);

    }
    public class InstrumentControl
    {
        #region UI Update
        public delegate void UpdateStatus(string Message);
        public event UpdateStatus updateStatus;
        public delegate void UpdateLog(string strMessage, bool bAppendCR = true);
        public event UpdateLog updateLog;
        public void Status(string Message)
        {
            if (updateStatus != null)
                updateStatus(Message);
        }
        public void Log(string strMessage, bool bAppendCR = true)
        {
            if (updateLog != null)
                updateLog(strMessage, bAppendCR);
        }
        #endregion
        protected FormattedIO488 ioM = null;
        protected ResourceManager rmM = null;
        public bool Initialized = false;
        protected string InstrumentID = "Generic";
        public bool bIsSimulationMode = false;
        protected string WaveformName = "SEMCOTEST";
        public InstrumentControl()
        {
        }


        public virtual bool Initialize(string strVISAAddress)
        {
            try
            {
                if( bIsSimulationMode )
                {
                    Initialized = true;
                    return true;
                }
                if (ioM == null)
                {
                    ioM = new FormattedIO488();
                    rmM = new ResourceManager();
                }
                ioM.IO = (IMessage)rmM.Open(strVISAAddress, AccessMode.NO_LOCK, 10000, "");
                ioM.IO.Timeout = 10000;
                Initialized = true;
                return true;
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                Initialized = false;
                return false;
            }
        }

        public string IdentifySignalGenerator( string strVISAAddress)
        {
            string strRet = "NOINSTRUMENT";
            try
            {
                if (ioM == null)
                {
                    ioM = new FormattedIO488();
                    rmM = new ResourceManager();
                }
                ioM.IO = (IMessage)rmM.Open(strVISAAddress, AccessMode.NO_LOCK, 10000, "");
                ioM.IO.Timeout = 10000;
                Initialized = true;
                this.SendCommand("*idn?");
                this.Read(ref strRet);
                ioM.IO.Close();
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                Initialized = false;
                //r//eturn false;
            }
            return strRet;
        }

        public void SendCommand(string strCmd)
        {
            try
            {
                if( !bIsSimulationMode )
                {
                    ioM.IO.WriteString(strCmd + "\n");
                }
                Log(InstrumentID + " -> " + strCmd);
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                throw ex;
            }
        }
        public void Read(ref string returnStr)
        {
            try
            {
                if( bIsSimulationMode )
                {
                    returnStr = "SIMULATION";
                    Log(InstrumentID + " <- " + returnStr);
                    return;
                }
                returnStr = "";
                returnStr = ioM.ReadString();
                Log(InstrumentID + " <- " + returnStr);

            }
            catch (Exception ex)
            {
                Log(ex.Message);
                throw ex;
            }
        }
    }

    public class N6700x:InstrumentControl
    {
        public N6700x()
        {
            this.InstrumentID = "N6700x";
        }
    }

    public class XSA:InstrumentControl
    {
        public XSA()
        {
            this.InstrumentID = "MXA";
        }
        public override bool Initialize(string strVISAAddress)
        {
            try
            {
                if (bIsSimulationMode)
                {
                    Initialized = true;
                    return true;
                }
                if (ioM == null)
                {
                    ioM = new FormattedIO488();
                    rmM = new ResourceManager();
                }
                ioM.IO = (IMessage)rmM.Open(strVISAAddress, AccessMode.NO_LOCK, 10000, "");
                ioM.IO.Timeout = 10000;

                SendCommand("*IDN?");
                string readStr = "";
                Read(ref readStr);
                if( readStr.Contains("VXT"))
                {
                    this.InstrumentID = "VXT";
                }
                else if ( readStr.Contains("MXA") | readStr.Contains("PXA")| readStr.Contains("EXA") | readStr.Contains("UXA"))
                {
                    this.InstrumentID = "xSA";
                }


                Initialized = true;

                return true;
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                Initialized = false;
                return false;
            }
        }
        public void SetAttenuator( double range2Set )
        {
            try
            {
                if( InstrumentID == "VXT" )
                {
                    SendCommand("POWER:RANGE " + (range2Set).ToString());
                    SendCommand("SENSe:POWer:RF:RANGe:OPTimize IMMediate");
                }
                else
                {
                    SendCommand("POWER:ATT " + range2Set.ToString() + "DBM");
                }
            }
            catch( Exception ex )
            {
                Log(ex.Message);
                throw ex;
            }
        }
    }

    public class MXG : InstrumentControl, SGControl
    {
        //public bool Initialized = false;
        public MXG()
        {
            this.InstrumentID = "MXG";
        }
        public bool GetInitializedStatus()
        {
            return base.Initialized;
        }
        public void DownloadWaveform(string strFileName)
        {
            try
            {
                if( bIsSimulationMode )
                {
                    Log("MXG in Simulation Mode..");
                    return;
                }
                byte[] dataBlock = System.IO.File.ReadAllBytes(strFileName);
                ioM.WriteIEEEBlock(":MEM:DATA \"SWFM1:TEST\",", dataBlock, true);
            }
            catch (Exception ex)
            {
                this.Log(ex.Message);
                throw ex;
            }
        }
        public void DownloadMarker(byte[] MarkerBlock, int nMarker)
        {
            try
            {
                if (bIsSimulationMode)
                {
                    Log("MXG in Simulation Mode..");
                    return;
                }
                //byte[] dataBlock = System.IO.File.ReadAllBytes(strFileName);
                ioM.WriteIEEEBlock(":MEM:DATA \"MKR" + nMarker.ToString() + ":TEST\",", MarkerBlock, true);
            }
            catch (Exception ex)
            {
                this.Log(ex.Message);
                throw ex;
            }
        }
        public void SetupSG( double T1, double T2, double DutyCycle, double freqInMhz, double pwrLevel, string str2Download,double T1Adjustment = 0, double T2Adjustment = 0)
        {
            try
            {
                if (bIsSimulationMode)
                {
                    Log("MXG in Simulation Mode..");
                    return;
                }

                string strWaveformToDownload = str2Download;
                string strRead = "";
                Log("Download Waveform with name TEST");
                this.DownloadWaveform(strWaveformToDownload);
                Log("Select Waveform");
                this.SendCommand("sour:rad:arb:wav " + "\"TEST\"");
                double WaveformSamplingRate = 0;
                this.SendCommand("sour:rad:arb:scl:rate?");
                this.Read(ref strRead);
                WaveformSamplingRate = Convert.ToDouble(strRead);
                int nTotalPoint = 0;
                this.SendCommand("arb:waveform:poin?");
                this.Read(ref strRead);
                nTotalPoint = Convert.ToInt32(strRead);

                //
                //Waveform structure 1usec 132usec 36usec total 169usec. 
                byte[] Marker1 = new byte[nTotalPoint];
                int nFirstOffNum = (int)((1e-6 - T1) * WaveformSamplingRate);

                //int nSignalNum = (int)(132e-6 * WaveformSamplingRate);
                double totalLength = nTotalPoint / WaveformSamplingRate;
                double offtimeLegnth = totalLength * (1 - DutyCycle);

                //int nLastOffNum = (int)((36e-6 - T2) * WaveformSamplingRate);
                int nLastOffNum = (int)((offtimeLegnth - 1e-6 - T2) * WaveformSamplingRate);
                Marker1.Initialize();
                for (int i = nFirstOffNum; i < (nTotalPoint - nLastOffNum); i++)
                    Marker1[i] = 1;

                Log("Marker Setting!");
                this.DownloadMarker(Marker1, 1);
                this.SendCommand("SOUR:RAD:ARB:MARK:SET \"TEST\",2,1,100,0");

                this.SendCommand("ROUTE:CONN:EVENT1 M1");
                this.SendCommand("ROUTE:CONN:bbtr M2");

                this.SendCommand("ARB:state on");
                this.SendCommand("output:mod:state on");

                this.SendCommand("FREQ " + freqInMhz.ToString(".000") + "MHZ");
                this.SendCommand("RADio:ARB:MARKer:RFBLank OFF");
                this.SendCommand("POWER " + pwrLevel.ToString(".00") + "DBM");
                this.SendCommand("output on");
            }
            catch( Exception ex )
            {
                Log(ex.Message);
                throw ex;
            }
        }
    }
    public class PSG : InstrumentControl,SGControl
    {
        public PSG()
        {
            this.InstrumentID = "PSG";
        }
        public bool GetInitializedStatus()
        {
            return base.Initialized;
        }
        public void DownloadWaveform(string strFileName)
        {
            try
            {
                if (bIsSimulationMode)
                {
                    Log("PSG in Simulation Mode..");
                    return;
                }
                byte[] dataBlock = System.IO.File.ReadAllBytes(strFileName);
                ioM.WriteIEEEBlock(":MEM:DATA \"SWFM1:" +WaveformName + "\",", dataBlock, true);
            }
            catch (Exception ex)
            {
                this.Log(ex.Message);
                throw ex;
            }
        }
        public void DownloadMarker(byte[] MarkerBlock, int nMarker)
        {
            try
            {
                if (bIsSimulationMode)
                {
                    Log("MXG in Simulation Mode..");
                    return;
                }
                //.nMarker = 2;
                //byte[] dataBlock = System.IO.File.ReadAllBytes(strFileName);
                ioM.WriteIEEEBlock(":MEM:DATA \"MKR" + nMarker.ToString() + ":" + WaveformName + ",", MarkerBlock, true);
            }
            catch (Exception ex)
            {
                this.Log(ex.Message);
                throw ex;
            }
        }
        public void SetupSG(double T1, double T2, double DutyCycle, double freqInMhz, double pwrLevel, string str2Download, double T1Adjustment = 0, double T2Adjustment = 0)
        {
            try
            {
                if (bIsSimulationMode)
                {
                    Log("MXG in Simulation Mode..");
                    return;
                }
                
                string strWaveformToDownload = str2Download;
                string strRead = "";
                Log("Download Waveform with name " + this.WaveformName);
                this.DownloadWaveform(strWaveformToDownload);
                Log("Select Waveform");
                this.SendCommand("sour:rad:arb:wav " + "\"" + WaveformName + "\"");
                double WaveformSamplingRate = 0;
                this.SendCommand("sour:rad:arb:scl:rate?");
                this.Read(ref strRead);
                WaveformSamplingRate = Convert.ToDouble(strRead);

                this.SendCommand("MMEM:CAT? \"WFM1:\"");
                Thread.Sleep(500);
                this.Read(ref strRead);


                string[] splitStr = strRead.Split(new string[] { "\",\"" }, StringSplitOptions.None);
                
                int nTotalPoint = 0;
                foreach( string t in splitStr )
                {
                    if( t.ToUpper().IndexOf(WaveformName.ToUpper()) != -1 )
                    {
                        nTotalPoint =(int)( Convert.ToInt32(t.Split(',')[2]) / 4);
                        break;
                    }
                }
                //this.SendCommand("arb:waveform:poin?");
                //this.Read(ref strRead);
                //nTotalPoint = Convert.ToInt32(strRead);

                //
                //Waveform structure 1usec 132usec 36usec total 169usec. 
                byte[] Marker1 = new byte[nTotalPoint];
                int nFirstOffNum = (int)((1e-6 - T1 + T1Adjustment) * WaveformSamplingRate);

                //int nSignalNum = (int)(132e-6 * WaveformSamplingRate);
                double totalLength = nTotalPoint / WaveformSamplingRate;
                double offtimeLegnth = totalLength * (1 - DutyCycle);
                double ontimeLength = totalLength * (DutyCycle);
                //int nLastOffNum = (int)((36e-6 - T2) * WaveformSamplingRate);
                int nLastOffNum = (int)((offtimeLegnth - 1e-6 - T2) * WaveformSamplingRate);
                Marker1.Initialize();
                int nOnLength = (int)((ontimeLength + T2 - T2Adjustment) * WaveformSamplingRate);
                //nOnLength = 3000;
                //for (int i = nFirstOffNum; i < (nTotalPoint - nLastOffNum); i++)
                //    Marker1[i] = 1;

                Log("Marker Setting!");
                this.SendCommand(":SOUR:RAD:ARB:MARKER:CLEAR \"" + WaveformName + "\",2,1,"+ nTotalPoint.ToString());

                //this.DownloadMarker(Marker1, 1);
                this.SendCommand(":SOUR:RAD:ARB:MARKER:SET \"" + WaveformName + "\",2," + nFirstOffNum.ToString() + "," + (nFirstOffNum + nOnLength ).ToString() + ",0");
                //this.SendCommand("SOUR:RAD:ARB:MARK:SET \"TEST\",2,1,100,0");
                //this.SendCommand("SOUR:RAD:ARB:MARK:SET " + WaveformName + ",2,1,100,0");

                //this.SendCommand("ROUTE:CONN:EVENT1 M1");
                //this.SendCommand("ROUTE:CONN:bbtr M2");

                this.SendCommand(":rad:ARB:state on");
                this.SendCommand("output:mod:state on");

                this.SendCommand("FREQ " + freqInMhz.ToString(".000") + "MHZ");
                this.SendCommand("RADio:ARB:MARKer:RFBLank OFF");
                this.SendCommand("POWER " + pwrLevel.ToString(".00") + "DBM");
                this.SendCommand("output on");
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                throw ex;
            }
        }
    }
}
