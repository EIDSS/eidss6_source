using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using bv.common.Core;
using bv.model.BLToolkit;
using BLToolkit.Data;
using eidss.model.AVR.DataBase;
using eidss.model.AVR.ServiceData;
using eidss.model.AVR.SourceData;
using eidss.model.Helpers;
using eidss.model.Resources;
using eidss.model.Trace;
using eidss.model.WindowsService.Serialization;

namespace EIDSS.AVR.Service.WcfFacade
{
    public static class AvrDbHelper
    {
        public class LayoutDTO
        {
            public LayoutDTO(long layoutId)
            {
                LayoutId = layoutId;
            }

            public long LayoutId { get; set; }
            public long QueryId { get; set; }
            public string DefaultLayoutName { get; set; }
            public bool UseArchivedData { get; set; }
        }

        public const string TraceTitle = "AVR DB";
        private static readonly TraceHelper m_Trace = new TraceHelper(TraceHelper.AVRCategory);

        private const int DaysInAWeek = 7;
        private static readonly object m_DbSyncLock = new object();

        public static long? GetQueryCacheId
            (long queryId, string lang, bool isArchive, int refresheAfterDays = DaysInAWeek, bool allowSelectInvalidated = false, long? userId = null)
        {
            return GetQueryCacheId(new QueryCacheKey(queryId, lang, isArchive), refresheAfterDays, allowSelectInvalidated);
        }

        public static long? GetQueryCacheId
            (QueryCacheKey queryCacheKey, int refresheAfterDays = DaysInAWeek, bool allowSelectInvalidated = false, long? userId = null)
        {
            using (var avrTran = new AvrDbTransaction())
            {
                DbManagerProxy manager = avrTran.Manager;

                var param = new List<IDbDataParameter>();
                param.Add(manager.Parameter("idfQuery", queryCacheKey.QueryId));
                param.Add(manager.Parameter("strLanguage", queryCacheKey.Lang));
                param.Add(manager.Parameter("blnUseArchivedData", queryCacheKey.IsArchive));
                param.Add(manager.Parameter("refreshedCacheOnUserCallAfterDays", refresheAfterDays));
                param.Add(manager.Parameter("allowSelectInvalidated", allowSelectInvalidated));
                if (userId.HasValue)
                    param.Add(manager.Parameter("UserID", userId.Value));
                else
                    param.Add(manager.Parameter("UserID", DBNull.Value));


                DbManager command = manager.SetSpCommand("spAsQueryCacheExist",
                    param.ToArray()
                    );
                object result;
                lock (m_DbSyncLock)
                {
                    result = command.ExecuteScalar();
                    avrTran.CommitTransaction();
                }

                return (result == null || result == DBNull.Value) ? null : (long?) result;
            }
        }

        public static DateTime? GetsQueryCacheUserRequestDate(long queryId, long? userId = null)
        {
            using (var avrTran = new AvrDbTransaction())
            {
                DbManagerProxy manager = avrTran.Manager;

                var param = new List<IDbDataParameter>();
                param.Add(manager.Parameter("idfQuery", queryId));
                if (userId.HasValue)
                    param.Add(manager.Parameter("UserID", userId.Value));
                else
                    param.Add(manager.Parameter("UserID", DBNull.Value));

                DbManager command = manager.SetSpCommand("spAsQueryCacheUserRequestDate", param.ToArray());
                object result;
                lock (m_DbSyncLock)
                {
                    result = command.ExecuteScalar();
                    avrTran.CommitTransaction();
                }
                return (result == null || result == DBNull.Value) ? null : (DateTime?) result;
            }
        }

        public static List<long?> GetQueryCacheValidUsers(long queryId, DateTime? requestedLaterThanDate)
        {
            using (var avrTran = new AvrDbTransaction())
            {
                DbManagerProxy manager = avrTran.Manager;

                var param = new List<IDbDataParameter>();
                param.Add(manager.Parameter("idfQuery", queryId));
                if (requestedLaterThanDate.HasValue)
                    param.Add(manager.Parameter("datRequestedLaterThan", requestedLaterThanDate.Value));
                else
                    param.Add(manager.Parameter("datRequestedLaterThan", DBNull.Value));

                DbManager command = manager.SetSpCommand("spAsQueryCacheGetValidUsers", 
                    param.ToArray());
                List<long?> userList = null;
                lock (m_DbSyncLock)
                {
                    userList = command.ExecuteList<long?>();
                    avrTran.CommitTransaction();
                }
                return (userList == null) ? new List<long?>() { null } : userList;
            }
        }

