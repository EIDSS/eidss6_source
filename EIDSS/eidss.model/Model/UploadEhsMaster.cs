using bv.common.Configuration;
using bv.model.BLToolkit;
using bv.model.Model.Core;
using eidss.model.Core;
using eidss.model.Helpers;
using eidss.model.Resources;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using bv.common.Core;
using Newtonsoft.Json.Linq;
using eidss.model.Model.UploadEhs;

namespace eidss.model.Schema
{
    public enum UploadEhsPatientResolution
    {
        Updated = 0,
        Created = 1,
        Dismissed = 2
    }
    
    public partial class UploadEhsMaster
    {
        partial void Disposed()
        {
            Clear();
        }

        private string _patientJson;
        private string _eventJson;
        private UploadEhsMasterState _patientState = UploadEhsMasterState.ReadyForUpload;
        private UploadEhsMasterState _eventState = UploadEhsMasterState.ReadyForUpload;
        private UploadEhsMasterState _state = UploadEhsMasterState.ReadyForUpload;
        private UploadEhsFileResult _lastPatientError = UploadEhsFileResult.Success;
        private UploadEhsFileResult _lastEventError = UploadEhsFileResult.Success;
        private string _lastPatientErrorMessage = string.Empty;
        private string _lastEventErrorMessage = string.Empty;
        private string _errorPatientFileName;
        private string _errorEventFileName;
        private string _resultEventFileName;


        public string PatientJson
        {
            get
            {
                return _patientJson;
            }
            set
            {
                _patientJson = value;
            }
        }
        public string EventJson
        {
            get
            {
                return _eventJson;
            }
            set
            {
                _eventJson = value;
            }
        }

        public string ErrorPatientFileName
        {
            get { return _errorPatientFileName; }
        }

        public string ErrorEventFileName
        {
            get { return _errorEventFileName; }
        }

