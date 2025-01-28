using bv.common.Diagnostics;
using bv.model.BLToolkit;
using bv.model.Model.Core;
using eidss.model.Core;
using eidss.model.Enums;
using eidss.model.Schema;
using eidss.web.common.Controllers;
using eidss.web.common.Utils;
using eidss.webclient.Models;
using eidss.webclient.Utils;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml;

namespace eidss.webclient.Controllers
{
    [AuthorizeEIDSS]
    public class PendingJournalController : BvController
    {
        private ValidationEventArgs m_Validation;
        private EidssUserContext m_EidssUserContext = (EidssUserContext)EidssUserContext.Instance;

        #region PendingJournal
        public ActionResult Index()
        {
            return IndexInternal<PendingEdsEventListItem.Accessor, PendingEdsEventListItem, PendingEdsEventListItem.PendingEdsEventListItemGridModelList>
                (PendingEdsEventListItem.Accessor.Instance(null), AutoGridRoots.PendingEdsEventList, false);
        }

        [CompressFilter]
        public ActionResult IndexAjax([DataSourceRequest] DataSourceRequest request, FormCollection form)
        {
            return IndexInternalAjax<PendingEdsEventListItem.Accessor, PendingEdsEventListItem, PendingEdsEventListItem.PendingEdsEventListItemGridModelList>
                (form, PendingEdsEventListItem.Accessor.Instance(null), AutoGridRoots.PendingEdsEventList, request);
        }

        public ActionResult InfoForSignPendingEds()
        {
            var edsTicket = string.Empty;
            int edsNumberOfAttempts = 3;
            if (EidssUserContext.User != null)
            {
                edsTicket = GetXmlDataToSign();
                edsNumberOfAttempts = EidssSiteContext.Instance.EdsAttemptCount;
            }

            return Json(new
            {
                ticket = edsTicket,
                numberOfAttempts = edsNumberOfAttempts
            });

        }

        [HttpPost]
        public ActionResult CleanUpPendingEds()
        {
            SaveCleanUpPendingChanges();
            string returnUrl = "/en-US/PendingJournal/Index";
            string adj = returnUrl.Replace("/", "");
            if (!Cultures.StringIsCulture(adj))
            {
                returnUrl = returnUrl.Substring(returnUrl.Substring(1).IndexOf("/") + 1);
            }
            else
            {
                returnUrl = returnUrl.Replace(adj, "");
            }
            return Json(new { returnUrl = String.Format("/{0}{1}", GetSelectedLanguage(), returnUrl) });
        }

        [HttpPost]
        public ActionResult SignPendingEds(Eds eds)
        {
            string returnUrl = "/en-US/PendingJournal/Index";
            string adj = returnUrl.Replace("/", "");
            if (!Cultures.StringIsCulture(adj))
            {
                returnUrl = returnUrl.Substring(returnUrl.Substring(1).IndexOf("/") + 1);
            }
            else
            {
                returnUrl = returnUrl.Replace(adj, "");
            }

            return Json(new { returnUrl = String.Format("/{0}{1}", GetSelectedLanguage(), returnUrl) });
        }


        public ActionResult ReflectCancelEdsEventToSecurityLog()
        {
            EdsCommonHelper.ReflectResultToSecurityLog(SecurityAuditEvent.SignEds, false, EdsResultCode.ActionCanceled, string.Empty);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReflectExceededAttemptsEdsEventToSecurityLog()
        {
            EdsCommonHelper.ReflectResultToSecurityLog(SecurityAuditEvent.SignEds, false, EdsResultCode.NumberOfAttemptsExceeded, string.Empty);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReflectNCALayerErrorEdsEventToSecurityLog()
        {
            EdsCommonHelper.ReflectResultToSecurityLog(SecurityAuditEvent.SignEds, false, EdsResultCode.NCALayerError, string.Empty);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region private methods

        private string GetSelectedLanguage()
        {
            var culture = Url.RequestContext.RouteData.Values.ContainsKey("culture") ? Url.RequestContext.RouteData.Values["culture"] : "en-US";
            return (culture == null) ? "en-US" : Url.RequestContext.RouteData.Values["culture"].ToString();
        }

        private void SaveCleanUpPendingChanges()
        {
            using (DbManagerProxy manager = DbManagerFactory.Factory.Create(ModelUserContext.Instance))
            {
                try
                {
                    manager.SetSpCommand(
                        "dbo.spPendingEds_CleanUpByUser"
                        , manager.Parameter("@idfUserID", (long)(EidssUserContext.User.ID))
                    ).ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Dbg.Debug("Pending EDS Clean Up Error: {0}", e.ToString());
                    throw;
                }
            }
        }

        private string GetXmlDataToSign()
        {
            var xmlDataToSign = string.Empty;
            using (DbManagerProxy manager = DbManagerFactory.Factory.Create(ModelUserContext.Instance))
            {
                try
                {
                    DataTable dt = manager.SetSpCommand("dbo.spPendingEds_SelectXmlToSign"
                    , manager.Parameter("@idfUserID", (long)(manager.Context.CurrentUser.ID))
                        ).ExecuteDataTable();
                    if ((dt != null) && (dt.Columns.Count > 0) && (dt.Rows.Count > 0))
                    {
                        xmlDataToSign = dt.Rows[0][0].ToString();
                    }
                    if (string.IsNullOrEmpty(xmlDataToSign))
                    {
                        //Assign xml with empty list of events
                        xmlDataToSign = string.Format("<?xml version=\"1.0\" encoding\"utf-8\" ?>\r\n<config xmlns=\"urn:config-schema\">\r\n<signdata>\r\n<eventlist/>\r\n<signdate>{0}</signdate>\r\n<signuser>{1}</signuser>\r\n</signdata>\r\n</config>\r\n",
                            DateTime.Now.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss"), manager.Context.CurrentUser.ID.ToString());
                    }
                }
                catch (Exception)
                {
                    xmlDataToSign = string.Empty;
                    throw;
                }
            }
            return xmlDataToSign;
        }

        #endregion
    }
}