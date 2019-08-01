using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ivi.Driver;
using Keysight.KtM941x;
using Ivi.Driver.Interop;
using Keysight.KtHvi;
using Keysight.KtM941xHvi;
using KeysightSD1;
using Agilent.AgM9300.Interop;
using System.Threading;

namespace ET4M941xA
{
    public class VXT2HVI : BaseClass
    {
        IKtM941x VXT2 = null;
        KtHvi hvi = null;
        IKtHviTrigger trigger = null;
        IKtHviSequence sequence1 = null;
        IAgM9300Ex2 Ref = null; //M9300A Freq. reference
        string options = "QueryInstrStatus = true, Simulate = false, DriverSetup=UseFileSystemBackingStore = 0";// "QueryInstrStatus=true, Simulate=false, DriverSetup= DisableLoadCorrection=true,ModuleDiagnostics=true,ShareAllVisaSessions=true";
        string refRFWaveformName = "Ref";
        public VXT2HVI()
        {
            this.ModuleName = "VXT2HVI";
        }

        public void InitializeM9300A(string strM9300AVisaAddress= "PXI0::CHASSIS1::SLOT10::INDEX0::INSTR")
        {
            if (Ref == null)
            {
                
                Ref = new AgM9300();
                Ref.Initialize(strM9300AVisaAddress, true, true);
                //Ref.Initialize("PXI0::CHASSIS1::SLOT10::FUNC0::INSTR", true, true);
                Ref.ReferenceBase3.BackPlaneReferenceEnabled = true;
                Ref.ReferenceBase3.ProgrammableOutputTrigger2.Enabled = true;
                Ref.ReferenceBase3.ProgrammableOutputTrigger2.Destination = AgM9300TriggerEnum.AgM9300TriggerPXITrigger0;
                Ref.Apply();
                Log("M9300A Init Done.");
            }
        }
        public void GeneratePXI0()
        {
            try
            {
                Ref.ReferenceBase3.ProgrammableOutputTrigger2.GenerateTrigger();
            }
            catch( Exception ex )
            {
                Log(ex.Message);
            }
        }
        public void InitializeVXT2(ControlModule ctrlParameter )
        {
            try
            {
                options = "QueryInstrStatus = true, Simulate = false, DriverSetup=UseFileSystemBackingStore=0,AppStart=" + (ctrlParameter.RunXapp.ToString().ToLower());// "QueryInstrStatus=true, Simulate=false, DriverSetup= DisableLoadCorrection=true,ModuleDiagnostics=true,ShareAllVisaSessions=true";
                //options = "QueryInstrStatus = true, Simulate = false, DriverSetup=AppStart=" + (ctrlParameter.RunXapp.ToString().ToLower() );// "QueryInstrStatus=true, Simulate=false, DriverSetup= DisableLoadCorrection=true,ModuleDiagnostics=true,ShareAllVisaSessions=true";
                Log(options);
                VXT2 = new KtM941x(ctrlParameter.VXTVISAAddress, true, true, options);
                VXT2.Source.Triggers.ExternalTrigger.Delay = 0;
                VXT2.Source.RF.Level = 0;
                VXT2.Source.RF.Frequency = ctrlParameter.FrequencyInMhz * 1e6;
                VXT2.Source.Modulation.Enabled = true;
                //VXT.Source.Modulation.IQ.ArbEnabled = true;
                VXT2.Source.RF.OutputEnabled = true;
                VXT2.Apply();
                
                IKtM941x VXT = VXT2;
                VXT.Source.Triggers.SynchronizationOutputTrigger.Type = Keysight.KtM941x.SynchronizationTriggerType.PerArb;
                VXT.Source.Triggers.SynchronizationOutputTrigger.DataMarker = Keysight.KtM941x.Marker.Marker1;
                VXT.Source.Triggers.SynchronizationOutputTrigger.Enabled = true;
                VXT.Source.Triggers.SynchronizationOutputTrigger.PulseWidth = 10e-6;
                VXT.Source.Triggers.SynchronizationOutputTrigger.Mode = Keysight.KtM941x.TriggerMode.Level;
                VXT.Source.Triggers.SynchronizationOutputTrigger.Polarity = Keysight.KtM941x.TriggerPolarity.Positive;
                VXT.Source.Triggers.SynchronizationOutputTrigger.Destination = ctrlParameter.SyncOutputTrg;// Keysight.KtM941x.Trigger.InternalTrigger;

                VXT.Source.Triggers.SynchronizationOutputTrigger2.Type = Keysight.KtM941x.SynchronizationTriggerType.PerArb;
                VXT.Source.Triggers.SynchronizationOutputTrigger2.DataMarker = Keysight.KtM941x.Marker.Marker1;
                VXT.Source.Triggers.SynchronizationOutputTrigger2.Enabled = true;
                VXT.Source.Triggers.SynchronizationOutputTrigger2.PulseWidth = 10e-6;
                VXT.Source.Triggers.SynchronizationOutputTrigger2.Mode = Keysight.KtM941x.TriggerMode.Level;
                VXT.Source.Triggers.SynchronizationOutputTrigger2.Polarity = Keysight.KtM941x.TriggerPolarity.Positive;
                VXT.Source.Triggers.SynchronizationOutputTrigger2.Destination = ctrlParameter.SyncOutputTrg2;// Keysight.KtM941x.Trigger.FrontPanelTrigger1;

                VXT.Source.Triggers.SynchronizationOutputTrigger3.Type = Keysight.KtM941x.SynchronizationTriggerType.PerArb;
                VXT.Source.Triggers.SynchronizationOutputTrigger3.DataMarker = Keysight.KtM941x.Marker.Marker1;
                VXT.Source.Triggers.SynchronizationOutputTrigger3.Enabled = true;
                VXT.Source.Triggers.SynchronizationOutputTrigger3.PulseWidth = 10e-6;
                VXT.Source.Triggers.SynchronizationOutputTrigger3.Mode = Keysight.KtM941x.TriggerMode.Level;
                VXT.Source.Triggers.SynchronizationOutputTrigger3.Polarity = Keysight.KtM941x.TriggerPolarity.Positive;
                VXT.Source.Triggers.SynchronizationOutputTrigger3.Destination = ctrlParameter.SyncOutputTrg3;// Keysight.KtM941x.Trigger.PXITrigger1;


                VXT.Source.Triggers.ExternalTrigger.Enabled = ctrlParameter.SourceExtlTrigEnabled;// true;// chkVXTExternalTrigger.Checked ? StartEvent.ExternalTrigger : StartEvent.Immediate;
                VXT.Source.Triggers.ExternalTrigger.Source = ctrlParameter.SourceExtTrig;

                VXT.Source.Modulation.StartPlaybackOn10MHzClock = true;

                // Added for ET
                VXT2.Service.SetValue("Source;UseSourceDspTrigger", "true");

                VXT.Apply();
                //Set PXI0 as input trigger for external triggering of RF output
                //NOTE: this code is moved here to try to solve the conflict w HVI trigger class
                //VXT2.Source.Triggers.ExternalTrigger.Source = Trigger.PXITrigger0;
                //VXT2.Source.Triggers.ExternalTrigger.Enabled = true; //chkVXTExternalTrigger.Checked;
                Log("VXT2 Initialize Done!");
            }
            catch( Exception ex )
            {
                Log(ex.Message);
                throw ex;
            }
        }


