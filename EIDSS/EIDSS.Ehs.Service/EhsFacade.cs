using bv.common.Configuration;
using bv.common.Core;
using bv.common.db.Core;
using bv.model.BLToolkit;
using bv.model.Model.Core;
using bv.winclient.Core;
using eidss.model.Core;
using eidss.model.Helpers;
using eidss.model.Model.UploadEhs;
using eidss.model.Schema;
using eidss.model.Trace;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace EIDSS.EHS.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EhsFacade" in both code and config file together.
    public class EhsFacade : IEhsFacade
    {
        public const string TraceTitle = "WCF Service Call";
        private static readonly TraceHelper m_Trace = new TraceHelper(TraceHelper.EhsCategory);

        private static readonly object m_SyncLock = new object();
        private static volatile bool m_Initialized;

        public ValidateDataResult ValidateData(string patientJson, string eventJson, long idfsUploadEhs, string lang)
        {
            m_Trace.TraceMethodCall(Utils.GetCurrentMethodName(), TraceTitle);
            InitEidssCoreIfNeeded();

            var result = new ValidateDataResult();

            try
            {
                var patientXml = JsonHelper.ConvertToXml(patientJson).OuterXml;
                var eventXml = JsonHelper.ConvertToXml(eventJson).OuterXml;

                using (DbManagerProxy manager = DbManagerFactory.Factory.Create(ModelUserContext.Instance))
                {
                    manager.SetSpCommand
                                ("spUploadEhsValidation_SelectDetail"
                                  , manager.Parameter("@idfsUploadEhs", idfsUploadEhs)
                                  , manager.Parameter("@PatientXml", patientXml )
                                  , manager.Parameter("@EventXml", eventXml)
                                  , manager.Parameter("@LangID", lang)
                                  , manager.OutputParameter("@hasPatientErrors", DBNull.Value)
                                  , manager.OutputParameter("@hasEventErrors", DBNull.Value)
                                  , manager.OutputParameter("@PatientJsonWithErrors", DBNull.Value)
                                  , manager.OutputParameter("@EventJsonWithErrors", DBNull.Value)
                                ).ExecuteNonQuery();

                    bool hasPatientErrors = (bool)manager.Parameter("@hasPatientErrors").Value;
                    bool hasEventErrors = (bool)manager.Parameter("@hasEventErrors").Value;
                    string patientJsonWithErrors = manager.Parameter("@PatientJsonWithErrors").Value.ToString();
                    string eventJsonWithErrors = manager.Parameter("@EventJsonWithErrors").Value.ToString();

                    if (hasPatientErrors)
                    {
                        result.PatientError = UploadEhsFileResult.ValidationError;
                        result.PatientState = UploadEhsMasterState.HasErrors;
                        result.HasPatientErrorFile = true;
                        result.PatientFileContent = ConvertToByteArray(patientJsonWithErrors);
                    }
                    else
                    {
                        result.PatientState = UploadEhsMasterState.ReadyForCheckExistingPatients;
                        result.PatientError = UploadEhsFileResult.Success;
                        result.HasPatientErrorFile = false;
                    }

                    if (hasEventErrors)
                    {
                        result.EventError = UploadEhsFileResult.ValidationError;
                        result.EventState = UploadEhsMasterState.HasErrors;
                        result.HasEventErrorFile = true;
                        result.EventFileContent = ConvertToByteArray(eventJsonWithErrors);
                    }
                    else
                    {
                        result.EventState = UploadEhsMasterState.ReadyForSave;
                        result.EventError = UploadEhsFileResult.Success;
                        result.HasEventErrorFile = false;
                    }
                }
            }
            catch (Exception ex)
            {
                WrapSqlException(ex);
                throw;
            }

            return result;
        }

        public SaveDataResult SaveUploadedData(long idfsUploadEhs, long idfPersonEnteredBy, long idfUserID, string lang)
        {

            var result = new SaveDataResult();
            try
            {
                m_Trace.TraceMethodCall(Utils.GetCurrentMethodName(), TraceTitle);
                InitEidssCoreIfNeeded();

                using (DbManagerProxy manager = DbManagerFactory.Factory.Create(ModelUserContext.Instance))
                {

                    manager.SetSpCommand
                        ("spUploadEhsMaster_Post"
                          , manager.Parameter("@idfsUploadEhs", idfsUploadEhs)
                          , manager.Parameter("@idfPersonEnteredBy", idfPersonEnteredBy)
                          , manager.Parameter("@idfUserID", idfUserID)
                          , manager.Parameter("@LangID", lang)
                          , manager.OutputParameter("@savedOk", DBNull.Value)
                          , manager.OutputParameter("@EventJsonWithResults", DBNull.Value)
                        ).ExecuteNonQuery();

                    result.IsSuccessful = (bool)manager.Parameter("@savedOk").Value;
                    string content = manager.Parameter("@EventJsonWithResults").Value.ToString();
                    result.EventJsonWithResults = ConvertToByteArray(content);

                }

                return result;
            }
            catch (Exception ex)
            {
                WrapSqlException(ex);
                throw;
            }
        }

        private byte[] ConvertToByteArray(string json)
        {
            if (string.IsNullOrEmpty(json))
                return null;

            byte[] bytes = Encoding.UTF8.GetBytes(json);
            return bytes;
        }

        private static void InitEidssCoreIfNeeded()
        {
            lock (m_SyncLock)
            {
                if (!m_Initialized)
                {
                    Config.ReloadSettings();

                    ConnectionManager.DefaultInstance.SetCredentials();

                    var mainCredentials = new ConnectionCredentials();
                    DbManagerFactory.SetSqlFactory(mainCredentials.ConnectionString, DatabaseType.Main, mainCredentials.CommandTimeout);

                    EidssUserContext.Init();

                    CustomCultureHelper.CurrentCountry = EidssSiteContext.Instance.CountryID;
                    m_Initialized = true;
                }
            }
        }

        private static void WrapSqlException(Exception ex)
        {
            string description = SqlExceptionHandler.GetExceptionDescription(ex);
            if (!String.IsNullOrEmpty(description))
            {
                throw new EhsDataException(description, ex);
            }
        }

    }
}
