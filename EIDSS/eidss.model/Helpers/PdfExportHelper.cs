using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Text.RegularExpressions;

using eidss.model.Avr.View;

namespace eidss.model.Helpers
{
    public sealed class PdfExportHelper
    {
        public sealed class CaptionString
        {
            public string Caption;

            public bool IsRowArea;

            public string SurrogateId;

            private CaptionString() { }

            public CaptionString(string caption, bool isRowArea, string surrogateId)
            {
                Caption = caption;
                IsRowArea = isRowArea;
                SurrogateId = surrogateId;
            }

            public static implicit operator string(CaptionString c)
            {
                return c.Caption;
            }
        }

        public sealed class AllCaptionsRetValue
        {
            public List<CaptionString> Captions;

            public int RecursionDepth;

            public AllCaptionsRetValue()
            {
                Captions = new List<CaptionString>();
                RecursionDepth = -1;
            }

            public AllCaptionsRetValue(CaptionString[] captions, int recDepth)
            {
                Captions = new List<CaptionString>(captions);
                RecursionDepth = recDepth;
            }
        }

        // Default A4 letter width. Is used if no other value supplied.
        private const float DocumentWidthinMillimetersDefault = 290;

        private const string SpaceString = " ";
        private const float ColumnIndentinMillimeters = 7;

        // Vurtually A4 letter width.
        private readonly float DocumentWidthinMillimeters;

        private readonly Font _font;
        private readonly CaptionString[] _captions;

        // Number of header lines.
        private int _bandLinesAmount = 0;

        // Header hight. Is used for programmatically setting the hight of the header.
        public int HeaderHieghtInPixels { get; set; }

        public bool ShouldCaptionBeWordWrapped { get; set; }

        public PdfExportHelper(BaseBand band, Font captionFont, float documentWidthinMillimeters = DocumentWidthinMillimetersDefault)
            : this(SelectAllCaptionsRecursively(band, 1, 1), captionFont, documentWidthinMillimeters)
        {
        }

        public PdfExportHelper(AllCaptionsRetValue allCaptions, Font captionFont, float documentWidthinMillimeters = DocumentWidthinMillimetersDefault)
        {
            _captions = allCaptions.Captions.ToArray();
            _bandLinesAmount = allCaptions.RecursionDepth;
            _font = captionFont;
            DocumentWidthinMillimeters = documentWidthinMillimeters;
            ShouldCaptionBeWordWrapped = false;

            using (Bitmap bmpTemp = new Bitmap(1, 1))
            {
                using (Graphics g = Graphics.FromImage(bmpTemp))
                {
                    g.PageUnit = GraphicsUnit.Millimeter;
                    float totalWidth = 0.0f;
                    for (int i = 0; i < _captions.Length; ++i)
                    {
                        totalWidth += g.MeasureString(_captions[i], captionFont).Width;
                        totalWidth += ColumnIndentinMillimeters;

                        if (totalWidth > DocumentWidthinMillimeters)
                        {
                            /* There is not enough space for all column captions.
                             * Let's check if word wrapping can help. */
                            ShouldCaptionBeWordWrapped = CanWordWrappingHelp(
                                _captions, captionFont, g, bmpTemp);

                            break;
                        }
                    }

                    HeaderHieghtInPixels = (int)CalculateHeaderHeight(_captions, g);
                }
            }
        }

        public string ProcessString(string text)
        {
            text = PreProcessString(text);

            if (ShouldCaptionBeWordWrapped)
            {
                return WrapString(text);
            }
            else
            {
                return UnWrapString(text);
            }
        }

