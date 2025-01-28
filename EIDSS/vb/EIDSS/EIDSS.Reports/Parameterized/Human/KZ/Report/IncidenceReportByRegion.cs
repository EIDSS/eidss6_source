using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using eidss.model.Reports.KZ;
using bv.model.BLToolkit;
using System.Data.SqlClient;
using EIDSS.Reports.Parameterized.Human.KZ.DataSet;
using eidss.model.Reports.Common;
using EIDSS.Reports.BaseControls;

namespace EIDSS.Reports.Parameterized.Human.KZ.Report
{
    public partial class IncidenceReportByRegion : ComparativeReportBase
    {
        public IncidenceReportByRegion()
        {
            InitializeComponent();
        }

        public void SetParameters(IncidenceReportByRegionKZModel model,
            DbManagerProxy manager, DbManagerProxy archiveManager)
        {
            dataset.EnforceConstraints = false;

            SetReportLabelsBase((SqlConnection)manager.Connection, model.Language, model.OrganizationId, model.SiteId);

            IncidenceReportByRegionKZDataset.spRepHumIncidenceReportByRegionKZDataDataTable datatable = dataset.spRepHumIncidenceReportByRegionKZData;
            IncidenceReportByRegionKZDataset.spRepHumIncidenceReportByRegionKZHeaderDataTable header = dataset.spRepHumIncidenceReportByRegionKZHeader;

            spRepHumIncidenceReportByRegionKZDataTableAdapter.Connection = (SqlConnection)manager.Connection;
            spRepHumIncidenceReportByRegionKZDataTableAdapter.Transaction = (SqlTransaction)manager.Transaction;
            spRepHumIncidenceReportByRegionKZHeaderTableAdapter.Connection = (SqlConnection)manager.Connection;
            spRepHumIncidenceReportByRegionKZHeaderTableAdapter.Transaction = (SqlTransaction)manager.Transaction;

            spRepHumIncidenceReportByRegionKZDataTableAdapter.Fill(
                datatable,
                model.Language,
                model.Year,
                model.StartMonth,
                model.EndMonth,
                model.Address.RegionId,
                model.Address.RayonId,
                model.idfsDiagnosisOrDiagnosisGroup);

            spRepHumIncidenceReportByRegionKZHeaderTableAdapter.Fill(
                header,
                model.Language,
                model.Year,
                FilterHelper.GetMonthName(model.StartMonth),
                FilterHelper.GetMonthName(model.EndMonth),
                model.Address.RegionId,
                model.Address.RayonId,
                model.UserId,
                model.idfsDiagnosisOrDiagnosisGroup);

            SetReportLabels(model, header);
        }

        private void SetReportLabels(IncidenceReportByRegionKZModel model, IncidenceReportByRegionKZDataset.spRepHumIncidenceReportByRegionKZHeaderDataTable header)
        {
            Year.Text = model.Year.ToString();
            ReportUIHelper.SetMonth(xrLabelFromMonth, xrLabelToMonth, xrLabel3, model.StartMonth.Value, model.EndMonth.Value);
        }
    }
}
