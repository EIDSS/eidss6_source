using bv.common.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace bv.common.Core
{
    public static class LogError
    {
        public static void Log(string filenamePrefix, Exception ex, Action<StreamWriter> callback = null, string formatFileName = null)
        {
            string path = Config.GetSetting("ErrorLogPath");
            var exTime = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff");
            if (!String.IsNullOrEmpty(path))
            {
                if (!Directory.Exists(path))
                {
                    try
                    {
                        Directory.CreateDirectory(path);
                    }
                    catch
                    {
                        return;
                    }
                }

                string filename = String.Format(formatFileName ?? "{0}{1:00}{2:00}{3}.txt", filenamePrefix, DateTime.Today.Month, DateTime.Today.Day, DateTime.Today.Year);
                try
                {
                    using (StreamWriter stream = File.AppendText(Path.Combine(path, filename)))
                    {
                        if (callback != null)
                        {
                            callback(stream);
                        }
                        if (ex != null)
                        {
                            var exMessage = ex.Message;
                            var innerEx = ex.InnerException;
                            var deep = 1;
                            while ((deep < 5) && (innerEx != null))
                            {
                                if (!string.IsNullOrEmpty(innerEx.Message))
                                {
                                    exMessage = string.Format("{0}\r\n {1}", exMessage, innerEx.Message);
                                }
                                innerEx = innerEx.InnerException;
                                deep++;
                            }
                            stream.Write(String.Format("{0} {1}\r\n {2}\r\n", exTime/*DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff")*/, exMessage/*ex.Message*/, ex.StackTrace));
                            stream.WriteLine("--------------------------------------------------\r\n");
                        }
                        stream.Flush();
                    }
                }
                catch
                {
                }
            }
        }

    }
}
