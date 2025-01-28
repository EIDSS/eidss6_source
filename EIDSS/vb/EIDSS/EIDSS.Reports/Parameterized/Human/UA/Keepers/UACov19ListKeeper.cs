using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EIDSS.Reports.BaseControls.Keeper;
using bv.model.BLToolkit;
using EIDSS.Reports.BaseControls.Report;
using EIDSS.Reports.Parameterized.Human.UA.Reports;
using eidss.model.Reports.UA;
using eidss.model.Reports.OperationContext;
using EIDSS.Reports.BaseControls.Filters;
using bv.winclient.Core;
using eidss.model.Resources;
using bv.common.Core;
using bv.model.Model.Core;
using eidss.model.Reports.Common;

namespace EIDSS.Reports.Parameterized.Human.UA.Keepers
{
    public partial class UACov19ListKeeper : BaseIntervalLocationKeeper
    {
        private long? defRegionId;
        private long? defRayonId;
        private long? defSettlementId;

        public UACov19ListKeeper()
        {
            ReportType = typeof(UACov19List);

            using (ContextKeeper.CreateNewContext(ContextValue.ReportFilterLoading))
            {
                try
                {
                    m_HasLoad = false;
                    IsResourceLoading = true;
                    InitializeComponent();
                    FilterHelper.GetDefaultLocation(out defRegionId, out defRayonId, out defSettlementId);
                    InitKeeperControls();
                }
                finally
                {
                    m_HasLoad = true;
                    IsResourceLoading = false;
                }
            }
        }

        private void ConfigureDateTimeEditor(DevExpress.XtraEditors.DateEdit de, DateTime defValue)
        {
            de.Properties.EditMask = "g";
            if (defValue != null)
                de.EditValue = defValue;
            var maxDate = TruncateDate(DateTime.Today).AddDays(1).AddMinutes(-1);
            de.Properties.MinValue = new DateTime(2019, 1, 1);
            de.Properties.MaxValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day).AddDays(1).AddSeconds(-1);
        }

        private void InitKeeperControls()
        {
            regionFilter.DefineBinding();
            rayonFilter.DefineBinding();
            settlementFilter.DefineBinding();

            if (defRegionId.HasValue)
            {
                regionFilter.EditValue = defRegionId.Value;
                if (defRayonId.HasValue)
                {
                    rayonFilter.EditValue = defRayonId.Value;
                    if (defSettlementId.HasValue)
                    {
                        settlementFilter.EditValue = defSettlementId.Value;
                    }
                    else
                    {
                        settlementFilter.EditValue = null;
                    }
                }
                else
                {
                    settlementFilter.EditValue = null;
                    rayonFilter.EditValue = null;
                }
            }
            else
            {
                settlementFilter.EditValue = null;
                rayonFilter.EditValue = null;
                regionFilter.EditValue = null;
            }

            filterCaseClassification.DefineBinding();
            filterOutcome.DefineBinding();

            lblStart.Text = EidssFields.Get("datStartDate");
            lblEnd.Text = EidssFields.Get("datEndDate");

            var defEndDate = TruncateDate(DateTime.Today).AddHours(18);
            var defStartDate = defEndDate.AddDays(-1);
            ConfigureDateTimeEditor(dtEnd, defEndDate);
            ConfigureDateTimeEditor(dtStart, defStartDate);

            filterCaseClassification.ExternalLookupCaption = EidssFields.Get("UACov19Filter_CaseClassification");
            filterCaseClassification.HideClearButton();
            filterOutcome.ExternalLookupCaption = EidssFields.Get("UACov19Filter_Outcome");

            filterCaseClassification.EditValue = 350000000; //Confirmed
            filterOutcome.EditValue = null;
 
            regionFilter.ApplyDefaultFilterLayout();
            rayonFilter.ApplyDefaultFilterLayout();
            settlementFilter.ApplyDefaultFilterLayout();
            filterCaseClassification.ApplyDefaultFilterLayout();
            filterOutcome.ApplyDefaultFilterLayout();

            ApplyDefaultFilterLayout(
                filterCaseClassification.Top,
                regionFilter.Left, regionFilter.Width,
                rayonFilter.Left, rayonFilter.Width - 40,
                settlementFilter.Left, rayonFilter.Width - 40);
        }

        protected internal override void ApplyResources(DbManagerProxy manager)
        {
            using (ContextKeeper.CreateNewContext(ContextValue.ReportFilterLoading))
            {
                try
                {
                    IsResourceLoading = true;
                    m_HasLoad = false;
                    base.ApplyResources(manager);
                    InitKeeperControls();
                }
                finally
                {
                    m_HasLoad = true;
                    IsResourceLoading = false;
                }
            }
        }

        protected override BaseReport GenerateReport(DbManagerProxy manager, DbManagerProxy archiveManager)
        {
            if (WinUtils.IsComponentInDesignMode(this))
            {
                return new BaseReport();
            }

            DateTime startDate;
            DateTime endDate;
            long? regionId = null;
            long? rayonId = null;
            long? settlementId = null;
            long? caseClassification = null;
            long? outcome = null;

            startDate = dtStart.DateTime;
            endDate = dtEnd.DateTime;
            if (regionFilter.EditValueId > 0)
                regionId = regionFilter.EditValueId;
            if (rayonFilter.EditValueId > 0)
                rayonId = rayonFilter.EditValueId;
            if (settlementFilter.EditValueId > 0)
                settlementId = settlementFilter.EditValueId;
            if (filterCaseClassification.EditValueId > 0)
                caseClassification = filterCaseClassification.EditValueId;
            if (filterOutcome.EditValueId > 0)
                outcome = filterOutcome.EditValueId;

            var model = new UACov19ListModel(CurrentCulture.ShortName, startDate, endDate, regionId, rayonId, settlementId, UseArchive);
            model.Language = CurrentCulture.ShortName;
            model.StartDate = startDate;
            model.EndDate = endDate;
            model.CaseClassification = caseClassification;
            model.Outcome = outcome;
            model.UseArchive = UseArchive;

            UACov19List report = (UACov19List)CreateReportObject();
            report.SetParameters(model, manager, archiveManager);

            return report;
        }


    }
}
