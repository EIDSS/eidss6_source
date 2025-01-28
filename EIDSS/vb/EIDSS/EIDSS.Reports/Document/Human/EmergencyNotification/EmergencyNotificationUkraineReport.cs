using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing.Printing;
using bv.model.BLToolkit;
using DevExpress.XtraReports.UI;
using eidss.model.Core;
using eidss.model.Enums;
using EIDSS.Reports.BaseControls.Report;
using EIDSS.Reports.Factory;
using System.Text;

namespace EIDSS.Reports.Document.Human.EmergencyNotification
{
    public sealed partial class EmergencyNotificationUkraineReport : BaseDocumentReport
    {
        public EmergencyNotificationUkraineReport()
        {
            InitializeComponent();
        }

        public override void SetParameters(string lang, Dictionary<string, string> parameters, DbManagerProxy manager, DbManagerProxy archiveManager)
        {
            base.SetParameters(lang, parameters, manager, archiveManager);

            string caseId = GetStringParameter(parameters, "@ObjID");

            NotificationAdapter.Connection = (SqlConnection)manager.Connection;
            NotificationAdapter.Transaction = (SqlTransaction)manager.Transaction;

            NotificationDataSet.EnforceConstraints = false;
            NotificationAdapter.Fill(NotificationDataSet.spRepHumNotificationFormUkraine, lang, long.Parse(caseId));


            NotificationSamplesAdapter.Connection = (SqlConnection)manager.Connection;
            NotificationSamplesAdapter.Transaction = (SqlTransaction)manager.Transaction;

            NotificationSamplesAdapter.Fill(NotificationDataSet.spRepHumNotificationFormUkraineSamples, lang, long.Parse(caseId));

            FillBarcode();

            ReportRtlHelper.SetRTL(this);
        }

        private void FillBarcode()
        {
            var code = string.Empty;
            if (NotificationDataSet.Tables[0].Rows.Count > 0)
            {
                code = NotificationDataSet.Tables[0].Rows[0]["1_2_CaseIdentifier"].ToString();
            }

            xrLabel3.Text = string.IsNullOrEmpty(code)
            ? string.Empty
            : m_BarCode.Code128(code);
        }

        private void SymptomsOnset_TextChanged(object sender, System.EventArgs e)
        {
            var cell1 = (sender as XRLabel);
            if (cell1 != null && !string.IsNullOrEmpty(cell1.Text))
            {
                string values = cell1.Text;
                xrLabel154.Text = values[0].ToString();
                xrLabel156.Text = values[1].ToString();
                xrLabel157.Text = values[3].ToString();
                xrLabel158.Text = values[4].ToString();
                xrLabel159.Text = values[6].ToString();
                xrLabel161.Text = values[7].ToString();
            }
        }

        private void InitialTreatment_TextChanged(object sender, System.EventArgs e)
        {
            var cell1 = (sender as XRLabel);
            if (cell1 != null && !string.IsNullOrEmpty(cell1.Text))
            {
                string values = cell1.Text;
                xrLabel163.Text = values[0].ToString();
                xrLabel164.Text = values[1].ToString();
                xrLabel165.Text = values[3].ToString();
                xrLabel166.Text = values[4].ToString();
                xrLabel167.Text = values[6].ToString();
                xrLabel168.Text = values[7].ToString();
            }
        }

        private void Diagnosis_TextChanged(object sender, System.EventArgs e)
        {
            var cell1 = (sender as XRLabel);
            if (cell1 != null && !string.IsNullOrEmpty(cell1.Text))
            {
                string values = cell1.Text;
                xrLabel169.Text = values[0].ToString();
                xrLabel170.Text = values[1].ToString();
                xrLabel171.Text = values[3].ToString();
                xrLabel172.Text = values[4].ToString();
                xrLabel173.Text = values[6].ToString();
                xrLabel174.Text = values[7].ToString();
            }
        }

        private void LastPresence_TextChanged(object sender, System.EventArgs e)
        {
            var cell1 = (sender as XRLabel);
            if (cell1 != null && !string.IsNullOrEmpty(cell1.Text))
            {
                string values = cell1.Text;
                xrLabel175.Text = values[0].ToString();
                xrLabel176.Text = values[1].ToString();
                xrLabel177.Text = values[3].ToString();
                xrLabel178.Text = values[4].ToString();
                xrLabel179.Text = values[6].ToString();
                xrLabel180.Text = values[7].ToString();
            }
        }

