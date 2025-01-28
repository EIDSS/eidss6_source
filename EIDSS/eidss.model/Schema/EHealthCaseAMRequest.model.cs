#pragma warning disable 0472,0414
using System;
using System.Text;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Xml.Serialization;
using BLToolkit.Aspects;
using BLToolkit.DataAccess;
using BLToolkit.EditableObjects;
using BLToolkit.Data;
using BLToolkit.Data.DataProvider;
using BLToolkit.Mapping;
using BLToolkit.Reflection;
using bv.common.Configuration;
using bv.common.Enums;
using bv.common.Core;
using bv.model.BLToolkit;
using bv.model.Model;
using bv.model.Helpers;
using bv.model.Model.Extenders;
using bv.model.Model.Core;
using bv.model.Model.Handlers;
using bv.model.Model.Validators;
using eidss.model.Core;
using eidss.model.Enums;
		

namespace eidss.model.Schema
{
        
        
    [XmlType(AnonymousType = true)]
    public abstract partial class EHealthCaseAMRequest : 
        EditableObject<EHealthCaseAMRequest>
        , IObject
        , IDisposable
        , ILookupUsage
        {
        
        [MapField(_str_idfEHealthCaseAMRequest), NonUpdatable, PrimaryKey]
        public abstract Int64? idfEHealthCaseAMRequest { get; set; }
                
        [LocalizedDisplayName(_str_idfPersonEnteredBy)]
        [MapField(_str_idfPersonEnteredBy)]
        public abstract Int64? idfPersonEnteredBy { get; set; }
        protected Int64? idfPersonEnteredBy_Original { get { return ((EditableValue<Int64?>)((dynamic)this)._idfPersonEnteredBy).OriginalValue; } }
        protected Int64? idfPersonEnteredBy_Previous { get { return ((EditableValue<Int64?>)((dynamic)this)._idfPersonEnteredBy).PreviousValue; } }
                
        [LocalizedDisplayName(_str_idfUserID)]
        [MapField(_str_idfUserID)]
        public abstract Int64? idfUserID { get; set; }
        protected Int64? idfUserID_Original { get { return ((EditableValue<Int64?>)((dynamic)this)._idfUserID).OriginalValue; } }
        protected Int64? idfUserID_Previous { get { return ((EditableValue<Int64?>)((dynamic)this)._idfUserID).PreviousValue; } }
                
        [LocalizedDisplayName(_str_LangID)]
        [MapField(_str_LangID)]
        public abstract String LangID { get; set; }
        protected String LangID_Original { get { return ((EditableValue<String>)((dynamic)this)._langID).OriginalValue; } }
        protected String LangID_Previous { get { return ((EditableValue<String>)((dynamic)this)._langID).PreviousValue; } }
                
        [LocalizedDisplayName(_str_xmlEHealthCaseAM)]
        [MapField(_str_xmlEHealthCaseAM)]
        public abstract String xmlEHealthCaseAM { get; set; }
        protected String xmlEHealthCaseAM_Original { get { return ((EditableValue<String>)((dynamic)this)._xmlEHealthCaseAM).OriginalValue; } }
        protected String xmlEHealthCaseAM_Previous { get { return ((EditableValue<String>)((dynamic)this)._xmlEHealthCaseAM).PreviousValue; } }
                
        [LocalizedDisplayName(_str_intRetCode)]
        [MapField(_str_intRetCode)]
        public abstract Int32? intRetCode { get; set; }
        protected Int32? intRetCode_Original { get { return ((EditableValue<Int32?>)((dynamic)this)._intRetCode).OriginalValue; } }
        protected Int32? intRetCode_Previous { get { return ((EditableValue<Int32?>)((dynamic)this)._intRetCode).PreviousValue; } }
                
        [LocalizedDisplayName(_str_strOutputComment)]
        [MapField(_str_strOutputComment)]
        public abstract String strOutputComment { get; set; }
        protected String strOutputComment_Original { get { return ((EditableValue<String>)((dynamic)this)._strOutputComment).OriginalValue; } }
        protected String strOutputComment_Previous { get { return ((EditableValue<String>)((dynamic)this)._strOutputComment).PreviousValue; } }
                
        #region Set/Get values
        #region filed_info definifion
        protected class field_info
        {
            internal string _name;
            internal string _formname;
            internal string _type;
            internal Func<EHealthCaseAMRequest, object> _get_func;
            internal Action<EHealthCaseAMRequest, string> _set_func;
            internal Action<EHealthCaseAMRequest, EHealthCaseAMRequest, CompareModel, DbManagerProxy> _compare_func;
        }
        internal const string _str_Parent = "Parent";
        internal const string _str_IsNew = "IsNew";
        
        internal const string _str_idfEHealthCaseAMRequest = "idfEHealthCaseAMRequest";
        internal const string _str_idfPersonEnteredBy = "idfPersonEnteredBy";
        internal const string _str_idfUserID = "idfUserID";
        internal const string _str_LangID = "LangID";
        internal const string _str_xmlEHealthCaseAM = "xmlEHealthCaseAM";
        internal const string _str_intRetCode = "intRetCode";
        internal const string _str_strOutputComment = "strOutputComment";
        internal const string _str_intDummy = "intDummy";
        internal const string _str_EHealthCaseAMItems = "EHealthCaseAMItems";
        private static readonly field_info[] _field_infos =
        {
                  
            new field_info {
              _name = _str_idfEHealthCaseAMRequest, _formname = _str_idfEHealthCaseAMRequest, _type = "Int64?",
              _get_func = o => o.idfEHealthCaseAMRequest,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseInt64Nullable(val); if (o.idfEHealthCaseAMRequest != newval) o.idfEHealthCaseAMRequest = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.idfEHealthCaseAMRequest != c.idfEHealthCaseAMRequest || o.IsRIRPropChanged(_str_idfEHealthCaseAMRequest, c)) 
                  m.Add(_str_idfEHealthCaseAMRequest, o.ObjectIdent + _str_idfEHealthCaseAMRequest, o.ObjectIdent2 + _str_idfEHealthCaseAMRequest, o.ObjectIdent3 + _str_idfEHealthCaseAMRequest, "Int64?", 
                    o.idfEHealthCaseAMRequest == null ? "" : o.idfEHealthCaseAMRequest.ToString(),                  
                  o.IsReadOnly(_str_idfEHealthCaseAMRequest), o.IsInvisible(_str_idfEHealthCaseAMRequest), o.IsRequired(_str_idfEHealthCaseAMRequest)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_idfPersonEnteredBy, _formname = _str_idfPersonEnteredBy, _type = "Int64?",
              _get_func = o => o.idfPersonEnteredBy,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseInt64Nullable(val); if (o.idfPersonEnteredBy != newval) o.idfPersonEnteredBy = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.idfPersonEnteredBy != c.idfPersonEnteredBy || o.IsRIRPropChanged(_str_idfPersonEnteredBy, c)) 
                  m.Add(_str_idfPersonEnteredBy, o.ObjectIdent + _str_idfPersonEnteredBy, o.ObjectIdent2 + _str_idfPersonEnteredBy, o.ObjectIdent3 + _str_idfPersonEnteredBy, "Int64?", 
                    o.idfPersonEnteredBy == null ? "" : o.idfPersonEnteredBy.ToString(),                  
                  o.IsReadOnly(_str_idfPersonEnteredBy), o.IsInvisible(_str_idfPersonEnteredBy), o.IsRequired(_str_idfPersonEnteredBy)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_idfUserID, _formname = _str_idfUserID, _type = "Int64?",
              _get_func = o => o.idfUserID,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseInt64Nullable(val); if (o.idfUserID != newval) o.idfUserID = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.idfUserID != c.idfUserID || o.IsRIRPropChanged(_str_idfUserID, c)) 
                  m.Add(_str_idfUserID, o.ObjectIdent + _str_idfUserID, o.ObjectIdent2 + _str_idfUserID, o.ObjectIdent3 + _str_idfUserID, "Int64?", 
                    o.idfUserID == null ? "" : o.idfUserID.ToString(),                  
                  o.IsReadOnly(_str_idfUserID), o.IsInvisible(_str_idfUserID), o.IsRequired(_str_idfUserID)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_LangID, _formname = _str_LangID, _type = "String",
              _get_func = o => o.LangID,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.LangID != newval) o.LangID = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.LangID != c.LangID || o.IsRIRPropChanged(_str_LangID, c)) 
                  m.Add(_str_LangID, o.ObjectIdent + _str_LangID, o.ObjectIdent2 + _str_LangID, o.ObjectIdent3 + _str_LangID, "String", 
                    o.LangID == null ? "" : o.LangID.ToString(),                  
                  o.IsReadOnly(_str_LangID), o.IsInvisible(_str_LangID), o.IsRequired(_str_LangID)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_xmlEHealthCaseAM, _formname = _str_xmlEHealthCaseAM, _type = "String",
              _get_func = o => o.xmlEHealthCaseAM,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.xmlEHealthCaseAM != newval) o.xmlEHealthCaseAM = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.xmlEHealthCaseAM != c.xmlEHealthCaseAM || o.IsRIRPropChanged(_str_xmlEHealthCaseAM, c)) 
                  m.Add(_str_xmlEHealthCaseAM, o.ObjectIdent + _str_xmlEHealthCaseAM, o.ObjectIdent2 + _str_xmlEHealthCaseAM, o.ObjectIdent3 + _str_xmlEHealthCaseAM, "String", 
                    o.xmlEHealthCaseAM == null ? "" : o.xmlEHealthCaseAM.ToString(),                  
                  o.IsReadOnly(_str_xmlEHealthCaseAM), o.IsInvisible(_str_xmlEHealthCaseAM), o.IsRequired(_str_xmlEHealthCaseAM)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_intRetCode, _formname = _str_intRetCode, _type = "Int32?",
              _get_func = o => o.intRetCode,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseInt32Nullable(val); if (o.intRetCode != newval) o.intRetCode = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.intRetCode != c.intRetCode || o.IsRIRPropChanged(_str_intRetCode, c)) 
                  m.Add(_str_intRetCode, o.ObjectIdent + _str_intRetCode, o.ObjectIdent2 + _str_intRetCode, o.ObjectIdent3 + _str_intRetCode, "Int32?", 
                    o.intRetCode == null ? "" : o.intRetCode.ToString(),                  
                  o.IsReadOnly(_str_intRetCode), o.IsInvisible(_str_intRetCode), o.IsRequired(_str_intRetCode)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_strOutputComment, _formname = _str_strOutputComment, _type = "String",
              _get_func = o => o.strOutputComment,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.strOutputComment != newval) o.strOutputComment = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.strOutputComment != c.strOutputComment || o.IsRIRPropChanged(_str_strOutputComment, c)) 
                  m.Add(_str_strOutputComment, o.ObjectIdent + _str_strOutputComment, o.ObjectIdent2 + _str_strOutputComment, o.ObjectIdent3 + _str_strOutputComment, "String", 
                    o.strOutputComment == null ? "" : o.strOutputComment.ToString(),                  
                  o.IsReadOnly(_str_strOutputComment), o.IsInvisible(_str_strOutputComment), o.IsRequired(_str_strOutputComment)); 
                  }
              }, 
        
            new field_info {
              _name = _str_intDummy, _formname = _str_intDummy, _type = "int",
              _get_func = o => o.intDummy,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseInt32(val); if (o.intDummy != newval) o.intDummy = newval; },
              _compare_func = (o, c, m, g) => {
              
                if (o.intDummy != c.intDummy || o.IsRIRPropChanged(_str_intDummy, c)) {
                  m.Add(_str_intDummy, o.ObjectIdent + _str_intDummy, o.ObjectIdent2 + _str_intDummy, o.ObjectIdent3 + _str_intDummy,  "int", 
                    o.intDummy == null ? "" : o.intDummy.ToString(),                  
                    o.IsReadOnly(_str_intDummy), o.IsInvisible(_str_intDummy), o.IsRequired(_str_intDummy));
                  }
                 }
              }, 
        
            new field_info {
              _name = _str_EHealthCaseAMItems, _formname = _str_EHealthCaseAMItems, _type = "Child",
              _get_func = o => null,
              _set_func = (o, val) => {
                  o.EHealthCaseAMItems.ForEach(c => c.SetValue("blnChecked", "false")); 
                  if (!string.IsNullOrEmpty(val)) 
                      val.Split(',').ToList().ForEach(i => o.EHealthCaseAMItems.First(c => (long)c.Key == Int64.Parse(i)).SetValue("blnChecked", "true"));
                  },
              _compare_func = (o, c, m, g) => {
                if (o.EHealthCaseAMItems.Count != c.EHealthCaseAMItems.Count || o.IsReadOnly(_str_EHealthCaseAMItems) != c.IsReadOnly(_str_EHealthCaseAMItems) || o.IsInvisible(_str_EHealthCaseAMItems) != c.IsInvisible(_str_EHealthCaseAMItems) || o.IsRequired(_str_EHealthCaseAMItems) != c._isRequired(o.m_isRequired, _str_EHealthCaseAMItems)) {
                  m.Add(_str_EHealthCaseAMItems, o.ObjectIdent + _str_EHealthCaseAMItems, o.ObjectIdent2 + _str_EHealthCaseAMItems, o.ObjectIdent3 + _str_EHealthCaseAMItems, "Child", o.idfEHealthCaseAMRequest == null ? "" : o.idfEHealthCaseAMRequest.ToString(), o.IsReadOnly(_str_EHealthCaseAMItems), o.IsInvisible(_str_EHealthCaseAMItems), o.IsRequired(_str_EHealthCaseAMItems)); 
                  }
                }
              }, 
        
            new field_info()
        };
        #endregion
        
        private string _getType(string name)
        {
            var i = _field_infos.FirstOrDefault(n => n._name == name);
            return i == null ? "" : i._type;
        }
        private object _getValue(string name)
        {
            var i = _field_infos.FirstOrDefault(n => n._name == name);
            return i == null ? null : i._get_func(this);
        }
        private void _setValue(string name, string val)
        {
            var i = _field_infos.FirstOrDefault(n => n._name == name);
            if (i != null) i._set_func(this, val);
        }
        internal CompareModel _compare(IObject o, CompareModel ret)
        {
            if (ret == null) ret = new CompareModel();
            if (o == null) return ret;
            EHealthCaseAMRequest obj = (EHealthCaseAMRequest)o;
            using (DbManagerProxy manager = DbManagerFactory.Factory.Create(ModelUserContext.Instance))
            {
                Accessor.Instance(null)._LoadLookups(manager, obj);
                foreach (var i in _field_infos)
                    if (i != null && i._compare_func != null) i._compare_func(this, obj, ret, manager);
            }
            return ret;
        }
        #endregion
    
        [LocalizedDisplayName(_str_EHealthCaseAMItems)]
        [Relation(typeof(EHealthCaseAM), eidss.model.Schema.EHealthCaseAM._str_idfEHealthCaseAMRequest, _str_idfEHealthCaseAMRequest)]
        public EditableList<EHealthCaseAM> EHealthCaseAMItems
        {
            get 
            {   
                return _EHealthCaseAMItems; 
            }
            set 
            {
                _EHealthCaseAMItems = value;
            }
        }
        protected EditableList<EHealthCaseAM> _EHealthCaseAMItems = new EditableList<EHealthCaseAM>();
                    
        private BvSelectList _getList(string name)
        {
        
            return null;
        }
    
          [LocalizedDisplayName(_str_intDummy)]
        public int intDummy
        {
            get { return m_intDummy; }
            set { if (m_intDummy != value) { m_intDummy = value; OnPropertyChanged(_str_intDummy); } }
        }
        private int m_intDummy;
        
        protected CacheScope m_CS;
        protected Accessor _getAccessor() { return Accessor.Instance(m_CS); }
        private IObjectPermissions m_permissions = null;
        internal IObjectPermissions _permissions { get { return m_permissions; } set { m_permissions = value; } }
        internal string m_ObjectName = "EHealthCaseAMRequest";

        #region Parent and Clone supporting
        [XmlIgnore]
        public IObject Parent
        {
            get { return m_Parent; }
            set { m_Parent = value; /*OnPropertyChanged(_str_Parent);*/ }
        }
        private IObject m_Parent;
        internal void _setParent()
        {
        EHealthCaseAMItems.ForEach(c => { c.Parent = this; });
                
        }
        partial void Cloned();
        partial void ClonedWithSetup();
        private bool bIsClone;
        public override object Clone()
        {
            var ret = base.Clone() as EHealthCaseAMRequest;
            ret.bIsClone = true;
            ret.Cloned();
            ret.m_Parent = this.Parent;
            ret.m_IsMarkedToDelete = this.m_IsMarkedToDelete;
            ret.m_IsForcedToDelete = this.m_IsForcedToDelete;
            ret._setParent();
            if (this.IsDirty && !ret.IsDirty)
                ret.SetChange();
            else if (!this.IsDirty && ret.IsDirty)
                ret.RejectChanges();
            return ret;
        }
        public IObject CloneWithSetup(DbManagerProxy manager, bool bRestricted = false)
        {
            var ret = base.Clone() as EHealthCaseAMRequest;
            ret.m_Parent = this.Parent;
            ret.m_IsMarkedToDelete = this.m_IsMarkedToDelete;
            ret.m_IsForcedToDelete = this.m_IsForcedToDelete;
            ret.m_IsNew = this.IsNew;
            ret.m_ObjectName = this.m_ObjectName;
        
            if (_EHealthCaseAMItems != null && _EHealthCaseAMItems.Count > 0)
            {
              ret.EHealthCaseAMItems.Clear();
              _EHealthCaseAMItems.ForEach(c => ret.EHealthCaseAMItems.Add(c.CloneWithSetup(manager, bRestricted)));
            }
                
            Accessor.Instance(null)._SetupLoad(manager, ret, true);
            ret.ClonedWithSetup();
            ret.DeepAcceptChanges();
            ret._setParent();
            ret._permissions = _permissions;
            return ret;
        }
        public EHealthCaseAMRequest CloneWithSetup()
        {
            using (DbManagerProxy manager = DbManagerFactory.Factory.Create(ModelUserContext.Instance))
            {
                return CloneWithSetup(manager) as EHealthCaseAMRequest;
            }
        }
        #endregion

        #region IObject implementation
        public object Key { get { return idfEHealthCaseAMRequest; } }
        public string KeyName { get { return "idfEHealthCaseAMRequest"; } }
        public object KeyLookup { get { return idfEHealthCaseAMRequest; } }
        public string ToStringProp { get { return ToString(); } }
        private bool m_IsNew;
        public bool IsNew { get { return m_IsNew; } }
        [XmlIgnore]
        [LocalizedDisplayName("HasChanges")]
        public bool HasChanges 
        { 
            get 
            { 
                return IsDirty
        
                    || EHealthCaseAMItems.IsDirty
                    || EHealthCaseAMItems.Count(c => c.IsMarkedToDelete || c.HasChanges) > 0
                
                ;
            }
        }
        public new void RejectChanges()
        {
        
            base.RejectChanges();
        
        }
        public void DeepRejectChanges()
        {
            RejectChanges();
        EHealthCaseAMItems.DeepRejectChanges();
                
        }
        public void DeepAcceptChanges()
        { 
            AcceptChanges();
        EHealthCaseAMItems.DeepAcceptChanges();
                
        }
        private bool m_bForceDirty;
        public override void AcceptChanges()
        {
            m_bForceDirty = false;
            base.AcceptChanges();
        }
        [XmlIgnore]
        [LocalizedDisplayName("IsDirty")]
        public override bool IsDirty
        {
            get { return m_bForceDirty || base.IsDirty; }
        }
        public void SetChange()
        { 
            m_bForceDirty = true;
        }
        public void DeepSetChange()
        { 
            SetChange();
        EHealthCaseAMItems.ForEach(c => c.SetChange());
                
        }
        public bool MarkToDelete() { return _Delete(false); }
        public string ObjectName { get { return m_ObjectName; } }
        public string ObjectIdent { get { return ObjectName + "_" + Key.ToString() + "_"; } }
        
        public string ObjectIdent2 { get { return ObjectIdent; } }
          
        public string ObjectIdent3 { get { return ObjectIdent; } }
          
        public IObjectAccessor GetAccessor() { return _getAccessor(); }
        public IObjectPermissions GetPermissions() { return _permissions; }
        private IObjectEnvironment _environment;
        public IObjectEnvironment Environment { get { return _environment; } set { _environment = value; } }
        public bool IsValidObject { get { return _isValid; } }
        public bool ReadOnly { get { return _readOnly || !_isValid; } set { _readOnly = value; } }
        public bool IsReadOnly(string name) { return _isReadOnly(name); }
        public bool IsInvisible(string name) { return _isInvisible(name); }
        public bool IsRequired(string name) { return _isRequired(m_isRequired, name); }
        public bool IsHiddenPersonalData(string name) { return _isHiddenPersonalData(name); }
        public string GetType(string name) { return _getType(name); }
        public object GetValue(string name) { return _getValue(name); }
        public void SetValue(string name, string val) { _setValue(name, val); }
        public CompareModel Compare(IObject o) { return _compare(o, null); }
        public BvSelectList GetList(string name) { return _getList(name); }
        public event ValidationEvent Validation;
        public event ValidationEvent ValidationEnd;
        public event AfterPostEvent AfterPost;
      
        public Dictionary<string, string> GetFieldTags(string name)
        {
          return null;
        }
      #endregion

        private bool IsRIRPropChanged(string fld, EHealthCaseAMRequest c)
        {
            return IsReadOnly(fld) != c.IsReadOnly(fld) || IsInvisible(fld) != c.IsInvisible(fld) || IsRequired(fld) != c._isRequired(m_isRequired, fld);
        }
        private bool IsLookupContentChanged(DbManagerProxy manager, string fld, EHealthCaseAMRequest c)
        {
            var thisLookup = GetValue(fld + "Lookup") as IList;
            var thatLookup = c.GetValue(fld + "Lookup") as IList;
            bool bChangeLookupContent = thisLookup.Count != thatLookup.Count;
            if (!bChangeLookupContent)
            {
                for (int i = 0; i < thisLookup.Count; i++)
                {
                    if (((thisLookup[i] as IObject).Key as IComparable).CompareTo((thatLookup[i] as IObject).Key) != 0 &&
                        (thisLookup[i] as IObject).ToStringProp != null && ((thisLookup[i] as IObject).ToStringProp as IComparable).CompareTo((thatLookup[i] as IObject).ToStringProp) != 0)
                    {
                        bChangeLookupContent = true;
                        break;
                    }
                }
            }
            return bChangeLookupContent;
        }
        

      

        public EHealthCaseAMRequest()
        {
            
        }

        partial void Changed(string fieldName);
        partial void Created(DbManagerProxy manager);
        partial void Loaded(DbManagerProxy manager);
        partial void Deleted();
        partial void ParsedFormCollection(NameValueCollection form);
        partial void ParsingFormCollection(NameValueCollection form);

        

        private bool m_IsForcedToDelete;
        [LocalizedDisplayName("IsForcedToDelete")]
        public bool IsForcedToDelete { get { return m_IsForcedToDelete; } }

        private bool m_IsMarkedToDelete;
        [LocalizedDisplayName("IsMarkedToDelete")]
        public bool IsMarkedToDelete { get { return m_IsMarkedToDelete; } }

        public void _SetupMainHandler()
        {
            PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(EHealthCaseAMRequest_PropertyChanged);
        }
        public void _RevokeMainHandler()
        {
            PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(EHealthCaseAMRequest_PropertyChanged);
        }
        private void EHealthCaseAMRequest_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            (sender as EHealthCaseAMRequest).Changed(e.PropertyName);
            
        }
        
        public bool ForceToDelete() { return _Delete(true); }
        internal bool _Delete(bool isForceDelete)
        {
            if (!_ValidateOnDelete()) return false;
            _DeletingExtenders();
            m_IsMarkedToDelete = true;
            m_IsForcedToDelete = m_IsForcedToDelete ? m_IsForcedToDelete : isForceDelete;
            OnPropertyChanged("IsMarkedToDelete");
            _DeletedExtenders();
            Deleted();
            return true;
        }
        private bool _ValidateOnDelete(bool bReport = true)
        {
            
            return true;                
              
        }
        private void _DeletingExtenders()
        {
            EHealthCaseAMRequest obj = this;
            
        }
        private void _DeletedExtenders()
        {
            EHealthCaseAMRequest obj = this;
            
        }
        
        public bool OnValidation(ValidationModelException ex)
        {
            if (Validation != null)
            {
                var args = new ValidationEventArgs(ex.MessageId, ex.FieldName, ex.PropertyName, ex.Pars, ex.ValidatorType, ex.Obj, ex.ValidationType);
                Validation(this, args);
                return args.Continue;
            }
            return false;
        }
        public bool OnValidationEnd(ValidationModelException ex)
        {
            if (ValidationEnd != null)
            {
                var args = new ValidationEventArgs(ex.MessageId, ex.FieldName, ex.PropertyName, ex.Pars, ex.ValidatorType, ex.Obj, ex.ValidationType);
                ValidationEnd(this, args);
                return args.Continue;
            }
            return false;
        }

        public void OnAfterPost()
        {
            if (AfterPost != null)
            {
                AfterPost(this, EventArgs.Empty);
            }
        }

        public FormSize FormSize
        {
            get { return FormSize.Undefined; }
        }
    
        private bool _isInvisible(string name)
        {
            
            return false;
                
        }

    
        private bool _isReadOnly(string name)
        {
            
            return ReadOnly;
                
        }

        private bool m_isValid = true;
        internal bool _isValid
        {
            get { return m_isValid; }
            set
            {
                m_isValid = value;
        
                foreach(var o in _EHealthCaseAMItems)
                    o._isValid &= value;
                
            }
        }

        private bool m_readOnly;
        private bool _readOnly
        {
            get { return m_readOnly; }
            set
            {
                m_readOnly = value;
        
                foreach(var o in _EHealthCaseAMItems)
                    o.ReadOnly |= value;
                
            }
        }


        internal Dictionary<string, Func<EHealthCaseAMRequest, bool>> m_isRequired;
        private bool _isRequired(Dictionary<string, Func<EHealthCaseAMRequest, bool>> isRequiredDict, string name)
        {
            if (isRequiredDict != null && isRequiredDict.ContainsKey(name))
                return isRequiredDict[name](this);
            return false;
        }
        
        public void AddRequired(string name, Func<EHealthCaseAMRequest, bool> func)
        {
            if (m_isRequired == null) 
                m_isRequired = new Dictionary<string, Func<EHealthCaseAMRequest, bool>>();
            if (!m_isRequired.ContainsKey(name))
                m_isRequired.Add(name, func);
        }
    
    internal Dictionary<string, Func<EHealthCaseAMRequest, bool>> m_isHiddenPersonalData;
    private bool _isHiddenPersonalData(string name)
    {
      if (m_isHiddenPersonalData != null && m_isHiddenPersonalData.ContainsKey(name))
        return m_isHiddenPersonalData[name](this);
      return false;
    }

    public void AddHiddenPersonalData(string name, Func<EHealthCaseAMRequest, bool> func)
    {
      if (m_isHiddenPersonalData == null)
        m_isHiddenPersonalData = new Dictionary<string, Func<EHealthCaseAMRequest, bool>>();
      if (!m_isHiddenPersonalData.ContainsKey(name))
        m_isHiddenPersonalData.Add(name, func);
    }
  
        #region IDisposable Members
        partial void Disposed();
        private bool bIsDisposed;
        protected bool bNeedLookupRemove;
        ~EHealthCaseAMRequest()
        {
            Dispose();
        }
        public void Dispose()
        {
            if (!bIsDisposed) 
            {
                bIsDisposed = true;
                m_Parent = null;
                m_permissions = null;
                
                this.ClearModelObjEventInvocations();
                
                if (!bIsClone)
                {
                    EHealthCaseAMItems.ForEach(c => c.Dispose());
                }
                EHealthCaseAMItems.ClearModelListEventInvocations();
                
                
                if (bNeedLookupRemove)
                {
                
                }
                Disposed();
            }
            GC.SuppressFinalize(this);
        }
        #endregion
    
        #region ILookupUsage Members
        public void ReloadLookupItem(DbManagerProxy manager, string lookup_object)
        {
            
        }
        #endregion
    
        public void ParseFormCollection(NameValueCollection form, bool bParseLookups = true, bool bParseRelations = true)
        {
            ParsingFormCollection(form);
            if (bParseLookups)
            {
                _field_infos.Where(i => i._type == "Lookup").ToList().ForEach(a => { if (form[ObjectIdent + a._formname] != null) a._set_func(this, form[ObjectIdent + a._formname]);} );
            }
            
            _field_infos.Where(i => i._type != "Lookup" && i._type != "Child" && i._type != "Relation" && i._type != null)
                .ToList().ForEach(a => { if (form.AllKeys.Contains(ObjectIdent + a._formname)) a._set_func(this, form[ObjectIdent + a._formname]);} );
      
            if (bParseRelations)
            {
        
            if (_EHealthCaseAMItems != null) _EHealthCaseAMItems.ForEach(c => c.ParseFormCollection(form, bParseLookups, bParseRelations));
                
            }
            ParsedFormCollection(form);
        }
    

        #region Accessor
        public abstract partial class Accessor
        : DataAccessor<EHealthCaseAMRequest>
            , IObjectAccessor
            , IObjectMeta
            , IObjectValidator
            
            , IObjectCreator
            , IObjectCreator<EHealthCaseAMRequest>
            
            , IObjectSelectDetail
            , IObjectSelectDetail<EHealthCaseAMRequest>
            , IObjectPost
            , IObjectDelete
                    
        {
            #region IObjectAccessor
            public string KeyName { get { return "idfEHealthCaseAMRequest"; } }
            #endregion
        
            public delegate void on_action(EHealthCaseAMRequest obj);
            private static Accessor g_Instance = CreateInstance<Accessor>();
            private CacheScope m_CS;
            public static Accessor Instance(CacheScope cs) 
            { 
                if (cs == null)
                    return g_Instance;
                lock(cs)
                {
                    object acc = cs.Get(typeof (Accessor));
                    if (acc != null)
                    {
                        return acc as Accessor;
                    }
                    Accessor ret = CreateInstance<Accessor>();
                    ret.m_CS = cs;
                    cs.Add(typeof(Accessor), ret);
                    return ret;
                }
            }
            private EHealthCaseAM.Accessor EHealthCaseAMItemsAccessor { get { return eidss.model.Schema.EHealthCaseAM.Accessor.Instance(m_CS); } }
            

            public virtual IObject SelectDetail(DbManagerProxy manager, object ident, int? HACode = null)
            {
                if (ident == null)
                {
                    return CreateNew(manager, null, HACode);
                }
                else
                {
                    return _SelectByKey(manager
                        , (Int64?)ident
                            
                        , null, null
                        );
                }
            }
            public virtual EHealthCaseAMRequest SelectDetailT(DbManagerProxy manager, object ident, int? HACode = null)
            {
                if (ident == null)
                {
                    return CreateNewT(manager, null, HACode);
                }
                else
                {
                    return _SelectByKey(manager
                        , (Int64?)ident
                            
                        , null, null
                        );
                }
            }

            
            public virtual EHealthCaseAMRequest SelectByKey(DbManagerProxy manager
                , Int64? idfEHealthCaseAMRequest
                )
            {
                return _SelectByKey(manager
                    , idfEHealthCaseAMRequest
                    , null, null
                    );
            }
            

            private EHealthCaseAMRequest _SelectByKey(DbManagerProxy manager
                , Int64? idfEHealthCaseAMRequest
                , on_action loading, on_action loaded
                )
            {
                return _SelectByKeyInternal(manager
                    , idfEHealthCaseAMRequest
                    , loading, loaded
                    )
                    
                    ;
            }
      
            
            protected virtual EHealthCaseAMRequest _SelectByKeyInternal(DbManagerProxy manager
                , Int64? idfEHealthCaseAMRequest
                , on_action loading, on_action loaded
                )
            {
            
                MapResultSet[] sets = new MapResultSet[1];
                List<EHealthCaseAMRequest> objs = new List<EHealthCaseAMRequest>();
                sets[0] = new MapResultSet(typeof(EHealthCaseAMRequest), objs);
                EHealthCaseAMRequest obj = null;
                try
                {
                    manager
                        .SetSpCommand("spEHealthCaseAMRequest_SelectDetail"
                            , manager.Parameter("@idfEHealthCaseAMRequest", idfEHealthCaseAMRequest)
                            )
                        .ExecuteResultSet(sets);

                    if (objs.Count == 0)
                        return null;

                    obj = objs[0];
                    obj.m_CS = m_CS;
                    
                  
                    if (loading != null)
                        loading(obj);
                    _SetupLoad(manager, obj);
                    

                    //obj._setParent();
                    if (loaded != null)
                    loaded(obj);
                    obj.Loaded(manager);
                    return obj;
                  }
                  catch(DataException e)
                  {
                    throw DbModelException.Create(obj, e);
                  }
                
            }
    
            private void _SetupAddChildHandlerEHealthCaseAMItems(EHealthCaseAMRequest obj)
            {
                obj.EHealthCaseAMItems.CollectionChanged += delegate(object sender, NotifyCollectionChangedEventArgs e)
                {
                    if (e.Action == NotifyCollectionChangedAction.Add)
                    {
                        foreach(var o in e.NewItems)
                        {
                            _SetupChildHandlers(obj, o);
                        }
                    }
                };
            }
            
            internal void _LoadEHealthCaseAMItems(EHealthCaseAMRequest obj)
            {
                using (DbManagerProxy manager = DbManagerFactory.Factory.Create(ModelUserContext.Instance))
                {
                    _LoadEHealthCaseAMItems(manager, obj);
                }
            }
            internal void _LoadEHealthCaseAMItems(DbManagerProxy manager, EHealthCaseAMRequest obj)
            {
              
              }
            
        
        
            internal void _SetupLoad(DbManagerProxy manager, EHealthCaseAMRequest obj, bool bCloning = false)
            {
                if (obj == null) return;
                
                // loading extenters - begin
                // loading extenters - end
                if (!bCloning)
                {
                
                _LoadEHealthCaseAMItems(manager, obj);
                }
                _LoadLookups(manager, obj);
                obj._setParent();
                
                // loaded extenters - begin
                // loaded extenters - end
                
                _SetupHandlers(obj);
                _SetupChildHandlers(obj, null);
                
                _SetupAddChildHandlerEHealthCaseAMItems(obj);
                
                _SetPermitions(obj._permissions, obj);
                _SetupRequired(obj);
                _SetupPersonalDataRestrictions(obj);
                obj._SetupMainHandler();
                obj.AcceptChanges();
            }
            
            internal void _SetPermitions(IObjectPermissions permissions, EHealthCaseAMRequest obj)
            {
                if (obj != null)
                {
                    obj._permissions = permissions;
                    if (obj._permissions != null)
                    {
                    
                        obj.EHealthCaseAMItems.ForEach(c => EHealthCaseAMItemsAccessor._SetPermitions(obj._permissions, c));
                    
                    }
                }
            }

    

            private EHealthCaseAMRequest _CreateNew(DbManagerProxy manager, IObject Parent, int? HACode, on_action creating, on_action created, bool isFake = false)
            {
                EHealthCaseAMRequest obj = null;
                try
                {
                    obj = EHealthCaseAMRequest.CreateInstance();
                    obj.m_CS = m_CS;
                    obj.m_IsNew = true;
                    obj.Parent = Parent;
                    
                    if (creating != null)
                        creating(obj);
                
                    // creating extenters - begin
              obj = SelectByKey(manager, GetNewIDExtender<EHealthCaseAMRequest>.getNewId(manager));
              obj.EHealthCaseAMItems.Clear();
            
                    // creating extenters - end
                
                    _LoadLookups(manager, obj);
                    _SetupHandlers(obj);
                    _SetupChildHandlers(obj, null);
                    
                    _SetupAddChildHandlerEHealthCaseAMItems(obj);
                    
                    obj._SetupMainHandler();
                    obj._setParent();
                
                    // created extenters - begin
              if (EidssUserContext.Instance != null)
                if (EidssUserContext.User != null)
                {                             
                  if (EidssUserContext.User.EmployeeID != null)
                  {
                    long em;
                    if (long.TryParse(EidssUserContext.User.EmployeeID.ToString(), out em))
                      obj.idfPersonEnteredBy = em;
                  }
                  if (EidssUserContext.User.ID != null)
                  {
                    long em;
                    if (long.TryParse(EidssUserContext.User.ID.ToString(), out em))
                      obj.idfUserID = em;
                  }
                }                        
            
                    // created extenters - end
        
                    if (created != null)
                        created(obj);
                    obj.Created(manager);
                    _SetPermitions(obj._permissions, obj);
                    _SetupRequired(obj);
                    _SetupPersonalDataRestrictions(obj);
                    return obj;
                }
                catch(DataException e)
                {
                    throw DbModelException.Create(obj, e);
                }
            }

            
            public EHealthCaseAMRequest CreateNewT(DbManagerProxy manager, IObject Parent, int? HACode = null)
            {
                return _CreateNew(manager, Parent, HACode, null, null);
            }
            public IObject CreateNew(DbManagerProxy manager, IObject Parent, int? HACode = null)
            {
                return _CreateNew(manager, Parent, HACode, null, null);
            }
            public EHealthCaseAMRequest CreateFakeT(DbManagerProxy manager, IObject Parent, int? HACode = null)
            {
                return _CreateNew(manager, Parent, HACode, null, null, true);
            }
            public IObject CreateFake(DbManagerProxy manager, IObject Parent, int? HACode = null)
            {
                return _CreateNew(manager, Parent, HACode, null, null, true);
            }
            
            public EHealthCaseAMRequest CreateWithParamsT(DbManagerProxy manager, IObject Parent, List<object> pars)
            {
                return _CreateNew(manager, Parent, null, null, null);
            }
            public IObject CreateWithParams(DbManagerProxy manager, IObject Parent, List<object> pars)
            {
                return _CreateNew(manager, Parent, null, null, null);
            }
            
            private void _SetupChildHandlers(EHealthCaseAMRequest obj, object newobj)
            {
                
            }
            
            private void _SetupHandlers(EHealthCaseAMRequest obj)
            {
                
            }
    

            internal void _LoadLookups(DbManagerProxy manager, EHealthCaseAMRequest obj)
            {
                
            }
    
            public bool DeleteObject(DbManagerProxy manager, object ident)
            {
                IObject obj = SelectDetail(manager, ident);
                if (!obj.MarkToDelete()) return false;
                return Post(manager, obj);
            }
        
            [SprocName("spEHealthCaseAMRequest_Post")]
            protected abstract void _post(DbManagerProxy manager, string LangID, 
                [Direction.InputOutput("intRetCode", "strOutputComment")] EHealthCaseAMRequest obj);
            protected void _postPredicate(DbManagerProxy manager, string LangID, 
                [Direction.InputOutput("intRetCode", "strOutputComment")] EHealthCaseAMRequest obj)
            {
                
                _post(manager, LangID, obj);
                
            }
            
            
            public bool Post(DbManagerProxy manager, IObject obj, bool bChildObject = false)
            {
                bool bSuccess = false;
                int iDeadlockAttemptsCount = BaseSettings.DeadlockAttemptsCount;
                for (int iAttemptNumber = 0; iAttemptNumber < iDeadlockAttemptsCount; iAttemptNumber++)
                {
                    bool bTransactionStarted = false;
                    try
                    {
                        EHealthCaseAMRequest bo = obj as EHealthCaseAMRequest;
                        
                        if (!bo.IsNew && bo.IsMarkedToDelete) // delete
                        {
                        }
                        else if (bo.IsNew && !bo.IsMarkedToDelete) // insert
                        {
                            
                        }
                        else if (!bo.IsMarkedToDelete) // update
                        {
                            
                        }

                        if (!manager.IsTransactionStarted)
                        {
                            
                            bTransactionStarted = true;
                            manager.BeginTransaction();
                        }
                        bSuccess = _PostNonTransaction(manager, obj as EHealthCaseAMRequest, bChildObject);
                        if (bTransactionStarted)
                        {
                            if (bSuccess)
                            {
                                obj.DeepAcceptChanges();
                                manager.CommitTransaction();
                                
                            }
                            else
                            {
                                manager.RollbackTransaction();
                            }
                            
                        }
                        if (bSuccess && bo.IsNew && !bo.IsMarkedToDelete) // insert
                        {
                            bo.m_IsNew = false;
                        }
                        if (bSuccess && bTransactionStarted)
                        {
                            bo.OnAfterPost();
                        }
                        
                        break;
                    }
                    catch(Exception e)
                    {
                        if (bTransactionStarted)
                        {
                            manager.RollbackTransaction();
                            
                            if (DbModelException.IsDeadlockException(e))
                            {
                                System.Threading.Thread.Sleep(BaseSettings.DeadlockDelay);
                                continue;
                            }
                        }
                    
                        if (e is DataException)
                        {
                            throw DbModelException.Create(obj, e as DataException);
                        }
                        if (e is System.Data.SqlClient.SqlException)
                        {
                            throw DbModelException.Create(obj, e as System.Data.SqlClient.SqlException);
                        }
                        else 
                            throw;
                    }
                }
                return bSuccess;
            }
            private bool _PostNonTransaction(DbManagerProxy manager, EHealthCaseAMRequest obj, bool bChildObject) 
            { 
                bool bHasChanges = obj.HasChanges;
                if (!obj.IsNew && obj.IsMarkedToDelete) // delete
                {
            
                    if (!ValidateCanDelete(manager, obj)) return false;
                                        
                }
                else if (!obj.IsMarkedToDelete) // insert/update
                {
                    if (!bChildObject)
                        if (!Validate(manager, obj, true, true, true)) 
                            return false;
                    
            
                    // posting extenters - begin
                obj.xmlEHealthCaseAM = new Func<EHealthCaseAMRequest, string>(c => c.Serialize())(obj);
                    // posting extenters - end
            
                    if (!obj.IsMarkedToDelete && bHasChanges)
                        _postPredicate(manager, ModelUserContext.CurrentLanguage, obj);
                                    
                    // posted extenters - begin
                    // posted extenters - end
            
                }
                //obj.AcceptChanges();
                
                return true;
            }
            
            public bool ValidateCanDelete(EHealthCaseAMRequest obj)
            {
                using (DbManagerProxy manager = DbManagerFactory.Factory.Create(ModelUserContext.Instance))
                {
                    return ValidateCanDelete(manager, obj);
                }
            }
            public bool ValidateCanDelete(DbManagerProxy manager, EHealthCaseAMRequest obj)
            {
            
                return true;
            }
        
      
            protected ValidationModelException ChainsValidate(EHealthCaseAMRequest obj)
            {
                
                return null;
            }
            protected bool ChainsValidate(EHealthCaseAMRequest obj, bool bRethrowException)
            {
                ValidationModelException ex = ChainsValidate(obj);
                if (ex != null)
                {
                    if (bRethrowException)
                        throw ex;
                    if (!obj.OnValidation(ex))
                    {
                        obj.OnValidationEnd(ex);
                        return false;
                    }
                }
                return true;
            }
        
            public bool Validate(DbManagerProxy manager, IObject obj, bool bPostValidation, bool bChangeValidation, bool bDeepValidation, bool bRethrowException = false)
            {
                return Validate(manager, obj as EHealthCaseAMRequest, bPostValidation, bChangeValidation, bDeepValidation, bRethrowException);
            }
            public bool Validate(DbManagerProxy manager, EHealthCaseAMRequest obj, bool bPostValidation, bool bChangeValidation, bool bDeepValidation, bool bRethrowException = false)
            {
                if (!ChainsValidate(obj, bRethrowException))
                    return false;
                    
                
                return true;
            }
           
    
            private void _SetupRequired(EHealthCaseAMRequest obj)
            {
            
            }
    
    private void _SetupPersonalDataRestrictions(EHealthCaseAMRequest obj)
    {
    
    
    }
  
            #region IObjectMeta
            public int? MaxSize(string name) { return Meta.Sizes.ContainsKey(name) ? (int?)Meta.Sizes[name] : null; }
            public bool RequiredByField(string name, IObject obj) { return Meta.RequiredByField.ContainsKey(name) ? Meta.RequiredByField[name](obj as EHealthCaseAMRequest) : false; }
            public bool RequiredByProperty(string name, IObject obj) { return Meta.RequiredByProperty.ContainsKey(name) ? Meta.RequiredByProperty[name](obj as EHealthCaseAMRequest) : false; }
            public List<SearchPanelMetaItem> SearchPanelMeta { get { return Meta.SearchPanelMeta; } }
            public List<GridMetaItem> GridMeta { get { return Meta.GridMeta; } }
            public List<ActionMetaItem> Actions { get { return Meta.Actions; } }
            public string DetailPanel { get { return "EHealthCaseAMRequestDetail"; } }
            public string HelpIdWin { get { return ""; } }
            public string HelpIdWeb { get { return ""; } }
            public string HelpIdHh { get { return ""; } }
            public string SqlSortOrder { get { return Meta.sqlSortOrder; } }
            #endregion
    
        }

        
        #region Meta
        public static class Meta
        {
            public static string spSelect = "spEHealthCaseAMRequest_SelectDetail";
            public static string spCount = "";
            public static string spPost = "spEHealthCaseAMRequest_Post";
            public static string spInsert = "";
            public static string spUpdate = "";
            public static string spDelete = "";
            public static string spCanDelete = "";
            public static string sqlSortOrder = "";
            public static Dictionary<string, int> Sizes = new Dictionary<string, int>();
            public static Dictionary<string, Func<EHealthCaseAMRequest, bool>> RequiredByField = new Dictionary<string, Func<EHealthCaseAMRequest, bool>>();
            public static Dictionary<string, Func<EHealthCaseAMRequest, bool>> RequiredByProperty = new Dictionary<string, Func<EHealthCaseAMRequest, bool>>();
            public static List<SearchPanelMetaItem> SearchPanelMeta = new List<SearchPanelMetaItem>();
            public static List<GridMetaItem> GridMeta = new List<GridMetaItem>();
            public static List<ActionMetaItem> Actions = new List<ActionMetaItem>();
            
            
    private static Dictionary<string, List<Func<bool>>> m_isHiddenPersonalData = new Dictionary<string, List<Func<bool>>>();
    internal static bool _isHiddenPersonalData(string name)
    {
      if (m_isHiddenPersonalData.ContainsKey(name))
          return m_isHiddenPersonalData[name].Aggregate(false, (s,c) => s | c());
      return false;
    }

    private static void AddHiddenPersonalData(string name, Func<bool> func)
    {
      if (!m_isHiddenPersonalData.ContainsKey(name))
          m_isHiddenPersonalData.Add(name, new List<Func<bool>>());
      m_isHiddenPersonalData[name].Add(func);
    }
  
            
            static Meta()
            {
                
                Sizes.Add(_str_LangID, 50);
                Sizes.Add(_str_xmlEHealthCaseAM, 2147483647);
                Sizes.Add(_str_strOutputComment, 2147483647);
        
                _SetupPersonalDataRestrictions();
            }
            
            
    private static void _SetupPersonalDataRestrictions()
    {
    

    }
  
        }
        #endregion
    

        #endregion
        }
    
}
	