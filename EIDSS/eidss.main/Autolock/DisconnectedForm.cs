using System;
using System.Collections;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.Utils.Win;
using DevExpress.XtraBars.Forms;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.Helpers;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Popup;
using bv.common.Core;
using bv.common.Resources;
using bv.winclient.BasePanel;
using bv.winclient.Core;
using bv.winclient.Layout;
using bv.winclient.Localization;
using eidss.gis.Tools.ToolForms;
using eidss.model.Core;
using eidss.model.Core.Security;
using eidss.model.Resources;
using System.Globalization;
using System.Threading;

namespace eidss.main.Autolock
{
    public partial class DisconnectedForm
    {
        //private bool m_AllowToClose;

        public void AutoLockForm_Load(Object sender, EventArgs e)
        {
            PlaceCenterWindow();
            ShowWindows(false);
            //SystemLanguages.SwitchInputLanguage(m_LastInputLang);
        }
        private static string m_LastInputLang = "en";

        public void sbLogout_Click(Object sender, EventArgs e)
        {
            //CloseAllWindows();
            DialogResult = DialogResult.Cancel;
            //m_AllowToClose = true;
            Close();
        }

        public void ShowWindows(bool bShow)
        {
            var formList = new ArrayList(Application.OpenForms);

            foreach (Form frm in formList)
            {
                if (frm is PopupBaseForm
                    || frm is GalleryItemImagePopupForm
                    || frm is SuperToolTipWindow
                    || frm is ToolTipControllerWindow
                    || frm is AppMenuForm
                    || frm is SubMenuControlForm
                    || frm is KeyTipForm
                    // commented because of bugN 5600 
                    //  || frm is DevExpress.XtraPivotGrid.Customization.CustomizationForm
                    || frm is InfoForm)
                {
                    if (frm.Visible)
                    {
                        frm.Hide();
                    }
                    continue;
                }

                if (frm != this)
                {
                    var result = frm.BeginInvoke((MethodInvoker)delegate() { if (frm != null) frm.Visible = bShow; });
                    frm.EndInvoke(result);
                }
            }
        }

        public void PlaceCenterWindow()
        {
            Left = (Screen.PrimaryScreen.Bounds.Width - Width) / 2;
            Top = (Screen.PrimaryScreen.Bounds.Height - Height) / 2;
        }

        //public void CloseAllWindows()
        //{
        //    //   Close all Detail forms first

        //    for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
        //    {
        //        Form frm = Application.OpenForms[i];
        //        if (frm == null)
        //        {
        //            continue;
        //        }

        //        if (frm.Controls.Count == 0)
        //        {
        //            continue;
        //        }

        //        IApplicationForm ctrl = BaseFormManager.FindChildIApplicationForm(frm);

        //        if (ctrl != null)
        //        {
        //            BaseFormManager.Close(ctrl);
        //        }
        //    }
        //    Application.DoEvents();
        //    BaseFormManager.CloseAll(false);
        //    Application.DoEvents();
        //}

        public void AutoLockForm_FormClosing(Object sender, FormClosingEventArgs e)
        {
            //e.Cancel = Convert.ToBoolean(!m_AllowToClose);
        }

        public DisconnectedForm()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();

            Icon = BaseFormManager.MainForm.Icon;
            LayoutCorrector.ApplySystemFont(this);
            //lLockMessage.Text = Properties.Resources.ResourceManager.GetString("lLockMessage.Text");
            //Text = Properties.Resources.ResourceManager.GetString("$this.Text");
            //lLockMessage.Text = BvMessages.Get("msgSessionWasClosed");
            //Text = BvMessages.Get("LoginConfirmation");
        }
        
        public DisconnectedForm(string lang)
        {
            // This call is required by the Windows Form Designer.
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(lang);
            InitializeComponent();

            Icon = BaseFormManager.MainForm.Icon;
            LayoutCorrector.ApplySystemFont(this);
            //lLockMessage.Text = Properties.Resources.ResourceManager.GetString("lLockMessage.Text",CultureInfo.GetCultureInfo(lang));
            //Text = Properties.Resources.ResourceManager.GetString("$this.Text", CultureInfo.GetCultureInfo(lang));
        }
    }
}