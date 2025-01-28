using System;

namespace eidss.model.AVR.ServiceData
{
    [Serializable]
    public class QueryTableHeaderDTO
    {
        public QueryTableHeaderDTO()
        {
            BinaryHeader = new QueryTablePacketDTO();
        }

        public QueryTableHeaderDTO(QueryTablePacketDTO binaryHeader, long queryCacheId, int packetCount, long? userId = null)
        {
            BinaryHeader = binaryHeader;
            PacketCount = packetCount;
            QueryCacheId = queryCacheId;
            UserId = userId;
        }

        public QueryTablePacketDTO BinaryHeader { get; set; }
        public int PacketCount { get; set; }
        public long QueryCacheId { get; set; }
        public long? UserId { get; set; }

    }
}