using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;
using System;
using System.Diagnostics;

namespace ET4M941xA
{
    public class DateTimeStamp
    {
        DateTime nowTime;
        public DateTimeStamp()
        { }
        public string GetNowTime()
        {
            string strTimeStamp = "";
            nowTime = DateTime.Now;

            strTimeStamp = nowTime.Year.ToString() + "-" +
                           nowTime.Month.ToString("00") + "-" +
                           nowTime.Day.ToString("00") + "  " +
                           nowTime.Hour.ToString("00") + "-" +
                           nowTime.Minute.ToString("00") + "-" +
                           nowTime.Second.ToString("00") + "-" +
                           nowTime.Millisecond.ToString("000");
            return strTimeStamp;
        }
    }

    public class MessageQueue
    {
        DateTimeStamp timeStamp = new DateTimeStamp();
        Queue<string> messageQueue = new Queue<string>();
        // Log update
        FileStream fs;
        StreamWriter sw;
        public int Count
        {
            get
            {
                return messageQueue.Count;
            }
        }

        public MessageQueue()
        {
            //fs = new FileStream(".\\MainLog.txt", FileMode.OpenOrCreate);
            //sw = new StreamWriter(fs);
            //fs.Seek(0, SeekOrigin.End);
            messageQueue.Clear();
        }
        private string EnQueueResult(string strMessage)
        {
            string T = timeStamp.GetNowTime() + " : " + strMessage;
            messageQueue.Enqueue(T);
            return T;
        }
        public void Enqueue(string strMessage)
        {
            //fs = new FileStream(".\\MainLog.txt", FileMode.OpenOrCreate);
            //sw = new StreamWriter(fs);
            //fs.Seek(0, SeekOrigin.End);
            //sw.WriteLine(EnQueueResult(strMessage));
            EnQueueResult(strMessage);
            //sw.Close();fs.Close();
        }
        public string Dequeue()
        {
            return messageQueue.Dequeue();
        }

        public void Close()
        {
            //sw.Close(); fs.Close();
        }
    }

    public class TickTime
    {
        Stopwatch timer = new Stopwatch();
        double InterValTick = 0;
        public TickTime()
        {
            //timer.Start();
        }

        public void Start(bool bReset = true)
        {
            if (bReset)
            {
                timer.Restart();
                InterValTick = 0;
            }
            else
                timer.Start();
        }
        public double GetElapsedTime(bool bReset = false)
        {
            double retVal = 0;
            if (bReset)
            {
                retVal = (double)timer.ElapsedTicks / (Stopwatch.Frequency / 1000);
                timer.Restart();
            }
            else
            {
                double check = (double)timer.ElapsedTicks / (Stopwatch.Frequency / 1000);
                retVal = check - InterValTick;
                InterValTick = check;
            }
            return retVal;
        }
        public string GetElapsedTimeAsString(bool addCR = false, bool bReset = false)
        {
            double retVal = 0;
            if (bReset)
            {
                retVal = (double)timer.ElapsedTicks / (Stopwatch.Frequency / 1000);
                timer.Restart();
            }
            else
            {
                double check = (double)timer.ElapsedTicks / (Stopwatch.Frequency / 1000);
                retVal = check - InterValTick;
                InterValTick = check;
            }
            return addCR ? retVal.ToString("0.0000") + "ms\r" : retVal.ToString("0.0000") + "ms";
        }
        public double GetTotalTime()
        {
            timer.Stop();
            return (double)timer.ElapsedTicks / (Stopwatch.Frequency / 1000);
        }
        public string GetTotalTimeAsString(bool addCR = false)
        {
            timer.Stop();
            string strReturn = ((double)timer.ElapsedTicks / (Stopwatch.Frequency / 1000)).ToString("0.0000") + "ms";
            strReturn += addCR ? "\r" : "";
            return strReturn;
        }

    }
    public static class CalculatePower
    {
        public static double[] PowerCal(double[] iqData)
        {
            double[] calIQ = new double[iqData.Length / 2];
            calIQ.Initialize();
            for (int i = 0; i < calIQ.Length; i++)
            {
                calIQ[i] = 10 * Math.Log10((iqData[2 * i] * iqData[2 * i] + iqData[(2 * i) + 1] * iqData[(2 * i) + 1]) * 10);
            }
            return calIQ;
        }
    }
    public class UpSampler
    {

