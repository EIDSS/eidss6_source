using bv.common.Core;
using bv.model.BLToolkit;
using eidss.model.Core;
using eidss.model.Reports;
using eidss.model.Reports.Common;
using eidss.model.Reports.UA;
using eidss.winclient.Reports;
using EIDSS.Reports.BaseControls;
using EIDSS.Reports.BaseControls.Report;
using EIDSS.Reports.Parameterized.Human.UA.DataSets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace EIDSS.Reports.Parameterized.Human.UA.Reports
{
    [CanWorkWithArchive]
    public partial class FormNum1 : FormNumBase
    {
        public FormNum1()
        {
            InitializeComponent();
        }

        public void SetParameters(UAFormModel model,
            DbManagerProxy manager, DbManagerProxy archiveManager)
        {
            SetParametersBase(model, manager);

            SetRegionCaptionText(xrLabel12RegionCaption, model);

            lblMonth.Text = FilterHelper.GetMonthName(model.Month).ToUpper();
            lblYear.Text = model.Year.ToString();

            formNum11.EnforceConstraints = false;

            EIDSS.Reports.Parameterized.Human.UA.DataSets.FormNum1.spRepHumanUAFormNum1DataTable dataTable = formNum11.spRepHumanUAFormNum1;

            Action<SqlConnection, SqlTransaction> action = ((connection, transaction) =>
            {
                spRepHumanUAFormNum1TableAdapter.Connection = connection;
                spRepHumanUAFormNum1TableAdapter.Transaction = transaction;
                spRepHumanUAFormNum1TableAdapter.CommandTimeout = BaseReport.CommandTimeout;

                spRepHumanUAFormNum1TableAdapter.Fill(dataTable, model.Language, model.Year, model.Month, model.RegionId);
            });

            InitializeReportHeaders(model, manager);

            BaseReport.FillDataTableWithArchive(action,
                manager, archiveManager,
                dataTable,
                model.Mode,
                new[] { "strDiseaseName", "intRowNumber", "strICD10" }, null, null, null);
        }
    }
}
