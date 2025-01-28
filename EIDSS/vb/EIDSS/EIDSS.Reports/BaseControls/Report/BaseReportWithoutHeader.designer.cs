//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing.Printing;
//using System.Linq;
//using DevExpress.XtraPrinting;
//using DevExpress.XtraReports.UI;
//using EIDSS.Reports.BaseControls.BaseDataSetTableAdapters;

//namespace EIDSS.Reports.BaseControls.Report
//{
//    partial class BaseReportWithoutHeader
//    {
//        /// <summary>
//        /// Required designer variable.
//        /// </summary>
//        protected IContainer components;

//        /// <summary> 
//        /// Clean up any resources being used.
//        /// </summary>
//        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #region Designer generated code

//        /// <summary>
//        /// Required method for Designer support - do not modify
//        /// the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseReportWithoutHeader));
//            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
//            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
//            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
//            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
//            this.m_BaseDataSet = new EIDSS.Reports.BaseControls.BaseDataSet();
//            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
//            this.m_BaseAdapter = new EIDSS.Reports.BaseControls.BaseDataSetTableAdapters.BaseAdapter();
//            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
//            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
//            ((System.ComponentModel.ISupportInitialize)(this.m_BaseDataSet)).BeginInit();
//            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
//            // 
//            // Detail
//            // 
//            resources.ApplyResources(this.Detail, "Detail");
//            this.Detail.Name = "Detail";
//            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//            this.Detail.StylePriority.UseFont = false;
//            this.Detail.StylePriority.UsePadding = false;
//            // 
//            // PageHeader
//            // 
//            resources.ApplyResources(this.PageHeader, "PageHeader");
//            this.PageHeader.Name = "PageHeader";
//            this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
//            this.PageHeader.StylePriority.UseFont = false;
//            this.PageHeader.StylePriority.UsePadding = false;
//            // 
//            // PageFooter
//            // 
//            this.PageFooter.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
//            | DevExpress.XtraPrinting.BorderSide.Right) 
//            | DevExpress.XtraPrinting.BorderSide.Bottom)));
//            resources.ApplyResources(this.PageFooter, "PageFooter");
//            this.PageFooter.Name = "PageFooter";
//            this.PageFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
//            this.PageFooter.StylePriority.UseBorders = false;
//            // 
//            // formattingRule1
//            // 
//            this.formattingRule1.Name = "formattingRule1";
//            // 
//            // m_BaseDataSet
//            // 
//            this.m_BaseDataSet.DataSetName = "BaseDataSet";
//            this.m_BaseDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
//            // 
//            // ReportHeader
//            // 
//            resources.ApplyResources(this.ReportHeader, "ReportHeader");
//            this.ReportHeader.Name = "ReportHeader";
//            // 
//            // m_BaseAdapter
//            // 
//            this.m_BaseAdapter.ClearBeforeFill = true;
//            // 
//            // topMarginBand1
//            // 
//            resources.ApplyResources(this.topMarginBand1, "topMarginBand1");
//            this.topMarginBand1.Name = "topMarginBand1";
//            // 
//            // bottomMarginBand1
//            // 
//            resources.ApplyResources(this.bottomMarginBand1, "bottomMarginBand1");
//            this.bottomMarginBand1.Name = "bottomMarginBand1";
//            // 
//            // BaseReportWithoutHeader
//            // 
//            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
//            this.Detail,
//            this.PageHeader,
//            this.PageFooter,
//            this.ReportHeader,
//            this.topMarginBand1,
//            this.bottomMarginBand1});
//            this.DataAdapter = this.m_BaseAdapter;
//            this.DataMember = "sprepGetBaseParameters";
//            this.DataSource = this.m_BaseDataSet;
//            resources.ApplyResources(this, "$this");
//            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
//            this.formattingRule1});
//            this.Version = "15.1";
//            ((System.ComponentModel.ISupportInitialize)(this.m_BaseDataSet)).EndInit();
//            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

//        }

//        #endregion

//        private FormattingRule formattingRule1;
//        protected DetailBand Detail;
//        protected PageHeaderBand PageHeader;
//        protected PageFooterBand PageFooter;
//        protected BaseAdapter m_BaseAdapter;
//        public ReportHeaderBand ReportHeader;
//        protected BaseDataSet m_BaseDataSet;
//        private TopMarginBand topMarginBand1;
//        private BottomMarginBand bottomMarginBand1;
//    }
//}