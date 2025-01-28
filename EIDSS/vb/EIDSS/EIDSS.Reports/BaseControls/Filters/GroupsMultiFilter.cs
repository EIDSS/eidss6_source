using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using bv.common.Core;
using bv.winclient.Core;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using eidss.model.Resources;
using bv.winclient.Layout;

namespace EIDSS.Reports.BaseControls.Filters
{
    public partial class GroupsMultiFilter : BaseFilter
    {
        /// <summary>
        ///     Fires immediately after lookup edit value has been changed
        /// </summary>
        public event EventHandler<MultiFilterEventArgs> ValueChanged;

        /// <summary>
        ///     Fires immediately after lookup edit value has been changed
        /// </summary>
        public event EventHandler<MultiFilterEventArgs> GroupAndIndividualValueChanged;

        private bool m_Checking;
        protected string m_GroupDisplayText = string.Empty;
        private readonly IDictionary<long, string> m_CheckedItems = new Dictionary<long, string>();
        private readonly IDictionary<long, string> m_CheckedGroupAndIndividualItems = new Dictionary<long, string>();

        protected GroupsMultiFilter()
        {
            InitializeComponent();
        }

        protected virtual string ParentColumnName
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

        protected virtual string SecondColumnName
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

        protected virtual string SecondColumnCaption
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

        protected virtual string CheckedComboBoxName
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected bool ShowCheckBoxes
        {
            get { return treeList.OptionsView.ShowCheckBoxes; }
            set { treeList.OptionsView.ShowCheckBoxes = value; }
        }

        public void ClearSelection()
        {
            using (CreateWaitDialog())
            {
                treeList.UncheckAll();
                m_CheckedItems.Clear();
                m_CheckedGroupAndIndividualItems.Clear();
                m_GroupDisplayText = string.Empty;
                // note: [ivan] don't remove this assignment. Otherwise lookup will not refresh the text.
                treeListLookUp.Text = " ";
                treeListLookUp.Text = string.Empty;
            }
        }

        public virtual void SetMandatory()
        {
            LayoutCorrector.SetStyleController(treeListLookUp, LayoutCorrector.MandatoryStyleController);
        }

        
        /// <summary>
        ///     Bind Datasouce to the Lookup Control and Customize Filter
        /// </summary>
        public override void DefineBinding()
        {
            using (CreateWaitDialog())
            {
                ResetDataSource();

                treeList.DataSource = DataSource;

                treeList.KeyFieldName = KeyColumnName;
                treeList.ParentFieldName = ParentColumnName;
                for (int i = 0; i < treeList.Columns.Count; i++)
                {
                    if (treeList.Columns[i].FieldName == ValueColumnName)
                    {
                        treeList.Columns[i].Caption = CheckedComboBoxName;
                    }
                    else if (treeList.Columns[i].FieldName == SecondColumnName)
                    {
                        treeList.Columns[i].Caption = SecondColumnCaption;
                    }
                    else
                    {
                        treeList.Columns[i].Visible = false;
                    }
                }
                //  treeList.OptionsView.ShowCheckBoxes = false;
                treeList.ExpandAll();

                treeListLookUp.Properties.PopupFormWidth = treeListLookUp.Width;

                treeList.NodesIterator.DoOperation(node =>
                {
                    var idItem = (long)node.GetValue(KeyColumnName);
                    if (m_CheckedItems.ContainsKey(idItem))
                    {
                        m_CheckedItems[idItem] = node.GetDisplayText("name");
                        node.Checked = true;
                    }
                });
                CalculateGroupDisplayText();
                treeListLookUp.Text = GetDisplayText();
                CalculateCheckedGroupAndIndividualItems();
                ApplyResources();
            }
        }
        protected override void ApplyResources()
        {
            base.ApplyResources();
            lblcheckedComboBoxName.Text = CheckedComboBoxName;
        }

        private void SetParentNodeState(TreeListNode node)
        {
            if (node.ParentNode != null)
            {
                if (node.ParentNode.Nodes.All(n => n.CheckState == CheckState.Checked))
                {
                    node.ParentNode.CheckState = CheckState.Checked;
                }
                else if (node.ParentNode.Nodes.All(n => n.CheckState == CheckState.Unchecked))
                {
                    node.ParentNode.CheckState = CheckState.Unchecked;
                }
                else
                {
                    node.ParentNode.CheckState = CheckState.Indeterminate;
                }
                SetParentNodeState(node.ParentNode);
            }
        }

