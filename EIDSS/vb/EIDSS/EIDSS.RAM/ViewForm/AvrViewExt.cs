using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using bv.common.Core;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using eidss.avr.db.Common;
using eidss.model.Avr.View;
using eidss.model.Helpers;
using bv.common.Configuration;

namespace eidss.avr.ViewForm
{
    public static class AvrViewExt
    {
        public static PdfExportHelper PdfHelper;

        public static void UpdatePdfCaptions(AvrView obj, GridView view)
        {
            bool prevValue = PdfHelper.ShouldCaptionBeWordWrapped;

            PdfHelper = new PdfExportHelper(obj, BaseSettings.GetSystemFont(true));

            if (prevValue != PdfHelper.ShouldCaptionBeWordWrapped)
            {
                for (int i = 0; i < view.Columns.Count; ++i)
                {
                    view.Columns[i].Caption = PdfHelper.ProcessString(
                        view.Columns[i].Caption);
                }
            }
        }

        #region Put AvrView Object in Grid Control

        // create in grid(control) the children of current view
        public static void AddToGrid(this AvrView obj, GridView view)
        {
            PdfHelper = new PdfExportHelper(obj, BaseSettings.GetSystemFont(true));

            view.Tag = obj;
            //            view.BeginInit();
            view.BeginUpdate();
            view.BeginSort();

            view.OptionsView.ColumnHeaderAutoHeight = DefaultBoolean.True;
            view.OptionsView.RowAutoHeight = true;

            Font reportFont = BaseSettings.GetSystemFont(true);
            
            view.AppearancePrint.EvenRow.Font = reportFont;
            view.AppearancePrint.FilterPanel.Font = reportFont;
            view.AppearancePrint.FooterPanel.Font = reportFont;
            view.AppearancePrint.GroupFooter.Font = reportFont;
            view.AppearancePrint.GroupRow.Font = reportFont;
            view.AppearancePrint.HeaderPanel.Font = reportFont;
            view.AppearancePrint.Lines.Font = reportFont;
            view.AppearancePrint.OddRow.Font = reportFont;
            view.AppearancePrint.Preview.Font = reportFont;
            view.AppearancePrint.Row.Font = reportFont;

            /*
             * DevExpress v. 15.1 does not adjust header hight for multiline columns.
             * Upgrading with a new version the following code should be used instead:
             * view.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
             */
            view.ColumnPanelRowHeight = AvrViewExt.PdfHelper.HeaderHieghtInPixels;

            CollectionBase bands = null;
            if (view is BandedGridView)
            {
                bands = ((BandedGridView) view).Bands;
                bands.Clear();
            }
            view.Columns.Clear();
            try
            {
                obj.AddToGrid(bands, view.Columns, view);

                //sorting
                List<AvrViewColumn> sortColumns = obj.GetSortColumns();
                var si = new GridColumnSortInfo[sortColumns.Count];
                int i = 0;
                foreach (AvrViewColumn col in sortColumns.OrderBy(x => x.SortOrder))
                {
                    si[i++] = new GridColumnSortInfo(view.Columns[col.UniquePath],
                        col.IsSortAscending ? ColumnSortOrder.Ascending : ColumnSortOrder.Descending);
                }
                view.SortInfo.ClearAndAddRange(si);
            }
            finally
            {

                view.EndSort();
                view.EndUpdate();
                //                view.EndInit();

            }
        }

        // create in grid(control) the children of current band
        private static void AddToGrid(this AvrViewBand obj, GridBandCollection collect)
        {
            GridBand grBand = collect.AddBand(obj.DisplayText);

            // only here we set properties of grid(control) band
            grBand.Tag = obj;
            grBand.Caption = obj.DisplayText;
            grBand.Visible = obj.IsVisible;
            grBand.Fixed = obj.IsFreezed ? FixedStyle.Left : FixedStyle.None;

            obj.AddToGrid(grBand.Children, grBand.Columns, grBand.View);
        }

        private static void AddToGrid(this BaseBand obj, CollectionBase bands, CollectionBase columns, GridView view)
        {
            // at first put bands that were reordered by user
            // after that put bands that were not reordered by user in pivot order
            foreach (AvrViewBand band in obj.Bands.FindAll(x => !x.IsToDelete).OrderBy(x => x.Order_ForUse))
            {
                band.AddToGrid((GridBandCollection)bands);
            }
            // at first put columns that were reordered by user
            // after that put columns that were not reordered by user in pivot order
            foreach (AvrViewColumn col in obj.Columns.FindAll(x => !x.IsToDelete).OrderBy(x => x.Order_ForUse))
            {
                col.AddToGrid(columns, view);
            }
        }

        // create in grid(control) the child - column of current band
        public static void AddToGrid(this AvrViewColumn obj, CollectionBase columns, GridView view)
        {
            GridColumn newCol = view.Columns.Add();
            newCol.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            newCol.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;

            newCol.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
            newCol.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;

            newCol.AppearanceHeader.Options.UseTextOptions = true;
            newCol.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;

            // only here we set properties of grid(control) column
            newCol.Tag = obj;
            newCol.Caption = PdfHelper.ProcessString(obj.DisplayText);
            newCol.FieldName = obj.UniquePath; // link to datasource
            // for export to pdf
            newCol.ColumnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            newCol.Visible = obj.IsVisible;
            newCol.Fixed = obj.IsFreezed ? FixedStyle.Left : FixedStyle.None;
            //if (obj.ColumnWidth != null)
            newCol.Width = obj.ColumnWidth;
            //else
            //{
            //    if (obj.ColumnWidth_Pivot != null)
            //        newCol.Width = (int)obj.ColumnWidth_Pivot;
            //}
            if (!string.IsNullOrEmpty(obj.ColumnFilter))
                newCol.FilterInfo = new ColumnFilterInfo(obj.ColumnFilter);

            newCol.OptionsColumn.AllowEdit = false;
            newCol.OptionsFilter.FilterPopupMode = FilterPopupMode.Default;//CheckedList

            if (columns is GridColumnCollection)
            {
                ((GridColumnCollection) columns).Add(newCol);
            }
            else if (columns is GridBandColumnCollection)
            {
                ((GridBandColumnCollection) columns).Add((BandedGridColumn) newCol);
            }
        }

