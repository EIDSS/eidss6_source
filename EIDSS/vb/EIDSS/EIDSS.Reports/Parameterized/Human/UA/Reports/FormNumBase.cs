using System;
using System.Reflection;
using System.Resources;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

using eidss.model.Reports.UA;
using bv.model.BLToolkit;
using EIDSS.Reports.BaseControls;

namespace EIDSS.Reports.Parameterized.Human.UA.Reports
{
    public partial class FormNumBase : EIDSS.Reports.BaseControls.Report.BaseReport
    {
        public FormNumBase()
        {
            InitializeComponent();

            xrPageInfo1.Visible = false;
        }

        protected void SetParametersBase(UAFormModel model, DbManagerProxy manager)
        {
            SetLanguage(model, manager);

            string format = (new CultureInfo("uk-UA")).DateTimeFormat.ShortDatePattern;
            ReportRebinder rebinder = this.GetDateRebinder(model.Language);

            DateTime dtNow = DateTime.Now;
            FooterTime.Text = string.Format("{0}", rebinder.ToTimeString(dtNow));
            FooterDate.Text = dtNow.ToString(format, Thread.CurrentThread.CurrentCulture);
        }

        protected void InitializeReportHeaders(UAFormModel model, DbManagerProxy manager)
        {
            xrLabelOrgName.Text = String.Empty;
            xrLabelCountry.Text = String.Empty;
            xrLabelAddress.Text = String.Empty;
            xrLabelAddressDetails.Text = String.Empty;
            xrLabelEmployeeName.Text = String.Empty;
            xrLabelCellNumber.Text = String.Empty;

            SqlCommand cmd = (SqlCommand)manager.Connection.CreateCommand();
            cmd.CommandText = "dbo.spRepHumanUAFormNum1Header";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LangID", SqlDbType.VarChar, 36).Value = model.Language;
            if (model.UserId.HasValue)
            {
                cmd.Parameters.Add("@UserId", SqlDbType.BigInt, 8).Value = model.UserId;
            }

            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (!reader.Read())
                    {
                        return;
                    }

                    xrLabelOrgName.Text = reader["strOrganizationFullName"] as string;
                    xrLabelCountry.Text = reader["strCountry"] as string;
                    xrLabelAddress.Text = reader["strAddressMain"] as string;
                    xrLabelAddressDetails.Text = reader["strHBA"] as string;

                    xrLabelEmployeeName.Text = reader["strEmployeeName"] as string;
                    xrLabelCellNumber.Text = reader["strEmployeePhone"] as string;
                }
            }
            catch
            {
                // TODO: log the exception!
            }
        }

        protected void SetRegionCaptionText(XRLabel regionLabel, UAFormModel model)
        {
            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(typeof(FormNumBase));

            string selectedRegion = String.Empty;
            if (model.Address != null)
            {
                selectedRegion = model.Address.RegionName(model.Language);
            }

            string regionCaption;

            if (String.IsNullOrEmpty(selectedRegion))
            {
                regionCaption = resources.GetString("NonSelectedRegion");
            }
            else
            {
                regionCaption = resources.GetString("SelectedRegion") + selectedRegion;

            }

            regionLabel.Text = String.Format("({0})", regionCaption);
        }
    }
}
