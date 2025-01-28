using System;
using bv.common.Core;
using DevExpress.XtraPivotGrid.Data;
using eidss.model.Reports.OperationContext;

namespace eidss.avr.Tools.DataTransactions
{
    public class DataTransaction : IDisposable
    {
        private readonly PivotGridData m_Data;
        private IContextKeeper m_Keeper;
        private readonly Action m_AfterDisposeTransaction;

        public DataTransaction()
        {
        }

        public DataTransaction(IContextKeeper keeper, Action afterDisposeTransaction, PivotGridData data)
        {
            Utils.CheckNotNull(keeper, "keeper");
            Utils.CheckNotNull(data, "data");
            m_Keeper = keeper;
            m_Data = data;

            m_AfterDisposeTransaction = afterDisposeTransaction;

            m_Data.BeginUpdate();
        }
        
        public bool HasData
        {
            get { return m_Data != null; }
        }

        public void Dispose()
        {
            if (m_Data != null)
            {
                using (m_Keeper.CreateNewContext(ContextValue.PivotSuppressRefreshing))
                {
                    m_Data.EndUpdate();
                }
            }
            if (m_AfterDisposeTransaction != null)
            {
                m_AfterDisposeTransaction();
            }
        }
    }
}