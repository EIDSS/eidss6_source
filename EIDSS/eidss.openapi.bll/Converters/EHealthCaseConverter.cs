using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using BLToolkit.EditableObjects;
using bv.model.BLToolkit;
using bv.model.Model.Core;
using eidss.model.Core;
using eidss.model.Schema;
using eidss.openapi.contract;
using eidss.openapi.bll.Exceptions;

namespace eidss.openapi.bll.Converters
{
    internal class EHealthCaseConverter :
        BaseConverter<eidss.openapi.contract.EHealthCase, eidss.model.Schema.EHealthCaseAM>
    {
        private static EHealthCaseConverter _instance = new EHealthCaseConverter();
        private EHealthCaseConverter() { AutoConverter.Nop(); }
        public static EHealthCaseConverter Instance { get { return _instance; } }

        internal static void Register()
        {
            Mapper.CreateMap<eidss.openapi.contract.EHealthCase, eidss.model.Schema.EHealthCaseAM>()

                .ForMember(c => c.DateOfCompletionOfPaperForm, e => e.MapFrom(m => m.DateOfCompletionOfPaperForm))

                .ForMember(m => m.Diagnosis, e => e.Condition(c => !string.IsNullOrEmpty(c.Diagnosis)))
                .ForMember(c => c.Diagnosis, e => e.MapFrom(m => m.Diagnosis))

                .ForMember(m => m.DiagnosisDate, e => e.Condition(c => c.DiagnosisDate != null))
                .ForMember(p => p.DiagnosisDate, e => e.MapFrom(m => m.DiagnosisDate))

                .ForMember(m => m.PersonalID, e => e.Condition(c => !string.IsNullOrEmpty(c.PersonalID)))
                .ForMember(c => c.PersonalID, e => e.MapFrom(m => m.PersonalID))

                .ForMember(m => m.PatientsLastName, e => e.Condition(c => !string.IsNullOrEmpty(c.PatientsLastName)))
                .ForMember(c => c.PatientsLastName, e => e.MapFrom(m => m.PatientsLastName))

                .ForMember(m => m.PatientsFirstName, e => e.Condition(c => !string.IsNullOrEmpty(c.PatientsFirstName)))
                .ForMember(c => c.PatientsFirstName, e => e.MapFrom(m => m.PatientsFirstName))

                .ForMember(c => c.PatientsMiddleName, e => e.MapFrom(m => m.PatientsMiddleName))

                .ForMember(m => m.PatientsDateOfBirth, e => e.Condition(c => c.PatientsDateOfBirth != null))
                .ForMember(c => c.PatientsDateOfBirth, e => e.MapFrom(m => m.PatientsDateOfBirth))

                .ForMember(m => m.PatientsSex, e => e.Condition(c => !string.IsNullOrEmpty(c.PatientsSex)))
                .ForMember(c => c.PatientsSex, e => e.MapFrom(m => m.PatientsSex))

                .ForMember(c => c.PatientsCitizenship, e => e.MapFrom(m => m.PatientsCitizenship))

                .ForMember(m => m.PatientsCurrentResidenceRegion, e => e.Condition(c => (c.PatientsCurrentResidenceRegion != null) && c.PatientsCurrentResidenceRegion > 0))
                .ForMember(p => p.PatientsCurrentResidenceRegion, e => e.MapFrom(m => m.PatientsCurrentResidenceRegion))

                .ForMember(c => c.PatientsCurrentResidence, e => e.MapFrom(m => m.PatientsCurrentResidence))

                .ForMember(c => c.PatientsPermanentResidenceRegion, e => e.MapFrom(m => m.PatientsPermanentResidenceRegion))

                .ForMember(c => c.PatientsPermanentResidence, e => e.MapFrom(m => m.PatientsPermanentResidence))

                .ForMember(c => c.NameOfEmployer, e => e.MapFrom(m => m.NameOfEmployer))

                .ForMember(c => c.PatientsOccupation, e => e.MapFrom(m => m.PatientsOccupation))

                .ForMember(c => c.HospitalName, e => e.MapFrom(m => m.HospitalName))

                .ForMember(c => c.Outcome, e => e.MapFrom(m => m.Outcome))

                .ForMember(c => c.DateOfDeath, e => e.MapFrom(m => m.DateOfDeath))

                .ForMember(c => c.HumanCaseLocalIdentifier, e => e.MapFrom(m => m.HumanCaseLocalIdentifier))

                .ForMember(c => c.VisitIdentifier, e => e.MapFrom(m => m.VisitIdentifier))

                .ForMember(c => c.NotificationSentByFacility, e => e.MapFrom(m => m.NotificationSentByFacility))
                .ForMember(c => c.NotificationSentByFacilityName, e => e.MapFrom(m => m.NotificationSentByFacilityName))
                .ForMember(c => c.NotificationSentByFacilityPhone, e => e.MapFrom(m => m.NotificationSentByFacilityPhone))
                .ForMember(c => c.NotificationSentByFacilityRegion, e => e.MapFrom(m => m.NotificationSentByFacilityRegion))
                .ForMember(c => c.NotificationSentByFacilityAddress, e => e.MapFrom(m => m.NotificationSentByFacilityAddress))

                .ForMember(c => c.NotificationSentByName, e => e.MapFrom(m => m.NotificationSentByName))

                .ForMember(c => c.NotificationSentBySsn, e => e.MapFrom(m => m.NotificationSentBySsn))

                .ForMember(c => c.NotificationSentByMobile, e => e.MapFrom(m => m.NotificationSentByMobile))

                .ForMember(c => c.NotificationDate, e => e.MapFrom(m => m.NotificationDate))

                .ForMember(c => c.PlaceOfHospitalization, e => e.MapFrom(m => m.PlaceOfHospitalization))
                .ForMember(c => c.PlaceOfHospitalizationName, e => e.MapFrom(m => m.PlaceOfHospitalizationName))
                .ForMember(c => c.PlaceOfHospitalizationPhone, e => e.MapFrom(m => m.PlaceOfHospitalizationPhone))
                .ForMember(c => c.PlaceOfHospitalizationRegion, e => e.MapFrom(m => m.PlaceOfHospitalizationRegion))
                .ForMember(c => c.PlaceOfHospitalizationAddress, e => e.MapFrom(m => m.PlaceOfHospitalizationAddress))

                .ForMember(c => c.DateOfHospitalization, e => e.MapFrom(m => m.DateOfHospitalization))

                .ForMember(c => c.PersonalIDType, e => e.MapFrom(m => m.PersonalIDType))

                .ForMember(c => c.Hospitalization, e => e.MapFrom(m => m.Hospitalization))

                .ForMember(c => c.StatusOfPatientAtTimeOfNotification, e => e.MapFrom(m => m.StatusOfPatientAtTimeOfNotification))

                // ignore
                .ForMember(p => p.idfEHealthCaseAMRequest, e => e.Ignore())
                .ForMember(p => p.idfEHealthCaseAMItem, e => e.Ignore())
                .ForAllMembers(e => e.Condition(AutoConverter.CheckReadOnly<eidss.model.Schema.EHealthCaseAM>))
                ;
            Mapper.CreateMap<eidss.model.Schema.EHealthCaseAM, eidss.openapi.contract.EHealthCase>()
                .ForMember(c => c.DateOfCompletionOfPaperForm, e => e.MapFrom(m => m.DateOfCompletionOfPaperForm))
                .ForMember(c => c.Diagnosis, e => e.MapFrom(m => m.Diagnosis))
                .ForMember(p => p.DiagnosisDate, e => e.MapFrom(m => m.DiagnosisDate))
                .ForMember(c => c.PersonalID, e => e.MapFrom(m => m.PersonalID))
                .ForMember(c => c.PatientsLastName, e => e.MapFrom(m => m.PatientsLastName))
                .ForMember(c => c.PatientsFirstName, e => e.MapFrom(m => m.PatientsFirstName))
                .ForMember(c => c.PatientsMiddleName, e => e.MapFrom(m => m.PatientsMiddleName))
                .ForMember(c => c.PatientsDateOfBirth, e => e.MapFrom(m => m.PatientsDateOfBirth))
                .ForMember(c => c.PatientsSex, e => e.MapFrom(m => m.PatientsSex))
                .ForMember(c => c.PatientsCitizenship, e => e.MapFrom(m => m.PatientsCitizenship))
                .ForMember(p => p.PatientsCurrentResidenceRegion, e => e.MapFrom(m => m.PatientsCurrentResidenceRegion))
                .ForMember(c => c.PatientsCurrentResidence, e => e.MapFrom(m => m.PatientsCurrentResidence))
                .ForMember(c => c.PatientsPermanentResidenceRegion, e => e.MapFrom(m => m.PatientsPermanentResidenceRegion))
                .ForMember(c => c.PatientsPermanentResidence, e => e.MapFrom(m => m.PatientsPermanentResidence))
                .ForMember(c => c.NameOfEmployer, e => e.MapFrom(m => m.NameOfEmployer))
                .ForMember(c => c.PatientsOccupation, e => e.MapFrom(m => m.PatientsOccupation))
                .ForMember(c => c.HospitalName, e => e.MapFrom(m => m.HospitalName))
                .ForMember(c => c.Outcome, e => e.MapFrom(m => m.Outcome))
                .ForMember(c => c.DateOfDeath, e => e.MapFrom(m => m.DateOfDeath))
                .ForMember(c => c.HumanCaseLocalIdentifier, e => e.MapFrom(m => m.HumanCaseLocalIdentifier))
                .ForMember(c => c.VisitIdentifier, e => e.MapFrom(m => m.VisitIdentifier))
                .ForMember(c => c.NotificationSentByFacility, e => e.MapFrom(m => m.NotificationSentByFacility))
                .ForMember(c => c.NotificationSentByFacilityName, e => e.MapFrom(m => m.NotificationSentByFacilityName))
                .ForMember(c => c.NotificationSentByFacilityPhone, e => e.MapFrom(m => m.NotificationSentByFacilityPhone))
                .ForMember(c => c.NotificationSentByFacilityRegion, e => e.MapFrom(m => m.NotificationSentByFacilityRegion))
                .ForMember(c => c.NotificationSentByFacilityAddress, e => e.MapFrom(m => m.NotificationSentByFacilityAddress))
                .ForMember(c => c.NotificationSentByName, e => e.MapFrom(m => m.NotificationSentByName))
                .ForMember(c => c.NotificationSentBySsn, e => e.MapFrom(m => m.NotificationSentBySsn))
                .ForMember(c => c.NotificationSentByMobile, e => e.MapFrom(m => m.NotificationSentByMobile))
                .ForMember(c => c.NotificationDate, e => e.MapFrom(m => m.NotificationDate))
                .ForMember(c => c.PlaceOfHospitalization, e => e.MapFrom(m => m.PlaceOfHospitalization))
                .ForMember(c => c.PlaceOfHospitalizationName, e => e.MapFrom(m => m.PlaceOfHospitalizationName))
                .ForMember(c => c.PlaceOfHospitalizationPhone, e => e.MapFrom(m => m.PlaceOfHospitalizationPhone))
                .ForMember(c => c.PlaceOfHospitalizationRegion, e => e.MapFrom(m => m.PlaceOfHospitalizationRegion))
                .ForMember(c => c.PlaceOfHospitalizationAddress, e => e.MapFrom(m => m.PlaceOfHospitalizationAddress))
                .ForMember(c => c.DateOfHospitalization, e => e.MapFrom(m => m.DateOfHospitalization))
                .ForMember(c => c.PersonalIDType, e => e.MapFrom(m => m.PersonalIDType))
                .ForMember(c => c.Hospitalization, e => e.MapFrom(m => m.Hospitalization))
                .ForMember(c => c.StatusOfPatientAtTimeOfNotification, e => e.MapFrom(m => m.StatusOfPatientAtTimeOfNotification))
                ;
        }

    }
}