        public string ResultEventFileName
        {
            get { return _resultEventFileName; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// Error code:
        /// 0 - no errors
        /// 1 - file(s) does(do) not exist
        /// 2 - usupported extension
        /// 3 - Invalid JSON format
        /// 4 - incorrect data format in some cells
        /// 5 - invalid file structure, error duirng file reading (some Ehs column names are absent or duplicated)
        /// </returns>
        public void GetItems(string patientFileName, string eventFileName, Stream patientfileStream, Stream eventfileStream)
        {
            using (var fileProcessor = new UploadEhsFileProcessor(patientfileStream, patientFileName, eventfileStream, eventFileName, this))
            {
                PatientFileName = patientFileName;
                EventFileName = eventFileName;

                var patientResult = fileProcessor.GetPatientItems();
                switch (patientResult)
                {
                    case UploadEhsFileResult.Success:
                        SetPatientState(UploadEhsMasterState.ReadyForValidation);
                        break;
                    case UploadEhsFileResult.ValidationError:
                        SetPatientState(UploadEhsMasterState.ReadyForValidation);
                        break;
                    case UploadEhsFileResult.InvalidFileStructure:
                        SetPatientError(patientResult, fileProcessor.PatientPropertyNamesErrors);
                        break;
                    default:
                        SetPatientError(patientResult);
                        break;
                }

                var eventResult = fileProcessor.GetEventItems();
                switch (eventResult)
                {
                    case UploadEhsFileResult.Success:
                        SetEventState(UploadEhsMasterState.ReadyForValidation);
                        break;
                    case UploadEhsFileResult.ValidationError:
                        SetEventState(UploadEhsMasterState.ReadyForValidation);
                        break;
                    case UploadEhsFileResult.InvalidFileStructure:
                        SetEventError(eventResult, fileProcessor.EventPropertyNamesErrors);
                        break;
                    default:
                        SetEventError(eventResult);
                        break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// Error code:
        /// 0 - no errors
        /// 1 - file doesn't exists
        /// 2 - usupported extension
        /// 3 - Ivalid header format (some Ehs column names are absent or duplicated)
        /// 4 - incorrect data format in some cells
        /// 5 - invalid file format, error duirng file reading
        /// </returns>
        public UploadEhsFileResult GetItems(string patientFilePath, string eventFilePath)
        {
            using (var fileProcessor = new UploadEhsFileProcessor(patientFilePath, eventFilePath, this))
            {
                Clear();
                PatientFileName = patientFilePath;
                EventFileName = eventFilePath;

                var patientResult = fileProcessor.GetPatientItems();
                switch (patientResult)
                {
                    case UploadEhsFileResult.Success:
                        SetPatientState(UploadEhsMasterState.ReadyForValidation);
                        break;
                    case UploadEhsFileResult.ValidationError:
                        SetPatientState(UploadEhsMasterState.ReadyForValidation);
                        break;
                    case UploadEhsFileResult.InvalidFileStructure:
                        SetPatientError(patientResult, fileProcessor.PatientPropertyNamesErrors);
                        break;
                    default:
                        SetPatientError(patientResult);
                        break;
                }

                var eventResult = fileProcessor.GetEventItems();
                switch (eventResult)
                {
                    case UploadEhsFileResult.Success:
                        SetEventState(UploadEhsMasterState.ReadyForValidation);
                        break;
                    case UploadEhsFileResult.ValidationError:
                        SetEventState(UploadEhsMasterState.ReadyForValidation);
                        break;
                    case UploadEhsFileResult.InvalidFileStructure:
                        SetEventError(eventResult, fileProcessor.EventPropertyNamesErrors);
                        break;
                    default:
                        SetEventError(eventResult);
                        break;
                }

                var result = patientResult | eventResult;
                return result;
            }
        }

        private static Dictionary<string, DateTime?> g_listUploadingSessions = new Dictionary<string, DateTime?>();

        private void RemoveObsoleteInactiveUploadingSessions()
        {
            lock (g_listUploadingSessions)
            {
                var dictObsoleteSessions = g_listUploadingSessions.Where(d => (d.Value == null) || (!d.Value.HasValue) || (DateTime.UtcNow.Subtract(d.Value.Value).TotalDays >= 1) || (DateTime.UtcNow.Subtract(d.Value.Value).TotalMinutes > BaseSettings.EhsSessionMaxActiveTimeInMinutes));
                if ((dictObsoleteSessions != null) && (dictObsoleteSessions.Count() > 0))
                {
                    var listObsoleteSessions = dictObsoleteSessions.Select(d => d.Key).ToList<string>();
                    foreach (var obsoleteKey in listObsoleteSessions)
                    {
                        g_listUploadingSessions.Remove(obsoleteKey);
                    }
                }
            }
        }

        public void EnterUploadingSession()
        {
            RemoveObsoleteInactiveUploadingSessions();
            lock (g_listUploadingSessions)
            {

                if (!g_listUploadingSessions.ContainsKey(ModelUserContext.ClientID))
                    g_listUploadingSessions.Add(ModelUserContext.ClientID, DateTime.UtcNow);
                else
                    g_listUploadingSessions[ModelUserContext.ClientID] = DateTime.UtcNow;
            }
        }
        public void LeaveUploadingSession()
        {
            lock (g_listUploadingSessions)
            {
                if (g_listUploadingSessions.ContainsKey(ModelUserContext.ClientID))
                    g_listUploadingSessions.Remove(ModelUserContext.ClientID);
            }
            RemoveObsoleteInactiveUploadingSessions();
        }
        public bool IsUploadingSessionAvailable()
        {
            lock (g_listUploadingSessions)
            {
                var activeSessions = g_listUploadingSessions.Where(d => (d.Value != null) && (d.Value.HasValue) && (DateTime.UtcNow.Subtract(d.Value.Value).TotalDays < 1) && (DateTime.UtcNow.Subtract(d.Value.Value).TotalMinutes <= BaseSettings.EhsSessionMaxActiveTimeInMinutes));
                var available = (activeSessions == null) || (activeSessions.Count() < BaseSettings.UploadingEhsSessionsCount);
                if (!available)
                    LogError.Log("ErrorLog", new ApplicationException(string.Format("Maximum number of avaliable import sessions:{0}", BaseSettings.UploadingEhsSessionsCount)));

                return available;
            }
        }

        public void SetResolutionForExistingPatient(long id, UploadEhsPatientResolution resolution)
        {
            var updateItem = ExistingPatientItems.FirstOrDefault(i => i.idfHumanActual == id);

            if (updateItem != null)
                updateItem.Resolution = (int)resolution;
        }

        public void DismissAllExistingPatientItems()
        {
            if ((_patientState != UploadEhsMasterState.HasExistingPatients) && (_patientState != UploadEhsMasterState.HasExistingPatientsResolutionErrors))
                return;

            var existingPatients = ExistingPatientItems.Where(d => !d.Resolved).ToList();

            existingPatients.ForEach(d =>
            {
                d.Resolution = (int)UploadEhsPatientResolution.Dismissed;
                d.Resolved = true;
            }
            );

            SetPatientState(UploadEhsMasterState.ReadyForSave);
        }

        public void FinalizeResolveExistingPatientItems()
        {
            if ((_patientState != UploadEhsMasterState.HasExistingPatients) && (_patientState != UploadEhsMasterState.HasExistingPatientsResolutionErrors))
                return;

            ExistingPatientItems.Where(d => d.Resolution == (int)UploadEhsPatientResolution.Dismissed
                                  || d.Resolution == (int)UploadEhsPatientResolution.Created
                                  || d.Resolution == (int)UploadEhsPatientResolution.Updated)
                .ToList()
                .ForEach(d =>
                {
                    d.Resolved = true;
                });

            if (ExistingPatientItems.Any(d => !d.Resolved))
                SetPatientState(UploadEhsMasterState.HasExistingPatientsResolutionErrors);
            else
                SetPatientState(UploadEhsMasterState.ReadyForSave);
        }

        public bool CheckForExistingPatientItems()
        {
            if (_patientState != UploadEhsMasterState.ReadyForCheckExistingPatients)
                return false;

            ExistingPatientItems.Clear();

            IList<UploadEhsExistingPatientItem> existingPatientList = new List<UploadEhsExistingPatientItem>();

            using (var manager = DbManagerFactory.Factory.Create(model.Core.EidssUserContext.Instance))
            {
                existingPatientList = UploadEhsExistingPatientItem.Accessor.Instance(null).SelectList(manager, idfsUploadEhs);
            }

            if ((existingPatientList != null) && (existingPatientList.Count > 0))
            {
                ExistingPatientItems.AddRange(existingPatientList.ToList());
            }

            if (ExistingPatientItems.Count > 0)
            {
                SetPatientState(UploadEhsMasterState.HasExistingPatients);
                return false;
            }

            SetPatientState(UploadEhsMasterState.ReadyForSave);

            return true;
        }


        //TODO: move to WCF
        public bool SaveResolutionForExistingPatientItems()
        {
            if (_patientState != UploadEhsMasterState.ReadyForSave)
                return false;

            if (ExistingPatientItems.Count == 0)
            {
                return true;
            }

            try
            {
                var resolutionXml = UploadEhsConverter.GetExistingPatientItemListResolutionXml(ExistingPatientItems.ToList());

                using (var manager = DbManagerFactory.Factory.Create(model.Core.EidssUserContext.Instance))
                {
                    manager.SetSpCommand
                            ("spUploadEhsExistingPatientItem_SetResolution"
                              , manager.Parameter("@idfsUploadEhs", idfsUploadEhs)
                              , manager.Parameter("@ResolutionXml", resolutionXml)
                              , manager.Parameter("@LangID", ModelUserContext.CurrentLanguage)
                            ).ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }


        public void Cancel()
        {
            try
            {
                using (var manager = DbManagerFactory.Factory.Create(model.Core.EidssUserContext.Instance))
                {
                    manager.SetSpCommand
                            ("spUploadEhs_DropTempTables"
                              , manager.Parameter("@idfsUploadEhs", idfsUploadEhs)
                           ).ExecuteNonQuery();
                }
            }
            catch(Exception e)
            {
            }

            SetState(UploadEhsMasterState.Canceled);
        }

        private string GetMessageByCode(UploadEhsFileResult errorCode, JsonStructureResult fileType, string errorParameter = "")
        {
            string errorMessage;

            switch (errorCode)
            {
                case UploadEhsFileResult.Success:
                    errorMessage = null;
                    break;
                case UploadEhsFileResult.NullFile:				 // file doesn't exist
                    errorMessage = EidssMessages.Get("msgUploadEhsFileNullFile");
                    break;
                case UploadEhsFileResult.InvalidJSONFormat:		// invalid JSON format
                    errorMessage = EidssMessages.Get("msgUploadEhsInvalidJSONFormat");
                    break;
                case UploadEhsFileResult.InvalidFileStructure:		// invalid file format, error duirng file reading
                    errorMessage = string.Format(EidssMessages.Get("msgUploadEhsInvalidFileStructure"), errorParameter);
                    if ((!string.IsNullOrEmpty(errorMessage)) && (errorMessage.Length > 483))
                        errorMessage = EidssMessages.Get("msgUploadEhsInvalidFileStructureNoAttributes");
                    break;
                case UploadEhsFileResult.ValidationError:
                case UploadEhsFileResult.IncorrectDataFormat:	// incorrect data format in some cells
                    errorMessage =
                        string.Format(
                            EidssMessages.Get("msgUploadEhsFileValidationError"),
                            GetErrorFileName(
                                fileType == JsonStructureResult.EventsFile ? m_EventFileName : m_PatientFileName,
                                fileType == JsonStructureResult.EventsFile ? _errorEventFileName : _errorPatientFileName));
                    break;
                case UploadEhsFileResult.UsupportedExtension:	// usupported extension
                    errorMessage = EidssMessages.Get("msgEhsUnsupportedExtention");
                    break;
                case UploadEhsFileResult.TooManyRecords:		// too many records
                    errorMessage = EidssMessages.Get("msgUploadFileSizeExceeded");
                    break;
                default:
                    errorMessage = EidssMessages.Get("msgUploadEhsFileError");
                    break;
            }

            return errorMessage;
        }

        public bool HasPatientsFileWithErrors()
        {
            return _patientState == UploadEhsMasterState.HasErrors && PatientErrorFileContent != null;

        }

        public bool HasEventsFileWithErrors()
        {
            return _eventState == UploadEhsMasterState.HasErrors && EventErrorFileContent != null;
        }

        public string[] GetLastPatientError()
        {
            if (_patientState != UploadEhsMasterState.HasErrors)
            {
                _lastPatientErrorMessage = string.Empty;
                _lastPatientError = UploadEhsFileResult.Success;
            }

            return (_lastPatientErrorMessage == null ? new string[] { } : _lastPatientErrorMessage.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));
        }
        public string[] GetLastEventError()
        {
            if (_eventState != UploadEhsMasterState.HasErrors)
            {
                _lastEventErrorMessage = string.Empty;
                _lastEventError = UploadEhsFileResult.Success;
            }

            return (_lastEventErrorMessage == null ? new string[] { } : _lastEventErrorMessage.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));
        }

        public string[] GetLastError()
        {
            var lastPatientError = GetLastPatientError();
            var lastEventError = GetLastEventError();

            var lastPatientErrorWithPrefix =
                    ((lastPatientError != null) && (lastPatientError.Length > 0)) ?
                        lastPatientError.Select(x => EidssMessages.Get("msgEhsPatientFileName") + ": " + x).ToArray() :
                        new string[] { };

            var lastEventErrorWithPrefix =
                    ((lastEventError != null) && (lastEventError.Length > 0)) ?
                    lastEventError.Select(x => EidssMessages.Get("msgEhsEventFileName") + ": " + x).ToArray() :
                    new string[] { };

            var lastError = lastPatientErrorWithPrefix.Concat(lastEventErrorWithPrefix).ToArray();

            return lastError;
        }


        private void SetPatientError(UploadEhsFileResult errorCode, string errorMessage, string errorParameter = "")
        {
            SetPatientState(UploadEhsMasterState.HasErrors);
            _lastPatientError = errorCode;
            _lastPatientErrorMessage = (!(string.IsNullOrEmpty(errorMessage)) && errorMessage.Contains("{0}")) ? string.Format(errorMessage, errorParameter) : errorMessage;
        }
        private void SetEventError(UploadEhsFileResult errorCode, string errorMessage, string errorParameter = "")
        {
            SetEventState(UploadEhsMasterState.HasErrors);
            _lastEventError = errorCode;
            _lastEventErrorMessage = (!(string.IsNullOrEmpty(errorMessage)) && errorMessage.Contains("{0}")) ? string.Format(errorMessage, errorParameter) : errorMessage;
        }

        public void SetPatientError(UploadEhsFileResult errorCode, string errorParameter = "")
        {
            SetPatientError(errorCode, GetMessageByCode(errorCode, JsonStructureResult.PatientFile, errorParameter), errorParameter);
        }


        public void SetEventError(UploadEhsFileResult errorCode, string errorParameter = "")
        {
            SetEventError(errorCode, GetMessageByCode(errorCode, JsonStructureResult.EventsFile, errorParameter), errorParameter);
        }

        public void SetPatientError(string errorMessage)
        {
            SetPatientError(UploadEhsFileResult.Unknown, errorMessage);
        }
        public void SetEventError(string errorMessage)
        {
            SetEventError(UploadEhsFileResult.Unknown, errorMessage);
        }

        public UploadEhsMasterState GetState()
        {
            return _patientState | _eventState;
        }

        public UploadEhsMasterState GetPatientState()
        {
            return _patientState;
        }

        public UploadEhsMasterState GetEventState()
        {
            return _eventState;
        }

        private void SetPatientState(UploadEhsMasterState newState)
        {
            _patientState = newState;

            if (newState != UploadEhsMasterState.HasErrors)
                PatientErrorFileContent = null;
        }

        private void SetEventState(UploadEhsMasterState newState)
        {
            _eventState = newState;

            if (newState != UploadEhsMasterState.HasErrors)
                EventErrorFileContent = null;

            if (newState != UploadEhsMasterState.Saved)
                EventResultFileContent = null;
        }

        private void SetState(UploadEhsMasterState newState)
        {
            SetPatientState(newState);
            SetEventState(newState);
        }


        public void Clear()
        {
            try
            {
                if (ExistingPatientItems != null)
                    ExistingPatientItems.Clear();

                PatientFileName = null;
                EventFileName = null;

                _lastPatientErrorMessage = string.Empty;
                _lastPatientError = UploadEhsFileResult.Success;
                _lastEventErrorMessage = string.Empty;
                _lastEventError = UploadEhsFileResult.Success;

                PatientErrorFileContent = null;
                EventErrorFileContent = null;
                EventResultFileContent = null;

                _resultEventFileName = null;
                _errorPatientFileName = null;
                _errorEventFileName = null;

                SetState(UploadEhsMasterState.ReadyForUpload);

            }
            catch (Exception ex)
            {
                LogError.Log("ErrorLog", ex);
            }
        }

        public string GetErrorFileName(string fileName, string errorFileName, JsonStructureResult type = 0)
        {
            var clearFileName = string.IsNullOrWhiteSpace(fileName) ? string.Empty : Path.GetFileNameWithoutExtension(fileName);

            if (string.IsNullOrWhiteSpace(errorFileName) || !errorFileName.Contains(clearFileName))
            {
                string ext = ".json";
                if (!string.IsNullOrWhiteSpace(fileName))
                {
                    ext = Path.GetExtension(fileName);
                    if (ext != ".json")
                        ext = ".json";
                }
                errorFileName = string.Format("{0}_Errors_{1}{2}", clearFileName, DateTime.Now.ToString("yyyyMMddHHmmss"), ext);
            }

            switch (type)
            {
                case JsonStructureResult.EventsFile:
                    _errorEventFileName = errorFileName;
                    break;
                case JsonStructureResult.PatientFile:
                    _errorPatientFileName = errorFileName;
                    break;
                default:
                    //We don't want to save any errors
                    break;
            }

            return errorFileName;
        }

        public string GetResultEventFileName()
        {
            var clearFileName = string.IsNullOrWhiteSpace(m_EventFileName) ? string.Empty : Path.GetFileNameWithoutExtension(m_EventFileName);

            if (string.IsNullOrWhiteSpace(_resultEventFileName) || !_resultEventFileName.Contains(clearFileName))
            {
                string ext = ".json";
                if (!string.IsNullOrWhiteSpace(m_EventFileName))
                {
                    ext = Path.GetExtension(m_EventFileName);
                    if (ext != ".json")
                        ext = ".json";
                }
                _resultEventFileName = string.Format("{0}_Results_{1}{2}", clearFileName, DateTime.Now.ToString("yyyyMMddHHmmss"), ext);
            }

            return _resultEventFileName;
        }

        public void ApplyValidationResults(ValidateDataResult result)
        {
            if (result == null)
            {
                PatientErrorFileContent = null;
                SetPatientError(UploadEhsFileResult.Unknown);
                EventErrorFileContent = null;
                SetEventError(UploadEhsFileResult.Unknown);
                return;
            }

            if (result.PatientState == UploadEhsMasterState.HasErrors)
            {
                PatientErrorFileContent = result.PatientFileContent;
                SetPatientError(result.PatientError);
            }
            else
            {
                PatientErrorFileContent = null;
                SetPatientState(result.PatientState);
            }

            if (result.EventState == UploadEhsMasterState.HasErrors)
            {
                EventErrorFileContent = result.EventFileContent;
                SetEventError(result.EventError);
            }
            else
            {
                EventErrorFileContent = null;
                SetEventState(result.EventState);
            }
        }

        public void ReflectSaveResult(SaveDataResult result)
        {
            if ((result == null) || ((result != null) && (!result.IsSuccessful)))
            {
                SetPatientError(UploadEhsFileResult.Unknown);
                SetState(UploadEhsMasterState.Failed);
                return;
            }
            
            SetState(UploadEhsMasterState.Saved);
            EventResultFileContent = result.EventJsonWithResults;
        }
    }

    /*WorkFlow:
	  
     ReadyForUpload->HasErrors
     *			|
     *			->ReadyForValidation->HasErrors->ReadyForUpload
     *							|
     *							->ReadyForCheckExistingPatients->HasExistingPatients->ReadyForSave
     *												|				|
     *												|				->HasExistingPatientsResolutionErrors->ReadyForSave
     *												->ReadyForSave->Saved
     *												            |
     *												            ->Failed
     *															|
     *															->Canceled
     *															
     * When the user begins upload a new file the sate is ReadyForUpload
    */
    public enum UploadEhsMasterState
    {
        ReadyForUpload = 0,
        HasErrors = 1,
        ReadyForValidation = 2,
        ReadyForCheckExistingPatients = 4,
        HasExistingPatients = 8,
        HasExistingPatientsResolutionErrors = 16,
        ReadyForSave = 32,
        Saved = 64,
        Canceled = 128,
        Failed = 256
    }
    public enum UploadEhsFileResult
    {
        Unknown = -1,
        Success = 0,
        NullFile = 1,
        UsupportedExtension = 2,
        InvalidJSONFormat = 4,
        IncorrectDataFormat = 8,
        InvalidFileStructure = 16,
        ValidationError = 32,
        TooManyRecords = 64
    }

    public enum JsonStructureResult
    {
        Invalid = 1,
        EventsFile = 2,
        PatientFile = 3
    }
}