        public static List<long> GetQueryCacheInvalidUsers(long queryId, DateTime requestedLaterThanDate)
        {
            using (var avrTran = new AvrDbTransaction())
            {
                DbManagerProxy manager = avrTran.Manager;

                DbManager command = manager.SetSpCommand("spAsQueryCacheGetInvalidUsers",
                    manager.Parameter("idfQuery", queryId),
                    manager.Parameter("datRequestedLaterThan", requestedLaterThanDate));

                List<long> userList = null;
                lock (m_DbSyncLock)
                {
                    userList = command.ExecuteList<long>();
                    avrTran.CommitTransaction();
                }
                return (userList == null) ? new List<long>() { } : userList;
            }
        }

        public static QueryTableHeaderDTO GetQueryCacheHeader(long queryCacheId, bool isSchedulerCall, bool isArchive, long? userId = null)
        {
            using (var avrTran = new AvrDbTransaction())
            {
                DbManagerProxy manager = avrTran.Manager;

                var param = new List<IDbDataParameter>();
                param.Add(manager.Parameter("idfQueryCache", queryCacheId));
                param.Add(manager.Parameter("blnSchedulerCall", isSchedulerCall));
                param.Add(manager.Parameter("blnUseArchivedData", isArchive));
                if (userId.HasValue)
                    param.Add(manager.Parameter("UserID", userId.Value));
                else
                    param.Add(manager.Parameter("UserID", DBNull.Value));

                DbManager command = manager.SetSpCommand("spAsQueryCacheGetHeader",
                    param.ToArray());

                QueryTablePacketDTO headerPacket = new QueryTablePacketDTO {IsArchive = isArchive};
                int packetCount = 0;
                lock (m_DbSyncLock)
                {
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            packetCount = (int) reader["intPacketCount"];
                            headerPacket.RowCount = (int) reader["intQueryColumnCount"];
                            var binaryBody = (byte[]) reader["blbQuerySchema"];
                            headerPacket.BinaryBody = new ChunkByteArray(binaryBody);
                        }
                    }

                    avrTran.CommitTransaction();
                }
                return new QueryTableHeaderDTO(headerPacket, queryCacheId, packetCount, userId);
            }
        }

        public static QueryTablePacketDTO GetQueryCachePacket(long queryCasheId, int packetNumber, long? userId = null)
        {
            using (var avrTran = new AvrDbTransaction())
            {
                var param = new List<IDbDataParameter>();
                param.Add(avrTran.Manager.Parameter("idfQueryCache", queryCasheId));
                param.Add(avrTran.Manager.Parameter("intQueryCachePacketNumber", packetNumber));
                if (userId.HasValue)
                    param.Add(avrTran.Manager.Parameter("UserID", userId.Value));
                else
                    param.Add(avrTran.Manager.Parameter("UserID", DBNull.Value));

                DbManager command = avrTran.Manager.SetSpCommand("spAsQueryCacheGetPacket",
                    param.ToArray());

                var result = new QueryTablePacketDTO();
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result.RowCount = (int) reader["intTableRowCount"];
                        result.BinaryBody = new ChunkByteArray((byte[]) reader["blbQueryCachePacket"]);
                        result.IsArchive = (bool) reader["blnArchivedData"];
                    }
                }
                avrTran.CommitTransaction();
                return result;
            }
        }

        public static long SaveQueryCache(QueryTableModel zippedTable)
        {
            using (var avrTran = new AvrDbTransaction())
            {
                long id = SaveQueryCacheWithoutTransaction(zippedTable);
                avrTran.CommitTransaction();
                return id;
            }
        }

        public static long SaveQueryCacheWithoutTransaction(QueryTableModel zippedTable)
        {
            lock (m_DbSyncLock)
            {
                using (DbManagerProxy manager = DbManagerFactory.Factory[DatabaseType.Avr].Create())
                {
                    var headerParam = new List<IDbDataParameter>();
                    headerParam.Add(manager.Parameter("idfQuery", zippedTable.QueryId));
                    headerParam.Add(manager.Parameter("strLanguage", zippedTable.Language));
                    headerParam.Add(manager.Parameter("intQueryColumnCount", zippedTable.Header.RowCount));
                    headerParam.Add(manager.Parameter("blbQuerySchema", zippedTable.Header.BinaryBody.ToArray()));
                    headerParam.Add(manager.Parameter("blnUseArchivedData", zippedTable.UseArchivedData));
                    if (zippedTable.UserId.HasValue)
                        headerParam.Add(manager.Parameter("UserID", zippedTable.UserId.Value));
                    else
                        headerParam.Add(manager.Parameter("UserID", DBNull.Value));

                    DbManager headerCommand = manager.SetSpCommand("spAsQueryCachePostHeader",
                        headerParam.ToArray()
                        );

                    var queryCasheId = (long) headerCommand.ExecuteScalar();
                    for (int i = 0; i < zippedTable.BodyPackets.Count; i++)
                    {
                        var param = new List<IDbDataParameter>();
                        param.Add(manager.Parameter("idfQueryCache", queryCasheId));
                        param.Add(manager.Parameter("intQueryCachePacketNumber", i));
                        param.Add(manager.Parameter("intPacketRowCount", zippedTable.BodyPackets[i].RowCount));
                        param.Add(manager.Parameter("blbQueryCachePacket", zippedTable.BodyPackets[i].BinaryBody.ToArray()));
                        param.Add(manager.Parameter("blnArchivedData", zippedTable.BodyPackets[i].IsArchive));
                        if (zippedTable.UserId.HasValue)
                            param.Add(manager.Parameter("UserID", zippedTable.UserId.Value));
                        else
                            param.Add(manager.Parameter("UserID", DBNull.Value));

                        DbManager command = manager.SetSpCommand("spAsQueryCachePostPacket",
                            param.ToArray()
                            );

                        command.ExecuteNonQuery();
                    }

                    return queryCasheId;
                }
            }
        }

        public static void InvalidateQueryCache(long queryId, string lang = null, long? userId = null)
        {
            using (var avrTran = new AvrDbTransaction())
            {
                DbManagerProxy manager = avrTran.Manager;

                var param = new List<IDbDataParameter>();
                param.Add(manager.Parameter("idfQuery", queryId));
                if (!string.IsNullOrEmpty(lang))
                    param.Add(manager.Parameter("strLanguage", lang));
                else
                    param.Add(manager.Parameter("strLanguage", DBNull.Value));
                if (userId.HasValue)
                    param.Add(manager.Parameter("UserID", userId.Value));
                else
                    param.Add(manager.Parameter("UserID", DBNull.Value));


                DbManager command = manager.SetSpCommand("spAsQueryCacheInvalidate",
                        param.ToArray());

                lock (m_DbSyncLock)
                {
                    command.ExecuteNonQuery();
                    avrTran.CommitTransaction();
                }
            }
        }

        public static int DeleteQueryCache(long queryId, string lang, bool leaveLastRecord, long? userId = null)
        {
            using (var avrTran = new AvrDbTransaction())
            {
                DbManagerProxy manager = avrTran.Manager;

                var param = new List<IDbDataParameter>();
                param.Add(manager.Parameter("idfQuery", queryId));
                param.Add(manager.Parameter("strLanguage", lang));
                param.Add(manager.Parameter("blnLeaveLastRecord", leaveLastRecord));
                if (userId.HasValue)
                    param.Add(manager.Parameter("UserID", userId.Value));
                else
                    param.Add(manager.Parameter("UserID", DBNull.Value));
                
                DbManager command = manager.SetSpCommand("spAsQueryCacheDelete",
                    param.ToArray()
                    );

                lock (m_DbSyncLock)
                {
                    object result = command.ExecuteScalar();
                    int numberDeleted = 0;
                    if (result is int)
                    {
                        numberDeleted = (int) result;
                    }
                    avrTran.CommitTransaction();
                    return numberDeleted;
                }
            }
        }

        public static DateTime GetQueryRefreshDateTime(long queryId, string lang, long? userId = null)
        {
            DateTime date = DateTime.Now;
            using (var avrTran = new AvrDbTransaction())
            {
                DbManagerProxy manager = avrTran.Manager;

                var param = new List<IDbDataParameter>();
                param.Add(manager.Parameter("idfQuery", queryId));
                param.Add(manager.Parameter("strLanguage", lang));
                if (userId.HasValue)
                    param.Add(manager.Parameter("UserID", userId.Value));
                else
                    param.Add(manager.Parameter("UserID", DBNull.Value));

                DbManager command = manager.SetSpCommand("spAsQueryCacheGetRefreshDateTime",
                    param.ToArray());
                lock (m_DbSyncLock)
                {
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            date = (DateTime) reader["datQueryRefresh"];
                        }
                    }
                    avrTran.CommitTransaction();
                }
            }
            return date;
        }

        public static long SaveViewCache(long queryCacheId, long layoutId, ViewDTO zippedTable, long? userId = null)
        {
            using (var avrTran = new AvrDbTransaction())
            {
                long id = SaveViewCacheWithoutTransaction(queryCacheId, layoutId, zippedTable, userId);
                avrTran.CommitTransaction();
                return id;
            }
        }

        public static long SaveViewCacheWithoutTransaction(long queryCacheId, long layoutId, ViewDTO zippedTable, long? userId = null)
        {
            lock (m_DbSyncLock)
            {
                using (DbManagerProxy manager = DbManagerFactory.Factory[DatabaseType.Avr].Create())
                {
                    var headerParam = new List<IDbDataParameter>();
                    headerParam.Add(manager.Parameter("idfQueryCache", queryCacheId));
                    headerParam.Add(manager.Parameter("idfLayout", layoutId));
                    headerParam.Add(manager.Parameter("blbViewSchema", zippedTable.Header.BinaryBody.ToArray()));
                    headerParam.Add(manager.Parameter("blbViewHeader", zippedTable.BinaryViewHeader));
                    headerParam.Add(manager.Parameter("intViewColumnCount", zippedTable.Header.RowCount));
                    if (userId.HasValue)
                        headerParam.Add(manager.Parameter("UserID", userId.Value));
                    else
                        headerParam.Add(manager.Parameter("UserID", DBNull.Value));

                    DbManager headerCommand = manager.SetSpCommand("spAsViewCachePostHeader",
                        headerParam.ToArray()
                        );

                    var viewCasheId = (long) headerCommand.ExecuteScalar();
                    for (int i = 0; i < zippedTable.BodyPackets.Count; i++)
                    {
                        var param = new List<IDbDataParameter>();
                        param.Add(manager.Parameter("idfViewCache", viewCasheId));
                        param.Add(manager.Parameter("intViewCachePacketNumber", i));
                        param.Add(manager.Parameter("intPacketRowCount", zippedTable.BodyPackets[i].RowCount));
                        param.Add(manager.Parameter("blbViewCachePacket", zippedTable.BodyPackets[i].BinaryBody.ToArray()));
                        if (userId.HasValue)
                            param.Add(manager.Parameter("UserID", userId.Value));
                        else
                            param.Add(manager.Parameter("UserID", DBNull.Value));

                        DbManager command = manager.SetSpCommand("spAsViewCachePostPacket",
                            param.ToArray()
                            );

                        command.ExecuteNonQuery();
                    }

                    return viewCasheId;
                }
            }
        }

        public static void InvalidateViewCache(long layoutId)
        {
            using (var avrTran = new AvrDbTransaction())
            {
                DbManagerProxy manager = avrTran.Manager;

                DbManager command = manager.SetSpCommand("spAsViewCacheInvalidate",
                    manager.Parameter("idfLayout", layoutId));

                lock (m_DbSyncLock)
                {
                    command.ExecuteNonQuery();
                    avrTran.CommitTransaction();
                }
            }
        }

        public static long? GetViewCacheId
            (long queryCacheId, long layoutId, int refresheAfterDays = DaysInAWeek, bool allowSelectInvalidated = false, long? userId = null)
        {
            using (var avrTran = new AvrDbTransaction())
            {
                DbManagerProxy manager = avrTran.Manager;

                var param = new List<IDbDataParameter>();
                param.Add(manager.Parameter("idfQueryCache", queryCacheId));
                param.Add(manager.Parameter("idfLayout", layoutId));
                param.Add(manager.Parameter("refreshedCacheOnUserCallAfterDays", refresheAfterDays));
                param.Add(manager.Parameter("allowSelectInvalidated", allowSelectInvalidated));
                if (userId.HasValue)
                    param.Add(manager.Parameter("UserID", userId.Value));
                else
                    param.Add(manager.Parameter("UserID", DBNull.Value));

                DbManager command = manager.SetSpCommand("spAsViewCacheExist",
                    param.ToArray()
                    );
                object result;
                lock (m_DbSyncLock)
                {
                    result = command.ExecuteScalar();
                    avrTran.CommitTransaction();
                }

                return (result == null || result == DBNull.Value) ? null : (long?) result;
            }
        }

        public static ViewDTO GetViewCache(long viewCacheId, bool isSchedulerCall, long? userId = null)
        {
            ViewDTO view;
            using (var avrTran = new AvrDbTransaction())
            {
                DbManagerProxy manager = avrTran.Manager;

                var headerParam = new List<IDbDataParameter>();
                headerParam.Add(manager.Parameter("idfViewCache", viewCacheId));
                headerParam.Add(manager.Parameter("blnSchedulerCall", isSchedulerCall));
                 if (userId.HasValue)
                    headerParam.Add(manager.Parameter("UserID", userId.Value));
                else
                    headerParam.Add(manager.Parameter("UserID", DBNull.Value));

                DbManager headerCmd = manager.SetSpCommand("spAsViewCacheGetHeader",
                    headerParam.ToArray());

                lock (m_DbSyncLock)
                {
                    int packetCount;
                    using (IDataReader reader = headerCmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return null;
                        }

                        BaseTableDTO viewTableDTO = new BaseTableDTO
                        {
                            Header =
                            {
                                RowCount = (int) reader["intViewColumnCount"],
                                BinaryBody = new ChunkByteArray((byte[]) reader["blbViewSchema"])
                            }
                        };

                        var binaryHeader = (byte[]) reader["blbViewHeader"];
                        view = new ViewDTO(viewTableDTO, binaryHeader);

                        packetCount = (int) reader["intPacketCount"];
                    }
                    for (int packetNumber = 0; packetNumber < packetCount; packetNumber++)
                    {

                        var packetParam = new List<IDbDataParameter>();
                        packetParam.Add(manager.Parameter("idfViewCache", viewCacheId));
                        packetParam.Add(manager.Parameter("intViewCachePacketNumber", packetNumber));
                        if (userId.HasValue)
                            packetParam.Add(manager.Parameter("UserID", userId.Value));
                        else
                            packetParam.Add(manager.Parameter("UserID", DBNull.Value));

                        DbManager packetCmd = manager.SetSpCommand("spAsViewCacheGetPacket",
                            packetParam.ToArray());

                        var packetDTO = new BaseTablePacketDTO();
                        using (IDataReader packetReader = packetCmd.ExecuteReader())
                        {
                            if (!packetReader.Read())
                            {
                                return null;
                            }

                            packetDTO.RowCount = (int) packetReader["intTableRowCount"];
                            packetDTO.BinaryBody = new ChunkByteArray((byte[]) packetReader["blbViewCachePacket"]);
                        }
                        view.BodyPackets.Add(packetDTO);
                    }

                    avrTran.CommitTransaction();
                }
            }
            return view;
        }

        public static QueryTableModel GetQueryResult(long queryId, string lang, bool isArchive, long? userId = null)
        {
            string queryString;
            QueryTableModel zippedTable;

            var watch = new Stopwatch();
            watch.Start();
            m_Trace.Trace(TraceTitle, "Executing actual query '{0}' for lang '{1}' and user {2}", queryId, lang, 
                userId.HasValue ? string.Format("'{0}'", userId.Value.ToString()) : "null");
            using (DbManagerProxy manager = DbManagerFactory.Factory.Create())
            {
                queryString = QueryHelper.GetQueryText(manager, queryId, false);

                QueryTableModel serializedTable = QueryHelper.GetInnerQueryResult(manager, queryString, lang, userId,
                    c => BinarySerializer.SerializeFromCommand(c, queryId, lang, false, 0, userId));
                zippedTable = BinaryCompressor.Zip(serializedTable);
            }

            if (isArchive)
            {
                m_Trace.Trace(TraceTitle, "Executing archive query '{0}' for lang '{1}' and user {2}", queryId, lang, 
                    userId.HasValue ? string.Format("'{0}'", userId.Value.ToString()) : "null");
                using (DbManagerProxy archiveManager = DbManagerFactory.Factory[DatabaseType.Archive].Create())
                {
                    using (DbManagerProxy manager = DbManagerFactory.Factory.Create())
                    {
                        QueryHelper.DropAndCreateArchiveQuery(manager, archiveManager, queryId);
                    }
                    QueryTableModel serializedArchiveTable = QueryHelper.GetInnerQueryResult(archiveManager, queryString, lang, userId,
                        c => BinarySerializer.SerializeFromCommand(c, queryId, lang, true, 0, userId));
                    QueryTableModel zippedArchiveTable = BinaryCompressor.Zip(serializedArchiveTable);

                    zippedTable.UseArchivedData = true;
                    foreach (QueryTablePacketDTO packet in zippedArchiveTable.BodyPackets)
                    {
                        packet.IsArchive = true;
                        zippedTable.BodyPackets.Add(packet);
                    }
                }
            }
            m_Trace.Trace(TraceTitle, "Executing query '{0}' for lang '{1}' and user {2} finished, duration={3},", 
                queryId, 
                lang, 
                userId.HasValue ? string.Format("'{0}'", userId.Value.ToString()) : "null", 
                watch.Elapsed);
            return zippedTable;
        }

        public static DatabaseNames GetDatabaseNames()
        {
            string eidssActualDbName;
            string eidssArchiveDbName = null;
            string avrDbName;
            try
            {
                using (DbManagerProxy manager = DbManagerFactory.Factory.Create())
                {
                    eidssActualDbName = GetDatabaseName(manager);
                }
            }
            catch (Exception ex)
            {
                string message = EidssMessages.Get("msgAvrServiceActualDbError", "AVR Service could not connect to Actual EIDSS Database");
                throw new AvrDataException(message, ex);
            }
            try
            {
                if (ArchiveSqlHelper.IsCredentialsCorrect())
                {
                    using (DbManagerProxy archiveManager = DbManagerFactory.Factory[DatabaseType.Archive].Create())
                    {
                        eidssArchiveDbName = GetDatabaseName(archiveManager);
                    }
                }
            }
            catch (Exception ex)
            {
                string message = EidssMessages.Get("msgAvrServiceArchiveDbError", "AVR Service could not connect to Archive EIDSS Database");
                throw new AvrDataException(message, ex);
            }

            try
            {
                using (DbManagerProxy avrManager = DbManagerFactory.Factory[DatabaseType.Avr].Create())
                {
                    avrDbName = GetDatabaseName(avrManager);
                }
            }
            catch (Exception ex)
            {
                string message = EidssMessages.Get("msgAvrServiceDbError", "AVR Service could not connect to AVR Service Database");
                throw new AvrDataException(message, ex);
            }

            return new DatabaseNames(eidssActualDbName, eidssArchiveDbName, avrDbName);
        }

        private static string GetDatabaseName(DbManagerProxy manager)
        {
            return manager != null && manager.Connection != null
                ? manager.Connection.Database
                : string.Empty;
        }

        public static List<long> GetQueryIdList()
        {
            List<long> result = GetValueList<long>("spAsQuerySelectLookup", "idflQuery");
            return result;
        }

        public static List<long> GetLayoutIdList()
        {
            List<long> result = GetValueList<long>("spAsLayoutSelectLookup", "idflLayout");
            return result;
        }

        private static List<T> GetValueList<T>(string spLookupName, string idColumnName)
        {
            using (DbManagerProxy manager = DbManagerFactory.Factory.Create())
            {
                DbManager command = manager.SetSpCommand(spLookupName, manager.Parameter("LangID", Localizer.lngEn));
                using (IDataReader reader = command.ExecuteReader())
                {
                    var result = new List<T>();
                    while (reader.Read())
                    {
                        var item = (T) reader[idColumnName];
                        result.Add(item);
                    }
                    return result;
                }
            }
        }

        public static string GetLayoutNameForLog(long id)
        {
            return GetValue("spAsLayoutSelectLookup", "LayoutID", id, "strDefaultLayoutName");
        }

        public static string GetQueryNameForLog(long id)
        {
            return GetValue("spAsQuerySelectLookup", "QueryID", id, "DefQueryName");
        }

        private static string GetValue(string spLookupName, string idName, long idValue, string valueColumnName)
        {
            try
            {
                using (DbManagerProxy manager = DbManagerFactory.Factory.Create())
                {
                    DbManager command = manager.SetSpCommand(spLookupName,
                        manager.Parameter("LangID", Localizer.lngEn),
                        manager.Parameter(idName, idValue));
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        return reader.Read()
                            ? Utils.Str(reader[valueColumnName])
                            : string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public static LayoutDTO GetLayoutDTO(long layoutId)
        {
            LayoutDTO dto = new LayoutDTO(layoutId);
            try
            {
                using (DbManagerProxy manager = DbManagerFactory.Factory.Create())
                {
                    DbManager command = manager.SetSpCommand("spAsLayoutSelectLookup",
                        manager.Parameter("LangID", Localizer.lngEn),
                        manager.Parameter("LayoutID", layoutId));
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            dto.DefaultLayoutName = reader["strDefaultLayoutName"].ToString();
                            dto.QueryId = (long) reader["idflQuery"];
                            dto.UseArchivedData = (bool) reader["blnUseArchivedData"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dto.DefaultLayoutName = ex.ToString();
            }
            return dto;
        }
    }
}