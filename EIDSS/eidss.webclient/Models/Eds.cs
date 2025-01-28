using bv.common.Core;
using eidss.model.Core;
using eidss.model.Core.Security;
using eidss.model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace eidss.webclient.Models
{
    public class Eds
    {
        public string Xml { get; set; }
        public string OriginalXml { get; set; }
        public bool IsChangePassword { get; set; }
    }
}