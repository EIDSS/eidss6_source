using bv.model.Model.Core;
using eidss.model.Schema;
using eidss.web.common.Controllers;
using eidss.web.common.Utils;
using eidss.webclient.Utils;
using System.IO;
using System.Web;
using System.Web.Mvc;
using bv.common.Configuration;
using BLToolkit.EditableObjects;
using System.Collections.Generic;
using System.Linq;
using eidss.model.Core;
using bv.model.BLToolkit;
using System;
using bv.common.Core;

namespace eidss.webclient.Controllers
{
    [AuthorizeEIDSS]
    public class UploadEhsController : BvController
    {
        [HttpGet]
        public ActionResult Details(long? id)
        {
            return DetailsInternal(id, UploadEhsMaster.Accessor.Instance(null), null, null,
                (m, a) => ObjectStorage.Using<UploadEhsMaster, UploadEhsMaster>(o =>
                {
                    HandleMasterState(o);

                    return o;
                }, ModelUserContext.ClientID, id.Value, null),
                null,
                null
                );
        }

        //[HttpPost]
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult PostFile(long id, HttpPostedFileBase patientFileUpload, HttpPostedFileBase eventFileUpload)
        {
            var isOk = ModelState.IsValid;

            return ObjectStorage.Using<UploadEhsMaster, RedirectToRouteResult>(o =>
            {
                o.Clear();
                o.EnterUploadingSession();
                if (patientFileUpload == null)
                    o.SetPatientError(UploadEhsFileResult.NullFile, Translator.GetMessageString("msgUploadEhsFileNullFile"));
                if (eventFileUpload == null)
                    o.SetEventError(UploadEhsFileResult.NullFile, Translator.GetMessageString("msgUploadEhsFileNullFile"));
                if ((patientFileUpload != null) && (patientFileUpload.ContentLength == 0))
                    o.SetPatientError(UploadEhsFileResult.InvalidJSONFormat, Translator.GetMessageString("msgUploadEhsInvalidJSONFormat"));
                if ((eventFileUpload != null) && (eventFileUpload.ContentLength == 0))
                    o.SetEventError(UploadEhsFileResult.InvalidJSONFormat, Translator.GetMessageString("msgUploadEhsInvalidJSONFormat"));
                if ((patientFileUpload != null) && (patientFileUpload.ContentLength > BaseSettings.UploadEhsMaxMBSize * 1024 * 1024))
                    o.SetPatientError(UploadEhsFileResult.TooManyRecords, Translator.GetMessageString("msgUploadFileSizeExceeded"));
                if ((eventFileUpload != null) && (eventFileUpload.ContentLength > BaseSettings.UploadEhsMaxMBSize * 1024 * 1024))
                    o.SetEventError(UploadEhsFileResult.TooManyRecords, Translator.GetMessageString("msgUploadFileSizeExceeded"));
                if (patientFileUpload != null && eventFileUpload != null && o.GetPatientState() != UploadEhsMasterState.HasErrors && o.GetEventState() != UploadEhsMasterState.HasErrors)
                {
                    o.GetItems(patientFileUpload.FileName, eventFileUpload.FileName, patientFileUpload.InputStream, eventFileUpload.InputStream);

                    if (o.GetPatientState() == UploadEhsMasterState.ReadyForValidation && o.GetEventState() == UploadEhsMasterState.ReadyForValidation)
                    {
                        using (var wrapper = new EhsClientWrapper())
                        {
                            var result = wrapper.Client.ValidateData(o.PatientJson, o.EventJson, o.idfsUploadEhs, ModelUserContext.CurrentLanguage);
                            o.ApplyValidationResults(result);
                        }
                    }

                }

                var state = o.GetState();
                if ((state & UploadEhsMasterState.HasErrors) > 0)
                    o.LeaveUploadingSession();


                return RedirectToAction("Details", "UploadEhs", new { id = id });
            }, ModelUserContext.ClientID, id, null);

        }

        [HttpGet]
        public ActionResult IsUploadFileEnabled(long id)
        {
            return ObjectStorage.Using<UploadEhsMaster, JsonResult>(o =>
            {
                return Json(new { Enabled = o.IsUploadingSessionAvailable(), Message = Translator.GetMessageString("msgUploadEhsSystemIsBusy") }, JsonRequestBehavior.AllowGet);
            }, ModelUserContext.ClientID, id, null);
        }