        public List<DataList> UpSample(List<DataList> originalData, double originalSamplingRate, double targetSamplingRate)
        {
            //List<DataList> originalData = new List<DataList>();
            List<DataList> targetlData = new List<DataList>();
            CalculateEquation cal = new CalculateEquation();
            try
            {
                double totalLengthInTime = originalData.Count * (1 / originalSamplingRate);//Total Length of data in time
                double targettedSamplingRate = targetSamplingRate;
                int nTargettedUpsampledDataNum = (int)(totalLengthInTime * targettedSamplingRate);
                targetlData.Clear();
                // First is same.
                targetlData.Add(originalData[0]);
                int nIndexTrack = 0;
                int TargetDataIncremlentIndex = 1;
                double TargetDataX;
                double TargetDataY = 0;
                do
                {
                    TargetDataX = TargetDataIncremlentIndex / targettedSamplingRate;
                    TargetDataIncremlentIndex++;
                    if (originalData[nIndexTrack + 1].x < TargetDataX)
                        nIndexTrack++;
                    if (TargetDataX >= totalLengthInTime)
                        break;
                    // If reach to end of stream. 
                    if (nIndexTrack > (originalData.Count - 3))
                        nIndexTrack = originalData.Count - 3;
                    TargetDataY = cal.GetValue(TargetDataX, originalData[nIndexTrack].x, originalData[nIndexTrack].y,
                                                         originalData[nIndexTrack + 1].x, originalData[nIndexTrack + 1].y,
                                                         originalData[nIndexTrack + 2].x, originalData[nIndexTrack + 2].y);
                    targetlData.Add(new DataList(TargetDataX, TargetDataY));
                } while (true);
                return targetlData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public static class GetLineNum
    {
        public static int TotalLines(string filePath)
        {
            using (StreamReader r = new StreamReader(filePath))
            {
                int i = 0;
                while (r.ReadLine() != null) { i++; }
                return i;
            }
        }
    }
    public static class GetTotalLine
    {
        public static int TotalLines(string filePath)
        {
            using (StreamReader r = new StreamReader(filePath))
            {
                int i = 0;
                while (r.ReadLine() != null) { i++; }
                return i;
            }
        }
    }
    public static class Normalizer
    {
        public static double[] Normalize(double[] source)
        {
            double[] retSource = new double[source.Length];
            try
            {
                double min = (source.Min() - (Math.Abs(source.Min()) * 0.01));
                double max = (source.Max() + (Math.Abs(source.Max()) * 0.01));
                //double[] NormalizedWaveformData = new double[source.Length];
                double a = 2 / (double)(max - min);
                double b = 1 - a * (double)max;
                for (int i = 0; i < source.Length; i++)
                {
                    //if (chkDebugOpt2.Checked)
                    //    NormalizedWaveformData[i] = 0.999;
                    //else
                    retSource[i] = a * (double)source[i] + b;
                    //dWfData[i] = (double)wfData[i] / 32767.0;
                }
                return retSource;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static double[] Normalize(double[] source, double scale)
        {
            double[] retSource = new double[source.Length];
            try
            {
                double min = (source.Min() - (Math.Abs(source.Min()) * 0.01));
                double max = (source.Max() + (Math.Abs(source.Max()) * 0.01));
                min *= scale;
                max *= scale;
                //double[] NormalizedWaveformData = new double[source.Length];
                double a = 2 / (double)(max - min);
                double b = 1 - a * (double)max;
                for (int i = 0; i < source.Length; i++)
                {
                    //if (chkDebugOpt2.Checked)
                    //    NormalizedWaveformData[i] = 0.999;
                    //else
                    retSource[i] = a * (double)source[i] + b;
                    //dWfData[i] = (double)wfData[i] / 32767.0;
                }
                return retSource;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
    public class CalculateEquation
    {

        public CalculateEquation()
        {

        }
        public double GetValue(double xTarget, double x1, double y1, double x2, double y2, double x3, double y3)
        {
            double finded = 0;
            double a = 0, b = 0, c = 0;
            b = (x1 + x2) / ((x2 - x3) * (x1 + x2) - 1) * (y2 - y3 - (y1 - y2) * (x2 * x2 - x3 * x3) / (x1 * x1 - x2 * x2));
            a = 1 / (x1 + x2) * ((y1 - y2) / (x1 - x2) - b);
            c = y1 - b * x1 - a * x1 * x1;
            finded = a * xTarget * xTarget + b * xTarget + c;
            return finded;
        }
        public double GetGap(double xTarget, double x1, double y1, double x2, double y2)
        {
            double gapFound = 0;

            double a = (y1 - y2) / (x1 - x2);
            double b = y1 - a * x1;
            gapFound = a * xTarget + b;

            return gapFound;
        }

    }
    public class DataList
    {
        public double x;
        public double y;
        public DataList(double inx, double iny)
        {
            x = inx;
            y = iny;
        }
    }

    public class INIAccess
    {
        public string path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        public int readNum;
        /// <summary>
        /// INIFile Constructor.
        /// </summary>
        /// <PARAM name="INIPath"></PARAM>
        public INIAccess(string INIPath)
        {
            path = INIPath;
        }
        public INIAccess()
        {

        }
        public void SetPath(string path)
        {
            this.path = path;
        }
        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// Section name
        /// <PARAM name="Key"></PARAM>
        /// Key Name
        /// <PARAM name="Value"></PARAM>
        /// Value Name
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }

        public void IniWriteIntArrayValue(string Section, string Key, int[] intDataArray)
        {
            string Value = "";
            for (int i = 0; i < intDataArray.Length; i++)
            {
                Value += intDataArray[i].ToString();
                if (i != (intDataArray.Length - 1))
                    Value += ",";
            }
            WritePrivateProfileString(Section, Key, Value, this.path);
        }

        public void IniWriteComplexArrayValue(string Section, string Key, Complex[] complexDataArray)
        {
            string Value = "";
            for (int i = 0; i < complexDataArray.Length; i++)
            {
                string strT = "";
                if (complexDataArray[i].Imaginary < 0)
                {
                    if (complexDataArray[i].Imaginary == -1)
                    {
                        strT = complexDataArray[i].Real.ToString() + "-j";
                    }
                    else
                    {
                        strT = complexDataArray[i].Real.ToString() + complexDataArray[i].Imaginary.ToString() + "j";
                    }
                }
                else
                {
                    if (complexDataArray[i].Imaginary == 1)
                    {
                        strT = complexDataArray[i].Real.ToString() + "+j";
                    }
                    else if (complexDataArray[i].Imaginary == 0)
                    {
                        strT = complexDataArray[i].Real.ToString();
                    }
                    else
                    {
                        strT = complexDataArray[i].Real.ToString() + "+" + complexDataArray[i].Imaginary.ToString() + "j";
                    }
                }
                Value += strT;
                if (i != (complexDataArray.Length - 1))
                    Value += ",";
            }

            WritePrivateProfileString(Section, Key, Value, this.path);
        }
        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// <PARAM name="Key"></PARAM>
        /// <PARAM name="Path"></PARAM>
        /// <returns></returns>
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(32767);
            readNum = GetPrivateProfileString(Section, Key, "", temp,
                                            32767, this.path);
            if (readNum < 1)
            {
                return "";
                //                throw new Exception("String reading at Section[" + Section + "] / Key[" + Key + "] reading failure!");
            }
            return temp.ToString();
        }
        public int IniReadIntValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(32767);
            readNum = GetPrivateProfileString(Section, Key, "", temp,
                                            32767, this.path);
            if (readNum < 1)
            {
                return 0;
                //                throw new Exception("Integer reading at Section[" + Section + "] / Key[" + Key + "] reading failure!");
            }
            return Convert.ToInt32(temp.ToString());
        }
        public double IniReadDoubleValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(32767);
            readNum = GetPrivateProfileString(Section, Key, "", temp,
                                            32767, this.path);
            if (readNum < 1)
            {
                return -9999;
                //                throw new Exception("Double reading at Section[" + Section + "] / Key[" + Key + "] reading failure!");
            }
            return Convert.ToDouble(temp.ToString());
        }
        public int[] IniReadIntArrayValue(string Section, string Key, char splitDelimeter)
        {
            StringBuilder temp = new StringBuilder(32767);
            int[] retIntArray = new int[1];
            readNum = GetPrivateProfileString(Section, Key, "", temp, 32767, this.path);
            try
            {
                if (readNum < 1)
                {
                    throw new Exception("Integer Array reading at Section[" + Section + "] / Key[" + Key + "] reading failure!");
                }
                string[] strSplit = temp.ToString().Split(splitDelimeter);
                if (strSplit.Length < 2)
                {
                    throw new Exception("Integer Array reading at Section[" + Section + "] / Key[" + Key + "] reading failure! Length is less than 2");
                }
                retIntArray = new int[strSplit.Length];
                for (int i = 0; i < retIntArray.Length; i++)
                {
                    retIntArray[i] = Convert.ToInt32(strSplit[i]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retIntArray;
        }
        public Complex[] IniReadComplexArrayValue(string Section, string Key, char splitDelimeter)
        {
            StringBuilder temp = new StringBuilder(32767);
            Complex[] retComplexArray = new Complex[1];

            readNum = GetPrivateProfileString(Section, Key, "", temp, 32767, this.path);
            try
            {
                if (readNum < 1)
                {
                    throw new Exception("Complex Array reading at Section[" + Section + "] / Key[" + Key + "] reading failure!");
                }
                string[] strSplit = temp.ToString().Split(splitDelimeter);
                if (strSplit.Length < 2)
                {
                    throw new Exception("Complex Array reading at Section[" + Section + "] / Key[" + Key + "] reading failure! Length is less than 2");
                }

                /*
                 * 1.¸ÕÀú Ã¹ ºÎÈ£¸¦ °Ë»öÇÑ´Ù. 
                 * 2. '-' ÀÌ¸é realÀ» -·Î ¼³Á¤ÇÑ´Ù. 
                 * 3. '-'¸¦ Á¦°ÅÇÑ´Ù. 
                 * 4. ´Ù½Ã '-'¸¦ °Ë»öÇÑ´Ù. 
                 * 5. '-'°¡ ¶Ç ÀÖÀ¸¸é imaginary¸¦ -·Î ÇÑ´Ù. 
                 * 6. '+'¸¦ °Ë»öÇÑ´Ù. 
                 * 7. ÀÖÀ¸¸é Imaginary¸¦ +·Î ÇÑ´Ù. 
                 * 8. µÑ ´Ù ¾øÀ¸¸é j¸¦ °Ë»öÇÑ´Ù. 
                 * 9. ¾øÀ¸¸é Imaginary¸¦ 0À¸·Î ¼³Á¤ÇÑ´Ù. 
                 * 10. ¿ä°É ³¡±îÁö ¹Ýº¹ÇÑ´Ù. 
                 */
                retComplexArray = new Complex[strSplit.Length];
                int nCount = 0;
                foreach (string value in strSplit)
                {
                    string strValue = value;
                    string[] splitStr = null;
                    bool bFirstIsMinus = false;
                    bool bSecondIsMinus = false;
                    double real = 0, imag = 0;
                    if (strValue[0] == '-')
                    {
                        // Ã¹¹øÂ° °ªÀÌ À½¼ö.. ½Ç¼öÀÎÁö Çã¼öÀÎÁö´Â ³ªÁß¿¡...
                        bFirstIsMinus = true;
                        strValue = strValue.Remove(0, 1);
                    }
                    splitStr = strValue.Split('+');
                    if (splitStr.Length < 2)
                    {
                        splitStr = strValue.Split('-');
                        bSecondIsMinus = true;// Çã¼öºÎ°¡ À½¼ö.
                    }
                    if (splitStr.Length < 2) // ¿©ÀüÈ÷ ÇÑ°³ ÀÌ¸é..
                    {
                        // ½Ç¼öÀÎÁö Çã¼öÀÎÁö Ã£´Â´Ù. 
                        if (strValue.IndexOf('j') == -1) //Çã¼ö Ç¥½Ã°¡ ¾øÀ¸¸é..
                        {
                            // ½Ç¼ö
                            if (bFirstIsMinus)
                                real = Convert.ToDouble(strValue) * -1;
                            else
                                real = Convert.ToDouble(strValue);
                            imag = 0;
                            retComplexArray[nCount++] = new Complex(real, imag);
                        }
                        else //¾Æ´Ï¸é
                        {
                            // Çã¼ö
                            strValue = strValue.Remove(strValue.Length - 1, 1);
                            if (strValue.Length == 0)
                                strValue = "1";
                            if (bFirstIsMinus)
                                imag = Convert.ToDouble(strValue) * -1;
                            else
                                imag = Convert.ToDouble(strValue);
                            real = 0;
                            retComplexArray[nCount++] = new Complex(real, imag);
                        }
                    }
                    else
                    {
                        real = Convert.ToDouble(splitStr[0]);
                        if (bFirstIsMinus)
                            real *= -1;
                        if (splitStr[1].Remove(splitStr[1].Length - 1, 1).Length == 0)
                            splitStr[1] = "1j";
                        imag = Convert.ToDouble(splitStr[1].Remove(splitStr.Length - 1, 1));

                        if (bSecondIsMinus)
                            imag *= -1;
                        retComplexArray[nCount++] = new Complex(real, imag);
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retComplexArray;
        }


    }
}
