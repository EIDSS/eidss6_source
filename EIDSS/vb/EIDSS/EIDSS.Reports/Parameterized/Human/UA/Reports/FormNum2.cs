using bv.model.BLToolkit;
using eidss.model.Reports;
using eidss.model.Reports.Common;
using eidss.model.Reports.UA;
using EIDSS.Reports.BaseControls;
using EIDSS.Reports.BaseControls.Report;
using EIDSS.Reports.Parameterized.Human.UA.DataSets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace EIDSS.Reports.Parameterized.Human.UA.Reports
{
    [CanWorkWithArchive]
    public partial class FormNum2 : FormNumBase

    {
        public FormNum2()
        {
            InitializeComponent();
        }

        public void SetParameters(UAFormModel model, DbManagerProxy manager, DbManagerProxy archiveManager)
        {
            SetParametersBase(model, manager);

            SetRegionCaptionText(xrLabel13RegionCaption, model);
            
            lblYear.Text = string.Format("{0}", model.Year);

            uaReportDataSet1.EnforceConstraints = false;

            SpecialInfectionAndParazitaryDiseaseReportNo2DataSet.spRepHumForm2UADataTable1DataTable dataTable1 = uaReportDataSet1.spRepHumForm2UADataTable1;
            SpecialInfectionAndParazitaryDiseaseReportNo2DataSet.spRepHumForm2UADataTable2DataTable dataTable2 = uaReportDataSet1.spRepHumForm2UADataTable2;
            SpecialInfectionAndParazitaryDiseaseReportNo2DataSet.spRepHumForm2UADataTable3DataTable dataTable3 = uaReportDataSet1.spRepHumForm2UADataTable3;
            SpecialInfectionAndParazitaryDiseaseReportNo2DataSet.spRepHumForm2UAHeaderDataTable dataTable4 = uaReportDataSet1.spRepHumForm2UAHeader;

            Action<SqlConnection, SqlTransaction> action = ((connection, transaction) =>
            {
                spRepHumForm2UADataTable1TableAdapter.Connection = connection;
                spRepHumForm2UADataTable1TableAdapter.Transaction = transaction;
                spRepHumForm2UADataTable1TableAdapter.CommandTimeout = BaseReport.CommandTimeout;

                spRepHumForm2UADataTable1TableAdapter.Fill(dataTable1, model.Language, model.Year, model.RegionId, model.UserId);
            });

            Action<SqlConnection, SqlTransaction> action2 = ((connection, transaction) =>
            {
                spRepHumForm2UADataTable2TableAdapter.Connection = connection;
                spRepHumForm2UADataTable2TableAdapter.Transaction = transaction;
                spRepHumForm2UADataTable2TableAdapter.CommandTimeout = BaseReport.CommandTimeout;

                spRepHumForm2UADataTable2TableAdapter.Fill(dataTable2, model.Language, model.Year, model.RegionId, model.UserId);
            });

            Action<SqlConnection, SqlTransaction> action3 = ((connection, transaction) =>
            {
                spRepHumForm2UADataTable3TableAdapter.Connection = connection;
                spRepHumForm2UADataTable3TableAdapter.Transaction = transaction;
                spRepHumForm2UADataTable3TableAdapter.CommandTimeout = BaseReport.CommandTimeout;

                spRepHumForm2UADataTable3TableAdapter.Fill(dataTable3, model.Language, model.Year, model.RegionId, model.UserId);
            });

            Action<SqlConnection, SqlTransaction> action4 = ((connection, transaction) => {
                spRepHumForm2UAHeaderTableAdapter.Connection = connection;
                spRepHumForm2UAHeaderTableAdapter.Transaction = transaction;
                spRepHumForm2UAHeaderTableAdapter.CommandTimeout = BaseReport.CommandTimeout;

                spRepHumForm2UAHeaderTableAdapter.Fill(dataTable4, model.Language, model.UserId);
            });

            BaseReport.FillDataTableWithArchive(action,
                manager, archiveManager,
                dataTable1,
                model.Mode,
                new[] { "strDiseaseName", "intRowNumber", "strICD10" }, null, null, null);

            BaseReport.FillDataTableWithArchive(action2,
                manager, archiveManager,
                dataTable2,
                model.Mode,
                new[] { "strDiseaseName", "intRowNumber", "strICD10" }, null, null, null);

            BaseReport.FillDataTableWithArchive(action3,
                manager, archiveManager,
                dataTable3,
                model.Mode,
                new[] { "strDiseaseName", "intRowNumber", "strICD10" }, null, null, null);

            BaseReport.FillDataTableWithArchive(action4,
                manager, archiveManager,
                dataTable4,
                model.Mode,
                new[] { "strOrganizationFullName", "strCountry", "strAddressMain", "strHBA", "strEmployeeName", "strEmployeePhone" }, null, null, null);
        }
    }
}
