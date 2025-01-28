using eidss.model.Trace;
using eidss.model.WcfService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIDSS.EHS.Service
{
    public sealed class EhsHostKeeper : ServiceHostKeeper
    {
        protected override Type ServiceType
        {
            get { return typeof(EhsFacade); }
        }

        protected override string DefaultServiceHostURL
        {
            get { return "http://localhost:50120/"; }
        }

        protected override string ServiceHostURLConfigName
        {
            get { return "EhsServiceHostURL"; }
        }

        protected override string TraceCategory
        {
            get { return TraceHelper.EhsCategory; }
        }
    }
}
