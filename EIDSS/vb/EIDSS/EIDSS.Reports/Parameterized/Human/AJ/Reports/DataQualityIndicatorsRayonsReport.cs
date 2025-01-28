using System.ComponentModel;
using EIDSS.Reports.Parameterized.Human.AJ.DataSets;
using DevExpress.XtraReports.UI;
using System.Drawing.Printing;

namespace EIDSS.Reports.Parameterized.Human.AJ.Reports
{
    public partial class DataQualityIndicatorsRayonsReport : BaseDataQualityIndicatorsReport
    {
        public DataQualityIndicatorsRayonsReport()
        {
            InitializeComponent();
        }

        protected override void BindSummaryAvarage(DQIDataSet.spRepHumDataQualityIndicatorsRow resultRow)
        {
            SummaryScoreByIndicatorsTotalCell.Text = resultRow.dbl_AVG_SummaryScoreByIndicators.ToString(DoubleFormat);
        }

        protected override bool HideDiagnosisIfEmpty(string[] checkedDiagnosis)
        {
            bool hide = base.HideDiagnosisIfEmpty(checkedDiagnosis);
            if (hide)
            {
                ((ISupportInitialize) (FirstSignatureTable)).BeginInit();
                ((ISupportInitialize) (SecondSignatureTable)).BeginInit();
                FirstOrganizationNameCell.WidthF = SecondOrganizationNameCell.WidthF = RegionHeaderCell.WidthF + RayonHeaderCell.WidthF;
                FirstPrintedFromEidssCell.WidthF = SecondPrintedFromEidssCell.WidthF = NumberOfCasesHeaderCell.WidthF;
                FirstDateTimeCell.WidthF = SecondDateTimeCell.WidthF = xrTableCell1.WidthF;
                ((ISupportInitialize) (SecondSignatureTable)).EndInit();
                ((ISupportInitialize) (FirstSignatureTable)).EndInit();
            }
            return hide;
        }

        private void IndicatorCell_BeforePrint(object sender, PrintEventArgs e)
        {
            if (!(sender is XRTableCell))
            {
                return;
            }
            var currentCell = ((XRTableCell)sender);

            double nominator;
            long denominator;
            if (double.TryParse(currentCell.Text, out nominator) &&
                long.TryParse(NumberOfCasesDetailCell.Text, out denominator))
            {
                double result = (denominator == 0)
                    ? 0.00
                    : (1.0 * nominator) / denominator;

                currentCell.Text = result.ToString("0.00");
            }
        }

        private void SubTotalCell_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            var cell = (sender as XRTableCell);
            if (cell != null)
            {
                double sum = 0;

                foreach (var calculatedValue in e.CalculatedValues)
                {
                    double tempValue;

                    if (calculatedValue != null && double.TryParse(calculatedValue.ToString(), out tempValue))
                        sum += tempValue;
                }

                e.Result = sum / this.CurrentSubTotalCases;
                e.Handled = true;
            }
        }

    }
}