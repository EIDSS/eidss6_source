using System;
using System.Linq;
using bv.common.Core;
using bv.common.Configuration;
using bv.model.BLToolkit;
using bv.model.Model.Core;
using bv.model.Helpers;
using eidss.model.Enums;
using eidss.model.Resources;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace eidss.model.Schema
{
    public partial class UploadEhsEventItem
    {
        public int ItemHash { get; set; }
        private JObject _rawValue = new JObject();
        public JObject RawValue { get {return _rawValue; } set { _rawValue = value; } }
        public void AddError(string colCaption, string error)
        {
            var err = validationErrors.FirstOrDefault(i => i.Item1 == colCaption);
            if(err !=null)
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
                validateMandatory(event_type, "event_type");
                validateMandatory(code, "code");
                validateMandatory(clinical_status_or_value, event_type.Equals("clinical", StringComparison.CurrentCultureIgnoreCase) ? "clinical_status" : "value");

                var date_attr = event_type.Equals("clinical", StringComparison.CurrentCultureIgnoreCase) ? "onset_date" : "issued";
                if (!validationErrors.Any(x => x.Item1 == date_attr))
                {
                    validateMandatory(onset_or_issued_date, date_attr);
                }

                validateMandatory(managing_organization_edrpou, "managing_organization_edrpou");
                validateMandatory(managing_organization_name, "managing_organization_name");
                validateMandatory(division_area, "division_area");
                validateMandatory(division_settlement, "division_settlement");

                // Check Reference Values
                validateRef(event_type, "event_type", () => (event_type.Equals("clinical", StringComparison.CurrentCultureIgnoreCase) || event_type.Equals("laboratory", StringComparison.CurrentCultureIgnoreCase)));
                if (event_type == "clinical")
                {
                    validateRef(code, "code", () => master.DiagnosisRefs.Any(i => i.ehsClinicalCode.Equals(code, StringComparison.CurrentCultureIgnoreCase)));
                }
                else
                {
                    validateRef(code, "code", () => master.TestNameRefs.Any(i => i.ehsLaboratoryCode.Equals(code, StringComparison.CurrentCultureIgnoreCase)));
                    validateRef(clinical_status_or_value, "value", () => master.TestResultRefs.Any(i => i.ehsLaboratoryValue.Equals(clinical_status_or_value, StringComparison.CurrentCultureIgnoreCase)));
                }

                validateRef(division_area, "division_area", () => master.RegionRefs.Any(i => i.ehsArea.Equals(division_area, StringComparison.CurrentCultureIgnoreCase)));
                validateRef(division_region, "address_region", () => string.IsNullOrEmpty(division_region) || master.RayonRefs.Any(i => i.ehsArea.Equals(division_area, StringComparison.CurrentCultureIgnoreCase) && i.ehsRegion.Equals(division_region, StringComparison.CurrentCultureIgnoreCase)));
                validateRef(division_settlement, "division_settlement", () => master.SettlementRefs.Any(i => i.ehsArea.Equals(division_area, StringComparison.CurrentCultureIgnoreCase) && Utils.Str(i.ehsRegion).Equals(Utils.Str(division_region), StringComparison.CurrentCultureIgnoreCase) && i.ehsSettlement.Equals(division_settlement, StringComparison.CurrentCultureIgnoreCase)));

                // Existing Patient
                if (!string.IsNullOrEmpty(patient_id))
                {
                    if (!master.PatientIdDictionary.ContainsKey(patient_id))
                        AddError("patient_id", EidssMessages.Get("msgEhsNotExistingPatient"));
                    else if (!validationErrors.Any(x => x.Item1 == date_attr))
                    {
                        var dateOfBirth = master.PatientIdDictionary[patient_id];
                        // Date Business Rules
                        if (dateOfBirth.HasValue && onset_or_issued_date.HasValue && dateOfBirth.Value > onset_or_issued_date.Value)
                            AddError(date_attr, EidssMessages.Get("msgEhsDateBusinessRuleFailed"));
                    }
                }

                var result = (validationErrors.Count == 0);

                if (result)
                {
                    PostWithoutMaster = 0;
                    intResult = 0;
                }

                return result;
            }
            catch(Exception ex)
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
            catch(Exception ex)
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

    }
}
