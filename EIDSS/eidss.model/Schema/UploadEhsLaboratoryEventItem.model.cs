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
    public abstract partial class UploadEhsLaboratoryEventItem : 
        EditableObject<UploadEhsLaboratoryEventItem>
        , IObject
        , IDisposable
        , ILookupUsage
        {
        
        [MapField(_str_idfsUploadEhs), NonUpdatable, PrimaryKey]
        public abstract Int64 idfsUploadEhs { get; set; }
                
        [LocalizedDisplayName(_str_idfsUploadEhsLaboratoryEventItem)]
        [MapField(_str_idfsUploadEhsLaboratoryEventItem)]
        public abstract Int64 idfsUploadEhsLaboratoryEventItem { get; set; }
        protected Int64 idfsUploadEhsLaboratoryEventItem_Original { get { return ((EditableValue<Int64>)((dynamic)this)._idfsUploadEhsLaboratoryEventItem).OriginalValue; } }
        protected Int64 idfsUploadEhsLaboratoryEventItem_Previous { get { return ((EditableValue<Int64>)((dynamic)this)._idfsUploadEhsLaboratoryEventItem).PreviousValue; } }
                
        [LocalizedDisplayName(_str_patient_id)]
        [MapField(_str_patient_id)]
        public abstract String patient_id { get; set; }
        protected String patient_id_Original { get { return ((EditableValue<String>)((dynamic)this)._patient_id).OriginalValue; } }
        protected String patient_id_Previous { get { return ((EditableValue<String>)((dynamic)this)._patient_id).PreviousValue; } }
                
        [LocalizedDisplayName(_str_code)]
        [MapField(_str_code)]
        public abstract String code { get; set; }
        protected String code_Original { get { return ((EditableValue<String>)((dynamic)this)._code).OriginalValue; } }
        protected String code_Previous { get { return ((EditableValue<String>)((dynamic)this)._code).PreviousValue; } }
                
        [LocalizedDisplayName(_str_value)]
        [MapField(_str_value)]
        public abstract String value { get; set; }
        protected String value_Original { get { return ((EditableValue<String>)((dynamic)this)._value).OriginalValue; } }
        protected String value_Previous { get { return ((EditableValue<String>)((dynamic)this)._value).PreviousValue; } }
                
        [LocalizedDisplayName(_str_primary_source)]
        [MapField(_str_primary_source)]
        public abstract String primary_source { get; set; }
        protected String primary_source_Original { get { return ((EditableValue<String>)((dynamic)this)._primary_source).OriginalValue; } }
        protected String primary_source_Previous { get { return ((EditableValue<String>)((dynamic)this)._primary_source).PreviousValue; } }
                
        [LocalizedDisplayName(_str_issued)]
        [MapField(_str_issued)]
        public abstract DateTime? issued { get; set; }
        protected DateTime? issued_Original { get { return ((EditableValue<DateTime?>)((dynamic)this)._issued).OriginalValue; } }
        protected DateTime? issued_Previous { get { return ((EditableValue<DateTime?>)((dynamic)this)._issued).PreviousValue; } }
                
        [LocalizedDisplayName(_str_inserted_at)]
        [MapField(_str_inserted_at)]
        public abstract DateTime? inserted_at { get; set; }
        protected DateTime? inserted_at_Original { get { return ((EditableValue<DateTime?>)((dynamic)this)._inserted_at).OriginalValue; } }
        protected DateTime? inserted_at_Previous { get { return ((EditableValue<DateTime?>)((dynamic)this)._inserted_at).PreviousValue; } }
                
        [LocalizedDisplayName(_str_managing_organization_id)]
        [MapField(_str_managing_organization_id)]
        public abstract String managing_organization_id { get; set; }
        protected String managing_organization_id_Original { get { return ((EditableValue<String>)((dynamic)this)._managing_organization_id).OriginalValue; } }
        protected String managing_organization_id_Previous { get { return ((EditableValue<String>)((dynamic)this)._managing_organization_id).PreviousValue; } }
                
        [LocalizedDisplayName(_str_managing_organization_edrpou)]
        [MapField(_str_managing_organization_edrpou)]
        public abstract String managing_organization_edrpou { get; set; }
        protected String managing_organization_edrpou_Original { get { return ((EditableValue<String>)((dynamic)this)._managing_organization_edrpou).OriginalValue; } }
        protected String managing_organization_edrpou_Previous { get { return ((EditableValue<String>)((dynamic)this)._managing_organization_edrpou).PreviousValue; } }
                
        [LocalizedDisplayName(_str_managing_organization_name)]
        [MapField(_str_managing_organization_name)]
        public abstract String managing_organization_name { get; set; }
        protected String managing_organization_name_Original { get { return ((EditableValue<String>)((dynamic)this)._managing_organization_name).OriginalValue; } }
        protected String managing_organization_name_Previous { get { return ((EditableValue<String>)((dynamic)this)._managing_organization_name).PreviousValue; } }
                
        [LocalizedDisplayName(_str_division_id)]
        [MapField(_str_division_id)]
        public abstract String division_id { get; set; }
        protected String division_id_Original { get { return ((EditableValue<String>)((dynamic)this)._division_id).OriginalValue; } }
        protected String division_id_Previous { get { return ((EditableValue<String>)((dynamic)this)._division_id).PreviousValue; } }
                
        [LocalizedDisplayName(_str_division_phones)]
        [MapField(_str_division_phones)]
        public abstract String division_phones { get; set; }
        protected String division_phones_Original { get { return ((EditableValue<String>)((dynamic)this)._division_phones).OriginalValue; } }
        protected String division_phones_Previous { get { return ((EditableValue<String>)((dynamic)this)._division_phones).PreviousValue; } }
                
        [LocalizedDisplayName(_str_division_email)]
        [MapField(_str_division_email)]
        public abstract String division_email { get; set; }
        protected String division_email_Original { get { return ((EditableValue<String>)((dynamic)this)._division_email).OriginalValue; } }
        protected String division_email_Previous { get { return ((EditableValue<String>)((dynamic)this)._division_email).PreviousValue; } }
                
        [LocalizedDisplayName(_str_division_zip)]
        [MapField(_str_division_zip)]
        public abstract String division_zip { get; set; }
        protected String division_zip_Original { get { return ((EditableValue<String>)((dynamic)this)._division_zip).OriginalValue; } }
        protected String division_zip_Previous { get { return ((EditableValue<String>)((dynamic)this)._division_zip).PreviousValue; } }
                
        [LocalizedDisplayName(_str_division_area)]
        [MapField(_str_division_area)]
        public abstract String division_area { get; set; }
        protected String division_area_Original { get { return ((EditableValue<String>)((dynamic)this)._division_area).OriginalValue; } }
        protected String division_area_Previous { get { return ((EditableValue<String>)((dynamic)this)._division_area).PreviousValue; } }
                
        [LocalizedDisplayName(_str_division_region)]
        [MapField(_str_division_region)]
        public abstract String division_region { get; set; }
        protected String division_region_Original { get { return ((EditableValue<String>)((dynamic)this)._division_region).OriginalValue; } }
        protected String division_region_Previous { get { return ((EditableValue<String>)((dynamic)this)._division_region).PreviousValue; } }
                
        [LocalizedDisplayName(_str_division_koatuu)]
        [MapField(_str_division_koatuu)]
        public abstract String division_koatuu { get; set; }
        protected String division_koatuu_Original { get { return ((EditableValue<String>)((dynamic)this)._division_koatuu).OriginalValue; } }
        protected String division_koatuu_Previous { get { return ((EditableValue<String>)((dynamic)this)._division_koatuu).PreviousValue; } }
                
        [LocalizedDisplayName(_str_division_settlement_type)]
        [MapField(_str_division_settlement_type)]
        public abstract String division_settlement_type { get; set; }
        protected String division_settlement_type_Original { get { return ((EditableValue<String>)((dynamic)this)._division_settlement_type).OriginalValue; } }
        protected String division_settlement_type_Previous { get { return ((EditableValue<String>)((dynamic)this)._division_settlement_type).PreviousValue; } }
                
        [LocalizedDisplayName(_str_division_settlement)]
        [MapField(_str_division_settlement)]
        public abstract String division_settlement { get; set; }
        protected String division_settlement_Original { get { return ((EditableValue<String>)((dynamic)this)._division_settlement).OriginalValue; } }
        protected String division_settlement_Previous { get { return ((EditableValue<String>)((dynamic)this)._division_settlement).PreviousValue; } }
                
        [LocalizedDisplayName(_str_division_street_type)]
        [MapField(_str_division_street_type)]
        public abstract String division_street_type { get; set; }
        protected String division_street_type_Original { get { return ((EditableValue<String>)((dynamic)this)._division_street_type).OriginalValue; } }
        protected String division_street_type_Previous { get { return ((EditableValue<String>)((dynamic)this)._division_street_type).PreviousValue; } }
                
        [LocalizedDisplayName(_str_division_street)]
        [MapField(_str_division_street)]
        public abstract String division_street { get; set; }
        protected String division_street_Original { get { return ((EditableValue<String>)((dynamic)this)._division_street).OriginalValue; } }
        protected String division_street_Previous { get { return ((EditableValue<String>)((dynamic)this)._division_street).PreviousValue; } }
                
        [LocalizedDisplayName(_str_division_building)]
        [MapField(_str_division_building)]
        public abstract String division_building { get; set; }
        protected String division_building_Original { get { return ((EditableValue<String>)((dynamic)this)._division_building).OriginalValue; } }
        protected String division_building_Previous { get { return ((EditableValue<String>)((dynamic)this)._division_building).PreviousValue; } }
                
        [LocalizedDisplayName(_str_doctor_performer_id)]
        [MapField(_str_doctor_performer_id)]
        public abstract String doctor_performer_id { get; set; }
        protected String doctor_performer_id_Original { get { return ((EditableValue<String>)((dynamic)this)._doctor_performer_id).OriginalValue; } }
        protected String doctor_performer_id_Previous { get { return ((EditableValue<String>)((dynamic)this)._doctor_performer_id).PreviousValue; } }
                
        [LocalizedDisplayName(_str_doctor_last_name)]
        [MapField(_str_doctor_last_name)]
        public abstract String doctor_last_name { get; set; }
        protected String doctor_last_name_Original { get { return ((EditableValue<String>)((dynamic)this)._doctor_last_name).OriginalValue; } }
        protected String doctor_last_name_Previous { get { return ((EditableValue<String>)((dynamic)this)._doctor_last_name).PreviousValue; } }
                
        [LocalizedDisplayName(_str_doctor_first_name)]
        [MapField(_str_doctor_first_name)]
        public abstract String doctor_first_name { get; set; }
        protected String doctor_first_name_Original { get { return ((EditableValue<String>)((dynamic)this)._doctor_first_name).OriginalValue; } }
        protected String doctor_first_name_Previous { get { return ((EditableValue<String>)((dynamic)this)._doctor_first_name).PreviousValue; } }
                
        [LocalizedDisplayName(_str_doctor_second_name)]
        [MapField(_str_doctor_second_name)]
        public abstract String doctor_second_name { get; set; }
        protected String doctor_second_name_Original { get { return ((EditableValue<String>)((dynamic)this)._doctor_second_name).OriginalValue; } }
        protected String doctor_second_name_Previous { get { return ((EditableValue<String>)((dynamic)this)._doctor_second_name).PreviousValue; } }
                
        [LocalizedDisplayName(_str_idfCase)]
        [MapField(_str_idfCase)]
        public abstract Int64 idfCase { get; set; }
        protected Int64 idfCase_Original { get { return ((EditableValue<Int64>)((dynamic)this)._idfCase).OriginalValue; } }
        protected Int64 idfCase_Previous { get { return ((EditableValue<Int64>)((dynamic)this)._idfCase).PreviousValue; } }
                
        [LocalizedDisplayName(_str_strCaseID)]
        [MapField(_str_strCaseID)]
        public abstract String strCaseID { get; set; }
        protected String strCaseID_Original { get { return ((EditableValue<String>)((dynamic)this)._strCaseID).OriginalValue; } }
        protected String strCaseID_Previous { get { return ((EditableValue<String>)((dynamic)this)._strCaseID).PreviousValue; } }
                
        [LocalizedDisplayName(_str_Resolution)]
        [MapField(_str_Resolution)]
        public abstract Int32? Resolution { get; set; }
        protected Int32? Resolution_Original { get { return ((EditableValue<Int32?>)((dynamic)this)._resolution).OriginalValue; } }
        protected Int32? Resolution_Previous { get { return ((EditableValue<Int32?>)((dynamic)this)._resolution).PreviousValue; } }
                
        [LocalizedDisplayName(_str_PostWithoutMaster)]
        [MapField(_str_PostWithoutMaster)]
        public abstract Int32 PostWithoutMaster { get; set; }
        protected Int32 PostWithoutMaster_Original { get { return ((EditableValue<Int32>)((dynamic)this)._postWithoutMaster).OriginalValue; } }
        protected Int32 PostWithoutMaster_Previous { get { return ((EditableValue<Int32>)((dynamic)this)._postWithoutMaster).PreviousValue; } }
                
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
                
        #region Set/Get values
        #region filed_info definifion
        protected class field_info
        {
            internal string _name;
            internal string _formname;
            internal string _type;
            internal Func<UploadEhsLaboratoryEventItem, object> _get_func;
            internal Action<UploadEhsLaboratoryEventItem, string> _set_func;
            internal Action<UploadEhsLaboratoryEventItem, UploadEhsLaboratoryEventItem, CompareModel, DbManagerProxy> _compare_func;
        }
        internal const string _str_Parent = "Parent";
        internal const string _str_IsNew = "IsNew";
        
        internal const string _str_idfsUploadEhs = "idfsUploadEhs";
        internal const string _str_idfsUploadEhsLaboratoryEventItem = "idfsUploadEhsLaboratoryEventItem";
        internal const string _str_patient_id = "patient_id";
        internal const string _str_code = "code";
        internal const string _str_value = "value";
        internal const string _str_primary_source = "primary_source";
        internal const string _str_issued = "issued";
        internal const string _str_inserted_at = "inserted_at";
        internal const string _str_managing_organization_id = "managing_organization_id";
        internal const string _str_managing_organization_edrpou = "managing_organization_edrpou";
        internal const string _str_managing_organization_name = "managing_organization_name";
        internal const string _str_division_id = "division_id";
        internal const string _str_division_phones = "division_phones";
        internal const string _str_division_email = "division_email";
        internal const string _str_division_zip = "division_zip";
        internal const string _str_division_area = "division_area";
        internal const string _str_division_region = "division_region";
        internal const string _str_division_koatuu = "division_koatuu";
        internal const string _str_division_settlement_type = "division_settlement_type";
        internal const string _str_division_settlement = "division_settlement";
        internal const string _str_division_street_type = "division_street_type";
        internal const string _str_division_street = "division_street";
        internal const string _str_division_building = "division_building";
        internal const string _str_doctor_performer_id = "doctor_performer_id";
        internal const string _str_doctor_last_name = "doctor_last_name";
        internal const string _str_doctor_first_name = "doctor_first_name";
        internal const string _str_doctor_second_name = "doctor_second_name";
        internal const string _str_idfCase = "idfCase";
        internal const string _str_strCaseID = "strCaseID";
        internal const string _str_Resolution = "Resolution";
        internal const string _str_PostWithoutMaster = "PostWithoutMaster";
        internal const string _str_idfPersonEnteredBy = "idfPersonEnteredBy";
        internal const string _str_idfUserID = "idfUserID";
        internal const string _str_validationErrors = "validationErrors";
        internal const string _str_master = "master";
        internal const string _str_isValid = "isValid";
        private static readonly field_info[] _field_infos =
        {
                  
            new field_info {
              _name = _str_idfsUploadEhs, _formname = _str_idfsUploadEhs, _type = "Int64",
              _get_func = o => o.idfsUploadEhs,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseInt64(val); if (o.idfsUploadEhs != newval) o.idfsUploadEhs = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.idfsUploadEhs != c.idfsUploadEhs || o.IsRIRPropChanged(_str_idfsUploadEhs, c)) 
                  m.Add(_str_idfsUploadEhs, o.ObjectIdent + _str_idfsUploadEhs, o.ObjectIdent2 + _str_idfsUploadEhs, o.ObjectIdent3 + _str_idfsUploadEhs, "Int64", 
                    o.idfsUploadEhs == null ? "" : o.idfsUploadEhs.ToString(),                  
                  o.IsReadOnly(_str_idfsUploadEhs), o.IsInvisible(_str_idfsUploadEhs), o.IsRequired(_str_idfsUploadEhs)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_idfsUploadEhsLaboratoryEventItem, _formname = _str_idfsUploadEhsLaboratoryEventItem, _type = "Int64",
              _get_func = o => o.idfsUploadEhsLaboratoryEventItem,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseInt64(val); if (o.idfsUploadEhsLaboratoryEventItem != newval) o.idfsUploadEhsLaboratoryEventItem = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.idfsUploadEhsLaboratoryEventItem != c.idfsUploadEhsLaboratoryEventItem || o.IsRIRPropChanged(_str_idfsUploadEhsLaboratoryEventItem, c)) 
                  m.Add(_str_idfsUploadEhsLaboratoryEventItem, o.ObjectIdent + _str_idfsUploadEhsLaboratoryEventItem, o.ObjectIdent2 + _str_idfsUploadEhsLaboratoryEventItem, o.ObjectIdent3 + _str_idfsUploadEhsLaboratoryEventItem, "Int64", 
                    o.idfsUploadEhsLaboratoryEventItem == null ? "" : o.idfsUploadEhsLaboratoryEventItem.ToString(),                  
                  o.IsReadOnly(_str_idfsUploadEhsLaboratoryEventItem), o.IsInvisible(_str_idfsUploadEhsLaboratoryEventItem), o.IsRequired(_str_idfsUploadEhsLaboratoryEventItem)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_patient_id, _formname = _str_patient_id, _type = "String",
              _get_func = o => o.patient_id,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.patient_id != newval) o.patient_id = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.patient_id != c.patient_id || o.IsRIRPropChanged(_str_patient_id, c)) 
                  m.Add(_str_patient_id, o.ObjectIdent + _str_patient_id, o.ObjectIdent2 + _str_patient_id, o.ObjectIdent3 + _str_patient_id, "String", 
                    o.patient_id == null ? "" : o.patient_id.ToString(),                  
                  o.IsReadOnly(_str_patient_id), o.IsInvisible(_str_patient_id), o.IsRequired(_str_patient_id)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_code, _formname = _str_code, _type = "String",
              _get_func = o => o.code,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.code != newval) o.code = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.code != c.code || o.IsRIRPropChanged(_str_code, c)) 
                  m.Add(_str_code, o.ObjectIdent + _str_code, o.ObjectIdent2 + _str_code, o.ObjectIdent3 + _str_code, "String", 
                    o.code == null ? "" : o.code.ToString(),                  
                  o.IsReadOnly(_str_code), o.IsInvisible(_str_code), o.IsRequired(_str_code)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_value, _formname = _str_value, _type = "String",
              _get_func = o => o.value,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.value != newval) o.value = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.value != c.value || o.IsRIRPropChanged(_str_value, c)) 
                  m.Add(_str_value, o.ObjectIdent + _str_value, o.ObjectIdent2 + _str_value, o.ObjectIdent3 + _str_value, "String", 
                    o.value == null ? "" : o.value.ToString(),                  
                  o.IsReadOnly(_str_value), o.IsInvisible(_str_value), o.IsRequired(_str_value)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_primary_source, _formname = _str_primary_source, _type = "String",
              _get_func = o => o.primary_source,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.primary_source != newval) o.primary_source = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.primary_source != c.primary_source || o.IsRIRPropChanged(_str_primary_source, c)) 
                  m.Add(_str_primary_source, o.ObjectIdent + _str_primary_source, o.ObjectIdent2 + _str_primary_source, o.ObjectIdent3 + _str_primary_source, "String", 
                    o.primary_source == null ? "" : o.primary_source.ToString(),                  
                  o.IsReadOnly(_str_primary_source), o.IsInvisible(_str_primary_source), o.IsRequired(_str_primary_source)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_issued, _formname = _str_issued, _type = "DateTime?",
              _get_func = o => o.issued,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseDateTimeNullable(val); if (o.issued != newval) o.issued = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.issued != c.issued || o.IsRIRPropChanged(_str_issued, c)) 
                  m.Add(_str_issued, o.ObjectIdent + _str_issued, o.ObjectIdent2 + _str_issued, o.ObjectIdent3 + _str_issued, "DateTime?", 
                    o.issued == null ? "" : o.issued.ToString(),                  
                  o.IsReadOnly(_str_issued), o.IsInvisible(_str_issued), o.IsRequired(_str_issued)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_inserted_at, _formname = _str_inserted_at, _type = "DateTime?",
              _get_func = o => o.inserted_at,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseDateTimeNullable(val); if (o.inserted_at != newval) o.inserted_at = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.inserted_at != c.inserted_at || o.IsRIRPropChanged(_str_inserted_at, c)) 
                  m.Add(_str_inserted_at, o.ObjectIdent + _str_inserted_at, o.ObjectIdent2 + _str_inserted_at, o.ObjectIdent3 + _str_inserted_at, "DateTime?", 
                    o.inserted_at == null ? "" : o.inserted_at.ToString(),                  
                  o.IsReadOnly(_str_inserted_at), o.IsInvisible(_str_inserted_at), o.IsRequired(_str_inserted_at)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_managing_organization_id, _formname = _str_managing_organization_id, _type = "String",
              _get_func = o => o.managing_organization_id,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.managing_organization_id != newval) o.managing_organization_id = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.managing_organization_id != c.managing_organization_id || o.IsRIRPropChanged(_str_managing_organization_id, c)) 
                  m.Add(_str_managing_organization_id, o.ObjectIdent + _str_managing_organization_id, o.ObjectIdent2 + _str_managing_organization_id, o.ObjectIdent3 + _str_managing_organization_id, "String", 
                    o.managing_organization_id == null ? "" : o.managing_organization_id.ToString(),                  
                  o.IsReadOnly(_str_managing_organization_id), o.IsInvisible(_str_managing_organization_id), o.IsRequired(_str_managing_organization_id)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_managing_organization_edrpou, _formname = _str_managing_organization_edrpou, _type = "String",
              _get_func = o => o.managing_organization_edrpou,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.managing_organization_edrpou != newval) o.managing_organization_edrpou = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.managing_organization_edrpou != c.managing_organization_edrpou || o.IsRIRPropChanged(_str_managing_organization_edrpou, c)) 
                  m.Add(_str_managing_organization_edrpou, o.ObjectIdent + _str_managing_organization_edrpou, o.ObjectIdent2 + _str_managing_organization_edrpou, o.ObjectIdent3 + _str_managing_organization_edrpou, "String", 
                    o.managing_organization_edrpou == null ? "" : o.managing_organization_edrpou.ToString(),                  
                  o.IsReadOnly(_str_managing_organization_edrpou), o.IsInvisible(_str_managing_organization_edrpou), o.IsRequired(_str_managing_organization_edrpou)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_managing_organization_name, _formname = _str_managing_organization_name, _type = "String",
              _get_func = o => o.managing_organization_name,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.managing_organization_name != newval) o.managing_organization_name = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.managing_organization_name != c.managing_organization_name || o.IsRIRPropChanged(_str_managing_organization_name, c)) 
                  m.Add(_str_managing_organization_name, o.ObjectIdent + _str_managing_organization_name, o.ObjectIdent2 + _str_managing_organization_name, o.ObjectIdent3 + _str_managing_organization_name, "String", 
                    o.managing_organization_name == null ? "" : o.managing_organization_name.ToString(),                  
                  o.IsReadOnly(_str_managing_organization_name), o.IsInvisible(_str_managing_organization_name), o.IsRequired(_str_managing_organization_name)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_division_id, _formname = _str_division_id, _type = "String",
              _get_func = o => o.division_id,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.division_id != newval) o.division_id = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.division_id != c.division_id || o.IsRIRPropChanged(_str_division_id, c)) 
                  m.Add(_str_division_id, o.ObjectIdent + _str_division_id, o.ObjectIdent2 + _str_division_id, o.ObjectIdent3 + _str_division_id, "String", 
                    o.division_id == null ? "" : o.division_id.ToString(),                  
                  o.IsReadOnly(_str_division_id), o.IsInvisible(_str_division_id), o.IsRequired(_str_division_id)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_division_phones, _formname = _str_division_phones, _type = "String",
              _get_func = o => o.division_phones,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.division_phones != newval) o.division_phones = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.division_phones != c.division_phones || o.IsRIRPropChanged(_str_division_phones, c)) 
                  m.Add(_str_division_phones, o.ObjectIdent + _str_division_phones, o.ObjectIdent2 + _str_division_phones, o.ObjectIdent3 + _str_division_phones, "String", 
                    o.division_phones == null ? "" : o.division_phones.ToString(),                  
                  o.IsReadOnly(_str_division_phones), o.IsInvisible(_str_division_phones), o.IsRequired(_str_division_phones)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_division_email, _formname = _str_division_email, _type = "String",
              _get_func = o => o.division_email,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.division_email != newval) o.division_email = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.division_email != c.division_email || o.IsRIRPropChanged(_str_division_email, c)) 
                  m.Add(_str_division_email, o.ObjectIdent + _str_division_email, o.ObjectIdent2 + _str_division_email, o.ObjectIdent3 + _str_division_email, "String", 
                    o.division_email == null ? "" : o.division_email.ToString(),                  
                  o.IsReadOnly(_str_division_email), o.IsInvisible(_str_division_email), o.IsRequired(_str_division_email)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_division_zip, _formname = _str_division_zip, _type = "String",
              _get_func = o => o.division_zip,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.division_zip != newval) o.division_zip = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.division_zip != c.division_zip || o.IsRIRPropChanged(_str_division_zip, c)) 
                  m.Add(_str_division_zip, o.ObjectIdent + _str_division_zip, o.ObjectIdent2 + _str_division_zip, o.ObjectIdent3 + _str_division_zip, "String", 
                    o.division_zip == null ? "" : o.division_zip.ToString(),                  
                  o.IsReadOnly(_str_division_zip), o.IsInvisible(_str_division_zip), o.IsRequired(_str_division_zip)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_division_area, _formname = _str_division_area, _type = "String",
              _get_func = o => o.division_area,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.division_area != newval) o.division_area = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.division_area != c.division_area || o.IsRIRPropChanged(_str_division_area, c)) 
                  m.Add(_str_division_area, o.ObjectIdent + _str_division_area, o.ObjectIdent2 + _str_division_area, o.ObjectIdent3 + _str_division_area, "String", 
                    o.division_area == null ? "" : o.division_area.ToString(),                  
                  o.IsReadOnly(_str_division_area), o.IsInvisible(_str_division_area), o.IsRequired(_str_division_area)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_division_region, _formname = _str_division_region, _type = "String",
              _get_func = o => o.division_region,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.division_region != newval) o.division_region = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.division_region != c.division_region || o.IsRIRPropChanged(_str_division_region, c)) 
                  m.Add(_str_division_region, o.ObjectIdent + _str_division_region, o.ObjectIdent2 + _str_division_region, o.ObjectIdent3 + _str_division_region, "String", 
                    o.division_region == null ? "" : o.division_region.ToString(),                  
                  o.IsReadOnly(_str_division_region), o.IsInvisible(_str_division_region), o.IsRequired(_str_division_region)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_division_koatuu, _formname = _str_division_koatuu, _type = "String",
              _get_func = o => o.division_koatuu,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.division_koatuu != newval) o.division_koatuu = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.division_koatuu != c.division_koatuu || o.IsRIRPropChanged(_str_division_koatuu, c)) 
                  m.Add(_str_division_koatuu, o.ObjectIdent + _str_division_koatuu, o.ObjectIdent2 + _str_division_koatuu, o.ObjectIdent3 + _str_division_koatuu, "String", 
                    o.division_koatuu == null ? "" : o.division_koatuu.ToString(),                  
                  o.IsReadOnly(_str_division_koatuu), o.IsInvisible(_str_division_koatuu), o.IsRequired(_str_division_koatuu)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_division_settlement_type, _formname = _str_division_settlement_type, _type = "String",
              _get_func = o => o.division_settlement_type,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.division_settlement_type != newval) o.division_settlement_type = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.division_settlement_type != c.division_settlement_type || o.IsRIRPropChanged(_str_division_settlement_type, c)) 
                  m.Add(_str_division_settlement_type, o.ObjectIdent + _str_division_settlement_type, o.ObjectIdent2 + _str_division_settlement_type, o.ObjectIdent3 + _str_division_settlement_type, "String", 
                    o.division_settlement_type == null ? "" : o.division_settlement_type.ToString(),                  
                  o.IsReadOnly(_str_division_settlement_type), o.IsInvisible(_str_division_settlement_type), o.IsRequired(_str_division_settlement_type)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_division_settlement, _formname = _str_division_settlement, _type = "String",
              _get_func = o => o.division_settlement,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.division_settlement != newval) o.division_settlement = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.division_settlement != c.division_settlement || o.IsRIRPropChanged(_str_division_settlement, c)) 
                  m.Add(_str_division_settlement, o.ObjectIdent + _str_division_settlement, o.ObjectIdent2 + _str_division_settlement, o.ObjectIdent3 + _str_division_settlement, "String", 
                    o.division_settlement == null ? "" : o.division_settlement.ToString(),                  
                  o.IsReadOnly(_str_division_settlement), o.IsInvisible(_str_division_settlement), o.IsRequired(_str_division_settlement)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_division_street_type, _formname = _str_division_street_type, _type = "String",
              _get_func = o => o.division_street_type,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.division_street_type != newval) o.division_street_type = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.division_street_type != c.division_street_type || o.IsRIRPropChanged(_str_division_street_type, c)) 
                  m.Add(_str_division_street_type, o.ObjectIdent + _str_division_street_type, o.ObjectIdent2 + _str_division_street_type, o.ObjectIdent3 + _str_division_street_type, "String", 
                    o.division_street_type == null ? "" : o.division_street_type.ToString(),                  
                  o.IsReadOnly(_str_division_street_type), o.IsInvisible(_str_division_street_type), o.IsRequired(_str_division_street_type)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_division_street, _formname = _str_division_street, _type = "String",
              _get_func = o => o.division_street,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.division_street != newval) o.division_street = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.division_street != c.division_street || o.IsRIRPropChanged(_str_division_street, c)) 
                  m.Add(_str_division_street, o.ObjectIdent + _str_division_street, o.ObjectIdent2 + _str_division_street, o.ObjectIdent3 + _str_division_street, "String", 
                    o.division_street == null ? "" : o.division_street.ToString(),                  
                  o.IsReadOnly(_str_division_street), o.IsInvisible(_str_division_street), o.IsRequired(_str_division_street)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_division_building, _formname = _str_division_building, _type = "String",
              _get_func = o => o.division_building,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.division_building != newval) o.division_building = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.division_building != c.division_building || o.IsRIRPropChanged(_str_division_building, c)) 
                  m.Add(_str_division_building, o.ObjectIdent + _str_division_building, o.ObjectIdent2 + _str_division_building, o.ObjectIdent3 + _str_division_building, "String", 
                    o.division_building == null ? "" : o.division_building.ToString(),                  
                  o.IsReadOnly(_str_division_building), o.IsInvisible(_str_division_building), o.IsRequired(_str_division_building)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_doctor_performer_id, _formname = _str_doctor_performer_id, _type = "String",
              _get_func = o => o.doctor_performer_id,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.doctor_performer_id != newval) o.doctor_performer_id = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.doctor_performer_id != c.doctor_performer_id || o.IsRIRPropChanged(_str_doctor_performer_id, c)) 
                  m.Add(_str_doctor_performer_id, o.ObjectIdent + _str_doctor_performer_id, o.ObjectIdent2 + _str_doctor_performer_id, o.ObjectIdent3 + _str_doctor_performer_id, "String", 
                    o.doctor_performer_id == null ? "" : o.doctor_performer_id.ToString(),                  
                  o.IsReadOnly(_str_doctor_performer_id), o.IsInvisible(_str_doctor_performer_id), o.IsRequired(_str_doctor_performer_id)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_doctor_last_name, _formname = _str_doctor_last_name, _type = "String",
              _get_func = o => o.doctor_last_name,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.doctor_last_name != newval) o.doctor_last_name = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.doctor_last_name != c.doctor_last_name || o.IsRIRPropChanged(_str_doctor_last_name, c)) 
                  m.Add(_str_doctor_last_name, o.ObjectIdent + _str_doctor_last_name, o.ObjectIdent2 + _str_doctor_last_name, o.ObjectIdent3 + _str_doctor_last_name, "String", 
                    o.doctor_last_name == null ? "" : o.doctor_last_name.ToString(),                  
                  o.IsReadOnly(_str_doctor_last_name), o.IsInvisible(_str_doctor_last_name), o.IsRequired(_str_doctor_last_name)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_doctor_first_name, _formname = _str_doctor_first_name, _type = "String",
              _get_func = o => o.doctor_first_name,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.doctor_first_name != newval) o.doctor_first_name = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.doctor_first_name != c.doctor_first_name || o.IsRIRPropChanged(_str_doctor_first_name, c)) 
                  m.Add(_str_doctor_first_name, o.ObjectIdent + _str_doctor_first_name, o.ObjectIdent2 + _str_doctor_first_name, o.ObjectIdent3 + _str_doctor_first_name, "String", 
                    o.doctor_first_name == null ? "" : o.doctor_first_name.ToString(),                  
                  o.IsReadOnly(_str_doctor_first_name), o.IsInvisible(_str_doctor_first_name), o.IsRequired(_str_doctor_first_name)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_doctor_second_name, _formname = _str_doctor_second_name, _type = "String",
              _get_func = o => o.doctor_second_name,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.doctor_second_name != newval) o.doctor_second_name = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.doctor_second_name != c.doctor_second_name || o.IsRIRPropChanged(_str_doctor_second_name, c)) 
                  m.Add(_str_doctor_second_name, o.ObjectIdent + _str_doctor_second_name, o.ObjectIdent2 + _str_doctor_second_name, o.ObjectIdent3 + _str_doctor_second_name, "String", 
                    o.doctor_second_name == null ? "" : o.doctor_second_name.ToString(),                  
                  o.IsReadOnly(_str_doctor_second_name), o.IsInvisible(_str_doctor_second_name), o.IsRequired(_str_doctor_second_name)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_idfCase, _formname = _str_idfCase, _type = "Int64",
              _get_func = o => o.idfCase,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseInt64(val); if (o.idfCase != newval) o.idfCase = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.idfCase != c.idfCase || o.IsRIRPropChanged(_str_idfCase, c)) 
                  m.Add(_str_idfCase, o.ObjectIdent + _str_idfCase, o.ObjectIdent2 + _str_idfCase, o.ObjectIdent3 + _str_idfCase, "Int64", 
                    o.idfCase == null ? "" : o.idfCase.ToString(),                  
                  o.IsReadOnly(_str_idfCase), o.IsInvisible(_str_idfCase), o.IsRequired(_str_idfCase)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_strCaseID, _formname = _str_strCaseID, _type = "String",
              _get_func = o => o.strCaseID,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.strCaseID != newval) o.strCaseID = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.strCaseID != c.strCaseID || o.IsRIRPropChanged(_str_strCaseID, c)) 
                  m.Add(_str_strCaseID, o.ObjectIdent + _str_strCaseID, o.ObjectIdent2 + _str_strCaseID, o.ObjectIdent3 + _str_strCaseID, "String", 
                    o.strCaseID == null ? "" : o.strCaseID.ToString(),                  
                  o.IsReadOnly(_str_strCaseID), o.IsInvisible(_str_strCaseID), o.IsRequired(_str_strCaseID)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_Resolution, _formname = _str_Resolution, _type = "Int32?",
              _get_func = o => o.Resolution,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseInt32Nullable(val); if (o.Resolution != newval) o.Resolution = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.Resolution != c.Resolution || o.IsRIRPropChanged(_str_Resolution, c)) 
                  m.Add(_str_Resolution, o.ObjectIdent + _str_Resolution, o.ObjectIdent2 + _str_Resolution, o.ObjectIdent3 + _str_Resolution, "Int32?", 
                    o.Resolution == null ? "" : o.Resolution.ToString(),                  
                  o.IsReadOnly(_str_Resolution), o.IsInvisible(_str_Resolution), o.IsRequired(_str_Resolution)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_PostWithoutMaster, _formname = _str_PostWithoutMaster, _type = "Int32",
              _get_func = o => o.PostWithoutMaster,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseInt32(val); if (o.PostWithoutMaster != newval) o.PostWithoutMaster = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.PostWithoutMaster != c.PostWithoutMaster || o.IsRIRPropChanged(_str_PostWithoutMaster, c)) 
                  m.Add(_str_PostWithoutMaster, o.ObjectIdent + _str_PostWithoutMaster, o.ObjectIdent2 + _str_PostWithoutMaster, o.ObjectIdent3 + _str_PostWithoutMaster, "Int32", 
                    o.PostWithoutMaster == null ? "" : o.PostWithoutMaster.ToString(),                  
                  o.IsReadOnly(_str_PostWithoutMaster), o.IsInvisible(_str_PostWithoutMaster), o.IsRequired(_str_PostWithoutMaster)); 
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
              _name = _str_validationErrors, _formname = _str_validationErrors, _type = "List<Tuple<string,string>>",
              _get_func = o => o.validationErrors,
              _set_func = (o, val) => { var newval = o.validationErrors; if (o.validationErrors != newval) o.validationErrors = newval; },
              _compare_func = (o, c, m, g) => {
               }
              }, 
        
            new field_info {
              _name = _str_master, _formname = _str_master, _type = "UploadEhsMaster",
              _get_func = o => o.master,
              _set_func = (o, val) => {  },
              _compare_func = (o, c, m, g) => { 
              
                }
              }, 
        
            new field_info {
              _name = _str_isValid, _formname = _str_isValid, _type = "bool",
              _get_func = o => o.isValid,
              _set_func = (o, val) => {  },
              _compare_func = (o, c, m, g) => { 
              
                if (o.isValid != c.isValid || o.IsRIRPropChanged(_str_isValid, c)) {
                  m.Add(_str_isValid, o.ObjectIdent + _str_isValid, o.ObjectIdent2 + _str_isValid, o.ObjectIdent3 + _str_isValid, "bool", o.isValid == null ? "" : o.isValid.ToString(), o.IsReadOnly(_str_isValid), o.IsInvisible(_str_isValid), o.IsRequired(_str_isValid));
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
            UploadEhsLaboratoryEventItem obj = (UploadEhsLaboratoryEventItem)o;
            using (DbManagerProxy manager = DbManagerFactory.Factory.Create(ModelUserContext.Instance))
            {
                Accessor.Instance(null)._LoadLookups(manager, obj);
                foreach (var i in _field_infos)
                    if (i != null && i._compare_func != null) i._compare_func(this, obj, ret, manager);
            }
            return ret;
        }
        #endregion
    
        private BvSelectList _getList(string name)
        {
        
            return null;
        }
    
          [XmlIgnore]
          [LocalizedDisplayName(_str_master)]
        public UploadEhsMaster master
        {
            get { return new Func<UploadEhsLaboratoryEventItem, UploadEhsMaster>(c => c.Parent as UploadEhsMaster)(this); }
            
        }
        
          [XmlIgnore]
          [LocalizedDisplayName(_str_isValid)]
        public bool isValid
        {
            get { return new Func<UploadEhsLaboratoryEventItem, bool>(c => c.validationErrors == null || c.validationErrors.Count == 0)(this); }
            
        }
        
          [LocalizedDisplayName(_str_validationErrors)]
        public List<Tuple<string,string>> validationErrors
        {
            get { return m_validationErrors; }
            set { if (m_validationErrors != value) { m_validationErrors = value; OnPropertyChanged(_str_validationErrors); } }
        }
        private List<Tuple<string,string>> m_validationErrors;
        
        protected CacheScope m_CS;
        protected Accessor _getAccessor() { return Accessor.Instance(m_CS); }
        private IObjectPermissions m_permissions = null;
        internal IObjectPermissions _permissions { get { return m_permissions; } set { m_permissions = value; } }
        internal string m_ObjectName = "UploadEhsLaboratoryEventItem";

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
        
        }
        partial void Cloned();
        partial void ClonedWithSetup();
        private bool bIsClone;
        public override object Clone()
        {
            var ret = base.Clone() as UploadEhsLaboratoryEventItem;
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
            var ret = base.Clone() as UploadEhsLaboratoryEventItem;
            ret.m_Parent = this.Parent;
            ret.m_IsMarkedToDelete = this.m_IsMarkedToDelete;
            ret.m_IsForcedToDelete = this.m_IsForcedToDelete;
            ret.m_IsNew = this.IsNew;
            ret.m_ObjectName = this.m_ObjectName;
        
            Accessor.Instance(null)._SetupLoad(manager, ret, true);
            ret.ClonedWithSetup();
            ret.DeepAcceptChanges();
            ret._setParent();
            ret._permissions = _permissions;
            return ret;
        }
        public UploadEhsLaboratoryEventItem CloneWithSetup()
        {
            using (DbManagerProxy manager = DbManagerFactory.Factory.Create(ModelUserContext.Instance))
            {
                return CloneWithSetup(manager) as UploadEhsLaboratoryEventItem;
            }
        }
        #endregion

        #region IObject implementation
        public object Key { get { return idfsUploadEhs; } }
        public string KeyName { get { return "idfsUploadEhs"; } }
        public object KeyLookup { get { return idfsUploadEhs; } }
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
        
        }
        public void DeepAcceptChanges()
        { 
            AcceptChanges();
        
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

        private bool IsRIRPropChanged(string fld, UploadEhsLaboratoryEventItem c)
        {
            return IsReadOnly(fld) != c.IsReadOnly(fld) || IsInvisible(fld) != c.IsInvisible(fld) || IsRequired(fld) != c._isRequired(m_isRequired, fld);
        }
        private bool IsLookupContentChanged(DbManagerProxy manager, string fld, UploadEhsLaboratoryEventItem c)
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
        

      

        public UploadEhsLaboratoryEventItem()
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
            PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(UploadEhsLaboratoryEventItem_PropertyChanged);
        }
        public void _RevokeMainHandler()
        {
            PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(UploadEhsLaboratoryEventItem_PropertyChanged);
        }
        private void UploadEhsLaboratoryEventItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            (sender as UploadEhsLaboratoryEventItem).Changed(e.PropertyName);
            
            if (e.PropertyName == _str_Parent)
                OnPropertyChanged(_str_master);
                  
            if (e.PropertyName == _str_validationErrors)
                OnPropertyChanged(_str_isValid);
                  
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
            UploadEhsLaboratoryEventItem obj = this;
            
        }
        private void _DeletedExtenders()
        {
            UploadEhsLaboratoryEventItem obj = this;
            
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
        
            }
        }

        private bool m_readOnly;
        private bool _readOnly
        {
            get { return m_readOnly; }
            set
            {
                m_readOnly = value;
        
            }
        }


        internal Dictionary<string, Func<UploadEhsLaboratoryEventItem, bool>> m_isRequired;
        private bool _isRequired(Dictionary<string, Func<UploadEhsLaboratoryEventItem, bool>> isRequiredDict, string name)
        {
            if (isRequiredDict != null && isRequiredDict.ContainsKey(name))
                return isRequiredDict[name](this);
            return false;
        }
        
        public void AddRequired(string name, Func<UploadEhsLaboratoryEventItem, bool> func)
        {
            if (m_isRequired == null) 
                m_isRequired = new Dictionary<string, Func<UploadEhsLaboratoryEventItem, bool>>();
            if (!m_isRequired.ContainsKey(name))
                m_isRequired.Add(name, func);
        }
    
    internal Dictionary<string, Func<UploadEhsLaboratoryEventItem, bool>> m_isHiddenPersonalData;
    private bool _isHiddenPersonalData(string name)
    {
      if (m_isHiddenPersonalData != null && m_isHiddenPersonalData.ContainsKey(name))
        return m_isHiddenPersonalData[name](this);
      return false;
    }

    public void AddHiddenPersonalData(string name, Func<UploadEhsLaboratoryEventItem, bool> func)
    {
      if (m_isHiddenPersonalData == null)
        m_isHiddenPersonalData = new Dictionary<string, Func<UploadEhsLaboratoryEventItem, bool>>();
      if (!m_isHiddenPersonalData.ContainsKey(name))
        m_isHiddenPersonalData.Add(name, func);
    }
  
        #region IDisposable Members
        partial void Disposed();
        private bool bIsDisposed;
        protected bool bNeedLookupRemove;
        ~UploadEhsLaboratoryEventItem()
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
        
            }
            ParsedFormCollection(form);
        }
    

        #region Accessor
        public abstract partial class Accessor
        : DataAccessor<UploadEhsLaboratoryEventItem>
            , IObjectAccessor
            , IObjectMeta
            , IObjectValidator
            
            , IObjectCreator
            , IObjectCreator<UploadEhsLaboratoryEventItem>
            
            , IObjectSelectDetailList
            , IObjectPost
                
        {
            #region IObjectAccessor
            public string KeyName { get { return "idfsUploadEhs"; } }
            #endregion
        
            public delegate void on_action(UploadEhsLaboratoryEventItem obj);
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
            
            public virtual List<IObject> SelectDetailList(DbManagerProxy manager, object ident, int? HACode = null)
            {
                return _SelectList(manager
                    , null
                    , null
                    ).Cast<IObject>().ToList();
            }
            
        
            public virtual List<UploadEhsLaboratoryEventItem> SelectList(DbManagerProxy manager
                )
            {
                return _SelectList(manager
                    , delegate(UploadEhsLaboratoryEventItem obj)
                        {
                        }
                    , delegate(UploadEhsLaboratoryEventItem obj)
                        {
                        }
                    );
            }

            

            public List<UploadEhsLaboratoryEventItem> _SelectList(DbManagerProxy manager
                , on_action loading, on_action loaded
                )
            {
                var ret = _SelectListInternal(manager
                    , loading
                    , loaded
                    );
                  
                  return ret;
            }
      
            
            public virtual List<UploadEhsLaboratoryEventItem> _SelectListInternal(DbManagerProxy manager
                , on_action loading, on_action loaded
                )
            {
                UploadEhsLaboratoryEventItem _obj = null;
                try
                {
                    MapResultSet[] sets = new MapResultSet[1];
                    List<UploadEhsLaboratoryEventItem> objs = new List<UploadEhsLaboratoryEventItem>();
                    sets[0] = new MapResultSet(typeof(UploadEhsLaboratoryEventItem), objs);
                    
                    manager
                        .SetSpCommand("spUploadEhsLaboratoryEventItem_SelectDetail"
                            )
                        .ExecuteResultSet(sets);
                    foreach(var obj in objs) 
                    {
                        _obj = obj;
                        obj.m_CS = m_CS;
                        
                        if (loading != null)
                            loading(obj);
                        _SetupLoad(manager, obj);
                        
                        if (loaded != null)
                            loaded(obj);
                    }
                    
                    return objs;
                }
                catch(DataException e)
                {
                    throw DbModelException.Create(_obj, e);
                }
            }
    
        
        
            internal void _SetupLoad(DbManagerProxy manager, UploadEhsLaboratoryEventItem obj, bool bCloning = false)
            {
                if (obj == null) return;
                
                // loading extenters - begin
                // loading extenters - end
                if (!bCloning)
                {
                
                }
                _LoadLookups(manager, obj);
                obj._setParent();
                
                // loaded extenters - begin
                // loaded extenters - end
                
                _SetupHandlers(obj);
                _SetupChildHandlers(obj, null);
                
                _SetPermitions(obj._permissions, obj);
                _SetupRequired(obj);
                _SetupPersonalDataRestrictions(obj);
                obj._SetupMainHandler();
                obj.AcceptChanges();
            }
            
            internal void _SetPermitions(IObjectPermissions permissions, UploadEhsLaboratoryEventItem obj)
            {
                if (obj != null)
                {
                    obj._permissions = permissions;
                    if (obj._permissions != null)
                    {
                    
                    }
                }
            }

    

            private UploadEhsLaboratoryEventItem _CreateNew(DbManagerProxy manager, IObject Parent, int? HACode, on_action creating, on_action created, bool isFake = false)
            {
                UploadEhsLaboratoryEventItem obj = null;
                try
                {
                    obj = UploadEhsLaboratoryEventItem.CreateInstance();
                    obj.m_CS = m_CS;
                    obj.m_IsNew = true;
                    obj.Parent = Parent;
                    
                    if (creating != null)
                        creating(obj);
                
                    // creating extenters - begin
                obj.validationErrors = new List<Tuple<string,string>>();
                obj.idfsUploadEhs = new Func<UploadEhsLaboratoryEventItem, long>(c => c.master == null ? 0 : c.master.idfsUploadEhs)(obj);
                obj.idfsUploadEhsLaboratoryEventItem = (new PositiveAutoIncrementExtender<UploadEhsLaboratoryEventItem>()).GetScalar(manager, obj, isFake);
                    // creating extenters - end
                
                    _LoadLookups(manager, obj);
                    _SetupHandlers(obj);
                    _SetupChildHandlers(obj, null);
                    
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

            
            public UploadEhsLaboratoryEventItem CreateNewT(DbManagerProxy manager, IObject Parent, int? HACode = null)
            {
                return _CreateNew(manager, Parent, HACode, null, null);
            }
            public IObject CreateNew(DbManagerProxy manager, IObject Parent, int? HACode = null)
            {
                return _CreateNew(manager, Parent, HACode, null, null);
            }
            public UploadEhsLaboratoryEventItem CreateFakeT(DbManagerProxy manager, IObject Parent, int? HACode = null)
            {
                return _CreateNew(manager, Parent, HACode, null, null, true);
            }
            public IObject CreateFake(DbManagerProxy manager, IObject Parent, int? HACode = null)
            {
                return _CreateNew(manager, Parent, HACode, null, null, true);
            }
            
            public UploadEhsLaboratoryEventItem CreateWithParamsT(DbManagerProxy manager, IObject Parent, List<object> pars)
            {
                return _CreateNew(manager, Parent, null, null, null);
            }
            public IObject CreateWithParams(DbManagerProxy manager, IObject Parent, List<object> pars)
            {
                return _CreateNew(manager, Parent, null, null, null);
            }
            
            private void _SetupChildHandlers(UploadEhsLaboratoryEventItem obj, object newobj)
            {
                
            }
            
            private void _SetupHandlers(UploadEhsLaboratoryEventItem obj)
            {
                
            }
    

            internal void _LoadLookups(DbManagerProxy manager, UploadEhsLaboratoryEventItem obj)
            {
                
            }
    
            [SprocName("spUploadEhsLaboratoryEventItem_Post")]
            protected abstract void _post(DbManagerProxy manager, 
                [Direction.InputOutput()] UploadEhsLaboratoryEventItem obj);
            protected void _postPredicate(DbManagerProxy manager, 
                [Direction.InputOutput()] UploadEhsLaboratoryEventItem obj)
            {
                
                if (new Func<UploadEhsLaboratoryEventItem, bool>(c => c.isValid)(obj))
                {
                
                _post(manager, obj);
                
                }
                
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
                        UploadEhsLaboratoryEventItem bo = obj as UploadEhsLaboratoryEventItem;
                        
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
                        bSuccess = _PostNonTransaction(manager, obj as UploadEhsLaboratoryEventItem, bChildObject);
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
            private bool _PostNonTransaction(DbManagerProxy manager, UploadEhsLaboratoryEventItem obj, bool bChildObject) 
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
                    // posting extenters - end
            
                    if (!obj.IsMarkedToDelete && bHasChanges)
                        _postPredicate(manager, obj);
                        
                    // posted extenters - begin
                    // posted extenters - end
            
                }
                //obj.AcceptChanges();
                
                return true;
            }
            
            public bool ValidateCanDelete(UploadEhsLaboratoryEventItem obj)
            {
                using (DbManagerProxy manager = DbManagerFactory.Factory.Create(ModelUserContext.Instance))
                {
                    return ValidateCanDelete(manager, obj);
                }
            }
            public bool ValidateCanDelete(DbManagerProxy manager, UploadEhsLaboratoryEventItem obj)
            {
            
                return true;
            }
        
      
            protected ValidationModelException ChainsValidate(UploadEhsLaboratoryEventItem obj)
            {
                
                return null;
            }
            protected bool ChainsValidate(UploadEhsLaboratoryEventItem obj, bool bRethrowException)
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
                return Validate(manager, obj as UploadEhsLaboratoryEventItem, bPostValidation, bChangeValidation, bDeepValidation, bRethrowException);
            }
            public bool Validate(DbManagerProxy manager, UploadEhsLaboratoryEventItem obj, bool bPostValidation, bool bChangeValidation, bool bDeepValidation, bool bRethrowException = false)
            {
                if (!ChainsValidate(obj, bRethrowException))
                    return false;
                    
                
                return true;
            }
           
    
            private void _SetupRequired(UploadEhsLaboratoryEventItem obj)
            {
            
            }
    
    private void _SetupPersonalDataRestrictions(UploadEhsLaboratoryEventItem obj)
    {
    
    
    }
  
            #region IObjectMeta
            public int? MaxSize(string name) { return Meta.Sizes.ContainsKey(name) ? (int?)Meta.Sizes[name] : null; }
            public bool RequiredByField(string name, IObject obj) { return Meta.RequiredByField.ContainsKey(name) ? Meta.RequiredByField[name](obj as UploadEhsLaboratoryEventItem) : false; }
            public bool RequiredByProperty(string name, IObject obj) { return Meta.RequiredByProperty.ContainsKey(name) ? Meta.RequiredByProperty[name](obj as UploadEhsLaboratoryEventItem) : false; }
            public List<SearchPanelMetaItem> SearchPanelMeta { get { return Meta.SearchPanelMeta; } }
            public List<GridMetaItem> GridMeta { get { return Meta.GridMeta; } }
            public List<ActionMetaItem> Actions { get { return Meta.Actions; } }
            public string DetailPanel { get { return "UploadEhsLaboratoryEventItemDetail"; } }
            public string HelpIdWin { get { return ""; } }
            public string HelpIdWeb { get { return ""; } }
            public string HelpIdHh { get { return ""; } }
            public string SqlSortOrder { get { return Meta.sqlSortOrder; } }
            #endregion
    
        }

        
        #region Meta
        public static class Meta
        {
            public static string spSelect = "spUploadEhsLaboratoryEventItem_SelectDetail";
            public static string spCount = "";
            public static string spPost = "spUploadEhsLaboratoryEventItem_Post";
            public static string spInsert = "";
            public static string spUpdate = "";
            public static string spDelete = "";
            public static string spCanDelete = "";
            public static string sqlSortOrder = "";
            public static Dictionary<string, int> Sizes = new Dictionary<string, int>();
            public static Dictionary<string, Func<UploadEhsLaboratoryEventItem, bool>> RequiredByField = new Dictionary<string, Func<UploadEhsLaboratoryEventItem, bool>>();
            public static Dictionary<string, Func<UploadEhsLaboratoryEventItem, bool>> RequiredByProperty = new Dictionary<string, Func<UploadEhsLaboratoryEventItem, bool>>();
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
                
                Sizes.Add(_str_patient_id, 100);
                Sizes.Add(_str_code, 200);
                Sizes.Add(_str_value, 200);
                Sizes.Add(_str_primary_source, 100);
                Sizes.Add(_str_managing_organization_id, 100);
                Sizes.Add(_str_managing_organization_edrpou, 200);
                Sizes.Add(_str_managing_organization_name, 2000);
                Sizes.Add(_str_division_id, 100);
                Sizes.Add(_str_division_phones, 200);
                Sizes.Add(_str_division_email, 200);
                Sizes.Add(_str_division_zip, 200);
                Sizes.Add(_str_division_area, 2000);
                Sizes.Add(_str_division_region, 2000);
                Sizes.Add(_str_division_koatuu, 200);
                Sizes.Add(_str_division_settlement_type, 200);
                Sizes.Add(_str_division_settlement, 2000);
                Sizes.Add(_str_division_street_type, 200);
                Sizes.Add(_str_division_street, 200);
                Sizes.Add(_str_division_building, 200);
                Sizes.Add(_str_doctor_performer_id, 100);
                Sizes.Add(_str_doctor_last_name, 200);
                Sizes.Add(_str_doctor_first_name, 200);
                Sizes.Add(_str_doctor_second_name, 200);
                Sizes.Add(_str_strCaseID, 200);
                Actions.Add(new ActionMetaItem(
                    "Edit",
                    ActionTypes.Edit,
                    false,
                    String.Empty,
                    String.Empty,
                    (manager, c, pars) => new ActResult(true, c),
                    null,
                    new ActionMetaItem.VisualItem(
                        /*from BvMessages*/"strEdit_Id",
                        "edit",
                        /*from BvMessages*/"tooltipEdit_Id",
                        /*from BvMessages*/"strView_Id",
                        "View1",
                        /*from BvMessages*/"tooltipEdit_Id",
                        ActionsAlignment.Right,
                        ActionsPanelType.Group,
                        ActionsAppType.All
                      ),
                      false,
                      null,
                      null,
                      null,
                      null,
                      null,
                      false
                      ));
                    
        
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
	