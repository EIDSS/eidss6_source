using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using bv.common.Core;
using eidss.model.Avr.Commands.Export;
using eidss.model.AVR.SourceData;
using eidss.model.Core.CultureInfo;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace eidss.model.Avr.Export
{
    public class NpoiExcelWrapper : IDisposable
    {
        private enum CellType
        {
            Header,
            Normal
        }

        private const int MaximumNumberOfRowsPerSheet = 65500;
        private const int MaximumNumberOfRowsForOpenXml = 30000;
        private const int MaximumSheetNameLength = 25;
        private readonly int m_MaxRowsCount;
        private readonly string m_ShortDatePattern;
        private readonly CultureInfo m_CurrentCulture;
        private readonly Dictionary<CellType, ICellStyle> m_CellTypes = new Dictionary<CellType, ICellStyle>();
        public IWorkbook Workbook { get; private set; }

        private void ResetWorkbook(ExportType type)
        {
            m_CellTypes.Clear();
            m_CellStyleCache.Clear();
            switch (type)
            {
                case ExportType.Xls:
                    Workbook = new HSSFWorkbook();
                    break;
                case ExportType.Xlsx:
                    Workbook = new XSSFWorkbook();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type", string.Format("Unsupported Export type {0}", type));
            }
        }

        public NpoiExcelWrapper(ExportType type)
        {
            switch (type)
            {
                case ExportType.Xls:
                    m_MaxRowsCount = MaximumNumberOfRowsPerSheet;
                    break;
                case ExportType.Xlsx:
                    m_MaxRowsCount = MaximumNumberOfRowsForOpenXml;
                    break;
            }

            ResetWorkbook(type);
            m_ShortDatePattern = Thread.CurrentThread.CurrentCulture
                .DateTimeFormat.ShortDatePattern;
            m_CurrentCulture = Thread.CurrentThread.CurrentCulture;
        }

        public void Dispose()
        {
            m_CellStyleCache.Clear();
            m_CellTypes.Clear();
            Thread.CurrentThread.CurrentCulture = m_CurrentCulture;
        }

        public static void QueryLineListToExcel(string fileName, AvrDataTable data, ExportType type)
        {
            using (var npoiExcelWrapper = new NpoiExcelWrapper(type))
            {
                npoiExcelWrapper.Export(fileName, data);
            }
        }

        public static List<byte[]> QueryLineListToExcel(AvrDataTable data, ExportType type)
        {
            using (var npoiExcelWrapper = new NpoiExcelWrapper(type))
            {
                return npoiExcelWrapper.Export(data);
            }
        }

        public void Export(string fileName, AvrDataTable data)
        {
            var exportDictionary = ExportToByteArrays(fileName, data);
            foreach (var export in exportDictionary)
            {
                using (var stream = new FileStream(export.Key, FileMode.Create))
                {
                    var xlsBytes = export.Value;
                    stream.Write(xlsBytes, 0, xlsBytes.Length);
                    stream.Close();
                }
            }
        }

        public List<byte[]> Export(AvrDataTable data)
        {
            var exportDictionary = ExportToByteArrays(string.Empty, data);
            return exportDictionary.Values.ToList();
        }

        private Dictionary<string, byte[]> ExportToByteArrays(string fileName, AvrDataTable data)
        {
            using (new CultureInfoTransaction(new CultureInfo("en-US")))
            {
                var sheetName = GetSheetName(data);
                var sheet = CreateNewSheet(sheetName, 0, data);
                var currentNpoiRowIndex = 1;
                var sheetIndex = 0;
                var fileNameSuffix = "";
                var result = new Dictionary<string, byte[]>();
                for (var rowIndex = 0; rowIndex < data.Rows.Count; rowIndex++)
                {
                    if (currentNpoiRowIndex >= m_MaxRowsCount)
                    {
                        currentNpoiRowIndex = 1;
                        sheetIndex++;
                        if (Workbook is HSSFWorkbook)
                        {
                            sheet = CreateNewSheet(sheetName, sheetIndex, data);
                        }
                        else
                        {
                            fileNameSuffix = sheetIndex.ToString(CultureInfo.InvariantCulture);
                            result.Add(AppendFileNameWithSuffix(fileName, fileNameSuffix), SaveWorkbook(Workbook));
                            ResetWorkbook(ExportType.Xlsx);
                            sheet = CreateNewSheet(sheetName, 0, data);
                        }
                    }

                    var excelRow = sheet.CreateRow(currentNpoiRowIndex++);

                    var colIndex = 0;
                    foreach (var column in data.Columns)
                    {
                        var cell = excelRow.CreateCell(colIndex);
                        colIndex++;
                        cell.CellStyle = GetCellStyle(CellType.Normal);
                        var row = data.Rows[rowIndex];
                        if (row[column.Ordinal] == DBNull.Value)
                        {
                            SetEmptyCellFormat(cell, column.DataType);
                            continue;
                        }

                        SetCellValue(cell, row[column.Ordinal]);
                    }
                }

                if (fileNameSuffix != "")
                {
                    fileNameSuffix = (++sheetIndex).ToString(CultureInfo.InvariantCulture);
                }

                result.Add(AppendFileNameWithSuffix(fileName, fileNameSuffix), SaveWorkbook(Workbook));

                return result;
            }
        }

        private static byte[] SaveWorkbook(IWorkbook workbook)
        {
            Utils.CheckNotNull(workbook, "workbook");

            using (var stream = new MemoryStream())
            {
                workbook.Write(stream);
                stream.Flush();
                if (stream.CanSeek)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                }

                var array = stream.ToArray();
                return array;
            }
        }

        internal static string AppendFileNameWithSuffix(string baseName, string suffix)
        {
            if (suffix != "")
            {
                suffix = "_" + suffix;
            }

            if (string.IsNullOrEmpty(baseName))
            {
                return suffix;
            }

            return Path.GetDirectoryName(baseName) + "\\" + Path.GetFileNameWithoutExtension(baseName) + suffix +
                   Path.GetExtension(baseName);
        }

        private static string GetSheetName(AvrDataTable data)
        {
            if (string.IsNullOrEmpty(data.TableName))
            {
                data.TableName = "Sheet";
            }

            return EscapeSheetName(data.TableName);
        }

        private ISheet CreateNewSheet(string sheetName, int sheetIndex, AvrDataTable data)
        {
            if (sheetIndex > 0)
            {
                sheetName = string.Format("{0} {1}", sheetName, sheetIndex);
            }

            var sheet = Workbook.CreateSheet(sheetName);
            // Create the header row
            var excelRow = sheet.CreateRow(0);

            var colIndex = 0;
            foreach (var col in data.Columns)
            {
                var cell = excelRow.CreateCell(colIndex);
                var caption = col.Caption ?? col.ColumnName;
                cell.SetCellValue(caption);
                cell.CellStyle = GetCellStyle(CellType.Header);
                if (IsDate(col.DataType) || IsNumeric(col.DataType))
                {
                    cell.CellStyle.Alignment = HorizontalAlignment.Right;
                }
                else
                {
                    cell.CellStyle.Alignment = HorizontalAlignment.Left;
                }

                sheet.AutoSizeColumn(colIndex);
                colIndex++;
            }
            //

            return sheet;
        }

        private void SetCellValue(ICell cell, object value)
        {
            if (value == null)
            {
                return;
            }

            if (IsDate(value.GetType()))
            {
                SetCellDateFormat(cell);
                cell.SetCellValue((DateTime) value);
            }
            else if (IsNumeric(value.GetType()))
            {
                cell.SetCellValue(
                    (double) Convert.ChangeType(value, typeof(double)));
            }
            else if (IsBool(value.GetType()))
            {
                cell.SetCellValue((bool) value);
            }
            else
            {
                cell.SetCellValue(value.ToString());
            }
        }

        private void SetEmptyCellFormat(ICell cell, Type dataType)
        {
            if (IsDate(dataType))
            {
                SetCellDateFormat(cell);
            }
        }

        private void SetCellDateFormat(ICell cell)
        {
            cell.CellStyle = GetCellStyleForFormat(m_ShortDatePattern);
        }

        private bool IsNumeric(Type dataType)
        {
            return typeof(short) == dataType
                   || typeof(int) == dataType
                   || typeof(long) == dataType
                   || typeof(sbyte) == dataType
                   || typeof(float) == dataType
                   || typeof(double) == dataType
                   || typeof(decimal) == dataType;
        }

        private static bool IsDate(Type dataType)
        {
            return typeof(DateTime) == dataType;
        }

        private static bool IsBool(Type dataType)
        {
            return typeof(bool) == dataType;
        }

        private static string EscapeSheetName(string sheetName)
        {
            var escapedSheetName = sheetName
                .Replace("/", "-")
                .Replace("\\", " ")
                .Replace("?", string.Empty)
                .Replace("*", string.Empty)
                .Replace("[", string.Empty)
                .Replace("]", string.Empty)
                .Replace(":", string.Empty);

            if (escapedSheetName.Length > MaximumSheetNameLength)
            {
                escapedSheetName = escapedSheetName.Substring(0, MaximumSheetNameLength);
            }

            return escapedSheetName;
        }

        private readonly Dictionary<string, ICellStyle> m_CellStyleCache = new Dictionary<string, ICellStyle>();

        private ICellStyle GetCellStyleForFormat(string dataFormat)
        {
            if (!m_CellStyleCache.ContainsKey(dataFormat))
            {
                var style = Workbook.CreateCellStyle();

                // check if this is a built-in format
                var builtinFormatId = HSSFDataFormat.GetBuiltinFormat(dataFormat);

                if (builtinFormatId != -1)
                {
                    style.DataFormat = builtinFormatId;
                }
                else
                {
                    // not a built-in format, so create a new one
                    var newDataFormat = Workbook.CreateDataFormat();
                    style.DataFormat = newDataFormat.GetFormat(dataFormat);
                }

                m_CellStyleCache[dataFormat] = style;
            }

            return m_CellStyleCache[dataFormat];
        }

        private ICellStyle GetCellStyle(CellType cellType)
        {
            if (!m_CellTypes.ContainsKey(cellType))
            {
                var style = Workbook.CreateCellStyle();
                switch (cellType)
                {
                    case CellType.Header:
                        var font = Workbook.CreateFont();
                        font.Boldweight = (short) FontBoldWeight.Bold;
                        style.SetFont(font);
                        break;
                }

                m_CellTypes[cellType] = style;
            }

            return m_CellTypes[cellType];
        }
    }
}