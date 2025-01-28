using eidss.model.Core;
using eidss.model.Reports.Common;
using eidss.model.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eidss.model.Reports.KZ
{
    [Serializable]
    public class ComparativeReportByRegionKZModel : BaseModel
    {
        public ComparativeReportByRegionKZModel()
        {

        }

        public ComparativeReportByRegionKZModel(long? regionId, long? rayonId)
        {
            Address = new AddressModel(regionId, rayonId){ RegionLabelId = " Form1KZRegion" };
        }

        public ComparativeReportByRegionKZModel(string language, bool useArchive, 
            int year1, int year2, 
            string diagnosis, int populationId, string population,
            long? IdfsDiagnosisOrDiagnosisGroup,
            int? startMonth = null, int? endMonth = null, 
            long? regionId = null, long? rayonId = null) 
            : base(language, useArchive)
        {
            Address = new AddressModel(regionId, rayonId);
            Year1 = year1;
            Year2 = year2;
            StartMonth = startMonth;
            EndMonth = endMonth;
            PopulationId = populationId;
            Population = population;
            idfsDiagnosisOrDiagnosisGroup = IdfsDiagnosisOrDiagnosisGroup;
            Diagnosis = diagnosis;
        }

        [LocalizedDisplayName("ComparativeReportByRegionYear1")]
        public int Year1 { get; set; }

        [LocalizedDisplayName("ComparativeReportByRegionYear2")]
        public int Year2 { get; set; }

        public AddressModel Address { get; set; }

        #region Population

        public int PopulationId { get; set; }

        [LocalizedDisplayName("ComparativeReportByRegionPopulation")]
        public string Population { get; set; }

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
            get { return FilterHelper.GetWebMonthList(-1, true); }
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
        #endregion
    }

    public enum Population 
    {
        All = 1,
        Town = 2,
        Rural
    }
}
