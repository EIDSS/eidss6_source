using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using EIDSS.Reports.BaseControls.Report;
using bv.model.BLToolkit;
using EIDSS.Reports.Parameterized.Human.KZ.Report;
using EIDSS.Reports.BaseControls.Filters;
using eidss.model.Reports.OperationContext;
using eidss.model.Reports.Common;
using bv.winclient.Layout;
using bv.common.Core;
using bv.winclient.Core;
using DevExpress.XtraEditors.Controls;
using EIDSS.Reports.BaseControls.Keeper;
using eidss.model.Reports.KZ;
using eidss.model.Core;
using eidss.model.Resources;

namespace EIDSS.Reports.Parameterized.Human.KZ.Keeper
{
    public partial class ComparativeReportKeeper : EIDSS.Reports.BaseControls.Keeper.BaseReportKeeper
    {
        private readonly ComponentResourceManager _resources = new ComponentResourceManager(typeof(ComparativeReportKeeper));
        private List<ItemWrapper> _monthCollection = null;

        private string ErrorMessage;

        // I could not implement it by means of the forms wizard.
        private BaseControls.Filters.RegionFilter region1Filter = new RegionFilter();
        private BaseControls.Filters.RayonFilter rayon1Filter = new RayonFilter();

        public ComparativeReportKeeper()
        {
            ReportType = typeof(ComparativeReport);
            InitializeComponent();
            using (ContextKeeper.CreateNewContext(ContextValue.ReportFilterLoading))
            {
                SetMandatory();

                Year2SpinEdit.Value = DateTime.Now.Year;
                Year2SpinEdit.Properties.MaxValue = DateTime.Now.Year;

                Year1SpinEdit.Value = DateTime.Now.Year - 1;
                Year1SpinEdit.Properties.MaxValue = DateTime.Now.Year - 1;

                BindLookup(StartMonthLookUp, MonthCollection, StartMonthLabel.Text);
                StartMonthLookUp.EditValue = MonthCollection[0];

                BindLookup(EndMonthLookUp, MonthCollection, EndMonthLabel.Text);
                EndMonthLookUp.EditValue = MonthCollection[DateTime.Now.Month - 1];
            }

            LayoutCorrector.SetStyleController(StartMonthLookUp, LayoutCorrector.MandatoryStyleController);
            LayoutCorrector.SetStyleController(EndMonthLookUp, LayoutCorrector.MandatoryStyleController);

            m_HasLoad = true;

            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(typeof(ComparativeReportKeeper));

            resources.ApplyResources(this.region1Filter, "region1Filter");
            region1Filter.TabIndex = 7;
            region1Filter.Location = new Point(20, 60);
            region1Filter.Size = new Size(200, 40);
            region1Filter.DefineBinding();
            region1Filter.ValueChanged += region1Filter_ValueChanged;
            this.pnlSettings.Controls.Add(region1Filter);

            resources.ApplyResources(this.rayon1Filter, "rayon1Filter");
            rayon1Filter.TabIndex = 8;
            rayon1Filter.Location = new Point(240, 60);
            rayon1Filter.Size = new Size(200, 40);
            rayon1Filter.ValueChanged += rayon1Filter_ValueChanged;
            this.pnlSettings.Controls.Add(rayon1Filter);
        }

        [Browsable(false)]
        protected int Year1Param
        {
            get { return (int)Year1SpinEdit.Value; }
        }

        [Browsable(false)]
        protected int Year2Param
        {
            get { return (int)Year2SpinEdit.Value; }
        }

        [Browsable(false)]
        protected int? StartMonthParam
        {
            get
            {
                return (StartMonthLookUp.EditValue == null)
                    ? (int?)null
                    : ((ItemWrapper)StartMonthLookUp.EditValue).Number;
            }
        }

        [Browsable(false)]
        protected int? EndMonthParam
        {
            get
            {
                return (EndMonthLookUp.EditValue == null)
                    ? (int?)null
                    : ((ItemWrapper)EndMonthLookUp.EditValue).Number;
            }
        }

        protected List<ItemWrapper> MonthCollection
        {
            get { return _monthCollection ?? (_monthCollection = FilterHelper.GetWinMonthList()); }
        }

        public void SetMandatory()
        {
            LayoutCorrector.SetStyleController(Year1SpinEdit, LayoutCorrector.MandatoryStyleController);
            LayoutCorrector.SetStyleController(Year2SpinEdit, LayoutCorrector.MandatoryStyleController);
        }

