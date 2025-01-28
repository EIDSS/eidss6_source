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
    public static class XmlBuilder
    {

        private static void AppendNodeWithInnerText(StringBuilder xmlBuilder, object name, object value, bool skipInCaseOfEmptyValue = false)
        {
            if (skipInCaseOfEmptyValue && Utils.IsEmpty(value))
                return;

            if (xmlBuilder.Length > 0)
            {
                xmlBuilder.AppendLine();
            }
            var strValue = string.Empty;
            if ((value != null) && (value is DateTime))
            {
                strValue = ((DateTime)value).ToString("yyyy-MM-dd hh:mm:ss.fff");
            }
            else
            {
                strValue = Utils.Str(value);
            }
            xmlBuilder.AppendFormat(@"<{0}>{1}</{0}>", name, SecurityElement.Escape(strValue));
        }

        private static void AppendOpenningNode(StringBuilder xmlBuilder, object name)
        {
            if (xmlBuilder.Length > 0)
            {
                xmlBuilder.AppendLine();
            }
            xmlBuilder.AppendFormat(@"<{0}>", name);
        }

        private static void AppendClosingNode(StringBuilder xmlBuilder, object name)
        {
            if (xmlBuilder.Length > 0)
            {
                xmlBuilder.AppendLine();
            }
            xmlBuilder.AppendFormat(@"</{0}>", name);
        }


        public static string SerializeEHealthCaseAM(string langId, List<EHealthCaseAM> eHealthCaseAMList)
        {
            var xmlBuilder = new StringBuilder();
            AppendOpenningNode(xmlBuilder, "root");
            AppendNodeWithInnerText(xmlBuilder, "LangId", langId);
            AppendOpenningNode(xmlBuilder, "eHealthCaseAMList");

            if ((eHealthCaseAMList == null) || (eHealthCaseAMList.Count == 0))
            {
                AppendClosingNode(xmlBuilder, "root");
                return xmlBuilder.ToString();
            }

            int ind = 0;
            foreach (var eHealthCaseAMItem in eHealthCaseAMList)
            {
                AppendOpenningNode(xmlBuilder, string.Format("eHeathCaseAM", ind.ToString()));

                //AppendNodeWithInnerText(xmlBuilder, "idfEHealthCaseAMItem", ind);
                AppendNodeWithInnerText(xmlBuilder, "DateOfCompletionOfPaperForm", eHealthCaseAMItem.DateOfCompletionOfPaperForm, true);
                AppendNodeWithInnerText(xmlBuilder, "Diagnosis", eHealthCaseAMItem.Diagnosis);
                AppendNodeWithInnerText(xmlBuilder, "DiagnosisDate", eHealthCaseAMItem.DiagnosisDate, true);
                AppendNodeWithInnerText(xmlBuilder, "PersonalID", eHealthCaseAMItem.PersonalID);
                AppendNodeWithInnerText(xmlBuilder, "PatientsLastName", eHealthCaseAMItem.PatientsLastName);
                AppendNodeWithInnerText(xmlBuilder, "PatientsFirstName", eHealthCaseAMItem.PatientsFirstName);
                AppendNodeWithInnerText(xmlBuilder, "PatientsMiddleName", eHealthCaseAMItem.PatientsMiddleName);
                AppendNodeWithInnerText(xmlBuilder, "PatientsDateOfBirth", eHealthCaseAMItem.PatientsDateOfBirth, true);
                AppendNodeWithInnerText(xmlBuilder, "PatientsSex", eHealthCaseAMItem.PatientsSex);
                AppendNodeWithInnerText(xmlBuilder, "PatientsCitizenship", eHealthCaseAMItem.PatientsCitizenship);
                AppendNodeWithInnerText(xmlBuilder, "PatientsCurrentResidenceRegion", eHealthCaseAMItem.PatientsCurrentResidenceRegion.HasValue ? eHealthCaseAMItem.PatientsCurrentResidenceRegion : eHealthCaseAMItem.PatientsPermanentResidenceRegion, true);
                AppendNodeWithInnerText(xmlBuilder, "PatientsCurrentResidence", eHealthCaseAMItem.PatientsCurrentResidenceRegion.HasValue ? eHealthCaseAMItem.PatientsCurrentResidence : eHealthCaseAMItem.PatientsPermanentResidence);
                AppendNodeWithInnerText(xmlBuilder, "PatientsPermanentResidenceRegion", eHealthCaseAMItem.PatientsPermanentResidenceRegion, true);
                AppendNodeWithInnerText(xmlBuilder, "PatientsPermanentResidence", eHealthCaseAMItem.PatientsPermanentResidence);
                AppendNodeWithInnerText(xmlBuilder, "NameOfEmployer", eHealthCaseAMItem.NameOfEmployer);
                AppendNodeWithInnerText(xmlBuilder, "PatientsOccupation", eHealthCaseAMItem.PatientsOccupation);
                AppendNodeWithInnerText(xmlBuilder, "HospitalName", eHealthCaseAMItem.HospitalName);
                AppendNodeWithInnerText(xmlBuilder, "Outcome", eHealthCaseAMItem.Outcome);
                AppendNodeWithInnerText(xmlBuilder, "DateOfDeath", eHealthCaseAMItem.DateOfDeath, true);
                AppendNodeWithInnerText(xmlBuilder, "HumanCaseLocalIdentifier", eHealthCaseAMItem.HumanCaseLocalIdentifier);
                AppendNodeWithInnerText(xmlBuilder, "VisitIdentifier", eHealthCaseAMItem.VisitIdentifier);
                AppendNodeWithInnerText(xmlBuilder, "NotificationSentByFacility", eHealthCaseAMItem.NotificationSentByFacility);
                AppendNodeWithInnerText(xmlBuilder, "NotificationSentByFacilityName", eHealthCaseAMItem.NotificationSentByFacilityName);
                AppendNodeWithInnerText(xmlBuilder, "NotificationSentByFacilityPhone", eHealthCaseAMItem.NotificationSentByFacilityPhone);
                AppendNodeWithInnerText(xmlBuilder, "NotificationSentByFacilityRegion", eHealthCaseAMItem.NotificationSentByFacilityRegion);
                AppendNodeWithInnerText(xmlBuilder, "NotificationSentByFacilityAddress", eHealthCaseAMItem.NotificationSentByFacilityAddress);
                AppendNodeWithInnerText(xmlBuilder, "NotificationSentByName", eHealthCaseAMItem.NotificationSentByName);
                AppendNodeWithInnerText(xmlBuilder, "NotificationSentBySsn", eHealthCaseAMItem.NotificationSentBySsn);
                AppendNodeWithInnerText(xmlBuilder, "NotificationSentByMobile", eHealthCaseAMItem.NotificationSentByMobile);
                AppendNodeWithInnerText(xmlBuilder, "NotificationDate", eHealthCaseAMItem.NotificationDate, true);
                AppendNodeWithInnerText(xmlBuilder, "PlaceOfHospitalization", eHealthCaseAMItem.PlaceOfHospitalization);
                AppendNodeWithInnerText(xmlBuilder, "PlaceOfHospitalizationName", eHealthCaseAMItem.PlaceOfHospitalizationName);
                AppendNodeWithInnerText(xmlBuilder, "PlaceOfHospitalizationPhone", eHealthCaseAMItem.PlaceOfHospitalizationPhone);
                AppendNodeWithInnerText(xmlBuilder, "PlaceOfHospitalizationRegion", eHealthCaseAMItem.PlaceOfHospitalizationRegion);
                AppendNodeWithInnerText(xmlBuilder, "PlaceOfHospitalizationAddress", eHealthCaseAMItem.PlaceOfHospitalizationAddress);
                AppendNodeWithInnerText(xmlBuilder, "DateOfHospitalization", eHealthCaseAMItem.DateOfHospitalization, true);
                AppendNodeWithInnerText(xmlBuilder, "PersonalIDType", eHealthCaseAMItem.PersonalIDType);
                AppendNodeWithInnerText(xmlBuilder, "Hospitalization", eHealthCaseAMItem.Hospitalization, true);
                AppendNodeWithInnerText(xmlBuilder, "StatusOfPatientAtTimeOfNotification", eHealthCaseAMItem.StatusOfPatientAtTimeOfNotification);
                
                AppendClosingNode(xmlBuilder, "eHeathCaseAM");

                ind++;
            }

            AppendClosingNode(xmlBuilder, "eHealthCaseAMList");
            AppendClosingNode(xmlBuilder, "root");
            return xmlBuilder.ToString();
        }

    }
}