        public virtual string GetDisplayText()
        {
            return string.Join(", ", m_CheckedItems.Values.ToArray());
        }

        public virtual string GetGroupAndIndividualDisplayText()
        {
            return string.Join(", ", m_CheckedGroupAndIndividualItems.Values.ToArray());
        }

        private void CustomDisplayText(object sender, CustomDisplayTextEventArgs e)
        {
            e.DisplayText = GetDisplayText();
        }

        public virtual void SetValue(long? value)
        {
            if (value == null)
            {
                return;
            }

            this.treeListLookUp.EditValue = value;
            this.treeListLookUp.RefreshEditValue();
        }

        private static WaitDialog CreateWaitDialog()
        {
            string title = EidssMessages.Get("msgPleaseWait");
            string caption = EidssMessages.Get("msgLoading");
            return new WaitDialog(caption, title);

        }
        private void AfterCheckNode(object sender, NodeEventArgs e)
        {
            if (m_Checking || !ShowCheckBoxes)
            {
                return;
            }
            WaitDialog wait = null;
            try
            {
                wait = CreateWaitDialog();

                m_Checking = true;

                treeList.BeginUpdate();
                if (e.Node.Checked)
                {
                    e.Node.CheckAll();
                }
                else
                {
                    e.Node.UncheckAll();
                }
                treeList.EndUpdate();

                SetParentNodeState(e.Node);
                var checkedNodes = treeList.GetAllCheckedNodes();
                m_CheckedItems.Clear();
                foreach (var node in checkedNodes)
                {
                    var idGroup = node.GetValue("blnGroup");
                    if (idGroup != DBNull.Value && !(bool)idGroup)
                    {
                        var idItem = (long)node.GetValue(KeyColumnName);
                        string name = node.GetDisplayText("name");

                        m_CheckedItems.Add(idItem, name);
                    }
                }

                CalculateCheckedGroupAndIndividualItems();

                CalculateGroupDisplayText();
                treeListLookUp.Text = GetDisplayText();
                treeListLookUp.Refresh();
                var args = new MultiFilterEventArgs(m_CheckedItems);
                ValueChanged.SafeRaise(sender, args);
                var groupAndIndividualArgs = new MultiFilterEventArgs(m_CheckedGroupAndIndividualItems);
                GroupAndIndividualValueChanged.SafeRaise(sender, groupAndIndividualArgs);
            }
            finally
            {
                if (wait != null)
                {
                    wait.Dispose();
                }
                m_Checking = false;
            }
        }

        protected virtual void FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            if (ShowCheckBoxes || e.Node == null)
            {
                return;
            }

            var idItem = (long) e.Node.GetValue(KeyColumnName);
            string name = e.Node.GetDisplayText("name");
            treeListLookUp.Text = name;
            m_CheckedItems.Clear();
            if (idItem > 0)
            {
                m_CheckedItems.Add(idItem, name);
            }
            CalculateGroupDisplayText();
            CalculateCheckedGroupAndIndividualItems();
            var args = new MultiFilterEventArgs(m_CheckedItems);
            ValueChanged.SafeRaise(sender, args);
            var groupAndIndividualArgs = new MultiFilterEventArgs(m_CheckedGroupAndIndividualItems);
            GroupAndIndividualValueChanged.SafeRaise(sender, groupAndIndividualArgs);
        }
        private void CalculateGroupDisplayText()
        {
            if (treeList != null)
            {
                List<TreeListNode> checkedNodes = treeList.GetAllCheckedNodes();

                List<string> items = new List<string>();
                foreach (var node in checkedNodes)
                {
                    if (node.HasChildren || (node.ParentNode != null && node.ParentNode.CheckState != CheckState.Checked))
                    {
                        string name = node.GetDisplayText("name");
                        items.Add(name);
                    }
                }
                items.Sort();
                m_GroupDisplayText = string.Join(", ", items.ToArray());
            }
        }

        private void CalculateCheckedGroupAndIndividualItems()
        {
            m_CheckedGroupAndIndividualItems.Clear();
            if (treeList != null)
            {
                List<TreeListNode> checkedNodes = treeList.GetAllCheckedNodes();

                foreach (var node in checkedNodes)
                {
                    if (node.HasChildren || (node.ParentNode != null && node.ParentNode.CheckState != CheckState.Checked))
                    {
                        var idItem = (long)node.GetValue(KeyColumnName);
                        var name = node.GetDisplayText("name");
                        m_CheckedGroupAndIndividualItems.Add(idItem, name);
                    }
                }
            }
        }

    }
}