using eidss.model.Trace;
using eidss.model.WindowsService;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace EIDSS.EHS.Service
{
    class Program
    {
        private static void Main(string[] args)
        {
            var service = new EidssService(() => new EhsHostKeeper(), TraceHelper.EhsCategory);
            if (args.Contains("/c"))
            {
                service.RunInConsole();
            }
            else
            {
                ServiceBase.Run(service);
            }
        }
    }
}
