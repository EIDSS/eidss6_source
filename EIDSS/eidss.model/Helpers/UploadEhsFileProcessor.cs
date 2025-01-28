using eidss.model.Resources;
using eidss.model.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using eidss.model.Model.UploadEhs;
using System.Linq;
using Newtonsoft.Json;
using System.Collections;

namespace eidss.model.Helpers
{
    internal class UploadEhsFileProcessor : IDisposable
    {
        private UploadEhsMaster _uploadMaster;
        private string _eventFilePath;
        private string _patientFilePath;
        private Stream _eventFileStream;
        private Stream _patientFileStream;

        private List<string> _patientPropertyNamesErrors = new List<string>();
        private List<string> _eventPropertyNamesErrors = new List<string>();

        #region ctors
        public UploadEhsFileProcessor(string patientFilePath, string eventFilePath, UploadEhsMaster uploadMaster)
        {
            _uploadMaster = uploadMaster;
            _patientFilePath = patientFilePath;
            _eventFilePath = eventFilePath;
            Init();
        }
        public UploadEhsFileProcessor(Stream patientStream, string patientFileName, Stream eventStream, string eventFileName, UploadEhsMaster uploadMaster)
        {
            _uploadMaster = uploadMaster;
            _patientFilePath = patientFileName;
            _patientFileStream = patientStream;
            _eventFilePath = eventFileName;
            _eventFileStream = eventStream;
            Init();
        }
        public UploadEhsFileProcessor(UploadEhsMaster uploadMaster)
        {
            _uploadMaster = uploadMaster;

            Init();
        }


        public void Dispose()
        {
            _uploadMaster = null;
            if (_patientFileStream != null)
            {
                _patientFileStream.Dispose();
                _patientFileStream = null;
            }
            if (_eventFileStream != null)
            {
                _eventFileStream.Dispose();
                _eventFileStream = null;
            }
        }

        private void Init()
        {
            if (string.IsNullOrEmpty(_patientFilePath))
                _patientFilePath = _uploadMaster.PatientFileName;

            if (string.IsNullOrEmpty(_eventFilePath))
                _eventFilePath = _uploadMaster.EventFileName;
        }

        #endregion

        public string PatientPropertyNamesErrors
        {
            get
            {
                if (_patientPropertyNamesErrors.Count == 0)
                    return null;
                if (_patientPropertyNamesErrors.Count == 1)
                    return _patientPropertyNamesErrors[0];
                return string.Join(", ", _patientPropertyNamesErrors);
            }
        }

