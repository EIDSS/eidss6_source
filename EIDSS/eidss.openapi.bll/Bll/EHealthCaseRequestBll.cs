using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bv.model.BLToolkit;
using bv.model.Model.Core;
using eidss.model.Core;
using eidss.model.Resources;
using eidss.openapi.contract;
using eidss.openapi.bll.Converters;
using eidss.openapi.bll.Exceptions;
using EHealthCaseAM = eidss.model.Schema.EHealthCaseAM;
using EHealthCaseAMRequest = eidss.model.Schema.EHealthCaseAMRequest;

namespace eidss.openapi.bll.Bll
{
    public class EHealthCaseRequestBll
    {
        /// <summary>
        /// Validates Date Business Rule for a pair of date fields
        /// </summary>
        /// <param name="DLess">The value of the date that shall be less than or equal to another date</param>
        /// <param name="DGreater">The value of the date that shall be greater than or equal to another date</param>
        /// <param name="DLessName">The name of the date that shall be less than or equal to another date</param>
        /// <param name="DGreaterName">The name of the date that shall be greater than or equal to another date</param>
        /// <param name="msg">Message in case of detected Business Rule error</param>
        /// <returns>Returns true if at least one of the date is empty or both dates pass Business Rule, otherwise - false</returns>
        private static bool ValidateDateBR(DateTime? DLess, DateTime? DGreater, string DLessName, string DGreaterName, out string msg, bool greaterIsCurrentDate = false)
        {
            bool res = true;
            msg = string.Empty;

            if ((!DLess.HasValue) || (!DGreater.HasValue))
                return true;

            if ((DLess <= new DateTime(1900, 1, 1)) || (DGreater <= new DateTime(1900, 1, 1)))
                return true;

            DateTime less = DLess.Value;
            DateTime greater = DGreater.Value;

            if ((less.Hour != 0) || (less.Minute != 0) || (less.Second != 0))
            {
                less = less.ToUniversalTime();
            }
            if (((greater.Hour != 0) || (greater.Minute != 0) || (greater.Second != 0)) && !greaterIsCurrentDate)
            {
                greater = greater.ToUniversalTime();
            }

            if (less > greater)
            {
                msg = string.Format("{0} must be less than or equal to {1}", DLessName, DGreaterName);
                res = false;
            }

            return res;
        }

        /// <summary>
        /// Validates maximum length of the string field
        /// </summary>
        /// <param name="strName">Name of the field</param>
        /// <param name="strVal">String value of the field</param>
        /// <param name="maxLen">Allowed maximum length of the field</param>
        /// <param name="msg">Message in case of exceeded string length</param>
        /// <returns>Returns true if the value is empty or its length less than or equal to the specified Maximum Length, otherwise - false</returns>
        private static bool ValidateStringLength(string strName, string strVal, int maxLen, out string msg)
        {
            bool res = true;
            msg = string.Empty;

            if (string.IsNullOrEmpty(strVal) || maxLen <= 0)
                return true;

            if (strVal.Length > maxLen)
            {
                msg = string.Format(
                        "Max string length is exceeded ({0}{1})", 
                        maxLen.ToString(),
                        strName.Length > 0 ? string.Format(" for {0}", strName) : string.Empty);
                res = false;

            }

            return res;
        }

