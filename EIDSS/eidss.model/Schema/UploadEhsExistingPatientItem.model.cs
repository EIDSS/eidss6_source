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
    public abstract partial class UploadEhsExistingPatientItem : 
        EditableObject<UploadEhsExistingPatientItem>
        , IObject
        , IDisposable
        , ILookupUsage
        {
        
        [MapField(_str_idfHumanActual), NonUpdatable, PrimaryKey]
        public abstract Int64? idfHumanActual { get; set; }
                
        [LocalizedDisplayName("PatientName")]
        [MapField(_str_strPatientName)]
        public abstract String strPatientName { get; set; }
        protected String strPatientName_Original { get { return ((EditableValue<String>)((dynamic)this)._strPatientName).OriginalValue; } }
        protected String strPatientName_Previous { get { return ((EditableValue<String>)((dynamic)this)._strPatientName).PreviousValue; } }
                
        [LocalizedDisplayName(_str_patient_id)]
        [MapField(_str_patient_id)]
        public abstract String patient_id { get; set; }
        protected String patient_id_Original { get { return ((EditableValue<String>)((dynamic)this)._patient_id).OriginalValue; } }
        protected String patient_id_Previous { get { return ((EditableValue<String>)((dynamic)this)._patient_id).PreviousValue; } }
                
        [LocalizedDisplayName("ehsPatientId")]
        [MapField(_str_patient_id_EIDSS)]
        public abstract String patient_id_EIDSS { get; set; }
        protected String patient_id_EIDSS_Original { get { return ((EditableValue<String>)((dynamic)this)._patient_id_EIDSS).OriginalValue; } }
        protected String patient_id_EIDSS_Previous { get { return ((EditableValue<String>)((dynamic)this)._patient_id_EIDSS).PreviousValue; } }
                
        [LocalizedDisplayName(_str_patient_id_EHS)]
        [MapField(_str_patient_id_EHS)]
        public abstract String patient_id_EHS { get; set; }
        protected String patient_id_EHS_Original { get { return ((EditableValue<String>)((dynamic)this)._patient_id_EHS).OriginalValue; } }
        protected String patient_id_EHS_Previous { get { return ((EditableValue<String>)((dynamic)this)._patient_id_EHS).PreviousValue; } }
                
        [LocalizedDisplayName(_str_last_name)]
        [MapField(_str_last_name)]
        public abstract String last_name { get; set; }
        protected String last_name_Original { get { return ((EditableValue<String>)((dynamic)this)._last_name).OriginalValue; } }
        protected String last_name_Previous { get { return ((EditableValue<String>)((dynamic)this)._last_name).PreviousValue; } }
                
        [LocalizedDisplayName("strLastName")]
        [MapField(_str_last_name_EIDSS)]
        public abstract String last_name_EIDSS { get; set; }
        protected String last_name_EIDSS_Original { get { return ((EditableValue<String>)((dynamic)this)._last_name_EIDSS).OriginalValue; } }
        protected String last_name_EIDSS_Previous { get { return ((EditableValue<String>)((dynamic)this)._last_name_EIDSS).PreviousValue; } }
                
        [LocalizedDisplayName(_str_last_name_EHS)]
        [MapField(_str_last_name_EHS)]
        public abstract String last_name_EHS { get; set; }
        protected String last_name_EHS_Original { get { return ((EditableValue<String>)((dynamic)this)._last_name_EHS).OriginalValue; } }
        protected String last_name_EHS_Previous { get { return ((EditableValue<String>)((dynamic)this)._last_name_EHS).PreviousValue; } }
                
        [LocalizedDisplayName(_str_first_name)]
        [MapField(_str_first_name)]
        public abstract String first_name { get; set; }
        protected String first_name_Original { get { return ((EditableValue<String>)((dynamic)this)._first_name).OriginalValue; } }
        protected String first_name_Previous { get { return ((EditableValue<String>)((dynamic)this)._first_name).PreviousValue; } }
                
        [LocalizedDisplayName("strFirstName")]
        [MapField(_str_first_name_EIDSS)]
        public abstract String first_name_EIDSS { get; set; }
        protected String first_name_EIDSS_Original { get { return ((EditableValue<String>)((dynamic)this)._first_name_EIDSS).OriginalValue; } }
        protected String first_name_EIDSS_Previous { get { return ((EditableValue<String>)((dynamic)this)._first_name_EIDSS).PreviousValue; } }
                
        [LocalizedDisplayName(_str_first_name_EHS)]
        [MapField(_str_first_name_EHS)]
        public abstract String first_name_EHS { get; set; }
        protected String first_name_EHS_Original { get { return ((EditableValue<String>)((dynamic)this)._first_name_EHS).OriginalValue; } }
        protected String first_name_EHS_Previous { get { return ((EditableValue<String>)((dynamic)this)._first_name_EHS).PreviousValue; } }
                
        [LocalizedDisplayName(_str_second_name)]
        [MapField(_str_second_name)]
        public abstract String second_name { get; set; }
        protected String second_name_Original { get { return ((EditableValue<String>)((dynamic)this)._second_name).OriginalValue; } }
        protected String second_name_Previous { get { return ((EditableValue<String>)((dynamic)this)._second_name).PreviousValue; } }
                
        [LocalizedDisplayName("strSecondName")]
        [MapField(_str_second_name_EIDSS)]
        public abstract String second_name_EIDSS { get; set; }
        protected String second_name_EIDSS_Original { get { return ((EditableValue<String>)((dynamic)this)._second_name_EIDSS).OriginalValue; } }
        protected String second_name_EIDSS_Previous { get { return ((EditableValue<String>)((dynamic)this)._second_name_EIDSS).PreviousValue; } }
                
        [LocalizedDisplayName(_str_second_name_EHS)]
        [MapField(_str_second_name_EHS)]
        public abstract String second_name_EHS { get; set; }
        protected String second_name_EHS_Original { get { return ((EditableValue<String>)((dynamic)this)._second_name_EHS).OriginalValue; } }
        protected String second_name_EHS_Previous { get { return ((EditableValue<String>)((dynamic)this)._second_name_EHS).PreviousValue; } }
                
        [LocalizedDisplayName(_str_person_birth_date)]
        [MapField(_str_person_birth_date)]
        public abstract DateTime? person_birth_date { get; set; }
        protected DateTime? person_birth_date_Original { get { return ((EditableValue<DateTime?>)((dynamic)this)._person_birth_date).OriginalValue; } }
        protected DateTime? person_birth_date_Previous { get { return ((EditableValue<DateTime?>)((dynamic)this)._person_birth_date).PreviousValue; } }
                
        [LocalizedDisplayName("datDateofBirth")]
        [MapField(_str_person_birth_date_EIDSS)]
        public abstract DateTime? person_birth_date_EIDSS { get; set; }
        protected DateTime? person_birth_date_EIDSS_Original { get { return ((EditableValue<DateTime?>)((dynamic)this)._person_birth_date_EIDSS).OriginalValue; } }
        protected DateTime? person_birth_date_EIDSS_Previous { get { return ((EditableValue<DateTime?>)((dynamic)this)._person_birth_date_EIDSS).PreviousValue; } }
                
        [LocalizedDisplayName(_str_person_birth_date_EHS)]
        [MapField(_str_person_birth_date_EHS)]
        public abstract DateTime? person_birth_date_EHS { get; set; }
        protected DateTime? person_birth_date_EHS_Original { get { return ((EditableValue<DateTime?>)((dynamic)this)._person_birth_date_EHS).OriginalValue; } }
        protected DateTime? person_birth_date_EHS_Previous { get { return ((EditableValue<DateTime?>)((dynamic)this)._person_birth_date_EHS).PreviousValue; } }
                
        [LocalizedDisplayName(_str_gender)]
        [MapField(_str_gender)]
        public abstract String gender { get; set; }
        protected String gender_Original { get { return ((EditableValue<String>)((dynamic)this)._gender).OriginalValue; } }
        protected String gender_Previous { get { return ((EditableValue<String>)((dynamic)this)._gender).PreviousValue; } }
                
        [LocalizedDisplayName("idfsSex")]
        [MapField(_str_gender_EIDSS)]
        public abstract String gender_EIDSS { get; set; }
        protected String gender_EIDSS_Original { get { return ((EditableValue<String>)((dynamic)this)._gender_EIDSS).OriginalValue; } }
        protected String gender_EIDSS_Previous { get { return ((EditableValue<String>)((dynamic)this)._gender_EIDSS).PreviousValue; } }
                
        [LocalizedDisplayName(_str_gender_EHS)]
        [MapField(_str_gender_EHS)]
        public abstract String gender_EHS { get; set; }
        protected String gender_EHS_Original { get { return ((EditableValue<String>)((dynamic)this)._gender_EHS).OriginalValue; } }
        protected String gender_EHS_Previous { get { return ((EditableValue<String>)((dynamic)this)._gender_EHS).PreviousValue; } }
                
        [LocalizedDisplayName(_str_phones)]
        [MapField(_str_phones)]
        public abstract String phones { get; set; }
        protected String phones_Original { get { return ((EditableValue<String>)((dynamic)this)._phones).OriginalValue; } }
        protected String phones_Previous { get { return ((EditableValue<String>)((dynamic)this)._phones).PreviousValue; } }
                
        [LocalizedDisplayName("strHomePhone")]
        [MapField(_str_phones_EIDSS)]
        public abstract String phones_EIDSS { get; set; }
        protected String phones_EIDSS_Original { get { return ((EditableValue<String>)((dynamic)this)._phones_EIDSS).OriginalValue; } }
        protected String phones_EIDSS_Previous { get { return ((EditableValue<String>)((dynamic)this)._phones_EIDSS).PreviousValue; } }
                
        [LocalizedDisplayName(_str_phones_EHS)]
        [MapField(_str_phones_EHS)]
        public abstract String phones_EHS { get; set; }
        protected String phones_EHS_Original { get { return ((EditableValue<String>)((dynamic)this)._phones_EHS).OriginalValue; } }
        protected String phones_EHS_Previous { get { return ((EditableValue<String>)((dynamic)this)._phones_EHS).PreviousValue; } }
                
        [LocalizedDisplayName(_str_address_zip)]
        [MapField(_str_address_zip)]
        public abstract String address_zip { get; set; }
        protected String address_zip_Original { get { return ((EditableValue<String>)((dynamic)this)._address_zip).OriginalValue; } }
        protected String address_zip_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_zip).PreviousValue; } }
                
        [LocalizedDisplayName("strPostCode")]
        [MapField(_str_address_zip_EIDSS)]
        public abstract String address_zip_EIDSS { get; set; }
        protected String address_zip_EIDSS_Original { get { return ((EditableValue<String>)((dynamic)this)._address_zip_EIDSS).OriginalValue; } }
        protected String address_zip_EIDSS_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_zip_EIDSS).PreviousValue; } }
                
        [LocalizedDisplayName(_str_address_zip_EHS)]
        [MapField(_str_address_zip_EHS)]
        public abstract String address_zip_EHS { get; set; }
        protected String address_zip_EHS_Original { get { return ((EditableValue<String>)((dynamic)this)._address_zip_EHS).OriginalValue; } }
        protected String address_zip_EHS_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_zip_EHS).PreviousValue; } }
                
        [LocalizedDisplayName(_str_address_area)]
        [MapField(_str_address_area)]
        public abstract String address_area { get; set; }
        protected String address_area_Original { get { return ((EditableValue<String>)((dynamic)this)._address_area).OriginalValue; } }
        protected String address_area_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_area).PreviousValue; } }
                
        [LocalizedDisplayName("idfsRegion")]
        [MapField(_str_address_area_EIDSS)]
        public abstract String address_area_EIDSS { get; set; }
        protected String address_area_EIDSS_Original { get { return ((EditableValue<String>)((dynamic)this)._address_area_EIDSS).OriginalValue; } }
        protected String address_area_EIDSS_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_area_EIDSS).PreviousValue; } }
                
        [LocalizedDisplayName(_str_address_area_EHS)]
        [MapField(_str_address_area_EHS)]
        public abstract String address_area_EHS { get; set; }
        protected String address_area_EHS_Original { get { return ((EditableValue<String>)((dynamic)this)._address_area_EHS).OriginalValue; } }
        protected String address_area_EHS_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_area_EHS).PreviousValue; } }
                
        [LocalizedDisplayName(_str_address_region)]
        [MapField(_str_address_region)]
        public abstract String address_region { get; set; }
        protected String address_region_Original { get { return ((EditableValue<String>)((dynamic)this)._address_region).OriginalValue; } }
        protected String address_region_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_region).PreviousValue; } }
                
        [LocalizedDisplayName("idfsRayon")]
        [MapField(_str_address_region_EIDSS)]
        public abstract String address_region_EIDSS { get; set; }
        protected String address_region_EIDSS_Original { get { return ((EditableValue<String>)((dynamic)this)._address_region_EIDSS).OriginalValue; } }
        protected String address_region_EIDSS_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_region_EIDSS).PreviousValue; } }
                
        [LocalizedDisplayName(_str_address_region_EHS)]
        [MapField(_str_address_region_EHS)]
        public abstract String address_region_EHS { get; set; }
        protected String address_region_EHS_Original { get { return ((EditableValue<String>)((dynamic)this)._address_region_EHS).OriginalValue; } }
        protected String address_region_EHS_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_region_EHS).PreviousValue; } }
                
        [LocalizedDisplayName(_str_address_settlement)]
        [MapField(_str_address_settlement)]
        public abstract String address_settlement { get; set; }
        protected String address_settlement_Original { get { return ((EditableValue<String>)((dynamic)this)._address_settlement).OriginalValue; } }
        protected String address_settlement_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_settlement).PreviousValue; } }
                
        [LocalizedDisplayName("idfsSettlement")]
        [MapField(_str_address_settlement_EIDSS)]
        public abstract String address_settlement_EIDSS { get; set; }
        protected String address_settlement_EIDSS_Original { get { return ((EditableValue<String>)((dynamic)this)._address_settlement_EIDSS).OriginalValue; } }
        protected String address_settlement_EIDSS_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_settlement_EIDSS).PreviousValue; } }
                
        [LocalizedDisplayName(_str_address_settlement_EHS)]
        [MapField(_str_address_settlement_EHS)]
        public abstract String address_settlement_EHS { get; set; }
        protected String address_settlement_EHS_Original { get { return ((EditableValue<String>)((dynamic)this)._address_settlement_EHS).OriginalValue; } }
        protected String address_settlement_EHS_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_settlement_EHS).PreviousValue; } }
                
        [LocalizedDisplayName(_str_address_street)]
        [MapField(_str_address_street)]
        public abstract String address_street { get; set; }
        protected String address_street_Original { get { return ((EditableValue<String>)((dynamic)this)._address_street).OriginalValue; } }
        protected String address_street_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_street).PreviousValue; } }
                
        [LocalizedDisplayName("strStreetName")]
        [MapField(_str_address_street_EIDSS)]
        public abstract String address_street_EIDSS { get; set; }
        protected String address_street_EIDSS_Original { get { return ((EditableValue<String>)((dynamic)this)._address_street_EIDSS).OriginalValue; } }
        protected String address_street_EIDSS_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_street_EIDSS).PreviousValue; } }
                
        [LocalizedDisplayName(_str_address_street_EHS)]
        [MapField(_str_address_street_EHS)]
        public abstract String address_street_EHS { get; set; }
        protected String address_street_EHS_Original { get { return ((EditableValue<String>)((dynamic)this)._address_street_EHS).OriginalValue; } }
        protected String address_street_EHS_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_street_EHS).PreviousValue; } }
                
        [LocalizedDisplayName(_str_address_building)]
        [MapField(_str_address_building)]
        public abstract String address_building { get; set; }
        protected String address_building_Original { get { return ((EditableValue<String>)((dynamic)this)._address_building).OriginalValue; } }
        protected String address_building_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_building).PreviousValue; } }
                
        [LocalizedDisplayName("strHouse")]
        [MapField(_str_address_building_EIDSS)]
        public abstract String address_building_EIDSS { get; set; }
        protected String address_building_EIDSS_Original { get { return ((EditableValue<String>)((dynamic)this)._address_building_EIDSS).OriginalValue; } }
        protected String address_building_EIDSS_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_building_EIDSS).PreviousValue; } }
                
        [LocalizedDisplayName(_str_address_building_EHS)]
        [MapField(_str_address_building_EHS)]
        public abstract String address_building_EHS { get; set; }
        protected String address_building_EHS_Original { get { return ((EditableValue<String>)((dynamic)this)._address_building_EHS).OriginalValue; } }
        protected String address_building_EHS_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_building_EHS).PreviousValue; } }
                
        [LocalizedDisplayName(_str_address_apartment)]
        [MapField(_str_address_apartment)]
        public abstract String address_apartment { get; set; }
        protected String address_apartment_Original { get { return ((EditableValue<String>)((dynamic)this)._address_apartment).OriginalValue; } }
        protected String address_apartment_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_apartment).PreviousValue; } }
                
        [LocalizedDisplayName("strApartment")]
        [MapField(_str_address_apartment_EIDSS)]
        public abstract String address_apartment_EIDSS { get; set; }
        protected String address_apartment_EIDSS_Original { get { return ((EditableValue<String>)((dynamic)this)._address_apartment_EIDSS).OriginalValue; } }
        protected String address_apartment_EIDSS_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_apartment_EIDSS).PreviousValue; } }
                
        [LocalizedDisplayName(_str_address_apartment_EHS)]
        [MapField(_str_address_apartment_EHS)]
        public abstract String address_apartment_EHS { get; set; }
        protected String address_apartment_EHS_Original { get { return ((EditableValue<String>)((dynamic)this)._address_apartment_EHS).OriginalValue; } }
        protected String address_apartment_EHS_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_apartment_EHS).PreviousValue; } }
                
        [LocalizedDisplayName(_str_address_type)]
        [MapField(_str_address_type)]
        public abstract String address_type { get; set; }
        protected String address_type_Original { get { return ((EditableValue<String>)((dynamic)this)._address_type).OriginalValue; } }
        protected String address_type_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_type).PreviousValue; } }
                
        [LocalizedDisplayName(_str_address_type_EIDSS)]
        [MapField(_str_address_type_EIDSS)]
        public abstract String address_type_EIDSS { get; set; }
        protected String address_type_EIDSS_Original { get { return ((EditableValue<String>)((dynamic)this)._address_type_EIDSS).OriginalValue; } }
        protected String address_type_EIDSS_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_type_EIDSS).PreviousValue; } }
                
        [LocalizedDisplayName(_str_address_type_EHS)]
        [MapField(_str_address_type_EHS)]
        public abstract String address_type_EHS { get; set; }
        protected String address_type_EHS_Original { get { return ((EditableValue<String>)((dynamic)this)._address_type_EHS).OriginalValue; } }
        protected String address_type_EHS_Previous { get { return ((EditableValue<String>)((dynamic)this)._address_type_EHS).PreviousValue; } }
                
        [LocalizedDisplayName(_str_Resolution)]
        [MapField(_str_Resolution)]
        public abstract Int32? Resolution { get; set; }
        protected Int32? Resolution_Original { get { return ((EditableValue<Int32?>)((dynamic)this)._resolution).OriginalValue; } }
        protected Int32? Resolution_Previous { get { return ((EditableValue<Int32?>)((dynamic)this)._resolution).PreviousValue; } }
                
        [LocalizedDisplayName(_str_idfsUploadEhs)]
        [MapField(_str_idfsUploadEhs)]
        public abstract Int64 idfsUploadEhs { get; set; }
        protected Int64 idfsUploadEhs_Original { get { return ((EditableValue<Int64>)((dynamic)this)._idfsUploadEhs).OriginalValue; } }
        protected Int64 idfsUploadEhs_Previous { get { return ((EditableValue<Int64>)((dynamic)this)._idfsUploadEhs).PreviousValue; } }
                
        [LocalizedDisplayName(_str_idfsUploadEhsPatientItem)]
        [MapField(_str_idfsUploadEhsPatientItem)]
        public abstract Int64? idfsUploadEhsPatientItem { get; set; }
        protected Int64? idfsUploadEhsPatientItem_Original { get { return ((EditableValue<Int64?>)((dynamic)this)._idfsUploadEhsPatientItem).OriginalValue; } }
        protected Int64? idfsUploadEhsPatientItem_Previous { get { return ((EditableValue<Int64?>)((dynamic)this)._idfsUploadEhsPatientItem).PreviousValue; } }
                
        [LocalizedDisplayName(_str_idfsNavigatorItem)]
        [MapField(_str_idfsNavigatorItem)]
        public abstract Int64? idfsNavigatorItem { get; set; }
        protected Int64? idfsNavigatorItem_Original { get { return ((EditableValue<Int64?>)((dynamic)this)._idfsNavigatorItem).OriginalValue; } }
        protected Int64? idfsNavigatorItem_Previous { get { return ((EditableValue<Int64?>)((dynamic)this)._idfsNavigatorItem).PreviousValue; } }
                
        #region Set/Get values
        #region filed_info definifion
        protected class field_info
        {
            internal string _name;
            internal string _formname;
            internal string _type;
            internal Func<UploadEhsExistingPatientItem, object> _get_func;
            internal Action<UploadEhsExistingPatientItem, string> _set_func;
            internal Action<UploadEhsExistingPatientItem, UploadEhsExistingPatientItem, CompareModel, DbManagerProxy> _compare_func;
        }
        internal const string _str_Parent = "Parent";
        internal const string _str_IsNew = "IsNew";
        
        internal const string _str_idfHumanActual = "idfHumanActual";
        internal const string _str_strPatientName = "strPatientName";
        internal const string _str_patient_id = "patient_id";
        internal const string _str_patient_id_EIDSS = "patient_id_EIDSS";
        internal const string _str_patient_id_EHS = "patient_id_EHS";
        internal const string _str_last_name = "last_name";
        internal const string _str_last_name_EIDSS = "last_name_EIDSS";
        internal const string _str_last_name_EHS = "last_name_EHS";
        internal const string _str_first_name = "first_name";
        internal const string _str_first_name_EIDSS = "first_name_EIDSS";
        internal const string _str_first_name_EHS = "first_name_EHS";
        internal const string _str_second_name = "second_name";
        internal const string _str_second_name_EIDSS = "second_name_EIDSS";
        internal const string _str_second_name_EHS = "second_name_EHS";
        internal const string _str_person_birth_date = "person_birth_date";
        internal const string _str_person_birth_date_EIDSS = "person_birth_date_EIDSS";
        internal const string _str_person_birth_date_EHS = "person_birth_date_EHS";
        internal const string _str_gender = "gender";
        internal const string _str_gender_EIDSS = "gender_EIDSS";
        internal const string _str_gender_EHS = "gender_EHS";
        internal const string _str_phones = "phones";
        internal const string _str_phones_EIDSS = "phones_EIDSS";
        internal const string _str_phones_EHS = "phones_EHS";
        internal const string _str_address_zip = "address_zip";
        internal const string _str_address_zip_EIDSS = "address_zip_EIDSS";
        internal const string _str_address_zip_EHS = "address_zip_EHS";
        internal const string _str_address_area = "address_area";
        internal const string _str_address_area_EIDSS = "address_area_EIDSS";
        internal const string _str_address_area_EHS = "address_area_EHS";
        internal const string _str_address_region = "address_region";
        internal const string _str_address_region_EIDSS = "address_region_EIDSS";
        internal const string _str_address_region_EHS = "address_region_EHS";
        internal const string _str_address_settlement = "address_settlement";
        internal const string _str_address_settlement_EIDSS = "address_settlement_EIDSS";
        internal const string _str_address_settlement_EHS = "address_settlement_EHS";
        internal const string _str_address_street = "address_street";
        internal const string _str_address_street_EIDSS = "address_street_EIDSS";
        internal const string _str_address_street_EHS = "address_street_EHS";
        internal const string _str_address_building = "address_building";
        internal const string _str_address_building_EIDSS = "address_building_EIDSS";
        internal const string _str_address_building_EHS = "address_building_EHS";
        internal const string _str_address_apartment = "address_apartment";
        internal const string _str_address_apartment_EIDSS = "address_apartment_EIDSS";
        internal const string _str_address_apartment_EHS = "address_apartment_EHS";
        internal const string _str_address_type = "address_type";
        internal const string _str_address_type_EIDSS = "address_type_EIDSS";
        internal const string _str_address_type_EHS = "address_type_EHS";
        internal const string _str_Resolution = "Resolution";
        internal const string _str_idfsUploadEhs = "idfsUploadEhs";
        internal const string _str_idfsUploadEhsPatientItem = "idfsUploadEhsPatientItem";
        internal const string _str_idfsNavigatorItem = "idfsNavigatorItem";
        internal const string _str_master = "master";
        internal const string _str_strStatus = "strStatus";
        internal const string _str_strStatusPresent = "strStatusPresent";
        internal const string _str_Resolved = "Resolved";
        private static readonly field_info[] _field_infos =
        {
                  
            new field_info {
              _name = _str_idfHumanActual, _formname = _str_idfHumanActual, _type = "Int64?",
              _get_func = o => o.idfHumanActual,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseInt64Nullable(val); if (o.idfHumanActual != newval) o.idfHumanActual = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.idfHumanActual != c.idfHumanActual || o.IsRIRPropChanged(_str_idfHumanActual, c)) 
                  m.Add(_str_idfHumanActual, o.ObjectIdent + _str_idfHumanActual, o.ObjectIdent2 + _str_idfHumanActual, o.ObjectIdent3 + _str_idfHumanActual, "Int64?", 
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
              _name = _str_patient_id_EIDSS, _formname = _str_patient_id_EIDSS, _type = "String",
              _get_func = o => o.patient_id_EIDSS,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.patient_id_EIDSS != newval) o.patient_id_EIDSS = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.patient_id_EIDSS != c.patient_id_EIDSS || o.IsRIRPropChanged(_str_patient_id_EIDSS, c)) 
                  m.Add(_str_patient_id_EIDSS, o.ObjectIdent + _str_patient_id_EIDSS, o.ObjectIdent2 + _str_patient_id_EIDSS, o.ObjectIdent3 + _str_patient_id_EIDSS, "String", 
                    o.patient_id_EIDSS == null ? "" : o.patient_id_EIDSS.ToString(),                  
                  o.IsReadOnly(_str_patient_id_EIDSS), o.IsInvisible(_str_patient_id_EIDSS), o.IsRequired(_str_patient_id_EIDSS)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_patient_id_EHS, _formname = _str_patient_id_EHS, _type = "String",
              _get_func = o => o.patient_id_EHS,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.patient_id_EHS != newval) o.patient_id_EHS = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.patient_id_EHS != c.patient_id_EHS || o.IsRIRPropChanged(_str_patient_id_EHS, c)) 
                  m.Add(_str_patient_id_EHS, o.ObjectIdent + _str_patient_id_EHS, o.ObjectIdent2 + _str_patient_id_EHS, o.ObjectIdent3 + _str_patient_id_EHS, "String", 
                    o.patient_id_EHS == null ? "" : o.patient_id_EHS.ToString(),                  
                  o.IsReadOnly(_str_patient_id_EHS), o.IsInvisible(_str_patient_id_EHS), o.IsRequired(_str_patient_id_EHS)); 
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
              _name = _str_last_name_EIDSS, _formname = _str_last_name_EIDSS, _type = "String",
              _get_func = o => o.last_name_EIDSS,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.last_name_EIDSS != newval) o.last_name_EIDSS = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.last_name_EIDSS != c.last_name_EIDSS || o.IsRIRPropChanged(_str_last_name_EIDSS, c)) 
                  m.Add(_str_last_name_EIDSS, o.ObjectIdent + _str_last_name_EIDSS, o.ObjectIdent2 + _str_last_name_EIDSS, o.ObjectIdent3 + _str_last_name_EIDSS, "String", 
                    o.last_name_EIDSS == null ? "" : o.last_name_EIDSS.ToString(),                  
                  o.IsReadOnly(_str_last_name_EIDSS), o.IsInvisible(_str_last_name_EIDSS), o.IsRequired(_str_last_name_EIDSS)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_last_name_EHS, _formname = _str_last_name_EHS, _type = "String",
              _get_func = o => o.last_name_EHS,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.last_name_EHS != newval) o.last_name_EHS = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.last_name_EHS != c.last_name_EHS || o.IsRIRPropChanged(_str_last_name_EHS, c)) 
                  m.Add(_str_last_name_EHS, o.ObjectIdent + _str_last_name_EHS, o.ObjectIdent2 + _str_last_name_EHS, o.ObjectIdent3 + _str_last_name_EHS, "String", 
                    o.last_name_EHS == null ? "" : o.last_name_EHS.ToString(),                  
                  o.IsReadOnly(_str_last_name_EHS), o.IsInvisible(_str_last_name_EHS), o.IsRequired(_str_last_name_EHS)); 
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
              _name = _str_first_name_EIDSS, _formname = _str_first_name_EIDSS, _type = "String",
              _get_func = o => o.first_name_EIDSS,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.first_name_EIDSS != newval) o.first_name_EIDSS = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.first_name_EIDSS != c.first_name_EIDSS || o.IsRIRPropChanged(_str_first_name_EIDSS, c)) 
                  m.Add(_str_first_name_EIDSS, o.ObjectIdent + _str_first_name_EIDSS, o.ObjectIdent2 + _str_first_name_EIDSS, o.ObjectIdent3 + _str_first_name_EIDSS, "String", 
                    o.first_name_EIDSS == null ? "" : o.first_name_EIDSS.ToString(),                  
                  o.IsReadOnly(_str_first_name_EIDSS), o.IsInvisible(_str_first_name_EIDSS), o.IsRequired(_str_first_name_EIDSS)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_first_name_EHS, _formname = _str_first_name_EHS, _type = "String",
              _get_func = o => o.first_name_EHS,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.first_name_EHS != newval) o.first_name_EHS = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.first_name_EHS != c.first_name_EHS || o.IsRIRPropChanged(_str_first_name_EHS, c)) 
                  m.Add(_str_first_name_EHS, o.ObjectIdent + _str_first_name_EHS, o.ObjectIdent2 + _str_first_name_EHS, o.ObjectIdent3 + _str_first_name_EHS, "String", 
                    o.first_name_EHS == null ? "" : o.first_name_EHS.ToString(),                  
                  o.IsReadOnly(_str_first_name_EHS), o.IsInvisible(_str_first_name_EHS), o.IsRequired(_str_first_name_EHS)); 
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
              _name = _str_second_name_EIDSS, _formname = _str_second_name_EIDSS, _type = "String",
              _get_func = o => o.second_name_EIDSS,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.second_name_EIDSS != newval) o.second_name_EIDSS = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.second_name_EIDSS != c.second_name_EIDSS || o.IsRIRPropChanged(_str_second_name_EIDSS, c)) 
                  m.Add(_str_second_name_EIDSS, o.ObjectIdent + _str_second_name_EIDSS, o.ObjectIdent2 + _str_second_name_EIDSS, o.ObjectIdent3 + _str_second_name_EIDSS, "String", 
                    o.second_name_EIDSS == null ? "" : o.second_name_EIDSS.ToString(),                  
                  o.IsReadOnly(_str_second_name_EIDSS), o.IsInvisible(_str_second_name_EIDSS), o.IsRequired(_str_second_name_EIDSS)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_second_name_EHS, _formname = _str_second_name_EHS, _type = "String",
              _get_func = o => o.second_name_EHS,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.second_name_EHS != newval) o.second_name_EHS = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.second_name_EHS != c.second_name_EHS || o.IsRIRPropChanged(_str_second_name_EHS, c)) 
                  m.Add(_str_second_name_EHS, o.ObjectIdent + _str_second_name_EHS, o.ObjectIdent2 + _str_second_name_EHS, o.ObjectIdent3 + _str_second_name_EHS, "String", 
                    o.second_name_EHS == null ? "" : o.second_name_EHS.ToString(),                  
                  o.IsReadOnly(_str_second_name_EHS), o.IsInvisible(_str_second_name_EHS), o.IsRequired(_str_second_name_EHS)); 
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
              _name = _str_person_birth_date_EIDSS, _formname = _str_person_birth_date_EIDSS, _type = "DateTime?",
              _get_func = o => o.person_birth_date_EIDSS,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseDateTimeNullable(val); if (o.person_birth_date_EIDSS != newval) o.person_birth_date_EIDSS = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.person_birth_date_EIDSS != c.person_birth_date_EIDSS || o.IsRIRPropChanged(_str_person_birth_date_EIDSS, c)) 
                  m.Add(_str_person_birth_date_EIDSS, o.ObjectIdent + _str_person_birth_date_EIDSS, o.ObjectIdent2 + _str_person_birth_date_EIDSS, o.ObjectIdent3 + _str_person_birth_date_EIDSS, "DateTime?", 
                    o.person_birth_date_EIDSS == null ? "" : o.person_birth_date_EIDSS.ToString(),                  
                  o.IsReadOnly(_str_person_birth_date_EIDSS), o.IsInvisible(_str_person_birth_date_EIDSS), o.IsRequired(_str_person_birth_date_EIDSS)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_person_birth_date_EHS, _formname = _str_person_birth_date_EHS, _type = "DateTime?",
              _get_func = o => o.person_birth_date_EHS,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseDateTimeNullable(val); if (o.person_birth_date_EHS != newval) o.person_birth_date_EHS = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.person_birth_date_EHS != c.person_birth_date_EHS || o.IsRIRPropChanged(_str_person_birth_date_EHS, c)) 
                  m.Add(_str_person_birth_date_EHS, o.ObjectIdent + _str_person_birth_date_EHS, o.ObjectIdent2 + _str_person_birth_date_EHS, o.ObjectIdent3 + _str_person_birth_date_EHS, "DateTime?", 
                    o.person_birth_date_EHS == null ? "" : o.person_birth_date_EHS.ToString(),                  
                  o.IsReadOnly(_str_person_birth_date_EHS), o.IsInvisible(_str_person_birth_date_EHS), o.IsRequired(_str_person_birth_date_EHS)); 
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
              _name = _str_gender_EIDSS, _formname = _str_gender_EIDSS, _type = "String",
              _get_func = o => o.gender_EIDSS,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.gender_EIDSS != newval) o.gender_EIDSS = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.gender_EIDSS != c.gender_EIDSS || o.IsRIRPropChanged(_str_gender_EIDSS, c)) 
                  m.Add(_str_gender_EIDSS, o.ObjectIdent + _str_gender_EIDSS, o.ObjectIdent2 + _str_gender_EIDSS, o.ObjectIdent3 + _str_gender_EIDSS, "String", 
                    o.gender_EIDSS == null ? "" : o.gender_EIDSS.ToString(),                  
                  o.IsReadOnly(_str_gender_EIDSS), o.IsInvisible(_str_gender_EIDSS), o.IsRequired(_str_gender_EIDSS)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_gender_EHS, _formname = _str_gender_EHS, _type = "String",
              _get_func = o => o.gender_EHS,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.gender_EHS != newval) o.gender_EHS = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.gender_EHS != c.gender_EHS || o.IsRIRPropChanged(_str_gender_EHS, c)) 
                  m.Add(_str_gender_EHS, o.ObjectIdent + _str_gender_EHS, o.ObjectIdent2 + _str_gender_EHS, o.ObjectIdent3 + _str_gender_EHS, "String", 
                    o.gender_EHS == null ? "" : o.gender_EHS.ToString(),                  
                  o.IsReadOnly(_str_gender_EHS), o.IsInvisible(_str_gender_EHS), o.IsRequired(_str_gender_EHS)); 
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
              _name = _str_phones_EIDSS, _formname = _str_phones_EIDSS, _type = "String",
              _get_func = o => o.phones_EIDSS,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.phones_EIDSS != newval) o.phones_EIDSS = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.phones_EIDSS != c.phones_EIDSS || o.IsRIRPropChanged(_str_phones_EIDSS, c)) 
                  m.Add(_str_phones_EIDSS, o.ObjectIdent + _str_phones_EIDSS, o.ObjectIdent2 + _str_phones_EIDSS, o.ObjectIdent3 + _str_phones_EIDSS, "String", 
                    o.phones_EIDSS == null ? "" : o.phones_EIDSS.ToString(),                  
                  o.IsReadOnly(_str_phones_EIDSS), o.IsInvisible(_str_phones_EIDSS), o.IsRequired(_str_phones_EIDSS)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_phones_EHS, _formname = _str_phones_EHS, _type = "String",
              _get_func = o => o.phones_EHS,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.phones_EHS != newval) o.phones_EHS = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.phones_EHS != c.phones_EHS || o.IsRIRPropChanged(_str_phones_EHS, c)) 
                  m.Add(_str_phones_EHS, o.ObjectIdent + _str_phones_EHS, o.ObjectIdent2 + _str_phones_EHS, o.ObjectIdent3 + _str_phones_EHS, "String", 
                    o.phones_EHS == null ? "" : o.phones_EHS.ToString(),                  
                  o.IsReadOnly(_str_phones_EHS), o.IsInvisible(_str_phones_EHS), o.IsRequired(_str_phones_EHS)); 
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
              _name = _str_address_zip_EIDSS, _formname = _str_address_zip_EIDSS, _type = "String",
              _get_func = o => o.address_zip_EIDSS,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.address_zip_EIDSS != newval) o.address_zip_EIDSS = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.address_zip_EIDSS != c.address_zip_EIDSS || o.IsRIRPropChanged(_str_address_zip_EIDSS, c)) 
                  m.Add(_str_address_zip_EIDSS, o.ObjectIdent + _str_address_zip_EIDSS, o.ObjectIdent2 + _str_address_zip_EIDSS, o.ObjectIdent3 + _str_address_zip_EIDSS, "String", 
                    o.address_zip_EIDSS == null ? "" : o.address_zip_EIDSS.ToString(),                  
                  o.IsReadOnly(_str_address_zip_EIDSS), o.IsInvisible(_str_address_zip_EIDSS), o.IsRequired(_str_address_zip_EIDSS)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_address_zip_EHS, _formname = _str_address_zip_EHS, _type = "String",
              _get_func = o => o.address_zip_EHS,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.address_zip_EHS != newval) o.address_zip_EHS = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.address_zip_EHS != c.address_zip_EHS || o.IsRIRPropChanged(_str_address_zip_EHS, c)) 
                  m.Add(_str_address_zip_EHS, o.ObjectIdent + _str_address_zip_EHS, o.ObjectIdent2 + _str_address_zip_EHS, o.ObjectIdent3 + _str_address_zip_EHS, "String", 
                    o.address_zip_EHS == null ? "" : o.address_zip_EHS.ToString(),                  
                  o.IsReadOnly(_str_address_zip_EHS), o.IsInvisible(_str_address_zip_EHS), o.IsRequired(_str_address_zip_EHS)); 
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
              _name = _str_address_area_EIDSS, _formname = _str_address_area_EIDSS, _type = "String",
              _get_func = o => o.address_area_EIDSS,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.address_area_EIDSS != newval) o.address_area_EIDSS = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.address_area_EIDSS != c.address_area_EIDSS || o.IsRIRPropChanged(_str_address_area_EIDSS, c)) 
                  m.Add(_str_address_area_EIDSS, o.ObjectIdent + _str_address_area_EIDSS, o.ObjectIdent2 + _str_address_area_EIDSS, o.ObjectIdent3 + _str_address_area_EIDSS, "String", 
                    o.address_area_EIDSS == null ? "" : o.address_area_EIDSS.ToString(),                  
                  o.IsReadOnly(_str_address_area_EIDSS), o.IsInvisible(_str_address_area_EIDSS), o.IsRequired(_str_address_area_EIDSS)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_address_area_EHS, _formname = _str_address_area_EHS, _type = "String",
              _get_func = o => o.address_area_EHS,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.address_area_EHS != newval) o.address_area_EHS = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.address_area_EHS != c.address_area_EHS || o.IsRIRPropChanged(_str_address_area_EHS, c)) 
                  m.Add(_str_address_area_EHS, o.ObjectIdent + _str_address_area_EHS, o.ObjectIdent2 + _str_address_area_EHS, o.ObjectIdent3 + _str_address_area_EHS, "String", 
                    o.address_area_EHS == null ? "" : o.address_area_EHS.ToString(),                  
                  o.IsReadOnly(_str_address_area_EHS), o.IsInvisible(_str_address_area_EHS), o.IsRequired(_str_address_area_EHS)); 
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
              _name = _str_address_region_EIDSS, _formname = _str_address_region_EIDSS, _type = "String",
              _get_func = o => o.address_region_EIDSS,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.address_region_EIDSS != newval) o.address_region_EIDSS = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.address_region_EIDSS != c.address_region_EIDSS || o.IsRIRPropChanged(_str_address_region_EIDSS, c)) 
                  m.Add(_str_address_region_EIDSS, o.ObjectIdent + _str_address_region_EIDSS, o.ObjectIdent2 + _str_address_region_EIDSS, o.ObjectIdent3 + _str_address_region_EIDSS, "String", 
                    o.address_region_EIDSS == null ? "" : o.address_region_EIDSS.ToString(),                  
                  o.IsReadOnly(_str_address_region_EIDSS), o.IsInvisible(_str_address_region_EIDSS), o.IsRequired(_str_address_region_EIDSS)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_address_region_EHS, _formname = _str_address_region_EHS, _type = "String",
              _get_func = o => o.address_region_EHS,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.address_region_EHS != newval) o.address_region_EHS = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.address_region_EHS != c.address_region_EHS || o.IsRIRPropChanged(_str_address_region_EHS, c)) 
                  m.Add(_str_address_region_EHS, o.ObjectIdent + _str_address_region_EHS, o.ObjectIdent2 + _str_address_region_EHS, o.ObjectIdent3 + _str_address_region_EHS, "String", 
                    o.address_region_EHS == null ? "" : o.address_region_EHS.ToString(),                  
                  o.IsReadOnly(_str_address_region_EHS), o.IsInvisible(_str_address_region_EHS), o.IsRequired(_str_address_region_EHS)); 
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
              _name = _str_address_settlement_EIDSS, _formname = _str_address_settlement_EIDSS, _type = "String",
              _get_func = o => o.address_settlement_EIDSS,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.address_settlement_EIDSS != newval) o.address_settlement_EIDSS = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.address_settlement_EIDSS != c.address_settlement_EIDSS || o.IsRIRPropChanged(_str_address_settlement_EIDSS, c)) 
                  m.Add(_str_address_settlement_EIDSS, o.ObjectIdent + _str_address_settlement_EIDSS, o.ObjectIdent2 + _str_address_settlement_EIDSS, o.ObjectIdent3 + _str_address_settlement_EIDSS, "String", 
                    o.address_settlement_EIDSS == null ? "" : o.address_settlement_EIDSS.ToString(),                  
                  o.IsReadOnly(_str_address_settlement_EIDSS), o.IsInvisible(_str_address_settlement_EIDSS), o.IsRequired(_str_address_settlement_EIDSS)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_address_settlement_EHS, _formname = _str_address_settlement_EHS, _type = "String",
              _get_func = o => o.address_settlement_EHS,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.address_settlement_EHS != newval) o.address_settlement_EHS = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.address_settlement_EHS != c.address_settlement_EHS || o.IsRIRPropChanged(_str_address_settlement_EHS, c)) 
                  m.Add(_str_address_settlement_EHS, o.ObjectIdent + _str_address_settlement_EHS, o.ObjectIdent2 + _str_address_settlement_EHS, o.ObjectIdent3 + _str_address_settlement_EHS, "String", 
                    o.address_settlement_EHS == null ? "" : o.address_settlement_EHS.ToString(),                  
                  o.IsReadOnly(_str_address_settlement_EHS), o.IsInvisible(_str_address_settlement_EHS), o.IsRequired(_str_address_settlement_EHS)); 
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
              _name = _str_address_street_EIDSS, _formname = _str_address_street_EIDSS, _type = "String",
              _get_func = o => o.address_street_EIDSS,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.address_street_EIDSS != newval) o.address_street_EIDSS = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.address_street_EIDSS != c.address_street_EIDSS || o.IsRIRPropChanged(_str_address_street_EIDSS, c)) 
                  m.Add(_str_address_street_EIDSS, o.ObjectIdent + _str_address_street_EIDSS, o.ObjectIdent2 + _str_address_street_EIDSS, o.ObjectIdent3 + _str_address_street_EIDSS, "String", 
                    o.address_street_EIDSS == null ? "" : o.address_street_EIDSS.ToString(),                  
                  o.IsReadOnly(_str_address_street_EIDSS), o.IsInvisible(_str_address_street_EIDSS), o.IsRequired(_str_address_street_EIDSS)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_address_street_EHS, _formname = _str_address_street_EHS, _type = "String",
              _get_func = o => o.address_street_EHS,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.address_street_EHS != newval) o.address_street_EHS = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.address_street_EHS != c.address_street_EHS || o.IsRIRPropChanged(_str_address_street_EHS, c)) 
                  m.Add(_str_address_street_EHS, o.ObjectIdent + _str_address_street_EHS, o.ObjectIdent2 + _str_address_street_EHS, o.ObjectIdent3 + _str_address_street_EHS, "String", 
                    o.address_street_EHS == null ? "" : o.address_street_EHS.ToString(),                  
                  o.IsReadOnly(_str_address_street_EHS), o.IsInvisible(_str_address_street_EHS), o.IsRequired(_str_address_street_EHS)); 
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
              _name = _str_address_building_EIDSS, _formname = _str_address_building_EIDSS, _type = "String",
              _get_func = o => o.address_building_EIDSS,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.address_building_EIDSS != newval) o.address_building_EIDSS = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.address_building_EIDSS != c.address_building_EIDSS || o.IsRIRPropChanged(_str_address_building_EIDSS, c)) 
                  m.Add(_str_address_building_EIDSS, o.ObjectIdent + _str_address_building_EIDSS, o.ObjectIdent2 + _str_address_building_EIDSS, o.ObjectIdent3 + _str_address_building_EIDSS, "String", 
                    o.address_building_EIDSS == null ? "" : o.address_building_EIDSS.ToString(),                  
                  o.IsReadOnly(_str_address_building_EIDSS), o.IsInvisible(_str_address_building_EIDSS), o.IsRequired(_str_address_building_EIDSS)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_address_building_EHS, _formname = _str_address_building_EHS, _type = "String",
              _get_func = o => o.address_building_EHS,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.address_building_EHS != newval) o.address_building_EHS = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.address_building_EHS != c.address_building_EHS || o.IsRIRPropChanged(_str_address_building_EHS, c)) 
                  m.Add(_str_address_building_EHS, o.ObjectIdent + _str_address_building_EHS, o.ObjectIdent2 + _str_address_building_EHS, o.ObjectIdent3 + _str_address_building_EHS, "String", 
                    o.address_building_EHS == null ? "" : o.address_building_EHS.ToString(),                  
                  o.IsReadOnly(_str_address_building_EHS), o.IsInvisible(_str_address_building_EHS), o.IsRequired(_str_address_building_EHS)); 
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
              _name = _str_address_apartment_EIDSS, _formname = _str_address_apartment_EIDSS, _type = "String",
              _get_func = o => o.address_apartment_EIDSS,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.address_apartment_EIDSS != newval) o.address_apartment_EIDSS = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.address_apartment_EIDSS != c.address_apartment_EIDSS || o.IsRIRPropChanged(_str_address_apartment_EIDSS, c)) 
                  m.Add(_str_address_apartment_EIDSS, o.ObjectIdent + _str_address_apartment_EIDSS, o.ObjectIdent2 + _str_address_apartment_EIDSS, o.ObjectIdent3 + _str_address_apartment_EIDSS, "String", 
                    o.address_apartment_EIDSS == null ? "" : o.address_apartment_EIDSS.ToString(),                  
                  o.IsReadOnly(_str_address_apartment_EIDSS), o.IsInvisible(_str_address_apartment_EIDSS), o.IsRequired(_str_address_apartment_EIDSS)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_address_apartment_EHS, _formname = _str_address_apartment_EHS, _type = "String",
              _get_func = o => o.address_apartment_EHS,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.address_apartment_EHS != newval) o.address_apartment_EHS = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.address_apartment_EHS != c.address_apartment_EHS || o.IsRIRPropChanged(_str_address_apartment_EHS, c)) 
                  m.Add(_str_address_apartment_EHS, o.ObjectIdent + _str_address_apartment_EHS, o.ObjectIdent2 + _str_address_apartment_EHS, o.ObjectIdent3 + _str_address_apartment_EHS, "String", 
                    o.address_apartment_EHS == null ? "" : o.address_apartment_EHS.ToString(),                  
                  o.IsReadOnly(_str_address_apartment_EHS), o.IsInvisible(_str_address_apartment_EHS), o.IsRequired(_str_address_apartment_EHS)); 
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
              _name = _str_address_type_EIDSS, _formname = _str_address_type_EIDSS, _type = "String",
              _get_func = o => o.address_type_EIDSS,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.address_type_EIDSS != newval) o.address_type_EIDSS = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.address_type_EIDSS != c.address_type_EIDSS || o.IsRIRPropChanged(_str_address_type_EIDSS, c)) 
                  m.Add(_str_address_type_EIDSS, o.ObjectIdent + _str_address_type_EIDSS, o.ObjectIdent2 + _str_address_type_EIDSS, o.ObjectIdent3 + _str_address_type_EIDSS, "String", 
                    o.address_type_EIDSS == null ? "" : o.address_type_EIDSS.ToString(),                  
                  o.IsReadOnly(_str_address_type_EIDSS), o.IsInvisible(_str_address_type_EIDSS), o.IsRequired(_str_address_type_EIDSS)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_address_type_EHS, _formname = _str_address_type_EHS, _type = "String",
              _get_func = o => o.address_type_EHS,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseString(val); if (o.address_type_EHS != newval) o.address_type_EHS = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.address_type_EHS != c.address_type_EHS || o.IsRIRPropChanged(_str_address_type_EHS, c)) 
                  m.Add(_str_address_type_EHS, o.ObjectIdent + _str_address_type_EHS, o.ObjectIdent2 + _str_address_type_EHS, o.ObjectIdent3 + _str_address_type_EHS, "String", 
                    o.address_type_EHS == null ? "" : o.address_type_EHS.ToString(),                  
                  o.IsReadOnly(_str_address_type_EHS), o.IsInvisible(_str_address_type_EHS), o.IsRequired(_str_address_type_EHS)); 
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
              _name = _str_idfsUploadEhsPatientItem, _formname = _str_idfsUploadEhsPatientItem, _type = "Int64?",
              _get_func = o => o.idfsUploadEhsPatientItem,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseInt64Nullable(val); if (o.idfsUploadEhsPatientItem != newval) o.idfsUploadEhsPatientItem = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.idfsUploadEhsPatientItem != c.idfsUploadEhsPatientItem || o.IsRIRPropChanged(_str_idfsUploadEhsPatientItem, c)) 
                  m.Add(_str_idfsUploadEhsPatientItem, o.ObjectIdent + _str_idfsUploadEhsPatientItem, o.ObjectIdent2 + _str_idfsUploadEhsPatientItem, o.ObjectIdent3 + _str_idfsUploadEhsPatientItem, "Int64?", 
                    o.idfsUploadEhsPatientItem == null ? "" : o.idfsUploadEhsPatientItem.ToString(),                  
                  o.IsReadOnly(_str_idfsUploadEhsPatientItem), o.IsInvisible(_str_idfsUploadEhsPatientItem), o.IsRequired(_str_idfsUploadEhsPatientItem)); 
                  }
              }, 
                  
            new field_info {
              _name = _str_idfsNavigatorItem, _formname = _str_idfsNavigatorItem, _type = "Int64?",
              _get_func = o => o.idfsNavigatorItem,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseInt64Nullable(val); if (o.idfsNavigatorItem != newval) o.idfsNavigatorItem = newval; },
              _compare_func = (o, c, m, g) => {
                if (o.idfsNavigatorItem != c.idfsNavigatorItem || o.IsRIRPropChanged(_str_idfsNavigatorItem, c)) 
                  m.Add(_str_idfsNavigatorItem, o.ObjectIdent + _str_idfsNavigatorItem, o.ObjectIdent2 + _str_idfsNavigatorItem, o.ObjectIdent3 + _str_idfsNavigatorItem, "Int64?", 
                    o.idfsNavigatorItem == null ? "" : o.idfsNavigatorItem.ToString(),                  
                  o.IsReadOnly(_str_idfsNavigatorItem), o.IsInvisible(_str_idfsNavigatorItem), o.IsRequired(_str_idfsNavigatorItem)); 
                  }
              }, 
        
            new field_info {
              _name = _str_Resolved, _formname = _str_Resolved, _type = "bool",
              _get_func = o => o.Resolved,
              _set_func = (o, val) => { var newval = ParsingHelper.ParseBoolean(val); if (o.Resolved != newval) o.Resolved = newval; },
              _compare_func = (o, c, m, g) => {
              
                if (o.Resolved != c.Resolved || o.IsRIRPropChanged(_str_Resolved, c)) {
                  m.Add(_str_Resolved, o.ObjectIdent + _str_Resolved, o.ObjectIdent2 + _str_Resolved, o.ObjectIdent3 + _str_Resolved,  "bool", 
                    o.Resolved == null ? "" : o.Resolved.ToString(),                  
                    o.IsReadOnly(_str_Resolved), o.IsInvisible(_str_Resolved), o.IsRequired(_str_Resolved));
                  }
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
            UploadEhsExistingPatientItem obj = (UploadEhsExistingPatientItem)o;
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
            get { return new Func<UploadEhsExistingPatientItem, UploadEhsMaster>(c => c.Parent as UploadEhsMaster)(this); }
            
        }
        
          [XmlIgnore]
          [LocalizedDisplayName(_str_strStatus)]
        public string strStatus
        {
            get { return new Func<UploadEhsExistingPatientItem, string>(c => eidss.model.Resources.EidssMessages.Get(c.Resolution.HasValue ? (c.Resolution.Value == (int)UploadEhsPatientResolution.Updated ? "rsUpdated" : (c.Resolution.Value == (int)UploadEhsPatientResolution.Created ? "rsCreated" : "rsDismissed")) : "rsUnspecified"))(this); }
            
        }
        
          [XmlIgnore]
          [LocalizedDisplayName(_str_strStatusPresent)]
        public string strStatusPresent
        {
            get { return new Func<UploadEhsExistingPatientItem, string>(c => eidss.model.Resources.EidssMessages.Get(c.Resolution.HasValue ? (c.Resolution.Value == (int)UploadEhsPatientResolution.Updated ? "rsUpdate" : (c.Resolution.Value == (int)UploadEhsPatientResolution.Created ? "rsCreate" : "rsDismiss")) : "rsUnspecified"))(this); }
            
        }
        
          [LocalizedDisplayName(_str_Resolved)]
        public bool Resolved
        {
            get { return m_Resolved; }
            set { if (m_Resolved != value) { m_Resolved = value; OnPropertyChanged(_str_Resolved); } }
        }
        private bool m_Resolved;
        
        protected CacheScope m_CS;
        protected Accessor _getAccessor() { return Accessor.Instance(m_CS); }
        private IObjectPermissions m_permissions = null;
        internal IObjectPermissions _permissions { get { return m_permissions; } set { m_permissions = value; } }
        internal string m_ObjectName = "UploadEhsExistingPatientItem";

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
            var ret = base.Clone() as UploadEhsExistingPatientItem;
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
            var ret = base.Clone() as UploadEhsExistingPatientItem;
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
        public UploadEhsExistingPatientItem CloneWithSetup()
        {
            using (DbManagerProxy manager = DbManagerFactory.Factory.Create(ModelUserContext.Instance))
            {
                return CloneWithSetup(manager) as UploadEhsExistingPatientItem;
            }
        }
        #endregion

        #region IObject implementation
        public object Key { get { return idfHumanActual; } }
        public string KeyName { get { return "idfHumanActual"; } }
        public object KeyLookup { get { return idfHumanActual; } }
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

        private bool IsRIRPropChanged(string fld, UploadEhsExistingPatientItem c)
        {
            return IsReadOnly(fld) != c.IsReadOnly(fld) || IsInvisible(fld) != c.IsInvisible(fld) || IsRequired(fld) != c._isRequired(m_isRequired, fld);
        }
        private bool IsLookupContentChanged(DbManagerProxy manager, string fld, UploadEhsExistingPatientItem c)
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
        

      

        public UploadEhsExistingPatientItem()
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
            PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(UploadEhsExistingPatientItem_PropertyChanged);
        }
        public void _RevokeMainHandler()
        {
            PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(UploadEhsExistingPatientItem_PropertyChanged);
        }
        private void UploadEhsExistingPatientItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            (sender as UploadEhsExistingPatientItem).Changed(e.PropertyName);
            
            if (e.PropertyName == _str_Parent)
                OnPropertyChanged(_str_master);
                  
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
            UploadEhsExistingPatientItem obj = this;
            
        }
        private void _DeletedExtenders()
        {
            UploadEhsExistingPatientItem obj = this;
            
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

    
        private static string[] readonly_names1 = "idfHumanActual".Split(new char[] { ',' });
        
        private static string[] readonly_names2 = "strPatientName".Split(new char[] { ',' });
        
        private static string[] readonly_names3 = "patient_id".Split(new char[] { ',' });
        
        private static string[] readonly_names4 = "patient_id_EIDSS".Split(new char[] { ',' });
        
        private static string[] readonly_names5 = "patient_id_EHS".Split(new char[] { ',' });
        
        private static string[] readonly_names6 = "last_name".Split(new char[] { ',' });
        
        private static string[] readonly_names7 = "last_name_EIDSS".Split(new char[] { ',' });
        
        private static string[] readonly_names8 = "last_name_EHS".Split(new char[] { ',' });
        
        private static string[] readonly_names9 = "first_name".Split(new char[] { ',' });
        
        private static string[] readonly_names10 = "first_name_EIDSS".Split(new char[] { ',' });
        
        private static string[] readonly_names11 = "first_name_EHS".Split(new char[] { ',' });
        
        private static string[] readonly_names12 = "second_name".Split(new char[] { ',' });
        
        private static string[] readonly_names13 = "second_name_EIDSS".Split(new char[] { ',' });
        
        private static string[] readonly_names14 = "second_name_EHS".Split(new char[] { ',' });
        
        private static string[] readonly_names15 = "person_birth_date".Split(new char[] { ',' });
        
        private static string[] readonly_names16 = "person_birth_date_EIDSS".Split(new char[] { ',' });
        
        private static string[] readonly_names17 = "person_birth_date_EHS".Split(new char[] { ',' });
        
        private static string[] readonly_names18 = "gender".Split(new char[] { ',' });
        
        private static string[] readonly_names19 = "gender_EIDSS".Split(new char[] { ',' });
        
        private static string[] readonly_names20 = "gender_EHS".Split(new char[] { ',' });
        
        private static string[] readonly_names21 = "phones".Split(new char[] { ',' });
        
        private static string[] readonly_names22 = "phones_EIDSS".Split(new char[] { ',' });
        
        private static string[] readonly_names23 = "phones_EHS".Split(new char[] { ',' });
        
        private static string[] readonly_names24 = "address_zip".Split(new char[] { ',' });
        
        private static string[] readonly_names25 = "address_zip_EIDSS".Split(new char[] { ',' });
        
        private static string[] readonly_names26 = "address_zip_EHS".Split(new char[] { ',' });
        
        private static string[] readonly_names27 = "address_area".Split(new char[] { ',' });
        
        private static string[] readonly_names28 = "address_area_EIDSS".Split(new char[] { ',' });
        
        private static string[] readonly_names29 = "address_area_EHS".Split(new char[] { ',' });
        
        private static string[] readonly_names30 = "address_region".Split(new char[] { ',' });
        
        private static string[] readonly_names31 = "address_region_EIDSS".Split(new char[] { ',' });
        
        private static string[] readonly_names32 = "address_region_EHS".Split(new char[] { ',' });
        
        private static string[] readonly_names33 = "address_settlement".Split(new char[] { ',' });
        
        private static string[] readonly_names34 = "address_settlement_EIDSS".Split(new char[] { ',' });
        
        private static string[] readonly_names35 = "address_settlement_EHS".Split(new char[] { ',' });
        
        private static string[] readonly_names36 = "address_street".Split(new char[] { ',' });
        
        private static string[] readonly_names37 = "address_street_EIDSS".Split(new char[] { ',' });
        
        private static string[] readonly_names38 = "address_street_EHS".Split(new char[] { ',' });
        
        private static string[] readonly_names39 = "address_building".Split(new char[] { ',' });
        
        private static string[] readonly_names40 = "address_building_EIDSS".Split(new char[] { ',' });
        
        private static string[] readonly_names41 = "address_building_EHS".Split(new char[] { ',' });
        
        private static string[] readonly_names42 = "address_apartment".Split(new char[] { ',' });
        
        private static string[] readonly_names43 = "address_apartment_EIDSS".Split(new char[] { ',' });
        
        private static string[] readonly_names44 = "address_apartment_EHS".Split(new char[] { ',' });
        
        private static string[] readonly_names45 = "address_type".Split(new char[] { ',' });
        
        private static string[] readonly_names46 = "address_type_EIDSS".Split(new char[] { ',' });
        
        private static string[] readonly_names47 = "address_type_EHS".Split(new char[] { ',' });
        
        private static string[] readonly_names48 = "idfsUploadEhs".Split(new char[] { ',' });
        
        private static string[] readonly_names49 = "idfsUploadEhsPatientItem".Split(new char[] { ',' });
        
        private static string[] readonly_names50 = "idfsNavigatorItem".Split(new char[] { ',' });
        
        private static string[] readonly_names51 = "strStatusPresent".Split(new char[] { ',' });
        
        private bool _isReadOnly(string name)
        {
            
            if (readonly_names1.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names2.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names3.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names4.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names5.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names6.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names7.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names8.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names9.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names10.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names11.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names12.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names13.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names14.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names15.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names16.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names17.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names18.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names19.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names20.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names21.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names22.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names23.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names24.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names25.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names26.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names27.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names28.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names29.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names30.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names31.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names32.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names33.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names34.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names35.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names36.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names37.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names38.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names39.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names40.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names41.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names42.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names43.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names44.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names45.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names46.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names47.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names48.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names49.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names50.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
            if (readonly_names51.Where(c => c == name).Count() > 0)
                return ReadOnly || new Func<UploadEhsExistingPatientItem, bool>(c => true)(this);
            
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


        internal Dictionary<string, Func<UploadEhsExistingPatientItem, bool>> m_isRequired;
        private bool _isRequired(Dictionary<string, Func<UploadEhsExistingPatientItem, bool>> isRequiredDict, string name)
        {
            if (isRequiredDict != null && isRequiredDict.ContainsKey(name))
                return isRequiredDict[name](this);
            return false;
        }
        
        public void AddRequired(string name, Func<UploadEhsExistingPatientItem, bool> func)
        {
            if (m_isRequired == null) 
                m_isRequired = new Dictionary<string, Func<UploadEhsExistingPatientItem, bool>>();
            if (!m_isRequired.ContainsKey(name))
                m_isRequired.Add(name, func);
        }
    
    internal Dictionary<string, Func<UploadEhsExistingPatientItem, bool>> m_isHiddenPersonalData;
    private bool _isHiddenPersonalData(string name)
    {
      if (m_isHiddenPersonalData != null && m_isHiddenPersonalData.ContainsKey(name))
        return m_isHiddenPersonalData[name](this);
      return false;
    }

    public void AddHiddenPersonalData(string name, Func<UploadEhsExistingPatientItem, bool> func)
    {
      if (m_isHiddenPersonalData == null)
        m_isHiddenPersonalData = new Dictionary<string, Func<UploadEhsExistingPatientItem, bool>>();
      if (!m_isHiddenPersonalData.ContainsKey(name))
        m_isHiddenPersonalData.Add(name, func);
    }
  
        #region IDisposable Members
        partial void Disposed();
        private bool bIsDisposed;
        protected bool bNeedLookupRemove;
        ~UploadEhsExistingPatientItem()
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
        : DataAccessor<UploadEhsExistingPatientItem>
            , IObjectAccessor
            , IObjectMeta
            , IObjectValidator
            
            , IObjectCreator
            , IObjectCreator<UploadEhsExistingPatientItem>
            
            , IObjectSelectDetailList
            , IObjectPost
                
        {
            #region IObjectAccessor
            public string KeyName { get { return "idfHumanActual"; } }
            #endregion
        
            public delegate void on_action(UploadEhsExistingPatientItem obj);
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
                    , (Int64?)ident
                        
                    , null
                    , null
                    ).Cast<IObject>().ToList();
            }
            
        
            public virtual List<UploadEhsExistingPatientItem> SelectList(DbManagerProxy manager
                , Int64? idfsUploadEhs
                )
            {
                return _SelectList(manager
                    , idfsUploadEhs
                    , delegate(UploadEhsExistingPatientItem obj)
                        {
                        }
                    , delegate(UploadEhsExistingPatientItem obj)
                        {
                        }
                    );
            }

            

            public List<UploadEhsExistingPatientItem> _SelectList(DbManagerProxy manager
                , Int64? idfsUploadEhs
                , on_action loading, on_action loaded
                )
            {
                var ret = _SelectListInternal(manager
                    , idfsUploadEhs
                    , loading
                    , loaded
                    );
                  
                  return ret;
            }
      
            
            public virtual List<UploadEhsExistingPatientItem> _SelectListInternal(DbManagerProxy manager
                , Int64? idfsUploadEhs
                , on_action loading, on_action loaded
                )
            {
                UploadEhsExistingPatientItem _obj = null;
                try
                {
                    MapResultSet[] sets = new MapResultSet[1];
                    List<UploadEhsExistingPatientItem> objs = new List<UploadEhsExistingPatientItem>();
                    sets[0] = new MapResultSet(typeof(UploadEhsExistingPatientItem), objs);
                    
                    manager
                        .SetSpCommand("spUploadEhsExistingPatientItem_SelectList"
                            , manager.Parameter("@idfsUploadEhs", idfsUploadEhs)
                            , manager.Parameter("@LangID", ModelUserContext.CurrentLanguage)
                            
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
            public virtual UploadEhsExistingPatientItem SelectDetailT(DbManagerProxy manager, object ident, int? HACode = null)
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

            
            public virtual UploadEhsExistingPatientItem SelectByKey(DbManagerProxy manager
                , Int64? idfsUploadEhs
                )
            {
                return _SelectByKey(manager
                    , idfsUploadEhs
                    , null, null
                    );
            }
            

            private UploadEhsExistingPatientItem _SelectByKey(DbManagerProxy manager
                , Int64? idfsUploadEhs
                , on_action loading, on_action loaded
                )
            {
                return _SelectByKeyInternal(manager
                    , idfsUploadEhs
                    , loading, loaded
                    )
                    
                    ;
            }
      
            
            protected virtual UploadEhsExistingPatientItem _SelectByKeyInternal(DbManagerProxy manager
                , Int64? idfsUploadEhs
                , on_action loading, on_action loaded
                )
            {
            
                MapResultSet[] sets = new MapResultSet[1];
                List<UploadEhsExistingPatientItem> objs = new List<UploadEhsExistingPatientItem>();
                sets[0] = new MapResultSet(typeof(UploadEhsExistingPatientItem), objs);
                UploadEhsExistingPatientItem obj = null;
                try
                {
                    manager
                        .SetSpCommand("spUploadEhsExistingPatientItem_SelectList"
                            , manager.Parameter("@idfsUploadEhs", idfsUploadEhs)
                            , manager.Parameter("@LangID", ModelUserContext.CurrentLanguage)
                            
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
    
        
        
            internal void _SetupLoad(DbManagerProxy manager, UploadEhsExistingPatientItem obj, bool bCloning = false)
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
            
            internal void _SetPermitions(IObjectPermissions permissions, UploadEhsExistingPatientItem obj)
            {
                if (obj != null)
                {
                    obj._permissions = permissions;
                    if (obj._permissions != null)
                    {
                    
                    }
                }
            }

    

            private UploadEhsExistingPatientItem _CreateNew(DbManagerProxy manager, IObject Parent, int? HACode, on_action creating, on_action created, bool isFake = false)
            {
                UploadEhsExistingPatientItem obj = null;
                try
                {
                    obj = UploadEhsExistingPatientItem.CreateInstance();
                    obj.m_CS = m_CS;
                    obj.m_IsNew = true;
                    obj.Parent = Parent;
                    
                    if (creating != null)
                        creating(obj);
                
                    // creating extenters - begin
                    // creating extenters - end
                
                    _LoadLookups(manager, obj);
                    _SetupHandlers(obj);
                    _SetupChildHandlers(obj, null);
                    
                    obj._SetupMainHandler();
                    obj._setParent();
                
                    // created extenters - begin
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

            
            public UploadEhsExistingPatientItem CreateNewT(DbManagerProxy manager, IObject Parent, int? HACode = null)
            {
                return _CreateNew(manager, Parent, HACode, null, null);
            }
            public IObject CreateNew(DbManagerProxy manager, IObject Parent, int? HACode = null)
            {
                return _CreateNew(manager, Parent, HACode, null, null);
            }
            public UploadEhsExistingPatientItem CreateFakeT(DbManagerProxy manager, IObject Parent, int? HACode = null)
            {
                return _CreateNew(manager, Parent, HACode, null, null, true);
            }
            public IObject CreateFake(DbManagerProxy manager, IObject Parent, int? HACode = null)
            {
                return _CreateNew(manager, Parent, HACode, null, null, true);
            }
            
            public UploadEhsExistingPatientItem CreateWithParamsT(DbManagerProxy manager, IObject Parent, List<object> pars)
            {
                return _CreateNew(manager, Parent, null, null, null);
            }
            public IObject CreateWithParams(DbManagerProxy manager, IObject Parent, List<object> pars)
            {
                return _CreateNew(manager, Parent, null, null, null);
            }
            
            private void _SetupChildHandlers(UploadEhsExistingPatientItem obj, object newobj)
            {
                
            }
            
            private void _SetupHandlers(UploadEhsExistingPatientItem obj)
            {
                
            }
    

            internal void _LoadLookups(DbManagerProxy manager, UploadEhsExistingPatientItem obj)
            {
                
            }
    
            public bool DeleteObject(DbManagerProxy manager, object ident)
            {
                IObject obj = SelectDetail(manager, ident);
                if (!obj.MarkToDelete()) return false;
                return Post(manager, obj);
            }
        
            public bool Post(DbManagerProxy manager, IObject obj, bool bChildObject = false) 
            {
                throw new NotImplementedException();
            }
            
      
            protected ValidationModelException ChainsValidate(UploadEhsExistingPatientItem obj)
            {
                
                return null;
            }
            protected bool ChainsValidate(UploadEhsExistingPatientItem obj, bool bRethrowException)
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
                return Validate(manager, obj as UploadEhsExistingPatientItem, bPostValidation, bChangeValidation, bDeepValidation, bRethrowException);
            }
            public bool Validate(DbManagerProxy manager, UploadEhsExistingPatientItem obj, bool bPostValidation, bool bChangeValidation, bool bDeepValidation, bool bRethrowException = false)
            {
                if (!ChainsValidate(obj, bRethrowException))
                    return false;
                    
                
                return true;
            }
           
    
            private void _SetupRequired(UploadEhsExistingPatientItem obj)
            {
            
            }
    
    private void _SetupPersonalDataRestrictions(UploadEhsExistingPatientItem obj)
    {
    
    
    }
  
            #region IObjectMeta
            public int? MaxSize(string name) { return Meta.Sizes.ContainsKey(name) ? (int?)Meta.Sizes[name] : null; }
            public bool RequiredByField(string name, IObject obj) { return Meta.RequiredByField.ContainsKey(name) ? Meta.RequiredByField[name](obj as UploadEhsExistingPatientItem) : false; }
            public bool RequiredByProperty(string name, IObject obj) { return Meta.RequiredByProperty.ContainsKey(name) ? Meta.RequiredByProperty[name](obj as UploadEhsExistingPatientItem) : false; }
            public List<SearchPanelMetaItem> SearchPanelMeta { get { return Meta.SearchPanelMeta; } }
            public List<GridMetaItem> GridMeta { get { return Meta.GridMeta; } }
            public List<ActionMetaItem> Actions { get { return Meta.Actions; } }
            public string DetailPanel { get { return "UploadEhsExistingPatientItemDetail"; } }
            public string HelpIdWin { get { return ""; } }
            public string HelpIdWeb { get { return ""; } }
            public string HelpIdHh { get { return ""; } }
            public string SqlSortOrder { get { return Meta.sqlSortOrder; } }
            #endregion
    
        }

        
        #region Meta
        public static class Meta
        {
            public static string spSelect = "spUploadEhsExistingPatientItem_SelectList";
            public static string spCount = "";
            public static string spPost = "";
            public static string spInsert = "";
            public static string spUpdate = "";
            public static string spDelete = "";
            public static string spCanDelete = "";
            public static string sqlSortOrder = "";
            public static Dictionary<string, int> Sizes = new Dictionary<string, int>();
            public static Dictionary<string, Func<UploadEhsExistingPatientItem, bool>> RequiredByField = new Dictionary<string, Func<UploadEhsExistingPatientItem, bool>>();
            public static Dictionary<string, Func<UploadEhsExistingPatientItem, bool>> RequiredByProperty = new Dictionary<string, Func<UploadEhsExistingPatientItem, bool>>();
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
                
                Sizes.Add(_str_strPatientName, 400);
                Sizes.Add(_str_patient_id, 100);
                Sizes.Add(_str_patient_id_EIDSS, 100);
                Sizes.Add(_str_patient_id_EHS, 100);
                Sizes.Add(_str_last_name, 200);
                Sizes.Add(_str_last_name_EIDSS, 200);
                Sizes.Add(_str_last_name_EHS, 200);
                Sizes.Add(_str_first_name, 200);
                Sizes.Add(_str_first_name_EIDSS, 200);
                Sizes.Add(_str_first_name_EHS, 200);
                Sizes.Add(_str_second_name, 200);
                Sizes.Add(_str_second_name_EIDSS, 200);
                Sizes.Add(_str_second_name_EHS, 200);
                Sizes.Add(_str_gender, 100);
                Sizes.Add(_str_gender_EIDSS, 2000);
                Sizes.Add(_str_gender_EHS, 100);
                Sizes.Add(_str_phones, 200);
                Sizes.Add(_str_phones_EIDSS, 200);
                Sizes.Add(_str_phones_EHS, 200);
                Sizes.Add(_str_address_zip, 200);
                Sizes.Add(_str_address_zip_EIDSS, 200);
                Sizes.Add(_str_address_zip_EHS, 200);
                Sizes.Add(_str_address_area, 2000);
                Sizes.Add(_str_address_area_EIDSS, 2000);
                Sizes.Add(_str_address_area_EHS, 2000);
                Sizes.Add(_str_address_region, 2000);
                Sizes.Add(_str_address_region_EIDSS, 2000);
                Sizes.Add(_str_address_region_EHS, 2000);
                Sizes.Add(_str_address_settlement, 2000);
                Sizes.Add(_str_address_settlement_EIDSS, 2000);
                Sizes.Add(_str_address_settlement_EHS, 2000);
                Sizes.Add(_str_address_street, 200);
                Sizes.Add(_str_address_street_EIDSS, 200);
                Sizes.Add(_str_address_street_EHS, 200);
                Sizes.Add(_str_address_building, 200);
                Sizes.Add(_str_address_building_EIDSS, 200);
                Sizes.Add(_str_address_building_EHS, 200);
                Sizes.Add(_str_address_apartment, 200);
                Sizes.Add(_str_address_apartment_EIDSS, 200);
                Sizes.Add(_str_address_apartment_EHS, 200);
                Sizes.Add(_str_address_type, 200);
                Sizes.Add(_str_address_type_EIDSS, 200);
                Sizes.Add(_str_address_type_EHS, 200);
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
	