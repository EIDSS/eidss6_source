﻿namespace EIDSS.Reports.BaseControls.Filters
{
    partial class LabAZLookupFilter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LabAZLookupFilter));
            this.SuspendLayout();
            // 
            // lblLookupName
            // 
            resources.ApplyResources(this.lblLookupName, "lblLookupName");
            bv.common.Resources.BvResourceManagerChanger.GetResourceManager(typeof(LabAZLookupFilter), out resources);
            // Form Is Localizable: True
            // 
            // SendToAZLookupFilter
            // 
            resources.ApplyResources(this, "$this");
            this.Name = "LabAZLookupFilter";
            this.ResumeLayout(false);

        }

        #endregion
    }
}
