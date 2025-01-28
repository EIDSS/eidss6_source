using System;
using System.Linq;
using bv.common.Core;
using bv.model.BLToolkit;
using bv.model.Model.Core;
using bv.model.Helpers;
using eidss.model.Resources;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace eidss.model.Schema
{

    public enum UploadEhsPatientResolution
    {
        Updated = 0,
        Created = 1,
        Dismissed = 2
    }
    public partial class UploadEhsPatientItem
    {
        public int ItemHash { get; set; }
        private JObject _rawValue = new JObject();
        public JObject RawValue { get { return _rawValue; } set { _rawValue = value; } }
        public void AddError(string colCaption, string error)
        {
            var err = validationErrors.FirstOrDefault(i => i.Item1 == colCaption);
            if (err != null)
            {
                error = err.Item2 + "\r\n" + error;
                validationErrors.Remove(err);
            }
            validationErrors.Add(new Tuple<string, string>(colCaption, error));

        }

        public bool ValidateItem()
        {
            try
            {
                var master = this.Parent as UploadEhsMaster;

                // Mandatory Fields
                validateMandatory(patient_id, "patient_id");
                validateMandatory(last_name, "last_name");
                validateMandatory(first_name, "first_name");
                if (!validationErrors.Any(x => x.Item1 == "person_birth_date"))
                {
                    validateMandatory(person_birth_date, "person_birth_date");
                }

                validateMandatory(gender, "gender");

                validateMandatory(address_type, "address_type");
                validateMandatory(address_area, "address_area");
                validateMandatory(address_settlement, "address_settlement");

                // Check Reference Values
                validateRef(gender, "gender", () => master.HumanGenderRefs.Any(i => i.ehsHumanGender.Equals(gender, StringComparison.CurrentCultureIgnoreCase)));
                validateRef(address_area, "address_area", () => master.RegionRefs.Any(i => i.ehsArea.Equals(address_area, StringComparison.CurrentCultureIgnoreCase)));
                validateRef(address_region, "address_region", () => string.IsNullOrEmpty(address_region) || master.RayonRefs.Any(i => i.ehsArea.Equals(address_area, StringComparison.CurrentCultureIgnoreCase) && i.ehsRegion.Equals(address_region, StringComparison.CurrentCultureIgnoreCase)));
                validateRef(address_settlement, "address_settlement", () => master.SettlementRefs.Any(i => i.ehsArea.Equals(address_area, StringComparison.CurrentCultureIgnoreCase) && Utils.Str(i.ehsRegion).Equals(Utils.Str(address_region), StringComparison.CurrentCultureIgnoreCase) && i.ehsSettlement.Equals(address_settlement, StringComparison.CurrentCultureIgnoreCase)));

                // Date Business Rules
                if ((!validationErrors.Any(x => x.Item1 == "person_birth_date")) && (person_birth_date.HasValue && person_birth_date > DateTime.Today))
                    AddError("person_birth_date", EidssMessages.Get("msgEhsDoBFuture"));

                var result = (validationErrors.Count == 0);
                if (result)
                {
                    PostWithoutMaster = 0;
                }

                return result;
            }
            catch (Exception ex)
            {
                string e = ex.Message;
                throw;
            }
        }

        private void validateMandatory(DateTime? val, string name)
        {
            if (!val.HasValue)
                AddError(name, String.Format(EidssMessages.Get("msgEhsMandatoryField"), name));
        }
        private void validateMandatory(int? val, string name)
        {
            if (!val.HasValue || val.Value == 0)
                AddError(name, String.Format(EidssMessages.Get("msgEhsMandatoryField"), name));
        }
        private void validateMandatory(int? val, string name, Func<bool> predicate)
        {
            if ((!val.HasValue || val.Value == 0) && predicate())
                AddError(name, String.Format(EidssMessages.Get("msgEhsMandatoryField"), name));
        }
        private void validateMandatory(string val, string name)
        {
            if (String.IsNullOrEmpty(val))
                AddError(name, String.Format(EidssMessages.Get("msgEhsMandatoryField"), name));
        }

        private void validateRef(int? val, string name, Func<bool> predicate)
        {
            try
            {
                if (val.HasValue && val.Value != 0 && !predicate())
                    AddError(name, String.Format(EidssMessages.Get("msgEhsUnknownReferenceValue"), name));
            }
            catch (Exception ex)
            {
                string e = ex.Message;
                throw;
            }
        }
        private void validateRef(string val, string name, Func<bool> predicate)
        {
            if (!String.IsNullOrEmpty(val) && !predicate())
                AddError(name, String.Format(EidssMessages.Get("msgEhsUnknownReferenceValue"), name));
        }

        public partial class Accessor
        {
            public UploadEhsPatientItem CreateNewForTest(DbManagerProxy manager, IObject Parent)
            {
                var item = CreateNewT(manager, Parent);
                item.patient_id = "85c5585e-4081-46dc-98a0-68162bc7d728";
                item.first_name = "Наталя";
                item.last_name = "Турунь";
                item.second_name = "Василівна";
                item.person_birth_date = new DateTime(1981, 6, 2);
                item.gender = "FEMALE";
                item.phones = "+380991324338";
                item.address_zip = "64333";
                item.address_area = "ХАРКІВСЬКА";
                item.address_region = "БАЛАКЛІЙСЬКИЙ";
                item.address_settlement_type = "TOWNSHIP";
                item.address_settlement = "ДОНЕЦЬ";
                item.address_street_type = "STREET";
                item.address_street = "Центральна";
                item.address_building = "466-А";
                item.address_type = "RESIDENCE";
                item.address_apartment = "99";
                return item;
            }

            public UploadEhsPatientItem CreateNewForTest2(DbManagerProxy manager, IObject Parent)
            {
                var item = CreateNewT(manager, Parent);
                item.patient_id = "71ca8bff-ad41-4db1-8f8d-27a69cb104a6";
                item.first_name = "Христина";
                item.last_name = "Бурунь";
                item.second_name = "Іванівна";
                item.person_birth_date = new DateTime(2009, 9, 9);
                item.gender = "FEMALE";
                item.phones = "+380984394444";
                item.address_zip = "79099";
                item.address_area = "ЛЬВІВСЬКА";
                item.address_region = null;
                item.address_settlement_type = "CITY";
                item.address_settlement = "ЛЬВІВ";
                item.address_street_type = "STREET";
                item.address_street = "Прогулкова";
                item.address_building = "99";
                item.address_type = "RESIDENCE";
                item.address_apartment = "";
                return item;
            }
        }
    }
}
