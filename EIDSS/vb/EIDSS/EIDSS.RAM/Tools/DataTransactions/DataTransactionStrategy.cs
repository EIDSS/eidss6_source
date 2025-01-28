using bv.common.Core;
using DevExpress.XtraPivotGrid.Data;
using eidss.model.Reports.OperationContext;
using System;

namespace eidss.avr.Tools.DataTransactions
{
    public class DataTransactionStrategy : IDataTransactionStrategy
    {
        private PivotGridData m_Data;
        private DataTransaction m_CurrentTransaction;
        

        public DataTransaction BeginTransaction(IContextKeeper keeper, PivotGridData data)
        {
            if (m_CurrentTransaction == null)
            {
                Utils.CheckNotNull(data, "data");
                Utils.CheckNotNull(keeper, "keeper");
                m_Data = data;
                m_CurrentTransaction = new DataTransaction(
                    keeper,
                    () => { m_CurrentTransaction = null; },
                    m_Data);
                return m_CurrentTransaction;
            }
            return new DataTransaction();
        }
    }
}