using System.Linq;
using bv.model.BLToolkit;
using eidss.model.Avr.Tree;

namespace eidss.model.Schema
{
    public partial class AvrQueryLookup
    {
        public static explicit operator AvrTreeElement(AvrQueryLookup query)
        {
            var treeElement = new AvrTreeElement(query.idflQuery,
                null,
                query.idfsGlobalQuery,
                AvrTreeElementType.Query,
                query.idflQuery,
                query.DefQueryName,
                query.QueryName,
                query.strDescription,
                query.blnReadOnly,
                false,
                query.strDescription /*strEnglishDescription*/);
            return treeElement;
        }

        public static AvrQueryLookup GetAvrQueryLookupById(long queryId)
        {
            AvrQueryLookup foundQuery;
            LookupManager.AddObject("Query", null, Accessor.Instance(null).GetType(), "_SelectListInternal");
            using (var manager = DbManagerFactory.Factory.Create())
            {
                var accessor = Accessor.Instance(null);
                var lookup = accessor.SelectLookupList(manager, queryId);
                foundQuery = lookup.SingleOrDefault();
            }

            return foundQuery;
        }
    }
}