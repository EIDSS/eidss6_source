using bv.common.Configuration;
using bv.model.BLToolkit;

namespace eidss.model.Helpers
{
    public static class ArchiveSqlHelper
    {
        public static void InitSqlFactory()
        {
            if (DbManagerFactory.Factory[DatabaseType.Archive] == null)
            {
                var credentials = GetCredentials();
                DbManagerFactory.SetSqlFactory(credentials.ConnectionString, DatabaseType.Archive, credentials.CommandTimeout);
            }
        }

        public static bool IsCredentialsCorrect()
        {
            return GetCredentials().IsCorrect;
        }

        private static ConnectionCredentials GetCredentials()
        {
            var credentials = new ConnectionCredentials(null, "Archive");
            return credentials;
        }
    }
}