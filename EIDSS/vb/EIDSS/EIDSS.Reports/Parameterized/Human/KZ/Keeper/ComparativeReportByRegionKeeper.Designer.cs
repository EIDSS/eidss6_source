using System;
namespace EIDSS.Reports.Parameterized.Human.KZ.Keeper
{
    partial class ComparativeReportByRegionKeeper
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
            this.region1Filter = new EIDSS.Reports.BaseControls.Filters.RegionFilter();
            this.rayon1Filter = new EIDSS.Reports.BaseControls.Filters.RayonFilter();
            this.Year1SpinEdit = new DevExpress.XtraEditors.SpinEdit();
            this.Year2SpinEdit = new DevExpress.XtraEditors.SpinEdit();
            this.StartMonthLookUp = new DevExpress.XtraEditors.LookUpEdit();
            this.EndMonthLookUp = new DevExpress.XtraEditors.LookUpEdit();
            this.StartYearLabel = new System.Windows.Forms.Label();
            this.EndYearLabel = new System.Windows.Forms.Label();
            this.StartMonthLabel = new System.Windows.Forms.Label();
            this.EndMonthLabel = new System.Windows.Forms.Label();
            this.populationFilter = new DevExpress.XtraEditors.LookUpEdit();
            this.populationLabel = new System.Windows.Forms.Label();
            this.DiagnosisFilter = new DevExpress.XtraEditors.TreeListLookUpEdit();
            this.treeListLookUpEdit1TreeList = new DevExpress.XtraTreeList.TreeList();
            this.DiagnosisLabel = new DevExpress.XtraEditors.LabelControl();
            this.pnlSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceUseArchiveData.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Year1SpinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Year2SpinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartMonthLookUp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EndMonthLookUp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.populationFilter.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSettings
            // 
            this.pnlSettings.Controls.Add(this.region1Filter);
            this.pnlSettings.Controls.Add(this.populationFilter);
            this.pnlSettings.Controls.Add(this.EndMonthLookUp);
            this.pnlSettings.Controls.Add(this.StartMonthLookUp);
            this.pnlSettings.Controls.Add(this.Year2SpinEdit);
            this.pnlSettings.Controls.Add(this.Year1SpinEdit);
            this.pnlSettings.Controls.Add(this.rayon1Filter);
            this.pnlSettings.Controls.Add(this.DiagnosisFilter);
            this.pnlSettings.Controls.Add(this.populationLabel);
            this.pnlSettings.Controls.Add(this.EndMonthLabel);
            this.pnlSettings.Controls.Add(this.StartMonthLabel);
            this.pnlSettings.Controls.Add(this.EndYearLabel);
            this.pnlSettings.Controls.Add(this.StartYearLabel);
            this.pnlSettings.Controls.Add(this.DiagnosisLabel);
            this.pnlSettings.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pnlSettings.MinimumSize = new System.Drawing.Size(724, 98);
            this.pnlSettings.Size = new System.Drawing.Size(788, 102);
            this.pnlSettings.Controls.SetChildIndex(this.StartYearLabel, 0);
            this.pnlSettings.Controls.SetChildIndex(this.EndYearLabel, 0);
            this.pnlSettings.Controls.SetChildIndex(this.StartMonthLabel, 0);
            this.pnlSettings.Controls.SetChildIndex(this.EndMonthLabel, 0);
            this.pnlSettings.Controls.SetChildIndex(this.populationLabel, 0);
            this.pnlSettings.Controls.SetChildIndex(this.ceUseArchiveData, 0);
            this.pnlSettings.Controls.SetChildIndex(this.GenerateReportButton, 0);
            this.pnlSettings.Controls.SetChildIndex(this.DiagnosisFilter, 0);
            this.pnlSettings.Controls.SetChildIndex(this.rayon1Filter, 0);
            this.pnlSettings.Controls.SetChildIndex(this.Year1SpinEdit, 0);
            this.pnlSettings.Controls.SetChildIndex(this.Year2SpinEdit, 0);
            this.pnlSettings.Controls.SetChildIndex(this.StartMonthLookUp, 0);
            this.pnlSettings.Controls.SetChildIndex(this.EndMonthLookUp, 0);
            this.pnlSettings.Controls.SetChildIndex(this.populationFilter, 0);
            this.pnlSettings.Controls.SetChildIndex(this.region1Filter, 0);
            this.pnlSettings.Controls.SetChildIndex(this.DiagnosisLabel, 0);
            // 
            // ceUseArchiveData
            // 
            this.ceUseArchiveData.Location = new System.Drawing.Point(666, 65);
            this.ceUseArchiveData.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ceUseArchiveData.Properties.Appearance.Font = new System.Drawing.Font("Arial", 8.25F);
            this.ceUseArchiveData.Properties.Appearance.Options.UseFont = true;
            this.ceUseArchiveData.Properties.AppearanceDisabled.Options.UseFont = true;
            this.ceUseArchiveData.Properties.AppearanceFocused.Options.UseFont = true;
            this.ceUseArchiveData.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.ceUseArchiveData.Size = new System.Drawing.Size(120, 19);
            this.ceUseArchiveData.TabIndex = 1009;
            // 
            // GenerateReportButton
            // 
            this.GenerateReportButton.Appearance.Font = new System.Drawing.Font("Arial", 8.25F);
            this.GenerateReportButton.Appearance.Options.UseFont = true;
            this.GenerateReportButton.Location = new System.Drawing.Point(620, 66);
            this.GenerateReportButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.GenerateReportButton.Size = new System.Drawing.Size(132, 20);
            this.GenerateReportButton.TabIndex = 1010;
            // Form Is Localizable: False
            // 
            // region1Filter
            // 
            this.region1Filter.Appearance.Font = new System.Drawing.Font("Arial", 8.25F);
            this.region1Filter.Appearance.Options.UseFont = true;
            this.region1Filter.Location = new System.Drawing.Point(31, 49);
            this.region1Filter.Name = "region1Filter";
            this.region1Filter.Size = new System.Drawing.Size(166, 44);
            this.region1Filter.TabIndex = 1007;
            this.region1Filter.ValueChanged += new System.EventHandler<EIDSS.Reports.BaseControls.Filters.SingleFilterEventArgs>(this.region1Filter_ValueChanged);
            // 
            // rayon1Filter
            // 
            this.rayon1Filter.Appearance.Options.UseFont = true;
            this.rayon1Filter.Location = new System.Drawing.Point(208, 50);
            this.rayon1Filter.Name = "rayon1Filter";
            this.rayon1Filter.Size = new System.Drawing.Size(137, 48);
            this.rayon1Filter.TabIndex = 1008;
            this.rayon1Filter.ValueChanged += new System.EventHandler<EIDSS.Reports.BaseControls.Filters.SingleFilterEventArgs>(this.rayon1Filter_ValueChanged);
            // 
            // Year1SpinEdit
            // 
            this.Year1SpinEdit.EditValue = new decimal(new int[] {
            2015,
            0,
            0,
            0});
            this.Year1SpinEdit.Location = new System.Drawing.Point(208, 26);
            this.Year1SpinEdit.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Year1SpinEdit.Name = "Year1SpinEdit";
            this.Year1SpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.Year1SpinEdit.Properties.Mask.EditMask = "\\d{1,5}";
            this.Year1SpinEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
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
            this.Year1SpinEdit.Size = new System.Drawing.Size(56, 20);
            this.Year1SpinEdit.TabIndex = 1002;
            this.Year1SpinEdit.EditValueChanged += new System.EventHandler(this.Year1SpinEdit_EditValueChanged);
            // 
            // Year2SpinEdit
            // 
            this.Year2SpinEdit.EditValue = new decimal(new int[] {
            2016,
            0,
            0,
            0});
            this.Year2SpinEdit.Location = new System.Drawing.Point(276, 26);
            this.Year2SpinEdit.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Year2SpinEdit.Name = "Year2SpinEdit";
            this.Year2SpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.Year2SpinEdit.Properties.Mask.EditMask = "\\d{1,5}";
            this.Year2SpinEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.Year2SpinEdit.Properties.MaxValue = new decimal(new int[] {
            2030,
            0,
            0,
            0});
            this.Year2SpinEdit.Properties.MinValue = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.Year2SpinEdit.Size = new System.Drawing.Size(56, 20);
            this.Year2SpinEdit.TabIndex = 1003;
            this.Year2SpinEdit.EditValueChanged += new System.EventHandler(this.Year2SpinEdit_EditValueChanged);
            // 
            // StartMonthLookUp
            // 
            this.StartMonthLookUp.Location = new System.Drawing.Point(364, 26);
            this.StartMonthLookUp.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.StartMonthLookUp.Name = "StartMonthLookUp";
            this.StartMonthLookUp.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.StartMonthLookUp.Properties.DropDownRows = 12;
            this.StartMonthLookUp.Properties.NullText = "";
            this.StartMonthLookUp.Size = new System.Drawing.Size(112, 20);
            this.StartMonthLookUp.TabIndex = 1004;
            this.StartMonthLookUp.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.MonthLookUp_ButtonClick);
            this.StartMonthLookUp.EditValueChanged += new System.EventHandler(this.StartMonthLookUp_EditValueChanged);
            // 
            // EndMonthLookUp
            // 
            this.EndMonthLookUp.Location = new System.Drawing.Point(488, 26);
            this.EndMonthLookUp.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.EndMonthLookUp.Name = "EndMonthLookUp";
            this.EndMonthLookUp.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.EndMonthLookUp.Properties.DropDownRows = 12;
            this.EndMonthLookUp.Properties.NullText = "";
            this.EndMonthLookUp.Size = new System.Drawing.Size(112, 20);
            this.EndMonthLookUp.TabIndex = 1005;
            this.EndMonthLookUp.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.MonthLookUp_ButtonClick);
            this.EndMonthLookUp.EditValueChanged += new System.EventHandler(this.EndMonthLookUp_EditValueChanged);
            // 
            // StartYearLabel
            // 
            this.StartYearLabel.AutoSize = true;
            this.StartYearLabel.Font = new System.Drawing.Font("Arial", 8.25F);
            this.StartYearLabel.ForeColor = System.Drawing.Color.Black;
            this.StartYearLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.StartYearLabel.Location = new System.Drawing.Point(207, 10);
            this.StartYearLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.StartYearLabel.Name = "StartYearLabel";
            this.StartYearLabel.Size = new System.Drawing.Size(54, 14);
            this.StartYearLabel.TabIndex = 1007;
            this.StartYearLabel.Text = "First Year";
            // 
            // EndYearLabel
            // 
            this.EndYearLabel.AutoSize = true;
            this.EndYearLabel.Font = new System.Drawing.Font("Arial", 8.25F);
            this.EndYearLabel.ForeColor = System.Drawing.Color.Black;
            this.EndYearLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.EndYearLabel.Location = new System.Drawing.Point(274, 10);
            this.EndYearLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.EndYearLabel.Name = "EndYearLabel";
            this.EndYearLabel.Size = new System.Drawing.Size(70, 14);
            this.EndYearLabel.TabIndex = 1008;
            this.EndYearLabel.Text = "Second Year";
            // 
            // StartMonthLabel
            // 
            this.StartMonthLabel.AutoSize = true;
            this.StartMonthLabel.Font = new System.Drawing.Font("Arial", 8.25F);
            this.StartMonthLabel.ForeColor = System.Drawing.Color.Black;
            this.StartMonthLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.StartMonthLabel.Location = new System.Drawing.Point(363, 10);
            this.StartMonthLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.StartMonthLabel.Name = "StartMonthLabel";
            this.StartMonthLabel.Size = new System.Drawing.Size(63, 14);
            this.StartMonthLabel.TabIndex = 1009;
            this.StartMonthLabel.Text = "From Month";
            // 
            // EndMonthLabel
            // 
            this.EndMonthLabel.AutoSize = true;
            this.EndMonthLabel.Font = new System.Drawing.Font("Arial", 8.25F);
            this.EndMonthLabel.ForeColor = System.Drawing.Color.Black;
            this.EndMonthLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.EndMonthLabel.Location = new System.Drawing.Point(486, 10);
            this.EndMonthLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.EndMonthLabel.Name = "EndMonthLabel";
            this.EndMonthLabel.Size = new System.Drawing.Size(50, 14);
            this.EndMonthLabel.TabIndex = 1010;
            this.EndMonthLabel.Text = "To Month";
            // 
            // populationFilter
            // 
            this.populationFilter.Location = new System.Drawing.Point(620, 26);
            this.populationFilter.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.populationFilter.Name = "populationFilter";
            this.populationFilter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.populationFilter.Properties.DropDownRows = 3;
            this.populationFilter.Size = new System.Drawing.Size(132, 20);
            this.populationFilter.TabIndex = 1006;
            // 
            // populationLabel
            // 
            this.populationLabel.AutoSize = true;
            this.populationLabel.Font = new System.Drawing.Font("Arial", 8.25F);
            this.populationLabel.ForeColor = System.Drawing.Color.Black;
            this.populationLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.populationLabel.Location = new System.Drawing.Point(619, 10);
            this.populationLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.populationLabel.Name = "populationLabel";
            this.populationLabel.Size = new System.Drawing.Size(56, 14);
            this.populationLabel.TabIndex = 1012;
            this.populationLabel.Text = "Population";
            // 
            // DiagnosisFilter
            // 
            this.DiagnosisFilter.Name = "treeListLookUpEditForDiagnoses";
            this.DiagnosisFilter.Location = new System.Drawing.Point(364, 67);
            this.DiagnosisFilter.Size = new System.Drawing.Size(236, 48);
            this.DiagnosisFilter.TabIndex = 1009;
            this.DiagnosisFilter.Properties.TreeList = this.treeListLookUpEdit1TreeList;
            // 
            // treeListLookUpEdit1TreeList
            // 
            this.treeListLookUpEdit1TreeList.Name = "treeListLookUpEdit1TreeList";
            this.treeListLookUpEdit1TreeList.OptionsBehavior.EnableFiltering = true;
            this.treeListLookUpEdit1TreeList.OptionsView.ShowIndentAsRowStyle = true;
            // 
            // StartYearLabel
            // 
            this.DiagnosisLabel.AutoSize = true;
            this.DiagnosisLabel.Font = new System.Drawing.Font("Arial", 8.25F);
            this.DiagnosisLabel.ForeColor = System.Drawing.Color.Black;
            this.DiagnosisLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.DiagnosisLabel.Location = new System.Drawing.Point(366, 50);
            this.DiagnosisLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.DiagnosisLabel.Name = "DiagnosisLabel";
            this.DiagnosisLabel.Size = new System.Drawing.Size(54, 14);
            this.DiagnosisLabel.TabIndex = 1007;
            this.DiagnosisLabel.Text = "Diagnosis";
            // 
            // ComparativeReportByRegionKeeper
            // 
            this.Appearance.Font = new System.Drawing.Font("Arial", 8.25F);
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.HeaderHeight = 124;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "ComparativeReportByRegionKeeper";
            this.Size = new System.Drawing.Size(792, 665);
            this.pnlSettings.ResumeLayout(false);
            this.pnlSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceUseArchiveData.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Year1SpinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Year2SpinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartMonthLookUp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EndMonthLookUp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.populationFilter.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BaseControls.Filters.RegionFilter region1Filter;
        private BaseControls.Filters.RayonFilter rayon1Filter;
        private DevExpress.XtraEditors.SpinEdit Year1SpinEdit;
        private DevExpress.XtraEditors.SpinEdit Year2SpinEdit;
        protected DevExpress.XtraEditors.LookUpEdit StartMonthLookUp;
        protected DevExpress.XtraEditors.LookUpEdit EndMonthLookUp;
        private System.Windows.Forms.Label StartYearLabel;
        private System.Windows.Forms.Label EndYearLabel;
        protected System.Windows.Forms.Label StartMonthLabel;
        protected System.Windows.Forms.Label EndMonthLabel;
        protected System.Windows.Forms.Label populationLabel;
        private DevExpress.XtraEditors.LookUpEdit populationFilter;
        private DevExpress.XtraEditors.TreeListLookUpEdit DiagnosisFilter;
        private DevExpress.XtraTreeList.TreeList treeListLookUpEdit1TreeList;
        private DevExpress.XtraEditors.LabelControl DiagnosisLabel;
    }
}