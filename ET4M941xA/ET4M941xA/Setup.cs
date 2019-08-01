using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;
using Keysight.KtM941x;
using KeysightSD1;

namespace ET4M941xA
{
    public enum AWGTRIGGERMODE
    {
        IMMEDIATE = 0,
        EXTERNATRIGGER = 2
    }
    public enum VXTTRIGGERMODE
    {
        IMMEDIATE,
        EXTERNAL
    }
    public class BaseClass
    {
        #region UI Update
        public delegate void UpdateStatus(string Message);
        public event UpdateStatus updateStatus;
        public delegate void UpdateLog(string strMessage, bool bAppendCR = true);
        public event UpdateLog updateLog;
        protected string ModuleName = "ModuleBase";
        protected void Status(string Message)
        {
            if (updateStatus != null)
                updateStatus(ModuleName + ":" + Message);
        }
        protected void Log(string strMessage, bool bAppendCR = true)
        {
            if (updateLog != null)
                updateLog(" >" + ModuleName + ":" + strMessage, bAppendCR);
        }
        #endregion
        public BaseClass() { }
    }
    public class ControlModule
    {

        [CategoryAttribute("Application")]
        public bool Use1GhzFixedSamplingRate { get; set; }
        [CategoryAttribute("VXT")]
        public bool RunXapp { get; set; }
        [CategoryAttribute("VXT")]
        public string VXTVISAAddress { get; set; }
        [CategoryAttribute("VXT")]
        public double FrequencyInMhz { get; set; }
        [CategoryAttribute("VXT")]
        public double Amplitude { get; set; }
        [CategoryAttribute("VXT")]
        public Trigger SyncOutputTrg { get; set; }
        [CategoryAttribute("VXT")]
        public Trigger SyncOutputTrg2 { get; set; }
        [CategoryAttribute("VXT")]
        public Trigger SyncOutputTrg3 { get; set; }
        [CategoryAttribute("VXT")]
        public double OutputPower { get; set; }
        [CategoryAttribute("VXT")]
        public bool SourceExtlTrigEnabled { get; set; }
        [CategoryAttribute("VXT")]
        public Trigger SourceExtTrig { get; set; }

        [CategoryAttribute("VXT")]
        public double RFSamplingRateInMhz { get; set; }
        //[CategoryAttribute("VXT AWG")]
        public int SyncDelay;// { get; set; }
        [CategoryAttribute("VXT AWG")]
        public int AWGExternalPXITriggerNum { get; set; }
        [CategoryAttribute("VXT AWG")]
        public int OSRFactor { get; set; }
        [CategoryAttribute("AWG")]
        public int AWGTriggerDelay { get; set; }
        [CategoryAttribute("AWG")]
        public double DummyWaveformLength { get; set; }
        [CategoryAttribute("AWG")]
        public int slotNumber { get; set; }
        [CategoryAttribute("AWG")]
        public AWGTRIGGERMODE TriggerMode { get; set; }
        [CategoryAttribute("AWG")]
        public SD_QueueMode queueMode { get; set; }
        [CategoryAttribute("AWG")]
        public int repeatCycle { get; set; }
        [CategoryAttribute("AWG Sync Trigger Out")]
        public int SyncTriggerOut { get; set; }
        //[CategoryAttribute("AWG External Trigger Src")]
        //public int ExternalTriggerIn { get; set; }


        //[CategoryAttribute("VXT")]
        public ModulationPlaybackMode PlaybackMode;//{ get; set; }



        public ControlModule()
        {
            VXTVISAAddress = "PXI0::CHASSIS1::SLOT16::INDEX0::INSTR";
            //RunXapp = false;
            FrequencyInMhz = 2000;
            Amplitude = 0;
            RunXapp = false;
            //VXTExternalPXITriggerNum = 0;
            //AWGExternalPXITriggerNum = 0;
            AWGTriggerDelay = 0;
            RFSamplingRateInMhz = 137.6;
            OSRFactor = 5;
            SourceExtTrig = Trigger.PXITrigger0;
            SyncOutputTrg = Trigger.InternalTrigger;
            SyncOutputTrg2 = Trigger.FrontPanelTrigger1;
            SyncOutputTrg3 = Trigger.PXITrigger1;
            PlaybackMode = ModulationPlaybackMode.Single;
            SourceExtlTrigEnabled = true;
            slotNumber = 14;
            //TriggerMode = SD_TriggerModes.AUTOTRIG;
            queueMode = SD_QueueMode.CYCLIC;
            repeatCycle = 0;
            SyncTriggerOut = 2;
            //ExternalTriggerIn = 0;
            TriggerMode = AWGTRIGGERMODE.EXTERNATRIGGER;

            Use1GhzFixedSamplingRate = false;
            DummyWaveformLength = 0.00099978;
            SyncDelay = 10;
        }


    }

    public class ETSETUP
    {

        [CategoryAttribute("Setup")]
        public double RFSamplingRateInMhz { get; set; }
        [CategoryAttribute("Setup")]
        public int AWGTriggerDelay { get; set; }
        [CategoryAttribute("Setup")]
        public double BasebandDelay { get; set; }
        [CategoryAttribute("Setup")]
        public bool UseDummyWaveform { get; set; }
        [CategoryAttribute("Setup")]
        public int OSFactor { get; set; }
        public ETSETUP()
        {
            UseDummyWaveform = false;
            OSFactor = 5;
        }


    }

}
