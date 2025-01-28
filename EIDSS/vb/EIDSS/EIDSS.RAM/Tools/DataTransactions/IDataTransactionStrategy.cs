using DevExpress.XtraPivotGrid.Data;
using eidss.model.Reports.OperationContext;

namespace eidss.avr.Tools.DataTransactions
{
    public interface IDataTransactionStrategy
    {
        DataTransaction BeginTransaction(IContextKeeper keeper, PivotGridData data);
    }
}