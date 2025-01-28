namespace EIDSS.Reports.Parameterized.Human.KZ.Keeper
{
    partial class ComparativeReportKeeper
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComparativeReportKeeper));
            this.Year1SpinEdit = new DevExpress.XtraEditors.SpinEdit();
            this.Year2SpinEdit = new DevExpress.XtraEditors.SpinEdit();
            this.StartMonthLookUp = new DevExpress.XtraEditors.LookUpEdit();
            this.EndMonthLookUp = new DevExpress.XtraEditors.LookUpEdit();
            this.StartYearLabel = new System.Windows.Forms.Label();
            this.EndYearLabel = new System.Windows.Forms.Label();
            this.StartMonthLabel = new System.Windows.Forms.Label();
            this.EndMonthLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceUseArchiveData.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Year1SpinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Year2SpinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartMonthLookUp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EndMonthLookUp.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSettings
            // 
            this.pnlSettings.Controls.Add(this.panel1);
            resources.ApplyResources(this.pnlSettings, "pnlSettings");
            this.pnlSettings.Controls.SetChildIndex(this.panel1, 0);
            this.pnlSettings.Controls.SetChildIndex(this.ceUseArchiveData, 0);
            this.pnlSettings.Controls.SetChildIndex(this.GenerateReportButton, 0);
            // 
            // ceUseArchiveData
            // 
            resources.ApplyResources(this.ceUseArchiveData, "ceUseArchiveData");
            this.ceUseArchiveData.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("ceUseArchiveData.Properties.Appearance.Font")));
            this.ceUseArchiveData.Properties.Appearance.Options.UseFont = true;
            this.ceUseArchiveData.Properties.AppearanceDisabled.Options.UseFont = true;
            this.ceUseArchiveData.Properties.AppearanceFocused.Options.UseFont = true;
            this.ceUseArchiveData.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.ceUseArchiveData.Properties.Caption = resources.GetString("ceUseArchiveData.Properties.Caption");
            // 
            // GenerateReportButton
            // 
            this.GenerateReportButton.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("GenerateReportButton.Appearance.Font")));
            this.GenerateReportButton.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.GenerateReportButton, "GenerateReportButton");
            bv.common.Resources.BvResourceManagerChanger.GetResourceManager(typeof(ComparativeReportKeeper), out resources);
            // Form Is Localizable: True
            // 
            // Year1SpinEdit
            // 
            resources.ApplyResources(this.Year1SpinEdit, "Year1SpinEdit");
            this.Year1SpinEdit.Name = "Year1SpinEdit";
            this.Year1SpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.Year1SpinEdit.Properties.Mask.EditMask = resources.GetString("Year1SpinEdit.Properties.Mask.EditMask");
            this.Year1SpinEdit.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("Year1SpinEdit.Properties.Mask.MaskType")));
            this.Year1SpinEdit.Properties.MaxValue = new decimal(new int[] {
            2030,
            0,
            0,
            0});
            this.Year1SpinEdit.Properties.MinValue = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.Year1SpinEdit.EditValueChanged += new System.EventHandler(this.Year1SpinEdit_EditValueChanged);
            // 
            // Year2SpinEdit
            // 
            resources.ApplyResources(this.Year2SpinEdit, "Year2SpinEdit");
            this.Year2SpinEdit.Name = "Year2SpinEdit";
            this.Year2SpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.Year2SpinEdit.Properties.Mask.EditMask = resources.GetString("Year2SpinEdit.Properties.Mask.EditMask");
            this.Year2SpinEdit.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("Year2SpinEdit.Properties.Mask.MaskType")));
            this.Year2SpinEdit.Properties.MaxValue = new decimal(new int[] {
            2030,
            0,
            0,
            0});
            this.Year2SpinEdit.Properties.MinValue = new decimal(new int[] {
            2001,
            0,
            0,
            0});
            this.Year2SpinEdit.EditValueChanged += new System.EventHandler(this.Year2SpinEdit_EditValueChanged);
            // 
            // StartMonthLookUp
            // 
            resources.ApplyResources(this.StartMonthLookUp, "StartMonthLookUp");
            this.StartMonthLookUp.Name = "StartMonthLookUp";
            this.StartMonthLookUp.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("StartMonthLookUp.Properties.Buttons")))),
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("StartMonthLookUp.Properties.Buttons1"))), resources.GetString("StartMonthLookUp.Properties.Buttons2"), ((int)(resources.GetObject("StartMonthLookUp.Properties.Buttons3"))), ((bool)(resources.GetObject("StartMonthLookUp.Properties.Buttons4"))), ((bool)(resources.GetObject("StartMonthLookUp.Properties.Buttons5"))), ((bool)(resources.GetObject("StartMonthLookUp.Properties.Buttons6"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("StartMonthLookUp.Properties.Buttons7"))), ((System.Drawing.Image)(resources.GetObject("StartMonthLookUp.Properties.Buttons8"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("StartMonthLookUp.Properties.Buttons9"), ((object)(resources.GetObject("StartMonthLookUp.Properties.Buttons10"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("StartMonthLookUp.Properties.Buttons11"))), ((bool)(resources.GetObject("StartMonthLookUp.Properties.Buttons12"))))});
            this.StartMonthLookUp.Properties.DropDownRows = 12;
            this.StartMonthLookUp.Properties.NullText = resources.GetString("StartMonthLookUp.Properties.NullText");
            this.StartMonthLookUp.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.MonthLookUp_ButtonClick);
            this.StartMonthLookUp.EditValueChanged += new System.EventHandler(this.StartMonthLookUp_EditValueChanged);
            // 
            // EndMonthLookUp
            // 
            resources.ApplyResources(this.EndMonthLookUp, "EndMonthLookUp");
            this.EndMonthLookUp.Name = "EndMonthLookUp";
            this.EndMonthLookUp.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("EndMonthLookUp.Properties.Buttons")))),
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("EndMonthLookUp.Properties.Buttons1"))), resources.GetString("EndMonthLookUp.Properties.Buttons2"), ((int)(resources.GetObject("EndMonthLookUp.Properties.Buttons3"))), ((bool)(resources.GetObject("EndMonthLookUp.Properties.Buttons4"))), ((bool)(resources.GetObject("EndMonthLookUp.Properties.Buttons5"))), ((bool)(resources.GetObject("EndMonthLookUp.Properties.Buttons6"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("EndMonthLookUp.Properties.Buttons7"))), ((System.Drawing.Image)(resources.GetObject("EndMonthLookUp.Properties.Buttons8"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, resources.GetString("EndMonthLookUp.Properties.Buttons9"), ((object)(resources.GetObject("EndMonthLookUp.Properties.Buttons10"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("EndMonthLookUp.Properties.Buttons11"))), ((bool)(resources.GetObject("EndMonthLookUp.Properties.Buttons12"))))});
            this.EndMonthLookUp.Properties.DropDownRows = 12;
            this.EndMonthLookUp.Properties.NullText = resources.GetString("EndMonthLookUp.Properties.NullText");
            this.EndMonthLookUp.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.MonthLookUp_ButtonClick);
            this.EndMonthLookUp.EditValueChanged += new System.EventHandler(this.EndMonthLookUp_EditValueChanged);
            // 
            // StartYearLabel
            // 
            resources.ApplyResources(this.StartYearLabel, "StartYearLabel");
            this.StartYearLabel.ForeColor = System.Drawing.Color.Black;
            this.StartYearLabel.Name = "StartYearLabel";
            // 
            // EndYearLabel
            // 
            resources.ApplyResources(this.EndYearLabel, "EndYearLabel");
            this.EndYearLabel.ForeColor = System.Drawing.Color.Black;
            this.EndYearLabel.Name = "EndYearLabel";
            // 
            // StartMonthLabel
            // 
            resources.ApplyResources(this.StartMonthLabel, "StartMonthLabel");
            this.StartMonthLabel.ForeColor = System.Drawing.Color.Black;
            this.StartMonthLabel.Name = "StartMonthLabel";
            // 
            // EndMonthLabel
            // 
            resources.ApplyResources(this.EndMonthLabel, "EndMonthLabel");
            this.EndMonthLabel.ForeColor = System.Drawing.Color.Black;
            this.EndMonthLabel.Name = "EndMonthLabel";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.StartYearLabel);
            this.panel1.Controls.Add(this.Year1SpinEdit);
            this.panel1.Controls.Add(this.Year2SpinEdit);
            this.panel1.Controls.Add(this.EndMonthLabel);
            this.panel1.Controls.Add(this.StartMonthLookUp);
            this.panel1.Controls.Add(this.StartMonthLabel);
            this.panel1.Controls.Add(this.EndMonthLookUp);
            this.panel1.Controls.Add(this.EndYearLabel);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // ComparativeReportKeeper
            // 
            this.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("ComparativeReportKeeper.Appearance.Font")));
            this.Appearance.Options.UseFont = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.HeaderHeight = 160;
            this.Name = "ComparativeReportKeeper";
            this.pnlSettings.ResumeLayout(false);
            this.pnlSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceUseArchiveData.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Year1SpinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Year2SpinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartMonthLookUp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EndMonthLookUp.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SpinEdit Year1SpinEdit;
        private DevExpress.XtraEditors.SpinEdit Year2SpinEdit;
        protected DevExpress.XtraEditors.LookUpEdit StartMonthLookUp;
        protected DevExpress.XtraEditors.LookUpEdit EndMonthLookUp;
        private System.Windows.Forms.Label StartYearLabel;
        private System.Windows.Forms.Label EndYearLabel;
        protected System.Windows.Forms.Label StartMonthLabel;
        protected System.Windows.Forms.Label EndMonthLabel;
        private System.Windows.Forms.Panel panel1;
    }
}