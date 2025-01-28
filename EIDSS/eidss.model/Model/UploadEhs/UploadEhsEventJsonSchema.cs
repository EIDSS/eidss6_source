using eidss.model.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace eidss.model.Model.UploadEhs
{
    public class UploadEhsEventJsonSchema:IJsonSchema
    {
        [JsonProperty(Required = Required.Default, PropertyName = "patient_id")]
        public string PatientId { get; set; }

        [JsonProperty(PropertyName = "laboratory", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public EhsLaboratory Laboratory { get; set; }

        [JsonProperty(PropertyName = "clinical", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public EhsClinical Clinical { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "managing_organization")]
        public EhsManagingOrganization ManagingOrganization { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "division")]
        public EhsDivision Division { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "doctor")]
        public EhsDoctor Doctor { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "errors", ObjectCreationHandling = ObjectCreationHandling.Replace, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public EhsError[] Errors { get; set; }
    }

    public class EhsLaboratory
    {
        [JsonProperty(Required = Required.Default, PropertyName = "code")]
        public string Code { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "value")]
        public string Value { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "primary_source")]
        public bool PrimarySource { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "issued")]
        public string Issued { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "inserted_at")]
        public string InsertedAt { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "errors", ObjectCreationHandling = ObjectCreationHandling.Replace, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public EhsError[] Errors { get; set; }
    }
    public class EhsManagingOrganization
    {
        [JsonProperty(Required = Required.Default, PropertyName = "managing_organization_id")]
        public string ManagingOrganizationId { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "edrpou")]
        public string Edrpou { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "errors", ObjectCreationHandling = ObjectCreationHandling.Replace, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public EhsError[] Errors { get; set; }
    }

    public class EhsDivision
    {
        [JsonProperty(Required = Required.Default, PropertyName = "division_id")]
        public string DivisionId { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "phones")]
        public List<EhsPhone> Phones { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "zip")]
        public string Zip { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "area")]
        public string Area { get; set; }

        [JsonProperty(PropertyName = "region")]
        public string Region { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "settlement_type")]
        public string SettlementType { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "koatuu")]
        public string Koatuu { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "settlement")]
        public string Settlement { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "street_type")]
        public string StreetType { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "street")]
        public string Street { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "building")]
        public string Building { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "errors", ObjectCreationHandling = ObjectCreationHandling.Replace, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public EhsError[] Errors { get; set; }
    }

    public class EhsDoctor
    {
        [JsonProperty(Required = Required.Default, PropertyName = "performer_id")]
        public string PerformerId { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "last_name")]
        public string LastName { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "first_name")]
        public string FirstName { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "second_name")]
        public string SecondName { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "errors", ObjectCreationHandling = ObjectCreationHandling.Replace, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public EhsError[] Errors { get; set; }
    }

    public class EhsClinical 
    {
        [JsonProperty(Required = Required.Default, PropertyName = "code")]
        public string Code { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "clinical_status")]
        public string ClinicalStatus { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "primary_source")]
        public string PrimarySource { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "onset_date")]
        public string OnsetDate { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "inserted_at")]
        public string InsertedAt { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "errors", ObjectCreationHandling = ObjectCreationHandling.Replace, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public EhsError[] Errors { get; set; }
    }
}
