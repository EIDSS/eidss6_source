using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eidss.webclient.Models
{
    public class ParallelSessionsInfo
    {
        public bool ShowParallelSessionsMessage { get; set; }
    }

    public class DisconnectionInfo
    {
        public bool IsDisconnected { get; set; }
    }

    public class IsDisconnectionEnabledInfo
    {
        public bool Enabled { get; set; }
        public bool CommunicateWithNCALayer { get; set; }
    }
}