        protected override BaseReport GenerateReport(DbManagerProxy manager, DbManagerProxy archiveManager)
        {
            long? firstRegionID = region1Filter.RegionId > 0 ? (long?)region1Filter.RegionId : null;
            long? firstRayonID = rayon1Filter.RayonId > 0 ? (long?)rayon1Filter.RayonId : null;

            var model = new ComparativeKZModel(CurrentCulture.ShortName,
                firstRegionID, firstRayonID,
                region1Filter.SelectedText, rayon1Filter.SelectedText,
                Year1Param, Year2Param, StartMonthParam, EndMonthParam,
                Convert.ToInt64(EidssUserContext.User.OrganizationID),
                EidssUserContext.User.ForbiddenPersonalDataGroups,
                UseArchive, String.Empty);

            ComparativeReport report = (ComparativeReport)CreateReportObject();
            report.SetParameters(model, manager, archiveManager);
            return report;
        }

        protected internal override void ApplyResources(DbManagerProxy manager)
        {
            try
            {
                IsResourceLoading = true;
                _monthCollection = null;
                m_HasLoad = false;
                base.ApplyResources(manager);

                ErrorMessage = _resources.GetString("ErrorMessage_Key");

                _resources.ApplyResources(StartYearLabel, "StartYearLabel");
                _resources.ApplyResources(EndYearLabel, "EndYearLabel");
                _resources.ApplyResources(StartMonthLabel, "StartMonthLabel");
                _resources.ApplyResources(EndMonthLabel, "EndMonthLabel");

                ApplyLookupResources(StartMonthLookUp, MonthCollection, StartMonthParam, StartMonthLabel.Text);
                ApplyLookupResources(EndMonthLookUp, MonthCollection, EndMonthParam, EndMonthLabel.Text);

                region1Filter.DefineBinding();
                rayon1Filter.DefineBinding();
                region1Filter.Top = rayon1Filter.Top + 1;
                region1Filter.Caption = EidssFields.Get("Form1KZRegion");

                if (ContextKeeper.ContainsContext(ContextValue.ReportKeeperFirstLoading))
                {
                    LocationHelper.SetDefaultFilters(manager, ContextKeeper, region1Filter, rayon1Filter);
                }
            }
            finally
            {
                m_HasLoad = true;
                IsResourceLoading = false;
            }
        }

        private void MonthLookUp_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            ClearLookUpButtonClick(sender, e, StartMonthLookUp, EndMonthLookUp);
        }

        private void StartMonthLookUp_EditValueChanged(object sender, EventArgs e)
        {
            CorrectLookupRange(StartMonthLookUp, EndMonthLookUp, MonthLookupEnum.Start);
        }

        private void EndMonthLookUp_EditValueChanged(object sender, EventArgs e)
        {
            CorrectLookupRange(StartMonthLookUp, EndMonthLookUp, MonthLookupEnum.End);
        }

        private void region1Filter_ValueChanged(object sender, BaseControls.Filters.SingleFilterEventArgs e)
        {
            rayon1Filter.Enabled = !String.IsNullOrEmpty(region1Filter.SelectedText);

            using (ContextKeeper.CreateNewContext(ContextValue.ReportFilterResetting))
            {
                LocationHelper.RegionFilterValueChanged(region1Filter, rayon1Filter, e);
            }
        }

        private void rayon1Filter_ValueChanged(object sender, BaseControls.Filters.SingleFilterEventArgs e)
        {
            if (ContextKeeper.ContainsContext(ContextValue.ReportFilterResetting))
            {
                return;
            }
            using (ContextKeeper.CreateNewContext(ContextValue.ReportFilterResetting))
            {
                LocationHelper.RayonFilterValueChanged(region1Filter, rayon1Filter, e);
            }
        }

        private void Year1SpinEdit_EditValueChanged(object sender, EventArgs e)
        {
            CorrectYearRange(true);
        }

        private void Year2SpinEdit_EditValueChanged(object sender, EventArgs e)
        {
            CorrectYearRange(false);
        }

        private void CorrectYearRange(bool isYear1)
        {
            if (Year2Param <= Year1Param)
            {
                if (!ContextKeeper.ContainsContext(ContextValue.ReportFilterResetting))
                {
                    if (!Utils.IsReportsServiceRunning && !Utils.IsAvrServiceRunning)
                    {
                        ErrorForm.ShowWarning(ErrorMessage);
                    }
                }
                using (ContextKeeper.CreateNewContext(ContextValue.ReportFilterResetting))
                {
                    if (isYear1)
                    {
                        Year1SpinEdit.EditValue = Year2Param - 1;
                    }
                    else
                    {
                        Year2SpinEdit.EditValue = Year1Param + 1;
                    }
                }
            }
        }
    }
}