        private UploadEhsExistingPatientItem GetPreviousExistingPatientItem(UploadEhsMaster master, UploadEhsExistingPatientItem currentItem)
        {
            var currentIndex = 0;
            var duplicates = GetExistingPatientItemsForMaster(master);

            if (currentItem != null)
                currentIndex = duplicates.IndexOf(currentItem);

            return currentIndex - 1 >= 0 ? duplicates[currentIndex - 1] : duplicates[duplicates.Count - 1];
        }

        private UploadEhsExistingPatientItem GetNextExistingPatientItem(UploadEhsMaster master, UploadEhsExistingPatientItem currentItem)
        {
            var currentIndex = 0;
            var duplicates = GetExistingPatientItemsForMaster(master);

            if (currentItem != null)
                currentIndex = duplicates.IndexOf(currentItem);

            return currentIndex + 1 < duplicates.Count ? duplicates[currentIndex + 1] : duplicates[0];
        }

        private UploadEhsExistingPatientItem SetResolutionGetNextExistingPatientItem(UploadEhsMaster master, UploadEhsExistingPatientItem currentItem, UploadEhsPatientResolution resolution)
        {
            master.SetResolutionForExistingPatient((long)currentItem.idfHumanActual, resolution);

            return GetNextExistingPatientItem(master, currentItem);
        }

        private UploadEhsExistingPatientItem HandleAction(UploadEhsMaster master, UploadEhsExistingPatientItem currentItem, string action)
        {
            if (string.IsNullOrWhiteSpace(action))
                return currentItem;

            switch (action.ToLower())
            {
                case "next":
                    return GetNextExistingPatientItem(master, currentItem);
                case "previous":
                    return GetPreviousExistingPatientItem(master, currentItem);
                case "createasnew":
                    return SetResolutionGetNextExistingPatientItem(master, currentItem, UploadEhsPatientResolution.Created);
                case "dismiss":
                    return SetResolutionGetNextExistingPatientItem(master, currentItem, UploadEhsPatientResolution.Dismissed);
                case "update":
                    return SetResolutionGetNextExistingPatientItem(master, currentItem, UploadEhsPatientResolution.Updated);
                default:
                    return currentItem;
            }
        }

        [HttpGet]
        public ActionResult UploadEhsUpdatePicker(long root, long id, string duplicateAction)
        {
            return PickerInternal<UploadEhsExistingPatientItem.Accessor, UploadEhsExistingPatientItem, UploadEhsMaster>(0, 1, "", UploadEhsExistingPatientItem.Accessor.Instance(null), null,
                (m, a, p) =>
                    ObjectStorage.Using<UploadEhsMaster, UploadEhsExistingPatientItem>(o =>
                    {
                        var duplicates = GetExistingPatientItemsForMaster(o);
                        var updateItem = duplicates.FirstOrDefault(i => i.idfHumanActual == id);

                        var updateItemsCount = duplicates.Count;

                        var currentIndex = 0;

                        updateItem = HandleAction(o, updateItem, duplicateAction);

                        if (updateItem != null)
                            currentIndex = duplicates.IndexOf(updateItem) + 1;

                        ViewBag.UpdateItemsCount = updateItemsCount;
                        ViewBag.CurrentUpdateItemIndex = currentIndex;

                        return updateItem;
                    },
                        ModelUserContext.ClientID, root, null),
                null,
                null,
                false);
        }

        [HttpGet]
        public ActionResult FinalizeResolveExistingPatientItems(long root)
        {
            return ObjectStorage.Using<UploadEhsMaster, RedirectToRouteResult>(o =>
            {
                o.FinalizeResolveExistingPatientItems();

                return RedirectToAction("Details", "UploadEhs", new { id = root });
            }, ModelUserContext.ClientID, root, null);
        }

