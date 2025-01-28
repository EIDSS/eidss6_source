using System;
using System.Collections.Generic;
using bv.common.Core;
using eidss.model.AVR.DataBase;

namespace eidss.model.AVR.ServiceData
{
    [Serializable]
    public class ChartTableDTO : BaseTableDTO
    {
        public ChartTableDTO()
        {
            ChartSettings = new byte[0];
        }

        public ChartTableDTO
            (long viewId, string lang, BaseTableDTO baseTable, byte[] chartSettings, DBChartType? chartType, Dictionary<string, object> textPatterns, int width, int height, long? userId = null)
        {
            Utils.CheckNotNull(baseTable, "baseTable");

            ViewId = viewId;
            Lang = lang;
            ChartSettings = chartSettings;
            ChartType = chartType;
            TextPatterns = textPatterns;
            Width = width;
            Height = height;
            UserId = userId;

            TableName = baseTable.TableName;
            Header = baseTable.Header;
            BodyPackets = baseTable.BodyPackets;
        }

        public long ViewId { get; set; }

        public string Lang { get; set; }

        public byte[] ChartSettings { get; set; }

        public DBChartType? ChartType { get; set; }

        public Dictionary<string, object> TextPatterns { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public long? UserId { get; set; }

        public override string ToString()
        {
            return string.Format("ViewId:'{0}', Lang:'{1}', UserId: {6}, Data: {2}, Chart type:{3}, Image Size:('{4}','{5}')",
                ViewId, Lang, base.ToString(), ChartType.HasValue ? ChartType.Value.ToString() : "Default", Width, Height,
                UserId.HasValue ? string.Format("'{0}'", UserId.Value.ToString()) : "null");
        }
    }
}