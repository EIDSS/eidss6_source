namespace EIDSS.Reports.BaseControls.Filters
{
    partial class SyndrOrganizationFilter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SyndrOrganizationFilter));
            this.SuspendLayout();
            // 
            // lblLookupName
            // 
            resources.ApplyResources(this.lblLookupName, "lblLookupName");
            bv.common.Resources.BvResourceManagerChanger.GetResourceManager(typeof(SyndrOrganizationFilter), out resources);
            // Form Is Localizable: True
            // 
            // SyndrOrganizationFilter
            // 
            resources.ApplyResources(this, "$this");
            this.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("SyndrOrganizationFilter.Appearance.Font")));
            this.Appearance.FontSizeDelta = ((int)(resources.GetObject("SyndrOrganizationFilter.Appearance.FontSizeDelta")));
            this.Appearance.FontStyleDelta = ((System.Drawing.FontStyle)(resources.GetObject("SyndrOrganizationFilter.Appearance.FontStyleDelta")));
            this.Appearance.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("SyndrOrganizationFilter.Appearance.GradientMode")));
            this.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("SyndrOrganizationFilter.Appearance.Image")));
            this.Appearance.Options.UseFont = true;
            this.Name = "SyndrOrganizationFilter";
            this.ResumeLayout(false);

        }

        #endregion
    }
}
