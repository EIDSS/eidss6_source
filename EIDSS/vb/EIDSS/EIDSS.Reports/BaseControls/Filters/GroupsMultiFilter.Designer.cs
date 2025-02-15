﻿namespace EIDSS.Reports.BaseControls.Filters
{
    partial class GroupsMultiFilter
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupsMultiFilter));
            this.lblcheckedComboBoxName = new System.Windows.Forms.Label();
            this.treeListLookUp = new DevExpress.XtraEditors.TreeListLookUpEdit();
            this.treeList = new DevExpress.XtraTreeList.TreeList();
            ((System.ComponentModel.ISupportInitialize)(this.treeListLookUp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();
            this.SuspendLayout();
            bv.common.Resources.BvResourceManagerChanger.GetResourceManager(typeof(GroupsMultiFilter), out resources);
            // Form Is Localizable: True
            // 
            // lblcheckedComboBoxName
            // 
            resources.ApplyResources(this.lblcheckedComboBoxName, "lblcheckedComboBoxName");
            this.lblcheckedComboBoxName.Name = "lblcheckedComboBoxName";
            // 
            // treeListLookUp
            // 
            resources.ApplyResources(this.treeListLookUp, "treeListLookUp");
            this.treeListLookUp.Name = "treeListLookUp";
            this.treeListLookUp.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("treeListLookUp.Properties.Appearance.Font")));
            this.treeListLookUp.Properties.Appearance.Options.UseFont = true;
            this.treeListLookUp.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("treeListLookUp.Properties.Buttons"))))});
            this.treeListLookUp.Properties.NullText = resources.GetString("treeListLookUp.Properties.NullText");
            this.treeListLookUp.Properties.TreeList = this.treeList;
            this.treeListLookUp.CustomDisplayText += new DevExpress.XtraEditors.Controls.CustomDisplayTextEventHandler(this.CustomDisplayText);
            // 
            // treeList
            // 
            resources.ApplyResources(this.treeList, "treeList");
            this.treeList.Name = "treeList";
            this.treeList.OptionsBehavior.EnableFiltering = true;
            this.treeList.OptionsView.ShowCheckBoxes = true;
            this.treeList.OptionsView.ShowIndentAsRowStyle = true;
            this.treeList.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.AfterCheckNode);
            this.treeList.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.FocusedNodeChanged);
            // 
            // GroupsMultiFilter
            // 
            this.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("GroupsMultiFilter.Appearance.Font")));
            this.Appearance.Options.UseFont = true;
            resources.ApplyResources(this, "$this");
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.Controls.Add(this.treeListLookUp);
            this.Controls.Add(this.lblcheckedComboBoxName);
            this.Name = "GroupsMultiFilter";
            ((System.ComponentModel.ISupportInitialize)(this.treeListLookUp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblcheckedComboBoxName;
        private DevExpress.XtraEditors.TreeListLookUpEdit treeListLookUp;
        private DevExpress.XtraTreeList.TreeList treeList;

    }
}
