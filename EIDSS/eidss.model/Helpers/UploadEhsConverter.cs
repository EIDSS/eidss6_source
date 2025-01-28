using bv.common.Core;
using bv.model.BLToolkit;
using eidss.model.Core;
using eidss.model.Model.UploadEhs;
using eidss.model.Resources;
using eidss.model.Schema;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text;

namespace eidss.model.Helpers
{
    public static class UploadEhsConverter
    {

        private static void AppendNode(StringBuilder xmlBuilder, object name, object value)
        {
            xmlBuilder.AppendFormat(@"<{0}>{1}</{0}>", name, SecurityElement.Escape(Utils.Str(value)));
        }

        public static string GetExistingPatientItemListResolutionXml(List<UploadEhsExistingPatientItem> existingPatientItems)
        {
            var xmlBuilder = new StringBuilder();
            xmlBuilder.AppendLine(@"<root>");

            if ((existingPatientItems == null) || (existingPatientItems.Count == 0))
            {
                xmlBuilder.Append(@"</root>");
                return xmlBuilder.ToString();
            }

            foreach (var existingPatientItem in existingPatientItems)
            {
                xmlBuilder.Append(@"<patient>");

                AppendNode(xmlBuilder, "idfsUploadEhsPatientItem", existingPatientItem.idfsUploadEhsPatientItem);
                AppendNode(xmlBuilder, "Resolution", existingPatientItem.Resolution);

                xmlBuilder.Append(@"</patient>");
                xmlBuilder.AppendLine();
            }

            xmlBuilder.Append(@"</root>");
            return xmlBuilder.ToString();
        }

    }
}