        /// <summary>
        /// Validates EHealthCase data contract for mandatory fields, maximum lengths of string fields, date business rules
        /// </summary>
        /// <param name="eHealthCaseItem">EHealthCase data contract</param>
        /// <param name="intRetCode">Output return code reflecting the result of the validation</param>
        /// <param name="strOutputComment">Output message describing the result of the validation</param>
        /// <returns></returns>
        private static bool ValidateEHealthCase(eidss.openapi.contract.EHealthCase eHealthCaseItem, out int? intRetCode, out string strOutputComment)
        {
            intRetCode = null;
            strOutputComment = string.Empty;
            int code;
            var err = string.Empty;
            var res = true;

            // Mandatory fields
            if (string.IsNullOrEmpty(eHealthCaseItem.Diagnosis))
            {
                code = (int)EHealthCaseErrorCode.EmptyDiagnosis;
                intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                err = "Empty Diagnosis";
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                res = false;
            }

            if ((eHealthCaseItem.DiagnosisDate == null) || (eHealthCaseItem.DiagnosisDate <= new DateTime(1900,1,1)))
            {
                code = (int)EHealthCaseErrorCode.NotSpecifiedDiagnosisDate;
                intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                err = "Not specified Diagnosis Date";
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                res = false;
            }

            if (string.IsNullOrEmpty(eHealthCaseItem.PersonalIDType))
            {
                code = (int)EHealthCaseErrorCode.EmptyPatientSsn;
                intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                err = "Empty Patient Personal ID Type";
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                res = false;
            }

            if (string.IsNullOrEmpty(eHealthCaseItem.PersonalID))
            {
                code = (int)EHealthCaseErrorCode.EmptyPatientSsn;
                intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                err = "Empty Patient SSN";
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                res = false;
            }

            if (string.IsNullOrEmpty(eHealthCaseItem.PatientsLastName))
            {
                code = (int)EHealthCaseErrorCode.EmptyPatientLastName;
                intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                err = "Empty Patient Last Name";
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                res = false;
            }

            if (string.IsNullOrEmpty(eHealthCaseItem.PatientsFirstName))
            {
                code = (int)EHealthCaseErrorCode.EmptyPatientFirstName;
                intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                err = "Empty Patient First Name";
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                res = false;
            }

            if ((eHealthCaseItem.PatientsDateOfBirth == null) || (eHealthCaseItem.PatientsDateOfBirth <= new DateTime(1900, 1, 1)))
            {
                code = (int)EHealthCaseErrorCode.NotSpecifiedDateOfBirth;
                intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                err = "Not specified Date of Birth";
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                res = false;
            }

            if (string.IsNullOrEmpty(eHealthCaseItem.PatientsSex))
            {
                code = (int)EHealthCaseErrorCode.EmptyPatientSex;
                intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                err = "Empty Patient Sex";
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                res = false;
            }

            if (eHealthCaseItem.PatientsPermanentResidenceRegion <= 0)
            {
                code = (int)EHealthCaseErrorCode.NotSpecifiedCRRegion;
                intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                err = "Not specified Permanent Residence Region";
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                res = false;
            }

            if (string.IsNullOrEmpty(eHealthCaseItem.HumanCaseLocalIdentifier))
            {
                code = (int)EHealthCaseErrorCode.EmptyHumanCaseLocalId;
                intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                err = "Empty Human Case Local Identifier";
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                res = false;
            }

            // Replace value with Null for non-mandatory dates with default values

            if ((eHealthCaseItem.DateOfCompletionOfPaperForm != null) && (eHealthCaseItem.DateOfCompletionOfPaperForm <= new DateTime(1900, 1, 1)))
            {
                eHealthCaseItem.DateOfCompletionOfPaperForm = null;
            }

            if ((eHealthCaseItem.NotificationDate != null) && (eHealthCaseItem.NotificationDate <= new DateTime(1900, 1, 1)))
            {
                eHealthCaseItem.NotificationDate = null;
            }

            if ((eHealthCaseItem.DateOfHospitalization != null) && (eHealthCaseItem.DateOfHospitalization <= new DateTime(1900, 1, 1)))
            {
                eHealthCaseItem.DateOfHospitalization = null;
            }

            if ((eHealthCaseItem.DateOfDeath != null) && (eHealthCaseItem.DateOfDeath <= new DateTime(1900, 1, 1)))
            {
                eHealthCaseItem.DateOfDeath = null;
            }

            // Date Business Rules

            //  1) Date of Birth <= Notification date
            if (!ValidateDateBR(eHealthCaseItem.PatientsDateOfBirth, eHealthCaseItem.NotificationDate, "Date of Birth", "Notification Date", out err))
            {
                code = (int)EHealthCaseErrorCode.DateBusinessRuleError;
                intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                res = false;
            }

            //  2) Date of Birth <= Current date
            if (!ValidateDateBR(eHealthCaseItem.PatientsDateOfBirth, DateTime.UtcNow, "Date of Birth", "Current Date", out err, true))
            {
                code = (int)EHealthCaseErrorCode.DateBusinessRuleError;
                intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                res = false;
            }

            //  3) Date of Birth <= Diagnosis date
            if (!ValidateDateBR(eHealthCaseItem.PatientsDateOfBirth, eHealthCaseItem.DiagnosisDate, "Date of Birth", "Diagnosis Date", out err))
            {
                code = (int)EHealthCaseErrorCode.DateBusinessRuleError;
                intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                res = false;
            }

            //  4) Date of Birth <= Date of hospitalization
            if (!ValidateDateBR(eHealthCaseItem.PatientsDateOfBirth, eHealthCaseItem.DateOfHospitalization, "Date of Birth", "Hospitalization Date", out err))
            {
                code = (int)EHealthCaseErrorCode.DateBusinessRuleError;
                intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                res = false;
            }

            //  5) Diagnosis date <= Current date
            if (!ValidateDateBR(eHealthCaseItem.DiagnosisDate, DateTime.UtcNow, "Diagnosis Date", "Current Date", out err, true))
            {
                code = (int)EHealthCaseErrorCode.DateBusinessRuleError;
                intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                res = false;
            }

            //  6) Daignosis date <= Date of completion of paper form
            if (!ValidateDateBR(eHealthCaseItem.DiagnosisDate, eHealthCaseItem.DateOfCompletionOfPaperForm, "Diagnosis Date", "Date of completion of paper form", out err))
            {
                code = (int)EHealthCaseErrorCode.DateBusinessRuleError;
                intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                res = false;
            }

            //  7) Date of Completion of Paper form <= Notification date
            if (!ValidateDateBR(eHealthCaseItem.DateOfCompletionOfPaperForm, eHealthCaseItem.NotificationDate, "Date of completion of paper form", "Notification Date", out err))
            {
                code = (int)EHealthCaseErrorCode.DateBusinessRuleError;
                intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                res = false;
            }

            //  8) Date of Death <= Current date
            if (!ValidateDateBR(eHealthCaseItem.DateOfDeath, DateTime.UtcNow, "Date of Death", "Current Date", out err, true))
            {
                code = (int)EHealthCaseErrorCode.DateBusinessRuleError;
                intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                res = false;
            }

            //  9) Notification date <= Current date
            if (!ValidateDateBR(eHealthCaseItem.NotificationDate, DateTime.UtcNow, "Notification Date", "Current Date", out err, true))
            {
                code = (int)EHealthCaseErrorCode.DateBusinessRuleError;
                intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                res = false;
            }

            // 10) Date of Hospitalization <= Current date
            if (!ValidateDateBR(eHealthCaseItem.DateOfHospitalization, DateTime.UtcNow, "Hospitalization Date", "Current Date", out err, true))
            {
                code = (int)EHealthCaseErrorCode.DateBusinessRuleError;
                intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                res = false;
            }

            //Maximum lengths
            if (!ValidateStringLength("Diagnosis", eHealthCaseItem.Diagnosis, 200, out err))
            {
                code = (int)EHealthCaseErrorCode.MaxLengthExceeded;
                intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                res = false;
            }

            if (!ValidateStringLength("Personal ID", eHealthCaseItem.PersonalID, 100, out err))
            {
                code = (int)EHealthCaseErrorCode.MaxLengthExceeded;
                intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                res = false;
            }

            if (!ValidateStringLength("Patient Last Name", eHealthCaseItem.PatientsLastName, 200, out err))
            {
                code = (int)EHealthCaseErrorCode.MaxLengthExceeded;
                intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                res = false;
            }

            if (!ValidateStringLength("Patient First Name", eHealthCaseItem.PatientsFirstName, 200, out err))
            {
                code = (int)EHealthCaseErrorCode.MaxLengthExceeded;
                intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                res = false;
            }

            if (!ValidateStringLength("Patient Middle Name", eHealthCaseItem.PatientsMiddleName, 200, out err))
            {
                eHealthCaseItem.PatientsMiddleName = string.Empty;
                //TODO: remove code for non-mandatory field with exceeded maximum length
                //code = (int)EHealthCaseErrorCode.MaxLengthExceeded;
                //intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                //res = false;
            }

            if (!ValidateStringLength("Patient Gender", eHealthCaseItem.PatientsSex, 200, out err))
            {
                code = (int)EHealthCaseErrorCode.MaxLengthExceeded;
                intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                res = false;
            }

            if (!ValidateStringLength("Patient Citizenship", eHealthCaseItem.PatientsCitizenship, 200, out err))
            {
                eHealthCaseItem.PatientsCitizenship = string.Empty;
                //TODO: remove code for non-mandatory field with exceeded maximum length
                //code = (int)EHealthCaseErrorCode.MaxLengthExceeded;
                //intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                //res = false;
            }

            if (!ValidateStringLength("Patient Current Residence Address", eHealthCaseItem.PatientsCurrentResidence, 2000, out err))
            {
                eHealthCaseItem.PatientsCurrentResidence = string.Empty;
                //TODO: remove code for non-mandatory field with exceeded maximum length
                //code = (int)EHealthCaseErrorCode.MaxLengthExceeded;
                //intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                //res = false;
            }

            if (!ValidateStringLength("Patient Permanent Residence Address", eHealthCaseItem.PatientsPermanentResidence, 2000, out err))
            {
                eHealthCaseItem.PatientsPermanentResidence = string.Empty;
                //TODO: remove code for non-mandatory field with exceeded maximum length
                //code = (int)EHealthCaseErrorCode.MaxLengthExceeded;
                //intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                //res = false;
            }

            if (!ValidateStringLength("Name of Employer", eHealthCaseItem.NameOfEmployer, 200, out err))
            {
                eHealthCaseItem.NameOfEmployer = string.Empty;
                //TODO: remove code for non-mandatory field with exceeded maximum length
                //code = (int)EHealthCaseErrorCode.MaxLengthExceeded;
                //intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                //res = false;
            }

            if (!ValidateStringLength("Patient Occupation", eHealthCaseItem.PatientsOccupation, 200, out err))
            {
                eHealthCaseItem.PatientsOccupation = string.Empty;
                //TODO: remove code for non-mandatory field with exceeded maximum length
                //code = (int)EHealthCaseErrorCode.MaxLengthExceeded;
                //intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                //res = false;
            }

            if (!ValidateStringLength("Hospital Tax ID", eHealthCaseItem.HospitalName, 200, out err))
            {
                eHealthCaseItem.HospitalName = string.Empty;
                //TODO: remove code for non-mandatory field with exceeded maximum length
                //code = (int)EHealthCaseErrorCode.MaxLengthExceeded;
                //intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                //res = false;
            }

            if (!ValidateStringLength("Outcome", eHealthCaseItem.Outcome, 200, out err))
            {
                eHealthCaseItem.Outcome = string.Empty;
                //TODO: remove code for non-mandatory field with exceeded maximum length
                //code = (int)EHealthCaseErrorCode.MaxLengthExceeded;
                //intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                //res = false;
            }

            if (!ValidateStringLength("Human Case Identifier", eHealthCaseItem.HumanCaseLocalIdentifier, 200, out err))
            {
                code = (int)EHealthCaseErrorCode.MaxLengthExceeded;
                intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                res = false;
            }

            if (!ValidateStringLength("Visit Identifier", eHealthCaseItem.VisitIdentifier, 200, out err))
            {
                eHealthCaseItem.VisitIdentifier = string.Empty;
                //TODO: remove code for non-mandatory field with exceeded maximum length
                //code = (int)EHealthCaseErrorCode.MaxLengthExceeded;
                //intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                //res = false;
            }

            if (!ValidateStringLength("Notification Sent By Facility Tax ID", eHealthCaseItem.NotificationSentByFacility, 200, out err))
            {
                eHealthCaseItem.NotificationSentByFacility = string.Empty;
                //TODO: remove code for non-mandatory field with exceeded maximum length
                //code = (int)EHealthCaseErrorCode.MaxLengthExceeded;
                //intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                //res = false;
            }

            if (!ValidateStringLength("Notification Sent By Facility Name", eHealthCaseItem.NotificationSentByFacilityName, 2000, out err))
            {
                eHealthCaseItem.NotificationSentByFacilityName = eHealthCaseItem.NotificationSentByFacility;
                //TODO: remove code for non-mandatory field with exceeded maximum length
                //code = (int)EHealthCaseErrorCode.MaxLengthExceeded;
                //intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                //res = false;
            }

            if (!ValidateStringLength("Notification Sent By Facility Phone", eHealthCaseItem.NotificationSentByFacilityPhone, 200, out err))
            {
                eHealthCaseItem.NotificationSentByFacilityPhone = string.Empty;
                //TODO: remove code for non-mandatory field with exceeded maximum length
                //code = (int)EHealthCaseErrorCode.MaxLengthExceeded;
                //intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                //res = false;
            }

            if (!ValidateStringLength("Notification Sent By Facility Address", eHealthCaseItem.NotificationSentByFacilityAddress, 2000, out err))
            {
                eHealthCaseItem.NotificationSentByFacilityAddress = string.Empty;
                //TODO: remove code for non-mandatory field with exceeded maximum length
                //code = (int)EHealthCaseErrorCode.MaxLengthExceeded;
                //intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                //res = false;
            }

            if (!ValidateStringLength("Notification Sent By Person SSN", eHealthCaseItem.NotificationSentBySsn, 200, out err))
            {
                eHealthCaseItem.NotificationSentBySsn = string.Empty;
                //TODO: remove code for non-mandatory field with exceeded maximum length
                //code = (int)EHealthCaseErrorCode.MaxLengthExceeded;
                //intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                //res = false;
            }

            if (!ValidateStringLength("Notification Sent By Person Name", eHealthCaseItem.NotificationSentByName, 200, out err))//700
            {
                eHealthCaseItem.NotificationSentByName = eHealthCaseItem.NotificationSentBySsn;
                //TODO: remove code for non-mandatory field with exceeded maximum length
                //code = (int)EHealthCaseErrorCode.MaxLengthExceeded;
                //intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                //res = false;
            }

            if (!ValidateStringLength("Notification Sent By Person Phone", eHealthCaseItem.NotificationSentByMobile, 200, out err))
            {
                eHealthCaseItem.NotificationSentByMobile = string.Empty;
                //TODO: remove code for non-mandatory field with exceeded maximum length
                //code = (int)EHealthCaseErrorCode.MaxLengthExceeded;
                //intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                //res = false;
            }

            if (!ValidateStringLength("Hospitalization Place Tax ID", eHealthCaseItem.PlaceOfHospitalization, 200, out err))
            {
                eHealthCaseItem.PlaceOfHospitalization = string.Empty;
                //TODO: remove code for non-mandatory field with exceeded maximum length
                //code = (int)EHealthCaseErrorCode.MaxLengthExceeded;
                //intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                //res = false;
            }

            if (!ValidateStringLength("Hospitalization Place Name", eHealthCaseItem.PlaceOfHospitalizationName, 2000, out err))
            {
                eHealthCaseItem.PlaceOfHospitalizationName = eHealthCaseItem.PlaceOfHospitalization;
                //TODO: remove code for non-mandatory field with exceeded maximum length
                //code = (int)EHealthCaseErrorCode.MaxLengthExceeded;
                //intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                //res = false;
            }

            if (!ValidateStringLength("Hospitalization Place Phone", eHealthCaseItem.PlaceOfHospitalizationPhone, 200, out err))
            {
                eHealthCaseItem.PlaceOfHospitalizationPhone = string.Empty;
                //TODO: remove code for non-mandatory field with exceeded maximum length
                //code = (int)EHealthCaseErrorCode.MaxLengthExceeded;
                //intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                //res = false;
            }

            if (!ValidateStringLength("Hospitalization Place Address", eHealthCaseItem.PlaceOfHospitalizationAddress, 2000, out err))
            {
                eHealthCaseItem.PlaceOfHospitalizationAddress = string.Empty;
                //TODO: remove code for non-mandatory field with exceeded maximum length
                //code = (int)EHealthCaseErrorCode.MaxLengthExceeded;
                //intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                //res = false;
            }

            if (!ValidateStringLength("Personal ID Type", eHealthCaseItem.PersonalIDType, 200, out err))
            {
                code = (int)EHealthCaseErrorCode.MaxLengthExceeded;
                intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                res = false;
            }

            if (!ValidateStringLength("Patient Status at the Time of the Notification", eHealthCaseItem.StatusOfPatientAtTimeOfNotification, 200, out err))
            {
                eHealthCaseItem.StatusOfPatientAtTimeOfNotification = string.Empty;
                //TODO: remove code for non-mandatory field with exceeded maximum length
                //code = (int)EHealthCaseErrorCode.MaxLengthExceeded;
                //intRetCode = !intRetCode.HasValue ? code : intRetCode.Value | code;
                strOutputComment = string.IsNullOrEmpty(strOutputComment) ? err : string.Format("{0}; {1}", strOutputComment, err);
                //res = false;
            }

            return res;
        }

