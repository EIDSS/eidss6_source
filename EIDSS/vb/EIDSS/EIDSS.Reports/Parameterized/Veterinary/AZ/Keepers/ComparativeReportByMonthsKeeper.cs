using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using System.Globalization;
using System.Windows.Forms;
using System.ComponentModel;

using bv.common.Core;
using bv.model.BLToolkit;
using eidss.model.Core.CultureInfo;
using EIDSS.Reports.BaseControls.Keeper;
using eidss.model.Reports.OperationContext;
using EIDSS.Reports.BaseControls.Filters;
using eidss.model.Reports.AZ;
using bv.winclient.Core;
using bv.winclient.Layout;
using DevExpress.XtraEditors.Controls;
using EIDSS.Reports.BaseControls.Report;
using EIDSS.Reports.Parameterized.Veterinary.AZ.Reports;
using eidss.model.Core;

namespace EIDSS.Reports.Parameterized.Veterinary.AZ.Keepers
{
    public class ComparativeReportByMonthsKeeper : BaseReportKeeper
    {
        private DevExpress.XtraEditors.SpinEdit ToYearSpin;
        private System.Windows.Forms.Label StartYearLabel;
        private System.Windows.Forms.Label EndYearLabel;
        private BaseControls.Filters.RegionAZFilter region1Filter;
        private BaseControls.Filters.RayonFilter rayon1Filter;
        private BaseControls.Filters.VetSingleDiagnosisAZFilter VetDiagnosisFilter;
        private BaseControls.Filters.SpeciesTypeAZMultiFilter SpeciesTypeFilter;
        private DevExpress.XtraEditors.SpinEdit FromYearSpin;

        CultureInfo _cultureInfo = Thread.CurrentThread.CurrentUICulture;

        private ComponentResourceManager _resources =
            new ComponentResourceManager(typeof(ComparativeReportByMonthsKeeper));

        // Messages which should be localized before use in UI.
        private string NoSpeciesAreSelected_Message;
        private string Three_Species_Warning_Message;
        private string Year_Range_Warning_Message;

        public ComparativeReportByMonthsKeeper()
        {
            ReportType = typeof(ComparativeReportByMonths);

            InitializeComponent();

            LayoutCorrector.SetStyleController(FromYearSpin, LayoutCorrector.MandatoryStyleController);
            LayoutCorrector.SetStyleController(ToYearSpin, LayoutCorrector.MandatoryStyleController);

            DateTime dtNow = DateTime.Now;

            FromYearSpin.Properties.MaxValue = dtNow.Year;
            FromYearSpin.Value = dtNow.Year - 1;

            ToYearSpin.Properties.MaxValue = dtNow.Year;
            ToYearSpin.Value = dtNow.Year;

            this.ToYearSpin.EditValueChanged += new System.EventHandler(this.ToYearSpin_EditValueChanged);
            this.ToYearSpin.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.ToYearSpin_EditValueChanging);

            this.FromYearSpin.EditValueChanged += new System.EventHandler(this.FromYearSpin_EditValueChanged);
            this.FromYearSpin.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.FromYearSpin_EditValueChanging);

