using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using EIDSS.Reports.BaseControls.Report;
using System.Collections.Generic;
using bv.model.BLToolkit;
using System.Data.SqlClient;

namespace EIDSS.Reports.Document.Veterinary.JO
{
    public partial class UrgentNotificationForm : BaseDocumentReport
    {
        public UrgentNotificationForm()
        {
            InitializeComponent();
        }

        public override void SetParameters(string lang, Dictionary<string, string> parameters, DbManagerProxy manager, DbManagerProxy archiveManager)
        {
            base.SetParameters(lang, parameters, manager, archiveManager);
            string caseId = GetStringParameter(parameters, "@ObjID");

            spRepVetNotificationFormJoTableAdapter.Connection = (SqlConnection)manager.Connection;
            spRepVetNotificationFormJoTableAdapter.Transaction = (SqlTransaction)manager.Transaction;

            urgentNotificationFormDataSet1.EnforceConstraints = false;
            spRepVetNotificationFormJoTableAdapter.Fill(urgentNotificationFormDataSet1.spRepVetNotificationFormJo, lang, long.Parse(caseId));


            spRepVetNotificationFormJoLabTestResultsTableAdapter.Connection = (SqlConnection)manager.Connection;
            spRepVetNotificationFormJoLabTestResultsTableAdapter.Transaction = (SqlTransaction)manager.Transaction;

            spRepVetNotificationFormJoLabTestResultsTableAdapter.Fill(urgentNotificationFormDataSet1.spRepVetNotificationFormJoLabTestResults, lang, long.Parse(caseId));

            if (lang.Equals("en", StringComparison.InvariantCultureIgnoreCase))
            {
                xrTable2.Visible = false;
                xrTable2En.Visible = true;

                xrLabTable.Visible = false;
                xrLabTableEn.Visible = true;

                //Grid
                xrLabel92.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                xrLabel19.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                xrLabel27.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                xrLabel36.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                xrLabel37.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                xrLabel45.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                xrLabel53.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                xrLabel61.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                xrLabel69.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                xrLabel105.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                xrLabel113.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                xrLabel121.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                xrLabel135.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                xrLabel143.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                xrLabel151.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                xrLabel159.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                xrLabel167.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;

                xrLabel104.Borders = xrLabel104.Borders | DevExpress.XtraPrinting.BorderSide.Top;
                xrLabel100.Borders = xrLabel100.Borders | DevExpress.XtraPrinting.BorderSide.Top;
                xrLabel73.Borders = xrLabel73.Borders | DevExpress.XtraPrinting.BorderSide.Top;
                xrLabel99.Borders = xrLabel99.Borders | DevExpress.XtraPrinting.BorderSide.Top;
                xrLabel71.Borders = xrLabel71.Borders | DevExpress.XtraPrinting.BorderSide.Top;
                xrLabel72.Borders = xrLabel72.Borders | DevExpress.XtraPrinting.BorderSide.Top;
                xrLabel69.Borders = xrLabel69.Borders | DevExpress.XtraPrinting.BorderSide.Top;
                xrLabel70.Borders = xrLabel70.Borders | DevExpress.XtraPrinting.BorderSide.Top;

                xrLabTestTypeHeader.Borders = xrLabTestTypeHeader.Borders | DevExpress.XtraPrinting.BorderSide.Right;
                xrLabResultHeader.Borders = DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
            }
            else
            {
                xrTable2.Visible = true;
                xrTable2En.Visible = false;

                xrLabTable.Visible = true;
                xrLabTableEn.Visible = false;

                xrLabel12.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                xrLabel11.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom;
                xrLabel13.Borders = DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                xrLabel14.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                xrLabel15.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;

                xrLabel106.Borders = xrLabel106.Borders | DevExpress.XtraPrinting.BorderSide.Top;
                xrLabel105.Borders = xrLabel105.Borders | DevExpress.XtraPrinting.BorderSide.Top;
                xrLabel108.Borders = xrLabel108.Borders | DevExpress.XtraPrinting.BorderSide.Top;
                xrLabel107.Borders = xrLabel107.Borders | DevExpress.XtraPrinting.BorderSide.Top;
                xrLabel110.Borders = xrLabel110.Borders | DevExpress.XtraPrinting.BorderSide.Top;
                xrLabel109.Borders = xrLabel109.Borders | DevExpress.XtraPrinting.BorderSide.Top;
                xrLabel111.Borders = xrLabel111.Borders | DevExpress.XtraPrinting.BorderSide.Top;
                xrLabel112.Borders = xrLabel112.Borders | DevExpress.XtraPrinting.BorderSide.Top;

                xrLabel97.Borders = xrLabel97.Borders | DevExpress.XtraPrinting.BorderSide.Left;
                xrLabel86.Borders = xrLabel86.Borders | DevExpress.XtraPrinting.BorderSide.Left;
                xrLabel83.Borders = xrLabel83.Borders | DevExpress.XtraPrinting.BorderSide.Left;
                xrLabel26.Borders = xrLabel26.Borders | DevExpress.XtraPrinting.BorderSide.Left;
                xrLabel33.Borders = xrLabel33.Borders | DevExpress.XtraPrinting.BorderSide.Left;
                xrLabel40.Borders = xrLabel40.Borders | DevExpress.XtraPrinting.BorderSide.Left;
                xrLabel48.Borders = xrLabel48.Borders | DevExpress.XtraPrinting.BorderSide.Left;
                xrLabel64.Borders = xrLabel64.Borders | DevExpress.XtraPrinting.BorderSide.Left;
                xrLabel56.Borders = xrLabel56.Borders | DevExpress.XtraPrinting.BorderSide.Left;
                xrLabel72.Borders = xrLabel72.Borders | DevExpress.XtraPrinting.BorderSide.Left;
                xrLabel108.Borders = xrLabel108.Borders | DevExpress.XtraPrinting.BorderSide.Left;
                xrLabel116.Borders = xrLabel116.Borders | DevExpress.XtraPrinting.BorderSide.Left;
                xrLabel130.Borders = xrLabel130.Borders | DevExpress.XtraPrinting.BorderSide.Left;
                xrLabel138.Borders = xrLabel138.Borders | DevExpress.XtraPrinting.BorderSide.Left;
                xrLabel146.Borders = xrLabel146.Borders | DevExpress.XtraPrinting.BorderSide.Left;
                xrLabel154.Borders = xrLabel154.Borders | DevExpress.XtraPrinting.BorderSide.Left;
                xrLabel162.Borders = xrLabel162.Borders | DevExpress.XtraPrinting.BorderSide.Left;
                xrLabel170.Borders = xrLabel170.Borders | DevExpress.XtraPrinting.BorderSide.Left;

                xrLabResultHeader.Borders = DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                xrLabTestTypeHeader.Borders = DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
            }
            
            //ReportRtlHelper.SetRTL(this);
        }

        private void xrLabel5_EvaluateBinding(object sender, BindingEventArgs e)
        {
            string datePattern = System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern;
            e.Binding.FormatString = "{0:" + datePattern + "}";  
        }

    }
}
