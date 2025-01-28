using System;
using System.ComponentModel;
using System.Drawing;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors.Controls;
using eidss.model.Avr.Chart;

namespace eidss.avr.ChartForm
{
    public partial class TitleSettings : BaseChartSettings
    {
        public Title CurrentTitle { get; set; }

        private bool m_VisibleChanged = true;
        private bool TitleVisibleChangedByUser
        {
            get { return m_VisibleChanged; }
            set { m_VisibleChanged = value; }
        }

        private void SetTitleVisibleValue(bool value)
        {
            TitleVisibleChangedByUser = false;
            cbVisible.Checked = value;
            TitleVisibleChangedByUser = true;
        }

        public TitleSettings()
        {
            InitializeComponent();
        }

        public TitleSettings(ChartDetailPanel chartDetailPanel) : base(chartDetailPanel)
        {
            InitializeComponent();
        }

        public override void Init(object properties)
        {
            ChartSettingsHelper.SetAlignmentList(cbAlignment, true);
            FromProperties(properties);
        }

        public override object ToProperties()
        {
            if (CurrentTitle == null) return null;
            var p = new TitleProperties();
            p.Text = cbVisible.Checked? CurrentTitle.Text : tbText.Text;
            p.Font.FromFont(CurrentTitle.Font, CurrentTitle.TextColor);
            p.Alignment = cbAlignment.SelectedIndex;
            p.Visibility = cbVisible.Checked;
            return p;
        }

        public override void FromProperties(object props)
        {
            if (CurrentTitle == null) return;
            base.FromProperties(props);
            var p = (TitleProperties) props;
            tbText.Text = p.Text;
            CurrentTitle.Text = p.Visibility ? p.Text : string.Empty;
            ChartDetailPanel.fontDialog.Font = CurrentTitle.Font = p.Font.ToFont();
            beFont.Text = p.Font.FontName;
            cbAlignment.SelectedIndex = p.Alignment;
            SetTitleVisibleValue(p.Visibility);
        }

        public static void SetupChart(Title title, object props)
        {
            var p = (TitleProperties)props;
            title.Font = p.Font.ToFont();
            title.Text = p.Visibility? p.Text : string.Empty;
            title.TextColor = p.Font.TextColor;
            title.Visible = p.Visibility;
            var al = ChartSettingsHelper.GetAlignment(p.Alignment);
            if (title is ChartTitle)
            {
                ((ChartTitle)title).Alignment = al;
            }
            else if (title is AxisTitle)
            {
                ((AxisTitle)title).Alignment = al;
            }
        }

        [Browsable(true), Localizable(true)]
        public string Title
        {
            get { return gcMain.Text; }
            set { gcMain.Text = value; }
        }

        private void beFont_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (CurrentTitle == null) return;
            Font font;
            Color color;
            ChartSettingsHelper.GetFont(ChartDetailPanel.fontDialog, out font, out color);
            CurrentTitle.Font = font;
            beFont.Text = font.Name;
            CurrentTitle.TextColor = color;
        }

        private void cbVisible_CheckedChanged(object sender, EventArgs e)
        {
            if ((CurrentTitle == null) || (!TitleVisibleChangedByUser)) return;
            CurrentTitle.Visible = cbVisible.Checked;
            CurrentTitle.Text = cbVisible.Checked ? tbText.Text : string.Empty;
        }

        private void cbTitleAlignment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrentTitle == null) return;
            var al = ChartSettingsHelper.GetAlignment(cbAlignment.SelectedIndex);
            if (CurrentTitle is ChartTitle)
            {
                ((ChartTitle)CurrentTitle).Alignment = al;
            }
            else if (CurrentTitle is AxisTitle)
            {
                ((AxisTitle)CurrentTitle).Alignment = al;
            }
        }

        private void tbChartName_EditValueChanged(object sender, EventArgs e)
        {
            if (CurrentTitle == null) return;
            CurrentTitle.Text = cbVisible.Checked ? tbText.Text : string.Empty;
        }
    }
}
