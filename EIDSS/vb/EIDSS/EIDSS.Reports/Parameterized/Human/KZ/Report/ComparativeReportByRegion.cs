using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using EIDSS.Reports.BaseControls.Report;
using eidss.model.Reports;
using bv.model.BLToolkit;
using eidss.model.Reports.KZ;
using System.Data.SqlClient;
using EIDSS.Reports.Parameterized.Human.KZ.DataSet;
using eidss.model.Reports.Common;
using bv.winclient.Layout;
using System.Text;
using EIDSS.Reports.BaseControls;

namespace EIDSS.Reports.Parameterized.Human.KZ.Report
{
    public partial class ComparativeReportByRegion : ComparativeReportBase
    {
        public ComparativeReportByRegion()
        {
            InitializeComponent();

            ReportHeader.HeightF = 160f;
        }

        public void SetParameters(ComparativeReportByRegionKZModel model,
            DbManagerProxy manager, DbManagerProxy archiveManager)
        {
            dataset.EnforceConstraints = false;

            SetReportLabelsBase((SqlConnection)manager.Connection, model.Language, model.OrganizationId, model.SiteId);

            ComparativeReportByRegionKZDataset.spRepHumComparativeReportByRegionKZDataDataTable datatable = dataset.spRepHumComparativeReportByRegionKZData;
            ComparativeReportByRegionKZDataset.spRepHumComparativeReportByRegionKZHeaderDataTable header = dataset.spRepHumComparativeReportByRegionKZHeader;
            
            spRepHumComparativeReportByRegionKZDataTableAdapter.Connection = (SqlConnection)manager.Connection;
            spRepHumComparativeReportByRegionKZDataTableAdapter.Transaction = (SqlTransaction)manager.Transaction;
            spRepHumComparativeReportByRegionKZHeaderTableAdapter.Connection = (SqlConnection)manager.Connection;
            spRepHumComparativeReportByRegionKZHeaderTableAdapter.Transaction = (SqlTransaction)manager.Transaction;
            
            spRepHumComparativeReportByRegionKZDataTableAdapter.Fill(
                datatable,
                model.Language,
                model.Year1,
                model.Year2,
                model.StartMonth,
                model.EndMonth,
                model.Address.RegionId,
                model.Address.RayonId,
                model.PopulationId,
                model.idfsDiagnosisOrDiagnosisGroup);

            spRepHumComparativeReportByRegionKZHeaderTableAdapter.Fill(
                header,
                model.Language,
                model.Year1,
                model.Year2,
                FilterHelper.GetMonthName(model.StartMonth),
                FilterHelper.GetMonthName(model.EndMonth),
                model.Address.RegionId,
                model.Address.RayonId,
                model.UserId,
                model.PopulationId,
                model.idfsDiagnosisOrDiagnosisGroup);

            SetReportLabels(model, header);
        }

        private void SetReportLabels(ComparativeReportByRegionKZModel model, ComparativeReportByRegionKZDataset.spRepHumComparativeReportByRegionKZHeaderDataTable header)
        {
            xrLabel12.Text = String.Format(xrLabel12.Text, model.Year2, model.Year1);
            xrLabel9.Text = model.Diagnosis;

            var populationColumn = header.Columns[header.Columns.IndexOf(header.strPopulationColumn.ToString())];
            var orgFullNameColumn = header.Columns[header.Columns.IndexOf(header.strOrganizationFullNameColumn.ToString())];

            ReportUIHelper.SetMonth(xrLabelFromMonth, xrLabelToMonth, xrLabel8, model.StartMonth.Value, model.EndMonth.Value);
            ReportUIHelper.SetYears(xrLabelFromYear, xrLabelToYear, xrLabel7, model.Year1, model.Year2);
        }

        protected void CellChangeDecimalPoint_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            CellChangeDecimalPoint_BeforePrint_Base(sender, e);
        }
    }
}
