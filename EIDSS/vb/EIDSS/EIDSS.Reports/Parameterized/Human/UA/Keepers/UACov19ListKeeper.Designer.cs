namespace EIDSS.Reports.Parameterized.Human.UA.Keepers
{
    partial class UACov19ListKeeper
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UACov19ListKeeper));
            this.filterCaseClassification = new EIDSS.Reports.BaseControls.Filters.ThaiCaseClassificationFilter();
            this.filterOutcome = new EIDSS.Reports.BaseControls.Filters.OutcomeLookupFilter();
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties)).BeginInit();
            this.pnlSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceUseArchiveData.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // regionFilter
            // 
            this.regionFilter.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("regionFilter.Appearance.Font")));
            this.regionFilter.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.regionFilter, "regionFilter");
            // 
            // rayonFilter
            // 
            this.rayonFilter.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("rayonFilter.Appearance.Font")));
            this.rayonFilter.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.rayonFilter, "rayonFilter");
            // 
            // settlementFilter
            // 
            this.settlementFilter.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("settlementFilter.Appearance.Font")));
            this.settlementFilter.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.settlementFilter, "settlementFilter");
            // 
            // lblStart
            // 
            resources.ApplyResources(this.lblStart, "lblStart");
            // 
            // lblEnd
            // 
            resources.ApplyResources(this.lblEnd, "lblEnd");
            // 
            // dtStart
            // 
            resources.ApplyResources(this.dtStart, "dtStart");
            // 
            // dtEnd
            // 
            resources.ApplyResources(this.dtEnd, "dtEnd");
            // 
            // pnlSettings
            // 
            this.pnlSettings.Controls.Add(this.filterOutcome);
            this.pnlSettings.Controls.Add(this.filterCaseClassification);
            resources.ApplyResources(this.pnlSettings, "pnlSettings");
            this.pnlSettings.Controls.SetChildIndex(this.GenerateReportButton, 0);
            this.pnlSettings.Controls.SetChildIndex(this.lblStart, 0);
            this.pnlSettings.Controls.SetChildIndex(this.lblEnd, 0);
            this.pnlSettings.Controls.SetChildIndex(this.dtStart, 0);
            this.pnlSettings.Controls.SetChildIndex(this.dtEnd, 0);
            this.pnlSettings.Controls.SetChildIndex(this.ceUseArchiveData, 0);
            this.pnlSettings.Controls.SetChildIndex(this.rayonFilter, 0);
            this.pnlSettings.Controls.SetChildIndex(this.regionFilter, 0);
            this.pnlSettings.Controls.SetChildIndex(this.settlementFilter, 0);
            this.pnlSettings.Controls.SetChildIndex(this.filterCaseClassification, 0);
            this.pnlSettings.Controls.SetChildIndex(this.filterOutcome, 0);
            // 
            // ceUseArchiveData
            // 
            resources.ApplyResources(this.ceUseArchiveData, "ceUseArchiveData");
            this.ceUseArchiveData.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("ceUseArchiveData.Properties.Appearance.Font")));
            this.ceUseArchiveData.Properties.Appearance.Options.UseFont = true;
            this.ceUseArchiveData.Properties.AppearanceDisabled.Font = ((System.Drawing.Font)(resources.GetObject("ceUseArchiveData.Properties.AppearanceDisabled.Font")));
            this.ceUseArchiveData.Properties.AppearanceDisabled.Options.UseFont = true;
            this.ceUseArchiveData.Properties.AppearanceFocused.Font = ((System.Drawing.Font)(resources.GetObject("ceUseArchiveData.Properties.AppearanceFocused.Font")));
            this.ceUseArchiveData.Properties.AppearanceFocused.Options.UseFont = true;
            this.ceUseArchiveData.Properties.AppearanceReadOnly.Font = ((System.Drawing.Font)(resources.GetObject("ceUseArchiveData.Properties.AppearanceReadOnly.Font")));
            this.ceUseArchiveData.Properties.AppearanceReadOnly.Options.UseFont = true;
            // 
            // GenerateReportButton
            // 
            this.GenerateReportButton.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("GenerateReportButton.Appearance.Font")));
            this.GenerateReportButton.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.GenerateReportButton, "GenerateReportButton");
            bv.common.Resources.BvResourceManagerChanger.GetResourceManager(typeof(UACov19ListKeeper), out resources);
            // Form Is Localizable: True
            // 
            // filterCaseClassification
            // 
            this.filterCaseClassification.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("filterCaseClassification.Appearance.Font")));
            this.filterCaseClassification.Appearance.Options.UseFont = true;
            this.filterCaseClassification.ExternalLookupCaption = "Case Classification";
            resources.ApplyResources(this.filterCaseClassification, "filterCaseClassification");
            this.filterCaseClassification.Name = "filterCaseClassification";
            // 
            // filterOutcome
            // 
            this.filterOutcome.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("filterOutcome.Appearance.Font")));
            this.filterOutcome.Appearance.Options.UseFont = true;
            this.filterOutcome.ExternalLookupCaption = "Outcome";
            resources.ApplyResources(this.filterOutcome, "filterOutcome");
            this.filterOutcome.Name = "filterOutcome";
            // 
            // UACov19ListKeeper
            // 
            this.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("UACov19ListKeeper.Appearance.Font")));
            this.Appearance.Options.UseFont = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.HeaderHeight = 380;
            this.Name = "UACov19ListKeeper";
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties)).EndInit();
            this.pnlSettings.ResumeLayout(false);
            this.pnlSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceUseArchiveData.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BaseControls.Filters.ThaiCaseClassificationFilter filterCaseClassification;
        private BaseControls.Filters.OutcomeLookupFilter filterOutcome;
    }
}
