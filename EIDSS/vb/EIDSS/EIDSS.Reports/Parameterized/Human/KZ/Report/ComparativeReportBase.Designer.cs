namespace EIDSS.Reports.Parameterized.Human.KZ.Report
{
    partial class ComparativeReportBase
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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComparativeReportBase));
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrLabelCurrentOrganization = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelPrintedFromEIDSS = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelPrintedDateAndTime = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.m_BaseDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableBaseHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // cellLanguage
            // 
            this.cellLanguage.StylePriority.UseTextAlignment = false;
            // 
            // lblReportName
            // 
            resources.ApplyResources(this.lblReportName, "lblReportName");
            this.lblReportName.StylePriority.UseBorders = false;
            this.lblReportName.StylePriority.UseBorderWidth = false;
            this.lblReportName.StylePriority.UseFont = false;
            this.lblReportName.StylePriority.UseTextAlignment = false;
            // 
            // Detail
            // 
            this.Detail.StylePriority.UseFont = false;
            this.Detail.StylePriority.UsePadding = false;
            // 
            // PageHeader
            // 
            this.PageHeader.Expanded = false;
            resources.ApplyResources(this.PageHeader, "PageHeader");
            this.PageHeader.StylePriority.UseFont = false;
            this.PageHeader.StylePriority.UsePadding = false;
            // 
            // PageFooter
            // 
            resources.ApplyResources(this.PageFooter, "PageFooter");
            this.PageFooter.StylePriority.UseBorders = false;
            // 
            // ReportHeader
            // 
            resources.ApplyResources(this.ReportHeader, "ReportHeader");
            // 
            // xrPageInfo1
            // 
            resources.ApplyResources(this.xrPageInfo1, "xrPageInfo1");
            this.xrPageInfo1.StylePriority.UseBorders = false;
            // 
            // cellReportHeader
            // 
            this.cellReportHeader.StylePriority.UseBorders = false;
            this.cellReportHeader.StylePriority.UseFont = false;
            this.cellReportHeader.StylePriority.UseTextAlignment = false;
            // 
            // cellBaseSite
            // 
            this.cellBaseSite.StylePriority.UseBorders = false;
            this.cellBaseSite.StylePriority.UseFont = false;
            this.cellBaseSite.StylePriority.UseTextAlignment = false;
            // 
            // tableBaseHeader
            // 
            resources.ApplyResources(this.tableBaseHeader, "tableBaseHeader");
            this.tableBaseHeader.StylePriority.UseBorders = false;
            this.tableBaseHeader.StylePriority.UseBorderWidth = false;
            this.tableBaseHeader.StylePriority.UseFont = false;
            this.tableBaseHeader.StylePriority.UsePadding = false;
            this.tableBaseHeader.StylePriority.UseTextAlignment = false;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabelCurrentOrganization,
            this.xrLabelPrintedFromEIDSS,
            this.xrLabelPrintedDateAndTime});
            resources.ApplyResources(this.ReportFooter, "ReportFooter");
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrLabelCurrentOrganization
            // 
            resources.ApplyResources(this.xrLabelCurrentOrganization, "xrLabelCurrentOrganization");
            this.xrLabelCurrentOrganization.Multiline = true;
            this.xrLabelCurrentOrganization.Name = "xrLabelCurrentOrganization";
            this.xrLabelCurrentOrganization.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelCurrentOrganization.StylePriority.UseFont = false;
            // 
            // xrLabelPrintedFromEIDSS
            // 
            this.xrLabelPrintedFromEIDSS.Borders = DevExpress.XtraPrinting.BorderSide.None;
            resources.ApplyResources(this.xrLabelPrintedFromEIDSS, "xrLabelPrintedFromEIDSS");
            this.xrLabelPrintedFromEIDSS.Name = "xrLabelPrintedFromEIDSS";
            this.xrLabelPrintedFromEIDSS.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 10, 0, 0, 100F);
            this.xrLabelPrintedFromEIDSS.StylePriority.UseBorders = false;
            this.xrLabelPrintedFromEIDSS.StylePriority.UseFont = false;
            this.xrLabelPrintedFromEIDSS.StylePriority.UsePadding = false;
            this.xrLabelPrintedFromEIDSS.StylePriority.UseTextAlignment = false;
            // 
            // xrLabelPrintedDateAndTime
            // 
            resources.ApplyResources(this.xrLabelPrintedDateAndTime, "xrLabelPrintedDateAndTime");
            this.xrLabelPrintedDateAndTime.Name = "xrLabelPrintedDateAndTime";
            this.xrLabelPrintedDateAndTime.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelPrintedDateAndTime.StylePriority.UseFont = false;
            // 
            // ComparativeReportBase
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.PageFooter,
            this.ReportHeader,
            this.ReportFooter});
            this.DataAdapter = null;
            this.DataMember = "";
            this.DataSource = null;
            resources.ApplyResources(this, "$this");
            this.Version = "15.1";
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.ReportHeader, 0);
            this.Controls.SetChildIndex(this.PageFooter, 0);
            this.Controls.SetChildIndex(this.PageHeader, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.m_BaseDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableBaseHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        protected DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        protected DevExpress.XtraReports.UI.XRLabel xrLabelCurrentOrganization;
        protected DevExpress.XtraReports.UI.XRLabel xrLabelPrintedFromEIDSS;
        protected DevExpress.XtraReports.UI.XRLabel xrLabelPrintedDateAndTime;


    }
}
