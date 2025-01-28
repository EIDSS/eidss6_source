using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using eidss.model.Avr.Pivot;
using eidss.model.Avr.View;
using eidss.model.AVR.ServiceData;
using eidss.model.AVR.SourceData;
using eidss.model.Core;
using eidss.model.WindowsService;
using eidss.model.WindowsService.Serialization;

namespace eidss.avr.db.CacheReceiver
{
    public class AvrCacheReceiver : AvrCacheConverter
    {
        protected int m_ReceiveCounter;
        private readonly IAVRFacade m_Facade;

        public AvrCacheReceiver(IAVRFacade facade)
        {
            m_Facade = facade;
        }

        public ChartExportDTO ExportChartToJpg(ChartTableModel tableModel)
        {
            BaseTableDTO serializedDTO = BinarySerializer.SerializeFromTable(tableModel.Table);
            BaseTableDTO zippedDTO = BinaryCompressor.Zip(serializedDTO);
            zippedDTO.TableName = string.Empty;

            var zippedData = new ChartTableDTO(tableModel.ViewId, tableModel.Lang, zippedDTO, tableModel.ChartSettings, tableModel.ChartType,
                tableModel.TextPatterns, tableModel.Width, tableModel.Height, tableModel.UserId);
            ChartExportDTO result = m_Facade.ExportChartToJpg(zippedData);
            return result;
        }

        public AvrPivotViewModel GetCachedView(string sessionId, long layoutId, string lang)
        {
            long? userId = null;
            if (EidssSiteContext.Instance.AVRUserSensitiveMode && (EidssUserContext.Instance.CurrentUser.ID != null) && (EidssUserContext.Instance.CurrentUser.ID is long))
            {
                userId = (long)EidssUserContext.Instance.CurrentUser.ID;
            }
            ViewDTO viewDTO = m_Facade.GetCachedView(sessionId, layoutId, lang, userId);

            string xmlViewStructure = BinaryCompressor.UnzipString(viewDTO.BinaryViewHeader);
            AvrView view = AvrViewSerializer.Deserialize(xmlViewStructure);

            BaseTableDTO unzippedDTO = BinaryCompressor.Unzip(viewDTO);
            DataTable viewData = BinarySerializer.DeserializeToTable(unzippedDTO);

            var model = new AvrPivotViewModel(view, viewData);

            return model;
        }

        public CachedQueryResult GetCachedQueryTable
            (long queryId, string lang, bool isArchive, string filter,
            LayoutBaseValidatorWaiter validatorWaiter, long queryCacheId = -1, long? userId = null)
        {
            if ((!userId.HasValue) && 
                (EidssSiteContext.Instance.AVRUserSensitiveMode && 
                 (EidssUserContext.Instance.CurrentUser.ID != null) && 
                 (EidssUserContext.Instance.CurrentUser.ID is long)))
            {
                userId = (long)EidssUserContext.Instance.CurrentUser.ID;
            }

            m_ReceiveCounter = 0;
            QueryTableHeaderDTO headerDTO = (queryCacheId > 0)
                ? m_Facade.GetConcreteCachedQueryTableHeader(queryCacheId, queryId, lang, isArchive, userId)
                : m_Facade.GetCachedQueryTableHeader(queryId, lang, isArchive, userId);

            var headerModel = new QueryTableHeaderModel(headerDTO);

            return GetCachedQueryTable(queryId, headerDTO, filter, tokenSource => ReceiveTableBodyPackets(headerModel, tokenSource),
                validatorWaiter);
        }

        private QueryTablePacketDTO[] ReceiveTableBodyPackets(QueryTableHeaderModel header, CancellationTokenSource tokenSource)
        {
            var result = new QueryTablePacketDTO[header.PacketCount];

            var runningTasks = new[]
            {
                Task.Factory.StartNew(() => ReceiveTableBodyPacket(header, result, tokenSource), tokenSource.Token),
                Task.Factory.StartNew(() => ReceiveTableBodyPacket(header, result, tokenSource), tokenSource.Token),
                Task.Factory.StartNew(() => ReceiveTableBodyPacket(header, result, tokenSource), tokenSource.Token)
            };

            Task.WaitAll(runningTasks);

            return result;
        }

        private void ReceiveTableBodyPacket
            (QueryTableHeaderModel header, QueryTablePacketDTO[] resultPackets, CancellationTokenSource tokenSource)
        {
            while (!tokenSource.Token.IsCancellationRequested)
            {
                try
                {
                    int counter = Interlocked.Increment(ref m_ReceiveCounter);
                    if (counter > header.PacketCount)
                    {
                        return;
                    }

                    QueryTablePacketDTO packet = m_Facade.GetCachedQueryTablePacket(header.QueryCacheId, counter - 1, header.PacketCount, header.UserId);
                    resultPackets[counter - 1] = packet;
                    QueueHelper.ThreadSafeEnqueue(m_ReceivedQueue, packet, m_ReceiveSyncLock, tokenSource);
                }
                catch (Exception)
                {
                    tokenSource.Cancel();
                    throw;
                }
            }
        }
    }
}