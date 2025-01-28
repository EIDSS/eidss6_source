using bv.common.Configuration;
using bv.common.Core;
using bv.common.Diagnostics;
using bv.model.BLToolkit;
using bv.model.Model.Core;
using eidss.model.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;

namespace eidss.model.Core
{
    public class ForcedDisconnectionClient
    {
        private static readonly object m_SyncObject = new object();
        private readonly Timer m_DelayedDisconnectionTimer;
        private static ForcedDisconnectionClient m_Instance;
        private bool m_trackingConnection;
        private string m_ClientID;
        private EidssUserContext m_EidssUserContext = (EidssUserContext)EidssUserContext.Instance;
        public event EventHandler Disconnected;
        public static ForcedDisconnectionClient Instance
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new ForcedDisconnectionClient();
                return m_Instance;
            }
            set { m_Instance = value; }

        }
        public ForcedDisconnectionClient(ConnectionCredentials credentials = null, string clientID = null)
        {
            m_DelayedDisconnectionTimer = new Timer(IsDisconnected, null,
                                Timeout.Infinite,
                                Timeout.Infinite);
            m_trackingConnection = false;
            if (Utils.IsEmpty(clientID))
                m_ClientID = ModelUserContext.ClientID;
            else
                m_ClientID = clientID;
            if(credentials!=null)
                DbManagerFactory.SetSqlFactory(credentials.ConnectionString, DatabaseType.Main, credentials.CommandTimeout);
            SubscribeToEvent((long) EventType.DisconnectParallelSessions);

        }


        private DbManagerProxy GetDbManager()
        {
                return DbManagerFactory.Factory.Create(ModelUserContext.Instance);
        }
        public bool SubscribeToEvent(long idfsEventTypeId)
        {
            using (DbManagerProxy manager = GetDbManager())
            {
                try
                {
                    manager.SetSpCommand("dbo.spEventLog_SubscribeToEvent",
                                          manager.Parameter("@idfClientID", m_ClientID),
                                          manager.Parameter("@idfsEventTypeID", idfsEventTypeId)
                                                 ).ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    Dbg.Debug("event subscription is fail: {0}", ex);
                    return false;
                }
            }
        }

        public bool HasParallelSessions(string userName)
        {
            using (DbManagerProxy manager = GetDbManager())
            {
                try
                {
                    return EidssUserContext.Instance.ParallelSessionsCount(userName, m_ClientID) > 0;
                }
                catch (Exception ex)
                {
                    Dbg.Debug("event subscription is fail: {0}", ex);
                    return false;
                }
            }
        }


        public void IsDisconnected()
        {
            lock (m_SyncObject)
            {
                    Dbg.ConditionalDebug(DebugDetalizationLevel.Low,
                                         "Started tracking other logins at {0}",
                                         DateTime.Now);
                    m_trackingConnection = true;
                    m_DelayedDisconnectionTimer.Change(1000, 10000);
            }
        }

        private void IsDisconnected(object state)
        {
            lock (m_SyncObject)
            {
                Dbg.ConditionalDebug(DebugDetalizationLevel.Low, "tracking Is disconnected");
                    bool isDisconnected = m_EidssUserContext.IsDisconnected(EidssUserContext.User.ID, m_ClientID);
                    if (isDisconnected)
                    {
                        m_DelayedDisconnectionTimer.Change(Timeout.Infinite, Timeout.Infinite);
                        m_trackingConnection = false;
                        if (Disconnected != null)
                            Disconnected.Invoke(this, new EventArgs());
                        //logout
                    }
            }
        }

        public void DisconnectMySession()
        {
             Dbg.ConditionalDebug(DebugDetalizationLevel.Low,
                                 "removing my session at {0}", DateTime.Now);

            using (DbManagerProxy manager = GetDbManager())
            {
                try
                {
                    m_EidssUserContext.DeleteLocalConnectionContext(EidssUserContext.ClientID);
                }
                catch (Exception ex)
                {
                    Dbg.Debug("event creation fail: {0}", ex);
                }
            }
        }

        public void RemoveParallelSessions(string userName)
        {
            Dbg.ConditionalDebug(DebugDetalizationLevel.Low,
                                 "removing parallel sessions at {0}", DateTime.Now);

            using (DbManagerProxy manager = GetDbManager())
            {
                try
                {
                    m_EidssUserContext.DeleteParallelSessions(userName, EidssUserContext.ClientID);
                    m_EidssUserContext.Event(manager, 0, EventType.DisconnectParallelSessions, null, "Sessions Disconnection");
                }
                catch (Exception ex)
                {
                    Dbg.Debug("event creation fail: {0}", ex);
                }
            }
            
        }
    }
}
