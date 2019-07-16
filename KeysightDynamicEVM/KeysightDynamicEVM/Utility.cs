using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using System.IO;

namespace KeysightDynamicEVM
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
}
