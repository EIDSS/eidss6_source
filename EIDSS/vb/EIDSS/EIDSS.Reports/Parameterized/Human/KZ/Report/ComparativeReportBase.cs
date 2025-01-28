using System;
using System.Text;
using System.Data.SqlClient;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using eidss.model.Reports.KZ;

namespace EIDSS.Reports.Parameterized.Human.KZ.Report
{
    public partial class ComparativeReportBase : EIDSS.Reports.BaseControls.Report.BaseReport
    {
        public ComparativeReportBase()
        {
            InitializeComponent();
        }

        protected void SetReportLabelsBase(SqlConnection cn, string language, long? orgId, long? siteId)
        {
            string organizationName = String.Empty;

            if (orgId.HasValue)
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.sprepGetBaseParameters";

                cmd.Parameters.Add("@LangID", SqlDbType.VarChar, 36).Value = language;
                cmd.Parameters.Add("@OrganizationId", SqlDbType.BigInt).Value = orgId.Value;
                if (siteId.HasValue)
                {
                    cmd.Parameters.Add("@SiteID", SqlDbType.BigInt).Value = siteId.Value;
                }
                else
                {
                    cmd.Parameters.Add("@SiteID", SqlDbType.BigInt).Value = DBNull.Value;
                }

                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (reader.Read())
                    {
                        organizationName = (string)reader["strOrganizationName"];
                    }
                }

                xrLabelCurrentOrganization.Text = organizationName;
            }

            DateTime dtNow = DateTime.Now;
            xrLabelPrintedDateAndTime.Text = String.Format("{0} {1}", dtNow.Date.ToString("dd.MM.yyy"), dtNow.ToString("HH:mm"));
        }

        protected void CellChangeDecimalPoint_BeforePrint_Base(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell currentCell = (XRTableCell)sender;
            StringBuilder celltext = new StringBuilder(currentCell.Text);

            for (int i = 1; i < celltext.Length; ++i)
            {
                if (celltext[i] == '.')
                {
                    celltext[i] = ',';
                    currentCell.Text = celltext.ToString();
                    return;
                }
            }
        }
    }
}