        public void WordWrapVerticalColumnHeaders(DataTable dt)
        {
            List<CaptionString> captionsWrapper = new List<CaptionString>(_captions);
            string[] indexes = (from c in captionsWrapper
                                   where c.IsRowArea
                                   select c.SurrogateId).ToArray();

            for (int j = 0; j < indexes.Length; ++j)
            {
                string currentKye = indexes[j];
                int searchingColumnIndex = -1;
                for (int k = 0; k < dt.Columns.Count; ++k)
                {
                    if (dt.Columns[k].ColumnName == currentKye)
                    {
                        searchingColumnIndex = k;
                        break;
                    }
                }

                if (searchingColumnIndex == -1)
                {
                    continue;
                }

                for (int i = 0; i < dt.Rows.Count; ++i)
                {
                    string currentString = dt.Rows[i].ItemArray[searchingColumnIndex] as string;

                    if (String.IsNullOrEmpty(currentString))
                    {
                        continue;
                    }

                    string processedString = ProcessString(currentString);
                    dt.Rows[i].SetField<string>(searchingColumnIndex, processedString);
                }
            }
        }

        private static string PreProcessString(string text)
        {
            return Regex.Replace(text.Trim(),
                "[ ]{2,}", " ", RegexOptions.CultureInvariant);
        }

        private static string WrapString(string text)
        {
            return text.Replace(SpaceString, Environment.NewLine);
        }

        private static string UnWrapString(string text)
        {
            return text.Replace(Environment.NewLine, SpaceString);
        }

        private bool CanWordWrappingHelp(CaptionString[] captions, Font captionFont, Graphics g, Bitmap bmp)
        {
            float totalWidth = 0.0f;
            for (int i = 0; i < captions.Length; ++i)
            {
                string textWrapped = WrapString(captions[i]);
                totalWidth += g.MeasureString(textWrapped, captionFont).Width;
                totalWidth += ColumnIndentinMillimeters;

                if (totalWidth > DocumentWidthinMillimeters)
                {
                    return false;
                }
            }

            return true;
        }

        private static AllCaptionsRetValue SelectAllCaptionsRecursively(BaseBand obj, int currentDepth, int maxDepth)
        {
            if (maxDepth < currentDepth)
            {
                maxDepth = currentDepth;
            }

            AllCaptionsRetValue retValue = new AllCaptionsRetValue();
            retValue.RecursionDepth = maxDepth;

            for (int i = 0; i < obj.Columns.Count; ++i)
            {
                if (!obj.Columns[i].IsToDelete)
                {
                    retValue.Captions.Add(new CaptionString(
                        PreProcessString(obj.Columns[i].DisplayText),
                        obj.Columns[i].IsRowArea, obj.Columns[i].UniquePath));
                }
            }

            for (int i = 0; i < obj.Bands.Count; ++i)
            {
                if (!obj.Bands[i].IsToDelete)
                {
                    AllCaptionsRetValue captions = SelectAllCaptionsRecursively(obj.Bands[i], currentDepth + 1, maxDepth);

                    if (captions.Captions.Count > 0)
                    {
                        retValue.Captions.AddRange(captions.Captions);
                    }

                    if (retValue.RecursionDepth < captions.RecursionDepth)
                    {
                        retValue.RecursionDepth = captions.RecursionDepth;
                    }
                }
            }

            return retValue;
        }

        private int CalculateHeaderHeight(CaptionString[] lines, Graphics g)
        {
            if (!ShouldCaptionBeWordWrapped)
            {
                return -1;
            }

            int maxAmount = 0;
            for (int i = 0; i < lines.Length; ++i)
            {
                string line = lines[i];

                int amount = 0;
                for (int j = 0; j < line.Length; ++j)
                {
                    if (line[j] == ' ' || line[j] == '\n')
                    {
                        ++amount;
                    }
                }

                if (maxAmount < amount)
                {
                    maxAmount = amount;
                }
            }

            StringBuilder mockText = new StringBuilder();
            for (int i = 0; i < maxAmount + _bandLinesAmount; ++i)
            {
                mockText.Append(String.Format("A{0}", Environment.NewLine));
            }

            GraphicsUnit originalUnit = g.PageUnit;
            g.PageUnit = GraphicsUnit.Pixel;
            int headerHeight = (int)g.MeasureString(mockText.ToString(), _font).Height;
            g.PageUnit = originalUnit;

            return headerHeight + _bandLinesAmount * 7;
        }
    }
}
