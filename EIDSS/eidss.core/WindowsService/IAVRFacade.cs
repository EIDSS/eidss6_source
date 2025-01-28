using System;
using System.Collections.Generic;
using System.ServiceModel;
using eidss.model.AVR.DataBase;
using eidss.model.AVR.ServiceData;

namespace eidss.model.WindowsService
{
    [ServiceContract]
    public interface IAVRFacade
    {
        /// <summary>
        ///     Perform Export chart
        /// </summary>
        /// <param name="zippedData">Container with chart parameters</param>
        /// <returns>binary representetion of the result image and chart settings</returns>
        [OperationContract]
        ChartExportDTO ExportChartToJpg(ChartTableDTO zippedData);

        /// <summary>
        ///     Load Layout with Data and construct corresponding View
        /// </summary>
        /// <param name="sessionId">ID of current web session</param>
        /// <param name="layoutId">ID of AVR layout</param>
        /// <param name="lang">Language</param>
        /// <param name="userId">ID of current user for user-sensitive mode of AVR Cache (it equals null if this mode is not activated)</param>
        /// <returns>default zipped Avr View structure and data for given layout and language</returns>
        [OperationContract]
        ViewDTO GetCachedView(string sessionId, long layoutId, string lang, long? userId);



        /// <summary>
        ///     Make current Cache of given view invalid for all supported languages and users.
        ///     So next time method <see cref="GetCachedView" />   will recalculate  View Cache.
        /// </summary>
        /// <param name="layoutId">ID of the AVR layout</param>
        [OperationContract]
        void InvalidateViewCache(long layoutId);

        /// <summary>
        ///     If Cache for given query id, language, and user id (if specified) exists  returns Header with zipped table structure which store Cache
        ///     If Cache doesn't exist - execute query on the EIDSS DB and store execution result in new table of AVR DB.
        ///     Then return table structure
        /// </summary>
        /// <param name="queryId">ID of the AVR query</param>
        /// <param name="lang">Language</param>
        /// <param name="isArchive">if true - get combined cache of actual and archive data. if false - only actual data</param>
        /// <param name="userId">ID of current user for user-sensitive mode of AVR Cache (it equals null if this mode is not activated)</param>
        /// <returns>Zipped table structure of the table with query result Cache</returns>
        [OperationContract]
        QueryTableHeaderDTO GetCachedQueryTableHeader(long queryId, string lang, bool isArchive, long? userId);

        /// <summary>
        ///     If Cache for given query id, language, and user id (if specified) exists  returns true
        ///     If Cache doesn't exist - returns false.
        ///     Then return table structure
        /// </summary>
        /// <param name="queryId">ID of the AVR query</param>
        /// <param name="lang">Language</param>
        /// <param name="isArchive">if true - get existance cache of actual and archive data. if false - only existance of actual data</param>
        /// <param name="userId">ID of current user for user-sensitive mode of AVR Cache (it equals null if this mode is not activated)</param>
        /// <returns></returns>
        [OperationContract]
        bool DoesCachedQueryExists(long queryId, string lang, bool isArchive, long? userId);

        /// <summary>
        ///     Make current Cache of given query, language and user (if specified) invalid and recalculate Cache
        ///     Should be called from Scheduler only
        /// </summary>
        /// <param name="queryId">ID of the AVR query</param>
        /// <param name="lang">Language</param>
        /// <param name="isArchive">if true - refresh cache of actual and archive data. if false - only actual data</param>
        /// <param name="userId">ID of current user for user-sensitive mode of AVR Cache (it equals null if this mode is not activated)</param>
        void RefreshCachedQueryTableByScheduler(long queryId, string lang, bool isArchive, long? userId);

        /// <summary>
        ///     If Cache for given query cache id exists - returns Header with zipped table structure which store Cache
        ///     If Cache doesn't exist - call <see cref="GetCachedQueryTableHeader" />
        /// </summary>
        /// <param name="queryCacheId">ID of the AVR query cache record</param>
        /// <param name="queryId">ID of the AVR query</param>
        /// <param name="lang">Language</param>
        /// <param name="isArchive">if true - get combined cache of actual and archive data. if false - only actual data</param>
        /// <param name="userId">ID of current user for user-sensitive mode of AVR Cache (it equals null if this mode is not activated)</param>
        /// <returns>Zipped table structure of the table with query result Cache</returns>
        [OperationContract]
        QueryTableHeaderDTO GetConcreteCachedQueryTableHeader(long queryCacheId, long queryId, string lang, bool isArchive, long? userId);