        /// <summary>
        /// Raises an exception if the code reflecting validation and post outputs requires
        /// </summary>
        /// <param name="request">EHealthCaseAMREquest model to check for the output code</param>
        private static void RaiseErrorIfNeeded(EHealthCaseAMRequest request)
        {
            if (request == null)
                return;
            if (!request.intRetCode.HasValue)
                return;

            EHealthCaseErrorCode checkCode;
            
            if (!Enum.TryParse<EHealthCaseErrorCode>(request.intRetCode.Value.ToString(), out checkCode))
            {
                return;
            }

            if ((checkCode & EHealthCaseErrorCode.WrongJsonSchema) != 0)
            {
                // Wrong Json Schema
                throw new ModelValidationException(request.strOutputComment, null);
            }

            if (((checkCode & EHealthCaseErrorCode.EmptyDiagnosis) != 0) || 
                ((checkCode & EHealthCaseErrorCode.NotSpecifiedDiagnosisDate) != 0) ||
                ((checkCode & EHealthCaseErrorCode.EmptyPatientFirstName) != 0) ||
                ((checkCode & EHealthCaseErrorCode.EmptyPatientLastName) != 0) ||
                ((checkCode & EHealthCaseErrorCode.NotSpecifiedDateOfBirth) != 0) ||
                ((checkCode & EHealthCaseErrorCode.EmptyPatientSex) != 0) ||
                ((checkCode & EHealthCaseErrorCode.EmptyPatientSsn) != 0) ||
                ((checkCode & EHealthCaseErrorCode.NotSpecifiedCRRegion) != 0) ||
                ((checkCode & EHealthCaseErrorCode.EmptyHumanCaseLocalId) != 0))
            {
                // Mandatory Fields
                throw new ModelValidationException(request.strOutputComment, null);
            }

            if ((checkCode & EHealthCaseErrorCode.DateBusinessRuleError) != 0)
            {
                // Date Business Rule Error
                throw new ModelValidationException(request.strOutputComment, null);
            }

            if ((checkCode & EHealthCaseErrorCode.NotExistingDiagnosis) != 0)
            {
                // Diagnosis does not belong to the concordance table
                throw new ModelValidationException(request.strOutputComment, null);
            }

            if ((checkCode & EHealthCaseErrorCode.NotExistingPersonalIDType) != 0)
            {
                // Personal ID Type does not belong to the concordance table
                throw new ModelValidationException(request.strOutputComment, null);
            }

            if ((checkCode & EHealthCaseErrorCode.NotExistingPatientSex) != 0)
            {
                // Patient Sex does not belong to the concordance table
                throw new ModelValidationException(request.strOutputComment, null);
            }

            if ((checkCode & EHealthCaseErrorCode.MaxLengthExceeded) != 0)
            {
                // Max length is exceeded for a field
                throw new ModelValidationException(request.strOutputComment, null);
            }

            if ((checkCode & EHealthCaseErrorCode.ExistingCaseNotMatchPatient) != 0)
            {
                // Personal ID of the existing human case doesn't match new Personal ID (ssn)
                throw new ModelValidationException(request.strOutputComment, null);
            }

            if ((checkCode & EHealthCaseErrorCode.OtherError) != 0)
            {
                //Other Error
                throw new ModelValidationException(request.strOutputComment, null);
            }
        }

