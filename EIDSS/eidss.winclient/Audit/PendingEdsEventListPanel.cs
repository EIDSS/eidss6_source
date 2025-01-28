using System;
using System.Windows.Forms;
using bv.common.Core;
using bv.model.Model.Core;
using bv.winclient.BasePanel;
using bv.winclient.Core;
using eidss.model.Enums;
using eidss.model.Schema;
using eidss.winclient.Schema;
using System.Collections.Generic;
using bv.model.BLToolkit;
using eidss.model.Core;
using eidss.winclient.ElectronicDigitalSignature;
using eidss.model.Core.Security;
using System.Xml;
using System.Text;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Data.SqlTypes;
using System.Data;
using eidss.model.Resources;

namespace eidss.winclient.Security
{
    public partial class PendingEdsEventListPanel : BaseListPanel_PendingEdsEventListItem
    {
        public PendingEdsEventListPanel()
        {
            InitializeComponent();
        }

        public override string GetDetailFormName(IObject o)
        {
            return string.Empty;
        }

        public static void Register(Control parentControl)
        {
            if (BaseFormManager.ArchiveMode)
                return;
            if (eidss.model.Core.EidssSiteContext.Instance.IsKazakhstanMoHCustomization)
            {
                new MenuAction(ShowMe, MenuActionManager.Instance, MenuActionManager.Instance.Journals,
                               "MenuPendingEdsJournal", 1070, false, -1,//(int) MenuIconsSmall.DataAuditTransactions,
                               -1)
                {
                    //SelectPermission = PermissionHelper.SelectPermission(EIDSSPermissionObject.AccessToDataAudit),
                    ShowInMenu = true,
                    BeginGroup = true
                };
                //Toolbar menu
                new MenuAction(ShowMe, MenuActionManager.Instance, MenuActionManager.Instance.Security,
                                           "MenuPendingEdsJournal", 100035, true, -1,//(int) MenuIconsSmall.DataAuditTransactions,
                               -1)
                {
                    //SelectPermission = PermissionHelper.SelectPermission(EIDSSPermissionObject.AccessToDataAudit),
                    ShowInMenu = false,
                    BeginGroup = false,
                    Group = (int)MenuGroup.ToolbarHelp
                };
            }

        }

        private static void ShowMe()
        {
            BaseFormManager.ShowClient(typeof(PendingEdsEventListPanel), BaseFormManager.MainForm,
                                       PendingEdsEventListItem.CreateInstance());
        }

        public override void SetCustomActions(List<ActionMetaItem> actions)
        {
            base.SetCustomActions(actions);
            SetActionFunction(actions, "CleanUpPendingEds", CleanUpPendingEds);
            SetActionFunction(actions, "SignPendingEds", SignPendingEds);
        }

        private ActResult CleanUpPendingEds(DbManagerProxy manager, IObject bo, List<object> parameters)
        {
            manager.SetSpCommand(
                "dbo.spPendingEds_CleanUpByUser"
                , manager.Parameter("@idfUserID", (long)(manager.Context.CurrentUser.ID))
            ).ExecuteNonQuery();

            Refresh();
            return true;
        }

        private bool GetXmlDataToSign(DbManagerProxy manager, out string xmlDataToSign)
        {
            bool res = false;
            xmlDataToSign = string.Empty;
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
                res = true;
            }
            catch (Exception)
            {
                xmlDataToSign = string.Empty;
                res = false;
                throw;
            }
            return res;
        }

        private ActResult SignPendingEds(DbManagerProxy manager, IObject bo, List<object> parameters)
        {
            return false;
        }
    }
}