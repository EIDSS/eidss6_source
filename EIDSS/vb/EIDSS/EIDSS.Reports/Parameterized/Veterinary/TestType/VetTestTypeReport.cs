using System;
using System.Data.SqlClient;
using bv.model.BLToolkit;
using eidss.model.Reports;
using eidss.model.Reports.Common;
using EIDSS.Reports.BaseControls.Report;
using EIDSS.Reports.Factory;
using System.Globalization;

namespace EIDSS.Reports.Parameterized.Veterinary.TestType
{
    [NullableAdapters]
    public sealed partial class VetTestTypeReport : BaseYearReport
    {
        public VetTestTypeReport()
        {
            InitializeComponent();
            ApplyAngle();
        }

        public override void SetParameters( BaseYearModel model, DbManagerProxy manager, DbManagerProxy archiveManager)
        {
            base.SetParameters(model, manager, archiveManager);

              Action<SqlConnection, SqlTransaction> action = ((connection, transaction) =>
            {
                vetTestTypeDataSet1.EnforceConstraints = false;
                sp_rep_VET_YearTestTypeReportTableAdapter1.Connection = connection;
                sp_rep_VET_YearTestTypeReportTableAdapter1.Transaction = transaction;
                sp_rep_VET_YearTestTypeReportTableAdapter1.CommandTimeout = CommandTimeout;
                sp_rep_VET_YearTestTypeReportTableAdapter1.Fill(
                    vetTestTypeDataSet1.spRepVetSamplesReportbySampleTypesWithinRegions,
                    model.Language, model.Year);
            });

            FillDataTableWithArchive(action,
              manager, archiveManager,
                vetTestTypeDataSet1.spRepVetSamplesReportbySampleTypesWithinRegions,
                model.Mode,
                new[] {"Disease", "Region", "SampleType", "TestType"});

            vetTestTypeDataSet1.spRepVetSamplesReportbySampleTypesWithinRegions.DefaultView.Sort = "Disease, Region, SampleType, TestType";

            ReportRtlHelper.SetRTL(this);
        }

        private void ApplyAngle()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VetTestTypeReport));
            string angleString = resources.GetString("angle", CultureInfo.CurrentUICulture);
            int angle = 0;
            if (int.TryParse(angleString, out angle))
            {
                xrTableCell1.Angle = angle;
                cell4.Angle = angle;
                xrTableCell2.Angle = angle;
                cell5.Angle = angle;
                xrTableCell5.Angle = angle;
                cell6.Angle = angle;
                xrTableCell6.Angle = angle;
                cellType.Angle = angle;
                xrTableCell7.Angle = angle;
                xrTableCell8.Angle = angle;
                xrTableCell10.Angle = angle;
                xrTableCell4.Angle = angle;
                tableHeader.HeightF = 140;
            }
        }

    }
}