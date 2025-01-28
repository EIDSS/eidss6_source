using System;
using System.ComponentModel;
using System.Data;
using bv.common.db;
using bv.common.db.Core;
using bv.winclient.Core;

namespace EIDSS.Reports.BaseControls.Filters
{
    public sealed partial class OutcomeLookupFilter : BaseLookupFilter
    {
        private readonly ComponentResourceManager m_Resources = new ComponentResourceManager(typeof (OutcomeLookupFilter));

        public OutcomeLookupFilter()
        {
            InitializeComponent();
        }

        protected override string KeyColumnName
        {
            get { return "idfsReference"; }
        }

        protected override string ValueColumnName
        {
            get { return "name"; }
        }

        protected override string LookupCaption
        {
            get { return lblLookupName.Text; }
        }

        protected override DataView CreateDataSource()
        {
            if (WinUtils.IsComponentInDesignMode(this))
            {
                return new DataView();
            }

            DataView dataSource = LookupCache.Get(BaseReferenceType.rftOutcome);
            if (dataSource == null)
            {
                throw new ApplicationException("Outcome Lookup is not filled");
            }
            return dataSource;

        }

        public override string ExternalLookupCaption
        {
            get { return lblLookupName.Text; }
            set
            {
                base.ExternalLookupCaption = value;
            }
        }

        protected override void ApplyResources()
        {
            base.ApplyResources();

            lblLookupName.Text = m_Resources.GetString("lblLookupName.Text");
        }
    }
}