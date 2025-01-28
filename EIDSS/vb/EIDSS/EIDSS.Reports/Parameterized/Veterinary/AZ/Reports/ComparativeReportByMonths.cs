using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using bv.model.BLToolkit;

using eidss.model.Reports.AZ;
using EIDSS.Reports.Parameterized.Veterinary.AZ.DataSets;
using eidss.model.Reports.Common;
using DevExpress.XtraCharts;
using eidss.model.Reports;
using EIDSS.Reports.BaseControls.Report;

namespace EIDSS.Reports.Parameterized.Veterinary.AZ.Reports
{
    [CanWorkWithArchive]
    public partial class ComparativeReportByMonths : EIDSS.Reports.BaseControls.Report.BaseReport
    {
        public DataSet _diagramDataSet = new DataSet();
        public DataTable _diagramDataTable = new DataTable();
        ComponentResourceManager _resources = new ComponentResourceManager(typeof(ComparativeReportByMonths));

        public ComparativeReportByMonths()
        {
            InitializeComponent();
        }

        public void SetParameters(VetComparativeByMonthModel model,
            DbManagerProxy manager, DbManagerProxy archiveManager)
        {
            SetReportLabelsBase((SqlConnection)manager.Connection, model);

            comparativeReportByMonthsDataSet1.EnforceConstraints = false;

            ComparativeReportByMonthsDataSet.spRepVetComparativeAZDataTable dataTable = comparativeReportByMonthsDataSet1.spRepVetComparativeAZ;

            BaseReport.FillDataTableWithArchive((c, t) =>
            {
                spRepVetComparativeAZTableAdapter1.Connection = c;
                spRepVetComparativeAZTableAdapter1.Transaction = t;
                //spRepVetComparativeAZTableAdapter1.CommandTimeout = BaseReport.CommandTimeout;

                spRepVetComparativeAZTableAdapter1.Fill(
                    dataTable,
                    model.Language,
                    model.YearFrom,
                    model.YearTo,
                    model.RegionId,
                    model.RayonId,
                    model.DiagnosisId,
                    model.GetSpeciesAsXml());
            },
                manager, archiveManager,
                dataTable,
                model.Mode,
                new[] { "strRowName" });

            //SetTestData(dataTable);

            GenerateDiagramDataSet(model, dataTable);
        }

