﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.IO;
using log4net.Repository.Hierarchy;
using log4net.Appender;

namespace eidss.openapi.web.Utils
{
    internal class Logger
    {
        private ILog _logger;
        private static Logger _instance;
        private Logger()
        {
            _logger = LogManager.GetLogger("openapi");
        }
        static Logger()
        {
            _instance = new Logger();
        }
        public static Logger Instance { get { return _instance; } }

        public void LogStart(string obj, string name)
        {
            _logger.WarnFormat("{0} - {1} started", obj, name);
        }
        public void LogFinish(string obj, string name)
        {
            _logger.WarnFormat("{0} - {1} finished", obj, name);
        }

        public void LogError(string obj, string name, string code, Exception e)
        {
            _logger.Error(string.Format("{0} - {1} failed with code {2}", obj, name, code), e);
        }

        public void LogInfo(string obj, string name, object data1)
        {
            _logger.InfoFormat("{0} - {1}: {2}", obj, name, data1.ToString());
        }
        public void LogInfo(string obj, string name, object data1, object data2)
        {
            _logger.InfoFormat("{0} - {1}: {2}, {3}", obj, name, data1.ToString(), data2.ToString());
        }
        public O LogInfoReturn<O>(string obj, string name, O data)
        {
            _logger.InfoFormat("{0} - {1}: return {2}", obj, name, data.ToString());
            return data;
        }
        public O LogInfoReturn<O>(string obj, string name, O data, string data2)
        {
            _logger.InfoFormat("{0} - {1}: return {2}, {3}", obj, name, data.ToString(), data2);
            return data;
        }

    }

    /// <summary>
    /// Custom Lock for Logger to delete empty log file when lock is released
    /// </summary>
    public class LoggerLock : log4net.Appender.FileAppender.MinimalLock
    {
        public override void ReleaseLock()
        {
            base.ReleaseLock();

            if (CurrentAppender == null)
                return;

            if (string.IsNullOrWhiteSpace(CurrentAppender.File))
                return;

            try
            {
                var logFile = new FileInfo(CurrentAppender.File);
                if (logFile.Exists && logFile.Length <= 0)
                {
                    logFile.Delete();
                }
            }
            catch(Exception)
            {
                //ignore unsuccessful attempt to delete empty log file
            }
        }
    }
}