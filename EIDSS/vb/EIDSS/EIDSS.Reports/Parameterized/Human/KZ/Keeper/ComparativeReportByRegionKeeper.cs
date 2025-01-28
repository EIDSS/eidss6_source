using System;
using System.Collections.Generic;
using System.ComponentModel;
using EIDSS.Reports.BaseControls.Report;
using bv.model.BLToolkit;
using EIDSS.Reports.Parameterized.Human.KZ.Report;
using EIDSS.Reports.BaseControls.Filters;
using eidss.model.Reports.OperationContext;
using eidss.model.Reports.Common;
using bv.winclient.Layout;
using bv.common.Core;
using DevExpress.XtraEditors.Controls;
using EIDSS.Reports.BaseControls.Keeper;
using eidss.model.Reports.KZ;
using bv.winclient.Core;
using eidss.model.Resources;

namespace EIDSS.Reports.Parameterized.Human.KZ.Keeper
{
    public partial class ComparativeReportByRegionKeeper : EIDSS.Reports.BaseControls.Keeper.BaseReportKeeper
    {
        private readonly ComponentResourceManager m_Resources = new ComponentResourceManager(typeof(ComparativeReportByRegionKeeper));
        private List<ItemWrapper> m_MonthCollection = null;
        private List<ItemWrapper> m_PopulationList = null;

        public ComparativeReportByRegionKeeper()
        {
            ReportType = typeof(ComparativeReportByRegion);

            InitializeComponent();

            using (ContextKeeper.CreateNewContext(ContextValue.ReportFilterLoading))
            {
                SetMandatory();

                DateTime dtNow = DateTime.Now;

                Year2SpinEdit.Value = dtNow.Year;
                Year2SpinEdit.Properties.MaxValue = dtNow.Year;

                Year1SpinEdit.Value = dtNow.Year - 1;
                Year1SpinEdit.Properties.MaxValue = dtNow.Year - 1;

                FillPopulation();
                BindLookup(populationFilter, m_PopulationList, populationLabel.Text);
                populationFilter.EditValue = m_PopulationList[0];

                BindLookup(StartMonthLookUp, MonthCollection, StartMonthLabel.Text);
                StartMonthLookUp.EditValue = MonthCollection[0];

                BindLookup(EndMonthLookUp, MonthCollection, EndMonthLabel.Text);
                EndMonthLookUp.EditValue = MonthCollection[dtNow.Month - 1];
            }

            m_HasLoad = true;
        }

        private void FillPopulation()
        {
            m_PopulationList = new List<ItemWrapper>();
            var localizedPopulation = FilterHelper.GetPopulationList();
            for (int i = 0; i < localizedPopulation.Count; i++)
            {
                m_PopulationList.Add(new ItemWrapper(localizedPopulation[i].Text, i + 1));
            }
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
            get { return m_MonthCollection ?? (m_MonthCollection = FilterHelper.GetWinMonthList()); }
        }

        [Browsable(false)]
        protected int? PopulationParam
        {
            get
            {
                return (populationFilter.EditValue == null)
                    ? (int?)null
                    : ((ItemWrapper)populationFilter.EditValue).Number;
            }
        }

        public void SetMandatory()
        {
            LayoutCorrector.SetStyleController(Year1SpinEdit, LayoutCorrector.MandatoryStyleController);
            LayoutCorrector.SetStyleController(Year2SpinEdit, LayoutCorrector.MandatoryStyleController);
            LayoutCorrector.SetStyleController(populationFilter, LayoutCorrector.MandatoryStyleController);
            LayoutCorrector.SetStyleController(StartMonthLookUp, LayoutCorrector.MandatoryStyleController);
            LayoutCorrector.SetStyleController(EndMonthLookUp, LayoutCorrector.MandatoryStyleController);
            LayoutCorrector.SetStyleController(DiagnosisFilter, LayoutCorrector.MandatoryStyleController);
        }

        protected override BaseReport GenerateReport(DbManagerProxy manager, DbManagerProxy archiveManager)
        {
            long? firstRegionID = region1Filter.RegionId > 0 ? (long?)region1Filter.RegionId : null;
            long? firstRayonID = rayon1Filter.RayonId > 0 ? (long?)rayon1Filter.RayonId : null;

            var model = new ComparativeReportByRegionKZModel(
                CurrentCulture.ShortName,
                UseArchive,
                Year1Param,
                Year2Param,
                DiagnosisFilter.Text,
                PopulationParam ?? 1 /*All*/,
                populationFilter.Text,
                (long)DiagnosisFilter.EditValue,
                StartMonthParam,
                EndMonthParam,
                firstRegionID,
                firstRayonID);

            ComparativeReportByRegion report = (ComparativeReportByRegion)CreateReportObject();
            report.SetParameters(model, manager, archiveManager);
            return report;
        }

        protected internal override void ApplyResources(DbManagerProxy manager)
        {
            try
            {
                IsResourceLoading = true;
                m_MonthCollection = null;
                m_HasLoad = false;

                base.ApplyResources(manager);

                m_Resources.ApplyResources(StartYearLabel, "StartYearLabel");
                m_Resources.ApplyResources(EndYearLabel, "EndYearLabel");
                m_Resources.ApplyResources(StartMonthLabel, "StartMonthLabel");
                m_Resources.ApplyResources(EndMonthLabel, "EndMonthLabel");
                m_Resources.ApplyResources(DiagnosisLabel, "DiagnosisLabel");

                m_Resources.ApplyResources(populationLabel, "populationLabel");

                ApplyLookupResources(StartMonthLookUp, MonthCollection, StartMonthParam, StartMonthLabel.Text);
                ApplyLookupResources(EndMonthLookUp, MonthCollection, EndMonthParam, EndMonthLabel.Text);

                FillPopulation();
                ApplyLookupResources(populationFilter, m_PopulationList, PopulationParam, populationLabel.Text);

                region1Filter.DefineBinding();
                rayon1Filter.DefineBinding();
                region1Filter.Top = rayon1Filter.Top + 1;
                region1Filter.Caption = EidssFields.Get("Form1KZRegion");

                if (ContextKeeper.ContainsContext(ContextValue.ReportKeeperFirstLoading))
                {
                    LocationHelper.SetDefaultFilters(manager, ContextKeeper, region1Filter, rayon1Filter);
                }

                object oldEditValue = DiagnosisFilter.EditValue;
                Core.LookupBinder.BindDiagnosisTreeLookup(DiagnosisFilter, null, String.Empty);
                DiagnosisFilter.EditValue = oldEditValue;
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
                        ErrorForm.ShowWarning("msgComparativeReportKZCorrectYear");
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