        /// <summary>
        /// Raises an exception if the code reflecting validation and post outputs requires
        /// </summary>
        /// <param name="checkCode">Code to check for raising an exception</param>
        /// <param name="strOutputComment">Output comment to provide in the exception</param>
        public static void RaiseErrorIfNeeded(EHealthCaseErrorCode checkCode, string strOutputComment)
        {
            if ((checkCode & EHealthCaseErrorCode.WrongJsonSchema) != 0)
            {
                // Wrong Json Schema
                throw new ModelValidationException(strOutputComment, null);
            }

            if (((checkCode & EHealthCaseErrorCode.EmptyDiagnosis) != 0) ||
                ((checkCode & EHealthCaseErrorCode.NotSpecifiedDiagnosisDate) != 0) ||
                ((checkCode & EHealthCaseErrorCode.EmptyPatientFirstName) != 0) ||
                ((checkCode & EHealthCaseErrorCode.EmptyPatientLastName) != 0) ||
                ((checkCode & EHealthCaseErrorCode.NotSpecifiedDateOfBirth) != 0) ||
                ((checkCode & EHealthCaseErrorCode.EmptyPatientSex) != 0) ||
                ((checkCode & EHealthCaseErrorCode.EmptyPatientSsn) != 0) ||
                ((checkCode & EHealthCaseErrorCode.NotSpecifiedCRRegion) != 0) ||
                ((checkCode & EHealthCaseErrorCode.EmptyHumanCaseLocalId) != 0))
            {
                // Mandatory Fields
                throw new ModelValidationException(strOutputComment, null);
            }

            if ((checkCode & EHealthCaseErrorCode.DateBusinessRuleError) != 0)
            {
                // Date Business Rule Error
                throw new ModelValidationException(strOutputComment, null);
            }

            if ((checkCode & EHealthCaseErrorCode.NotExistingDiagnosis) != 0)
            {
                // Diagnosis does not belong to the concordance table
                throw new ModelValidationException(strOutputComment, null);
            }

            if ((checkCode & EHealthCaseErrorCode.NotExistingPersonalIDType) != 0)
            {
                // Personal ID Type does not belong to the concordance table
                throw new ModelValidationException(strOutputComment, null);
            }

            if ((checkCode & EHealthCaseErrorCode.NotExistingPatientSex) != 0)
            {
                // Patient Sex does not belong to the concordance table
                throw new ModelValidationException(strOutputComment, null);
            }

            if ((checkCode & EHealthCaseErrorCode.MaxLengthExceeded) != 0)
            {
                // Max length is exceeded for a field
                throw new ModelValidationException(strOutputComment, null);
            }

            if ((checkCode & EHealthCaseErrorCode.ExistingCaseNotMatchPatient) != 0)
            {
                // Personal ID of the existing human case doesn't match new Personal ID (ssn)
                throw new ModelValidationException(strOutputComment, null);
            }

            if ((checkCode & EHealthCaseErrorCode.OtherError) != 0)
            {
                //Other Error
                throw new ModelValidationException(strOutputComment, null);
            }
        }