        public void SetSyncOutputTriggering(ControlModule ctrlParameter)
        {
            IKtM941x VXT = VXT2;
            VXT.Source.Triggers.SynchronizationOutputTrigger.Type = Keysight.KtM941x.SynchronizationTriggerType.PerArb;
            VXT.Source.Triggers.SynchronizationOutputTrigger.DataMarker = Keysight.KtM941x.Marker.Marker1;
            VXT.Source.Triggers.SynchronizationOutputTrigger.Enabled = true;
            VXT.Source.Triggers.SynchronizationOutputTrigger.PulseWidth = 10e-6;
            VXT.Source.Triggers.SynchronizationOutputTrigger.Mode = Keysight.KtM941x.TriggerMode.Level;
            VXT.Source.Triggers.SynchronizationOutputTrigger.Polarity = Keysight.KtM941x.TriggerPolarity.Positive;
            VXT.Source.Triggers.SynchronizationOutputTrigger.Destination = ctrlParameter.SyncOutputTrg;// Keysight.KtM941x.Trigger.InternalTrigger;

            VXT.Source.Triggers.SynchronizationOutputTrigger2.Type = Keysight.KtM941x.SynchronizationTriggerType.PerArb;
            VXT.Source.Triggers.SynchronizationOutputTrigger2.DataMarker = Keysight.KtM941x.Marker.Marker1;
            VXT.Source.Triggers.SynchronizationOutputTrigger2.Enabled = true;
            VXT.Source.Triggers.SynchronizationOutputTrigger2.PulseWidth = 10e-6;
            VXT.Source.Triggers.SynchronizationOutputTrigger2.Mode = Keysight.KtM941x.TriggerMode.Level;
            VXT.Source.Triggers.SynchronizationOutputTrigger2.Polarity = Keysight.KtM941x.TriggerPolarity.Positive;
            VXT.Source.Triggers.SynchronizationOutputTrigger2.Destination = ctrlParameter.SyncOutputTrg2;// Keysight.KtM941x.Trigger.FrontPanelTrigger1;

            VXT.Source.Triggers.SynchronizationOutputTrigger3.Type = Keysight.KtM941x.SynchronizationTriggerType.PerArb;
            VXT.Source.Triggers.SynchronizationOutputTrigger3.DataMarker = Keysight.KtM941x.Marker.Marker1;
            VXT.Source.Triggers.SynchronizationOutputTrigger3.Enabled = true;
            VXT.Source.Triggers.SynchronizationOutputTrigger3.PulseWidth = 10e-6;
            VXT.Source.Triggers.SynchronizationOutputTrigger3.Mode = Keysight.KtM941x.TriggerMode.Level;
            VXT.Source.Triggers.SynchronizationOutputTrigger3.Polarity = Keysight.KtM941x.TriggerPolarity.Positive;
            VXT.Source.Triggers.SynchronizationOutputTrigger3.Destination = ctrlParameter.SyncOutputTrg3;// Keysight.KtM941x.Trigger.PXITrigger1;


            VXT.Source.Triggers.ExternalTrigger.Enabled = ctrlParameter.SourceExtlTrigEnabled;// true;// chkVXTExternalTrigger.Checked ? StartEvent.ExternalTrigger : StartEvent.Immediate;
            VXT.Source.Triggers.ExternalTrigger.Source = ctrlParameter.SourceExtTrig;

            VXT.Source.Modulation.StartPlaybackOn10MHzClock = true;
            VXT.Apply();
        }
        public void InitializeHVI()
        {
            try
            {
                string moduleResourceName = "KtHvi";
                string  moduleInitOptions = "Simulate=False";
                hvi = new KtHvi(moduleResourceName, false, false, moduleInitOptions);

                //Open module is simulated mode
                int chassisNumber = 1;
                hvi.Platform.Chassis.AddWithOptions(chassisNumber, "Simulate=True,DriverSetup=model=M9018B,NoDriver=True");

                // Retrieving engine addresses from KtCornerstone interface
                //var engineNames0 = VXT2.Hvi.HviEngines.Count;
                Keysight.KtM941xHvi.KtM941xHvi VXT2hvi = (Keysight.KtM941xHvi.KtM941xHvi)VXT2.Hvi;
                var engineNames0 = VXT2hvi.Engines.Receiver;

                //var engine1Address = VXT2.Hvi.HviEngines["ReceiverEngine"].LocalAddress;
                ulong engine1ID = (ulong)VXT2hvi.Engines.Receiver;

                string sequenceName1 = "Sequence1";
                var engine1 = hvi.Engines.Add((long)engine1ID, sequenceName1);
                //var engine1 = hvi.Engines.Add("Engine1", sequenceName1);
                sequence1 = engine1.MainSequence;
                // Assign triggers to HVI
                TriggerResourceId[] syncResources = { TriggerResourceId.PxiTrigger6, TriggerResourceId.PxiTrigger7 };
                hvi.Platform.SetSyncResources(syncResources);

                // Assign triggers to HVi
                //var theTriggers = VXT2.Hvi.Triggers;
                int triggerID = (int)VXT2hvi.Triggers.Pxi0;
                //JooYoung add comment
                //engine1.Triggers.Add(triggerID, "sequenceTrigger");
                engine1.Triggers.Add(triggerID, "sourceTrigger");
                // JooYoung add line below. 
                //engine1.Triggers.Add("PxiTrig0", "sequenceTrigger");
                //

               // trigger = engine1.Triggers["sequenceTrigger"];
                trigger = engine1.Triggers["sourceTrigger"];
                trigger.Configuration.Direction = Direction.Output;
                trigger.Configuration.DriveMode = DriveMode.PushPull;
                trigger.Configuration.Polarity = Keysight.KtHvi.TriggerPolarity.ActiveHigh;
                trigger.Configuration.Delay = 0;
                trigger.Configuration.SyncMode = SyncMode.Sync_User0;
                trigger.Configuration.TriggerMode = Keysight.KtHvi.TriggerMode.Pulse;
                trigger.Configuration.PulseLength = 500;
                trigger.ApplyConfiguration();
            }
            catch( Exception ex )
            {
                Log(ex.Message);
                throw ex;
            }
        }

