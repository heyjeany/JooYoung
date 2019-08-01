using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Keysight.KtM941x;
using Ivi.Driver.Interop;
using Keysight.KtHvi;
using Keysight.KtM941xHvi;
using KeysightSD1;
using Agilent.AgM9300.Interop;
using System.Threading;

namespace ET4M941xA
{
    public class M320xA: BaseClass
    {
        SD_AOU moduleAOU = new SD_AOU();
        int status;
        int chassis_number = 1;
        int slot_number = 14;//14; // 8;
        int nAWG = 1;
        bool bIsM3201A = false;
        private int scalingFactor = -999;

        public M320xA()
        {
            this.ModuleName = "M320xA";
        }

        public void InitializeAWG( int nSlotNumner )
           
        {
            this.slot_number = nSlotNumner;
            if ((status = moduleAOU.open("", chassis_number, slot_number)) < 0)
            {
                Log("Error opening the AWG module! Please make sure the slot and chassis are correct. Aborting the Demo...");
                return;
            }
            else
            {
                Log("======= AWG INFO =======");
                Log("ID:\t\t" + status.ToString());
                Log("AWG Model:\t" + moduleAOU.getProductName());
                Log("Serial number:\t" + moduleAOU.getSerialNumber());
                Log("Chassis:\t\t" + moduleAOU.getChassis());
                Log("Slot:\t\t" + moduleAOU.getSlot());
                Log("HW version:\t" + moduleAOU.getHardwareVersion());
                //Log("========================");
                //.Log("");
                string model = moduleAOU.getProductName();
                if (model.Contains("3353"))
                    bIsM3201A = true;
                else if (model.Contains("3201"))
                    bIsM3201A = true;
                else
                    bIsM3201A = false;

                Log("HW support " + (bIsM3201A ? "200Mhz to 500Mhz" : "400Mhz to 1Ghz"));
            }
            Log("AWG Initialized");
        }
        public void AWGQueueWaveform(double SystemFrequency, ref double SynchronizeFrequency, double[] waveform, ControlModule ctrlParameter, double dummyWaveformLength = 1e-6, bool useDummyWaveform = false)
        {
            try
            {
                nAWG = 1; //  100; //averaging
                double hwVer = moduleAOU.getHardwareVersion();
                if (hwVer < 4)
                    nAWG = 0;
                else
                    nAWG = 1;

                //multiple channels setting
                //int nChannels = 4;
                //int awgMask = 0;
                //for (int ii = 0; ii < nChannels; ii++)
                //    awgMask |= 1 << ii;

                //AWG reset 
                moduleAOU.AWGstop(nAWG);
                moduleAOU.waveformFlush();
                moduleAOU.AWGflush(nAWG);

                Log(" > Set CLK frequency");
                //SystemFrequency = 1e6 * Convert.ToDouble(clkFreqAWG.Value);
                int setMode = 0; //0->LOW_JITTER, 1->FAST_TUNE
                this.SetAWGClockFrequency(SystemFrequency, out scalingFactor, true);
                //moduleAOU.clockSetFrequency(SystemFrequency, setMode);
                SynchronizeFrequency = moduleAOU.clockGetSyncFrequency();
                Log(" > AWG M320xA CLK Freqs");
                Log(" *** CLKsysFreq = " + SystemFrequency.ToString());
                Log(" *** CLKsyncFreq = " + SynchronizeFrequency.ToString());
                //Log("");

                //Wfm length
                //NOTE, 070519: set both RF and AWG wfm length to 1ms
                //int wfmLen = Convert.ToInt32((1e-3) * SystemFrequency);
                //Log("Queue Waveform.");
                this.AWGQueueConfig(ctrlParameter);
                //waveform
                int wfmType = SD_WaveformTypes.WAVE_ANALOG;
                int wfmNum = 0, onTime = 100; //wfmLen = 1000,
                //double[] wfmData = new double[wfmLen];
                //for (int ii = 0; ii < onTime; ii++)
                    //wfmData[ii] = 1;
                SD_Wave wave = new SD_Wave(wfmType, waveform);

                Log(" *** AWG Sampling Num : " + waveform.Length.ToString());
                
                //SD_Wave wave = new SD_Wave(@"P:\4Francesco\ET_DPD\TestA_20000.csv"); //use this to create wfm from file
                var t = moduleAOU.waveformLoad(wave, wfmNum);//, wfmNum);

                if (!useDummyWaveform)
                    t = moduleAOU.AWGqueueWaveform(nAWG, wfmNum, (int)ctrlParameter.TriggerMode, ctrlParameter.AWGTriggerDelay, ctrlParameter.repeatCycle, scalingFactor);
                else
                {
                    int nDummyLength = (int)(dummyWaveformLength * SystemFrequency);
                    int mod = nDummyLength % 16;
                    nDummyLength -= mod;
                    Log(" *** Dummy AWG Sampling Num : " + nDummyLength.ToString());
                    double[] dummyWave = new double[nDummyLength];
                    for (int i = 0; i < nDummyLength / 2; i++)
                    {
                        dummyWave[i] =0.5;
                    }
                    for (int i = nDummyLength / 2; i < nDummyLength; i++)
                    {
                        dummyWave[i] = 0.5;
                    }
                    double[] normalDummy = Normalizer.Normalize(dummyWave);
                    SD_Wave dWave = new SD_Wave(wfmType, normalDummy);
                    t = moduleAOU.waveformLoad(dWave, wfmNum + 1);
                    t = moduleAOU.AWGqueueWaveform(nAWG, wfmNum+1, (int)ctrlParameter.TriggerMode, ctrlParameter.AWGTriggerDelay, 1, scalingFactor);
                    t = moduleAOU.AWGqueueWaveform(nAWG, wfmNum, (int)AWGTRIGGERMODE.IMMEDIATE,0, ctrlParameter.repeatCycle, scalingFactor);
                }
                
            }
            catch( Exception ex )
            {
                Log(ex.Message);
                throw ex;
            }

        }

