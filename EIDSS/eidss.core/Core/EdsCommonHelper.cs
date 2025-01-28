using eidss.model.Enums;
using eidss.model.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace eidss.model.Core
{
    public static class EdsCommonHelper
    {
        #region Get EDS Messages

        private static string ConvertEdsResultCodeToShowErrId(EdsResultCode errCode)
        {
            return string.Format("msgEdsResult{0}", errCode.ToString());
        }

        private static string ConvertEdsResultCodeToSecurityLogErrId(EdsResultCode errCode)
        {
            return string.Format("msgEdsSecResult{0}", errCode.ToString());
        }

        private static string ConvertEdsResultCodeToErrId(EdsResultCode errCode, bool forSecurityLog = false)
        {
            return  (
                forSecurityLog ? 
                    ConvertEdsResultCodeToSecurityLogErrId(errCode) : 
                    ConvertEdsResultCodeToShowErrId(errCode)
                    );

        }

        public static string GetMessageByEdsResult(EdsResultCode errCode, bool forSecurityLog = false)
        {
            return GetMessageByEdsResult(errCode, string.Empty, forSecurityLog);
        }

        public static string GetMessageByEdsResult(string errId)
        {
            return GetMessageByEdsResult(errId, string.Empty);
        }

        public static string GetMessageByEdsResult(EdsResultCode errCode, string errDetails, bool forSecurityLog = false)
        {
            string errId = ConvertEdsResultCodeToErrId(errCode, forSecurityLog);
            return GetMessageByEdsResult(errId, errDetails);
        }

        public static string GetMessageByEdsResult(string errId, string errDetails)
        {
            string errorMessage = "";
            if (!string.IsNullOrEmpty(errId))
            {
                errorMessage = EidssMessages.Get(errId);
            }

            if (!string.IsNullOrEmpty(errDetails))
            {
                errorMessage = string.Format("{0} {1}", errorMessage, errDetails);
            }

            return errorMessage;
        }

        #endregion

        #region Reflect EDS Result To Security Log

        public static void ReflectResultToSecurityLog(SecurityAuditEvent ev, bool success, string errId, string description)
        {
            ReflectResultToSecurityLog(ev, success, errId, string.Empty, description);
        }

        public static void ReflectResultToSecurityLog(SecurityAuditEvent ev, bool success, EdsResultCode errCode, string description)
        {
            ReflectResultToSecurityLog(ev, success, errCode, string.Empty, description);
        }

        public static void ReflectResultToSecurityLog(SecurityAuditEvent ev, bool success, EdsResultCode errCode, string errDetails, string description)
        {
            string errId = ConvertEdsResultCodeToErrId(errCode, true);
            ReflectResultToSecurityLog(ev, success, errId, errDetails, description);
        }

        public static void ReflectResultToSecurityLog(SecurityAuditEvent ev, bool success, string errId, string errDetails, string description)
        {
            string errorMessage = GetMessageByEdsResult(errId, errDetails);

            if (!(bv.common.Core.Utils.IsEmpty(EidssUserContext.User.ID)))
            {
                SecurityLog.WriteToEventLogDB(EidssUserContext.User.ID, ev, success, null,
                    errorMessage, description, SecurityAuditProcessType.Eidss);
            }
        }

        #endregion

        #region EDS support methods

        public static bool IsEdsNeeded(SecurityAuditEvent securityEv)
        {
            if (EidssUserContext.User == null)
                return false;

            bool isNeeded;

            switch (securityEv)
            {
                case SecurityAuditEvent.SignEds:
                    isNeeded = EidssUserContext.User.SavingEds;
                    break;
                case SecurityAuditEvent.AuthorizationEds:
                    isNeeded = EidssUserContext.User.AuthorizationEds;
                    break;
                default:
                    isNeeded = false;
                    break;
            }

            return isNeeded;
        }

        // Check Certificate Key Type: Authorization or Signature
        public static bool IsCorrectKeyUsage(string keyUsage, SecurityAuditEvent securityEv)
        {
            bool isCorrect = false;

            if ((securityEv != SecurityAuditEvent.AuthorizationEds) && (securityEv != SecurityAuditEvent.SignEds))
                return true;

            if ((string.IsNullOrEmpty(keyUsage)) || ((!keyUsage.StartsWith("keyUsage=digitalSignature", StringComparison.InvariantCultureIgnoreCase))))
                return false;

            isCorrect =
                securityEv == SecurityAuditEvent.SignEds ?
                keyUsage.Contains("nonRepudiation") :
                (!keyUsage.Contains("nonRepudiation"));

            return isCorrect;
        }

        public static int IsRecalled(string recalledCheckInfo)
        {
            int isRecalled = -1; //Unknown result

            if (string.IsNullOrEmpty(recalledCheckInfo))
                return isRecalled;

            var recalledSplit = recalledCheckInfo.Split('\r');
            if ((recalledSplit == null) || (recalledSplit.Length == 0))
                return isRecalled;

            recalledSplit = recalledSplit[0].Split(':');
            if ((recalledSplit == null) || (recalledSplit.Length < 3))
                return isRecalled;

            string recalledStatus = recalledSplit[2].Trim();

            if (recalledStatus.Equals("good", StringComparison.InvariantCultureIgnoreCase))
                isRecalled = 0;//Good
            else if (recalledStatus.Equals("revoked", StringComparison.InvariantCultureIgnoreCase))
                isRecalled = 1;//Recalled

            return isRecalled;
        }

        public static string GetValueFromCertificateProperty(string propertyText)
        {
            string propertyValue = propertyText;
            if (string.IsNullOrEmpty(propertyValue))
                return string.Empty;

            if (!propertyValue.Contains("="))
                return propertyValue;

            var propertyParts = propertyValue.Split('=');
            if ((propertyParts != null) && (propertyParts.Length > 1))
            { 
                propertyValue = propertyParts[1];
            }
            return propertyValue;
        }

        private static bool ParseNotAfterDate(string certNotAfter, out DateTime? notAfter)
        {
            notAfter = null;
            bool res = false;

            if (string.IsNullOrEmpty(certNotAfter))
                return res;

            if (!certNotAfter.Contains("="))
                return res;

            var notAfterParts = certNotAfter.Split('=');
            if ((notAfterParts == null) || (notAfterParts.Length < 2))
                return res;

            var notAfterValuePart = certNotAfter.Split('=')[1];
            if (string.IsNullOrEmpty(notAfterValuePart))
                return res;

            notAfterParts = notAfterValuePart.Split(' ');
            if ((notAfterParts == null) || (notAfterParts.Length == 0))
                return res;

            string parseString = notAfterParts.Aggregate("", (x, y) => 
                            (string.IsNullOrEmpty(y) ||
                             (!y.Equals("ALMT", StringComparison.InvariantCultureIgnoreCase))) ? 
                                string.Format("{0} {1}", x, y) : 
                                x).Trim();

            if (string.IsNullOrEmpty(parseString))
                return res;

            DateTime parseDate;
            res = DateTime.TryParseExact(
                    parseString, 
                    "dd.MM.yyyy HH:mm:ss", 
                    System.Globalization.CultureInfo.InvariantCulture, 
                    DateTimeStyles.None, 
                    out parseDate);

            if (res)
                notAfter = parseDate;

            return res;
        }

        public static bool ValidateNotAfterDate(string certNotAfter, out DateTime? notAfter)
        {
            bool res = ParseNotAfterDate(certNotAfter, out notAfter);
            res = res && notAfter.HasValue;
            if (res)
            {
                res = (notAfter.Value > DateTime.Now);
            }
            return res;
        }

        public static string GetErrorTextFromException(Exception ex)
        {
            string errDetails;
            errDetails = (ex != null) ? ex.Message : string.Empty;
            if ((ex != null) && (ex.InnerException != null))
            {
                errDetails = string.Format("{0}\r\n{1}", errDetails, ex.InnerException.Message);
            }
            return errDetails;
        }

        #endregion
    }

    public enum EdsResultCode : int
    {
        Success = 0,
        FileReadError = 1,
        ActionCanceled = 2,
        WrongPassword = 3,
        NoCertificateInStore = 4,
        EdsExpired = 5,
        NotMatchingIinOrBin = 6,
        EdsRecalled = 7,
        EdsRecallStatusUnknown = 8,
        WrongAuthEdsKeyType = 9,
        WrongSignEdsKeyType = 10,
        EmptyIinOrBin = 11,
        NumberOfAttemptsExceeded = 12,
        NCALayerError = 13,
        Other = 14,
        Unknown = 15
    }

}
