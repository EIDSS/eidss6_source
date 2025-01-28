using System.ComponentModel;
using System.Data;
using bv.common.db.Core;
using bv.common.Resources;
using eidss.model.Resources;

namespace EIDSS.Reports.BaseControls.Filters
{
    public sealed partial class ThaiProvinceDistrictsGroupsMultiFilter : GroupsMultiFilter
    {
        private readonly ComponentResourceManager m_Resources = new ComponentResourceManager(typeof (ThaiProvinceDistrictsGroupsMultiFilter));

        public ThaiProvinceDistrictsGroupsMultiFilter()
        {
            InitializeComponent();
            ShowCheckBoxes = true;
        }

        protected override string KeyColumnName
        {
            get { return "idfsProvinceOrDistrict"; }
        }

        protected override string ValueColumnName
        {
            get { return "name"; }
        }

        protected override string ParentColumnName
        {
            get { return "idfsRegion"; }
        }

        protected override string SecondColumnName
        {
            get { return "name"; }
        }

        protected override string SecondColumnCaption
        {
            get { return EidssMessages.Get("name"); }
        }

        protected override string CheckedComboBoxName
        {
            get { return m_Resources.GetString("lblcheckedComboBoxName.Text"); }
        }

        public override string GetDisplayText()
        {
            return m_GroupDisplayText;
        }

        protected override DataView CreateDataSource()
        {
            DataView view = LookupCache.Get(LookupTables.ProvinceDistrictTree);
            if (!view.Table.Rows.Contains(0))
            {
                view.Table.Rows.Add(0, -1, -1,  "(" + BvMessages.Get("strSelectAll_Id") + ")", 0, true, 0);
            }
            view.Sort = "intOrder, name";
            view.RowFilter = "intRowStatus <> 1";

            return view;
        }
    }
}