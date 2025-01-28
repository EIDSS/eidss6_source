//using bv.common.Core;
using System.Data;
using System;
//using bv.common.db.Core;
//using bv.common.db;
using System.Windows.Forms;
using System.Collections;
//using bv.common.Objects;
using Microsoft.VisualBasic;
using System.Diagnostics;
using bv.winclient.Core;

//using bv.common.win;
//using bv.common.Diagnostics;
//using bv.common;
//using bv.common.win.Core;



namespace eidss.main.Autolock
{

    [global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class DisconnectedForm : BvForm
    {
        // Inherits Form

        //Form overrides dispose to clean up the component list.
        [System.Diagnostics.DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components != null)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        //Required by the Windows Form Designer
        private System.ComponentModel.Container components = null;

        //NOTE: The following procedure is required by the Windows Form Designer
        //It can be modified using the Windows Form Designer.
        //Do not modify it using the code editor.
        //[System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisconnectedForm));
            this.lLockMessage = new System.Windows.Forms.Label();
            this.sbLogout = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // lLockMessage
            // 
            resources.ApplyResources(this.lLockMessage, "lLockMessage");
            this.lLockMessage.Name = "lLockMessage";
            // 
            // sbLogout
            // 
            resources.ApplyResources(this.sbLogout, "sbLogout");
            this.sbLogout.Name = "sbLogout";
            this.sbLogout.Click += new System.EventHandler(this.sbLogout_Click);
            // 
            // DisconnectedForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sbLogout);
            this.Controls.Add(this.lLockMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpTopicId = "Logging_on";
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DisconnectedForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AutoLockForm_FormClosing);
            this.Load += new System.EventHandler(this.AutoLockForm_Load);
            this.ResumeLayout(false);

        }
        internal System.Windows.Forms.Label lLockMessage;
        internal DevExpress.XtraEditors.SimpleButton sbLogout;
    }
}

