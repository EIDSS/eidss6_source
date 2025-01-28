using System;
using System.ComponentModel;
using EIDSS.Reports.BaseControls.Keeper;
using bv.model.BLToolkit;
using EIDSS.Reports.BaseControls.Report;
using EIDSS.Reports.Parameterized.Human.UA.Reports;
using eidss.model.Reports.UA;
using eidss.model.Reports.OperationContext;
using EIDSS.Reports.BaseControls.Filters;
using bv.winclient.Core;

namespace EIDSS.Reports.Parameterized.Human.UA.Keepers
{
    public partial class UAFormNo2Keeper : BaseDateKeeper
    {
        internal UAFormNo2Keeper()
        {
            ReportType = typeof(FormNum2);
            cbMonth.Visible = false;
            label2.Visible = false;

            InitializeComponent();
            SetCurrentStartMonth();
            SetMandatory();

            MinYear = 2010;
            MaxYear = DateTime.Now.Year;
        }

        [Browsable(false)]
        private long? RegionID
        {
            get { return regionFilter.RegionId > 0 ? (long?)regionFilter.RegionId : null; }
        }

        protected override BaseReport GenerateReport(DbManagerProxy manager, DbManagerProxy archiveManager)
        {
            if (WinUtils.IsComponentInDesignMode(this))
            {
                return new BaseReport();
            }

            FormNum2 report = (FormNum2)CreateReportObject();

            UAFormModel model = new UAFormModel(RegionID);

            model.Language = CurrentCulture.ShortName;
            model.Year = YearParam;
            model.Month = StartMonthParam;
            model.UseArchive = UseArchive;

            report.SetParameters(model, manager, archiveManager);

            return report;
        }

        protected internal override void ApplyResources(DbManagerProxy manager)
        {
            base.ApplyResources(manager);

            regionFilter.DefineBinding();

            if (ContextKeeper.ContainsContext(ContextValue.ReportKeeperFirstLoading))
            {
                LocationHelper.SetDefaultFilters(manager, ContextKeeper, regionFilter);
            }
        }
    }
}
