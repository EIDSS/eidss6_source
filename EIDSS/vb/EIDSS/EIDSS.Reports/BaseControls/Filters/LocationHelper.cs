using bv.model.BLToolkit;
using eidss.model.Reports.Common;
using eidss.model.Reports.OperationContext;

namespace EIDSS.Reports.BaseControls.Filters
{
    public static class LocationHelper
    {
        public static void SetDefaultFilters
            (DbManagerProxy manager, IContextKeeper context,
            RegionFilter regionFilter = null, RayonFilter rayonFilter = null, 
            bool hasChe = false, SettlementFilter settlementFilter = null)
        {
            if (context.ContainsContext(ContextValue.ReportFilterResetting))
            {
                return;
            }

            using (context.CreateNewContext(ContextValue.ReportFilterLoading))
            {
                long? regionId;
                long? rayonId;
                long? settlementId;
                FilterHelper.GetDefaultLocation(out regionId, out rayonId, out settlementId, hasChe);

                if (regionFilter != null)
                {
                    if (regionId.HasValue && regionFilter.RegionId < 0)
                    {
                        regionFilter.RegionId = regionId.Value;
                    }
                }
                if (rayonFilter != null)
                {
                    if (rayonId.HasValue && rayonFilter.RayonId < 0)
                    {
                        rayonFilter.RayonId = rayonId.Value;
                    }
                }
                if (settlementFilter != null)
                {
                    if (settlementId.HasValue && settlementFilter.SettlementId < 0)
                    {
                        settlementFilter.SettlementId = settlementId.Value;
                    }
                }
            }
        }

        //TODO: add optional settlementFilter
        public static void RegionFilterValueChanged(RegionFilter regionFilter, RayonFilter rayonFilter, SingleFilterEventArgs e)
        {
            if ((regionFilter.RegionId == -1) || (regionFilter.RegionId != rayonFilter.RegionId))
            {
                rayonFilter.RayonId = -1;
            }
            rayonFilter.RegionId = regionFilter.RegionId;
        }

        //TODO: add optional settlementFilter
        public static void RayonFilterValueChanged(RegionFilter regionFilter, RayonFilter rayonFilter, SingleFilterEventArgs e)
        {
            rayonFilter.RegionId = rayonFilter.RegionId;
            if ((rayonFilter.RegionId != -1) && (regionFilter.RegionId != rayonFilter.RegionId))
            {
                regionFilter.RegionId = rayonFilter.RegionId;
            }
        }
        //TODO:SettlementFilterValueChanged
    }
}