using eidss.model.Core;
using eidss.model.Enums;
using eidss.model.Reports.Common;
using eidss.model.Reports.KZ;
using eidss.model.Resources;
using eidss.model.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eidss.webclient.Models.Reports
{
    [Serializable]
    public class ComparativeReportByRegionKZWebModel : BaseModel
    {
        public ComparativeReportByRegionKZWebModel()
        {
            DiagOrGroupLookup = new List<SelectListItemSurrogate>();
        }

        public ComparativeReportByRegionKZWebModel(long? regionId, long? rayonId)
            :this()
        {
            Address = new AddressModel(regionId, rayonId){ RegionLabelId = " Form1KZRegion" };
        }

        public ComparativeReportByRegionKZWebModel(string language, bool useArchive, 
            int year1, int year2, 
            string diagnosis, int? populationId, string population,
            int? startMonth = null, int? endMonth = null, 
            long? regionId = null, long? rayonId = null) 
            : base(language, useArchive)
        {
            Address = new AddressModel(regionId, rayonId);
            Year1 = year1;
            Year2 = year2;
            StartMonth = startMonth;
            EndMonth = endMonth;
            Diagnosis = diagnosis;
            PopulationId = populationId ?? 1 /*All*/;

            DiagOrGroupLookup = new List<SelectListItemSurrogate>();
        }

        [LocalizedDisplayName("ComparativeReportByRegionYear1")]
        public int Year1 { get; set; }

        [LocalizedDisplayName("ComparativeReportByRegionYear2")]
        public int Year2 { get; set; }

        public AddressModel Address { get; set; }

        #region Population

        public int PopulationId { get; set; }

        [LocalizedDisplayName("ComparativeReportByRegionPopulation")]
        public string Population { get {
                var key = Enum.GetName(typeof(Population), PopulationId);
                return EidssMessages.Instance.GetString(key);
            }
        }

        public List<SelectListItemSurrogate> SelectedPopulations
        {
            get { return FilterHelper.GetPopulationList(); }
        }

        #endregion

        #region Months

        [LocalizedDisplayName("ComparativeReportByRegionFromMonth")]
        public int? StartMonth { get; set; }

        [LocalizedDisplayName("ComparativeReportByRegionToMonth")]
        public int? EndMonth { get; set; }

        public List<SelectListItemSurrogate> UnselectedMonthList
        {
            get {return FilterHelper.GetWebMonthList(DateTime.Now.Month, true);}
        }

        public List<SelectListItemSurrogate> SelectedMonthList
        {
            get { return FilterHelper.GetWebMonthList(1, true); }
        }

        #endregion

        #region Diagnosis

        [LocalizedDisplayName("ComparativeReportByRegionDiagnosis")]
        public string Diagnosis { get; set; }

        public long? idfsDiagnosisOrDiagnosisGroup { get; set; }
        public List<SelectListItemSurrogate> DiagOrGroupLookup { get; set; }
        public long Key
        {
            get { return GetHashCode(); }
        }

        public void LoadDiagnosesList()
        {
            DiagOrGroupLookup = FilterHelper.LoadDiagnosesAndGroups().ToList();
        }

        #endregion

        public static explicit operator ComparativeReportByRegionKZModel(ComparativeReportByRegionKZWebModel model)
        {
            var selectedDiagnosisNames = model.Diagnosis;
            AddressModel address = model.Address;
            var result = new ComparativeReportByRegionKZModel(model.Language,
                model.UseArchive,model.Year1,
                model.Year2, model.Diagnosis, model.PopulationId, model.Population,
                model.idfsDiagnosisOrDiagnosisGroup,
                model.StartMonth, model.EndMonth,
                address.RegionId, address.RayonId)
            {
                ExportFormat = model.ExportFormat,
                IsOpenInNewWindow = model.IsOpenInNewWindow
            };

            return result;
        }
    }
}