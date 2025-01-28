//using System.ComponentModel;
//using System.Data;
//using bv.common.db.Core;
//using bv.common.Resources;
//using eidss.model.Resources;

//namespace EIDSS.Reports.BaseControls.Filters
//{
//    // TODO: Remove after replacing.
//    public sealed partial class HumDiagnosisGroupsDiagnosesFilter : GroupsMultiFilter
//    {
//        private readonly ComponentResourceManager m_Resources = new ComponentResourceManager(typeof (HumDiagnosisGroupsDiagnosesFilter));
//        private string m_DisplayedText;
//        private long m_DisplayedValue;

//        public HumDiagnosisGroupsDiagnosesFilter()
//        {
//            InitializeComponent();
//            FilterType = HumDiagnosisGroupsType.DiagnosesAndGroupsHuman;
//            ShowCheckBoxes = false;

//            ValueChanged += HumDiagnosisGroupsDiagnosesFilter_ValueChanged;
//        }

//        public HumDiagnosisGroupsType FilterType { get; set; }

//        protected override string KeyColumnName
//        {
//            get { return "idfsDiagnosisOrDiagnosisGroup"; }
//        }

//        protected override string ValueColumnName
//        {
//            get { return "name"; }
//        }

//        protected override string ParentColumnName
//        {
//            get { return "idfsDiagnosisGroup"; }
//        }

//        protected override string SecondColumnName
//        {
//            get { return "strIDC10"; }
//        }

//        protected override string SecondColumnCaption
//        {
//            get { return EidssMessages.Get("colIDC10"); }
//        }

//        protected override string CheckedComboBoxName
//        {
//            get { return m_Resources.GetString("lblSingleDiagnosisLabel.Text"); }
//        }

//        public override string GetDisplayText()
//        {
//            return m_DisplayedText;
//        }

//        public long GetDisplayValue()
//        {
//            return m_DisplayedValue;
//        }

//        public override void SetValue(long? value)
//        {
//            m_DisplayedValue = value ?? m_DisplayedValue;
//            var rows = DataSource.Table.Select(string.Format("idfsDiagnosisOrDiagnosisGroup={0}", m_DisplayedValue));

//            if (value == 0)
//            {
//                m_DisplayedText = string.Empty;
//            }
//            else if (rows.Length>0)
//            {
//                m_DisplayedText = rows[0]["name"].ToString();
//            }

//            base.SetValue(value);
//        }

//        protected override void FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
//        {
//            if (ShowCheckBoxes || e.Node == null)
//            {
//                return;
//            }

//            m_DisplayedValue = (long) e.Node.GetValue(KeyColumnName);
//            base.FocusedNodeChanged(sender, e);
//        }

//        protected override DataView CreateDataSource()
//        {
//            DataView view = new DataView();

//            switch (FilterType)
//            {
//                case HumDiagnosisGroupsType.DiagnosesAndGroupsHuman:
//                    view = LookupCache.Get(LookupTables.DiagnosesAndGroupsHuman);
//                    break;
//                case HumDiagnosisGroupsType.DiagnosesAndGroupsHumanStandard:
//                    view = LookupCache.Get(LookupTables.DiagnosesAndGroupsHumanStandard);
//                    break;
//                case HumDiagnosisGroupsType.DiagnosesAndGroupsHumanGGComparative:
//                    view = LookupCache.Get(LookupTables.DiagnosesAndGroupsHumanGGComparative);
//                    break;
//            }

//            if (!view.Table.Rows.Contains(0))
//            {
//                //view.Table.Rows.Add(0, -1, "(" + EidssMessages.Get("Clear") + ")", "", "", 1, 0);
//            }

//            switch (FilterType)
//            {
//                case HumDiagnosisGroupsType.DiagnosesAndGroupsHuman:
//                    view.Sort = "blnGroup Desc, name";
//                    view.RowFilter = "intRowStatus <> 1";
//                    break;
//                case HumDiagnosisGroupsType.DiagnosesAndGroupsHumanStandard:
//                    view.Sort = "blnGroup Desc, name";
//                    view.RowFilter = "intRowStatus <> 1";
//                    break;
//                case HumDiagnosisGroupsType.DiagnosesAndGroupsHumanGGComparative:
//                    view.Sort = "intOrder, name";
//                    view.RowFilter = "intRowStatus <> 1";
//                    break;
//            }

//            return view;
//        }

//        private void HumDiagnosisGroupsDiagnosesFilter_ValueChanged(object sender, MultiFilterEventArgs e)
//        {
//            m_DisplayedText = e.TextResult;
//        }
//    }
//}