            m_HasLoad = true;
        }

        protected override BaseReport GenerateReport(DbManagerProxy manager, DbManagerProxy archiveManager)
        {
            long? firstRegionID = region1Filter.RegionId > 0 ? (long?)region1Filter.RegionId : null;
            long? firstRayonID = rayon1Filter.RayonId > 0 ? (long?)rayon1Filter.RayonId : null;

            VetComparativeByMonthModel model = new VetComparativeByMonthModel();

            model.OrganizationId = Convert.ToInt64(EidssUserContext.User.OrganizationID);
            model.Language = CurrentCulture.ShortName;

            model.YearFrom = (int)FromYearSpin.Value;
            model.YearTo = (int)ToYearSpin.Value;

            if (VetDiagnosisFilter.EditValueId != -1)
            {
                model.DiagnosisId = VetDiagnosisFilter.EditValueId;
                model.Diagnosis = VetDiagnosisFilter.SelectedText;
            }

            string speciesValue = Utils.Str(SpeciesTypeFilter.EditValue);
            model.SpecieIds = speciesValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            model.Species = SpeciesTypeFilter.SelectUIValues();

            model.RegionId = firstRegionID;
            model.RegionName = region1Filter.SelectedText;

            model.RayonId = firstRayonID;
            model.RayonName = rayon1Filter.SelectedText;

            model.ForbiddenGroups = EidssUserContext.User.ForbiddenPersonalDataGroups;
            model.UseArchive = UseArchive;

            ComparativeReportByMonths report = (ComparativeReportByMonths)CreateReportObject();
            report.SetParameters(model, manager, archiveManager);
            return report;
        }

        protected override bool CheckBusinessRules(bool printException)
        {
            VetComparativeByMonthModel.ModelStateType currentState =
                VetComparativeByMonthModel.FigureOutTheState(
                    (int)FromYearSpin.Value,
                    (int)ToYearSpin.Value,
                    SpeciesTypeFilter.SelectUIValues().Length);

            switch(currentState)
            {
                case VetComparativeByMonthModel.ModelStateType.NoSpeciesWrongState:

                    ErrorForm.ShowWarning(NoSpeciesAreSelected_Message);
                    return false;

                case VetComparativeByMonthModel.ModelStateType.WrongState:
                    ErrorForm.ShowWarning("Wrong state.");
                    return false;
            }

            return true;
        }

        protected internal override void ApplyResources(DbManagerProxy manager)
        {
            base.ApplyResources(manager);

            NoSpeciesAreSelected_Message = _resources.GetString("NoSpeciesAreSelected_Key");
            Three_Species_Warning_Message = _resources.GetString("Three_Species_Warning");
            Year_Range_Warning_Message = _resources.GetString("Year_Range_Warning");

            StartYearLabel.Text = _resources.GetString("StartYearLabel.Text");
            EndYearLabel.Text = _resources.GetString("EndYearLabel.Text");

            VetDiagnosisFilter.SurveillanceType = VetSummarySurveillanceType.PassiveSurveillanceIndex;
            VetDiagnosisFilter.DefineBinding();
            SpeciesTypeFilter.Code = eidss.model.Enums.HACode.LivestockAvian;
            SpeciesTypeFilter.SelectAllItemVisible = false;
            SpeciesTypeFilter.DefineBinding();

            region1Filter.DefineBinding();
            rayon1Filter.DefineBinding();
            region1Filter.Top = rayon1Filter.Top + 1;

            if (ContextKeeper.ContainsContext(ContextValue.ReportKeeperFirstLoading))
            {
                LocationHelper.SetDefaultFilters(manager, ContextKeeper, region1Filter, rayon1Filter);
            }
        }

        private void VetDiagnosisFilter_ValueChanged(object sender, BaseControls.Filters.SingleFilterEventArgs e)
        {
            using (new CultureInfoTransaction(_cultureInfo))
            {
                SpeciesTypeFilter.ClearSelection();

                SpeciesTypeFilter.Code =
                    VetDiagnosisFilter.Code == eidss.model.Enums.HACode.None ?
                        eidss.model.Enums.HACode.All :
                        VetDiagnosisFilter.Code;

                SpeciesTypeFilter.DefineBinding();
            }
        }

        private void FromYearSpin_EditValueChanged(object sender, EventArgs e)
        {
            if (FromYearSpin.Value > ToYearSpin.Value)
            {
                ToYearSpin.Value = FromYearSpin.Value;
            }
        }

        private void ToYearSpin_EditValueChanged(object sender, EventArgs e)
        {
            if (FromYearSpin.Value > ToYearSpin.Value)
            {
                FromYearSpin.Value = ToYearSpin.Value;
            }
        }

        private bool MessageBoxWasAlreadyShown = false;

        private void FromYearSpin_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue is decimal)
            {
                if (Math.Abs((decimal)e.NewValue - ToYearSpin.Value) > 2)
                {
                    e.Cancel = true;

                    ErrorForm.ShowWarningFormat(Year_Range_Warning_Message, null);

                    MessageBoxWasAlreadyShown = true;
                }
            }
        }

        private void ToYearSpin_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue is decimal)
            {
                if (Math.Abs(FromYearSpin.Value - (decimal)e.NewValue) > 2)
                {
                    e.Cancel = true;

                    ErrorForm.ShowWarningFormat(Year_Range_Warning_Message, null);

                    MessageBoxWasAlreadyShown = true;
                }
            }
        }

        private void FromYearSpin_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Math.Abs(FromYearSpin.Value - ToYearSpin.Value) > 2)
            {
                FromYearSpin.Value = ToYearSpin.Value;

                if (!MessageBoxWasAlreadyShown)
                {
                    ErrorForm.ShowWarningFormat(Year_Range_Warning_Message, null);
                }
                
                MessageBoxWasAlreadyShown = false;
            }
        }

        private void ToYearSpin_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Math.Abs(FromYearSpin.Value - ToYearSpin.Value) > 2)
            {
                ToYearSpin.Value = FromYearSpin.Value;

                if (!MessageBoxWasAlreadyShown)
                {
                    ErrorForm.ShowWarningFormat(Year_Range_Warning_Message, null);
                }
                
                MessageBoxWasAlreadyShown = false;
            }
        }

        private void SpeciesTypeFilter_ValueChanging(object sender, ChangingEventArgs e)
        {
            string newValue = Utils.Str(e.NewValue);
            if (newValue.Split(',').Count() > 3)
            {
                //if (!Utils.IsReportsServiceRunning && !Utils.IsAvrServiceRunning)
                {
                    ErrorForm.ShowWarningFormat(Three_Species_Warning_Message, null);
                }
                e.Cancel = true;
            }
        }

        private void region1Filter_ValueChanged(object sender, SingleFilterEventArgs e)
        {
            rayon1Filter.Enabled = !String.IsNullOrEmpty(region1Filter.SelectedText);

            using (ContextKeeper.CreateNewContext(ContextValue.ReportFilterResetting))
            {
                LocationHelper.RegionFilterValueChanged(region1Filter, rayon1Filter, e);
            }
        }

        private void InitializeComponent()
        {
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComparativeReportByMonthsKeeper));
            this.FromYearSpin = new DevExpress.XtraEditors.SpinEdit();
            this.ToYearSpin = new DevExpress.XtraEditors.SpinEdit();
            this.StartYearLabel = new System.Windows.Forms.Label();
            this.EndYearLabel = new System.Windows.Forms.Label();
            this.region1Filter = new EIDSS.Reports.BaseControls.Filters.RegionAZFilter();
            this.rayon1Filter = new EIDSS.Reports.BaseControls.Filters.RayonFilter();
            this.VetDiagnosisFilter = new EIDSS.Reports.BaseControls.Filters.VetSingleDiagnosisAZFilter();
            this.SpeciesTypeFilter = new EIDSS.Reports.BaseControls.Filters.SpeciesTypeAZMultiFilter();
            this.pnlSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceUseArchiveData.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FromYearSpin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ToYearSpin.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSettings
            // 
            this.pnlSettings.Controls.Add(this.SpeciesTypeFilter);
            this.pnlSettings.Controls.Add(this.VetDiagnosisFilter);
            this.pnlSettings.Controls.Add(this.rayon1Filter);
            this.pnlSettings.Controls.Add(this.region1Filter);
            this.pnlSettings.Controls.Add(this.EndYearLabel);
            this.pnlSettings.Controls.Add(this.StartYearLabel);
            this.pnlSettings.Controls.Add(this.ToYearSpin);
            this.pnlSettings.Controls.Add(this.FromYearSpin);
            resources.ApplyResources(this.pnlSettings, "pnlSettings");
            this.pnlSettings.Controls.SetChildIndex(this.ceUseArchiveData, 0);
            this.pnlSettings.Controls.SetChildIndex(this.GenerateReportButton, 0);
            this.pnlSettings.Controls.SetChildIndex(this.FromYearSpin, 0);
            this.pnlSettings.Controls.SetChildIndex(this.ToYearSpin, 0);
            this.pnlSettings.Controls.SetChildIndex(this.StartYearLabel, 0);
            this.pnlSettings.Controls.SetChildIndex(this.EndYearLabel, 0);
            this.pnlSettings.Controls.SetChildIndex(this.region1Filter, 0);
            this.pnlSettings.Controls.SetChildIndex(this.rayon1Filter, 0);
            this.pnlSettings.Controls.SetChildIndex(this.VetDiagnosisFilter, 0);
            this.pnlSettings.Controls.SetChildIndex(this.SpeciesTypeFilter, 0);
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
            bv.common.Resources.BvResourceManagerChanger.GetResourceManager(typeof(ComparativeReportByMonthsKeeper), out resources);
            // Form Is Localizable: True
            // 
            // FromYearSpin
            // 
            resources.ApplyResources(this.FromYearSpin, "FromYearSpin");
            this.FromYearSpin.Name = "FromYearSpin";
            this.FromYearSpin.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            serializableAppearanceObject2.Options.UseTextOptions = true;
            serializableAppearanceObject2.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            serializableAppearanceObject2.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.FromYearSpin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("FromYearSpin.Properties.Buttons"))), resources.GetString("FromYearSpin.Properties.Buttons1"), ((int)(resources.GetObject("FromYearSpin.Properties.Buttons2"))), ((bool)(resources.GetObject("FromYearSpin.Properties.Buttons3"))), ((bool)(resources.GetObject("FromYearSpin.Properties.Buttons4"))), ((bool)(resources.GetObject("FromYearSpin.Properties.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("FromYearSpin.Properties.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("FromYearSpin.Properties.Buttons7"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, resources.GetString("FromYearSpin.Properties.Buttons8"), ((object)(resources.GetObject("FromYearSpin.Properties.Buttons9"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("FromYearSpin.Properties.Buttons10"))), ((bool)(resources.GetObject("FromYearSpin.Properties.Buttons11"))))});
            this.FromYearSpin.Properties.Mask.EditMask = resources.GetString("FromYearSpin.Properties.Mask.EditMask");
            this.FromYearSpin.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("FromYearSpin.Properties.Mask.MaskType")));
            this.FromYearSpin.Properties.MaxValue = new decimal(new int[] {
            2030,
            0,
            0,
            0});
            this.FromYearSpin.Properties.MinValue = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.FromYearSpin.Validating += new System.ComponentModel.CancelEventHandler(this.FromYearSpin_Validating);
            // 
            // ToYearSpin
            // 
            resources.ApplyResources(this.ToYearSpin, "ToYearSpin");
            this.ToYearSpin.Name = "ToYearSpin";
            serializableAppearanceObject1.Options.UseTextOptions = true;
            serializableAppearanceObject1.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            serializableAppearanceObject1.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.ToYearSpin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("ToYearSpin.Properties.Buttons"))), resources.GetString("ToYearSpin.Properties.Buttons1"), ((int)(resources.GetObject("ToYearSpin.Properties.Buttons2"))), ((bool)(resources.GetObject("ToYearSpin.Properties.Buttons3"))), ((bool)(resources.GetObject("ToYearSpin.Properties.Buttons4"))), ((bool)(resources.GetObject("ToYearSpin.Properties.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("ToYearSpin.Properties.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("ToYearSpin.Properties.Buttons7"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("ToYearSpin.Properties.Buttons8"), ((object)(resources.GetObject("ToYearSpin.Properties.Buttons9"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("ToYearSpin.Properties.Buttons10"))), ((bool)(resources.GetObject("ToYearSpin.Properties.Buttons11"))))});
            this.ToYearSpin.Properties.Mask.EditMask = resources.GetString("ToYearSpin.Properties.Mask.EditMask");
            this.ToYearSpin.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("ToYearSpin.Properties.Mask.MaskType")));
            this.ToYearSpin.Properties.MaxValue = new decimal(new int[] {
            2030,
            0,
            0,
            0});
            this.ToYearSpin.Properties.MinValue = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.ToYearSpin.Validating += new System.ComponentModel.CancelEventHandler(this.ToYearSpin_Validating);
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
            // region1Filter
            // 
            this.region1Filter.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("region1Filter.Appearance.Font")));
            this.region1Filter.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.region1Filter, "region1Filter");
            this.region1Filter.Name = "region1Filter";
            this.region1Filter.ValueChanged += new System.EventHandler<EIDSS.Reports.BaseControls.Filters.SingleFilterEventArgs>(this.region1Filter_ValueChanged);
            // 
            // rayon1Filter
            // 
            this.rayon1Filter.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.rayon1Filter, "rayon1Filter");
            this.rayon1Filter.Name = "rayon1Filter";
            // 
            // VetDiagnosisFilter
            // 
            this.VetDiagnosisFilter.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("VetDiagnosisFilter.Appearance.Font")));
            this.VetDiagnosisFilter.Appearance.Options.UseFont = true;
            this.VetDiagnosisFilter.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            resources.ApplyResources(this.VetDiagnosisFilter, "VetDiagnosisFilter");
            this.VetDiagnosisFilter.Name = "VetDiagnosisFilter";
            this.VetDiagnosisFilter.ValueChanged += new System.EventHandler<EIDSS.Reports.BaseControls.Filters.SingleFilterEventArgs>(this.VetDiagnosisFilter_ValueChanged);
            // 
            // SpeciesTypeFilter
            // 
            this.SpeciesTypeFilter.Appearance.Options.UseFont = true;
            this.SpeciesTypeFilter.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            resources.ApplyResources(this.SpeciesTypeFilter, "SpeciesTypeFilter");
            this.SpeciesTypeFilter.Name = "SpeciesTypeFilter";
            this.SpeciesTypeFilter.ValueChanging += new System.EventHandler<DevExpress.XtraEditors.Controls.ChangingEventArgs>(this.SpeciesTypeFilter_ValueChanging);
            // 
            // ComparativeReportByMonthsKeeper
            // 
            this.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("ComparativeReportByMonthsKeeper.Appearance.Font")));
            this.Appearance.Options.UseFont = true;
            this.HeaderHeight = 150;
            this.Name = "ComparativeReportByMonthsKeeper";
            this.pnlSettings.ResumeLayout(false);
            this.pnlSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceUseArchiveData.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FromYearSpin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ToYearSpin.Properties)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
