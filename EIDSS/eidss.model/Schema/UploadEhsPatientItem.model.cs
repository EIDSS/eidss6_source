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
    public abstract partial class UploadEhsPatientItem : 
        EditableObject<UploadEhsPatientItem>
        , IObject
        , IDisposable
        , ILookupUsage
        {
        
        [MapField(_str_idfsUploadEhs), NonUpdatable, PrimaryKey]
        public abstract Int64 idfsUploadEhs { get; set; }
                
        [LocalizedDisplayName(_str_idfsUploadEhsPatientItem)]
        [MapField(_str_idfsUploadEhsPatientItem)]
        public abstract Int64 idfsUploadEhsPatientItem { get; set; }
        protected Int64 idfsUploadEhsPatientItem_Original { get { return ((EditableValue<Int64>)((dynamic)this)._idfsUploadEhsPatientItem).OriginalValue; } }
        protected Int64 idfsUploadEhsPatientItem_Previous { get { return ((EditableValue<Int64>)((dynamic)this)._idfsUploadEhsPatientItem).PreviousValue; } }
                
        [LocalizedDisplayName(_str_patient_id)]
        [MapField(_str_patient_id)]
        public abstract String patient_id { get; set; }
        protected String patient_id_Original { get { return ((EditableValue<String>)((dynamic)this)._patient_id).OriginalValue; } }
        protected String patient_id_Previous { get { return ((EditableValue<String>)((dynamic)this)._patient_id).PreviousValue; } }
                
        [LocalizedDisplayName(_str_first_name)]
        [MapField(_str_first_name)]
        public abstract String first_name { get; set; }
        protected String first_name_Original { get { return ((EditableValue<String>)((dynamic)this)._first_name).OriginalValue; } }
        protected String first_name_Previous { get { return ((EditableValue<String>)((dynamic)this)._first_name).PreviousValue; } }
                
        [LocalizedDisplayName(_str_last_name)]
        [MapField(_str_last_name)]
        public abstract String last_name { get; set; }
        protected String last_name_Original { get { return ((EditableValue<String>)((dynamic)this)._last_name).OriginalValue; } }
        protected String last_name_Previous { get { return ((EditableValue<String>)((dynamic)this)._last_name).PreviousValue; } }
                
        [LocalizedDisplayName(_str_second_name)]
        [MapField(_str_second_name)]
        public abstract String second_name { get; set; }
        protected String second_name_Original { get { return ((EditableValue<String>)((dynamic)this)._second_name).OriginalValue; } }
        protected String second_name_Previous { get { return ((EditableValue<String>)((dynamic)this)._second_name).PreviousValue; } }
                
        [LocalizedDisplayName(_str_person_birth_date)]
        [MapField(_str_person_birth_date)]
        public abstract DateTime? person_birth_date { get; set; }
        protected DateTime? person_birth_date_Original { get { return ((EditableValue<DateTime?>)((dynamic)this)._person_birth_date).OriginalValue; } }
        protected DateTime? person_birth_date_Previous { get { return ((EditableValue<DateTime?>)((dynamic)this)._person_birth_date).PreviousValue; } }
                
        [LocalizedDisplayName(_str_gender)]
        [MapField(_str_gender)]
        public abstract String gender { get; set; }
        protected String gender_Original { get { return ((EditableValue<String>)((dynamic)this)._gender).OriginalValue; } }
        protected String gender_Previous { get { return ((EditableValue<String>)((dynamic)this)._gender).PreviousValue; } }
                
        [LocalizedDisplayName(_str_phones)]
        [MapField(_str_phones)]
        public abstract String phones { get; set; }
        protected String phones_Original { get { return ((EditableValue<String>)((dynamic)this)._phones).OriginalValue; } }
        protected String phones_Previous { get { return ((EditableValue<String>)((dynamic)this)._phones).PreviousValue; } }
                
        [LocalizedDisplayName(_str_address_zip)]
        [MapField(_str_address_zip)]
        public abstract String address_zip { get; set; }
        protected String address_zip_Original { get { return ((EditableValue<String>)((dynamic)this)._address_zip).OriginalValue; } }
        protected String address_zip_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_zip).PreviousValue; } }
                
        [LocalizedDisplayName(_str_address_area)]
        [MapField(_str_address_area)]
        public abstract String address_area { get; set; }
        protected String address_area_Original { get { return ((EditableValue<String>)((dynamic)this)._address_area).OriginalValue; } }
        protected String address_area_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_area).PreviousValue; } }
                
        [LocalizedDisplayName(_str_address_region)]
        [MapField(_str_address_region)]
        public abstract String address_region { get; set; }
        protected String address_region_Original { get { return ((EditableValue<String>)((dynamic)this)._address_region).OriginalValue; } }
        protected String address_region_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_region).PreviousValue; } }
                
        [LocalizedDisplayName(_str_address_settlement_type)]
        [MapField(_str_address_settlement_type)]
        public abstract String address_settlement_type { get; set; }
        protected String address_settlement_type_Original { get { return ((EditableValue<String>)((dynamic)this)._address_settlement_type).OriginalValue; } }
        protected String address_settlement_type_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_settlement_type).PreviousValue; } }
                
        [LocalizedDisplayName(_str_address_settlement)]
        [MapField(_str_address_settlement)]
        public abstract String address_settlement { get; set; }
        protected String address_settlement_Original { get { return ((EditableValue<String>)((dynamic)this)._address_settlement).OriginalValue; } }
        protected String address_settlement_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_settlement).PreviousValue; } }
                
        [LocalizedDisplayName(_str_address_street_type)]
        [MapField(_str_address_street_type)]
        public abstract String address_street_type { get; set; }
        protected String address_street_type_Original { get { return ((EditableValue<String>)((dynamic)this)._address_street_type).OriginalValue; } }
        protected String address_street_type_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_street_type).PreviousValue; } }
                
        [LocalizedDisplayName(_str_address_street)]
        [MapField(_str_address_street)]
        public abstract String address_street { get; set; }
        protected String address_street_Original { get { return ((EditableValue<String>)((dynamic)this)._address_street).OriginalValue; } }
        protected String address_street_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_street).PreviousValue; } }
                
        [LocalizedDisplayName(_str_address_building)]
        [MapField(_str_address_building)]
        public abstract String address_building { get; set; }
        protected String address_building_Original { get { return ((EditableValue<String>)((dynamic)this)._address_building).OriginalValue; } }
        protected String address_building_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_building).PreviousValue; } }
                
        [LocalizedDisplayName(_str_address_type)]
        [MapField(_str_address_type)]
        public abstract String address_type { get; set; }
        protected String address_type_Original { get { return ((EditableValue<String>)((dynamic)this)._address_type).OriginalValue; } }
        protected String address_type_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_type).PreviousValue; } }
                
        [LocalizedDisplayName(_str_address_apartment)]
        [MapField(_str_address_apartment)]
        public abstract String address_apartment { get; set; }
        protected String address_apartment_Original { get { return ((EditableValue<String>)((dynamic)this)._address_apartment).OriginalValue; } }
        protected String address_apartment_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_apartment).PreviousValue; } }
                
        [LocalizedDisplayName(_str_idfHumanActual)]
        [MapField(_str_idfHumanActual)]
        public abstract Int64 idfHumanActual { get; set; }
        protected Int64 idfHumanActual_Original { get { return ((EditableValue<Int64>)((dynamic)this)._idfHumanActual).OriginalValue; } }
        protected Int64 idfHumanActual_Previous { get { return ((EditableValue<Int64>)((dynamic)this)._idfHumanActual).PreviousValue; } }
                
        [LocalizedDisplayName(_str_strPatientName)]
        [MapField(_str_strPatientName)]
        public abstract String strPatientName { get; set; }
        protected String strPatientName_Original { get { return ((EditableValue<String>)((dynamic)this)._strPatientName).OriginalValue; } }
        protected String strPatientName_Previous { get { return ((EditableValue<String>)((dynamic)this)._strPatientName).PreviousValue; } }
                
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
            internal Func<UploadEhsPatientItem, object> _get_func;
            internal Action<UploadEhsPatientItem, string> _set_func;
            internal Action<UploadEhsPatientItem, UploadEhsPatientItem, CompareModel, DbManagerProxy> _compare_func;
        }
        internal const string _str_Parent = "Parent";
        internal const string _str_IsNew = "IsNew";
        
        internal const string _str_idfsUploadEhs = "idfsUploadEhs";
        internal const string _str_idfsUploadEhsPatientItem = "idfsUploadEhsPatientItem";
        internal const string _str_patient_id = "patient_id";
        internal const string _str_first_name = "first_name";
        internal const string _str_last_name = "last_name";
        internal const string _str_second_name = "second_name";
        internal const string _str_person_birth_date = "person_birth_date";
        internal const string _str_gender = "gender";
        internal const string _str_phones = "phones";
        internal const string _str_address_zip = "address_zip";
        internal const string _str_address_area = "address_area";
        internal const string _str_address_region = "address_region";
        internal const string _str_address_settlement_type = "address_settlement_type";
        internal const string _str_address_settlement = "address_settlement";
        internal const string _str_address_street_type = "address_street_type";
        internal const string _str_address_street = "address_street";
        internal const string _str_address_building = "address_building";
        internal const string _str_address_type = "address_type";
        internal const string _str_address_apartment = "address_apartment";
        internal const string _str_idfHumanActual = "idfHumanActual";
        internal const string _str_strPatientName = "strPatientName";
        internal const string _str_Resolution = "Resolution";
        internal const string _str_PostWithoutMaster = "PostWithoutMaster";
        internal const string _str_idfPersonEnteredBy = "idfPersonEnteredBy";
        internal const string _str_idfUserID = "idfUserID";
        internal const string _str_validationErrors = "validationErrors";
        internal const string _str_master = "master";
        internal const string _str_isValid = "isValid";
        internal const string _str_strStatus = "strStatus";
        internal const string _str_strStatusPresent = "strStatusPresent";
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
              _name = _str_idfsUploadEhsPatientItem, _formname = _str_idfsUploadEhsPatientItem, _type = "Int64",
              _get_func = o => o.idfsUploadEhsPatientItem,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseInt64(val); if (o.idfsUploadEhsPatientItem != newval) o.idfsUploadEhsPatientItem = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.idfsUploadEhsPatientItem != c.idfsUploadEhsPatientItem || o.IsRIRPropChanged(_str_idfsUploadEhsPatientItem, c)) 
                  m.Add(_str_idfsUploadEhsPatientItem, o.ObjectIdent + _str_idfsUploadEhsPatientItem, o.ObjectIdent2 + _str_idfsUploadEhsPatientItem, o.ObjectIdent3 + _str_idfsUploadEhsPatientItem, "Int64", 
                    o.idfsUploadEhsPatientItem == null ? "" : o.idfsUploadEhsPatientItem.ToString(),                  
                  o.IsReadOnly(_str_idfsUploadEhsPatientItem), o.IsInvisible(_str_idfsUploadEhsPatientItem), o.IsRequired(_str_idfsUploadEhsPatientItem)); 
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
              _name = _str_first_name, _formname = _str_first_name, _type = "String",
              _get_func = o => o.first_name,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.first_name != newval) o.first_name = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.first_name != c.first_name || o.IsRIRPropChanged(_str_first_name, c)) 
                  m.Add(_str_first_name, o.ObjectIdent + _str_first_name, o.ObjectIdent2 + _str_first_name, o.ObjectIdent3 + _str_first_name, "String", 
                    o.first_name == null ? "" : o.first_name.ToString(),                  
                  o.IsReadOnly(_str_first_name), o.IsInvisible(_str_first_name), o.IsRequired(_str_first_name)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_last_name, _formname = _str_last_name, _type = "String",
              _get_func = o => o.last_name,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.last_name != newval) o.last_name = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.last_name != c.last_name || o.IsRIRPropChanged(_str_last_name, c)) 
                  m.Add(_str_last_name, o.ObjectIdent + _str_last_name, o.ObjectIdent2 + _str_last_name, o.ObjectIdent3 + _str_last_name, "String", 
                    o.last_name == null ? "" : o.last_name.ToString(),                  
                  o.IsReadOnly(_str_last_name), o.IsInvisible(_str_last_name), o.IsRequired(_str_last_name)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_second_name, _formname = _str_second_name, _type = "String",
              _get_func = o => o.second_name,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.second_name != newval) o.second_name = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.second_name != c.second_name || o.IsRIRPropChanged(_str_second_name, c)) 
                  m.Add(_str_second_name, o.ObjectIdent + _str_second_name, o.ObjectIdent2 + _str_second_name, o.ObjectIdent3 + _str_second_name, "String", 
                    o.second_name == null ? "" : o.second_name.ToString(),                  
                  o.IsReadOnly(_str_second_name), o.IsInvisible(_str_second_name), o.IsRequired(_str_second_name)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_person_birth_date, _formname = _str_person_birth_date, _type = "DateTime?",
              _get_func = o => o.person_birth_date,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseDateTimeNullable(val); if (o.person_birth_date != newval) o.person_birth_date = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.person_birth_date != c.person_birth_date || o.IsRIRPropChanged(_str_person_birth_date, c)) 
                  m.Add(_str_person_birth_date, o.ObjectIdent + _str_person_birth_date, o.ObjectIdent2 + _str_person_birth_date, o.ObjectIdent3 + _str_person_birth_date, "DateTime?", 
                    o.person_birth_date == null ? "" : o.person_birth_date.ToString(),                  
                  o.IsReadOnly(_str_person_birth_date), o.IsInvisible(_str_person_birth_date), o.IsRequired(_str_person_birth_date)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_gender, _formname = _str_gender, _type = "String",
              _get_func = o => o.gender,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.gender != newval) o.gender = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.gender != c.gender || o.IsRIRPropChanged(_str_gender, c)) 
                  m.Add(_str_gender, o.ObjectIdent + _str_gender, o.ObjectIdent2 + _str_gender, o.ObjectIdent3 + _str_gender, "String", 
                    o.gender == null ? "" : o.gender.ToString(),                  
                  o.IsReadOnly(_str_gender), o.IsInvisible(_str_gender), o.IsRequired(_str_gender)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_phones, _formname = _str_phones, _type = "String",
              _get_func = o => o.phones,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.phones != newval) o.phones = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.phones != c.phones || o.IsRIRPropChanged(_str_phones, c)) 
                  m.Add(_str_phones, o.ObjectIdent + _str_phones, o.ObjectIdent2 + _str_phones, o.ObjectIdent3 + _str_phones, "String", 
                    o.phones == null ? "" : o.phones.ToString(),                  
                  o.IsReadOnly(_str_phones), o.IsInvisible(_str_phones), o.IsRequired(_str_phones)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_address_zip, _formname = _str_address_zip, _type = "String",
              _get_func = o => o.address_zip,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.address_zip != newval) o.address_zip = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.address_zip != c.address_zip || o.IsRIRPropChanged(_str_address_zip, c)) 
                  m.Add(_str_address_zip, o.ObjectIdent + _str_address_zip, o.ObjectIdent2 + _str_address_zip, o.ObjectIdent3 + _str_address_zip, "String", 
                    o.address_zip == null ? "" : o.address_zip.ToString(),                  
                  o.IsReadOnly(_str_address_zip), o.IsInvisible(_str_address_zip), o.IsRequired(_str_address_zip)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_address_area, _formname = _str_address_area, _type = "String",
              _get_func = o => o.address_area,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.address_area != newval) o.address_area = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.address_area != c.address_area || o.IsRIRPropChanged(_str_address_area, c)) 
                  m.Add(_str_address_area, o.ObjectIdent + _str_address_area, o.ObjectIdent2 + _str_address_area, o.ObjectIdent3 + _str_address_area, "String", 
                    o.address_area == null ? "" : o.address_area.ToString(),                  
                  o.IsReadOnly(_str_address_area), o.IsInvisible(_str_address_area), o.IsRequired(_str_address_area)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_address_region, _formname = _str_address_region, _type = "String",
              _get_func = o => o.address_region,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.address_region != newval) o.address_region = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.address_region != c.address_region || o.IsRIRPropChanged(_str_address_region, c)) 
                  m.Add(_str_address_region, o.ObjectIdent + _str_address_region, o.ObjectIdent2 + _str_address_region, o.ObjectIdent3 + _str_address_region, "String", 
                    o.address_region == null ? "" : o.address_region.ToString(),                  
                  o.IsReadOnly(_str_address_region), o.IsInvisible(_str_address_region), o.IsRequired(_str_address_region)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_address_settlement_type, _formname = _str_address_settlement_type, _type = "String",
              _get_func = o => o.address_settlement_type,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.address_settlement_type != newval) o.address_settlement_type = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.address_settlement_type != c.address_settlement_type || o.IsRIRPropChanged(_str_address_settlement_type, c)) 
                  m.Add(_str_address_settlement_type, o.ObjectIdent + _str_address_settlement_type, o.ObjectIdent2 + _str_address_settlement_type, o.ObjectIdent3 + _str_address_settlement_type, "String", 
                    o.address_settlement_type == null ? "" : o.address_settlement_type.ToString(),                  
                  o.IsReadOnly(_str_address_settlement_type), o.IsInvisible(_str_address_settlement_type), o.IsRequired(_str_address_settlement_type)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_address_settlement, _formname = _str_address_settlement, _type = "String",
              _get_func = o => o.address_settlement,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.address_settlement != newval) o.address_settlement = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.address_settlement != c.address_settlement || o.IsRIRPropChanged(_str_address_settlement, c)) 
                  m.Add(_str_address_settlement, o.ObjectIdent + _str_address_settlement, o.ObjectIdent2 + _str_address_settlement, o.ObjectIdent3 + _str_address_settlement, "String", 
                    o.address_settlement == null ? "" : o.address_settlement.ToString(),                  
                  o.IsReadOnly(_str_address_settlement), o.IsInvisible(_str_address_settlement), o.IsRequired(_str_address_settlement)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_address_street_type, _formname = _str_address_street_type, _type = "String",
              _get_func = o => o.address_street_type,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.address_street_type != newval) o.address_street_type = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.address_street_type != c.address_street_type || o.IsRIRPropChanged(_str_address_street_type, c)) 
                  m.Add(_str_address_street_type, o.ObjectIdent + _str_address_street_type, o.ObjectIdent2 + _str_address_street_type, o.ObjectIdent3 + _str_address_street_type, "String", 
                    o.address_street_type == null ? "" : o.address_street_type.ToString(),                  
                  o.IsReadOnly(_str_address_street_type), o.IsInvisible(_str_address_street_type), o.IsRequired(_str_address_street_type)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_address_street, _formname = _str_address_street, _type = "String",
              _get_func = o => o.address_street,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.address_street != newval) o.address_street = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.address_street != c.address_street || o.IsRIRPropChanged(_str_address_street, c)) 
                  m.Add(_str_address_street, o.ObjectIdent + _str_address_street, o.ObjectIdent2 + _str_address_street, o.ObjectIdent3 + _str_address_street, "String", 
                    o.address_street == null ? "" : o.address_street.ToString(),                  
                  o.IsReadOnly(_str_address_street), o.IsInvisible(_str_address_street), o.IsRequired(_str_address_street)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_address_building, _formname = _str_address_building, _type = "String",
              _get_func = o => o.address_building,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.address_building != newval) o.address_building = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.address_building != c.address_building || o.IsRIRPropChanged(_str_address_building, c)) 
                  m.Add(_str_address_building, o.ObjectIdent + _str_address_building, o.ObjectIdent2 + _str_address_building, o.ObjectIdent3 + _str_address_building, "String", 
                    o.address_building == null ? "" : o.address_building.ToString(),                  
                  o.IsReadOnly(_str_address_building), o.IsInvisible(_str_address_building), o.IsRequired(_str_address_building)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_address_type, _formname = _str_address_type, _type = "String",
              _get_func = o => o.address_type,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.address_type != newval) o.address_type = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.address_type != c.address_type || o.IsRIRPropChanged(_str_address_type, c)) 
                  m.Add(_str_address_type, o.ObjectIdent + _str_address_type, o.ObjectIdent2 + _str_address_type, o.ObjectIdent3 + _str_address_type, "String", 
                    o.address_type == null ? "" : o.address_type.ToString(),                  
                  o.IsReadOnly(_str_address_type), o.IsInvisible(_str_address_type), o.IsRequired(_str_address_type)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_address_apartment, _formname = _str_address_apartment, _type = "String",
              _get_func = o => o.address_apartment,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.address_apartment != newval) o.address_apartment = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.address_apartment != c.address_apartment || o.IsRIRPropChanged(_str_address_apartment, c)) 
                  m.Add(_str_address_apartment, o.ObjectIdent + _str_address_apartment, o.ObjectIdent2 + _str_address_apartment, o.ObjectIdent3 + _str_address_apartment, "String", 
                    o.address_apartment == null ? "" : o.address_apartment.ToString(),                  
                  o.IsReadOnly(_str_address_apartment), o.IsInvisible(_str_address_apartment), o.IsRequired(_str_address_apartment)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_idfHumanActual, _formname = _str_idfHumanActual, _type = "Int64",
              _get_func = o => o.idfHumanActual,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseInt64(val); if (o.idfHumanActual != newval) o.idfHumanActual = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.idfHumanActual != c.idfHumanActual || o.IsRIRPropChanged(_str_idfHumanActual, c)) 
                  m.Add(_str_idfHumanActual, o.ObjectIdent + _str_idfHumanActual, o.ObjectIdent2 + _str_idfHumanActual, o.ObjectIdent3 + _str_idfHumanActual, "Int64", 
                    o.idfHumanActual == null ? "" : o.idfHumanActual.ToString(),                  
                  o.IsReadOnly(_str_idfHumanActual), o.IsInvisible(_str_idfHumanActual), o.IsRequired(_str_idfHumanActual)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_strPatientName, _formname = _str_strPatientName, _type = "String",
              _get_func = o => o.strPatientName,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.strPatientName != newval) o.strPatientName = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.strPatientName != c.strPatientName || o.IsRIRPropChanged(_str_strPatientName, c)) 
                  m.Add(_str_strPatientName, o.ObjectIdent + _str_strPatientName, o.ObjectIdent2 + _str_strPatientName, o.ObjectIdent3 + _str_strPatientName, "String", 
                    o.strPatientName == null ? "" : o.strPatientName.ToString(),                  
                  o.IsReadOnly(_str_strPatientName), o.IsInvisible(_str_strPatientName), o.IsRequired(_str_strPatientName)); 
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
        
            new field_info {
              _name = _str_strStatus, _formname = _str_strStatus, _type = "string",
              _get_func = o => o.strStatus,
              _set_func = (o, val) => {  },
              _compare_func = (o, c, m, g) => { 
              
                if (o.strStatus != c.strStatus || o.IsRIRPropChanged(_str_strStatus, c)) {
                  m.Add(_str_strStatus, o.ObjectIdent + _str_strStatus, o.ObjectIdent2 + _str_strStatus, o.ObjectIdent3 + _str_strStatus, "string", o.strStatus == null ? "" : o.strStatus.ToString(), o.IsReadOnly(_str_strStatus), o.IsInvisible(_str_strStatus), o.IsRequired(_str_strStatus));
                  }
                
                }
              }, 
        
            new field_info {
              _name = _str_strStatusPresent, _formname = _str_strStatusPresent, _type = "string",
              _get_func = o => o.strStatusPresent,
              _set_func = (o, val) => {  },
              _compare_func = (o, c, m, g) => { 
              
                if (o.strStatusPresent != c.strStatusPresent || o.IsRIRPropChanged(_str_strStatusPresent, c)) {
                  m.Add(_str_strStatusPresent, o.ObjectIdent + _str_strStatusPresent, o.ObjectIdent2 + _str_strStatusPresent, o.ObjectIdent3 + _str_strStatusPresent, "string", o.strStatusPresent == null ? "" : o.strStatusPresent.ToString(), o.IsReadOnly(_str_strStatusPresent), o.IsInvisible(_str_strStatusPresent), o.IsRequired(_str_strStatusPresent));
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
            UploadEhsPatientItem obj = (UploadEhsPatientItem)o;
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
            get { return new Func<UploadEhsPatientItem, UploadEhsMaster>(c => c.Parent as UploadEhsMaster)(this); }
            
        }
        
          [XmlIgnore]
          [LocalizedDisplayName(_str_isValid)]
        public bool isValid
        {
            get { return new Func<UploadEhsPatientItem, bool>(c => c.validationErrors == null || c.validationErrors.Count == 0)(this); }
            
        }
        
          [XmlIgnore]
          [LocalizedDisplayName(_str_strStatus)]
        public string strStatus
        {
            get { return new Func<UploadEhsPatientItem, string>(c => eidss.model.Resources.EidssMessages.Get(c.Resolution.HasValue ? (c.Resolution.Value == (int)UploadEhsPatientResolution.Updated ? "rsUpdated" : (c.Resolution.Value == (int)UploadEhsPatientResolution.Created ? "rsCreated" : "rsDismissed")) : "rsUnspecified"))(this); }
            
        }
        
          [XmlIgnore]
          [LocalizedDisplayName(_str_strStatusPresent)]
        public string strStatusPresent
        {
            get { return new Func<UploadEhsPatientItem, string>(c => eidss.model.Resources.EidssMessages.Get(c.Resolution.HasValue ? (c.Resolution.Value == (int)UploadEhsPatientResolution.Updated ? "rsUpdate" : (c.Resolution.Value == (int)UploadEhsPatientResolution.Created ? "rsCreate" : "rsDismiss")) : "rsUnspecified"))(this); }
            
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
        internal string m_ObjectName = "UploadEhsPatientItem";

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
            var ret = base.Clone() as UploadEhsPatientItem;
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
            var ret = base.Clone() as UploadEhsPatientItem;
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
        public UploadEhsPatientItem CloneWithSetup()
        {
            using (DbManagerProxy manager = DbManagerFactory.Factory.Create(ModelUserContext.Instance))
            {
                return CloneWithSetup(manager) as UploadEhsPatientItem;
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

        private bool IsRIRPropChanged(string fld, UploadEhsPatientItem c)
        {
            return IsReadOnly(fld) != c.IsReadOnly(fld) || IsInvisible(fld) != c.IsInvisible(fld) || IsRequired(fld) != c._isRequired(m_isRequired, fld);
        }
        private bool IsLookupContentChanged(DbManagerProxy manager, string fld, UploadEhsPatientItem c)
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
        

      

        public UploadEhsPatientItem()
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
            PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(UploadEhsPatientItem_PropertyChanged);
        }
        public void _RevokeMainHandler()
        {
            PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(UploadEhsPatientItem_PropertyChanged);
        }
        private void UploadEhsPatientItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            (sender as UploadEhsPatientItem).Changed(e.PropertyName);
            
            if (e.PropertyName == _str_Parent)
                OnPropertyChanged(_str_master);
                  
            if (e.PropertyName == _str_validationErrors)
                OnPropertyChanged(_str_isValid);
                  
            if (e.PropertyName == _str_Resolution)
                OnPropertyChanged(_str_strStatus);
                  
            if (e.PropertyName == _str_Resolution)
                OnPropertyChanged(_str_strStatusPresent);
                  
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
            UploadEhsPatientItem obj = this;
            
        }
        private void _DeletedExtenders()
        {
            UploadEhsPatientItem obj = this;
            
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

    
        private static string[] readonly_names1 = "strStatusPresent".Split(new char[] { ',' });
        
        private bool _isReadOnly(string name)
        {
            
            if (readonly_names1.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsPatientItem, bool>(c => true)(this);
            
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


        internal Dictionary<string, Func<UploadEhsPatientItem, bool>> m_isRequired;
        private bool _isRequired(Dictionary<string, Func<UploadEhsPatientItem, bool>> isRequiredDict, string name)
        {
            if (isRequiredDict != null && isRequiredDict.ContainsKey(name))
                return isRequiredDict[name](this);
            return false;
        }
        
        public void AddRequired(string name, Func<UploadEhsPatientItem, bool> func)
        {
            if (m_isRequired == null) 
                m_isRequired = new Dictionary<string, Func<UploadEhsPatientItem, bool>>();
            if (!m_isRequired.ContainsKey(name))
                m_isRequired.Add(name, func);
        }
    
    internal Dictionary<string, Func<UploadEhsPatientItem, bool>> m_isHiddenPersonalData;
    private bool _isHiddenPersonalData(string name)
    {
      if (m_isHiddenPersonalData != null && m_isHiddenPersonalData.ContainsKey(name))
        return m_isHiddenPersonalData[name](this);
      return false;
    }

    public void AddHiddenPersonalData(string name, Func<UploadEhsPatientItem, bool> func)
    {
      if (m_isHiddenPersonalData == null)
        m_isHiddenPersonalData = new Dictionary<string, Func<UploadEhsPatientItem, bool>>();
      if (!m_isHiddenPersonalData.ContainsKey(name))
        m_isHiddenPersonalData.Add(name, func);
    }
  
        #region IDisposable Members
        partial void Disposed();
        private bool bIsDisposed;
        protected bool bNeedLookupRemove;
        ~UploadEhsPatientItem()
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
        : DataAccessor<UploadEhsPatientItem>
            , IObjectAccessor
            , IObjectMeta
            , IObjectValidator
            
            , IObjectCreator
            , IObjectCreator<UploadEhsPatientItem>
            
            , IObjectSelectDetailList
            , IObjectPost
                
        {
            #region IObjectAccessor
            public string KeyName { get { return "idfsUploadEhs"; } }
            #endregion
        
            public delegate void on_action(UploadEhsPatientItem obj);
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
            
        
            public virtual List<UploadEhsPatientItem> SelectList(DbManagerProxy manager
                )
            {
                return _SelectList(manager
                    , delegate(UploadEhsPatientItem obj)
                        {
                        }
                    , delegate(UploadEhsPatientItem obj)
                        {
                        }
                    );
            }

            

            public List<UploadEhsPatientItem> _SelectList(DbManagerProxy manager
                , on_action loading, on_action loaded
                )
            {
                var ret = _SelectListInternal(manager
                    , loading
                    , loaded
                    );
                  
                  return ret;
            }
      
            
            public virtual List<UploadEhsPatientItem> _SelectListInternal(DbManagerProxy manager
                , on_action loading, on_action loaded
                )
            {
                UploadEhsPatientItem _obj = null;
                try
                {
                    MapResultSet[] sets = new MapResultSet[1];
                    List<UploadEhsPatientItem> objs = new List<UploadEhsPatientItem>();
                    sets[0] = new MapResultSet(typeof(UploadEhsPatientItem), objs);
                    
                    manager
                        .SetSpCommand("spUploadEhsPatientItem_SelectDetail"
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
    
        
        
            internal void _SetupLoad(DbManagerProxy manager, UploadEhsPatientItem obj, bool bCloning = false)
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
            
            internal void _SetPermitions(IObjectPermissions permissions, UploadEhsPatientItem obj)
            {
                if (obj != null)
                {
                    obj._permissions = permissions;
                    if (obj._permissions != null)
                    {
                    
                    }
                }
            }

    

            private UploadEhsPatientItem _CreateNew(DbManagerProxy manager, IObject Parent, int? HACode, on_action creating, on_action created, bool isFake = false)
            {
                UploadEhsPatientItem obj = null;
                try
                {
                    obj = UploadEhsPatientItem.CreateInstance();
                    obj.m_CS = m_CS;
                    obj.m_IsNew = true;
                    obj.Parent = Parent;
                    
                    if (creating != null)
                        creating(obj);
                
                    // creating extenters - begin
                obj.validationErrors = new List<Tuple<string,string>>();
                obj.idfsUploadEhs = new Func<UploadEhsPatientItem, long>(c => c.master == null ? 0 : c.master.idfsUploadEhs)(obj);
                obj.idfsUploadEhsPatientItem = (new PositiveAutoIncrementExtender<UploadEhsPatientItem>()).GetScalar(manager, obj, isFake);
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

            
            public UploadEhsPatientItem CreateNewT(DbManagerProxy manager, IObject Parent, int? HACode = null)
            {
                return _CreateNew(manager, Parent, HACode, null, null);
            }
            public IObject CreateNew(DbManagerProxy manager, IObject Parent, int? HACode = null)
            {
                return _CreateNew(manager, Parent, HACode, null, null);
            }
            public UploadEhsPatientItem CreateFakeT(DbManagerProxy manager, IObject Parent, int? HACode = null)
            {
                return _CreateNew(manager, Parent, HACode, null, null, true);
            }
            public IObject CreateFake(DbManagerProxy manager, IObject Parent, int? HACode = null)
            {
                return _CreateNew(manager, Parent, HACode, null, null, true);
            }
            
            public UploadEhsPatientItem CreateWithParamsT(DbManagerProxy manager, IObject Parent, List<object> pars)
            {
                return _CreateNew(manager, Parent, null, null, null);
            }
            public IObject CreateWithParams(DbManagerProxy manager, IObject Parent, List<object> pars)
            {
                return _CreateNew(manager, Parent, null, null, null);
            }
            
            private void _SetupChildHandlers(UploadEhsPatientItem obj, object newobj)
            {
                
            }
            
            private void _SetupHandlers(UploadEhsPatientItem obj)
            {
                
            }
    

            internal void _LoadLookups(DbManagerProxy manager, UploadEhsPatientItem obj)
            {
                
            }
    
            [SprocName("spUploadEhsPatientItem_Post")]
            protected abstract void _post(DbManagerProxy manager, 
                [Direction.InputOutput("idfHumanActual")] UploadEhsPatientItem obj);
            protected void _postPredicate(DbManagerProxy manager, 
                [Direction.InputOutput("idfHumanActual")] UploadEhsPatientItem obj)
            {
                
                if (new Func<UploadEhsPatientItem, bool>(c => c.isValid)(obj))
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
                        UploadEhsPatientItem bo = obj as UploadEhsPatientItem;
                        
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
                        bSuccess = _PostNonTransaction(manager, obj as UploadEhsPatientItem, bChildObject);
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
            private bool _PostNonTransaction(DbManagerProxy manager, UploadEhsPatientItem obj, bool bChildObject) 
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
            
            public bool ValidateCanDelete(UploadEhsPatientItem obj)
            {
                using (DbManagerProxy manager = DbManagerFactory.Factory.Create(ModelUserContext.Instance))
                {
                    return ValidateCanDelete(manager, obj);
                }
            }
            public bool ValidateCanDelete(DbManagerProxy manager, UploadEhsPatientItem obj)
            {
            
                return true;
            }
        
      
            protected ValidationModelException ChainsValidate(UploadEhsPatientItem obj)
            {
                
                return null;
            }
            protected bool ChainsValidate(UploadEhsPatientItem obj, bool bRethrowException)
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
                return Validate(manager, obj as UploadEhsPatientItem, bPostValidation, bChangeValidation, bDeepValidation, bRethrowException);
            }
            public bool Validate(DbManagerProxy manager, UploadEhsPatientItem obj, bool bPostValidation, bool bChangeValidation, bool bDeepValidation, bool bRethrowException = false)
            {
                if (!ChainsValidate(obj, bRethrowException))
                    return false;
                    
                
                return true;
            }
           
    
            private void _SetupRequired(UploadEhsPatientItem obj)
            {
            
            }
    
    private void _SetupPersonalDataRestrictions(UploadEhsPatientItem obj)
    {
    
    
    }
  
            #region IObjectMeta
            public int? MaxSize(string name) { return Meta.Sizes.ContainsKey(name) ? (int?)Meta.Sizes[name] : null; }
            public bool RequiredByField(string name, IObject obj) { return Meta.RequiredByField.ContainsKey(name) ? Meta.RequiredByField[name](obj as UploadEhsPatientItem) : false; }
            public bool RequiredByProperty(string name, IObject obj) { return Meta.RequiredByProperty.ContainsKey(name) ? Meta.RequiredByProperty[name](obj as UploadEhsPatientItem) : false; }
            public List<SearchPanelMetaItem> SearchPanelMeta { get { return Meta.SearchPanelMeta; } }
            public List<GridMetaItem> GridMeta { get { return Meta.GridMeta; } }
            public List<ActionMetaItem> Actions { get { return Meta.Actions; } }
            public string DetailPanel { get { return "UploadEhsPatientItemDetail"; } }
            public string HelpIdWin { get { return ""; } }
            public string HelpIdWeb { get { return ""; } }
            public string HelpIdHh { get { return ""; } }
            public string SqlSortOrder { get { return Meta.sqlSortOrder; } }
            #endregion
    
        }

        
        #region Meta
        public static class Meta
        {
            public static string spSelect = "spUploadEhsPatientItem_SelectDetail";
            public static string spCount = "";
            public static string spPost = "spUploadEhsPatientItem_Post";
            public static string spInsert = "";
            public static string spUpdate = "";
            public static string spDelete = "";
            public static string spCanDelete = "";
            public static string sqlSortOrder = "";
            public static Dictionary<string, int> Sizes = new Dictionary<string, int>();
            public static Dictionary<string, Func<UploadEhsPatientItem, bool>> RequiredByField = new Dictionary<string, Func<UploadEhsPatientItem, bool>>();
            public static Dictionary<string, Func<UploadEhsPatientItem, bool>> RequiredByProperty = new Dictionary<string, Func<UploadEhsPatientItem, bool>>();
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
                Sizes.Add(_str_first_name, 200);
                Sizes.Add(_str_last_name, 200);
                Sizes.Add(_str_second_name, 200);
                Sizes.Add(_str_gender, 100);
                Sizes.Add(_str_phones, 200);
                Sizes.Add(_str_address_zip, 200);
                Sizes.Add(_str_address_area, 2000);
                Sizes.Add(_str_address_region, 2000);
                Sizes.Add(_str_address_settlement_type, 200);
                Sizes.Add(_str_address_settlement, 2000);
                Sizes.Add(_str_address_street_type, 200);
                Sizes.Add(_str_address_street, 200);
                Sizes.Add(_str_address_building, 200);
                Sizes.Add(_str_address_type, 200);
                Sizes.Add(_str_address_apartment, 200);
                Sizes.Add(_str_strPatientName, 1000);
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
	