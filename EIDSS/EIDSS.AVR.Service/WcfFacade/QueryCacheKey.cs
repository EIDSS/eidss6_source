using System;

namespace EIDSS.AVR.Service.WcfFacade
{
    [Serializable]
    public class QueryCacheKey
    {
        public QueryCacheKey(long queryId, string lang, bool isArchive, long? userId = null)
        {
            QueryId = queryId;
            Lang = lang;
            IsArchive = isArchive;
            UserId = userId;
        }

        public long QueryId { get; set; }
        public string Lang { get; set; }
        public bool IsArchive { get; set; }
        public long? UserId { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = QueryId.GetHashCode();
                hashCode = (hashCode * 397) ^ (Lang != null ? Lang.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ IsArchive.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(QueryCacheKey a, QueryCacheKey b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            {
                return true;
            }
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return false;
            }
            return a.Equals(b);
        }

        public static bool operator !=(QueryCacheKey a, QueryCacheKey b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }
            return Equals((QueryCacheKey) obj);
        }

        protected bool Equals(QueryCacheKey other)
        {
            return QueryId == other.QueryId && string.Equals(Lang, other.Lang) && IsArchive.Equals(other.IsArchive)
                 && (((!UserId.HasValue) && (!other.UserId.HasValue)) || (UserId.HasValue && other.UserId.HasValue && (UserId.Value == other.UserId.Value)));
        }

        public override string ToString()
        {
            return string.Format("ID={0}, Lang={1}, IsArchive={2}, UserID={3}", 
                QueryId, Lang, IsArchive, UserId.HasValue ? UserId.Value.ToString() : "null");
        }
    }
}