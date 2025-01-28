using System.Data.SqlClient;
using System.Linq;

namespace EIDSS.Reports.Parameterized.Human.UA.DataSets
{
    
    
    public partial class UACov19List {
    }
}
namespace EIDSS.Reports.Parameterized.Human.UA.DataSets.UACov19ListTableAdapters
{
    public partial class spRepHumanUACov19ListTableAdapter
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