        /// <summary>
        /// Implements CreateCase method in the EHealth Controller of Open API
        /// </summary>
        /// <param name="eHealthCase">EHealthCaseRequest data contract to transfer case(s)</param>
        /// <param name="outputCode">Output code reflecting the result of the validation and post procedures</param>
        /// <param name="strOutcomeComment">Output comment describing the result of the validation and post procedures</param>
        /// <returns>Returns EHealthCaseResponse data contract with the results of validation and post procedures</returns>
        public static eidss.openapi.contract.EHealthCaseResponse CreateCase(eidss.openapi.contract.EHealthCaseRequest eHealthCase, out EHealthCaseErrorCode outputCode, out string strOutcomeComment)
        {
            outputCode = EHealthCaseErrorCode.OtherError;
            strOutcomeComment = string.Empty;
            using (var manager = DbManagerFactory.Factory.Create(EidssUserContext.Instance))
            {
                var acc = EHealthCaseAMRequest.Accessor.Instance(null);
                var cs = acc.CreateNewT(manager, null, null);
                if (eHealthCase == null)
                {
                    cs.intRetCode = (int)EHealthCaseErrorCode.WrongJsonSchema;
                    cs.strOutputComment = "Wrong Json Schema";
                }
                else
                {
                    if ((eHealthCase.Values == null) || (eHealthCase.Values.Count == 0))
                    {
                        cs.intRetCode = (int)EHealthCaseErrorCode.NoCasesToTransfer;
                        cs.strOutputComment = "No cases to transfer";
                    }
                    else
                    {
                        if (cs != null)
                        {
                            cs.LangID = ModelUserContext.CurrentLanguage;
                            int? intRetCode;
                            string strOutputComment;
                            foreach (var item in eHealthCase.Values)
                            {
                                var accItem = EHealthCaseAM.Accessor.Instance(null);
                                var csItem = accItem.CreateNew(manager, cs, null) as EHealthCaseAM;
                                if (csItem != null)
                                {
                                    intRetCode = null;
                                    strOutputComment = string.Empty;
                                    if (!ValidateEHealthCase(item, out intRetCode, out strOutputComment))
                                    {
                                        cs.intRetCode = intRetCode;
                                        cs.strOutputComment = strOutputComment;
                                        break;
                                    }
                                    if (!string.IsNullOrEmpty(strOutputComment))
                                    {
                                        cs.strOutputComment = strOutputComment;
                                    }

                                    csItem = EHealthCaseConverter.Instance.ToModel(manager, csItem, item);

                                    cs.EHealthCaseAMItems.Add(csItem);
                                }
                            }
                        }
                        //TODO: avoid BLToolkitExtension in the type name
                        //cs = EHealthCaseRequestConverter.Instance.ToModel(manager, cs, eHealthCase);

                        if (!cs.intRetCode.HasValue)
                        {
                            cs.Validation += (sender, args) =>
                            {
                                throw new ModelValidationException(args.MessageId, args.Pars);
                            };

                            acc.Post(manager, cs);
                        }
                    }
                }

                var response = new EHealthCaseResponse();
                response.Result = cs.intRetCode.HasValue ? cs.intRetCode.Value.ToString() : "Unknown";
                response.Description = cs.strOutputComment;

                if ((cs != null) && (cs.intRetCode.HasValue))
                {
                    EHealthCaseErrorCode checkCode;

                    if (Enum.TryParse<EHealthCaseErrorCode>(cs.intRetCode.Value.ToString(), out checkCode))
                    {
                        outputCode = checkCode;
                        strOutcomeComment = cs.strOutputComment;
                    } 
                }

                //TODO: remove when verified solution with the exception in the controller
                //RaiseErrorIfNeeded(cs);

                return response;
            }
        }