        public void VXT2Close()
        {
            if( VXT2 != null )
            {
                Log("Close VXT2..");
                Ivi.Driver.IIviDriver driver = (Ivi.Driver.IIviDriver)VXT2;
                driver.Close();
            }
        }
        public void VXT2QueueWaveform( double[] IQData, ControlModule ctrlParameter)
        {
            try
            {
                ErrorQueryResult result;
                do
                {
                    //NOTE: it is necessary to create driver variable to read the error queue
                    Ivi.Driver.IIviDriver driver = (Ivi.Driver.IIviDriver)VXT2;
                    result = driver.Utility.ErrorQuery();
                    Log("ErrorQuery:" + result.Code.ToString() + " " + result.Message);
                } while (result.Code != 0);

                byte[] Marker;
                VXT2.Source.Modulation.Stop();
                //Log("Load RF waveform.");
                Thread.Sleep(100);
                RemoveARB(refRFWaveformName);
                Marker = new byte[IQData.Length / 2];
                for (int i = 0; i < 15; i++)
                    Marker[i] = 1;
                Keysight.KtM941x.Marker rfBlankingMarker = Keysight.KtM941x.Marker.None;
                //VXT2.Source.Modulation.IQ.UploadArbDoubles(refRFWaveformName, dIQData, SamplingRateToSet, rmsPower, scaleFactor); //SamplingRate in Sa/s
                VXT2.Source.Modulation.IQ.UploadArbDoublesWithMarkers(refRFWaveformName, IQData, Marker, ctrlParameter.RFSamplingRateInMhz * 1e6, 2.5, 0.9, rfBlankingMarker);
                Log("Load Waveform Done!");
                
            }
            catch( Exception ex )
            {
                Log(ex.Message);
                throw ex;
            }
        }

