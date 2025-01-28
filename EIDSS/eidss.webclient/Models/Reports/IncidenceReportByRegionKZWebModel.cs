using eidss.model.Core;
using eidss.model.Reports.Common;
using eidss.model.Reports.KZ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eidss.webclient.Models.Reports
{
    [Serializable]
    public class IncidenceReportByRegionKZWebModel : BaseModel
    {
        public IncidenceReportByRegionKZWebModel()
        {
            DiagOrGroupLookup = new List<SelectListItemSurrogate>();
        }

        public IncidenceReportByRegionKZWebModel(long? regionId, long? rayonId)
            :this()
        {
            Address = new AddressModel(regionId, rayonId){ RegionLabelId = " Form1KZRegion" };
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

        public static explicit operator IncidenceReportByRegionKZModel(IncidenceReportByRegionKZWebModel model)
        {
            var selectedDiagnosisNames = model.Diagnosis;
            AddressModel address = model.Address;
            var result = new IncidenceReportByRegionKZModel(model.Language,
                model.UseArchive, model.Year, model.Diagnosis,
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