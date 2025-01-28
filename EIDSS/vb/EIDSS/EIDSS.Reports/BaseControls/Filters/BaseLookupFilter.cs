using System;
using System.ComponentModel;
using bv.common.Core;
using bv.winclient.Core;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using System.Drawing;
using System.Windows.Forms;

namespace EIDSS.Reports.BaseControls.Filters
{
    public partial class BaseLookupFilter : BaseFilter
    {
        /// <summary>
        ///     Fires immediately after lookup edit value has been changed
        /// </summary>
        public event EventHandler<SingleFilterEventArgs> ValueChanged;

        private bool m_IsClear;

        public BaseLookupFilter()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Returns Editor Value. If Value is null, returns -1;
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public long EditValueId
        {
            get
            {
                long id;
                return long.TryParse(Utils.Str(LookUp.EditValue), out id) ? id : -1;
            }
            set { LookUp.EditValue = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object EditValue
        {
            get { return LookUp.EditValue; }
            set { LookUp.EditValue = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ItemIndex
        {
            get { return LookUp.ItemIndex; }
            set { LookUp.ItemIndex = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool LookUpenabled
        {
            get { return LookUp.Enabled; }
            protected set { LookUp.Enabled = value; }
        }

        public int Count
        {
            get { return DataSource.Count; }
        }

        public string SelectedText
        {
            get { return LookUp.Text; }
        }

       

        /// <summary>
        ///     Caption of the Lookup Control
        /// </summary>
        [Browsable(false)]
        protected virtual string LookupCaption
        {
            get
            {
                if (WinUtils.IsComponentInDesignMode(this))
                {
                    return string.Empty;
                }
                throw new InvalidOperationException("Property  should be overrided in child class.");
            }
        }

        /// <summary>
        ///     Get or Set Caption of the Lookup Control and Lookup Column Name
        /// </summary>
        [Browsable(false)]
        public virtual string ExternalLookupCaption
        {
            get
            { 
                if (WinUtils.IsComponentInDesignMode(this))
                {
                    return string.Empty;
                }
                return lblLookupName.Text; 
            }
            set
            { 
                lblLookupName.Text = value;
                if (LookUp.Properties.Columns.Count > 0)
                {
                    LookUp.Properties.Columns[0].Caption = value;
                }
            }
        }

        /// <summary>
        ///     Get or Set Location of the Label Control
        /// </summary>
        [Browsable(false)]
        protected virtual Point LabelLocation
        {
            get
            {
                if (WinUtils.IsComponentInDesignMode(this))
                {
                    return new Point(0, 0);
                }
                return lblLookupName.Location;
            }
            set
            {
                var dock = lblLookupName.Dock;
                lblLookupName.Dock = System.Windows.Forms.DockStyle.None;

                var anchor = lblLookupName.Anchor;
                lblLookupName.Anchor = System.Windows.Forms.AnchorStyles.None;
                
                lblLookupName.Location = value;
                lblLookupName.Anchor = anchor;
                lblLookupName.Dock = dock;
            }
        }

        /// <summary>
        ///     Get or Set Width of the Label Control
        /// </summary>
        [Browsable(false)]
        protected virtual int LabelWidth
        {
            get
            {
                if (WinUtils.IsComponentInDesignMode(this))
                {
                    return 0;
                }
                return lblLookupName.Width;
            }
            set
            {
                if ((value == null) || (value <= 0))
                    return;

                var dock = lblLookupName.Dock;
                lblLookupName.Dock = System.Windows.Forms.DockStyle.None;

                var anchor = lblLookupName.Anchor;
                lblLookupName.Anchor = System.Windows.Forms.AnchorStyles.None;

                lblLookupName.Width = value;
                lblLookupName.Anchor = anchor;
                lblLookupName.Dock = dock;
            }
        }

        /// <summary>
        ///     Get or Set Height of the Label Control
        /// </summary>
        [Browsable(false)]
        protected virtual int LabelHeight
        {
            get
            {
                if (WinUtils.IsComponentInDesignMode(this))
                {
                    return 0;
                }
                return lblLookupName.Height;
            }
            set
            {
                if ((value == null) || (value <= 0))
                    return;

                var dock = lblLookupName.Dock;
                lblLookupName.Dock = System.Windows.Forms.DockStyle.None;

                var anchor = lblLookupName.Anchor;
                lblLookupName.Anchor = System.Windows.Forms.AnchorStyles.None;

                lblLookupName.Height = value;
                lblLookupName.Anchor = anchor;
                lblLookupName.Dock = dock;
            }
        }

        /// <summary>
        ///     Get or Set Anchor Style of the Label Control
        /// </summary>
        [Browsable(false)]
        protected virtual AnchorStyles LabelAnchor
        {
            get
            {
                if (WinUtils.IsComponentInDesignMode(this))
                {
                    return System.Windows.Forms.AnchorStyles.None;
                }
                return lblLookupName.Anchor;
            }
            set
            {
                var dock = lblLookupName.Dock;
                lblLookupName.Dock = System.Windows.Forms.DockStyle.None;

                lblLookupName.Anchor = value;
                if (value == AnchorStyles.None)
                    lblLookupName.Dock = dock;
            }
        }

        /// <summary>
        ///     Get or Set Dock Style of the Label Control
        /// </summary>
        [Browsable(false)]
        protected virtual DockStyle LabelDock
        {
            get
            {
                if (WinUtils.IsComponentInDesignMode(this))
                {
                    return System.Windows.Forms.DockStyle.None;
                }
                return lblLookupName.Dock;
            }
            set
            {
                var anchor = lblLookupName.Anchor;
                lblLookupName.Anchor = System.Windows.Forms.AnchorStyles.None;

                lblLookupName.Dock = value;
                if (value == DockStyle.None)
                    lblLookupName.Anchor = anchor;
            }
        }

        /// <summary>
        ///     Get or Set Location of the Lookup Control
        /// </summary>
        [Browsable(false)]
        protected virtual Point LookupLocation
        {
            get
            {
                if (WinUtils.IsComponentInDesignMode(this))
                {
                    return new Point(0, 0);
                }
                return LookUp.Location;
            }
            set
            {
                var dock = LookUp.Dock;
                LookUp.Dock = System.Windows.Forms.DockStyle.None;

                var anchor = LookUp.Anchor;
                LookUp.Anchor = System.Windows.Forms.AnchorStyles.None;

                LookUp.Location = value;
                LookUp.Anchor = anchor;
                LookUp.Dock = dock;
            }
        }

        /// <summary>
        ///     Get or Set Width of the Lookup Control
        /// </summary>
        [Browsable(false)]
        protected virtual int LookupWidth
        {
            get
            {
                if (WinUtils.IsComponentInDesignMode(this))
                {
                    return 0;
                }
                return LookUp.Width;
            }
            set
            {
                if ((value == null) || (value <= 0))
                    return;

                var dock = LookUp.Dock;
                LookUp.Dock = System.Windows.Forms.DockStyle.None;

                var anchor = LookUp.Anchor;
                LookUp.Anchor = System.Windows.Forms.AnchorStyles.None;

                LookUp.Width = value;
                LookUp.Anchor = anchor;
                LookUp.Dock = dock;
            }
        }

        /// <summary>
        ///     Get or Set Height of the Lookup Control
        /// </summary>
        [Browsable(false)]
        protected virtual int LookupHeight
        {
            get
            {
                if (WinUtils.IsComponentInDesignMode(this))
                {
                    return 0;
                }
                return LookUp.Height;
            }
            set
            {
                if ((value == null) || (value <= 0))
                    return;

                var dock = LookUp.Dock;
                LookUp.Dock = System.Windows.Forms.DockStyle.None;

                var anchor = LookUp.Anchor;
                LookUp.Anchor = System.Windows.Forms.AnchorStyles.None;

                LookUp.Height = value;
                LookUp.Anchor = anchor;
                LookUp.Dock = dock;
            }
        }

        /// <summary>
        ///     Get or Set Anchor Style of the Lookup Control
        /// </summary>
        [Browsable(false)]
        protected virtual AnchorStyles LookupAnchor
        {
            get
            {
                if (WinUtils.IsComponentInDesignMode(this))
                {
                    return System.Windows.Forms.AnchorStyles.None;
                }
                return LookUp.Anchor;
            }
            set
            {
                var dock = LookUp.Dock;
                LookUp.Dock = System.Windows.Forms.DockStyle.None;

                LookUp.Anchor = value;
                if (value == AnchorStyles.None)
                    LookUp.Dock = dock;
            }
        }

        /// <summary>
        ///     Get or Set Dock Style of the Lookup Control
        /// </summary>
        [Browsable(false)]
        protected virtual DockStyle LookupDock
        {
            get
            {
                if (WinUtils.IsComponentInDesignMode(this))
                {
                    return System.Windows.Forms.DockStyle.None;
                }
                return LookUp.Dock;
            }
            set
            {
                var anchor = LookUp.Anchor;
                LookUp.Anchor = System.Windows.Forms.AnchorStyles.None;

                LookUp.Dock = value;
                if (value == DockStyle.None)
                    LookUp.Anchor = anchor;
            }
        }

        /// <summary>
        ///     Apply Default Filter Layout
        /// </summary>
        public void ApplyDefaultFilterLayout()
        {
            LabelDock = DockStyle.None;
            LabelLocation = new Point(0, lblLookupName.Location.Y);
            LabelWidth = this.Width;
            LabelAnchor = AnchorStyles.Top | AnchorStyles.Left;
            lblLookupName.Height = 14;
            lblLookupName.BringToFront();
 
            LookupDock = DockStyle.None;
            LookupLocation = new Point(0, LookUp.Location.Y);
            LookupWidth = this.Width;
            LookupAnchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        }

        public void HideClearButton()
        {
            if (LookUp.Properties.Buttons.Count == 0)
                return;
            foreach (EditorButton b in LookUp.Properties.Buttons)
            {
                if (((b.Kind == ButtonPredefines.Clear) || (b.Kind == ButtonPredefines.Delete)) &&
                    b.Visible)
                    b.Visible = false;
            }
        }


        /// <summary>
        ///     Set Filter Mandatory
        /// </summary>
        public void SetMandatory()
        {
            SetLookupMandatory(LookUp);
        }
        public void SetReadOnly()
        {
            SetLookupReadOnly(LookUp);
        }

        public override void DefineBinding()
        {
            LookUp.SuspendLayout();
            LookUp.Properties.Columns.Clear();
            LookUp.Properties.Columns.Add(new LookUpColumnInfo(ValueColumnName,
                LookupCaption,
                200, FormatType.None, "", true, HorzAlignment.Near));
            LookUp.Properties.PopupWidth = LookUp.Width;

            object oldValue = LookUp.EditValue;
            string oldFilter = DataSource.RowFilter;
            ResetDataSource();
            LookUp.Properties.DataSource = DataSource;
            DataSource.RowFilter = oldFilter;

            LookUp.Properties.DisplayMember = ValueColumnName;
            LookUp.Properties.ValueMember = KeyColumnName;

            LookUp.EditValue = oldValue;
            ApplyResources();
            LookUp.ResumeLayout();
        }

        private void LookupEditValueChanged(object sender, EventArgs e)
        {
            string value = Utils.Str(LookUp.GetColumnValue(ValueColumnName));
            EventHandler<SingleFilterEventArgs> tmpHandler = ValueChanged;
            if (tmpHandler != null)
            {
                tmpHandler(sender, new SingleFilterEventArgs(EditValueId, value, m_IsClear));
            }
        }

        private void LookUp_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind != ButtonPredefines.Delete)
            {
                return;
            }

            try
            {
                m_IsClear = true;

                LookUp.ClosePopup();
                string filter = DataSource.RowFilter;
                LookUp.EditValue = null;
                DataSource.RowFilter = filter;
                LookUp.Reset();
            }
            finally
            {
                m_IsClear = false;
            }
        }
    }
}