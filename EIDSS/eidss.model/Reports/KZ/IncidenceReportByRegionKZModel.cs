using eidss.model.Core;
using eidss.model.Reports.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eidss.model.Reports.KZ
{
    [Serializable]
    public class IncidenceReportByRegionKZModel : BaseModel
    {
        public IncidenceReportByRegionKZModel()
        {

        }

        public IncidenceReportByRegionKZModel(long? regionId, long? rayonId)
        {
            Address = new AddressModel(regionId, rayonId){ RegionLabelId = " Form1KZRegion" };
        }

        public IncidenceReportByRegionKZModel(string language, bool useArchive, 
            int year,
            string Diagnosis,
            long? IdfsDiagnosisOrDiagnosisGroup,
            int? startMonth = null, int? endMonth = null, 
            long? regionId = null, long? rayonId = null) 
            : base(language, useArchive)
        {
            Address = new AddressModel(regionId, rayonId);
            Year = year;
            StartMonth = startMonth;
            EndMonth = endMonth;
            idfsDiagnosisOrDiagnosisGroup = IdfsDiagnosisOrDiagnosisGroup;
        }

        [LocalizedDisplayName("YearForAggr")]
        public int Year { get; set; }

        public AddressModel Address { get; set; }

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
}
