using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using bv.common.Core;
using bv.common.Resources;
using bv.winclient.BasePanel;
using bv.winclient.Core;
using bv.winclient.Layout;
using bv.winclient.Localization;
using eidss.model.Core;
using eidss.model.Enums;
using eidss.model.Schema;

namespace eidss.main.Login
{
    public partial class DisconnectParallelSessionsForm : BvForm
    {
        public DisconnectParallelSessionsForm()
        {
            InitializeComponent();


            HelpTopicId = "disconnect_parallel_sessions";

            btnOk.Text = BvMessages.Get("strOK_Id");
            btnCancel.Text = BvMessages.Get("strCancel_Id");
           
            LayoutCorrector.ApplySystemFont(this);
        }

        public void btnOk_Click(Object sender, EventArgs e)
        {
            try
            {

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                if (SqlExceptionHandler.Handle(ex))
                    return;
                string errMessage;
                if (ex is SqlException)
                    errMessage = SecurityMessages.GetDBErrorMessage((ex as SqlException).Number, null, null);
                else
                    errMessage = SecurityMessages.GetDBErrorMessage(0, null, null);
                ErrorForm.ShowErrorDirect(errMessage, ex);
            }
        }

        //public static void Register(Control parentControl)
        //{
        //    if (BaseFormManager.ArchiveMode)
        //        return;
        //    var manager = MenuActionManager.Instance;
        //    new MenuAction(ShowMe, manager, manager.Security, "MenuChangePassword", 1000, false, (int)MenuIconsSmall.ChangePassword, -1) { Name = "btnChangePassword" };
        //}

        public static void ShowMe()
        {
            var form = new DisconnectParallelSessionsForm();
            BaseFormManager.ShowModal(form, null);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