        public string EventPropertyNamesErrors
        {
            get
            {
                if (_eventPropertyNamesErrors.Count == 0)
                    return null;
                if (_eventPropertyNamesErrors.Count == 1)
                    return _eventPropertyNamesErrors[0];
                return string.Join(", ", _eventPropertyNamesErrors);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// Error code:
        /// 0 - no errors
        /// 1 - file(s) does(do) not exists
        /// 2 - usupported extension
        /// 3 - Ivalid JSON format
        /// 4 - incorrect data format in some cells
        /// 5 - invalid structure format, error duirng file reading (some column names don't match Ehs format column names)
        /// </returns>
        public UploadEhsFileResult GetPatientItems()
        {
            try
            {
                UploadEhsFileResult result = UploadEhsFileResult.Success;
                if (_patientFileStream == null)
                    return UploadEhsFileResult.InvalidJSONFormat;
                using (StreamReader r = new StreamReader(_patientFileStream))
                {
                    string json = r.ReadToEnd();

                    JToken actualJson = null;
                    var validationResult = json.IsValidJson<UploadEhsPatientJsonSchema[]>(ref actualJson);
                    if (!validationResult.IsValid)
                    {
                        if (validationResult.ErrorMessages == null || validationResult.ErrorMessages.Count == 0)
                            return UploadEhsFileResult.InvalidJSONFormat;

                        foreach (var error in validationResult.ErrorMessages.Distinct())
                        {
                            if (string.IsNullOrEmpty(error) || (!error.Contains(':')))
                                continue;
                            _patientPropertyNamesErrors.Add(error.Split(':')[1].Trim().TrimEnd('.'));
                        }

                        return UploadEhsFileResult.InvalidFileStructure;
                    }

                    if ((actualJson == null) || (!actualJson.HasValues))
                        return UploadEhsFileResult.InvalidFileStructure;

                    _uploadMaster.PatientJson = json;
                }

                return result;
            }
            catch (Exception ex)
            {
                bv.common.Core.LogError.Log("ErrorLog", ex);
                return UploadEhsFileResult.InvalidFileStructure;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// Error code:
        /// 0 - no errors
        /// 1 - file(s) does(do) not exists
        /// 2 - usupported extension
        /// 3 - Ivalid JSON format
        /// 4 - incorrect data format in some cells
        /// 5 - invalid structure format, error duirng file reading (some column names don't match Ehs format column names)
        /// </returns>
        public UploadEhsFileResult GetEventItems()
        {
            try
            {
                UploadEhsFileResult result = UploadEhsFileResult.Success;
                if (_eventFileStream == null)
                    return UploadEhsFileResult.InvalidJSONFormat;
                using (StreamReader r = new StreamReader(_eventFileStream))
                {
                    string json = r.ReadToEnd();

                    JToken actualJson = null;

                    var validationResult = json.IsValidJson<UploadEhsEventJsonSchema[]>(ref actualJson);
                    if (!validationResult.IsValid)
                    {
                        if (validationResult.ErrorMessages==null || validationResult.ErrorMessages.Count==0)
                            return UploadEhsFileResult.InvalidJSONFormat;

                        foreach (var error in validationResult.ErrorMessages.Distinct())
                        {
                            if (string.IsNullOrEmpty(error) || (!error.Contains(':')))
                                continue;
                            _eventPropertyNamesErrors.Add(error.Split(':')[1].Trim().TrimEnd('.'));
                        }

                        result = UploadEhsFileResult.InvalidFileStructure;
                    }

                    if ((actualJson == null) || (!actualJson.HasValues))
                        return UploadEhsFileResult.InvalidFileStructure;

                    if ((actualJson.Type != JTokenType.Object) && (actualJson.Type != JTokenType.Array))
                        return UploadEhsFileResult.InvalidFileStructure;

                    if (actualJson.Type == JTokenType.Object)
                    {
                        if (((actualJson["clinical"] == null) || (!actualJson["clinical"].HasValues))
                            && ((actualJson["laboratory"] == null) || (!actualJson["laboratory"].HasValues))
                            && (!_eventPropertyNamesErrors.Contains("clinical/laboratory")))
                        {
                            _eventPropertyNamesErrors.Add("clinical/laboratory");
                            return UploadEhsFileResult.InvalidFileStructure;
                        }
                    }
                    else
                    {
                        foreach (var eventItem in actualJson)
                        {
                            if ((eventItem == null) || (!eventItem.HasValues))
                            {
                                return UploadEhsFileResult.InvalidFileStructure;
                            }
                            if (((eventItem["clinical"] == null) || (!eventItem["clinical"].HasValues))
                                && ((eventItem["laboratory"] == null) || (!eventItem["laboratory"].HasValues))
                                && (!_eventPropertyNamesErrors.Contains("clinical/laboratory")))
                            {
                                _eventPropertyNamesErrors.Add("clinical/laboratory");
                                return UploadEhsFileResult.InvalidFileStructure;
                            }
                        }
                    }

                    _uploadMaster.EventJson = json;
                }


                return result;
            }
            catch (Exception ex)
            {
                bv.common.Core.LogError.Log("ErrorLog", ex);
                return UploadEhsFileResult.InvalidFileStructure;
            }
        }
    }
}
