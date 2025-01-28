using System.Data;
using bv.common.Configuration;
using bv.common.db.Core;
using bv.common.Objects;
using bv.model.BLToolkit;
using EIDSS;
using eidss.model.Core.CultureInfo;
using eidss.model.WcfService;

namespace eidss.avr.service.WcfFacade
{
    public class EidssAvrServiceInitializer
    {
        private static readonly object m_SyncRoot = new object();
        private static BaseLanguageProcessor m_LanguageProcessor;

        public static void CheckAndInitEidssCore()
        {
            lock (m_SyncRoot)
            {
                if (!IsLookupCacheInitialized())
                {
                    InitEidssCore();
                    StoredProcParamsCache.ClearCache();
                    LookupCache.Reload();
                }
            }
        }

        public static bool IsLookupCacheInitialized()
        {
            LookupTableInfo lookupTable = LookupCache.LookupTables[LookupTables.QuerySearchField.ToString()];
            DataTable result = LookupCache.Fill(lookupTable);
            return (result != null);
        }

        public static void InitEidssCore()
        {
            EidssServiceInitializer.InitEidssCore();

            var avrCredentials = new ConnectionCredentials(null, "AvrService");
            DbManagerFactory.SetSqlFactory(avrCredentials.ConnectionString, DatabaseType.Avr, avrCredentials.CommandTimeout);
            var archCredentials = new ConnectionCredentials(null, "Archive");
            DbManagerFactory.SetSqlFactory(archCredentials.ConnectionString, DatabaseType.Archive, archCredentials.CommandTimeout);

            EIDSS_LookupCacheHelper.Init();

            m_LanguageProcessor = new BaseLanguageProcessor();
            m_LanguageProcessor.InitLanguages();
        }
    }
}