using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace eidss.openapi.contract
{
    /// <summary>Full description of the request for transferring cases from e-Health into EIDSS</summary>
    [DataContract]
    public class EHealthCaseRequest
    {
        /// <summary>
        /// List of the e-Health Cases to be transferred into EIDSS.
        /// </summary>
        [DataMember(Name = "Values")]
        public List<EHealthCase> Values { get; set; }
    }

    /// <summary>Full description and list of attributes of the e-Health Case</summary>
    [DataContract]
    public class EHealthCase
    {
        /// <summary>
        /// The date on which a case has been registered in e-Health. <br/>
        /// This is a mandatory attribute for transferring a case from e-Health.
        /// </summary>
        [DataMember(Name = "date_of_completion_of_paper_form", IsRequired = false)]
        public DateTime? DateOfCompletionOfPaperForm { get; set; }

        /// <summary>
        /// The string code of initial diagnosis that was given when case was registered in e-Health. <br/>
        /// This is a mandatory attribute for transferring a case from e-Health.
        /// </summary>
        [DataMember(Name = "diagnosis", IsRequired = true)]
        public string Diagnosis { get; set; }

        /// <summary>
        /// The date when an initial diagnosis was made. <br/>
        /// This is a mandatory attribute for transferring a case from e-Health.
        /// </summary>
        [DataMember(Name = "diagnosis_date", IsRequired = true)]
        public DateTime DiagnosisDate { get; set; }

        /// <summary>
        /// The string value representing SSN (Personal Identifier) of the patient. <br/>
        /// This is a mandatory attribute for transferring a case from e-Health.
        /// </summary>
        [DataMember(Name = "personal_ID", IsRequired = true)]
        public string PersonalID { get; set; }

        /// <summary>
        /// Last name of the patient. <br/>
        /// This is a mandatory attribute for transferring a case from e-Health.
        /// </summary>
        [DataMember(Name = "patients_last_Name", IsRequired = true)]
        public string PatientsLastName { get; set; }

        /// <summary>
        /// First name of the patient. <br/>
        /// This is a mandatory attribute for transferring a case from e-Health.
        /// </summary>
        [DataMember(Name = "patients_first_Name", IsRequired = true)]
        public string PatientsFirstName { get; set; }

        /// <summary>
        /// Patronymic or middle name of the patient.
        /// </summary>
        [DataMember(Name = "patients_middle_Name", IsRequired = false)]
        public string PatientsMiddleName { get; set; }

        /// <summary>
        /// The date of the patient’s birth. <br/>
        /// This is a mandatory attribute for transferring a case from e-Health.
        /// </summary>
        [DataMember(Name = "patients_date_of_birth", IsRequired = true)]
        public DateTime PatientsDateOfBirth { get; set; }

        /// <summary>
        /// The string value representing the gender of the patient. <br/>
        /// This is a mandatory attribute for transferring a case from e-Health.
        /// </summary>
        [DataMember(Name = "patients_sex", IsRequired = true)]
        public string PatientsSex { get; set; }

        /// <summary>
        /// The string value representing the citizenship of the patient.
        /// </summary>
        [DataMember(Name = "patients_citizenship", IsRequired = false)]
        public string PatientsCitizenship { get; set; }

        /// <summary>
        /// The integer code representing region name of the patient's current residence address.
        /// </summary>
        [DataMember(Name = "patients_current_residence_region", IsRequired = false)]
        public int? PatientsCurrentResidenceRegion { get; set; }

        /// <summary>
        /// The address string representing the patient's current residence address within the specified region.
        /// </summary>
        [DataMember(Name = "patients_current_residence", IsRequired = false)]
        public string PatientsCurrentResidence { get; set; }

        /// <summary>
        /// The integer code representing region name of the patient's permanent residence address. <br/>
        /// This is a mandatory attribute for transferring a case from e-Health.
        /// </summary>
        [DataMember(Name = "patients_permanent_residence_region", IsRequired = true)]
        public int PatientsPermanentResidenceRegion { get; set; }

        /// <summary>
        /// The address string representing the patient's permanent residence address within the specified region.
        /// </summary>
        [DataMember(Name = "patients_permanent_residence", IsRequired = false)]
        public string PatientsPermanentResidence { get; set; }

        /// <summary>
        /// Name of the facility of the patient’s current employer, children’s facility or school if the patient is a student.
        /// </summary>
        [DataMember(Name = "name_of_employer", IsRequired = false)]
        public string NameOfEmployer { get; set; }

        /// <summary>
        /// The string value representing the occupation of the patient.
        /// </summary>
        [DataMember(Name = "patients_occupation", IsRequired = false)]
        public string PatientsOccupation { get; set; }

        /// <summary>
        /// The string code representing the hospital facility where the patient was at the time of the notification. <br/>
        /// By default, it coincides with the facility that submitted the case to e-Health. <br/>
        /// The value is taken if the hospitalization attribute is equal to 1.
        /// </summary>
        [DataMember(Name = "hospital_name", IsRequired = false)]
        public string HospitalName { get; set; }

        /// <summary>
        /// The string value representing the outcome of the case.
        /// </summary>
        [DataMember(Name = "outcome", IsRequired = false)]
        public string Outcome { get; set; }

        /// <summary>
        /// The date of the patient’s death.
        /// </summary>
        [DataMember(Name = "date_of_death", IsRequired = false)]
        public DateTime? DateOfDeath { get; set; }

        /// <summary>
        /// The string value representing unique case identification number, e.g. identifier of the case registered in e-Health. <br/>
        /// This is a mandatory attribute for transferring a case from e-Health.
        /// </summary>
        [DataMember(Name = "human_case_local_identifier", IsRequired = true)]
        public string HumanCaseLocalIdentifier { get; set; }

        /// <summary>
        /// The string value representing unique identification number of the patient's visit <br/>
        /// to the health care organization in e-Health. <br/>
        /// This is a mandatory attribute for transferring a case from e-Health.
        /// </summary>
        [DataMember(Name = "visit_id", IsRequired = false)]
        public string VisitIdentifier { get; set; }

        /// <summary>
        /// The string code representing the facility that submitted the case to e-Health.
        /// </summary>
        [DataMember(Name = "notification_sent_by_facility", IsRequired = false)]
        public string NotificationSentByFacility { get; set; }

        /// <summary>
        /// The name of the facility that submitted the case to e-Health.
        /// </summary>
        [DataMember(Name = "notification_sent_by_facility_name", IsRequired = false)]
        public string NotificationSentByFacilityName { get; set; }

        /// <summary>
        /// The string value representing the phone number of the facility that submitted the case to e-Health.
        /// </summary>
        [DataMember(Name = "notification_sent_by_facility_phone_number", IsRequired = false)]
        public string NotificationSentByFacilityPhone { get; set; }

        /// <summary>
        /// The integer code representing region name, where the facility submitted the case to e-Health is located.
        /// </summary>
        [DataMember(Name = "notification_sent_by_facility_region", IsRequired = false)]
        public int? NotificationSentByFacilityRegion { get; set; }

        /// <summary>
        /// The address string representing the address of the facility submitted the case to e-Health within the specified region.
        /// </summary>
        [DataMember(Name = "notification_sent_by_facility_address", IsRequired = false)]
        public string NotificationSentByFacilityAddress { get; set; }

        /// <summary>
        /// The Last, First and Patronymic name of the employee that submitted the case to e-Health.
        /// </summary>
        [DataMember(Name = "notification_sent_by_name", IsRequired = false)]
        public string NotificationSentByName { get; set; }

        /// <summary>
        /// The SSN (Personal Identifier) of the employee that submitted the case to e-Health.
        /// </summary>
        [DataMember(Name = "notification_sent_by_ssn", IsRequired = false)]
        public string NotificationSentBySsn { get; set; }

        /// <summary>
        /// The phone number of the employee that submitted the case to e-Health.
        /// </summary>
        [DataMember(Name = "notification_sent_by_mobile", IsRequired = false)]
        public string NotificationSentByMobile { get; set; }

        /// <summary>
        /// The date when a case has been transferred from e-Health into EIDSS.
        /// </summary>
        [DataMember(Name = "notification_date", IsRequired = false)]
        public DateTime? NotificationDate { get; set; }

        /// <summary>
        /// The string code representing the facility, where the patient is hospitalized. <br/>
        /// By default, it coincides with the facility that submitted the case to e-Health. <br/>
        /// The value is taken if the hospitalization attribute is equal to 1.
        /// </summary>
        [DataMember(Name = "place_of_hospitalization", IsRequired = false)]
        public string PlaceOfHospitalization { get; set; }

        /// <summary>
        /// The name of the facility, where the patient is hospitalized. <br/>
        /// By default, it coincides with the facility that submitted the case to e-Health. <br/>
        /// The value is taken if the hospitalization attribute is equal to 1.
        /// </summary>
        [DataMember(Name = "place_of_hospitalization_name", IsRequired = false)]
        public string PlaceOfHospitalizationName { get; set; }

        /// <summary>
        /// The string value representing the phone number of the facility, where the patient is hospitalized. <br/>
        /// By default, it coincides with the phone number of the facility that submitted the case to e-Health. <br/>
        /// The value is taken if the hospitalization attribute is equal to 1.
        /// </summary>
        [DataMember(Name = "place_of_hospitalization_phone_number", IsRequired = false)]
        public string PlaceOfHospitalizationPhone { get; set; }

        /// <summary>
        /// The integer code representing region name, where the patient is hospitalized. <br/>
        /// By default, it coincides with the region name, where the facility submitted the case to e-Health is located. <br/>
        /// The value is taken if the hospitalization attribute is equal to 1.
        /// </summary>
        [DataMember(Name = "place_of_hospitalization_region", IsRequired = false)]
        public int? PlaceOfHospitalizationRegion { get; set; }

        /// <summary>
        /// The address string representing the address, where the patient is hospitalized. <br/>
        /// By default, it coincides with address of the facility that submitted the case to e-Health. <br/>
        /// The value is taken if the hospitalization attribute is equal to 1.
        /// </summary>
        [DataMember(Name = "place_of_hospitalization_address", IsRequired = false)]
        public string PlaceOfHospitalizationAddress { get; set; }

        /// <summary>
        /// The date when the patient was hospitalized for the current diagnosis.
        /// </summary>
        [DataMember(Name = "date_of_hospitalization", IsRequired = false)]
        public DateTime? DateOfHospitalization { get; set; }

        /// <summary>
        /// The string value representing the type of the personal identifier of the patient.
        /// </summary>
        [DataMember(Name = "personal_ID_type", IsRequired = false)]
        public string PersonalIDType { get; set; }

        /// <summary>
        /// The integer code representing status of the hospitalization of the patient.
        /// </summary>
        [DataMember(Name = "hospitalization", IsRequired = false)]
        public int? Hospitalization { get; set; }

        /// <summary>
        /// The string value representing physical status of the patient at the time of notification.
        /// </summary>
        [DataMember(Name = "status_of_the_patient_at_time_of_notification", IsRequired = false)]
        public string StatusOfPatientAtTimeOfNotification { get; set; }
    }
}
