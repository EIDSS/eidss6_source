#pragma warning disable 0472,0414
using System;
using System.Text;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Xml.Serialization;
using BLToolkit.Aspects;
using BLToolkit.DataAccess;
using BLToolkit.EditableObjects;
using BLToolkit.Data;
using BLToolkit.Data.DataProvider;
using BLToolkit.Mapping;
using BLToolkit.Reflection;
using bv.common.Configuration;
using bv.common.Enums;
using bv.common.Core;
using bv.model.BLToolkit;
using bv.model.Model;
using bv.model.Helpers;
using bv.model.Model.Extenders;
using bv.model.Model.Core;
using bv.model.Model.Handlers;
using bv.model.Model.Validators;
using eidss.model.Core;
using eidss.model.Enums;
		

namespace eidss.model.Schema
{
        
        
    [XmlType(AnonymousType = true)]
    public abstract partial class EHealthCaseAM : 
        EditableObject<EHealthCaseAM>
        , IObject
        , IDisposable
        , ILookupUsage
        {
        
        [MapField(_str_DateOfCompletionOfPaperForm), NonUpdatable, PrimaryKey]
        public abstract DateTime? DateOfCompletionOfPaperForm { get; set; }
                
        [LocalizedDisplayName(_str_Diagnosis)]
        [MapField(_str_Diagnosis)]
        public abstract String Diagnosis { get; set; }
        protected String Diagnosis_Original { get { return ((EditableValue<String>)((dynamic)this)._diagnosis).OriginalValue; } }
        protected String Diagnosis_Previous { get { return ((EditableValue<String>)((dynamic)this)._diagnosis).PreviousValue; } }
                
        [LocalizedDisplayName(_str_DiagnosisDate)]
        [MapField(_str_DiagnosisDate)]
        public abstract DateTime? DiagnosisDate { get; set; }
        protected DateTime? DiagnosisDate_Original { get { return ((EditableValue<DateTime?>)((dynamic)this)._diagnosisDate).OriginalValue; } }
        protected DateTime? DiagnosisDate_Previous { get { return ((EditableValue<DateTime?>)((dynamic)this)._diagnosisDate).PreviousValue; } }
                
        [LocalizedDisplayName(_str_PersonalID)]
        [MapField(_str_PersonalID)]
        public abstract String PersonalID { get; set; }
        protected String PersonalID_Original { get { return ((EditableValue<String>)((dynamic)this)._personalID).OriginalValue; } }
        protected String PersonalID_Previous { get { return ((EditableValue<String>)((dynamic)this)._personalID).PreviousValue; } }
                
        [LocalizedDisplayName(_str_PatientsLastName)]
        [MapField(_str_PatientsLastName)]
        public abstract String PatientsLastName { get; set; }
        protected String PatientsLastName_Original { get { return ((EditableValue<String>)((dynamic)this)._patientsLastName).OriginalValue; } }
        protected String PatientsLastName_Previous { get { return ((EditableValue<String>)((dynamic)this)._patientsLastName).PreviousValue; } }
                
        [LocalizedDisplayName(_str_PatientsFirstName)]
        [MapField(_str_PatientsFirstName)]
        public abstract String PatientsFirstName { get; set; }
        protected String PatientsFirstName_Original { get { return ((EditableValue<String>)((dynamic)this)._patientsFirstName).OriginalValue; } }
        protected String PatientsFirstName_Previous { get { return ((EditableValue<String>)((dynamic)this)._patientsFirstName).PreviousValue; } }
                
        [LocalizedDisplayName(_str_PatientsMiddleName)]
        [MapField(_str_PatientsMiddleName)]
        public abstract String PatientsMiddleName { get; set; }
        protected String PatientsMiddleName_Original { get { return ((EditableValue<String>)((dynamic)this)._patientsMiddleName).OriginalValue; } }
        protected String PatientsMiddleName_Previous { get { return ((EditableValue<String>)((dynamic)this)._patientsMiddleName).PreviousValue; } }
                
        [LocalizedDisplayName(_str_PatientsDateOfBirth)]
        [MapField(_str_PatientsDateOfBirth)]
        public abstract DateTime? PatientsDateOfBirth { get; set; }
        protected DateTime? PatientsDateOfBirth_Original { get { return ((EditableValue<DateTime?>)((dynamic)this)._patientsDateOfBirth).OriginalValue; } }
        protected DateTime? PatientsDateOfBirth_Previous { get { return ((EditableValue<DateTime?>)((dynamic)this)._patientsDateOfBirth).PreviousValue; } }
                
        [LocalizedDisplayName(_str_PatientsSex)]
        [MapField(_str_PatientsSex)]
        public abstract String PatientsSex { get; set; }
        protected String PatientsSex_Original { get { return ((EditableValue<String>)((dynamic)this)._patientsSex).OriginalValue; } }
        protected String PatientsSex_Previous { get { return ((EditableValue<String>)((dynamic)this)._patientsSex).PreviousValue; } }
                
        [LocalizedDisplayName(_str_PatientsCitizenship)]
        [MapField(_str_PatientsCitizenship)]
        public abstract String PatientsCitizenship { get; set; }
        protected String PatientsCitizenship_Original { get { return ((EditableValue<String>)((dynamic)this)._patientsCitizenship).OriginalValue; } }
        protected String PatientsCitizenship_Previous { get { return ((EditableValue<String>)((dynamic)this)._patientsCitizenship).PreviousValue; } }
                
        [LocalizedDisplayName(_str_PatientsCurrentResidenceRegion)]
        [MapField(_str_PatientsCurrentResidenceRegion)]
        public abstract Int32? PatientsCurrentResidenceRegion { get; set; }
        protected Int32? PatientsCurrentResidenceRegion_Original { get { return ((EditableValue<Int32?>)((dynamic)this)._patientsCurrentResidenceRegion).OriginalValue; } }
        protected Int32? PatientsCurrentResidenceRegion_Previous { get { return ((EditableValue<Int32?>)((dynamic)this)._patientsCurrentResidenceRegion).PreviousValue; } }
                
        [LocalizedDisplayName(_str_PatientsCurrentResidence)]
        [MapField(_str_PatientsCurrentResidence)]
        public abstract String PatientsCurrentResidence { get; set; }
        protected String PatientsCurrentResidence_Original { get { return ((EditableValue<String>)((dynamic)this)._patientsCurrentResidence).OriginalValue; } }
        protected String PatientsCurrentResidence_Previous { get { return ((EditableValue<String>)((dynamic)this)._patientsCurrentResidence).PreviousValue; } }
                
        [LocalizedDisplayName(_str_PatientsPermanentResidenceRegion)]
        [MapField(_str_PatientsPermanentResidenceRegion)]
        public abstract Int32? PatientsPermanentResidenceRegion { get; set; }
        protected Int32? PatientsPermanentResidenceRegion_Original { get { return ((EditableValue<Int32?>)((dynamic)this)._patientsPermanentResidenceRegion).OriginalValue; } }
        protected Int32? PatientsPermanentResidenceRegion_Previous { get { return ((EditableValue<Int32?>)((dynamic)this)._patientsPermanentResidenceRegion).PreviousValue; } }
                
        [LocalizedDisplayName(_str_PatientsPermanentResidence)]
        [MapField(_str_PatientsPermanentResidence)]
        public abstract String PatientsPermanentResidence { get; set; }
        protected String PatientsPermanentResidence_Original { get { return ((EditableValue<String>)((dynamic)this)._patientsPermanentResidence).OriginalValue; } }
        protected String PatientsPermanentResidence_Previous { get { return ((EditableValue<String>)((dynamic)this)._patientsPermanentResidence).PreviousValue; } }
                
        [LocalizedDisplayName(_str_NameOfEmployer)]
        [MapField(_str_NameOfEmployer)]
        public abstract String NameOfEmployer { get; set; }
        protected String NameOfEmployer_Original { get { return ((EditableValue<String>)((dynamic)this)._nameOfEmployer).OriginalValue; } }
        protected String NameOfEmployer_Previous { get { return ((EditableValue<String>)((dynamic)this)._nameOfEmployer).PreviousValue; } }
                
        [LocalizedDisplayName(_str_PatientsOccupation)]
        [MapField(_str_PatientsOccupation)]
        public abstract String PatientsOccupation { get; set; }
        protected String PatientsOccupation_Original { get { return ((EditableValue<String>)((dynamic)this)._patientsOccupation).OriginalValue; } }
        protected String PatientsOccupation_Previous { get { return ((EditableValue<String>)((dynamic)this)._patientsOccupation).PreviousValue; } }
                
        [LocalizedDisplayName(_str_HospitalName)]
        [MapField(_str_HospitalName)]
        public abstract String HospitalName { get; set; }
        protected String HospitalName_Original { get { return ((EditableValue<String>)((dynamic)this)._hospitalName).OriginalValue; } }
        protected String HospitalName_Previous { get { return ((EditableValue<String>)((dynamic)this)._hospitalName).PreviousValue; } }
                
        [LocalizedDisplayName(_str_Outcome)]
        [MapField(_str_Outcome)]
        public abstract String Outcome { get; set; }
        protected String Outcome_Original { get { return ((EditableValue<String>)((dynamic)this)._outcome).OriginalValue; } }
        protected String Outcome_Previous { get { return ((EditableValue<String>)((dynamic)this)._outcome).PreviousValue; } }
                
        [LocalizedDisplayName(_str_DateOfDeath)]
        [MapField(_str_DateOfDeath)]
        public abstract DateTime? DateOfDeath { get; set; }
        protected DateTime? DateOfDeath_Original { get { return ((EditableValue<DateTime?>)((dynamic)this)._dateOfDeath).OriginalValue; } }
        protected DateTime? DateOfDeath_Previous { get { return ((EditableValue<DateTime?>)((dynamic)this)._dateOfDeath).PreviousValue; } }
                
        [LocalizedDisplayName(_str_HumanCaseLocalIdentifier)]
        [MapField(_str_HumanCaseLocalIdentifier)]
        public abstract String HumanCaseLocalIdentifier { get; set; }
        protected String HumanCaseLocalIdentifier_Original { get { return ((EditableValue<String>)((dynamic)this)._humanCaseLocalIdentifier).OriginalValue; } }
        protected String HumanCaseLocalIdentifier_Previous { get { return ((EditableValue<String>)((dynamic)this)._humanCaseLocalIdentifier).PreviousValue; } }
                
        [LocalizedDisplayName(_str_VisitIdentifier)]
        [MapField(_str_VisitIdentifier)]
        public abstract String VisitIdentifier { get; set; }
        protected String VisitIdentifier_Original { get { return ((EditableValue<String>)((dynamic)this)._visitIdentifier).OriginalValue; } }
        protected String VisitIdentifier_Previous { get { return ((EditableValue<String>)((dynamic)this)._visitIdentifier).PreviousValue; } }
                
        [LocalizedDisplayName(_str_NotificationSentByFacility)]
        [MapField(_str_NotificationSentByFacility)]
        public abstract String NotificationSentByFacility { get; set; }
        protected String NotificationSentByFacility_Original { get { return ((EditableValue<String>)((dynamic)this)._notificationSentByFacility).OriginalValue; } }
        protected String NotificationSentByFacility_Previous { get { return ((EditableValue<String>)((dynamic)this)._notificationSentByFacility).PreviousValue; } }
                
        [LocalizedDisplayName(_str_NotificationSentByFacilityName)]
        [MapField(_str_NotificationSentByFacilityName)]
        public abstract String NotificationSentByFacilityName { get; set; }
        protected String NotificationSentByFacilityName_Original { get { return ((EditableValue<String>)((dynamic)this)._notificationSentByFacilityName).OriginalValue; } }
        protected String NotificationSentByFacilityName_Previous { get { return ((EditableValue<String>)((dynamic)this)._notificationSentByFacilityName).PreviousValue; } }
                
        [LocalizedDisplayName(_str_NotificationSentByFacilityPhone)]
        [MapField(_str_NotificationSentByFacilityPhone)]
        public abstract String NotificationSentByFacilityPhone { get; set; }
        protected String NotificationSentByFacilityPhone_Original { get { return ((EditableValue<String>)((dynamic)this)._notificationSentByFacilityPhone).OriginalValue; } }
        protected String NotificationSentByFacilityPhone_Previous { get { return ((EditableValue<String>)((dynamic)this)._notificationSentByFacilityPhone).PreviousValue; } }
                
        [LocalizedDisplayName(_str_NotificationSentByFacilityRegion)]
        [MapField(_str_NotificationSentByFacilityRegion)]
        public abstract Int32? NotificationSentByFacilityRegion { get; set; }
        protected Int32? NotificationSentByFacilityRegion_Original { get { return ((EditableValue<Int32?>)((dynamic)this)._notificationSentByFacilityRegion).OriginalValue; } }
        protected Int32? NotificationSentByFacilityRegion_Previous { get { return ((EditableValue<Int32?>)((dynamic)this)._notificationSentByFacilityRegion).PreviousValue; } }
                
        [LocalizedDisplayName(_str_NotificationSentByFacilityAddress)]
        [MapField(_str_NotificationSentByFacilityAddress)]
        public abstract String NotificationSentByFacilityAddress { get; set; }
        protected String NotificationSentByFacilityAddress_Original { get { return ((EditableValue<String>)((dynamic)this)._notificationSentByFacilityAddress).OriginalValue; } }
        protected String NotificationSentByFacilityAddress_Previous { get { return ((EditableValue<String>)((dynamic)this)._notificationSentByFacilityAddress).PreviousValue; } }
                
        [LocalizedDisplayName(_str_NotificationSentByName)]
        [MapField(_str_NotificationSentByName)]
        public abstract String NotificationSentByName { get; set; }
        protected String NotificationSentByName_Original { get { return ((EditableValue<String>)((dynamic)this)._notificationSentByName).OriginalValue; } }
        protected String NotificationSentByName_Previous { get { return ((EditableValue<String>)((dynamic)this)._notificationSentByName).PreviousValue; } }
                
        [LocalizedDisplayName(_str_NotificationSentBySsn)]
        [MapField(_str_NotificationSentBySsn)]
        public abstract String NotificationSentBySsn { get; set; }
        protected String NotificationSentBySsn_Original { get { return ((EditableValue<String>)((dynamic)this)._notificationSentBySsn).OriginalValue; } }
        protected String NotificationSentBySsn_Previous { get { return ((EditableValue<String>)((dynamic)this)._notificationSentBySsn).PreviousValue; } }
                
        [LocalizedDisplayName(_str_NotificationSentByMobile)]
        [MapField(_str_NotificationSentByMobile)]
        public abstract String NotificationSentByMobile { get; set; }
        protected String NotificationSentByMobile_Original { get { return ((EditableValue<String>)((dynamic)this)._notificationSentByMobile).OriginalValue; } }
        protected String NotificationSentByMobile_Previous { get { return ((EditableValue<String>)((dynamic)this)._notificationSentByMobile).PreviousValue; } }
                
        [LocalizedDisplayName(_str_NotificationDate)]
        [MapField(_str_NotificationDate)]
        public abstract DateTime? NotificationDate { get; set; }
        protected DateTime? NotificationDate_Original { get { return ((EditableValue<DateTime?>)((dynamic)this)._notificationDate).OriginalValue; } }
        protected DateTime? NotificationDate_Previous { get { return ((EditableValue<DateTime?>)((dynamic)this)._notificationDate).PreviousValue; } }
                
        [LocalizedDisplayName(_str_PlaceOfHospitalization)]
        [MapField(_str_PlaceOfHospitalization)]
        public abstract String PlaceOfHospitalization { get; set; }
        protected String PlaceOfHospitalization_Original { get { return ((EditableValue<String>)((dynamic)this)._placeOfHospitalization).OriginalValue; } }
        protected String PlaceOfHospitalization_Previous { get { return ((EditableValue<String>)((dynamic)this)._placeOfHospitalization).PreviousValue; } }
                
        [LocalizedDisplayName(_str_PlaceOfHospitalizationName)]
        [MapField(_str_PlaceOfHospitalizationName)]
        public abstract String PlaceOfHospitalizationName { get; set; }
        protected String PlaceOfHospitalizationName_Original { get { return ((EditableValue<String>)((dynamic)this)._placeOfHospitalizationName).OriginalValue; } }
        protected String PlaceOfHospitalizationName_Previous { get { return ((EditableValue<String>)((dynamic)this)._placeOfHospitalizationName).PreviousValue; } }
                
        [LocalizedDisplayName(_str_PlaceOfHospitalizationPhone)]
        [MapField(_str_PlaceOfHospitalizationPhone)]
        public abstract String PlaceOfHospitalizationPhone { get; set; }
        protected String PlaceOfHospitalizationPhone_Original { get { return ((EditableValue<String>)((dynamic)this)._placeOfHospitalizationPhone).OriginalValue; } }
        protected String PlaceOfHospitalizationPhone_Previous { get { return ((EditableValue<String>)((dynamic)this)._placeOfHospitalizationPhone).PreviousValue; } }
                
        [LocalizedDisplayName(_str_PlaceOfHospitalizationRegion)]
        [MapField(_str_PlaceOfHospitalizationRegion)]
        public abstract Int32? PlaceOfHospitalizationRegion { get; set; }
        protected Int32? PlaceOfHospitalizationRegion_Original { get { return ((EditableValue<Int32?>)((dynamic)this)._placeOfHospitalizationRegion).OriginalValue; } }
        protected Int32? PlaceOfHospitalizationRegion_Previous { get { return ((EditableValue<Int32?>)((dynamic)this)._placeOfHospitalizationRegion).PreviousValue; } }
                
        [LocalizedDisplayName(_str_PlaceOfHospitalizationAddress)]
        [MapField(_str_PlaceOfHospitalizationAddress)]
        public abstract String PlaceOfHospitalizationAddress { get; set; }
        protected String PlaceOfHospitalizationAddress_Original { get { return ((EditableValue<String>)((dynamic)this)._placeOfHospitalizationAddress).OriginalValue; } }
        protected String PlaceOfHospitalizationAddress_Previous { get { return ((EditableValue<String>)((dynamic)this)._placeOfHospitalizationAddress).PreviousValue; } }
                
        [LocalizedDisplayName(_str_DateOfHospitalization)]
        [MapField(_str_DateOfHospitalization)]
        public abstract DateTime? DateOfHospitalization { get; set; }
        protected DateTime? DateOfHospitalization_Original { get { return ((EditableValue<DateTime?>)((dynamic)this)._dateOfHospitalization).OriginalValue; } }
        protected DateTime? DateOfHospitalization_Previous { get { return ((EditableValue<DateTime?>)((dynamic)this)._dateOfHospitalization).PreviousValue; } }
                
        [LocalizedDisplayName(_str_PersonalIDType)]
        [MapField(_str_PersonalIDType)]
        public abstract String PersonalIDType { get; set; }
        protected String PersonalIDType_Original { get { return ((EditableValue<String>)((dynamic)this)._personalIDType).OriginalValue; } }
        protected String PersonalIDType_Previous { get { return ((EditableValue<String>)((dynamic)this)._personalIDType).PreviousValue; } }
                
        [LocalizedDisplayName(_str_Hospitalization)]
        [MapField(_str_Hospitalization)]
        public abstract Int32? Hospitalization { get; set; }
        protected Int32? Hospitalization_Original { get { return ((EditableValue<Int32?>)((dynamic)this)._hospitalization).OriginalValue; } }
        protected Int32? Hospitalization_Previous { get { return ((EditableValue<Int32?>)((dynamic)this)._hospitalization).PreviousValue; } }
                
        [LocalizedDisplayName(_str_StatusOfPatientAtTimeOfNotification)]
        [MapField(_str_StatusOfPatientAtTimeOfNotification)]
        public abstract String StatusOfPatientAtTimeOfNotification { get; set; }
        protected String StatusOfPatientAtTimeOfNotification_Original { get { return ((EditableValue<String>)((dynamic)this)._statusOfPatientAtTimeOfNotification).OriginalValue; } }
        protected String StatusOfPatientAtTimeOfNotification_Previous { get { return ((EditableValue<String>)((dynamic)this)._statusOfPatientAtTimeOfNotification).PreviousValue; } }
                
        [LocalizedDisplayName(_str_idfEHealthCaseAMRequest)]
        [MapField(_str_idfEHealthCaseAMRequest)]
        public abstract Int64? idfEHealthCaseAMRequest { get; set; }
        protected Int64? idfEHealthCaseAMRequest_Original { get { return ((EditableValue<Int64?>)((dynamic)this)._idfEHealthCaseAMRequest).OriginalValue; } }
        protected Int64? idfEHealthCaseAMRequest_Previous { get { return ((EditableValue<Int64?>)((dynamic)this)._idfEHealthCaseAMRequest).PreviousValue; } }
                
        [LocalizedDisplayName(_str_idfEHealthCaseAMItem)]
        [MapField(_str_idfEHealthCaseAMItem)]
        public abstract Int64? idfEHealthCaseAMItem { get; set; }
        protected Int64? idfEHealthCaseAMItem_Original { get { return ((EditableValue<Int64?>)((dynamic)this)._idfEHealthCaseAMItem).OriginalValue; } }
        protected Int64? idfEHealthCaseAMItem_Previous { get { return ((EditableValue<Int64?>)((dynamic)this)._idfEHealthCaseAMItem).PreviousValue; } }
                
        #region Set/Get values
        #region filed_info definifion
        protected class field_info
        {
            internal string _name;
            internal string _formname;
            internal string _type;
            internal Func<EHealthCaseAM, object> _get_func;
            internal Action<EHealthCaseAM, string> _set_func;
            internal Action<EHealthCaseAM, EHealthCaseAM, CompareModel, DbManagerProxy> _compare_func;
        }
        internal const string _str_Parent = "Parent";
        internal const string _str_IsNew = "IsNew";
        
        internal const string _str_DateOfCompletionOfPaperForm = "DateOfCompletionOfPaperForm";
        internal const string _str_Diagnosis = "Diagnosis";
        internal const string _str_DiagnosisDate = "DiagnosisDate";
        internal const string _str_PersonalID = "PersonalID";
        internal const string _str_PatientsLastName = "PatientsLastName";
        internal const string _str_PatientsFirstName = "PatientsFirstName";
        internal const string _str_PatientsMiddleName = "PatientsMiddleName";
        internal const string _str_PatientsDateOfBirth = "PatientsDateOfBirth";
        internal const string _str_PatientsSex = "PatientsSex";
        internal const string _str_PatientsCitizenship = "PatientsCitizenship";
        internal const string _str_PatientsCurrentResidenceRegion = "PatientsCurrentResidenceRegion";
        internal const string _str_PatientsCurrentResidence = "PatientsCurrentResidence";
        internal const string _str_PatientsPermanentResidenceRegion = "PatientsPermanentResidenceRegion";
        internal const string _str_PatientsPermanentResidence = "PatientsPermanentResidence";
        internal const string _str_NameOfEmployer = "NameOfEmployer";
        internal const string _str_PatientsOccupation = "PatientsOccupation";
        internal const string _str_HospitalName = "HospitalName";
        internal const string _str_Outcome = "Outcome";
        internal const string _str_DateOfDeath = "DateOfDeath";
        internal const string _str_HumanCaseLocalIdentifier = "HumanCaseLocalIdentifier";
        internal const string _str_VisitIdentifier = "VisitIdentifier";
        internal const string _str_NotificationSentByFacility = "NotificationSentByFacility";
        internal const string _str_NotificationSentByFacilityName = "NotificationSentByFacilityName";
        internal const string _str_NotificationSentByFacilityPhone = "NotificationSentByFacilityPhone";
        internal const string _str_NotificationSentByFacilityRegion = "NotificationSentByFacilityRegion";
        internal const string _str_NotificationSentByFacilityAddress = "NotificationSentByFacilityAddress";
        internal const string _str_NotificationSentByName = "NotificationSentByName";
        internal const string _str_NotificationSentBySsn = "NotificationSentBySsn";
        internal const string _str_NotificationSentByMobile = "NotificationSentByMobile";
        internal const string _str_NotificationDate = "NotificationDate";
        internal const string _str_PlaceOfHospitalization = "PlaceOfHospitalization";
        internal const string _str_PlaceOfHospitalizationName = "PlaceOfHospitalizationName";
        internal const string _str_PlaceOfHospitalizationPhone = "PlaceOfHospitalizationPhone";
        internal const string _str_PlaceOfHospitalizationRegion = "PlaceOfHospitalizationRegion";
        internal const string _str_PlaceOfHospitalizationAddress = "PlaceOfHospitalizationAddress";
        internal const string _str_DateOfHospitalization = "DateOfHospitalization";
        internal const string _str_PersonalIDType = "PersonalIDType";
        internal const string _str_Hospitalization = "Hospitalization";
        internal const string _str_StatusOfPatientAtTimeOfNotification = "StatusOfPatientAtTimeOfNotification";
        internal const string _str_idfEHealthCaseAMRequest = "idfEHealthCaseAMRequest";
        internal const string _str_idfEHealthCaseAMItem = "idfEHealthCaseAMItem";
        internal const string _str_master = "master";
        private static readonly field_info[] _field_infos =
        {
                  
            new field_info {
              _name = _str_DateOfCompletionOfPaperForm, _formname = _str_DateOfCompletionOfPaperForm, _type = "DateTime?",
              _get_func = o => o.DateOfCompletionOfPaperForm,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseDateTimeNullable(val); if (o.DateOfCompletionOfPaperForm != newval) o.DateOfCompletionOfPaperForm = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.DateOfCompletionOfPaperForm != c.DateOfCompletionOfPaperForm || o.IsRIRPropChanged(_str_DateOfCompletionOfPaperForm, c)) 
                  m.Add(_str_DateOfCompletionOfPaperForm, o.ObjectIdent + _str_DateOfCompletionOfPaperForm, o.ObjectIdent2 + _str_DateOfCompletionOfPaperForm, o.ObjectIdent3 + _str_DateOfCompletionOfPaperForm, "DateTime?", 
                    o.DateOfCompletionOfPaperForm == null ? "" : o.DateOfCompletionOfPaperForm.ToString(),                  
                  o.IsReadOnly(_str_DateOfCompletionOfPaperForm), o.IsInvisible(_str_DateOfCompletionOfPaperForm), o.IsRequired(_str_DateOfCompletionOfPaperForm)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_Diagnosis, _formname = _str_Diagnosis, _type = "String",
              _get_func = o => o.Diagnosis,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.Diagnosis != newval) o.Diagnosis = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.Diagnosis != c.Diagnosis || o.IsRIRPropChanged(_str_Diagnosis, c)) 
                  m.Add(_str_Diagnosis, o.ObjectIdent + _str_Diagnosis, o.ObjectIdent2 + _str_Diagnosis, o.ObjectIdent3 + _str_Diagnosis, "String", 
                    o.Diagnosis == null ? "" : o.Diagnosis.ToString(),                  
                  o.IsReadOnly(_str_Diagnosis), o.IsInvisible(_str_Diagnosis), o.IsRequired(_str_Diagnosis)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_DiagnosisDate, _formname = _str_DiagnosisDate, _type = "DateTime?",
              _get_func = o => o.DiagnosisDate,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseDateTimeNullable(val); if (o.DiagnosisDate != newval) o.DiagnosisDate = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.DiagnosisDate != c.DiagnosisDate || o.IsRIRPropChanged(_str_DiagnosisDate, c)) 
                  m.Add(_str_DiagnosisDate, o.ObjectIdent + _str_DiagnosisDate, o.ObjectIdent2 + _str_DiagnosisDate, o.ObjectIdent3 + _str_DiagnosisDate, "DateTime?", 
                    o.DiagnosisDate == null ? "" : o.DiagnosisDate.ToString(),                  
                  o.IsReadOnly(_str_DiagnosisDate), o.IsInvisible(_str_DiagnosisDate), o.IsRequired(_str_DiagnosisDate)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_PersonalID, _formname = _str_PersonalID, _type = "String",
              _get_func = o => o.PersonalID,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.PersonalID != newval) o.PersonalID = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.PersonalID != c.PersonalID || o.IsRIRPropChanged(_str_PersonalID, c)) 
                  m.Add(_str_PersonalID, o.ObjectIdent + _str_PersonalID, o.ObjectIdent2 + _str_PersonalID, o.ObjectIdent3 + _str_PersonalID, "String", 
                    o.PersonalID == null ? "" : o.PersonalID.ToString(),                  
                  o.IsReadOnly(_str_PersonalID), o.IsInvisible(_str_PersonalID), o.IsRequired(_str_PersonalID)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_PatientsLastName, _formname = _str_PatientsLastName, _type = "String",
              _get_func = o => o.PatientsLastName,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.PatientsLastName != newval) o.PatientsLastName = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.PatientsLastName != c.PatientsLastName || o.IsRIRPropChanged(_str_PatientsLastName, c)) 
                  m.Add(_str_PatientsLastName, o.ObjectIdent + _str_PatientsLastName, o.ObjectIdent2 + _str_PatientsLastName, o.ObjectIdent3 + _str_PatientsLastName, "String", 
                    o.PatientsLastName == null ? "" : o.PatientsLastName.ToString(),                  
                  o.IsReadOnly(_str_PatientsLastName), o.IsInvisible(_str_PatientsLastName), o.IsRequired(_str_PatientsLastName)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_PatientsFirstName, _formname = _str_PatientsFirstName, _type = "String",
              _get_func = o => o.PatientsFirstName,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.PatientsFirstName != newval) o.PatientsFirstName = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.PatientsFirstName != c.PatientsFirstName || o.IsRIRPropChanged(_str_PatientsFirstName, c)) 
                  m.Add(_str_PatientsFirstName, o.ObjectIdent + _str_PatientsFirstName, o.ObjectIdent2 + _str_PatientsFirstName, o.ObjectIdent3 + _str_PatientsFirstName, "String", 
                    o.PatientsFirstName == null ? "" : o.PatientsFirstName.ToString(),                  
                  o.IsReadOnly(_str_PatientsFirstName), o.IsInvisible(_str_PatientsFirstName), o.IsRequired(_str_PatientsFirstName)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_PatientsMiddleName, _formname = _str_PatientsMiddleName, _type = "String",
              _get_func = o => o.PatientsMiddleName,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.PatientsMiddleName != newval) o.PatientsMiddleName = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.PatientsMiddleName != c.PatientsMiddleName || o.IsRIRPropChanged(_str_PatientsMiddleName, c)) 
                  m.Add(_str_PatientsMiddleName, o.ObjectIdent + _str_PatientsMiddleName, o.ObjectIdent2 + _str_PatientsMiddleName, o.ObjectIdent3 + _str_PatientsMiddleName, "String", 
                    o.PatientsMiddleName == null ? "" : o.PatientsMiddleName.ToString(),                  
                  o.IsReadOnly(_str_PatientsMiddleName), o.IsInvisible(_str_PatientsMiddleName), o.IsRequired(_str_PatientsMiddleName)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_PatientsDateOfBirth, _formname = _str_PatientsDateOfBirth, _type = "DateTime?",
              _get_func = o => o.PatientsDateOfBirth,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseDateTimeNullable(val); if (o.PatientsDateOfBirth != newval) o.PatientsDateOfBirth = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.PatientsDateOfBirth != c.PatientsDateOfBirth || o.IsRIRPropChanged(_str_PatientsDateOfBirth, c)) 
                  m.Add(_str_PatientsDateOfBirth, o.ObjectIdent + _str_PatientsDateOfBirth, o.ObjectIdent2 + _str_PatientsDateOfBirth, o.ObjectIdent3 + _str_PatientsDateOfBirth, "DateTime?", 
                    o.PatientsDateOfBirth == null ? "" : o.PatientsDateOfBirth.ToString(),                  
                  o.IsReadOnly(_str_PatientsDateOfBirth), o.IsInvisible(_str_PatientsDateOfBirth), o.IsRequired(_str_PatientsDateOfBirth)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_PatientsSex, _formname = _str_PatientsSex, _type = "String",
              _get_func = o => o.PatientsSex,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.PatientsSex != newval) o.PatientsSex = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.PatientsSex != c.PatientsSex || o.IsRIRPropChanged(_str_PatientsSex, c)) 
                  m.Add(_str_PatientsSex, o.ObjectIdent + _str_PatientsSex, o.ObjectIdent2 + _str_PatientsSex, o.ObjectIdent3 + _str_PatientsSex, "String", 
                    o.PatientsSex == null ? "" : o.PatientsSex.ToString(),                  
                  o.IsReadOnly(_str_PatientsSex), o.IsInvisible(_str_PatientsSex), o.IsRequired(_str_PatientsSex)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_PatientsCitizenship, _formname = _str_PatientsCitizenship, _type = "String",
              _get_func = o => o.PatientsCitizenship,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.PatientsCitizenship != newval) o.PatientsCitizenship = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.PatientsCitizenship != c.PatientsCitizenship || o.IsRIRPropChanged(_str_PatientsCitizenship, c)) 
                  m.Add(_str_PatientsCitizenship, o.ObjectIdent + _str_PatientsCitizenship, o.ObjectIdent2 + _str_PatientsCitizenship, o.ObjectIdent3 + _str_PatientsCitizenship, "String", 
                    o.PatientsCitizenship == null ? "" : o.PatientsCitizenship.ToString(),                  
                  o.IsReadOnly(_str_PatientsCitizenship), o.IsInvisible(_str_PatientsCitizenship), o.IsRequired(_str_PatientsCitizenship)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_PatientsCurrentResidenceRegion, _formname = _str_PatientsCurrentResidenceRegion, _type = "Int32?",
              _get_func = o => o.PatientsCurrentResidenceRegion,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseInt32Nullable(val); if (o.PatientsCurrentResidenceRegion != newval) o.PatientsCurrentResidenceRegion = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.PatientsCurrentResidenceRegion != c.PatientsCurrentResidenceRegion || o.IsRIRPropChanged(_str_PatientsCurrentResidenceRegion, c)) 
                  m.Add(_str_PatientsCurrentResidenceRegion, o.ObjectIdent + _str_PatientsCurrentResidenceRegion, o.ObjectIdent2 + _str_PatientsCurrentResidenceRegion, o.ObjectIdent3 + _str_PatientsCurrentResidenceRegion, "Int32?", 
                    o.PatientsCurrentResidenceRegion == null ? "" : o.PatientsCurrentResidenceRegion.ToString(),                  
                  o.IsReadOnly(_str_PatientsCurrentResidenceRegion), o.IsInvisible(_str_PatientsCurrentResidenceRegion), o.IsRequired(_str_PatientsCurrentResidenceRegion)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_PatientsCurrentResidence, _formname = _str_PatientsCurrentResidence, _type = "String",
              _get_func = o => o.PatientsCurrentResidence,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.PatientsCurrentResidence != newval) o.PatientsCurrentResidence = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.PatientsCurrentResidence != c.PatientsCurrentResidence || o.IsRIRPropChanged(_str_PatientsCurrentResidence, c)) 
                  m.Add(_str_PatientsCurrentResidence, o.ObjectIdent + _str_PatientsCurrentResidence, o.ObjectIdent2 + _str_PatientsCurrentResidence, o.ObjectIdent3 + _str_PatientsCurrentResidence, "String", 
                    o.PatientsCurrentResidence == null ? "" : o.PatientsCurrentResidence.ToString(),                  
                  o.IsReadOnly(_str_PatientsCurrentResidence), o.IsInvisible(_str_PatientsCurrentResidence), o.IsRequired(_str_PatientsCurrentResidence)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_PatientsPermanentResidenceRegion, _formname = _str_PatientsPermanentResidenceRegion, _type = "Int32?",
              _get_func = o => o.PatientsPermanentResidenceRegion,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseInt32Nullable(val); if (o.PatientsPermanentResidenceRegion != newval) o.PatientsPermanentResidenceRegion = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.PatientsPermanentResidenceRegion != c.PatientsPermanentResidenceRegion || o.IsRIRPropChanged(_str_PatientsPermanentResidenceRegion, c)) 
                  m.Add(_str_PatientsPermanentResidenceRegion, o.ObjectIdent + _str_PatientsPermanentResidenceRegion, o.ObjectIdent2 + _str_PatientsPermanentResidenceRegion, o.ObjectIdent3 + _str_PatientsPermanentResidenceRegion, "Int32?", 
                    o.PatientsPermanentResidenceRegion == null ? "" : o.PatientsPermanentResidenceRegion.ToString(),                  
                  o.IsReadOnly(_str_PatientsPermanentResidenceRegion), o.IsInvisible(_str_PatientsPermanentResidenceRegion), o.IsRequired(_str_PatientsPermanentResidenceRegion)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_PatientsPermanentResidence, _formname = _str_PatientsPermanentResidence, _type = "String",
              _get_func = o => o.PatientsPermanentResidence,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.PatientsPermanentResidence != newval) o.PatientsPermanentResidence = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.PatientsPermanentResidence != c.PatientsPermanentResidence || o.IsRIRPropChanged(_str_PatientsPermanentResidence, c)) 
                  m.Add(_str_PatientsPermanentResidence, o.ObjectIdent + _str_PatientsPermanentResidence, o.ObjectIdent2 + _str_PatientsPermanentResidence, o.ObjectIdent3 + _str_PatientsPermanentResidence, "String", 
                    o.PatientsPermanentResidence == null ? "" : o.PatientsPermanentResidence.ToString(),                  
                  o.IsReadOnly(_str_PatientsPermanentResidence), o.IsInvisible(_str_PatientsPermanentResidence), o.IsRequired(_str_PatientsPermanentResidence)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_NameOfEmployer, _formname = _str_NameOfEmployer, _type = "String",
              _get_func = o => o.NameOfEmployer,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.NameOfEmployer != newval) o.NameOfEmployer = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.NameOfEmployer != c.NameOfEmployer || o.IsRIRPropChanged(_str_NameOfEmployer, c)) 
                  m.Add(_str_NameOfEmployer, o.ObjectIdent + _str_NameOfEmployer, o.ObjectIdent2 + _str_NameOfEmployer, o.ObjectIdent3 + _str_NameOfEmployer, "String", 
                    o.NameOfEmployer == null ? "" : o.NameOfEmployer.ToString(),                  
                  o.IsReadOnly(_str_NameOfEmployer), o.IsInvisible(_str_NameOfEmployer), o.IsRequired(_str_NameOfEmployer)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_PatientsOccupation, _formname = _str_PatientsOccupation, _type = "String",
              _get_func = o => o.PatientsOccupation,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.PatientsOccupation != newval) o.PatientsOccupation = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.PatientsOccupation != c.PatientsOccupation || o.IsRIRPropChanged(_str_PatientsOccupation, c)) 
                  m.Add(_str_PatientsOccupation, o.ObjectIdent + _str_PatientsOccupation, o.ObjectIdent2 + _str_PatientsOccupation, o.ObjectIdent3 + _str_PatientsOccupation, "String", 
                    o.PatientsOccupation == null ? "" : o.PatientsOccupation.ToString(),                  
                  o.IsReadOnly(_str_PatientsOccupation), o.IsInvisible(_str_PatientsOccupation), o.IsRequired(_str_PatientsOccupation)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_HospitalName, _formname = _str_HospitalName, _type = "String",
              _get_func = o => o.HospitalName,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.HospitalName != newval) o.HospitalName = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.HospitalName != c.HospitalName || o.IsRIRPropChanged(_str_HospitalName, c)) 
                  m.Add(_str_HospitalName, o.ObjectIdent + _str_HospitalName, o.ObjectIdent2 + _str_HospitalName, o.ObjectIdent3 + _str_HospitalName, "String", 
                    o.HospitalName == null ? "" : o.HospitalName.ToString(),                  
                  o.IsReadOnly(_str_HospitalName), o.IsInvisible(_str_HospitalName), o.IsRequired(_str_HospitalName)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_Outcome, _formname = _str_Outcome, _type = "String",
              _get_func = o => o.Outcome,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.Outcome != newval) o.Outcome = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.Outcome != c.Outcome || o.IsRIRPropChanged(_str_Outcome, c)) 
                  m.Add(_str_Outcome, o.ObjectIdent + _str_Outcome, o.ObjectIdent2 + _str_Outcome, o.ObjectIdent3 + _str_Outcome, "String", 
                    o.Outcome == null ? "" : o.Outcome.ToString(),                  
                  o.IsReadOnly(_str_Outcome), o.IsInvisible(_str_Outcome), o.IsRequired(_str_Outcome)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_DateOfDeath, _formname = _str_DateOfDeath, _type = "DateTime?",
              _get_func = o => o.DateOfDeath,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseDateTimeNullable(val); if (o.DateOfDeath != newval) o.DateOfDeath = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.DateOfDeath != c.DateOfDeath || o.IsRIRPropChanged(_str_DateOfDeath, c)) 
                  m.Add(_str_DateOfDeath, o.ObjectIdent + _str_DateOfDeath, o.ObjectIdent2 + _str_DateOfDeath, o.ObjectIdent3 + _str_DateOfDeath, "DateTime?", 
                    o.DateOfDeath == null ? "" : o.DateOfDeath.ToString(),                  
                  o.IsReadOnly(_str_DateOfDeath), o.IsInvisible(_str_DateOfDeath), o.IsRequired(_str_DateOfDeath)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_HumanCaseLocalIdentifier, _formname = _str_HumanCaseLocalIdentifier, _type = "String",
              _get_func = o => o.HumanCaseLocalIdentifier,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.HumanCaseLocalIdentifier != newval) o.HumanCaseLocalIdentifier = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.HumanCaseLocalIdentifier != c.HumanCaseLocalIdentifier || o.IsRIRPropChanged(_str_HumanCaseLocalIdentifier, c)) 
                  m.Add(_str_HumanCaseLocalIdentifier, o.ObjectIdent + _str_HumanCaseLocalIdentifier, o.ObjectIdent2 + _str_HumanCaseLocalIdentifier, o.ObjectIdent3 + _str_HumanCaseLocalIdentifier, "String", 
                    o.HumanCaseLocalIdentifier == null ? "" : o.HumanCaseLocalIdentifier.ToString(),                  
                  o.IsReadOnly(_str_HumanCaseLocalIdentifier), o.IsInvisible(_str_HumanCaseLocalIdentifier), o.IsRequired(_str_HumanCaseLocalIdentifier)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_VisitIdentifier, _formname = _str_VisitIdentifier, _type = "String",
              _get_func = o => o.VisitIdentifier,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.VisitIdentifier != newval) o.VisitIdentifier = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.VisitIdentifier != c.VisitIdentifier || o.IsRIRPropChanged(_str_VisitIdentifier, c)) 
                  m.Add(_str_VisitIdentifier, o.ObjectIdent + _str_VisitIdentifier, o.ObjectIdent2 + _str_VisitIdentifier, o.ObjectIdent3 + _str_VisitIdentifier, "String", 
                    o.VisitIdentifier == null ? "" : o.VisitIdentifier.ToString(),                  
                  o.IsReadOnly(_str_VisitIdentifier), o.IsInvisible(_str_VisitIdentifier), o.IsRequired(_str_VisitIdentifier)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_NotificationSentByFacility, _formname = _str_NotificationSentByFacility, _type = "String",
              _get_func = o => o.NotificationSentByFacility,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.NotificationSentByFacility != newval) o.NotificationSentByFacility = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.NotificationSentByFacility != c.NotificationSentByFacility || o.IsRIRPropChanged(_str_NotificationSentByFacility, c)) 
                  m.Add(_str_NotificationSentByFacility, o.ObjectIdent + _str_NotificationSentByFacility, o.ObjectIdent2 + _str_NotificationSentByFacility, o.ObjectIdent3 + _str_NotificationSentByFacility, "String", 
                    o.NotificationSentByFacility == null ? "" : o.NotificationSentByFacility.ToString(),                  
                  o.IsReadOnly(_str_NotificationSentByFacility), o.IsInvisible(_str_NotificationSentByFacility), o.IsRequired(_str_NotificationSentByFacility)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_NotificationSentByFacilityName, _formname = _str_NotificationSentByFacilityName, _type = "String",
              _get_func = o => o.NotificationSentByFacilityName,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.NotificationSentByFacilityName != newval) o.NotificationSentByFacilityName = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.NotificationSentByFacilityName != c.NotificationSentByFacilityName || o.IsRIRPropChanged(_str_NotificationSentByFacilityName, c)) 
                  m.Add(_str_NotificationSentByFacilityName, o.ObjectIdent + _str_NotificationSentByFacilityName, o.ObjectIdent2 + _str_NotificationSentByFacilityName, o.ObjectIdent3 + _str_NotificationSentByFacilityName, "String", 
                    o.NotificationSentByFacilityName == null ? "" : o.NotificationSentByFacilityName.ToString(),                  
                  o.IsReadOnly(_str_NotificationSentByFacilityName), o.IsInvisible(_str_NotificationSentByFacilityName), o.IsRequired(_str_NotificationSentByFacilityName)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_NotificationSentByFacilityPhone, _formname = _str_NotificationSentByFacilityPhone, _type = "String",
              _get_func = o => o.NotificationSentByFacilityPhone,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.NotificationSentByFacilityPhone != newval) o.NotificationSentByFacilityPhone = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.NotificationSentByFacilityPhone != c.NotificationSentByFacilityPhone || o.IsRIRPropChanged(_str_NotificationSentByFacilityPhone, c)) 
                  m.Add(_str_NotificationSentByFacilityPhone, o.ObjectIdent + _str_NotificationSentByFacilityPhone, o.ObjectIdent2 + _str_NotificationSentByFacilityPhone, o.ObjectIdent3 + _str_NotificationSentByFacilityPhone, "String", 
                    o.NotificationSentByFacilityPhone == null ? "" : o.NotificationSentByFacilityPhone.ToString(),                  
                  o.IsReadOnly(_str_NotificationSentByFacilityPhone), o.IsInvisible(_str_NotificationSentByFacilityPhone), o.IsRequired(_str_NotificationSentByFacilityPhone)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_NotificationSentByFacilityRegion, _formname = _str_NotificationSentByFacilityRegion, _type = "Int32?",
              _get_func = o => o.NotificationSentByFacilityRegion,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseInt32Nullable(val); if (o.NotificationSentByFacilityRegion != newval) o.NotificationSentByFacilityRegion = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.NotificationSentByFacilityRegion != c.NotificationSentByFacilityRegion || o.IsRIRPropChanged(_str_NotificationSentByFacilityRegion, c)) 
                  m.Add(_str_NotificationSentByFacilityRegion, o.ObjectIdent + _str_NotificationSentByFacilityRegion, o.ObjectIdent2 + _str_NotificationSentByFacilityRegion, o.ObjectIdent3 + _str_NotificationSentByFacilityRegion, "Int32?", 
                    o.NotificationSentByFacilityRegion == null ? "" : o.NotificationSentByFacilityRegion.ToString(),                  
                  o.IsReadOnly(_str_NotificationSentByFacilityRegion), o.IsInvisible(_str_NotificationSentByFacilityRegion), o.IsRequired(_str_NotificationSentByFacilityRegion)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_NotificationSentByFacilityAddress, _formname = _str_NotificationSentByFacilityAddress, _type = "String",
              _get_func = o => o.NotificationSentByFacilityAddress,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.NotificationSentByFacilityAddress != newval) o.NotificationSentByFacilityAddress = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.NotificationSentByFacilityAddress != c.NotificationSentByFacilityAddress || o.IsRIRPropChanged(_str_NotificationSentByFacilityAddress, c)) 
                  m.Add(_str_NotificationSentByFacilityAddress, o.ObjectIdent + _str_NotificationSentByFacilityAddress, o.ObjectIdent2 + _str_NotificationSentByFacilityAddress, o.ObjectIdent3 + _str_NotificationSentByFacilityAddress, "String", 
                    o.NotificationSentByFacilityAddress == null ? "" : o.NotificationSentByFacilityAddress.ToString(),                  
                  o.IsReadOnly(_str_NotificationSentByFacilityAddress), o.IsInvisible(_str_NotificationSentByFacilityAddress), o.IsRequired(_str_NotificationSentByFacilityAddress)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_NotificationSentByName, _formname = _str_NotificationSentByName, _type = "String",
              _get_func = o => o.NotificationSentByName,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.NotificationSentByName != newval) o.NotificationSentByName = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.NotificationSentByName != c.NotificationSentByName || o.IsRIRPropChanged(_str_NotificationSentByName, c)) 
                  m.Add(_str_NotificationSentByName, o.ObjectIdent + _str_NotificationSentByName, o.ObjectIdent2 + _str_NotificationSentByName, o.ObjectIdent3 + _str_NotificationSentByName, "String", 
                    o.NotificationSentByName == null ? "" : o.NotificationSentByName.ToString(),                  
                  o.IsReadOnly(_str_NotificationSentByName), o.IsInvisible(_str_NotificationSentByName), o.IsRequired(_str_NotificationSentByName)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_NotificationSentBySsn, _formname = _str_NotificationSentBySsn, _type = "String",
              _get_func = o => o.NotificationSentBySsn,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.NotificationSentBySsn != newval) o.NotificationSentBySsn = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.NotificationSentBySsn != c.NotificationSentBySsn || o.IsRIRPropChanged(_str_NotificationSentBySsn, c)) 
                  m.Add(_str_NotificationSentBySsn, o.ObjectIdent + _str_NotificationSentBySsn, o.ObjectIdent2 + _str_NotificationSentBySsn, o.ObjectIdent3 + _str_NotificationSentBySsn, "String", 
                    o.NotificationSentBySsn == null ? "" : o.NotificationSentBySsn.ToString(),                  
                  o.IsReadOnly(_str_NotificationSentBySsn), o.IsInvisible(_str_NotificationSentBySsn), o.IsRequired(_str_NotificationSentBySsn)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_NotificationSentByMobile, _formname = _str_NotificationSentByMobile, _type = "String",
              _get_func = o => o.NotificationSentByMobile,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.NotificationSentByMobile != newval) o.NotificationSentByMobile = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.NotificationSentByMobile != c.NotificationSentByMobile || o.IsRIRPropChanged(_str_NotificationSentByMobile, c)) 
                  m.Add(_str_NotificationSentByMobile, o.ObjectIdent + _str_NotificationSentByMobile, o.ObjectIdent2 + _str_NotificationSentByMobile, o.ObjectIdent3 + _str_NotificationSentByMobile, "String", 
                    o.NotificationSentByMobile == null ? "" : o.NotificationSentByMobile.ToString(),                  
                  o.IsReadOnly(_str_NotificationSentByMobile), o.IsInvisible(_str_NotificationSentByMobile), o.IsRequired(_str_NotificationSentByMobile)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_NotificationDate, _formname = _str_NotificationDate, _type = "DateTime?",
              _get_func = o => o.NotificationDate,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseDateTimeNullable(val); if (o.NotificationDate != newval) o.NotificationDate = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.NotificationDate != c.NotificationDate || o.IsRIRPropChanged(_str_NotificationDate, c)) 
                  m.Add(_str_NotificationDate, o.ObjectIdent + _str_NotificationDate, o.ObjectIdent2 + _str_NotificationDate, o.ObjectIdent3 + _str_NotificationDate, "DateTime?", 
                    o.NotificationDate == null ? "" : o.NotificationDate.ToString(),                  
                  o.IsReadOnly(_str_NotificationDate), o.IsInvisible(_str_NotificationDate), o.IsRequired(_str_NotificationDate)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_PlaceOfHospitalization, _formname = _str_PlaceOfHospitalization, _type = "String",
              _get_func = o => o.PlaceOfHospitalization,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.PlaceOfHospitalization != newval) o.PlaceOfHospitalization = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.PlaceOfHospitalization != c.PlaceOfHospitalization || o.IsRIRPropChanged(_str_PlaceOfHospitalization, c)) 
                  m.Add(_str_PlaceOfHospitalization, o.ObjectIdent + _str_PlaceOfHospitalization, o.ObjectIdent2 + _str_PlaceOfHospitalization, o.ObjectIdent3 + _str_PlaceOfHospitalization, "String", 
                    o.PlaceOfHospitalization == null ? "" : o.PlaceOfHospitalization.ToString(),                  
                  o.IsReadOnly(_str_PlaceOfHospitalization), o.IsInvisible(_str_PlaceOfHospitalization), o.IsRequired(_str_PlaceOfHospitalization)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_PlaceOfHospitalizationName, _formname = _str_PlaceOfHospitalizationName, _type = "String",
              _get_func = o => o.PlaceOfHospitalizationName,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.PlaceOfHospitalizationName != newval) o.PlaceOfHospitalizationName = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.PlaceOfHospitalizationName != c.PlaceOfHospitalizationName || o.IsRIRPropChanged(_str_PlaceOfHospitalizationName, c)) 
                  m.Add(_str_PlaceOfHospitalizationName, o.ObjectIdent + _str_PlaceOfHospitalizationName, o.ObjectIdent2 + _str_PlaceOfHospitalizationName, o.ObjectIdent3 + _str_PlaceOfHospitalizationName, "String", 
                    o.PlaceOfHospitalizationName == null ? "" : o.PlaceOfHospitalizationName.ToString(),                  
                  o.IsReadOnly(_str_PlaceOfHospitalizationName), o.IsInvisible(_str_PlaceOfHospitalizationName), o.IsRequired(_str_PlaceOfHospitalizationName)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_PlaceOfHospitalizationPhone, _formname = _str_PlaceOfHospitalizationPhone, _type = "String",
              _get_func = o => o.PlaceOfHospitalizationPhone,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.PlaceOfHospitalizationPhone != newval) o.PlaceOfHospitalizationPhone = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.PlaceOfHospitalizationPhone != c.PlaceOfHospitalizationPhone || o.IsRIRPropChanged(_str_PlaceOfHospitalizationPhone, c)) 
                  m.Add(_str_PlaceOfHospitalizationPhone, o.ObjectIdent + _str_PlaceOfHospitalizationPhone, o.ObjectIdent2 + _str_PlaceOfHospitalizationPhone, o.ObjectIdent3 + _str_PlaceOfHospitalizationPhone, "String", 
                    o.PlaceOfHospitalizationPhone == null ? "" : o.PlaceOfHospitalizationPhone.ToString(),                  
                  o.IsReadOnly(_str_PlaceOfHospitalizationPhone), o.IsInvisible(_str_PlaceOfHospitalizationPhone), o.IsRequired(_str_PlaceOfHospitalizationPhone)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_PlaceOfHospitalizationRegion, _formname = _str_PlaceOfHospitalizationRegion, _type = "Int32?",
              _get_func = o => o.PlaceOfHospitalizationRegion,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseInt32Nullable(val); if (o.PlaceOfHospitalizationRegion != newval) o.PlaceOfHospitalizationRegion = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.PlaceOfHospitalizationRegion != c.PlaceOfHospitalizationRegion || o.IsRIRPropChanged(_str_PlaceOfHospitalizationRegion, c)) 
                  m.Add(_str_PlaceOfHospitalizationRegion, o.ObjectIdent + _str_PlaceOfHospitalizationRegion, o.ObjectIdent2 + _str_PlaceOfHospitalizationRegion, o.ObjectIdent3 + _str_PlaceOfHospitalizationRegion, "Int32?", 
                    o.PlaceOfHospitalizationRegion == null ? "" : o.PlaceOfHospitalizationRegion.ToString(),                  
                  o.IsReadOnly(_str_PlaceOfHospitalizationRegion), o.IsInvisible(_str_PlaceOfHospitalizationRegion), o.IsRequired(_str_PlaceOfHospitalizationRegion)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_PlaceOfHospitalizationAddress, _formname = _str_PlaceOfHospitalizationAddress, _type = "String",
              _get_func = o => o.PlaceOfHospitalizationAddress,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.PlaceOfHospitalizationAddress != newval) o.PlaceOfHospitalizationAddress = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.PlaceOfHospitalizationAddress != c.PlaceOfHospitalizationAddress || o.IsRIRPropChanged(_str_PlaceOfHospitalizationAddress, c)) 
                  m.Add(_str_PlaceOfHospitalizationAddress, o.ObjectIdent + _str_PlaceOfHospitalizationAddress, o.ObjectIdent2 + _str_PlaceOfHospitalizationAddress, o.ObjectIdent3 + _str_PlaceOfHospitalizationAddress, "String", 
                    o.PlaceOfHospitalizationAddress == null ? "" : o.PlaceOfHospitalizationAddress.ToString(),                  
                  o.IsReadOnly(_str_PlaceOfHospitalizationAddress), o.IsInvisible(_str_PlaceOfHospitalizationAddress), o.IsRequired(_str_PlaceOfHospitalizationAddress)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_DateOfHospitalization, _formname = _str_DateOfHospitalization, _type = "DateTime?",
              _get_func = o => o.DateOfHospitalization,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseDateTimeNullable(val); if (o.DateOfHospitalization != newval) o.DateOfHospitalization = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.DateOfHospitalization != c.DateOfHospitalization || o.IsRIRPropChanged(_str_DateOfHospitalization, c)) 
                  m.Add(_str_DateOfHospitalization, o.ObjectIdent + _str_DateOfHospitalization, o.ObjectIdent2 + _str_DateOfHospitalization, o.ObjectIdent3 + _str_DateOfHospitalization, "DateTime?", 
                    o.DateOfHospitalization == null ? "" : o.DateOfHospitalization.ToString(),                  
                  o.IsReadOnly(_str_DateOfHospitalization), o.IsInvisible(_str_DateOfHospitalization), o.IsRequired(_str_DateOfHospitalization)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_PersonalIDType, _formname = _str_PersonalIDType, _type = "String",
              _get_func = o => o.PersonalIDType,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.PersonalIDType != newval) o.PersonalIDType = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.PersonalIDType != c.PersonalIDType || o.IsRIRPropChanged(_str_PersonalIDType, c)) 
                  m.Add(_str_PersonalIDType, o.ObjectIdent + _str_PersonalIDType, o.ObjectIdent2 + _str_PersonalIDType, o.ObjectIdent3 + _str_PersonalIDType, "String", 
                    o.PersonalIDType == null ? "" : o.PersonalIDType.ToString(),                  
                  o.IsReadOnly(_str_PersonalIDType), o.IsInvisible(_str_PersonalIDType), o.IsRequired(_str_PersonalIDType)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_Hospitalization, _formname = _str_Hospitalization, _type = "Int32?",
              _get_func = o => o.Hospitalization,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseInt32Nullable(val); if (o.Hospitalization != newval) o.Hospitalization = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.Hospitalization != c.Hospitalization || o.IsRIRPropChanged(_str_Hospitalization, c)) 
                  m.Add(_str_Hospitalization, o.ObjectIdent + _str_Hospitalization, o.ObjectIdent2 + _str_Hospitalization, o.ObjectIdent3 + _str_Hospitalization, "Int32?", 
                    o.Hospitalization == null ? "" : o.Hospitalization.ToString(),                  
                  o.IsReadOnly(_str_Hospitalization), o.IsInvisible(_str_Hospitalization), o.IsRequired(_str_Hospitalization)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_StatusOfPatientAtTimeOfNotification, _formname = _str_StatusOfPatientAtTimeOfNotification, _type = "String",
              _get_func = o => o.StatusOfPatientAtTimeOfNotification,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.StatusOfPatientAtTimeOfNotification != newval) o.StatusOfPatientAtTimeOfNotification = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.StatusOfPatientAtTimeOfNotification != c.StatusOfPatientAtTimeOfNotification || o.IsRIRPropChanged(_str_StatusOfPatientAtTimeOfNotification, c)) 
                  m.Add(_str_StatusOfPatientAtTimeOfNotification, o.ObjectIdent + _str_StatusOfPatientAtTimeOfNotification, o.ObjectIdent2 + _str_StatusOfPatientAtTimeOfNotification, o.ObjectIdent3 + _str_StatusOfPatientAtTimeOfNotification, "String", 
                    o.StatusOfPatientAtTimeOfNotification == null ? "" : o.StatusOfPatientAtTimeOfNotification.ToString(),                  
                  o.IsReadOnly(_str_StatusOfPatientAtTimeOfNotification), o.IsInvisible(_str_StatusOfPatientAtTimeOfNotification), o.IsRequired(_str_StatusOfPatientAtTimeOfNotification)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_idfEHealthCaseAMRequest, _formname = _str_idfEHealthCaseAMRequest, _type = "Int64?",
              _get_func = o => o.idfEHealthCaseAMRequest,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseInt64Nullable(val); if (o.idfEHealthCaseAMRequest != newval) o.idfEHealthCaseAMRequest = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.idfEHealthCaseAMRequest != c.idfEHealthCaseAMRequest || o.IsRIRPropChanged(_str_idfEHealthCaseAMRequest, c)) 
                  m.Add(_str_idfEHealthCaseAMRequest, o.ObjectIdent + _str_idfEHealthCaseAMRequest, o.ObjectIdent2 + _str_idfEHealthCaseAMRequest, o.ObjectIdent3 + _str_idfEHealthCaseAMRequest, "Int64?", 
                    o.idfEHealthCaseAMRequest == null ? "" : o.idfEHealthCaseAMRequest.ToString(),                  
                  o.IsReadOnly(_str_idfEHealthCaseAMRequest), o.IsInvisible(_str_idfEHealthCaseAMRequest), o.IsRequired(_str_idfEHealthCaseAMRequest)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_idfEHealthCaseAMItem, _formname = _str_idfEHealthCaseAMItem, _type = "Int64?",
              _get_func = o => o.idfEHealthCaseAMItem,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseInt64Nullable(val); if (o.idfEHealthCaseAMItem != newval) o.idfEHealthCaseAMItem = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.idfEHealthCaseAMItem != c.idfEHealthCaseAMItem || o.IsRIRPropChanged(_str_idfEHealthCaseAMItem, c)) 
                  m.Add(_str_idfEHealthCaseAMItem, o.ObjectIdent + _str_idfEHealthCaseAMItem, o.ObjectIdent2 + _str_idfEHealthCaseAMItem, o.ObjectIdent3 + _str_idfEHealthCaseAMItem, "Int64?", 
                    o.idfEHealthCaseAMItem == null ? "" : o.idfEHealthCaseAMItem.ToString(),                  
                  o.IsReadOnly(_str_idfEHealthCaseAMItem), o.IsInvisible(_str_idfEHealthCaseAMItem), o.IsRequired(_str_idfEHealthCaseAMItem)); 
                  }
              }, 
        
            new field_info {
              _name = _str_master, _formname = _str_master, _type = "EHealthCaseAMRequest",
              _get_func = o => o.master,
              _set_func = (o, val) => {  },
              _compare_func = (o, c, m, g) => { 
              
                }
              }, 
        
            new field_info()
        };
        #endregion
        
        private string _getType(string name)
        {
            var i = _field_infos.FirstOrDefault(n => n._name == name);
            return i == null ? "" : i._type;
        }
        private object _getValue(string name)
        {
            var i = _field_infos.FirstOrDefault(n => n._name == name);
            return i == null ? null : i._get_func(this);
        }
        private void _setValue(string name, string val)
        {
            var i = _field_infos.FirstOrDefault(n => n._name == name);
            if (i != null) i._set_func(this, val);
        }
        internal CompareModel _compare(IObject o, CompareModel ret)
        {
            if (ret == null) ret = new CompareModel();
            if (o == null) return ret;
            EHealthCaseAM obj = (EHealthCaseAM)o;
            using (DbManagerProxy manager = DbManagerFactory.Factory.Create(ModelUserContext.Instance))
            {
                Accessor.Instance(null)._LoadLookups(manager, obj);
                foreach (var i in _field_infos)
                    if (i != null && i._compare_func != null) i._compare_func(this, obj, ret, manager);
            }
            return ret;
        }
        #endregion
    
        private BvSelectList _getList(string name)
        {
        
            return null;
        }
    
          [XmlIgnore]
          [LocalizedDisplayName(_str_master)]
        public EHealthCaseAMRequest master
        {
            get { return new Func<EHealthCaseAM, EHealthCaseAMRequest>(c => c.Parent as EHealthCaseAMRequest)(this); }
            
        }
        
        protected CacheScope m_CS;
        protected Accessor _getAccessor() { return Accessor.Instance(m_CS); }
        private IObjectPermissions m_permissions = null;
        internal IObjectPermissions _permissions { get { return m_permissions; } set { m_permissions = value; } }
        internal string m_ObjectName = "EHealthCaseAM";

        #region Parent and Clone supporting
        [XmlIgnore]
        public IObject Parent
        {
            get { return m_Parent; }
            set { m_Parent = value; /*OnPropertyChanged(_str_Parent);*/ }
        }
        private IObject m_Parent;
        internal void _setParent()
        {
        
        }
        partial void Cloned();
        partial void ClonedWithSetup();
        private bool bIsClone;
        public override object Clone()
        {
            var ret = base.Clone() as EHealthCaseAM;
            ret.bIsClone = true;
            ret.Cloned();
            ret.m_Parent = this.Parent;
            ret.m_IsMarkedToDelete = this.m_IsMarkedToDelete;
            ret.m_IsForcedToDelete = this.m_IsForcedToDelete;
            ret._setParent();
            if (this.IsDirty && !ret.IsDirty)
                ret.SetChange();
            else if (!this.IsDirty && ret.IsDirty)
                ret.RejectChanges();
            return ret;
        }
        public IObject CloneWithSetup(DbManagerProxy manager, bool bRestricted = false)
        {
            var ret = base.Clone() as EHealthCaseAM;
            ret.m_Parent = this.Parent;
            ret.m_IsMarkedToDelete = this.m_IsMarkedToDelete;
            ret.m_IsForcedToDelete = this.m_IsForcedToDelete;
            ret.m_IsNew = this.IsNew;
            ret.m_ObjectName = this.m_ObjectName;
        
            Accessor.Instance(null)._SetupLoad(manager, ret, true);
            ret.ClonedWithSetup();
            ret.DeepAcceptChanges();
            ret._setParent();
            ret._permissions = _permissions;
            return ret;
        }
        public EHealthCaseAM CloneWithSetup()
        {
            using (DbManagerProxy manager = DbManagerFactory.Factory.Create(ModelUserContext.Instance))
            {
                return CloneWithSetup(manager) as EHealthCaseAM;
            }
        }
        #endregion

        #region IObject implementation
        public object Key { get { return DateOfCompletionOfPaperForm; } }
        public string KeyName { get { return "DateOfCompletionOfPaperForm"; } }
        public object KeyLookup { get { return DateOfCompletionOfPaperForm; } }
        public string ToStringProp { get { return ToString(); } }
        private bool m_IsNew;
        public bool IsNew { get { return m_IsNew; } }
        [XmlIgnore]
        [LocalizedDisplayName("HasChanges")]
        public bool HasChanges 
        { 
            get 
            { 
                return IsDirty
        
                ;
            }
        }
        public new void RejectChanges()
        {
        
            base.RejectChanges();
        
        }
        public void DeepRejectChanges()
        {
            RejectChanges();
        
        }
        public void DeepAcceptChanges()
        { 
            AcceptChanges();
        
        }
        private bool m_bForceDirty;
        public override void AcceptChanges()
        {
            m_bForceDirty = false;
            base.AcceptChanges();
        }
        [XmlIgnore]
        [LocalizedDisplayName("IsDirty")]
        public override bool IsDirty
        {
            get { return m_bForceDirty || base.IsDirty; }
        }
        public void SetChange()
        { 
            m_bForceDirty = true;
        }
        public void DeepSetChange()
        { 
            SetChange();
        
        }
        public bool MarkToDelete() { return _Delete(false); }
        public string ObjectName { get { return m_ObjectName; } }
        public string ObjectIdent { get { return ObjectName + "_" + Key.ToString() + "_"; } }
        
        public string ObjectIdent2 { get { return ObjectIdent; } }
          
        public string ObjectIdent3 { get { return ObjectIdent; } }
          
        public IObjectAccessor GetAccessor() { return _getAccessor(); }
        public IObjectPermissions GetPermissions() { return _permissions; }
        private IObjectEnvironment _environment;
        public IObjectEnvironment Environment { get { return _environment; } set { _environment = value; } }
        public bool IsValidObject { get { return _isValid; } }
        public bool ReadOnly { get { return _readOnly || !_isValid; } set { _readOnly = value; } }
        public bool IsReadOnly(string name) { return _isReadOnly(name); }
        public bool IsInvisible(string name) { return _isInvisible(name); }
        public bool IsRequired(string name) { return _isRequired(m_isRequired, name); }
        public bool IsHiddenPersonalData(string name) { return _isHiddenPersonalData(name); }
        public string GetType(string name) { return _getType(name); }
        public object GetValue(string name) { return _getValue(name); }
        public void SetValue(string name, string val) { _setValue(name, val); }
        public CompareModel Compare(IObject o) { return _compare(o, null); }
        public BvSelectList GetList(string name) { return _getList(name); }
        public event ValidationEvent Validation;
        public event ValidationEvent ValidationEnd;
        public event AfterPostEvent AfterPost;
      
        public Dictionary<string, string> GetFieldTags(string name)
        {
          return null;
        }
      #endregion

        private bool IsRIRPropChanged(string fld, EHealthCaseAM c)
        {
            return IsReadOnly(fld) != c.IsReadOnly(fld) || IsInvisible(fld) != c.IsInvisible(fld) || IsRequired(fld) != c._isRequired(m_isRequired, fld);
        }
        private bool IsLookupContentChanged(DbManagerProxy manager, string fld, EHealthCaseAM c)
        {
            var thisLookup = GetValue(fld + "Lookup") as IList;
            var thatLookup = c.GetValue(fld + "Lookup") as IList;
            bool bChangeLookupContent = thisLookup.Count != thatLookup.Count;
            if (!bChangeLookupContent)
            {
                for (int i = 0; i < thisLookup.Count; i++)
                {
                    if (((thisLookup[i] as IObject).Key as IComparable).CompareTo((thatLookup[i] as IObject).Key) != 0 &&
                        (thisLookup[i] as IObject).ToStringProp != null && ((thisLookup[i] as IObject).ToStringProp as IComparable).CompareTo((thatLookup[i] as IObject).ToStringProp) != 0)
                    {
                        bChangeLookupContent = true;
                        break;
                    }
                }
            }
            return bChangeLookupContent;
        }
        

      

        public EHealthCaseAM()
        {
            
        }

        partial void Changed(string fieldName);
        partial void Created(DbManagerProxy manager);
        partial void Loaded(DbManagerProxy manager);
        partial void Deleted();
        partial void ParsedFormCollection(NameValueCollection form);
        partial void ParsingFormCollection(NameValueCollection form);

        

        private bool m_IsForcedToDelete;
        [LocalizedDisplayName("IsForcedToDelete")]
        public bool IsForcedToDelete { get { return m_IsForcedToDelete; } }

        private bool m_IsMarkedToDelete;
        [LocalizedDisplayName("IsMarkedToDelete")]
        public bool IsMarkedToDelete { get { return m_IsMarkedToDelete; } }

        public void _SetupMainHandler()
        {
            PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(EHealthCaseAM_PropertyChanged);
        }
        public void _RevokeMainHandler()
        {
            PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(EHealthCaseAM_PropertyChanged);
        }
        private void EHealthCaseAM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            (sender as EHealthCaseAM).Changed(e.PropertyName);
            
            if (e.PropertyName == _str_Parent)
                OnPropertyChanged(_str_master);
                  
        }
        
        public bool ForceToDelete() { return _Delete(true); }
        internal bool _Delete(bool isForceDelete)
        {
            if (!_ValidateOnDelete()) return false;
            _DeletingExtenders();
            m_IsMarkedToDelete = true;
            m_IsForcedToDelete = m_IsForcedToDelete ? m_IsForcedToDelete : isForceDelete;
            OnPropertyChanged("IsMarkedToDelete");
            _DeletedExtenders();
            Deleted();
            return true;
        }
        private bool _ValidateOnDelete(bool bReport = true)
        {
            
            return true;                
              
        }
        private void _DeletingExtenders()
        {
            EHealthCaseAM obj = this;
            
        }
        private void _DeletedExtenders()
        {
            EHealthCaseAM obj = this;
            
        }
        
        public bool OnValidation(ValidationModelException ex)
        {
            if (Validation != null)
            {
                var args = new ValidationEventArgs(ex.MessageId, ex.FieldName, ex.PropertyName, ex.Pars, ex.ValidatorType, ex.Obj, ex.ValidationType);
                Validation(this, args);
                return args.Continue;
            }
            return false;
        }
        public bool OnValidationEnd(ValidationModelException ex)
        {
            if (ValidationEnd != null)
            {
                var args = new ValidationEventArgs(ex.MessageId, ex.FieldName, ex.PropertyName, ex.Pars, ex.ValidatorType, ex.Obj, ex.ValidationType);
                ValidationEnd(this, args);
                return args.Continue;
            }
            return false;
        }

        public void OnAfterPost()
        {
            if (AfterPost != null)
            {
                AfterPost(this, EventArgs.Empty);
            }
        }

        public FormSize FormSize
        {
            get { return FormSize.Undefined; }
        }
    
        private bool _isInvisible(string name)
        {
            
            return false;
                
        }

    
        private bool _isReadOnly(string name)
        {
            
            return ReadOnly;
                
        }

        private bool m_isValid = true;
        internal bool _isValid
        {
            get { return m_isValid; }
            set
            {
                m_isValid = value;
        
            }
        }

        private bool m_readOnly;
        private bool _readOnly
        {
            get { return m_readOnly; }
            set
            {
                m_readOnly = value;
        
            }
        }


        internal Dictionary<string, Func<EHealthCaseAM, bool>> m_isRequired;
        private bool _isRequired(Dictionary<string, Func<EHealthCaseAM, bool>> isRequiredDict, string name)
        {
            if (isRequiredDict != null && isRequiredDict.ContainsKey(name))
                return isRequiredDict[name](this);
            return false;
        }
        
        public void AddRequired(string name, Func<EHealthCaseAM, bool> func)
        {
            if (m_isRequired == null) 
                m_isRequired = new Dictionary<string, Func<EHealthCaseAM, bool>>();
            if (!m_isRequired.ContainsKey(name))
                m_isRequired.Add(name, func);
        }
    
    internal Dictionary<string, Func<EHealthCaseAM, bool>> m_isHiddenPersonalData;
    private bool _isHiddenPersonalData(string name)
    {
      if (m_isHiddenPersonalData != null && m_isHiddenPersonalData.ContainsKey(name))
        return m_isHiddenPersonalData[name](this);
      return false;
    }

    public void AddHiddenPersonalData(string name, Func<EHealthCaseAM, bool> func)
    {
      if (m_isHiddenPersonalData == null)
        m_isHiddenPersonalData = new Dictionary<string, Func<EHealthCaseAM, bool>>();
      if (!m_isHiddenPersonalData.ContainsKey(name))
        m_isHiddenPersonalData.Add(name, func);
    }
  
        #region IDisposable Members
        partial void Disposed();
        private bool bIsDisposed;
        protected bool bNeedLookupRemove;
        ~EHealthCaseAM()
        {
            Dispose();
        }
        public void Dispose()
        {
            if (!bIsDisposed) 
            {
                bIsDisposed = true;
                m_Parent = null;
                m_permissions = null;
                
                this.ClearModelObjEventInvocations();
                
                
                if (bNeedLookupRemove)
                {
                
                }
                Disposed();
            }
            GC.SuppressFinalize(this);
        }
        #endregion
    
        #region ILookupUsage Members
        public void ReloadLookupItem(DbManagerProxy manager, string lookup_object)
        {
            
        }
        #endregion
    
        public void ParseFormCollection(NameValueCollection form, bool bParseLookups = true, bool bParseRelations = true)
        {
            ParsingFormCollection(form);
            if (bParseLookups)
            {
                _field_infos.Where(i => i._type == "Lookup").ToList().ForEach(a => { if (form[ObjectIdent + a._formname] != null) a._set_func(this, form[ObjectIdent + a._formname]);} );
            }
            
            _field_infos.Where(i => i._type != "Lookup" && i._type != "Child" && i._type != "Relation" && i._type != null)
                .ToList().ForEach(a => { if (form.AllKeys.Contains(ObjectIdent + a._formname)) a._set_func(this, form[ObjectIdent + a._formname]);} );
      
            if (bParseRelations)
            {
        
            }
            ParsedFormCollection(form);
        }
    

        #region Accessor
        public abstract partial class Accessor
        : DataAccessor<EHealthCaseAM>
            , IObjectAccessor
            , IObjectMeta
            , IObjectValidator
            
            , IObjectCreator
            , IObjectCreator<EHealthCaseAM>
            
            , IObjectSelectDetailList
            , IObjectPost
                
        {
            #region IObjectAccessor
            public string KeyName { get { return "DateOfCompletionOfPaperForm"; } }
            #endregion
        
            public delegate void on_action(EHealthCaseAM obj);
            private static Accessor g_Instance = CreateInstance<Accessor>();
            private CacheScope m_CS;
            public static Accessor Instance(CacheScope cs) 
            { 
                if (cs == null)
                    return g_Instance;
                lock(cs)
                {
                    object acc = cs.Get(typeof (Accessor));
                    if (acc != null)
                    {
                        return acc as Accessor;
                    }
                    Accessor ret = CreateInstance<Accessor>();
                    ret.m_CS = cs;
                    cs.Add(typeof(Accessor), ret);
                    return ret;
                }
            }
            
            public virtual List<IObject> SelectDetailList(DbManagerProxy manager, object ident, int? HACode = null)
            {
                return _SelectList(manager
                    , (Int64?)ident
                        
                    , null
                    , null
                    ).Cast<IObject>().ToList();
            }
            
        
            public virtual List<EHealthCaseAM> SelectList(DbManagerProxy manager
                , Int64? idfEHealthCaseAMRequest
                )
            {
                return _SelectList(manager
                    , idfEHealthCaseAMRequest
                    , delegate(EHealthCaseAM obj)
                        {
                        }
                    , delegate(EHealthCaseAM obj)
                        {
                        }
                    );
            }

            

            public List<EHealthCaseAM> _SelectList(DbManagerProxy manager
                , Int64? idfEHealthCaseAMRequest
                , on_action loading, on_action loaded
                )
            {
                var ret = _SelectListInternal(manager
                    , idfEHealthCaseAMRequest
                    , loading
                    , loaded
                    );
                  
                  return ret;
            }
      
            
            public virtual List<EHealthCaseAM> _SelectListInternal(DbManagerProxy manager
                , Int64? idfEHealthCaseAMRequest
                , on_action loading, on_action loaded
                )
            {
                EHealthCaseAM _obj = null;
                try
                {
                    MapResultSet[] sets = new MapResultSet[1];
                    List<EHealthCaseAM> objs = new List<EHealthCaseAM>();
                    sets[0] = new MapResultSet(typeof(EHealthCaseAM), objs);
                    
                    manager
                        .SetSpCommand("spEHealthCaseAM_SelectList"
                            , manager.Parameter("@idfEHealthCaseAMRequest", idfEHealthCaseAMRequest)
                            )
                        .ExecuteResultSet(sets);
                    foreach(var obj in objs) 
                    {
                        _obj = obj;
                        obj.m_CS = m_CS;
                        
                        if (loading != null)
                            loading(obj);
                        _SetupLoad(manager, obj);
                        
                        if (loaded != null)
                            loaded(obj);
                    }
                    
                    return objs;
                }
                catch(DataException e)
                {
                    throw DbModelException.Create(_obj, e);
                }
            }
    

            public virtual IObject SelectDetail(DbManagerProxy manager, object ident, int? HACode = null)
            {
                if (ident == null)
                {
                    return CreateNew(manager, null, HACode);
                }
                else
                {
                    return _SelectByKey(manager
                        , (Int64?)ident
                            
                        , null, null
                        );
                }
            }
            public virtual EHealthCaseAM SelectDetailT(DbManagerProxy manager, object ident, int? HACode = null)
            {
                if (ident == null)
                {
                    return CreateNewT(manager, null, HACode);
                }
                else
                {
                    return _SelectByKey(manager
                        , (Int64?)ident
                            
                        , null, null
                        );
                }
            }

            
            public virtual EHealthCaseAM SelectByKey(DbManagerProxy manager
                , Int64? idfEHealthCaseAMRequest
                )
            {
                return _SelectByKey(manager
                    , idfEHealthCaseAMRequest
                    , null, null
                    );
            }
            

            private EHealthCaseAM _SelectByKey(DbManagerProxy manager
                , Int64? idfEHealthCaseAMRequest
                , on_action loading, on_action loaded
                )
            {
                return _SelectByKeyInternal(manager
                    , idfEHealthCaseAMRequest
                    , loading, loaded
                    )
                    
                    ;
            }
      
            
            protected virtual EHealthCaseAM _SelectByKeyInternal(DbManagerProxy manager
                , Int64? idfEHealthCaseAMRequest
                , on_action loading, on_action loaded
                )
            {
            
                MapResultSet[] sets = new MapResultSet[1];
                List<EHealthCaseAM> objs = new List<EHealthCaseAM>();
                sets[0] = new MapResultSet(typeof(EHealthCaseAM), objs);
                EHealthCaseAM obj = null;
                try
                {
                    manager
                        .SetSpCommand("spEHealthCaseAM_SelectList"
                            , manager.Parameter("@idfEHealthCaseAMRequest", idfEHealthCaseAMRequest)
                            )
                        .ExecuteResultSet(sets);

                    if (objs.Count == 0)
                        return null;

                    obj = objs[0];
                    obj.m_CS = m_CS;
                    
                  
                    if (loading != null)
                        loading(obj);
                    _SetupLoad(manager, obj);
                    

                    //obj._setParent();
                    if (loaded != null)
                    loaded(obj);
                    obj.Loaded(manager);
                    return obj;
                  }
                  catch(DataException e)
                  {
                    throw DbModelException.Create(obj, e);
                  }
                
            }
    
        
        
            internal void _SetupLoad(DbManagerProxy manager, EHealthCaseAM obj, bool bCloning = false)
            {
                if (obj == null) return;
                
                // loading extenters - begin
                // loading extenters - end
                if (!bCloning)
                {
                
                }
                _LoadLookups(manager, obj);
                obj._setParent();
                
                // loaded extenters - begin
                // loaded extenters - end
                
                _SetupHandlers(obj);
                _SetupChildHandlers(obj, null);
                
                _SetPermitions(obj._permissions, obj);
                _SetupRequired(obj);
                _SetupPersonalDataRestrictions(obj);
                obj._SetupMainHandler();
                obj.AcceptChanges();
            }
            
            internal void _SetPermitions(IObjectPermissions permissions, EHealthCaseAM obj)
            {
                if (obj != null)
                {
                    obj._permissions = permissions;
                    if (obj._permissions != null)
                    {
                    
                    }
                }
            }

    

            private EHealthCaseAM _CreateNew(DbManagerProxy manager, IObject Parent, int? HACode, on_action creating, on_action created, bool isFake = false)
            {
                EHealthCaseAM obj = null;
                try
                {
                    obj = EHealthCaseAM.CreateInstance();
                    obj.m_CS = m_CS;
                    obj.m_IsNew = true;
                    obj.Parent = Parent;
                    
                    if (creating != null)
                        creating(obj);
                
                    // creating extenters - begin
                    // creating extenters - end
                
                    _LoadLookups(manager, obj);
                    _SetupHandlers(obj);
                    _SetupChildHandlers(obj, null);
                    
                    obj._SetupMainHandler();
                    obj._setParent();
                
                    // created extenters - begin
                    // created extenters - end
        
                    if (created != null)
                        created(obj);
                    obj.Created(manager);
                    _SetPermitions(obj._permissions, obj);
                    _SetupRequired(obj);
                    _SetupPersonalDataRestrictions(obj);
                    return obj;
                }
                catch(DataException e)
                {
                    throw DbModelException.Create(obj, e);
                }
            }

            
            public EHealthCaseAM CreateNewT(DbManagerProxy manager, IObject Parent, int? HACode = null)
            {
                return _CreateNew(manager, Parent, HACode, null, null);
            }
            public IObject CreateNew(DbManagerProxy manager, IObject Parent, int? HACode = null)
            {
                return _CreateNew(manager, Parent, HACode, null, null);
            }
            public EHealthCaseAM CreateFakeT(DbManagerProxy manager, IObject Parent, int? HACode = null)
            {
                return _CreateNew(manager, Parent, HACode, null, null, true);
            }
            public IObject CreateFake(DbManagerProxy manager, IObject Parent, int? HACode = null)
            {
                return _CreateNew(manager, Parent, HACode, null, null, true);
            }
            
            public EHealthCaseAM CreateWithParamsT(DbManagerProxy manager, IObject Parent, List<object> pars)
            {
                return _CreateNew(manager, Parent, null, null, null);
            }
            public IObject CreateWithParams(DbManagerProxy manager, IObject Parent, List<object> pars)
            {
                return _CreateNew(manager, Parent, null, null, null);
            }
            
            private void _SetupChildHandlers(EHealthCaseAM obj, object newobj)
            {
                
            }
            
            private void _SetupHandlers(EHealthCaseAM obj)
            {
                
            }
    

            internal void _LoadLookups(DbManagerProxy manager, EHealthCaseAM obj)
            {
                
            }
    
            public bool DeleteObject(DbManagerProxy manager, object ident)
            {
                IObject obj = SelectDetail(manager, ident);
                if (!obj.MarkToDelete()) return false;
                return Post(manager, obj);
            }
        
            public bool Post(DbManagerProxy manager, IObject obj, bool bChildObject = false) 
            {
                throw new NotImplementedException();
            }
            
      
            protected ValidationModelException ChainsValidate(EHealthCaseAM obj)
            {
                
                return null;
            }
            protected bool ChainsValidate(EHealthCaseAM obj, bool bRethrowException)
            {
                ValidationModelException ex = ChainsValidate(obj);
                if (ex != null)
                {
                    if (bRethrowException)
                        throw ex;
                    if (!obj.OnValidation(ex))
                    {
                        obj.OnValidationEnd(ex);
                        return false;
                    }
                }
                return true;
            }
        
            public bool Validate(DbManagerProxy manager, IObject obj, bool bPostValidation, bool bChangeValidation, bool bDeepValidation, bool bRethrowException = false)
            {
                return Validate(manager, obj as EHealthCaseAM, bPostValidation, bChangeValidation, bDeepValidation, bRethrowException);
            }
            public bool Validate(DbManagerProxy manager, EHealthCaseAM obj, bool bPostValidation, bool bChangeValidation, bool bDeepValidation, bool bRethrowException = false)
            {
                if (!ChainsValidate(obj, bRethrowException))
                    return false;
                    
                
                return true;
            }
           
    
            private void _SetupRequired(EHealthCaseAM obj)
            {
            
            }
    
    private void _SetupPersonalDataRestrictions(EHealthCaseAM obj)
    {
    
    
    }
  
            #region IObjectMeta
            public int? MaxSize(string name) { return Meta.Sizes.ContainsKey(name) ? (int?)Meta.Sizes[name] : null; }
            public bool RequiredByField(string name, IObject obj) { return Meta.RequiredByField.ContainsKey(name) ? Meta.RequiredByField[name](obj as EHealthCaseAM) : false; }
            public bool RequiredByProperty(string name, IObject obj) { return Meta.RequiredByProperty.ContainsKey(name) ? Meta.RequiredByProperty[name](obj as EHealthCaseAM) : false; }
            public List<SearchPanelMetaItem> SearchPanelMeta { get { return Meta.SearchPanelMeta; } }
            public List<GridMetaItem> GridMeta { get { return Meta.GridMeta; } }
            public List<ActionMetaItem> Actions { get { return Meta.Actions; } }
            public string DetailPanel { get { return "EHealthCaseAMDetail"; } }
            public string HelpIdWin { get { return ""; } }
            public string HelpIdWeb { get { return ""; } }
            public string HelpIdHh { get { return ""; } }
            public string SqlSortOrder { get { return Meta.sqlSortOrder; } }
            #endregion
    
        }

        
        #region Meta
        public static class Meta
        {
            public static string spSelect = "spEHealthCaseAM_SelectList";
            public static string spCount = "";
            public static string spPost = "";
            public static string spInsert = "";
            public static string spUpdate = "";
            public static string spDelete = "";
            public static string spCanDelete = "";
            public static string sqlSortOrder = "";
            public static Dictionary<string, int> Sizes = new Dictionary<string, int>();
            public static Dictionary<string, Func<EHealthCaseAM, bool>> RequiredByField = new Dictionary<string, Func<EHealthCaseAM, bool>>();
            public static Dictionary<string, Func<EHealthCaseAM, bool>> RequiredByProperty = new Dictionary<string, Func<EHealthCaseAM, bool>>();
            public static List<SearchPanelMetaItem> SearchPanelMeta = new List<SearchPanelMetaItem>();
            public static List<GridMetaItem> GridMeta = new List<GridMetaItem>();
            public static List<ActionMetaItem> Actions = new List<ActionMetaItem>();
            
            
    private static Dictionary<string, List<Func<bool>>> m_isHiddenPersonalData = new Dictionary<string, List<Func<bool>>>();
    internal static bool _isHiddenPersonalData(string name)
    {
      if (m_isHiddenPersonalData.ContainsKey(name))
          return m_isHiddenPersonalData[name].Aggregate(false, (s,c) => s | c());
      return false;
    }

    private static void AddHiddenPersonalData(string name, Func<bool> func)
    {
      if (!m_isHiddenPersonalData.ContainsKey(name))
          m_isHiddenPersonalData.Add(name, new List<Func<bool>>());
      m_isHiddenPersonalData[name].Add(func);
    }
  
            
            static Meta()
            {
                
                Sizes.Add(_str_Diagnosis, 200);
                Sizes.Add(_str_PersonalID, 100);
                Sizes.Add(_str_PatientsLastName, 200);
                Sizes.Add(_str_PatientsFirstName, 200);
                Sizes.Add(_str_PatientsMiddleName, 200);
                Sizes.Add(_str_PatientsSex, 200);
                Sizes.Add(_str_PatientsCitizenship, 200);
                Sizes.Add(_str_PatientsCurrentResidence, 2000);
                Sizes.Add(_str_PatientsPermanentResidence, 2000);
                Sizes.Add(_str_NameOfEmployer, 200);
                Sizes.Add(_str_PatientsOccupation, 200);
                Sizes.Add(_str_HospitalName, 200);
                Sizes.Add(_str_Outcome, 200);
                Sizes.Add(_str_HumanCaseLocalIdentifier, 200);
                Sizes.Add(_str_VisitIdentifier, 200);
                Sizes.Add(_str_NotificationSentByFacility, 200);
                Sizes.Add(_str_NotificationSentByFacilityName, 2000);
                Sizes.Add(_str_NotificationSentByFacilityPhone, 200);
                Sizes.Add(_str_NotificationSentByFacilityAddress, 2000);
                Sizes.Add(_str_NotificationSentByName, 700);
                Sizes.Add(_str_NotificationSentBySsn, 200);
                Sizes.Add(_str_NotificationSentByMobile, 200);
                Sizes.Add(_str_PlaceOfHospitalization, 200);
                Sizes.Add(_str_PlaceOfHospitalizationName, 2000);
                Sizes.Add(_str_PlaceOfHospitalizationPhone, 200);
                Sizes.Add(_str_PlaceOfHospitalizationAddress, 2000);
                Sizes.Add(_str_PersonalIDType, 200);
                Sizes.Add(_str_StatusOfPatientAtTimeOfNotification, 200);
                Actions.Add(new ActionMetaItem(
                    "Edit",
                    ActionTypes.Edit,
                    false,
                    String.Empty,
                    String.Empty,
                    (manager, c, pars) => new ActResult(true, c),
                    null,
                    new ActionMetaItem.VisualItem(
                        /*from BvMessages*/"strEdit_Id",
                        "edit",
                        /*from BvMessages*/"tooltipEdit_Id",
                        /*from BvMessages*/"strView_Id",
                        "View1",
                        /*from BvMessages*/"tooltipEdit_Id",
                        ActionsAlignment.Right,
                        ActionsPanelType.Group,
                        ActionsAppType.All
                      ),
                      false,
                      null,
                      null,
                      null,
                      null,
                      null,
                      false
                      ));
                    
        
                _SetupPersonalDataRestrictions();
            }
            
            
    private static void _SetupPersonalDataRestrictions()
    {
    

    }
  
        }
        #endregion
    

        #endregion
        }
    
}
	