        /// <summary>
        /// Implements CreateCase method in the EHealth region of Soap Service
        /// </summary>
        /// <param name="eHealthCase">EHealthCaseRequest data contract to transfer case(s)</param>
        /// <returns>Returns EHealthCaseResponse data contract with the results of validation and post procedures</returns>
        public static eidss.openapi.contract.EHealthCaseResponse CreateCase(eidss.openapi.contract.EHealthCaseRequest eHealthCase)
        {
            EHealthCaseResponse response = null;
            try
            {
                EHealthCaseErrorCode checkCode;
                string strOutcomeComment = string.Empty;
                response = CreateCase(eHealthCase, out checkCode, out strOutcomeComment);
                RaiseErrorIfNeeded(checkCode, strOutcomeComment);
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
    }

    /// <summary>
    /// Output codes matching results of the validation and post procedures for EHealthCaseRequest data contract
    /// </summary>
    public enum EHealthCaseErrorCode
    {
        Success = 0,//Not an error
        WrongJsonSchema = 1,
        NoCasesToTransfer = 2,//Not an error
        EmptyDiagnosis = 4,
        NotSpecifiedDiagnosisDate = 8,
        EmptyPatientSsn = 16,
        EmptyPatientLastName = 32,
        EmptyPatientFirstName = 64,
        NotSpecifiedDateOfBirth = 128,
        EmptyPatientSex = 256,
        NotSpecifiedCRRegion = 512,
        EmptyHumanCaseLocalId = 1024,
        DateBusinessRuleError = 2048,
        NotExistingDiagnosis = 4096,
        NotExistingCRRegion = 8192,//Not an error
        NotExistingPersonalIDType = 16384,
        NotExistingPatientSex = 32768,
        ExistingCaseNotMatchPatient = 65536,
        MaxLengthExceeded = 131072,
        OtherError = 262144,
    }

}