        public double SetAWGClockFrequency(double AwgClockFrequency, out int ScalingFactor, bool bIsM3202A = false)
        {
            double nReturnValue = 0;
            ScalingFactor = 0;
            double dAWGFrequencyToSet = AwgClockFrequency;
            double limitedFrequency = bIsM3201A ? 100e6 : 400e6;

            try
            {
                if (dAWGFrequencyToSet < limitedFrequency)
                {
                    do
                    {
                        ScalingFactor += 1;
                        dAWGFrequencyToSet = (5 * ScalingFactor) * AwgClockFrequency;
                    } while (dAWGFrequencyToSet < 400e6);
                }
                if (dAWGFrequencyToSet > (bIsM3201A ? 500e6 : 1e9))
                    throw new Exception("Out of range of Supported Frequency " + dAWGFrequencyToSet.ToString());
                nReturnValue = moduleAOU.clockSetFrequency(dAWGFrequencyToSet, 0);
                return nReturnValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AWGQueueConfig(ControlModule ctrlParameter)
        {
            try
            {
                //Set AWG mode
                double amplitude = 1;
                nAWG = 1; //  100; //averaging
                double hwVer = moduleAOU.getHardwareVersion();
                if (hwVer < 4)
                    nAWG = 0;
                else
                    nAWG = 1;
                moduleAOU.channelWaveShape(0 + nAWG, SD_Waveshapes.AOU_AWG);
                moduleAOU.channelAmplitude(0 + nAWG, 1);
                moduleAOU.channelOffset(0 + nAWG, 0.5);
                moduleAOU.channelWaveShape(1 + nAWG, SD_Waveshapes.AOU_PARTNER);
                moduleAOU.channelAmplitude(1 + nAWG, -1);
                moduleAOU.channelOffset(1 + nAWG, -0.5);
                //Queue settings
                int syncMode = SD_SyncModes.SYNC_CLK10;
                //int queueMode = Convert.ToInt32(SD_QueueMode.CYCLIC);
                //int startDelay = ctrlParameter.AWGTriggerDelay;// Convert.ToInt32(txtAWGDelay.Text); //1 GHz: 19456 + 587;//950 MHz: 19456 + 587 - 1041; //Unit is [10 ns]. Compensate for ~195us delay between AWGout and VXT2out
                //int prescaler = 0, nCycles = 0;// 1; // 0;
                moduleAOU.AWGqueueConfig(nAWG, (int)ctrlParameter.queueMode);
                //moduleAOU.AWGqueueConfig(nAWG, SD_QueueMode.ONE_SHOT); -> this demonstrates the "enum bug"
                moduleAOU.AWGqueueSyncMode(nAWG, syncMode);

                // Addd below setting. 
                //Trigger settings
                //int syncMode = SD_SyncModes.SYNC_CLK10;
                int queueMode = Convert.ToInt32(ctrlParameter.queueMode);
                int extSource = SD_TriggerExternalSources.TRIGGER_PXI0 + ctrlParameter.AWGExternalPXITriggerNum;
                int triggerBehavior = SD_TriggerBehaviors.TRIGGER_RISE;
                moduleAOU.AWGtriggerExternalConfig(nAWG, extSource, triggerBehavior, syncMode);
                var t = moduleAOU.AWGtriggerExternalConfig(nAWG, extSource, triggerBehavior, syncMode);
                moduleAOU.triggerIOconfig(SD_TriggerDirections.AOU_TRG_OUT);
                int PXIMask = 1 << ctrlParameter.SyncTriggerOut;
                t = moduleAOU.AWGqueueMarkerConfig(nAWG, 3, PXIMask, 1, 0, 1, 18, 0);
                //Log("1.3 Configure Playing AWG.. " + t.ToString());
                //t = moduleAOU.AWGqueueWaveform(nAWG, 0, (int)ctrlParameter.TriggerMode, ctrlParameter.AWGTriggerDelay, ctrlParameter.repeatCycle, 0);
                Log("Config AWG trigger mode and sync mode. ");

            }
            catch (Exception ex )
            {
                Log(ex.Message);
                throw ex;
            }
        }
        public void SynchronizeHVIModule()
        {
            try
            {
                //CLKresetPhase
                //Put AWG into a reset phase mode with external trigger control
                int trigBehavior = SD_TriggerBehaviors.TRIGGER_RISE, trigSource = SD_TriggerExternalSources.TRIGGER_PXI0;
                double skew = 0;
                moduleAOU.clockResetPhase(trigBehavior, trigSource, skew);
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                throw ex;
            }
        }
        public void PlayWaveform()
        {
            try
            {
#if false
                moduleAOU.triggerIOconfig(SD_TriggerDirections.AOU_TRG_OUT);
                int PXIMask = 1 << ctrlParameter.SyncTriggerOut;
                var t = moduleAOU.AWGqueueMarkerConfig(nAWG, 3, PXIMask, 1, 0, 1, 18, 0);
                Log("1.4 AWG Sync Output set");
#endif
                moduleAOU.AWGstop(nAWG);
                var t = moduleAOU.AWGstart(nAWG);
                Log("AWG Playing. " + t.ToString());
            }
            catch ( Exception ex )
            {
                Log(ex.Message);
                throw ex;
            }
        }
        public void Stop(bool reArm = false )
        {
            try
            {
                moduleAOU.AWGstop(nAWG);
                if (reArm)
                {
                    moduleAOU.AWGstart(nAWG);
                    Log("AWG Play stopped and replay again.");
                }
                else
                {
                    Log("AWG Play stopped");
                }
            }
            catch( Exception ex )
            {
                Log(ex.Message);
                throw ex;
            }
        }
        public void FreqTest( double sysFreq, ref double syncFreq)
        {
            try
            {
                int setMode = 0; //0->LOW_JITTER, 1->FAST_TUNE
                moduleAOU.clockSetFrequency(sysFreq, setMode);
                syncFreq = moduleAOU.clockGetSyncFrequency();
                //Log("AWG M3202A CLK Freqs");
                //Log("CLKsysFreq = " + SystemFrequency.ToString());
                //Log("CLKsyncFreq = " + SynchronizeFrequency.ToString());
            }
            catch( Exception ex )
            {
                Log(ex.Message);
                throw ex;
            }
        }
    }
}
