using DevExpress.XtraReports.UI;
using eidss.model.Reports.Common;
using System;
using System.Drawing;

namespace EIDSS.Reports.BaseControls
{
    public static class ReportUIHelper
    {
        public static void SetMonth(XRLabel startLabel, XRLabel endLabel, XRLabel middleLabel, int startValue, int endValue)
        {
            SettleSamePairOfLabels(startLabel, endLabel, middleLabel, FilterHelper.GetMonthName(startValue), FilterHelper.GetMonthName(endValue));
        }

        public static void SetYears(XRLabel startLabel, XRLabel endLabel, XRLabel middleLabel, int startValue, int endValue)
        {
            SettleSamePairOfLabels(startLabel, endLabel, middleLabel, Convert.ToString(startValue), Convert.ToString(endValue));
        }

        private static void SettleSamePairOfLabels(XRLabel startLabel, XRLabel endLabel, XRLabel middleLabel, string startValue, string endValue)
        {
            if (String.Compare(startValue, endValue, true) == 0)
            {
                startLabel.Text = endLabel.Text = String.Empty;
                startLabel.Visible = endLabel.Visible = false;

                const int widthDelta = 50;
                middleLabel.LocationF = new PointF(middleLabel.LocationF.X - widthDelta / 2, middleLabel.LocationF.Y);
                middleLabel.SizeF = new System.Drawing.SizeF(middleLabel.SizeF.Width + widthDelta, middleLabel.SizeF.Height);
                middleLabel.Text = startValue;
            }
            else
            {
                startLabel.Text = startValue;
                endLabel.Text = endValue;
            }
        }
    }
}
