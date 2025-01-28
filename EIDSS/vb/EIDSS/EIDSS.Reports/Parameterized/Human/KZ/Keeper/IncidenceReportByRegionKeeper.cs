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
    public partial class IncidenceReportByRegionKeeper : EIDSS.Reports.BaseControls.Keeper.BaseReportKeeper
    {
        private readonly ComponentResourceManager m_Resources = new ComponentResourceManager(typeof(IncidenceReportByRegionKeeper));
        private List<ItemWrapper> m_MonthCollection = null;
        private string m_Diagnosis;

        public IncidenceReportByRegionKeeper()
        {
            ReportType = typeof(IncidenceReportByRegion);
            InitializeComponent();
            using (ContextKeeper.CreateNewContext(ContextValue.ReportFilterLoading))
            {
                SetMandatory();

                Year1SpinEdit.Value = DateTime.Now.Year;
                Year1SpinEdit.Properties.MaxValue = DateTime.Now.Year;
                Year1SpinEdit.Properties.MinValue = 2000;

                BindLookup(StartMonthLookUp, MonthCollection, StartMonthLabel.Text);
                StartMonthLookUp.EditValue = MonthCollection[0];

                BindLookup(EndMonthLookUp, MonthCollection, EndMonthLabel.Text);
                EndMonthLookUp.EditValue = MonthCollection[DateTime.Now.Month - 1];
            }

            m_HasLoad = true;
        }

        [Browsable(false)]
        protected int Year1Param
        {
            get { return (int)Year1SpinEdit.Value; }
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

        public void SetMandatory()
        {
            LayoutCorrector.SetStyleController(Year1SpinEdit, LayoutCorrector.MandatoryStyleController);
            LayoutCorrector.SetStyleController(StartMonthLookUp, LayoutCorrector.MandatoryStyleController);
            LayoutCorrector.SetStyleController(EndMonthLookUp, LayoutCorrector.MandatoryStyleController);
            LayoutCorrector.SetStyleController(DiagnosisFilter, LayoutCorrector.MandatoryStyleController);
        }

        protected override BaseReport GenerateReport(DbManagerProxy manager, DbManagerProxy archiveManager)
        {
            long? firstRegionID = region1Filter.RegionId > 0 ? (long?)region1Filter.RegionId : null;
            long? firstRayonID = rayon1Filter.RayonId > 0 ? (long?)rayon1Filter.RayonId : null;

            var model = new IncidenceReportByRegionKZModel(CurrentCulture.ShortName,
                UseArchive, Year1Param,
                DiagnosisFilter.Text,
                (long)DiagnosisFilter.EditValue,
                StartMonthParam, EndMonthParam,
                firstRegionID, firstRayonID);

            IncidenceReportByRegion report = (IncidenceReportByRegion)CreateReportObject();
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

                m_Resources.ApplyResources(YearLabel, "YearLabel");
                m_Resources.ApplyResources(StartMonthLabel, "StartMonthLabel");
                m_Resources.ApplyResources(EndMonthLabel, "EndMonthLabel");
                m_Resources.ApplyResources(DiagnosisLabel, "DiagnosisLabel");

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

        private void DiagnosisFilter_ValueChanged(object sender, MultiFilterEventArgs e)
        {
            m_Diagnosis = e.TextResult;
        }
    }
}