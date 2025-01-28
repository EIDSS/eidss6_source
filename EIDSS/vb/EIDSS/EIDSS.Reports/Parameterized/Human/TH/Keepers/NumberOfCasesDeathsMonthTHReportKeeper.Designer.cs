using EIDSS.Reports.BaseControls.Filters;

namespace EIDSS.Reports.Parameterized.Human.TH.Keepers
{
    partial class NumberOfCasesDeathsMonthTHReportKeeper
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any m_Resources being used.
        /// </summary>
        /// <param name="disposing">true if managed m_Resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NumberOfCasesDeathsMonthTHReportKeeper));
            this.DiagnosesFilter = new EIDSS.Reports.BaseControls.Filters.HumDiagnosisMultiFilter();
            this.RegionFilter = new EIDSS.Reports.BaseControls.Filters.ThaiRegionMultiFilter();
            this.ZonesFilter = new EIDSS.Reports.BaseControls.Filters.ThaiZonesMultiFilter();
            this.CaseClassificationFilter = new EIDSS.Reports.BaseControls.Filters.ThaiCaseClassificationFilter();
            this.ProvinceFilter = new EIDSS.Reports.BaseControls.Filters.ThaiProvinceMultiFilter();
            this.ProvinceDistrictMultiFilter = new EIDSS.Reports.BaseControls.Filters.ThaiProvinceDistrictsGroupsMultiFilter();
            this.ReportModeLookUp = new DevExpress.XtraEditors.LookUpEdit();
            this.ReportModeLabel = new System.Windows.Forms.Label();
            this.pnlSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceUseArchiveData.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportModeLookUp.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSettings
            // 
            this.pnlSettings.Controls.Add(this.ReportModeLookUp);
            this.pnlSettings.Controls.Add(this.ReportModeLabel);
            this.pnlSettings.Controls.Add(this.ProvinceDistrictMultiFilter);
            this.pnlSettings.Controls.Add(this.ProvinceFilter);
            this.pnlSettings.Controls.Add(this.CaseClassificationFilter);
            this.pnlSettings.Controls.Add(this.ZonesFilter);
            this.pnlSettings.Controls.Add(this.RegionFilter);
            this.pnlSettings.Controls.Add(this.DiagnosesFilter);
            resources.ApplyResources(this.pnlSettings, "pnlSettings");
            this.pnlSettings.Controls.SetChildIndex(this.GenerateReportButton, 0);
            this.pnlSettings.Controls.SetChildIndex(this.ceUseArchiveData, 0);
            this.pnlSettings.Controls.SetChildIndex(this.DiagnosesFilter, 0);
            this.pnlSettings.Controls.SetChildIndex(this.RegionFilter, 0);
            this.pnlSettings.Controls.SetChildIndex(this.ZonesFilter, 0);
            this.pnlSettings.Controls.SetChildIndex(this.CaseClassificationFilter, 0);
            this.pnlSettings.Controls.SetChildIndex(this.ProvinceFilter, 0);
            this.pnlSettings.Controls.SetChildIndex(this.ProvinceDistrictMultiFilter, 0);
            this.pnlSettings.Controls.SetChildIndex(this.ReportModeLabel, 0);
            this.pnlSettings.Controls.SetChildIndex(this.ReportModeLookUp, 0);
            // 
            // ceUseArchiveData
            // 
            resources.ApplyResources(this.ceUseArchiveData, "ceUseArchiveData");
            this.ceUseArchiveData.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("ceUseArchiveData.Properties.Appearance.Font")));
            this.ceUseArchiveData.Properties.Appearance.Options.UseFont = true;
            this.ceUseArchiveData.Properties.AppearanceDisabled.Options.UseFont = true;
            this.ceUseArchiveData.Properties.AppearanceFocused.Options.UseFont = true;
            this.ceUseArchiveData.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.ceUseArchiveData.Properties.Caption = resources.GetString("ceUseArchiveData.Properties.Caption");
            // 
            // GenerateReportButton
            // 
            this.GenerateReportButton.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("GenerateReportButton.Appearance.Font")));
            this.GenerateReportButton.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.GenerateReportButton, "GenerateReportButton");
            bv.common.Resources.BvResourceManagerChanger.GetResourceManager(typeof(NumberOfCasesDeathsMonthTHReportKeeper), out resources);
            // Form Is Localizable: True
            // 
            // DiagnosesFilter
            // 
            this.DiagnosesFilter.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("DiagnosesFilter.Appearance.Font")));
            this.DiagnosesFilter.Appearance.Options.UseFont = true;
            this.DiagnosesFilter.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            resources.ApplyResources(this.DiagnosesFilter, "DiagnosesFilter");
            this.DiagnosesFilter.Name = "DiagnosesFilter";
            this.DiagnosesFilter.ValueChanged += new System.EventHandler<EIDSS.Reports.BaseControls.Filters.MultiFilterEventArgs>(this.DiagnosisFilter_ValueChanged);
            // 
            // RegionFilter
            // 
            this.RegionFilter.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("RegionFilter.Appearance.Font")));
            this.RegionFilter.Appearance.Options.UseFont = true;
            this.RegionFilter.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            resources.ApplyResources(this.RegionFilter, "RegionFilter");
            this.RegionFilter.Name = "RegionFilter";
            this.RegionFilter.ValueChanged += new System.EventHandler<EIDSS.Reports.BaseControls.Filters.MultiFilterEventArgs>(this.RegionFilter_ValueChanged);
            // 
            // ZonesFilter
            // 
            this.ZonesFilter.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("ZonesFilter.Appearance.Font")));
            this.ZonesFilter.Appearance.Options.UseFont = true;
            this.ZonesFilter.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            resources.ApplyResources(this.ZonesFilter, "ZonesFilter");
            this.ZonesFilter.Name = "ZonesFilter";
            this.ZonesFilter.ValueChanged += new System.EventHandler<EIDSS.Reports.BaseControls.Filters.MultiFilterEventArgs>(this.ZonesFilter_ValueChanged);
            // 
            // CaseClassificationFilter
            // 
            this.CaseClassificationFilter.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("CaseClassificationFilter.Appearance.Font")));
            this.CaseClassificationFilter.Appearance.Options.UseFont = true;
            this.CaseClassificationFilter.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            resources.ApplyResources(this.CaseClassificationFilter, "CaseClassificationFilter");
            this.CaseClassificationFilter.Name = "CaseClassificationFilter";
            // 
            // ProvinceFilter
            // 
            this.ProvinceFilter.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("ProvinceFilter.Appearance.Font")));
            this.ProvinceFilter.Appearance.Options.UseFont = true;
            this.ProvinceFilter.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            resources.ApplyResources(this.ProvinceFilter, "ProvinceFilter");
            this.ProvinceFilter.Name = "ProvinceFilter";
            this.ProvinceFilter.ValueChanged += new System.EventHandler<EIDSS.Reports.BaseControls.Filters.MultiFilterEventArgs>(this.ProvinceFilter_ValueChanged);
            // 
            // ProvinceDistrictMultiFilter
            // 
            this.ProvinceDistrictMultiFilter.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("ProvinceDistrictMultiFilter.Appearance.Font")));
            this.ProvinceDistrictMultiFilter.Appearance.Options.UseFont = true;
            this.ProvinceDistrictMultiFilter.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            resources.ApplyResources(this.ProvinceDistrictMultiFilter, "ProvinceDistrictMultiFilter");
            this.ProvinceDistrictMultiFilter.Name = "ProvinceDistrictMultiFilter";
            this.ProvinceDistrictMultiFilter.ValueChanged += new System.EventHandler<EIDSS.Reports.BaseControls.Filters.MultiFilterEventArgs>(this.ProvinceDistrictMultiFilter_ValueChanged);
            // 
            // ReportModeLookUp
            // 
            resources.ApplyResources(this.ReportModeLookUp, "ReportModeLookUp");
            this.ReportModeLookUp.Name = "ReportModeLookUp";
            this.ReportModeLookUp.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("ReportModeLookUp.Properties.Appearance.Font")));
            this.ReportModeLookUp.Properties.Appearance.Options.UseFont = true;
            this.ReportModeLookUp.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("ReportModeLookUp.Properties.Buttons")))),
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("ReportModeLookUp.Properties.Buttons1"))))});
            this.ReportModeLookUp.Properties.DropDownRows = 12;
            this.ReportModeLookUp.Properties.NullText = resources.GetString("ReportModeLookUp.Properties.NullText");
            this.ReportModeLookUp.EditValueChanged += new System.EventHandler(this.ReportModeLookUp_EditValueChanged);
            // 
            // ReportModeLabel
            // 
            resources.ApplyResources(this.ReportModeLabel, "ReportModeLabel");
            this.ReportModeLabel.ForeColor = System.Drawing.Color.Black;
            this.ReportModeLabel.Name = "ReportModeLabel";
            // 
            // NumberOfCasesDeathsMonthTHReportKeeper
            // 
            this.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("NumberOfCasesDeathsMonthTHReportKeeper.Appearance.Font")));
            this.Appearance.Options.UseFont = true;
            this.HeaderHeight = 176;
            this.Name = "NumberOfCasesDeathsMonthTHReportKeeper";
            this.pnlSettings.ResumeLayout(false);
            this.pnlSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceUseArchiveData.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportModeLookUp.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private HumDiagnosisMultiFilter DiagnosesFilter;
        private ThaiZonesMultiFilter ZonesFilter;
        private ThaiRegionMultiFilter RegionFilter;
        private ThaiCaseClassificationFilter CaseClassificationFilter;
        private ThaiProvinceMultiFilter ProvinceFilter;
        private ThaiProvinceDistrictsGroupsMultiFilter ProvinceDistrictMultiFilter;
        private DevExpress.XtraEditors.LookUpEdit ReportModeLookUp;
        private System.Windows.Forms.Label ReportModeLabel;
    }
}
