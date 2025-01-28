using System;
using System.Collections.Generic;
using bv.model.Model.Core;
using eidss.model.Enums;
using eidss.model.Reports.Common;
using eidss.model.Reports.TH;
using eidss.model.Resources;
using eidss.model.Schema;

namespace eidss.webclient.Models.Reports
{
    [Serializable]
    public class NumberOfCasesDeathsMonthTHWebModel : NumberOfCasesDeathsMonthTHModel
    {
        public NumberOfCasesDeathsMonthTHWebModel()
        {
            Language = ModelUserContext.CurrentLanguage;
            Diagnoses.AddSelectAllItem = true;
            ProvinceDistrictTreeLookup = new List<ThaiProvinceDistrictTreeLookup>();
            //LoadProvinceDistrictTree();
        }
        public NumberOfCasesDeathsMonthTHWebModel
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
            : base(lang, year, reportModeIndex, diagnoses, regions, zones, provinces, districts, caseClassification, organizationId, forbiddenGroups, useArchive)
        {
            Language = ModelUserContext.CurrentLanguage;
            Diagnoses.AddSelectAllItem = true;
            ProvinceDistrictTreeLookup = new List<ThaiProvinceDistrictTreeLookup>();
        }
        public override string Diagnoses_CheckedItems { get; set; }
        public override string ZoneFilter_CheckedItems { get; set; }
        public override string RegionFilter_CheckedItems { get; set; }
        public override string ProvinceFilter_CheckedItems { get; set; }

        //multiple province district tree support properties
        //public override string ProvinceDistrictTreeFilter_CheckedItems { get; set; }
        public string SelectedDistricts { get; set; }
        public List<ThaiProvinceDistrictTreeLookup> ProvinceDistrictTreeLookup { get; set; }

        public void LoadProvinceDistrictTree()
        {
            FilterHelper.LoadProvinceDistrictTreeLookup(ProvinceDistrictTreeLookup, null, null);
        }

        public NumberOfCasesDeathsMonthTHModel ConvertToBaseModel()
        {
            string[] districtCheckedItems = null;
            if (!string.IsNullOrEmpty(this.SelectedDistricts))
                districtCheckedItems = this.SelectedDistricts.Split(new[] { "," },
                    StringSplitOptions.RemoveEmptyEntries);

            var baseModel = new NumberOfCasesDeathsMonthTHModel(Language, Year,
                ReportModeIndex,
                Diagnoses.CheckedItems,
                Regions.CheckedItems,
                Zones.CheckedItems,
                Provinces.CheckedItems,
                districtCheckedItems,
                CaseClassification,
                OrganizationId, ForbiddenGroups, UseArchive);

            // Add a copy of the export options chosen
            baseModel.ExportFormat = this.ExportFormat;

            return baseModel;
        }

        public long Key
        {
            get { return GetHashCode(); }
        }
    }
}