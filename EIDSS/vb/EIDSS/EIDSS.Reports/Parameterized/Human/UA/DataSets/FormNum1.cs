using EIDSS.Reports.BaseControls.BaseDataSetTableAdapters;
using System.Data.SqlClient;
using System.Linq;

namespace EIDSS.Reports.Parameterized.Human.UA.DataSets.FormNum1TableAdapters
{
    public partial class spRepHumanUAFormNum1TableAdapter
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
namespace EIDSS.Reports.Parameterized.Human.UA.DataSets {
    
    
    public partial class FormNum1 {
    }
}