        /// <summary>
        ///     Returns one of packet of zipped table with query cache
        /// </summary>
        /// <param name="queryCacheId">ID of the AVR query cache record</param>
        /// <param name="packetNumber">Number of the packet of zipped table body</param>
        /// ///
        /// <param name="totalPacketCount">Total packet count of zipped table body</param>
        /// <param name="userId">ID of current user for user-sensitive mode of AVR Cache (it equals null if this mode is not activated)</param>
        /// <returns>Zipped packet of the table with query result Cache</returns>
        [OperationContract]
        QueryTablePacketDTO GetCachedQueryTablePacket(long queryCacheId, int packetNumber, int totalPacketCount, long? userId);

        /// <summary>
        ///     Make current Cache of given query, language, and user (if specified) invalid.
        ///     So next time method <see cref="GetCachedQueryTableHeader" />   will recalculate Cache.
        /// </summary>
        /// <param name="queryId">ID of the AVR query</param>
        /// <param name="lang">Language</param>
        /// <param name="userId">ID of current user for user-sensitive mode of AVR Cache (it equals null if this mode is not activated)</param>
        [OperationContract]
        void InvalidateQueryCacheForLanguage(long queryId, string lang, long? userId);

        /// <summary>
        ///     Make current Cache of given query invalid for all supported languages and users (including not specified).
        ///     So next time method <see cref="GetCachedQueryTableHeader" />   will recalculate Cache.
        /// </summary>
        /// <param name="queryId">ID of the AVR query</param>
        [OperationContract]
        void InvalidateQueryCache(long queryId);

        /// <summary>
        ///     Delete old Cache of given query, language, and user (if specified).
        /// </summary>
        /// <param name="queryId">ID of the AVR query</param>
        /// <param name="lang">Language</param>
        /// <param name="leaveLastRecord">Defines is last record of cache should be leaved</param>
        /// <param name="userId">ID of current user for user-sensitive mode of AVR Cache (it equals null if this mode is not activated)</param>
        void DeleteQueryCacheForLanguage(long queryId, string lang, bool leaveLastRecord, long? userId);

        /// <summary>
        ///     Returns Date and Time when query cache was refreshed
        /// </summary>
        /// <param name="queryId">ID of the AVR query</param>
        /// ///
        /// <param name="lang">Language</param>
        /// <param name="userId">ID of current user for user-sensitive mode of AVR Cache (it equals null if this mode is not activated)</param>
        /// <returns></returns>
        [OperationContract]
        DateTime GetQueryRefreshDateTime(long queryId, string lang, long? userId);

        /// <summary>
        ///     Returns Date  of latest user request of query cache
        /// </summary>
        /// <param name="queryId">ID of the AVR query</param>
        /// <param name="userId">ID of current user for user-sensitive mode of AVR Cache (it equals null if this mode is not activated)</param>
        /// <returns></returns>
        DateTime? GetsQueryCacheUserRequestDate(long queryId, long? userId);

        /// <summary>
        ///     Returns ist of users requested the query cache later than specified date including null value
        /// </summary>
        /// <param name="queryId">ID of the AVR query</param>
        /// <param name="requestedLaterThanDate">Date and time to compare with date of query cache request (it can be null to skip comparison)</param>
        /// <returns></returns>
        List<long?> GetQueryCacheValidUsers(long queryId, DateTime? requestedLaterThanDate);

        /// <summary>
        ///     Returns ist of users not requested the query cache later than specified date
        /// </summary>
        /// <param name="queryId">ID of the AVR query</param>
        /// <param name="requestedLaterThanDate">Date and time to compare with date of query cache request</param>
        /// <returns></returns>
        List<long> GetQueryCacheInvalidUsers(long queryId, DateTime requestedLaterThanDate);


        /// <summary>
        ///     Returns version of the service
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        Version GetServiceVersion();

        /// <summary>
        ///     Returns actual EIDSS database name from connection string
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DatabaseNames GetDatabaseName();

        /// <summary>
        ///     Retrieves list of existing queries
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<long> GetQueryIdList();

        /// <summary>
        ///     Retrieves list of existing Layouts and its versions
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<long> GetLayoutIdList();

        /// <summary>
        /// Create layout copy on given language (include pivot, view, chart and map copy)
        /// </summary>
        /// <param name="layoutId">ID of the AVR Layout to copy</param>
        /// <param name="lang">Language</param>
        /// <param name="userId">ID of current user for user-sensitive mode of AVR Cache (it equals null if this mode is not activated)</param>
        /// <returns>ID of created Layout Copy</returns>
        [OperationContract]
        long CopyLayout(long layoutId, string lang);
    }
}