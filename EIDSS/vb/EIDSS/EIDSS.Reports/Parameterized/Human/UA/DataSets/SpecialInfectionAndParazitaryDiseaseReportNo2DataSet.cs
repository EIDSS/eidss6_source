using System.Data.SqlClient;
using System.Linq;

namespace EIDSS.Reports.Parameterized.Human.UA.DataSets {
    
    
    public partial class SpecialInfectionAndParazitaryDiseaseReportNo2DataSet {
    }
}

namespace EIDSS.Reports.Parameterized.Human.UA.DataSets.SpecialInfectionAndParazitaryDiseaseReportNo2DataSetTableAdapters 
{
    public partial class spRepHumForm2UAHeaderTableAdapter
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

    public partial class spRepHumForm2UADataTable3TableAdapter
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

    public partial class spRepHumForm2UADataTable2TableAdapter
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
    
    
    public partial class spRepHumForm2UADataTable1TableAdapter 
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