        private void SetReportLabelsBase(SqlConnection cn, VetComparativeByMonthModel model)
        {
            string organizationName = String.Empty;

            if (model.OrganizationId.HasValue)
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.sprepGetBaseParameters";

                cmd.Parameters.Add("@LangID", SqlDbType.VarChar, 36).Value = model.Language;
                cmd.Parameters.Add("@OrganizationId", SqlDbType.BigInt).Value = model.OrganizationId.Value;
                if (model.SiteId.HasValue)
                {
                    cmd.Parameters.Add("@SiteID", SqlDbType.BigInt).Value = model.SiteId.Value;
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

            string yearPart;
            string yearEndingPart;
            GenerateCaptionParams(model, out yearPart, out yearEndingPart);
            xrLabelBeginOfTheCaption.Text = String.Format(xrLabelBeginOfTheCaption.Text, yearPart, yearEndingPart);

            xrLabelRegionsAndSpecies.Text = GenarateSubCaptionString(model);

            if (model.StateType == VetComparativeByMonthModel.ModelStateType.ByYear)
            {
                xrTableCellCaption.Text = _resources.GetString("Year_Key");
            }
            else if (model.StateType == VetComparativeByMonthModel.ModelStateType.BySpecies)
            {
                xrTableCellCaption.Text = _resources.GetString("Specie_Key");
            }
        }

        private void GenerateCaptionParams(VetComparativeByMonthModel model, out string year, out string yearEnding)
        {
            if (model.YearFrom == model.YearTo)
            {
                year = Convert.ToString(model.YearFrom);
                yearEnding = _resources.GetString("YearWordText");
            }
            else
            {
                year = String.Format("{0} - {1}", model.YearFrom, model.YearTo);
                yearEnding = _resources.GetString("YearsWordText");
            }
        }

        private void AddSpecies(StringBuilder strBuilder, VetComparativeByMonthModel model)
        {
            bool wasAtLeastOneAdd = InsertCommaIfItNeeds(strBuilder, model);

            for (int i = 0; i < model.Species.Length; ++i)
            {
                strBuilder.Append(model.Species[i]);
                strBuilder.Append(", ");
            }

            if (model.Species.Length > 0 || wasAtLeastOneAdd)
            {
                strBuilder.Remove(strBuilder.Length - 2, 2);
            }
        }

        private void AddRegionAndRayons(StringBuilder strBuilder, VetComparativeByMonthModel model)
        {
            bool wasAtLeastOneAdd = InsertCommaIfItNeeds(strBuilder, model);

            if (!String.IsNullOrEmpty(model.RayonName) && model.RegionId != AddressModel.OtherRayonsId && model.RegionId != AddressModel.BakuId)
            {
                strBuilder.Append(model.RayonName);
                strBuilder.Append(", ");

                strBuilder.Append(model.RegionName);
                strBuilder.Append(", ");

                wasAtLeastOneAdd = true;
            }
            else if (String.IsNullOrEmpty(model.RayonName) && !String.IsNullOrEmpty(model.RegionName))
            {
                strBuilder.Append(model.RegionName);
                strBuilder.Append(", ");
                wasAtLeastOneAdd = true;
            }
            else
            {
                if (!String.IsNullOrEmpty(model.RegionName))
                {
                    if (model.RegionId != AddressModel.OtherRayonsId)
                    {
                        strBuilder.Append(model.RegionName);
                        strBuilder.Append(", ");
                        wasAtLeastOneAdd = true;
                    }

                    if (!String.IsNullOrEmpty(model.RayonName))
                    {
                        strBuilder.Append(model.RayonName);
                        strBuilder.Append(", ");
                        wasAtLeastOneAdd = true;
                    }
                }
            }

            if (String.IsNullOrEmpty(model.RegionName))
            {
                strBuilder.Append(_resources.GetString("Azerbaijan"));
                strBuilder.Append(", ");
                wasAtLeastOneAdd = true;
            }

            if (wasAtLeastOneAdd)
            {
                strBuilder.Remove(strBuilder.Length - 2, 2);
            }
        }

        private void AddDiagnosis(StringBuilder strBuilder, VetComparativeByMonthModel model)
        {
            if (!String.IsNullOrEmpty(model.Diagnosis))
            {
                InsertCommaIfItNeeds(strBuilder, model);

                strBuilder.Append(model.Diagnosis);
            }
        }

        private bool InsertCommaIfItNeeds(StringBuilder strBuilder, VetComparativeByMonthModel model)
        {
            bool shouldWeAddComma = false;
            for (int i = strBuilder.Length - 1; i > 0; --i)
            {
                if (strBuilder[i] == ' ')
                {
                    continue;
                }
                else if (strBuilder[i] == ',')
                {
                    break;
                }
                else if (strBuilder[i] == '\n' || strBuilder[i] == '\r')
                {
                    break;
                }
                else
                {
                    shouldWeAddComma = true;
                    break;
                }
            }

            if (shouldWeAddComma)
            {
                strBuilder.Append(", ");
            }

            return shouldWeAddComma;
        }

        private string GenarateSubCaptionString(VetComparativeByMonthModel model)
        {
            StringBuilder subCaption = new StringBuilder();

            if (model.StateType == VetComparativeByMonthModel.ModelStateType.BySpecies)
            {
                AddSpecies(subCaption, model);

                subCaption.AppendLine();

                AddRegionAndRayons(subCaption, model);

                AddDiagnosis(subCaption, model);
            }
            else if (model.StateType == VetComparativeByMonthModel.ModelStateType.ByYear)
            {
                AddRegionAndRayons(subCaption, model);

                AddDiagnosis(subCaption, model);

                AddSpecies(subCaption, model);
            }
            else
            {
                subCaption.Append("WrongState");
            }

            return subCaption.ToString();
        }

        private void GenerateDiagramDataSet(VetComparativeByMonthModel model,
            ComparativeReportByMonthsDataSet.spRepVetComparativeAZDataTable sourceTable)
        {
            _diagramDataTable.Columns.Add("Months", typeof(string));

            if (model.StateType == VetComparativeByMonthModel.ModelStateType.ByYear)
            {
                for (int i = model.YearFrom; i <= model.YearTo; ++i)
                {
                    _diagramDataTable.Columns.Add(Convert.ToString(i), typeof(int));
                }
            }
            else if (model.StateType == VetComparativeByMonthModel.ModelStateType.BySpecies)
            {
                for (int i = 0; i < model.Species.Length; ++i)
                {
                    _diagramDataTable.Columns.Add(model.Species[i], typeof(int));
                }
            }
            else
            {
                throw new ApplicationException("Wrong VetComparativeByMonthModel state.");
            }

            _diagramDataTable.AcceptChanges();

            AddMonthRows(_diagramDataTable);

            for (int i = 0; i < sourceTable.Rows.Count; ++i)
            {
                for (int j = 2; j < sourceTable.Columns.Count - 1; ++j)
                {
                    _diagramDataTable.Rows[j - 2][i + 1] = sourceTable.Rows[i][j];
                }
            }

            _diagramDataSet.Tables.Add(_diagramDataTable);
            _diagramDataTable.AcceptChanges();

            xrChart.BeginInit();

            xrChart.DataSource = _diagramDataSet;

            for (int i = 1; i < _diagramDataTable.Columns.Count; ++i)
            {
                Series newSerie = new Series();
                newSerie.LegendText = _diagramDataTable.Columns[i].ColumnName;
                newSerie.DataSource = _diagramDataTable;
                newSerie.ArgumentDataMember = "Months";
                newSerie.ValueDataMembers.AddRange(_diagramDataTable.Columns[i].ColumnName);
                xrChart.Series.Add(newSerie);
            }

            if (IsTableEmpty(sourceTable))
            {
                XYDiagram xyDiagram1 = new XYDiagram();

                var myAxisY = xyDiagram1.AxisY;
                myAxisY.WholeRange.MinValue = 0;
                myAxisY.WholeRange.SideMarginsValue = 0;
                myAxisY.WholeRange.MaxValue = 0.5;

                xrChart.Diagram = xyDiagram1;
            }

            xrChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xrChart.EndInit();
        }

        private void AddMonthRows(DataTable dt)
        {
            dt.Rows.Add(_resources.GetString("xrTableCellJanuary.Text"));
            dt.Rows.Add(_resources.GetString("xrTableCellFebruary.Text"));
            dt.Rows.Add(_resources.GetString("xrTableCellMarch.Text"));
            dt.Rows.Add(_resources.GetString("xrTableCellApril.Text"));
            dt.Rows.Add(_resources.GetString("xrTableCellMay.Text"));
            dt.Rows.Add(_resources.GetString("xrTableCellJune.Text"));
            dt.Rows.Add(_resources.GetString("xrTableCellJuly.Text"));
            dt.Rows.Add(_resources.GetString("xrTableCellAugust.Text"));
            dt.Rows.Add(_resources.GetString("xrTableCellSeptember.Text"));
            dt.Rows.Add(_resources.GetString("xrTableCellOctober.Text"));
            dt.Rows.Add(_resources.GetString("xrTableCellNovember.Text"));
            dt.Rows.Add(_resources.GetString("xrTableCellDecember.Text"));
        }

        private bool IsTableEmpty(ComparativeReportByMonthsDataSet.spRepVetComparativeAZDataTable dtSourceData)
        {
            int total = 0;
            for (int i = 0; i < dtSourceData.Rows.Count; ++i)
            {
                total += (int)dtSourceData.Rows[i]["total"];
            }

            return total == 0;
        }

        private void SetTestData(ComparativeReportByMonthsDataSet.spRepVetComparativeAZDataTable sourceTable)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < sourceTable.Rows.Count; ++i)
            {
                int sum = 0;
                int j = 2;
                for (; j < sourceTable.Columns.Count - 1; ++j)
                {
                    int cValue = r.Next(0, 60);
                    sum += cValue;
                    sourceTable.Rows[i][j] = cValue;
                }
                sourceTable.Rows[i][j] = sum;
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComparativeReportByMonths));
            this.xrLabelBeginOfTheCaption = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelRegionsAndSpecies = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellCaption = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellJanuary = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellFebruary = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellMarch = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellApril = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellMay = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellJune = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellJuly = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellAugust = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellSeptember = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellOctober = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNovember = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellDecember = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotal = new DevExpress.XtraReports.UI.XRTableCell();
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellCaptionData = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellJanuaryData = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellFebruaryData = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellMarchData = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellAprilData = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellMayData = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellJuneData = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellJulyData = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellAugustData = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellSeptemberData = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellOctoberData = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNovemberData = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellDecemberData = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotalData = new DevExpress.XtraReports.UI.XRTableCell();
            this.spRepVetComparativeAZTableAdapter1 = new EIDSS.Reports.Parameterized.Veterinary.AZ.DataSets.ComparativeReportByMonthsDataSetTableAdapters.spRepVetComparativeAZTableAdapter();
            this.comparativeReportByMonthsDataSet1 = new EIDSS.Reports.Parameterized.Veterinary.AZ.DataSets.ComparativeReportByMonthsDataSet();
            this.xrLabelCurrentOrganization = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelPrintedFromEIDSS = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelPrintedDateAndTime = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrChart = new DevExpress.XtraReports.UI.XRChart();
            ((System.ComponentModel.ISupportInitialize)(this.m_BaseDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableBaseHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comparativeReportByMonthsDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // cellLanguage
            // 
            resources.ApplyResources(this.cellLanguage, "cellLanguage");
            this.cellLanguage.StylePriority.UseTextAlignment = false;
            // 
            // lblReportName
            // 
            resources.ApplyResources(this.lblReportName, "lblReportName");
            this.lblReportName.StylePriority.UseBorders = false;
            this.lblReportName.StylePriority.UseBorderWidth = false;
            this.lblReportName.StylePriority.UseFont = false;
            this.lblReportName.StylePriority.UseTextAlignment = false;
            // 
            // Detail
            // 
            resources.ApplyResources(this.Detail, "Detail");
            this.Detail.StylePriority.UseFont = false;
            this.Detail.StylePriority.UsePadding = false;
            // 
            // PageHeader
            // 
            resources.ApplyResources(this.PageHeader, "PageHeader");
            this.PageHeader.Expanded = false;
            this.PageHeader.PrintOn = ((DevExpress.XtraReports.UI.PrintOnPages)((DevExpress.XtraReports.UI.PrintOnPages.NotWithReportHeader | DevExpress.XtraReports.UI.PrintOnPages.NotWithReportFooter)));
            this.PageHeader.StylePriority.UseFont = false;
            this.PageHeader.StylePriority.UsePadding = false;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabelCurrentOrganization,
            this.xrLabelPrintedFromEIDSS,
            this.xrLabelPrintedDateAndTime});
            resources.ApplyResources(this.PageFooter, "PageFooter");
            this.PageFooter.StylePriority.UseBorders = false;
            this.PageFooter.Controls.SetChildIndex(this.xrPageInfo1, 0);
            this.PageFooter.Controls.SetChildIndex(this.xrLabelPrintedDateAndTime, 0);
            this.PageFooter.Controls.SetChildIndex(this.xrLabelPrintedFromEIDSS, 0);
            this.PageFooter.Controls.SetChildIndex(this.xrLabelCurrentOrganization, 0);
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabelRegionsAndSpecies,
            this.xrLabelBeginOfTheCaption});
            resources.ApplyResources(this.ReportHeader, "ReportHeader");
            this.ReportHeader.Controls.SetChildIndex(this.tableBaseHeader, 0);
            this.ReportHeader.Controls.SetChildIndex(this.xrLabelBeginOfTheCaption, 0);
            this.ReportHeader.Controls.SetChildIndex(this.xrLabelRegionsAndSpecies, 0);
            // 
            // xrPageInfo1
            // 
            resources.ApplyResources(this.xrPageInfo1, "xrPageInfo1");
            this.xrPageInfo1.StylePriority.UseBorders = false;
            // 
            // cellReportHeader
            // 
            resources.ApplyResources(this.cellReportHeader, "cellReportHeader");
            this.cellReportHeader.StylePriority.UseBorders = false;
            this.cellReportHeader.StylePriority.UseFont = false;
            this.cellReportHeader.StylePriority.UseTextAlignment = false;
            // 
            // cellBaseSite
            // 
            resources.ApplyResources(this.cellBaseSite, "cellBaseSite");
            this.cellBaseSite.StylePriority.UseBorders = false;
            this.cellBaseSite.StylePriority.UseFont = false;
            this.cellBaseSite.StylePriority.UseTextAlignment = false;
            // 
            // cellBaseCountry
            // 
            resources.ApplyResources(this.cellBaseCountry, "cellBaseCountry");
            // 
            // cellBaseLeftHeader
            // 
            resources.ApplyResources(this.cellBaseLeftHeader, "cellBaseLeftHeader");
            // 
            // tableBaseHeader
            // 
            resources.ApplyResources(this.tableBaseHeader, "tableBaseHeader");
            this.tableBaseHeader.StylePriority.UseBorders = false;
            this.tableBaseHeader.StylePriority.UseBorderWidth = false;
            this.tableBaseHeader.StylePriority.UseFont = false;
            this.tableBaseHeader.StylePriority.UsePadding = false;
            this.tableBaseHeader.StylePriority.UseTextAlignment = false;
            // 
            // xrLabelBeginOfTheCaption
            // 
            resources.ApplyResources(this.xrLabelBeginOfTheCaption, "xrLabelBeginOfTheCaption");
            this.xrLabelBeginOfTheCaption.Name = "xrLabelBeginOfTheCaption";
            this.xrLabelBeginOfTheCaption.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelBeginOfTheCaption.StylePriority.UseFont = false;
            this.xrLabelBeginOfTheCaption.StylePriority.UseTextAlignment = false;
            // 
            // xrLabelRegionsAndSpecies
            // 
            resources.ApplyResources(this.xrLabelRegionsAndSpecies, "xrLabelRegionsAndSpecies");
            this.xrLabelRegionsAndSpecies.Multiline = true;
            this.xrLabelRegionsAndSpecies.Name = "xrLabelRegionsAndSpecies";
            this.xrLabelRegionsAndSpecies.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelRegionsAndSpecies.StylePriority.UseFont = false;
            this.xrLabelRegionsAndSpecies.StylePriority.UseTextAlignment = false;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            resources.ApplyResources(this.GroupHeader1, "GroupHeader1");
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrTable1
            // 
            resources.ApplyResources(this.xrTable1, "xrTable1");
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellCaption,
            this.xrTableCellJanuary,
            this.xrTableCellFebruary,
            this.xrTableCellMarch,
            this.xrTableCellApril,
            this.xrTableCellMay,
            this.xrTableCellJune,
            this.xrTableCellJuly,
            this.xrTableCellAugust,
            this.xrTableCellSeptember,
            this.xrTableCellOctober,
            this.xrTableCellNovember,
            this.xrTableCellDecember,
            this.xrTableCellTotal});
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            this.xrTableRow1.Name = "xrTableRow1";
            // 
            // xrTableCellCaption
            // 
            this.xrTableCellCaption.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            resources.ApplyResources(this.xrTableCellCaption, "xrTableCellCaption");
            this.xrTableCellCaption.Name = "xrTableCellCaption";
            this.xrTableCellCaption.StylePriority.UseBorders = false;
            this.xrTableCellCaption.StylePriority.UseFont = false;
            // 
            // xrTableCellJanuary
            // 
            this.xrTableCellJanuary.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            resources.ApplyResources(this.xrTableCellJanuary, "xrTableCellJanuary");
            this.xrTableCellJanuary.Name = "xrTableCellJanuary";
            this.xrTableCellJanuary.StylePriority.UseBorders = false;
            this.xrTableCellJanuary.StylePriority.UseFont = false;
            // 
            // xrTableCellFebruary
            // 
            this.xrTableCellFebruary.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            resources.ApplyResources(this.xrTableCellFebruary, "xrTableCellFebruary");
            this.xrTableCellFebruary.Name = "xrTableCellFebruary";
            this.xrTableCellFebruary.StylePriority.UseBorders = false;
            this.xrTableCellFebruary.StylePriority.UseFont = false;
            // 
            // xrTableCellMarch
            // 
            this.xrTableCellMarch.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            resources.ApplyResources(this.xrTableCellMarch, "xrTableCellMarch");
            this.xrTableCellMarch.Name = "xrTableCellMarch";
            this.xrTableCellMarch.StylePriority.UseBorders = false;
            this.xrTableCellMarch.StylePriority.UseFont = false;
            // 
            // xrTableCellApril
            // 
            this.xrTableCellApril.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            resources.ApplyResources(this.xrTableCellApril, "xrTableCellApril");
            this.xrTableCellApril.Name = "xrTableCellApril";
            this.xrTableCellApril.StylePriority.UseBorders = false;
            this.xrTableCellApril.StylePriority.UseFont = false;
            // 
            // xrTableCellMay
            // 
            this.xrTableCellMay.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            resources.ApplyResources(this.xrTableCellMay, "xrTableCellMay");
            this.xrTableCellMay.Name = "xrTableCellMay";
            this.xrTableCellMay.StylePriority.UseBorders = false;
            this.xrTableCellMay.StylePriority.UseFont = false;
            // 
            // xrTableCellJune
            // 
            this.xrTableCellJune.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            resources.ApplyResources(this.xrTableCellJune, "xrTableCellJune");
            this.xrTableCellJune.Name = "xrTableCellJune";
            this.xrTableCellJune.StylePriority.UseBorders = false;
            this.xrTableCellJune.StylePriority.UseFont = false;
            // 
            // xrTableCellJuly
            // 
            this.xrTableCellJuly.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            resources.ApplyResources(this.xrTableCellJuly, "xrTableCellJuly");
            this.xrTableCellJuly.Name = "xrTableCellJuly";
            this.xrTableCellJuly.StylePriority.UseBorders = false;
            this.xrTableCellJuly.StylePriority.UseFont = false;
            // 
            // xrTableCellAugust
            // 
            this.xrTableCellAugust.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            resources.ApplyResources(this.xrTableCellAugust, "xrTableCellAugust");
            this.xrTableCellAugust.Name = "xrTableCellAugust";
            this.xrTableCellAugust.StylePriority.UseBorders = false;
            this.xrTableCellAugust.StylePriority.UseFont = false;
            // 
            // xrTableCellSeptember
            // 
            this.xrTableCellSeptember.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            resources.ApplyResources(this.xrTableCellSeptember, "xrTableCellSeptember");
            this.xrTableCellSeptember.Name = "xrTableCellSeptember";
            this.xrTableCellSeptember.StylePriority.UseBorders = false;
            this.xrTableCellSeptember.StylePriority.UseFont = false;
            // 
            // xrTableCellOctober
            // 
            this.xrTableCellOctober.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            resources.ApplyResources(this.xrTableCellOctober, "xrTableCellOctober");
            this.xrTableCellOctober.Name = "xrTableCellOctober";
            this.xrTableCellOctober.StylePriority.UseBorders = false;
            this.xrTableCellOctober.StylePriority.UseFont = false;
            // 
            // xrTableCellNovember
            // 
            this.xrTableCellNovember.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            resources.ApplyResources(this.xrTableCellNovember, "xrTableCellNovember");
            this.xrTableCellNovember.Name = "xrTableCellNovember";
            this.xrTableCellNovember.StylePriority.UseBorders = false;
            this.xrTableCellNovember.StylePriority.UseFont = false;
            // 
            // xrTableCellDecember
            // 
            this.xrTableCellDecember.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            resources.ApplyResources(this.xrTableCellDecember, "xrTableCellDecember");
            this.xrTableCellDecember.Name = "xrTableCellDecember";
            this.xrTableCellDecember.StylePriority.UseBorders = false;
            this.xrTableCellDecember.StylePriority.UseFont = false;
            // 
            // xrTableCellTotal
            // 
            this.xrTableCellTotal.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)));
            resources.ApplyResources(this.xrTableCellTotal, "xrTableCellTotal");
            this.xrTableCellTotal.Name = "xrTableCellTotal";
            this.xrTableCellTotal.StylePriority.UseBorders = false;
            this.xrTableCellTotal.StylePriority.UseFont = false;
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1});
            this.DetailReport.DataAdapter = this.spRepVetComparativeAZTableAdapter1;
            this.DetailReport.DataMember = "spRepVetComparativeAZ";
            this.DetailReport.DataSource = this.comparativeReportByMonthsDataSet1;
            resources.ApplyResources(this.DetailReport, "DetailReport");
            this.DetailReport.Level = 0;
            this.DetailReport.Name = "DetailReport";
            // 
            // Detail1
            // 
            this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            resources.ApplyResources(this.Detail1, "Detail1");
            this.Detail1.KeepTogether = true;
            this.Detail1.KeepTogetherWithDetailReports = true;
            this.Detail1.Name = "Detail1";
            // 
            // xrTable2
            // 
            resources.ApplyResources(this.xrTable2, "xrTable2");
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.StylePriority.UseFont = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellCaptionData,
            this.xrTableCellJanuaryData,
            this.xrTableCellFebruaryData,
            this.xrTableCellMarchData,
            this.xrTableCellAprilData,
            this.xrTableCellMayData,
            this.xrTableCellJuneData,
            this.xrTableCellJulyData,
            this.xrTableCellAugustData,
            this.xrTableCellSeptemberData,
            this.xrTableCellOctoberData,
            this.xrTableCellNovemberData,
            this.xrTableCellDecemberData,
            this.xrTableCellTotalData});
            resources.ApplyResources(this.xrTableRow2, "xrTableRow2");
            this.xrTableRow2.Name = "xrTableRow2";
            // 
            // xrTableCellCaptionData
            // 
            this.xrTableCellCaptionData.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellCaptionData.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "spRepVetComparativeAZ.strRowName")});
            resources.ApplyResources(this.xrTableCellCaptionData, "xrTableCellCaptionData");
            this.xrTableCellCaptionData.Name = "xrTableCellCaptionData";
            this.xrTableCellCaptionData.StylePriority.UseBorders = false;
            this.xrTableCellCaptionData.StylePriority.UseFont = false;
            // 
            // xrTableCellJanuaryData
            // 
            this.xrTableCellJanuaryData.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellJanuaryData.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "spRepVetComparativeAZ.intJanuary")});
            resources.ApplyResources(this.xrTableCellJanuaryData, "xrTableCellJanuaryData");
            this.xrTableCellJanuaryData.Name = "xrTableCellJanuaryData";
            this.xrTableCellJanuaryData.StylePriority.UseBorders = false;
            // 
            // xrTableCellFebruaryData
            // 
            this.xrTableCellFebruaryData.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellFebruaryData.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "spRepVetComparativeAZ.intFebruary")});
            resources.ApplyResources(this.xrTableCellFebruaryData, "xrTableCellFebruaryData");
            this.xrTableCellFebruaryData.Name = "xrTableCellFebruaryData";
            this.xrTableCellFebruaryData.StylePriority.UseBorders = false;
            // 
            // xrTableCellMarchData
            // 
            this.xrTableCellMarchData.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellMarchData.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "spRepVetComparativeAZ.intMarch")});
            resources.ApplyResources(this.xrTableCellMarchData, "xrTableCellMarchData");
            this.xrTableCellMarchData.Name = "xrTableCellMarchData";
            this.xrTableCellMarchData.StylePriority.UseBorders = false;
            // 
            // xrTableCellAprilData
            // 
            this.xrTableCellAprilData.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellAprilData.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "spRepVetComparativeAZ.intApril")});
            resources.ApplyResources(this.xrTableCellAprilData, "xrTableCellAprilData");
            this.xrTableCellAprilData.Name = "xrTableCellAprilData";
            this.xrTableCellAprilData.StylePriority.UseBorders = false;
            // 
            // xrTableCellMayData
            // 
            this.xrTableCellMayData.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellMayData.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "spRepVetComparativeAZ.intMay")});
            resources.ApplyResources(this.xrTableCellMayData, "xrTableCellMayData");
            this.xrTableCellMayData.Name = "xrTableCellMayData";
            this.xrTableCellMayData.StylePriority.UseBorders = false;
            // 
            // xrTableCellJuneData
            // 
            this.xrTableCellJuneData.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellJuneData.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "spRepVetComparativeAZ.intJune")});
            resources.ApplyResources(this.xrTableCellJuneData, "xrTableCellJuneData");
            this.xrTableCellJuneData.Name = "xrTableCellJuneData";
            this.xrTableCellJuneData.StylePriority.UseBorders = false;
            // 
            // xrTableCellJulyData
            // 
            this.xrTableCellJulyData.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellJulyData.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "spRepVetComparativeAZ.intJuly")});
            resources.ApplyResources(this.xrTableCellJulyData, "xrTableCellJulyData");
            this.xrTableCellJulyData.Name = "xrTableCellJulyData";
            this.xrTableCellJulyData.StylePriority.UseBorders = false;
            // 
            // xrTableCellAugustData
            // 
            this.xrTableCellAugustData.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellAugustData.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "spRepVetComparativeAZ.intAugust")});
            resources.ApplyResources(this.xrTableCellAugustData, "xrTableCellAugustData");
            this.xrTableCellAugustData.Name = "xrTableCellAugustData";
            this.xrTableCellAugustData.StylePriority.UseBorders = false;
            // 
            // xrTableCellSeptemberData
            // 
            this.xrTableCellSeptemberData.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellSeptemberData.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "spRepVetComparativeAZ.intSeptember")});
            resources.ApplyResources(this.xrTableCellSeptemberData, "xrTableCellSeptemberData");
            this.xrTableCellSeptemberData.Name = "xrTableCellSeptemberData";
            this.xrTableCellSeptemberData.StylePriority.UseBorders = false;
            // 
            // xrTableCellOctoberData
            // 
            this.xrTableCellOctoberData.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellOctoberData.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "spRepVetComparativeAZ.intOctober")});
            resources.ApplyResources(this.xrTableCellOctoberData, "xrTableCellOctoberData");
            this.xrTableCellOctoberData.Name = "xrTableCellOctoberData";
            this.xrTableCellOctoberData.StylePriority.UseBorders = false;
            // 
            // xrTableCellNovemberData
            // 
            this.xrTableCellNovemberData.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNovemberData.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "spRepVetComparativeAZ.intNovember")});
            resources.ApplyResources(this.xrTableCellNovemberData, "xrTableCellNovemberData");
            this.xrTableCellNovemberData.Name = "xrTableCellNovemberData";
            this.xrTableCellNovemberData.StylePriority.UseBorders = false;
            // 
            // xrTableCellDecemberData
            // 
            this.xrTableCellDecemberData.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellDecemberData.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "spRepVetComparativeAZ.intDecember")});
            resources.ApplyResources(this.xrTableCellDecemberData, "xrTableCellDecemberData");
            this.xrTableCellDecemberData.Name = "xrTableCellDecemberData";
            this.xrTableCellDecemberData.StylePriority.UseBorders = false;
            // 
            // xrTableCellTotalData
            // 
            this.xrTableCellTotalData.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTotalData.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "spRepVetComparativeAZ.total")});
            resources.ApplyResources(this.xrTableCellTotalData, "xrTableCellTotalData");
            this.xrTableCellTotalData.Name = "xrTableCellTotalData";
            this.xrTableCellTotalData.StylePriority.UseBorders = false;
            // 
            // spRepVetComparativeAZTableAdapter1
            // 
            this.spRepVetComparativeAZTableAdapter1.ClearBeforeFill = true;
            // 
            // comparativeReportByMonthsDataSet1
            // 
            this.comparativeReportByMonthsDataSet1.DataSetName = "ComparativeReportByMonthsDataSet";
            this.comparativeReportByMonthsDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // xrLabelCurrentOrganization
            // 
            this.xrLabelCurrentOrganization.Borders = DevExpress.XtraPrinting.BorderSide.None;
            resources.ApplyResources(this.xrLabelCurrentOrganization, "xrLabelCurrentOrganization");
            this.xrLabelCurrentOrganization.Multiline = true;
            this.xrLabelCurrentOrganization.Name = "xrLabelCurrentOrganization";
            this.xrLabelCurrentOrganization.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelCurrentOrganization.StylePriority.UseBorders = false;
            this.xrLabelCurrentOrganization.StylePriority.UseFont = false;
            // 
            // xrLabelPrintedFromEIDSS
            // 
            this.xrLabelPrintedFromEIDSS.Borders = DevExpress.XtraPrinting.BorderSide.None;
            resources.ApplyResources(this.xrLabelPrintedFromEIDSS, "xrLabelPrintedFromEIDSS");
            this.xrLabelPrintedFromEIDSS.Name = "xrLabelPrintedFromEIDSS";
            this.xrLabelPrintedFromEIDSS.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 10, 0, 0, 100F);
            this.xrLabelPrintedFromEIDSS.StylePriority.UseBorders = false;
            this.xrLabelPrintedFromEIDSS.StylePriority.UseFont = false;
            this.xrLabelPrintedFromEIDSS.StylePriority.UsePadding = false;
            this.xrLabelPrintedFromEIDSS.StylePriority.UseTextAlignment = false;
            // 
            // xrLabelPrintedDateAndTime
            // 
            this.xrLabelPrintedDateAndTime.Borders = DevExpress.XtraPrinting.BorderSide.None;
            resources.ApplyResources(this.xrLabelPrintedDateAndTime, "xrLabelPrintedDateAndTime");
            this.xrLabelPrintedDateAndTime.Name = "xrLabelPrintedDateAndTime";
            this.xrLabelPrintedDateAndTime.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelPrintedDateAndTime.StylePriority.UseBorders = false;
            this.xrLabelPrintedDateAndTime.StylePriority.UseFont = false;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrChart});
            resources.ApplyResources(this.GroupFooter1, "GroupFooter1");
            this.GroupFooter1.GroupUnion = DevExpress.XtraReports.UI.GroupFooterUnion.WithLastDetail;
            this.GroupFooter1.Name = "GroupFooter1";
            this.GroupFooter1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.GroupFooter1.PrintAtBottom = true;
            this.GroupFooter1.StylePriority.UsePadding = false;
            // 
            // xrChart
            // 
            this.xrChart.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom;
            resources.ApplyResources(this.xrChart, "xrChart");
            this.xrChart.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrChart.DataSource = this.comparativeReportByMonthsDataSet1;
            this.xrChart.Name = "xrChart";
            this.xrChart.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 5, 50, 96F);
            this.xrChart.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.xrChart.StylePriority.UsePadding = false;
            // 
            // ComparativeReportByMonths
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.PageFooter,
            this.ReportHeader,
            this.GroupHeader1,
            this.DetailReport,
            this.GroupFooter1});
            this.DataAdapter = null;
            this.DataMember = "";
            this.DataSource = null;
            resources.ApplyResources(this, "$this");
            this.Version = "15.1";
            this.Controls.SetChildIndex(this.GroupFooter1, 0);
            this.Controls.SetChildIndex(this.DetailReport, 0);
            this.Controls.SetChildIndex(this.GroupHeader1, 0);
            this.Controls.SetChildIndex(this.ReportHeader, 0);
            this.Controls.SetChildIndex(this.PageFooter, 0);
            this.Controls.SetChildIndex(this.PageHeader, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.m_BaseDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableBaseHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comparativeReportByMonthsDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        private XRLabel xrLabelRegionsAndSpecies;
        private GroupHeaderBand GroupHeader1;
        private XRTable xrTable1;
        private XRTableRow xrTableRow1;
        private XRTableCell xrTableCellCaption;
        private XRTableCell xrTableCellJanuary;
        private XRTableCell xrTableCellFebruary;
        private XRTableCell xrTableCellMarch;
        private XRTableCell xrTableCellApril;
        private XRTableCell xrTableCellMay;
        private XRTableCell xrTableCellJune;
        private XRTableCell xrTableCellJuly;
        private XRTableCell xrTableCellAugust;
        private XRTableCell xrTableCellSeptember;
        private XRTableCell xrTableCellOctober;
        private XRTableCell xrTableCellNovember;
        private XRTableCell xrTableCellDecember;
        private XRTableCell xrTableCellTotal;
        private DetailReportBand DetailReport;
        private DetailBand Detail1;
        private DataSets.ComparativeReportByMonthsDataSetTableAdapters.spRepVetComparativeAZTableAdapter spRepVetComparativeAZTableAdapter1;
        private DataSets.ComparativeReportByMonthsDataSet comparativeReportByMonthsDataSet1;
        private XRTable xrTable2;
        private XRTableRow xrTableRow2;
        private XRTableCell xrTableCellCaptionData;
        private XRTableCell xrTableCellJanuaryData;
        private XRTableCell xrTableCellFebruaryData;
        private XRTableCell xrTableCellMarchData;
        private XRTableCell xrTableCellAprilData;
        private XRTableCell xrTableCellMayData;
        private XRTableCell xrTableCellJuneData;
        private XRTableCell xrTableCellJulyData;
        private XRTableCell xrTableCellAugustData;
        private XRTableCell xrTableCellSeptemberData;
        private XRTableCell xrTableCellOctoberData;
        private XRTableCell xrTableCellNovemberData;
        private XRTableCell xrTableCellDecemberData;
        private XRTableCell xrTableCellTotalData;
        protected XRLabel xrLabelCurrentOrganization;
        protected XRLabel xrLabelPrintedFromEIDSS;
        protected XRLabel xrLabelPrintedDateAndTime;
        private GroupFooterBand GroupFooter1;
        private XRChart xrChart;
        private XRLabel xrLabelBeginOfTheCaption;
    }
}
