using System;
using System.ComponentModel;
using System.Linq;
using bv.common.Core;
using bv.model.BLToolkit;
using bv.model.Model.Core;
using eidss.model.Core;
using eidss.model.Reports.OperationContext;
using eidss.model.Reports.TH;
using EIDSS.Reports.BaseControls.Filters;
using EIDSS.Reports.BaseControls.Keeper;
using EIDSS.Reports.BaseControls.Report;
using EIDSS.Reports.Parameterized.Human.TH.Reports;
using eidss.model.Reports.Common;
using System.Collections.Generic;
using bv.winclient.Core;

namespace EIDSS.Reports.Parameterized.Human.TH.Keepers
{
    public sealed partial class NumberOfCasesDeathsMonthTHReportKeeper : BaseYearKeeper
    {
        private readonly ComponentResourceManager m_Resources =
            new ComponentResourceManager(typeof (NumberOfCasesDeathsMonthTHReportKeeper));

        private const int ProvincesByZonesIndex = 1;
        private const int ProvincesByRegionsIndex = 2;
        private const int DistrictsByProvincesIndex = 3;
        private const int SubDistrictsByDistrictsIndex = 4;

        public string[] m_CheckedDiagnosis = new string[0];
        public string[] m_CheckedZones = new string[0];
        public string[] m_CheckedRegions = new string[0];
        private string[] m_CheckedProvinces = new string[0];
        private string[] m_CheckedProvinceDistricts = new string[0];
        private List<ItemWrapper> m_ReportModeCollection;
        private bool m_IsThaiCulture;
        private int m_Year;

        public NumberOfCasesDeathsMonthTHReportKeeper()
        {
            try
            {
                ReportType = typeof(NumberOfCasesDeathsMonthTHReport);
                InitializeComponent();
                IsResourceLoading = true;
                using (ContextKeeper.CreateNewContext(ContextValue.ReportFilterLoading))
                {
                    m_Year = DateTime.Now.Year;
                    BaseFilter.SetLookupMandatory(ReportModeLookUp);
                    BindLookup(ReportModeLookUp, ReportModeCollection, ReportModeLabel.Text);
                    
                    ApplyYearResources();
                    //todo: [ivan] extract method
                    ProvinceFilter.Left = ZonesFilter.Left;
                    ProvinceFilter.Width = ZonesFilter.Width;

                    RegionFilter.Left = ZonesFilter.Left;
                    RegionFilter.Width = ZonesFilter.Width;

                    ProvinceDistrictMultiFilter.Width = ZonesFilter.Width;
                    ProvinceDistrictMultiFilter.Left = ZonesFilter.Left;
                }
            }
            finally
            {
                IsResourceLoading = false;
                m_HasLoad = true;
            }
        }

        [Browsable(false)]
        private int? ReportModeParam
        {
            get
            {
                return (ReportModeLookUp.EditValue == null)
                    ? (int?)null
                    : ((ItemWrapper)ReportModeLookUp.EditValue).Number;
            }
        }

        [Browsable(false)]
        private int ReportModeIndex
        {
            get
            {
                var wrapper = ReportModeLookUp.EditValue as ItemWrapper;
                int type = (wrapper != null)
                    ? wrapper.Number
                    : 0;
                return type;
            }
        }

        [Browsable(false)]
        private long? CaseClassification
        {
            get { return CaseClassificationFilter.EditValueId > 0 ? (long?) CaseClassificationFilter.EditValueId : null; }
        }
        [Browsable(false)]
        private int DeltaYear
        {
            get { return m_IsThaiCulture ? 543 : 0; }
        }
        [Browsable(false)]
        private List<ItemWrapper> ReportModeCollection
        {
            get { return m_ReportModeCollection ?? (m_ReportModeCollection = FilterHelper.GetWinReportModeList()); }
        }

        private void DiagnosisFilter_ValueChanged(object sender, MultiFilterEventArgs e)
        {
            m_CheckedDiagnosis = e.KeyArray.ToArray();
        }

        private void ZonesFilter_ValueChanged(object sender, MultiFilterEventArgs e)
        {
            m_CheckedZones = e.KeyArray.ToArray();
        }

        private void RegionFilter_ValueChanged(object sender, MultiFilterEventArgs e)
        {
            m_CheckedRegions = e.KeyArray.ToArray();
        }

        private void ProvinceFilter_ValueChanged(object sender, MultiFilterEventArgs e)
        {
            m_CheckedProvinces = e.KeyArray;
        }