        public static void setColumnsTypes(this AvrView obj, GridView view, AggregateCashe aggrCache)
        {
            foreach (GridColumn gCol in view.Columns)
            {
                if (gCol.Tag != null)
                {
                    var col = (AvrViewColumn) gCol.Tag;
                    if (col.IsAggregate)
                    {
                        //bool ratio = col.SourceViewColumn != null &&
                        //             col.DenominatorViewColumn != null &&
                        //             view.Columns.ColumnByFieldName(col.SourceViewColumn).ColumnType == typeof(int) &&
                        //             view.Columns.ColumnByFieldName(col.DenominatorViewColumn).ColumnType == typeof(int);
                        col.setAggrColumnType(gCol, false, aggrCache);
                    }
                    else
                        col.setColumnType(gCol);
                }
            }
        }

        public static void setColumnType(this AvrViewColumn obj, GridColumn col)
        {
            obj.FieldType = col.ColumnType;

            if (col.ColumnType.Name == "AvrMonth")
            {
                col.DisplayFormat.FormatType = FormatType.Custom;
                col.DisplayFormat.FormatString = "m";
                col.DisplayFormat.Format = new CustomFormatter();
            }
            else if (col.ColumnType.Name == "DayOfWeek")
            {
                col.DisplayFormat.FormatType = FormatType.Custom;
                col.DisplayFormat.FormatString = "d";
                col.DisplayFormat.Format = new CustomFormatter();
            }
            else if (col.ColumnType.IsNumeric())
            {
                col.DisplayFormat.FormatType = FormatType.Numeric;
                col.DisplayFormat.FormatString = String.IsNullOrEmpty(obj.Precision_) ? "" : "N" + obj.Precision_;
            }
            else if (col.ColumnType.IsDate())
                col.DisplayFormat.FormatType = FormatType.DateTime;
        }

        public static void setAggrColumnType(this AvrViewColumn obj, GridColumn col, bool ratio, AggregateCashe aggrCache)
        {
            if (ratio)
            {
                obj.FieldType = typeof (string);
                col.UnboundType = UnboundColumnType.String;
            }
            else
            {
                switch (obj.AggregateFunction)
                {
                    case (long)ViewAggregateFunction.CumulativePercent:
                        col.OptionsColumn.AllowSort = DefaultBoolean.False;
                        obj.FieldType = typeof(double);
                        col.UnboundType = UnboundColumnType.Decimal;
                        col.DisplayFormat.FormatType = FormatType.Numeric;
                        col.DisplayFormat.FormatString = "P" + obj.Precision_;
                        break;
                    case (long)ViewAggregateFunction.PercentOfColumn:
                    case (long)ViewAggregateFunction.PercentOfRow:
                        obj.FieldType = typeof (double);
                        col.UnboundType = UnboundColumnType.Decimal;
                        col.DisplayFormat.FormatType = FormatType.Numeric;
                        col.DisplayFormat.FormatString = "P" + obj.Precision_;
                        break;
                    case (long)ViewAggregateFunction.Proportion:
                        obj.FieldType = typeof (double);
                        col.UnboundType = UnboundColumnType.Decimal;
                        col.DisplayFormat.FormatType = FormatType.Numeric;
                        col.DisplayFormat.FormatString = String.IsNullOrEmpty(obj.Precision_) ? "" : "N" + obj.Precision_;
                        break;
                    case (long)ViewAggregateFunction.Ratio:
                        obj.FieldType = typeof (string);
                        col.UnboundType = UnboundColumnType.String;
                        break;
                    case (long)ViewAggregateFunction.MaxOfRow:
                    case (long)ViewAggregateFunction.MinOfRow:
                        AvrViewColumn neibN = obj.Owner.Columns.Find(n => !n.IsToDelete && n.IsVisible && !n.IsAggregate && n.FieldType.IsNumeric());
                        AvrViewColumn neibD = obj.Owner.Columns.Find(n => !n.IsToDelete && n.IsVisible && !n.IsAggregate && n.FieldType == typeof(DateTime));
                        if (neibN == null || neibD == null)
                        {
                            if (neibN != null)
                            {
                                obj.FieldType = typeof (double);
                                col.UnboundType = UnboundColumnType.Decimal;
                                col.DisplayFormat.FormatType = FormatType.Numeric;
                                col.DisplayFormat.FormatString = String.IsNullOrEmpty(obj.Precision_) ? "" : "N" + obj.Precision_;
                            }
                            else
                            {
                                obj.FieldType = typeof (DateTime);
                                col.UnboundType = UnboundColumnType.DateTime;
                                col.DisplayFormat.FormatType = FormatType.DateTime;
                            }
                        }
                        break;
                    default:
                        obj.FieldType = typeof (string);
                        col.UnboundType = UnboundColumnType.String;
                        break;
                }
            }

            aggrCache.AddColumn(col.FieldName);
        }

        #endregion
    }

}