using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eidss.model.Reports.Common;
using eidss.model.Enums;
using eidss.model.Core;

namespace eidss.model.Reports.KZ
{
    [Serializable]
    public sealed class ComparativeKZModel : BaseModel
    {
        public ComparativeKZModel()
        {
        }

        public ComparativeKZModel
            (string language, 
            long? regionId, long? rayonId,
            string regionName, string rayonName,
            int year1, int year2, 
            int? startMonth, int? endMonth, 
            long? organizationId, List<PersonalDataGroup> forbiddenGroups, bool useArchive, string exportFormat)
            : base(language, useArchive)
        {
            RegionId = regionId;
            RayonId = rayonId;
            RegionName = regionName;
            RayonName = rayonName;
            Year1 = year1;
            Year2 = year2;
            StartMonth = startMonth;
            EndMonth = endMonth;

            OrganizationId = organizationId;
            ForbiddenGroups = forbiddenGroups;
            ExportFormat = exportFormat;
        }

        public long? RegionId { get; set; }

        public long? RayonId { get; set; }

        private string _regionName = String.Empty;
        public string RegionName
        {
            get { return _regionName; }
            set { _regionName = value ?? String.Empty; }
        }

        private string _rayonName = String.Empty;
        public string RayonName
        {
            get { return _rayonName; }
            set { _rayonName = value ?? String.Empty; }
        }

        public int Year1 { get; set; }
        public int Year2 { get; set; }

        public int? StartMonth { get; set; }
        public int? EndMonth { get; set; }

        public string GenerateName()
        {
            return String.Format("ComparativeReport");
        }
    }
}
