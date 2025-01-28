using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using DevExpress.XtraReports.UI;
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
using EIDSS.Reports.Parameterized.Human.UA.DataSets.UACov19ListTableAdapters;


namespace EIDSS.Reports.Parameterized.Human.UA.Reports
{
    public partial class UACov19List : EIDSS.Reports.BaseControls.Report.BaseReport
    {
        public UACov19List()
        {
            InitializeComponent();

            xrPageInfo1.Visible = false;
        }

        public void SetParameters(UACov19ListModel model, DbManagerProxy manager, DbManagerProxy archiveManager)
        {
            SetLanguage(model, manager);

            //Check if needed
            ShowWarningIfDataInArchive(manager, model.StartDate, model.UseArchive);

            //string format = (new CultureInfo("uk-UA")).DateTimeFormat.ShortDatePattern;
            //ReportRebinder rebinder = this.GetDateRebinder(model.Language);

            //DateTime dtNow = DateTime.Now;
            //FooterTime.Text = string.Format("{0}", rebinder.ToTimeString(dtNow));
            //FooterDate.Text = dtNow.ToString(format, Thread.CurrentThread.CurrentCulture);

            UACov19ListDataSet.EnforceConstraints = false;

            EIDSS.Reports.Parameterized.Human.UA.DataSets.UACov19List.spRepHumanUACov19ListDataTable dataTable = 
                UACov19ListDataSet.spRepHumanUACov19List;

            Action<SqlConnection, SqlTransaction> action = ((connection, transaction) =>
            {
                spRepHumanUACov19ListTableAdapter1.Connection = connection;
                spRepHumanUACov19ListTableAdapter1.Transaction = transaction;
                spRepHumanUACov19ListTableAdapter1.CommandTimeout = BaseReport.CommandTimeout;

                spRepHumanUACov19ListTableAdapter1.Fill(dataTable, 
                    model.Language, 
                    model.StartDate, model.EndDate, 
                    model.CaseClassification, model.Outcome, 
                    model.Address.RegionId, model.Address.RayonId, model.Address.SettlementId, 
                    model.UserId);
            });

            FillDataTableWithArchive(action,
                manager, archiveManager,
                UACov19ListDataSet.spRepHumanUACov19List,
                model.Mode,
                new[] {"_01_strCaseID"});

            UACov19ListDataSet.spRepHumanUACov19List.DefaultView.Sort = "_43_datEnteredDate, _01_strCaseID";
        }
    }
}
