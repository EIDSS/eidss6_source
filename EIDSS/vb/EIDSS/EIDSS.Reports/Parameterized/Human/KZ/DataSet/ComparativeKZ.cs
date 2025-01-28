using System.Data.SqlClient;
using System.Linq;

namespace EIDSS.Reports.Parameterized.Human.KZ.DataSet
{
}
namespace EIDSS.Reports.Parameterized.Human.KZ.DataSet.ComparativeKZTableAdapters
{
    public partial class spRepHumComparativeKZTableAdapter
    {
        internal int CommandTimeout
        {
            get { return CommandCollection.Select(c => c.CommandTimeout).FirstOrDefault(); }
            set
            {
                foreach (SqlCommand command in CommandCollection)
                {
                    command.CommandTimeout = value;
                }
            }
        }
    }
}

namespace EIDSS.Reports.Parameterized.Human.KZ.DataSet {
    
    
    public partial class ComparativeKZ {
    }
}