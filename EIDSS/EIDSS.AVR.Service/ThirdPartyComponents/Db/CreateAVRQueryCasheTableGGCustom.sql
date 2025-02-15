USE [AVR_CASHE]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AVR_HumanCaseReportGGCustom_65K]') AND type in (N'U'))
DROP TABLE [dbo].[AVR_HumanCaseReportGGCustom_65K]
GO



CREATE TABLE [dbo].[AVR_HumanCaseReportGGCustom_65K](
	[sflHC_CompletionPaperFormDate] [datetime] NULL, 
	[sflHC_AntimicrobialTherapy_ID] [bigint] NULL, 
	[sflHC_AntimicrobialTherapy] [nvarchar](4000) NULL, 
	[sflHC_PatientFirstSoughtCareDate] [datetime] NULL, 
	[sflHC_PatientHospitalizationDate] [datetime] NULL, 
	[sflHC_HospitalizationPlace] [nvarchar](400) NULL, 
	[sflHC_Hospitalization_ID] [bigint] NULL, 
	[sflHC_Hospitalization] [nvarchar](4000) NULL, 
	[sflHC_PatientSex_ID] [bigint] NULL, 
	[sflHC_PatientSex] [nvarchar](4000) NULL, 
	[sflHC_PatientNotificationStatus_ID] [bigint] NULL, 
	[sflHC_PatientNotificationStatus] [nvarchar](4000) NULL, 
	[sflHC_SymptomOnsetDate] [datetime] NULL, 
	[sflHC_PatientName] [nvarchar](1204) NULL, 
	[sflHC_PatientEmployerPhone] [nvarchar](400) NULL, 
	[sflHC_PatientPhone] [nvarchar](400) NULL, 
	[sflHC_PatientEmployer] [nvarchar](400) NULL, 
	[sflHC_PatientEmpRegion_ID] [bigint] NULL, 
	[sflHC_PatientEmpRegion] [nvarchar](730) NULL, 
	[sflHC_PatientEmpRayon_ID] [bigint] NULL, 
	[sflHC_PatientEmpRayon] [nvarchar](730) NULL, 
	[sflHC_PatientEmpAddress] [nvarchar](1606) NULL, 
	[sflHC_PatientEmpSettlement_ID] [bigint] NULL, 
	[sflHC_PatientEmpSettlement] [nvarchar](730) NULL, 
	[sflHC_FacilityLastVisitDate] [datetime] NULL, 
	[sflHC_PatientOccupation_ID] [bigint] NULL, 
	[sflHC_PatientOccupation] [nvarchar](4000) NULL, 
	[sflHC_PatientNationality_ID] [bigint] NULL, 
	[sflHC_PatientNationality] [nvarchar](4000) NULL, 
	[sflHC_PatientDOB] [datetime] NULL, 
	[sflHC_PatientDischargeDate] [datetime] NULL, 
	[sflHC_PatientDeathDate] [datetime] NULL, 
	[sflHC_PatientAgeGroup] [nvarchar](200) NULL, 
	[sflHC_PatientAgeType_ID] [bigint] NULL, 
	[sflHC_PatientAgeType] [nvarchar](4000) NULL, 
	[sflHC_PatientAge] [int] NULL, 
	[sflHC_PatientCRRegion_ID] [bigint] NULL, 
	[sflHC_PatientCRRegion] [nvarchar](730) NULL, 
	[sflHC_PatientCRRayon_ID] [bigint] NULL, 
	[sflHC_PatientCRRayon] [nvarchar](730) NULL, 
	[sflHC_PatientCRAddress] [nvarchar](1606) NULL, 
	[sflHC_PatientCRSettlement_ID] [bigint] NULL, 
	[sflHC_PatientCRSettlement] [nvarchar](730) NULL, 
	[sflHC_CurrentLocation] [nvarchar](400) NULL, 
	[sflHC_HospitalizationStatus_ID] [bigint] NULL, 
	[sflHC_HospitalizationStatus] [nvarchar](4000) NULL, 
	[sflHC_ExposureDate] [datetime] NULL, 
	[sflHC_OutbreakID] [nvarchar](400) NULL, 
	[sflHC_DaysAfterNotification] [int] NULL, 
	[sflHC_RelatedToOutbreak_ID] [bigint] NULL, 
	[sflHC_RelatedToOutbreak] [nvarchar](4000) NULL, 
	[sflHC_ChangedDiagnosisCode] [nvarchar](400) NULL, 
	[sflHC_ChangedDiagnosis_ID] [bigint] NULL, 
	[sflHC_ChangedDiagnosis] [nvarchar](4000) NULL, 
	[sflHC_EnteredDate] [datetime] NULL, 
	[sflHC_ReceivedByPerson] [nvarchar](1204) NULL, 
	[sflHC_ReceivedByOffice_ID] [bigint] NULL, 
	[sflHC_ReceivedByOffice] [nvarchar](4000) NULL, 
	[sflHC_NotificationDate] [datetime] NULL, 
	[sflHC_SentByPerson] [nvarchar](1204) NULL, 
	[sflHC_SentByOffice_ID] [bigint] NULL, 
	[sflHC_SentByOffice] [nvarchar](4000) NULL, 
	[sflHC_ModificationDate] [datetime] NULL, 
	[sflHC_CaseProgressStatus_ID] [bigint] NULL, 
	[sflHC_CaseProgressStatus] [nvarchar](4000) NULL, 
	[sflHC_InitialCaseClassification_ID] [bigint] NULL, 
	[sflHC_InitialCaseClassification] [nvarchar](4000) NULL, 
	[sflHC_FinalCaseClassification_ID] [bigint] NULL, 
	[sflHC_FinalCaseClassification] [nvarchar](4000) NULL, 
	[sflHC_FinalDiagnosisCode] [nvarchar](400) NULL, 
	[sflHC_FinalDiagnosisDate] [datetime] NULL, 
	[sflHC_FinalDiagnosis_ID] [bigint] NULL, 
	[sflHC_FinalDiagnosis] [nvarchar](4000) NULL, 
	[sflHC_CaseClassification_ID] [bigint] NULL, 
	[sflHC_CaseClassification] [nvarchar](4000) NULL, 
	[sflHC_InvestigatedByOffice_ID] [bigint] NULL, 
	[sflHC_InvestigatedByOffice] [nvarchar](4000) NULL, 
	[sflHC_InvestigationStartDate] [datetime] NULL, 
	[sflHC_LocationRegion_ID] [bigint] NULL, 
	[sflHC_LocationRegion] [nvarchar](730) NULL, 
	[sflHC_LocationRayon_ID] [bigint] NULL, 
	[sflHC_LocationRayon] [nvarchar](730) NULL, 
	[sflHC_LocationCoordinates] [nvarchar](128) NULL, 
	[sflHC_LocationSettlement_ID] [bigint] NULL, 
	[sflHC_LocationSettlement] [nvarchar](730) NULL, 
	[sflHC_SamplesCollected_ID] [bigint] NULL, 
	[sflHC_SamplesCollected] [nvarchar](4000) NULL, 
	[sflHC_ChangedDiagnosisDate] [datetime] NULL, 
	[sflHC_DiagnosisCode] [nvarchar](400) NULL, 
	[sflHC_DiagnosisDate] [datetime] NULL, 
	[sflHC_Diagnosis_ID] [bigint] NULL, 
	[sflHC_Diagnosis] [nvarchar](4000) NULL, 
	[sflHC_Outcome_ID] [bigint] NULL, 
	[sflHC_Outcome] [nvarchar](4000) NULL, 
	[sflHC_LocalID] [nvarchar](400) NULL, 
	[sflHC_CaseID] [nvarchar](400) NULL, 
	[sflHC_LabDiagBasis_ID] [int] NULL, 
	[sflHC_LabDiagBasis] [nvarchar](4000) NULL, 
	[sflHC_EpiDiagBasis_ID] [int] NULL, 
	[sflHC_EpiDiagBasis] [nvarchar](4000) NULL, 
	[sflHC_ClinicalDiagBasis_ID] [int] NULL, 
	[sflHC_ClinicalDiagBasis] [nvarchar](4000) NULL
) ON [PRIMARY]