        [HttpGet]
        public ActionResult SaveUploadedData(long id)
        {
            return ObjectStorage.Using<UploadEhsMaster, RedirectToRouteResult>(o =>
            {
                if (o.GetState() == UploadEhsMasterState.ReadyForSave)
                {
                    try
                    {
                        o.SaveResolutionForExistingPatientItems();
                        using (var wrapper = new EhsClientWrapper())
                        {
                            var employeeId = (long?)ModelUserContext.Instance.CurrentUser.EmployeeID;
                            var userId = (long?)ModelUserContext.Instance.CurrentUser.ID;
                            var result = wrapper.Client.SaveUploadedData(o.idfsUploadEhs, employeeId ?? 0, userId ?? 0, ModelUserContext.CurrentLanguage);

                            o.ReflectSaveResult(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogError.Log("ErrorLog", ex);
                    }
                    finally
                    {
                        o.LeaveUploadingSession();
                    }
                }

                return RedirectToAction("Details", "UploadEhs", new { id = id });
            }, ModelUserContext.ClientID, id, null);
        }

        [HttpGet]
        public ActionResult CancelUploadedData(long id)
        {
            return ObjectStorage.Using<UploadEhsMaster, RedirectToRouteResult>(o =>
            {
                o.Cancel();
                o.LeaveUploadingSession();

                return RedirectToAction("Details", "UploadEhs", new { id = id });
            }, ModelUserContext.ClientID, id, null);
        }

        private string GetContentType(string fileName)
        {
            var ext = Path.GetExtension(fileName);

            switch (ext)
            {
                case "json":
                    return "application/json";
                default:
                    return "application/octet-stream";
            }
        }

        [HttpGet]
        public FileResult GetPatientErrorsFile(long id)
        {
            return ObjectStorage.Using<UploadEhsMaster, FileResult>(o =>
            {
                var fileName = o.GetErrorFileName(o.PatientFileName, o.ErrorPatientFileName, JsonStructureResult.PatientFile);
                var contentType = GetContentType(fileName);

                var byteArray = o.PatientErrorFileContent ?? new byte[] { };

                return File(byteArray, contentType, fileName);
            }, ModelUserContext.ClientID, id, null);
        }

        public FileResult GetEventErrorsFile(long id)
        {
            return ObjectStorage.Using<UploadEhsMaster, FileResult>(o =>
            {
                var fileName = o.GetErrorFileName(o.EventFileName, o.ErrorEventFileName, JsonStructureResult.EventsFile);
                var contentType = GetContentType(fileName);

                var byteArray = o.EventErrorFileContent ?? new byte[] { };

                return File(byteArray, contentType, fileName);
            }, ModelUserContext.ClientID, id, null);
        }

        [HttpGet]
        public FileResult GetResultFile(long id)
        {
            return ObjectStorage.Using<UploadEhsMaster, FileResult>(o =>
            {
                var fileName = o.GetResultEventFileName();
                var contentType = GetContentType(fileName);

                var byteArray = o.EventResultFileContent ?? new byte[] { };

                return File(byteArray, contentType, fileName);

            }, ModelUserContext.ClientID, id, null);
        }

        private void HandleMasterReadyForSave(UploadEhsMaster master)
        {
            if (master.GetState() != UploadEhsMasterState.ReadyForSave)
                return;

            master.EnterUploadingSession();
        }

        private void HandleValidatedMaster(UploadEhsMaster master)
        {
            var state = master.GetState();
            if ((state & UploadEhsMasterState.ReadyForCheckExistingPatients) == 0)
                return;

            master.EnterUploadingSession();

            bool validationResult = master.CheckForExistingPatientItems();

            if (!validationResult)
                return;

            HandleMasterReadyForSave(master);
        }


        private void HandleMasterState(UploadEhsMaster master)
        {
            if (master == null)
                return;

            var state = master.GetState();

            if (((state & UploadEhsMasterState.ReadyForUpload) > 0) ||
                ((state & UploadEhsMasterState.ReadyForValidation) > 0) ||
                ((state & UploadEhsMasterState.HasErrors) > 0))
                return;
            
            if ((state & UploadEhsMasterState.ReadyForCheckExistingPatients) > 0)
                HandleValidatedMaster(master);
            else if (state == UploadEhsMasterState.ReadyForSave)
                HandleMasterReadyForSave(master);
        }

        [HttpGet]
        public ActionResult DismissAllExistingPatientItems(long root)
        {
            return ObjectStorage.Using<UploadEhsMaster, RedirectToRouteResult>(o =>
            {
                o.DismissAllExistingPatientItems();

                return RedirectToAction("Details", "UploadEhs", new { id = root });
            }, ModelUserContext.ClientID, root, null);
        }

        public static IList<UploadEhsExistingPatientItem> GetExistingPatientItemsForMaster(UploadEhsMaster master)
        {
            if (master.GetPatientState() != UploadEhsMasterState.HasExistingPatients && master.GetPatientState() != UploadEhsMasterState.HasExistingPatientsResolutionErrors)
                return new List<UploadEhsExistingPatientItem>();

            if (master.GetPatientState() == UploadEhsMasterState.HasExistingPatients)
                return master.ExistingPatientItems;

            return master.ExistingPatientItems.Where(d => !d.Resolved).ToList();
        }
    }
}
