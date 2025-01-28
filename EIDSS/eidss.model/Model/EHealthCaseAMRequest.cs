using bv.common.Configuration;
using bv.model.BLToolkit;
using bv.model.Model.Core;
using eidss.model.Core;
using eidss.model.Helpers;
using eidss.model.Resources;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using bv.common.Core;
using Newtonsoft.Json.Linq;
using System.Xml.Serialization;

namespace eidss.model.Schema
{

    public partial class EHealthCaseAMRequest
    {
        partial void Disposed()
        {
            this.EHealthCaseAMItems.Clear();
        }

        public string Serialize()
        {
            var obj = this as eidss.model.Schema.EHealthCaseAMRequest;
            var xmlToSave = XmlBuilder.SerializeEHealthCaseAM(ModelUserContext.CurrentLanguage, obj.EHealthCaseAMItems.ToList<EHealthCaseAM>());
            return xmlToSave;
        }
    }

}