GO

INSERT into [dbo].[AVR_HumanCaseReportGGCustom_65K]
select 
	sflHC_CompletionPaperFormDate,
	sflHC_AntimicrobialTherapy_ID,
	sflHC_AntimicrobialTherapy,
	sflHC_PatientFirstSoughtCareDate,
	sflHC_PatientHospitalizationDate,
	sflHC_HospitalizationPlace,
	sflHC_Hospitalization_ID,
	sflHC_Hospitalization,
	sflHC_PatientSex_ID,
	sflHC_PatientSex,
	sflHC_PatientNotificationStatus_ID,
	sflHC_PatientNotificationStatus,
	sflHC_SymptomOnsetDate,
	sflHC_PatientName,
	sflHC_PatientEmployerPhone,
	sflHC_PatientPhone,
	sflHC_PatientEmployer,
	sflHC_PatientEmpRegion_ID,
	sflHC_PatientEmpRegion,
	sflHC_PatientEmpRayon_ID,
	sflHC_PatientEmpRayon,
	sflHC_PatientEmpAddress,
	sflHC_PatientEmpSettlement_ID,
	sflHC_PatientEmpSettlement,
	sflHC_FacilityLastVisitDate,
	sflHC_PatientOccupation_ID,
	sflHC_PatientOccupation,
	sflHC_PatientNationality_ID,
	sflHC_PatientNationality,
	sflHC_PatientDOB,
	sflHC_PatientDischargeDate,
	sflHC_PatientDeathDate,
	sflHC_PatientAgeGroup,
	sflHC_PatientAgeType_ID,
	sflHC_PatientAgeType,
	sflHC_PatientAge,
	sflHC_PatientCRRegion_ID,
	sflHC_PatientCRRegion,
	sflHC_PatientCRRayon_ID,
	sflHC_PatientCRRayon,
	sflHC_PatientCRAddress,
	sflHC_PatientCRSettlement_ID,
	sflHC_PatientCRSettlement,
	sflHC_CurrentLocation,
	sflHC_HospitalizationStatus_ID,
	sflHC_HospitalizationStatus,
	sflHC_ExposureDate,
	sflHC_OutbreakID,
	sflHC_DaysAfterNotification,
	sflHC_RelatedToOutbreak_ID,
	sflHC_RelatedToOutbreak,
	sflHC_ChangedDiagnosisCode,
	sflHC_ChangedDiagnosis_ID,
	sflHC_ChangedDiagnosis,
	sflHC_EnteredDate,
	sflHC_ReceivedByPerson,
	sflHC_ReceivedByOffice_ID,
	sflHC_ReceivedByOffice,
	sflHC_NotificationDate,
	sflHC_SentByPerson,
	sflHC_SentByOffice_ID,
	sflHC_SentByOffice,
	sflHC_ModificationDate,
	sflHC_CaseProgressStatus_ID,
	sflHC_CaseProgressStatus,
	sflHC_InitialCaseClassification_ID,
	sflHC_InitialCaseClassification,
	sflHC_FinalCaseClassification_ID,
	sflHC_FinalCaseClassification,
	sflHC_FinalDiagnosisCode,
	sflHC_FinalDiagnosisDate,
	sflHC_FinalDiagnosis_ID,
	sflHC_FinalDiagnosis,
	sflHC_CaseClassification_ID,
	sflHC_CaseClassification,
	sflHC_InvestigatedByOffice_ID,
	sflHC_InvestigatedByOffice,
	sflHC_InvestigationStartDate,
	sflHC_LocationRegion_ID,
	sflHC_LocationRegion,
	sflHC_LocationRayon_ID,
	sflHC_LocationRayon,
	sflHC_LocationCoordinates,
	sflHC_LocationSettlement_ID,
	sflHC_LocationSettlement,
	sflHC_SamplesCollected_ID,
	sflHC_SamplesCollected,
	sflHC_ChangedDiagnosisDate,
	sflHC_DiagnosisCode,
	sflHC_DiagnosisDate,
	sflHC_Diagnosis_ID,
	sflHC_Diagnosis,
	sflHC_Outcome_ID,
	sflHC_Outcome,
	sflHC_LocalID,
	sflHC_CaseID,
	sflHC_LabDiagBasis_ID,
	sflHC_LabDiagBasis,
	sflHC_EpiDiagBasis_ID,
	sflHC_EpiDiagBasis,
	sflHC_ClinicalDiagBasis_ID,
	sflHC_ClinicalDiagBasis
from [EIDSS_v5_GG_65k].[dbo].fn010SearchQuery__218770001101218770001101('en')

--select * from [dbo].[AVR_HumanCaseReportGGCustom_65K]

