using eidss.model.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eidss.model.Model.UploadEhs
{
    public class UploadEhsPatientJsonSchema: IJsonSchema
    {
        [JsonProperty(Required = Required.Default, PropertyName = "patient_id")]
        public string PatientId { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "first_name")]
        public string FirstName { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "last_name")]
        public string LastName { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "second_name")]
        public string SecondName { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "person_birth_date")]
        public string PersonBirthDate { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "gender")]
        public string Gender { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "phones")]
        public List<EhsPhone> Phones { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "address")]
        public EhsAddress Address { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "errors", ObjectCreationHandling = ObjectCreationHandling.Replace, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public EhsError[] Errors { get; set; }
    }

    public class EhsAddress 
    {
        [JsonProperty(Required = Required.Default, PropertyName = "zip")]
        public string Zip { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "area")]
        public string Area { get; set; }

        [JsonProperty(PropertyName = "region")]
        public string Region { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "settlement_type")]
        public string SettlementType { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "settlement")]
        public string Settlement { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "street_type")]
        public string StreetType { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "street")]
        public string Street { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "building")]
        public string Building { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "apartment")]
        public string Appartment { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "errors", ObjectCreationHandling = ObjectCreationHandling.Replace, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public EhsError[] Errors { get; set; }
    }

    public class EhsPhone
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "number")]
        public string Number { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "errors", ObjectCreationHandling = ObjectCreationHandling.Replace, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public EhsError[] Errors { get; set; }
    }

    public class EhsError
    {
        [JsonProperty(Required = Required.Default, PropertyName = "error_field", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string error_field { get; set; }

        [JsonProperty(Required = Required.Default, PropertyName = "error_message", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string error_message { get; set; }
    }
}