        private void Hospitalization_TextChanged(object sender, System.EventArgs e)
        {
            var cell1 = (sender as XRLabel);
            if (cell1 != null && !string.IsNullOrEmpty(cell1.Text))
            {
                string values = cell1.Text;
                xrLabel181.Text = values[0].ToString();
                xrLabel182.Text = values[1].ToString();
                xrLabel183.Text = values[3].ToString();
                xrLabel184.Text = values[4].ToString();
                xrLabel185.Text = values[6].ToString();
                xrLabel186.Text = values[7].ToString();
            }
        }

        private void DateOfBirth_TextChanged(object sender, System.EventArgs e)
        {
            var cell1 = (sender as XRLabel);
            if (cell1 != null && !string.IsNullOrEmpty(cell1.Text))
            {
                string values = cell1.Text;
                xrLabel187.Text = values[0].ToString();
                xrLabel188.Text = values[1].ToString();
                xrLabel189.Text = values[3].ToString();
                xrLabel190.Text = values[4].ToString();
                xrLabel191.Text = values[6].ToString();
                xrLabel192.Text = values[7].ToString();
            }
        }

        private void UniqueID_TextChanged(object sender, System.EventArgs e)
        {
            var cell1 = (sender as XRLabel);
            if (cell1 != null && !string.IsNullOrEmpty(cell1.Text))
            {
                string values = cell1.Text;
                xrLabel142.Text = values[0].ToString();
                xrLabel146.Text = values[1].ToString();
                xrLabel147.Text = values[2].ToString();
                xrLabel148.Text = values[3].ToString();
                xrLabel149.Text = values[4].ToString();
                xrLabel151.Text = values[5].ToString();
                xrLabel152.Text = values[6].ToString();
                xrLabel153.Text = values[7].ToString();
            }
        }

        private void ReceivedByOrg_TextChanged(object sender, System.EventArgs e)
        {
            var cell1 = (sender as XRLabel);
            int offset = 95;
            if (xrLabel30.WidthF < 646)
            {
                offset = 75;
            }

            if (cell1 != null && !string.IsNullOrEmpty(cell1.Text))
            {
                string[] values = cell1.Text.Split(' ');
                var firstPart = new StringBuilder();
                var secondPart = new StringBuilder();
                for (int i = 0; i < values.Length; i++)
                {
                    firstPart.Append(values[i] + " ");
                    if (firstPart.Length + values[i].Length + 1 > offset)
                    {
                        i++;
                        for (; i < values.Length; i++)
                        {
                            secondPart.Append(values[i] + " ");
                        }
                        break;
                    }
                }

                xrLabel30.Text = firstPart.ToString();
                xrLabel135.Text = secondPart.ToString();
            }
        }

        private void xrTable10_BeforePrint(object sender, PrintEventArgs e)
        {
            var table = (XRTable)sender;

            if (table.Rows.Count > 0)
                xrTable1.Rows[5].Visible = false;
            if (table.Rows.Count > 1)
                xrTable1.Rows[4].Visible = false;
            if (table.Rows.Count > 2)
                xrTable1.Rows[3].Visible = false;
            if (table.Rows.Count > 3)
                xrTable1.Rows[2].Visible = false;
            if (table.Rows.Count > 4)
                xrTable1.Rows[1].Visible = false;
            if (table.Rows.Count > 5)
                xrTable1.Rows[0].Visible = false;
        }

        private void FullName_TextChanged(object sender, System.EventArgs e)
        {
            var cell1 = (sender as XRLabel);
            if (cell1 != null && !string.IsNullOrEmpty(cell1.Text))
            {
                string[] values = cell1.Text.Split(' ');
                var firstPart = new StringBuilder();
                var secondPart = new StringBuilder();
                for (int i = 0; i < values.Length; i++)
                {
                    firstPart.Append(values[i] + " ");
                    if (firstPart.Length + values[i].Length + 1 > 80)
                    {
                        i++;
                        for (; i < values.Length; i++)
                        {
                            secondPart.Append(values[i] + " ");
                        }
                        break;
                    }
                }

                xrLabel33.Text = firstPart.ToString();
                xrLabel196.Text = secondPart.ToString();
            }
        }
    }
}