        public void SetBasebandDelay( double BBDelay )
        {
            try
            {
                Log("Get BB Delay    " + (VXT2.Source.Modulation.Baseband.Delay * 1e9).ToString(".000") + " nSec");
                VXT2.Source.Modulation.Baseband.Delay = BBDelay;
                VXT2.Source.Apply();
                Log("Set BB Delay to " + (BBDelay * 1e9).ToString(".000") + " nSec");
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }

        public void RemoveARB(string referenceName)
        {
            try
            {
                string arbCatalog = VXT2.Source.Modulation.CatalogContents();
                if (arbCatalog.IndexOf(referenceName) != -1)
                {
                    VXT2.Source.Modulation.IQ.RemoveArb(referenceName);
                    //UpdateLog("Remove ARB : " + referenceName);
                }
                else
                {
                    //UpdateLog("ARB " + referenceName + " is not in ARB Memory.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void SynchronizeHVIModule( double SynchronizeFrequency  )
        {
            try
            {
                double[] nonHviSystemClocks = { SynchronizeFrequency }; // DUT clock
                hvi.Synchronization.SetNonHviSystemClocks(nonHviSystemClocks);

                // Perform the synchronization.
                hvi.Synchronization.Synchronize();

            }
            catch ( Exception ex )
            {
                Log(ex.Message);
                throw ex;
            }
        }
        public void GenerateTrigger()
        {
            try
            {
                //VXT2 triggers PXI0
                //trigger.Off();
                trigger.On();
                //trigger.Off();

                Log("PXI0 Triggered by VXT2!");
                //Log("");
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }
        public void PlayWaveform(ControlModule ctrlParameter)
        {
            try
            {
                VXT2.Source.Modulation.Stop();
                VXT2.Source.Triggers.ExternalTrigger.Enabled = ctrlParameter.SourceExtlTrigEnabled;
                VXT2.Source.Triggers.ExternalTrigger.Source = ctrlParameter.SourceExtTrig;
                VXT2.Source.Modulation.IQ.ArbEnabled = true;
                VXT2.Source.Modulation.Enabled = true;
                VXT2.Source.RF.OutputEnabled = true;
                VXT2.Source.RF.Frequency = ctrlParameter.FrequencyInMhz * 1e6;
                VXT2.Source.RF.Level = ctrlParameter.OutputPower;
                VXT2.Source.Modulation.PlaybackMode = ctrlParameter.PlaybackMode;
                Log(ctrlParameter.PlaybackMode.ToString());
                //VXT2.Source.Modulation.
                VXT2.Apply();

                if (ctrlParameter.SourceExtlTrigEnabled)
                {
                    VXT2.Source.Modulation.PlayArb(refRFWaveformName, StartEvent.ExternalTrigger);
                    Log("Play - External trigger.");
                }
                else
                {
                    VXT2.Source.Modulation.PlayArb(refRFWaveformName, StartEvent.Immediate);
                    Log("Play - immediate!");
                }
            }
            catch( Exception ex )
            {
                Log(ex.Message);
            }
        }

        public void Stop(bool ExternalTrigger = true, bool reArm = true)
        {
            try
            {
                VXT2.Source.Modulation.Stop();
                if (reArm)
                {
                    VXT2.Apply(); //apply settings  
                    VXT2.Source.Modulation.PlayArb(refRFWaveformName, (ExternalTrigger ? StartEvent.ExternalTrigger : StartEvent.Immediate));
                    Log("VXT2 Play stopped and replay again.");
                }
                else
                {
                    Log("VXT2 Play stopped");
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                throw ex;
            }
        }
    }
}
