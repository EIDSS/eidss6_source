
using System;
using System.Collections.Generic;
using eidss.model.Reports.Common;
using eidss.model.Core;
using eidss.model.Reports.KZ;

namespace eidss.webclient.Models.Reports
{
    [Serializable]
    public class HumanComparativeKZReportModel : BaseModel
    {
        public HumanComparativeKZReportModel()
        {

        }

        public HumanComparativeKZReportModel(long? regionId, long? rayonId)
        {
            Address = new AddressModel(regionId, rayonId) { RegionLabelId = " Form1KZRegion" };
        }

        public HumanComparativeKZReportModel(string language, bool useArchive,
            int year1, int year2,
            string diagnosis, string population,
            int? startMonth = null, int? endMonth = null,
            long? regionId = null, long? rayonId = null)
            : base(language, useArchive)
        {
            Address = new AddressModel(regionId, rayonId);
            Year1 = year1;
            Year2 = year2;
            StartMonth = startMonth;
            EndMonth = endMonth;
        }

        [LocalizedDisplayName("ComparativeReportByRegionYear1")]
        public int Year1 { get; set; }

        [LocalizedDisplayName("ComparativeReportByRegionYear2")]
        public int Year2 { get; set; }

        public AddressModel Address { get; set; }

        #region Months

        [LocalizedDisplayName("ComparativeReportByRegionFromMonth")]
        public int? StartMonth { get; set; }

        [LocalizedDisplayName("ComparativeReportByRegionToMonth")]
        public int? EndMonth { get; set; }

        public List<SelectListItemSurrogate> UnselectedMonthList
        {
            get { return FilterHelper.GetWebMonthList(DateTime.Now.Month, true); }
        }

        public List<SelectListItemSurrogate> SelectedMonthList
        {
            get { return FilterHelper.GetWebMonthList(1, true); }
        }

        #endregion

        public static explicit operator ComparativeKZModel(HumanComparativeKZReportModel model)
        {
            return new ComparativeKZModel(
                model.Language,
                model.Address.RegionId,
                model.Address.RayonId,
                model.Address.RegionName(model.Language),
                model.Address.RayonName(model.Language),
                model.Year1,
                model.Year2,
                model.StartMonth,
                model.EndMonth,
                model.OrganizationId,
                model.ForbiddenGroups,
                model.UseArchive,
                model.ExportFormat);
        }
    }
}