        private void ProvinceDistrictMultiFilter_ValueChanged(object sender, MultiFilterEventArgs e)
        {
            m_CheckedProvinceDistricts = e.KeyArray;
        }
        private void ReportModeLookUp_EditValueChanged(object sender, EventArgs e)
        {
            ZonesFilter.Visible = ReportModeIndex == ProvincesByZonesIndex;
            RegionFilter.Visible = ReportModeIndex == ProvincesByRegionsIndex;
            ProvinceFilter.Visible = ReportModeIndex == DistrictsByProvincesIndex;
            ProvinceDistrictMultiFilter.Visible = ReportModeIndex == SubDistrictsByDistrictsIndex;

            ZonesFilter.ClearSelection();
            RegionFilter.ClearSelection();
            ProvinceFilter.ClearSelection();
            ProvinceDistrictMultiFilter.ClearSelection();

            m_CheckedZones = new string[0];
            m_CheckedRegions = new string[0];
            m_CheckedProvinces = new string[0];
            m_CheckedProvinceDistricts = new string[0];
        }

        protected override bool CheckBusinessRules(bool printException)
        {
            if (m_CheckedProvinceDistricts.Length > NumberOfCasesDeathsMonthTHModel.MaxNumberOfDistrict)
            {
                const string defaultFormat =
                    "Too many districts are selected for displaying in the report. The number of selected districts shall not exceed {0}. Please clear some districts and try to generate the report again.";
                ErrorForm.ShowWarningFormat("msgTooManyDistrictsThaiReports", defaultFormat, NumberOfCasesDeathsMonthTHModel.MaxNumberOfDistrict);
                return false;
            }
            if (m_CheckedDiagnosis.Length > NumberOfCasesDeathsMonthTHModel.MaxNumberOfDiagnosis)
            {
                const string defaultFormat =
                    "Too many diagnoses are selected for displaying in the report. The number of selected diagnoses shall not exceed {0}. Please clear some diagnoses and try to generate the report again.";
                ErrorForm.ShowWarningFormat("msgTooManyDiagnosisThaiReports", defaultFormat, NumberOfCasesDeathsMonthTHModel.MaxNumberOfDiagnosis);
                return false;
            }
            return true;
        }

        protected override BaseReport GenerateReport(DbManagerProxy manager, DbManagerProxy archiveManager)
        {
            var model = new NumberOfCasesDeathsMonthTHModel(CurrentCulture.ShortName,
                m_Year, ReportModeIndex, 
                m_CheckedDiagnosis, m_CheckedRegions, m_CheckedZones, m_CheckedProvinces, m_CheckedProvinceDistricts,
                CaseClassification,
                Convert.ToInt64(EidssUserContext.User.OrganizationID),
                EidssUserContext.User.ForbiddenPersonalDataGroups,
                UseArchive);

            dynamic report = CreateReportObject();
            report.SetParameters(model, manager, archiveManager);
            return (BaseReport) report;
        }

        protected override void OnBeforeYearChange()
        {
            if (!ContextKeeper.ContainsContext(ContextValue.ReportFilterResetting))
            {
                m_Year = YearParam - DeltaYear;
            }
        }

        protected internal override void ApplyResources(DbManagerProxy manager)
        {
            base.ApplyResources(manager);
            m_IsThaiCulture = ModelUserContext.CurrentLanguage == Localizer.lngThai;
            m_ReportModeCollection = null;

            m_Resources.ApplyResources(pnlSettings, "pnlSettings");
            
            ReportModeLabel.Text = m_Resources.GetString("ReportModeLabel.Text");
            ApplyLookupResources(ReportModeLookUp, ReportModeCollection, ReportModeParam, ReportModeLabel.Text);

            ApplyYearResources();
            DiagnosesFilter.DefineBinding();
            RegionFilter.DefineBinding();
            ZonesFilter.DefineBinding();
            CaseClassificationFilter.DefineBinding();
            ProvinceFilter.DefineBinding();
            ProvinceDistrictMultiFilter.DefineBinding();
        }

        private void ApplyYearResources()
        {
            using (ContextKeeper.CreateNewContext(ContextValue.ReportFilterResetting))
            {
                MaxYear = DeltaYear + DateTime.Now.Year;
                MinYear = m_IsThaiCulture ? 2543 : 2000;
                YearParam = m_Year + DeltaYear;
            }
        }

    }
}