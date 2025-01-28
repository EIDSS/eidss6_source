using eidss.model.Reports.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eidss.model.Core;
using bv.model.Model.Core;
using eidss.model.Enums;

namespace eidss.model.Reports.UA
{
    [Serializable]
    public class UACov19ListModel : BaseIntervalModel
    {
        private AddressModel _address;
        public AddressModel Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
            }
        }

        [LocalizedDisplayName("UACov19Filter_CaseClassification")]
        public long? CaseClassification { get; set; }

        [LocalizedDisplayName("UACov19Filter_Outcome")]
        public long? Outcome { get; set; }

        public List<SelectListItemSurrogate> CaseClassificationsList { get; private set; }
        public List<SelectListItemSurrogate> OutcomesList { get; private set; }
        public List<SelectListItemSurrogate> CaseClassificationsListEn { get; private set; }
        public List<SelectListItemSurrogate> OutcomesListEn { get; private set; }

        private long? defRegionId;
        private long? defRayonId;
        private long? defSettlementId;

        public void ClearLookups()
        {
            if (CaseClassificationsList != null) CaseClassificationsList.Clear();
            if (OutcomesList != null) OutcomesList.Clear();
            if (CaseClassificationsListEn != null) CaseClassificationsListEn.Clear();
            if (OutcomesListEn != null) OutcomesListEn.Clear();
        }

        public void InitLookups()
        {
            ClearLookups();
            FilterHelper.GetDefaultLocation(out defRegionId, out defRayonId, out defSettlementId);
            CaseClassificationsList = FilterHelper.GetCaseClassificationsList(ModelUserContext.CurrentLanguage, (int)HACode.Human, false, false, false);
            OutcomesList = FilterHelper.GetOutcomesList(ModelUserContext.CurrentLanguage, (int)HACode.Human, true);
            CaseClassificationsListEn = FilterHelper.GetCaseClassificationsList("en", (int)HACode.Human, false, false, false);
            OutcomesListEn = FilterHelper.GetOutcomesList("en", (int)HACode.Human, true);
        }

        private string GetLookupValueName(List<SelectListItemSurrogate> lookup, long? id)
        {
            var strName = string.Empty;
            
            if ((lookup != null) && (id.HasValue))
            {
                var item = lookup.FirstOrDefault(c => (c.Value != null) && c.Value.Equals(id.Value.ToString(), StringComparison.InvariantCultureIgnoreCase));
                if (item != null)
                    strName = item.Text;
            }

            return strName;
        }

        public UACov19ListModel()
        {
            InitLookups();

            Address = new AddressModel(defRegionId, defRayonId, defSettlementId);
            Address.IsSettlementVisible = true;

            MinStartDate = new DateTime(2019, 1, 1);
            MaxStartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day).AddDays(1).AddSeconds(-1);
            StartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day).AddDays(-1).AddHours(18);
            
            MinEndDate = new DateTime(2019, 1, 1);
            MaxEndDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day).AddDays(1).AddSeconds(-1);
            EndDate = StartDate.AddDays(1);

            CaseClassification = 350000000; //Confirmed

            ValidationMessage = string.Empty;
        }

        public UACov19ListModel(long? regionId, long? rayonId, long? settlementId)
        {
            InitLookups();

            Address = new AddressModel(regionId, rayonId, settlementId);
            Address.IsSettlementVisible = true;

            MinStartDate = new DateTime(2019, 1, 1);
            MaxStartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day).AddDays(1).AddSeconds(-1);
            StartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day).AddDays(-1).AddHours(18);

            MinEndDate = new DateTime(2019, 1, 1);
            MaxEndDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day).AddDays(1).AddSeconds(-1);
            EndDate = StartDate.AddDays(1);

            CaseClassification = 350000000; //Confirmed

            ValidationMessage = string.Empty;
        }

        public UACov19ListModel
            (string lang, DateTime startDate, DateTime endDate, 
             long? regionId, long? rayonId, long? settlementId, 
             bool useArchive): base(lang, startDate, endDate, useArchive)
        {
            InitLookups();

            Address = new AddressModel(regionId, rayonId, settlementId);
            Address.IsSettlementVisible = true;

            MinStartDate = new DateTime(2019, 1, 1);
            MaxStartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day).AddDays(1).AddSeconds(-1);
            StartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day).AddDays(-1).AddHours(18);

            MinEndDate = new DateTime(2019, 1, 1);
            MaxEndDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day).AddDays(1).AddSeconds(-1);
            EndDate = StartDate.AddDays(1);

            CaseClassification = 350000000; //Confirmed

            ValidationMessage = string.Empty;
        }


        public void Dispose()
        {
            ClearLookups();
        }


        public override List<SelectListItemSurrogate> SupportedExportFormats
        {
            get { return FilterHelper.GetExcelExportFormat(); }
        }

        public string GenerateName()
        {
            StringBuilder name = new StringBuilder();

            /*
             Region, Rayon, Settlement, 
             Case Classification, Patient Status, 
             Date from (date format: YYYYMMDDhhmm), Date to (date format: YYYYMMDDhhmm).
             */

            if (Address.RegionId.HasValue)
            {
                name.Append(Address.RegionName("en"));
                if (Address.RayonId.HasValue)
                {
                    name.Append('_');
                    name.Append(Address.RayonName("en"));

                    if (Address.SettlementId.HasValue)
                    {
                        name.Append('_');
                        name.Append(Address.SettlementName("en"));
                    }
                    //else
                    //{
                    //    name.Append("NoSettlement");
                    //}
                }
                //else
                //{
                //    name.Append("NoRayon");
                //}
            }
            else
            {
                name.Append("Ukraine");
            }

            if (Outcome.HasValue)
            {
                var strOutcome = GetLookupValueName(OutcomesListEn, Outcome.Value);
                if (!string.IsNullOrEmpty(strOutcome))
                {
                    name.Append('_');
                    name.Append(strOutcome);
                }
            }
            else
            {
                if (CaseClassification.HasValue)
                {
                    var strCaseClassification = GetLookupValueName(CaseClassificationsListEn, CaseClassification.Value);
                    if (!string.IsNullOrEmpty(strCaseClassification))
                    {
                        name.Append('_');
                        name.Append(strCaseClassification);
                    }
                }
            }

            name.Append('_');
            if ((StartDate != null) &&
                ((!MinStartDate.HasValue) || (MinStartDate.HasValue && StartDate >= MinStartDate)) &&
                ((!MaxStartDate.HasValue) || (MaxStartDate.HasValue && StartDate <= MaxStartDate)))
            {
                name.Append(StartDate.ToString("yyyyMMddHHmm"));
            }
            else
            {
                name.Append("NoDateFrom");
            }

            name.Append('_');
            if ((EndDate != null) &&
                ((!MinEndDate.HasValue) || (MinEndDate.HasValue && EndDate >= MinEndDate)) &&
                ((!MaxEndDate.HasValue) || (MaxEndDate.HasValue && EndDate <= MaxEndDate)))
            {
                name.Append(EndDate.ToString("yyyyMMddHHmm"));
            }
            else
            {
                name.Append("NoDateTo");
            }

            return name.ToString();
        }

        public override string ToString()
        {
            var strCaseClassification = string.Empty;
            if (CaseClassification.HasValue)
            {
                strCaseClassification = GetLookupValueName(CaseClassificationsListEn, CaseClassification.Value);
            }

            var strOutcome = string.Empty;
            if (Outcome.HasValue)
            {
                strOutcome = GetLookupValueName(OutcomesListEn, Outcome.Value);
            }

            return base.ToString() + string.Format(" Region:{0}, Rayon:{1}, Settlement:{2}, CaseClasification:{3}, Outcome:{4}",
                Address.RegionId.HasValue ? Address.RegionId.Value.ToString() + string.Format("({0})", Address.RegionName("en")) : string.Empty,
                Address.RayonId.HasValue ? Address.RayonId.Value.ToString() + string.Format("({0})", Address.RayonName("en")) : string.Empty,
                Address.SettlementId.HasValue ? Address.SettlementId.Value.ToString() + string.Format("({0})", Address.SettlementName("en")) : string.Empty,
                CaseClassification.HasValue ? CaseClassification.Value.ToString() + string.Format("({0})", strCaseClassification) : string.Empty,
                Outcome.HasValue ? Outcome.Value.ToString() + string.Format("({0})", strOutcome) : string.Empty
                );
        }

    }
}
