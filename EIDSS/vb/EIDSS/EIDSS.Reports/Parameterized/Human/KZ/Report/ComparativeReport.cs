using System;
using System.Text;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using eidss.model.Reports.KZ;
using bv.model.BLToolkit;
using eidss.model.Reports;
using EIDSS.Reports.BaseControls.Report;
using System.Data.SqlClient;
using eidss.model.Reports.Common;
using EIDSS.Reports.BaseControls;
using System.ComponentModel;

namespace EIDSS.Reports.Parameterized.Human.KZ.Report
{
    public partial class ComparativeReport : ComparativeReportBase
    {
        private ComponentResourceManager _resources = new ComponentResourceManager(typeof(ComparativeReport));

        public ComparativeReport()
        {
            InitializeComponent();
        }

        public void SetParameters(ComparativeKZModel model,
            DbManagerProxy manager, DbManagerProxy archiveManager)
        {
            SetReportLabels(model);

            SetReportLabelsBase((SqlConnection)manager.Connection, model.Language, model.OrganizationId, model.SiteId);

            comparativeKZ1.EnforceConstraints = false;

            EIDSS.Reports.Parameterized.Human.KZ.DataSet.ComparativeKZ.spRepHumComparativeKZDataTable dataTable = comparativeKZ1.spRepHumComparativeKZ;

            spRepHumComparativeKZTableAdapter1.Connection = (SqlConnection)manager.Connection;
            spRepHumComparativeKZTableAdapter1.Transaction = (SqlTransaction)manager.Transaction;
            spRepHumComparativeKZTableAdapter1.CommandTimeout = BaseReport.CommandTimeout;

            spRepHumComparativeKZTableAdapter1.Fill(
                dataTable,
                model.Language,
                model.Year1,
                model.Year2,
                model.StartMonth,
                model.EndMonth,
                model.RegionId,
                model.RayonId,
                model.SiteId);
        }

        private void SetReportLabels(ComparativeKZModel model)
        {
            xrLabelYearFirst.Text = String.Format(xrLabelYearFirst.Text, model.Year1);
            xrLabelYearSecond.Text = String.Format(xrLabelYearSecond.Text, model.Year2);

            xrLabelFromYear.Text = Convert.ToString(model.Year1);
            xrLabelToYear.Text = Convert.ToString(model.Year2);

            xrLabelRegoin.Text = GenerateRegionText(model);
            xrLabelDifference.Text = String.Format(xrLabelDifference.Text, model.Year2, model.Year1);

            ReportUIHelper.SetMonth(xrLabelFromMonth, xrLabelToMonth, xrLabelMonthDash, model.StartMonth.Value, model.EndMonth.Value);
        }

        private string GenerateRegionText(ComparativeKZModel model)
        {
            if (String.IsNullOrEmpty(model.RegionName) && String.IsNullOrEmpty(model.RayonName))
            {
                return _resources.GetString("Kazakhstan_Key");
            }
            else
            {
                return model.RegionName + (String.IsNullOrEmpty(model.RayonName) ? " " : ", ") + model.RayonName;
            }
        }

        private void CellChangeDecimalPoint_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            CellChangeDecimalPoint_BeforePrint_Base(sender, e);
        }
    }
}
