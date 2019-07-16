using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polenter.Serialization;
using System.Windows.Forms.Design;
namespace KeysightDynamicEVM
{
    public class PARAMETERCLASS
    {

        [CategoryAttribute("Waveform")]
        [EditorAttribute(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string WaveformFileName { get; set; }
        [CategoryAttribute("MXG")]
        public string SGVISAAddress { get; set; }
        [CategoryAttribute("MXG")]
        public double SGFreqInMhz { get; set; }
        [CategoryAttribute("MXG")]
        public double SGPowerLevel { get; set; }

        [CategoryAttribute("xSA")]
        public string xSAVISAAddress { get; set; }
        [CategoryAttribute("xSA")]
        public double Range { get; set; }
        [CategoryAttribute("N6700x")]
        public string N6700VISAAddress { get; set; }
        [CategoryAttribute("N6700x")]
        public double Voltage { get; set; }
        [CategoryAttribute("N6700x")]
        public double Current { get; set; }

        [CategoryAttribute("Waveform")]
        public double T1 { get; set; }
        [CategoryAttribute("Waveform")]
        public double T2 { get; set; }
        [CategoryAttribute("Waveform")]
        public double T1Adjustment { get; set; }
        [CategoryAttribute("Waveform")]
        public double T2Adjustment { get; set; }
        [CategoryAttribute("Waveform")]
        public double DutyCycle { get; set; }


        [CategoryAttribute("Testing")]
        public int GapBtwMeasure { get; set; }

        public PARAMETERCLASS()
        {
            WaveformFileName = @".\\waveform\\Duty_76_173_68us.wfm";

  
            SGVISAAddress = "TCPIP0::10.112.36.195::inst0::INSTR";
            SGVISAAddress = "TCPIP0::141.121.90.121::inst0::INSTR";
            SGVISAAddress = "TCPIP0::141.121.151.76::inst0::INSTR";

            xSAVISAAddress = "TCPIP0::10.112.39.94::hislip1::INSTR";
            xSAVISAAddress = "TCPIP0::141.121.92.252::hislip1::INSTR";
            xSAVISAAddress = "TCPIP0::141.121.151.180::hislip0::INSTR";

            SGFreqInMhz = 400;
            Range = 10;
            SGPowerLevel = 0;

            N6700VISAAddress = "TCPIP0::10.112.37.80::inst0::INSTR";
            Voltage = 3.0;
            Current = 3;

            T1 = 500e-9;
            T2 = 500e-9;
            T1Adjustment = 0;
            T2Adjustment = 500e-9;
            DutyCycle = 0.76;

            GapBtwMeasure = 100;


        }
    }
}
