using System.Collections.Generic;

namespace eidss.model.AVR.ServiceData
{
    public class QueryTableModel
    {
        public QueryTableModel(long queryId, string language, long? userId = null)
        {
            QueryId = queryId;
            Language = language;
            UserId = userId;
            Header = new QueryTablePacketDTO();
            BodyPackets = new List<QueryTablePacketDTO>();
        }

        public QueryTablePacketDTO Header { get; set; }
        public IList<QueryTablePacketDTO> BodyPackets { get; set; }
        public long QueryId { get; set; }
        public string Language { get; set; }
        public bool UseArchivedData { get; set; }
        public long? UserId { get; set; }
    }
}