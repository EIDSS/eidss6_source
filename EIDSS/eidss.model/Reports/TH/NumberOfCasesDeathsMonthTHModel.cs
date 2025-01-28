using System;
using System.Collections.Generic;
using System.Text;
using bv.common.Core;
using bv.model.Model.Core;
using eidss.model.Core;
using eidss.model.Enums;
using eidss.model.Reports.Common;

namespace eidss.model.Reports.TH
{
    [Serializable]
    public class NumberOfCasesDeathsMonthTHModel : BaseThaiYearModel
    {
        public const int MaxNumberOfDistrict = 50;
        public const int MaxNumberOfDiagnosis = 10;
        public NumberOfCasesDeathsMonthTHModel()
        {
            Diagnoses = new MultipleDiagnosisModel();
            Regions = new MultipleRegionTHModel();
            Zones = new MultipleZoneTHModel();
            Provinces = new MultipleProvinceTHModel();
            //TODO: check if switch to separate model solution//Districts = new MultipleProvinceDistrictTreeTHModel();
            DistrictCheckedItems = new string[0];
           
            Language = ModelUserContext.CurrentLanguage;
            if (Language == Localizer.lngThai)
            {
                Year = ThaiCalendarHelper.GregorianYearToThai(Year);
            }
        }

        public NumberOfCasesDeathsMonthTHModel
            (string lang,
                int year,
                int reportModeIndex,
                string[] diagnoses,
                string[] regions,
                string[] zones,
                string[] provinces,
                string[] districts,
                long? caseClassification,
                long? organizationId, List<PersonalDataGroup> forbiddenGroups, bool useArchive)
            : base(lang, year, useArchive)
        {
            ReportModeIndex = reportModeIndex;
            Diagnoses = new MultipleDiagnosisModel(diagnoses);
            Regions = new MultipleRegionTHModel(regions);
            Zones = new MultipleZoneTHModel(zones);
            Provinces = new MultipleProvinceTHModel(provinces);
            //TODO: check if switch to separate model solution//Districts = new MultipleProvinceDistrictTreeTHModel(districts);
            DistrictCheckedItems = districts ?? new string[0];
            CaseClassification = caseClassification;
            OrganizationId = organizationId;
            ForbiddenGroups = forbiddenGroups;
        }

        public int ReportModeIndex { get; set; }
        public MultipleDiagnosisModel Diagnoses { get; set; }
        public MultipleRegionTHModel Regions { get; set; }
        public MultipleZoneTHModel Zones { get; set; }
        public MultipleProvinceTHModel Provinces { get; set; }
        //TODO: check if switch to separate model solution//public MultipleProvinceDistrictTreeTHModel Districts { get; set; }
        public string[] DistrictCheckedItems { get; set; }//TODO: old solution should be removed if separate model solution is applied for the new filter//
        public long? CaseClassification { get; set; }
        public virtual string Diagnoses_CheckedItems { get; set; }
        public virtual string ZoneFilter_CheckedItems { get; set; }
        public virtual string RegionFilter_CheckedItems { get; set; }
        public virtual string ProvinceFilter_CheckedItems { get; set; }
        //TODO: check if switch to separate model solution//public virtual string ProvinceDistrictTreeFilter_CheckedItems { get; set; }

        public List<SelectListItemSurrogate> CaseClassificationsList
        {
            get { return FilterHelper.GetCaseClassificationsList(ModelUserContext.CurrentLanguage, (int) HACode.Human, true, true, true); }
        }

        public List<SelectListItemSurrogate> GetReportModeModelList()
        {
            List<SelectListItemSurrogate> result = FilterHelper.GetWebReportModeList();
            return result;
        }

        public override string ToString()
        {
            var district = new StringBuilder();
            if (DistrictCheckedItems != null)
            {
                foreach (string item in DistrictCheckedItems)
                {
                    district.AppendFormat("{0},", item);
                }
            }

            return string.Format("{0}, Regions = {1}, Zones = {2}, Diagnoses={3}, Case Classification={4}, Provinces = {5}, Districts = {6}, Report Mode Index = {7}",
                base.ToString(), Regions, Zones, Diagnoses, CaseClassification, Provinces,
                                    district, //TODO: check if switch to separate model solution//Districts, 
                                    ReportModeIndex);
        }
    }
}